using SCWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SqlSugar;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Reflection;

namespace SCWeb.Controllers
{
    public class ImageController : BaseController
    {
        // GET: Image
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult IndexList()
        {
            var page = int.Parse(Request["page"] ?? "1");
            var limit = int.Parse(Request["limit"] ?? "10");
            DataTable newDataTable = SqlHelper.SelectTable(sqlimg);
            newDataTable.Columns.Add("Imgurl");
            //string imgurl = "";
            foreach (DataRow dr1 in newDataTable.Rows)
            {
                if (dr1["SimpleImage"].ToString().Length > 0)
                {
                    byte[] photo = new byte[0];
                    photo = (byte[])dr1["SimpleImage"];
                    string path = Server.MapPath("~/Upload").TrimEnd('\\') + @"\";
                    FileStream fs = new FileStream(path + dr1["MasterID"].ToString() + ".jpg", System.IO.FileMode.Create);
                    BinaryWriter bw = new BinaryWriter(fs);
                    bw.Write(photo, 0, photo.Length);
                    fs.Flush();
                    fs.Close();
                    dr1["Imgurl"] = "/Upload/" + dr1["MasterID"].ToString() + ".jpg";

                }
            }
            newDataTable.Columns.Remove(newDataTable.Columns["SimpleImage"]);
            newDataTable.Columns.Remove(newDataTable.Columns["MasterID"]);
            IList<View_modelIMG> list = ModelConvertHelper<View_modelIMG>.ConvertToModel(newDataTable);
            return Json(new { code = 0, msg = "", count = list.Count(), data = list.Skip((page - 1) * limit).Take(limit) }, JsonRequestBehavior.AllowGet);
        }
        public class ModelConvertHelper<T> where T : new()
        {
            public static IList<T> ConvertToModel(DataTable dt)
            {
                // 定义集合    
                IList<T> ts = new List<T>();

                // 获得此模型的类型   
                Type type = typeof(T);
                string tempName = "";

                foreach (DataRow dr in dt.Rows)
                {
                    T t = new T();
                    // 获得此模型的公共属性      
                    PropertyInfo[] propertys = t.GetType().GetProperties();
                    foreach (PropertyInfo pi in propertys)
                    {
                        tempName = pi.Name;  // 检查DataTable是否包含此列    

                        if (dt.Columns.Contains(tempName))
                        {
                            // 判断此属性是否有Setter      
                            if (!pi.CanWrite) continue;

                            object value = dr[tempName];
                            if (value != DBNull.Value)
                                pi.SetValue(t, value, null);
                        }
                    }
                    ts.Add(t);
                }
                return ts;
            }
        }
    }
}