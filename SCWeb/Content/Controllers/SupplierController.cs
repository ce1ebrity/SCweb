using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Newtonsoft.Json.Converters;
using SCWeb.Helper;
using SCWeb.Models;
using Newtonsoft.Json;
using System.IO;
using NPOI.HSSF.UserModel;

namespace SCWeb.Controllers
{
    public class SupplierController : Controller
    {
        // GET: Supplier

        /// <summary>
        /// 供应商加载
        /// </summary>
        /// <returns></returns>
        public ActionResult SupplierIndex()
        {
            return View();
        }
        /// <summary>
        /// 供应商修改添加的界面,编辑前绑定数据
        /// </summary>
        /// <returns></returns>
        public ActionResult SupplierAddUpdate(string id)
        {
            string sql = "select * from BPM_GYSGCGL where id=@id ";
            DataTable dt = SqlHelper.SelectTable(sql, new SqlParameter("@id", id));
            if (dt.Rows.Count > 0)
            {
                #region 用Viewbag装数据前台调用
                ViewBag.GYSGC01 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC01"].ToString()) == true ? "" : dt.Rows[0]["GYSGC01"].ToString();
                ViewBag.GYSGC02 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC02"].ToString()) == true ? "0" : dt.Rows[0]["GYSGC02"].ToString();
                ViewBag.GYSGC03 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC03"].ToString()) == true ? "" : dt.Rows[0]["GYSGC03"].ToString();
                ViewBag.GYSGC04 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC04"].ToString()) == true ? "" : dt.Rows[0]["GYSGC04"].ToString();
                ViewBag.GYSGC05 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC05"].ToString()) == true ? "" : dt.Rows[0]["GYSGC05"].ToString();
                ViewBag.GYSGC06 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC06"].ToString()) == true ? "" : dt.Rows[0]["GYSGC06"].ToString();
                ViewBag.GYSGC07 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC07"].ToString()) == true ? "" : dt.Rows[0]["GYSGC07"].ToString();
                ViewBag.GYSGC08 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC08"].ToString()) == true ? "" : dt.Rows[0]["GYSGC08"].ToString();
                ViewBag.GYSGC09 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC09"].ToString()) == true ? "" : dt.Rows[0]["GYSGC09"].ToString();
                ViewBag.GYSGC10 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC10"].ToString()) == true ? "" : dt.Rows[0]["GYSGC10"].ToString();
                ViewBag.GYSGC11 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC11"].ToString()) == true ? "" : dt.Rows[0]["GYSGC11"].ToString();
                ViewBag.GYSGC12 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC12"].ToString()) == true ? "" : dt.Rows[0]["GYSGC12"].ToString();
                ViewBag.GYSGC13 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC13"].ToString()) == true ? "" : dt.Rows[0]["GYSGC13"].ToString();
                ViewBag.GYSGC14 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC14"].ToString()) == true ? "" : dt.Rows[0]["GYSGC14"].ToString();
                ViewBag.GYSGC15 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC15"].ToString()) == true ? "" : dt.Rows[0]["GYSGC15"].ToString();
                ViewBag.GYSGC16 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC16"].ToString()) == true ? "" : dt.Rows[0]["GYSGC16"].ToString();
                ViewBag.GYSGC17 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC17"].ToString()) == true ? "" : dt.Rows[0]["GYSGC17"].ToString();
                ViewBag.GYSGC18 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC18"].ToString()) == true ? "" : dt.Rows[0]["GYSGC18"].ToString();
                ViewBag.GYSGC19 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC19"].ToString()) == true ? "" : dt.Rows[0]["GYSGC19"].ToString();
                ViewBag.GYSGC20 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC20"].ToString()) == true ? "" : dt.Rows[0]["GYSGC20"].ToString();
                ViewBag.GYSGC21 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC21"].ToString()) == true ? "" : dt.Rows[0]["GYSGC21"].ToString();
                ViewBag.GYSGC22 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC22"].ToString()) == true ? "" : dt.Rows[0]["GYSGC22"].ToString();
                ViewBag.GYSGC23 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC23"].ToString()) == true ? "" : dt.Rows[0]["GYSGC23"].ToString();
                ViewBag.GYSGC24 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC24"].ToString()) == true ? "" : dt.Rows[0]["GYSGC24"].ToString();
                ViewBag.GYSGC25 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC25"].ToString()) == true ? "" : dt.Rows[0]["GYSGC25"].ToString();
                ViewBag.GYSGC26 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC26"].ToString()) == true ? "" : dt.Rows[0]["GYSGC26"].ToString();
                ViewBag.GYSGC27 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC27"].ToString()) == true ? "" : dt.Rows[0]["GYSGC27"].ToString();
                ViewBag.GYSGC28 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC28"].ToString()) == true ? "" : dt.Rows[0]["GYSGC28"].ToString();
                ViewBag.GYSGC29 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC29"].ToString()) == true ? "" : dt.Rows[0]["GYSGC29"].ToString();
                ViewBag.GYSGC30 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC30"].ToString()) == true ? "" : dt.Rows[0]["GYSGC30"].ToString();
                ViewBag.GYSGC31 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC31"].ToString()) == true ? "" : dt.Rows[0]["GYSGC31"].ToString();
                ViewBag.GYSGC32 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC32"].ToString()) == true ? "" : dt.Rows[0]["GYSGC32"].ToString();
                ViewBag.GYSGC33 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC33"].ToString()) == true ? "0" : dt.Rows[0]["GYSGC33"].ToString();
                ViewBag.GYSGC34 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC34"].ToString()) == true ? "0" : dt.Rows[0]["GYSGC34"].ToString();
                ViewBag.GYSGC35 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC35"].ToString()) == true ? "" : dt.Rows[0]["GYSGC35"].ToString();
                ViewBag.GYSGC36 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC36"].ToString()) == true ? "" : dt.Rows[0]["GYSGC36"].ToString();
                ViewBag.GYSGC37 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC37"].ToString()) == true ? "" : dt.Rows[0]["GYSGC37"].ToString();
                ViewBag.GYSGC38 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC38"].ToString()) == true ? "" : dt.Rows[0]["GYSGC38"].ToString();
                ViewBag.GYSGC39 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC39"].ToString()) == true ? "" : dt.Rows[0]["GYSGC39"].ToString();
                ViewBag.GYSGC40 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC40"].ToString()) == true ? "" : dt.Rows[0]["GYSGC40"].ToString();
                ViewBag.GYSGC41 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC41"].ToString()) == true ? "" : dt.Rows[0]["GYSGC41"].ToString();
                ViewBag.GYSGC42 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC42"].ToString()) == true ? "" : dt.Rows[0]["GYSGC42"].ToString();
                ViewBag.GYSGC43 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC43"].ToString()) == true ? "" : dt.Rows[0]["GYSGC43"].ToString();
                ViewBag.GYSGC44 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC44"].ToString()) == true ? "" : dt.Rows[0]["GYSGC44"].ToString();
                ViewBag.GYSGC45 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC45"].ToString()) == true ? "" : dt.Rows[0]["GYSGC45"].ToString();
                ViewBag.GYSGC46 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC46"].ToString()) == true ? "" : dt.Rows[0]["GYSGC46"].ToString();
                ViewBag.GYSGC47 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC47"].ToString()) == true ? "" : dt.Rows[0]["GYSGC47"].ToString();
                ViewBag.GYSGC48 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC48"].ToString()) == true ? "" : dt.Rows[0]["GYSGC48"].ToString();
                ViewBag.GYSGC50 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC50"].ToString()) == true ? "" : dt.Rows[0]["GYSGC50"].ToString();
                #endregion
            }
            else
            {
                #region 用Viewbag装数据前台调用
                ViewBag.GYSGC01 = "";
                ViewBag.GYSGC02 = "0";
                ViewBag.GYSGC03 = "";
                ViewBag.GYSGC04 = "";
                ViewBag.GYSGC05 = "";
                ViewBag.GYSGC06 = "";
                ViewBag.GYSGC07 = "";
                ViewBag.GYSGC08 = "";
                ViewBag.GYSGC09 = "";
                ViewBag.GYSGC10 = "";
                ViewBag.GYSGC11 = "";
                ViewBag.GYSGC12 = "";
                ViewBag.GYSGC13 = "";
                ViewBag.GYSGC14 = "";
                ViewBag.GYSGC15 = "";
                ViewBag.GYSGC16 = "";
                ViewBag.GYSGC17 = "";
                ViewBag.GYSGC18 = "";
                ViewBag.GYSGC19 = "";
                ViewBag.GYSGC20 = "";
                ViewBag.GYSGC21 = "";
                ViewBag.GYSGC22 = "";
                ViewBag.GYSGC23 = "";
                ViewBag.GYSGC24 = "";
                ViewBag.GYSGC25 = "";
                ViewBag.GYSGC26 = "";
                ViewBag.GYSGC27 = "";
                ViewBag.GYSGC28 = "";
                ViewBag.GYSGC29 = "";
                ViewBag.GYSGC30 = "";
                ViewBag.GYSGC31 = "";
                ViewBag.GYSGC32 = "";
                ViewBag.GYSGC33 = "0";
                ViewBag.GYSGC34 = "0";
                ViewBag.GYSGC35 = "";
                ViewBag.GYSGC36 = "";
                ViewBag.GYSGC37 = "";
                ViewBag.GYSGC38 = "";
                ViewBag.GYSGC39 = "";
                ViewBag.GYSGC40 = "";
                ViewBag.GYSGC41 = "";
                ViewBag.GYSGC42 = "";
                ViewBag.GYSGC43 = "";
                ViewBag.GYSGC44 = "";
                ViewBag.GYSGC45 = "";
                ViewBag.GYSGC46 = "";
                ViewBag.GYSGC47 = "";
                ViewBag.GYSGC48 = "";
                ViewBag.GYSGC50 = "";
                #endregion
            }
            ViewBag.id = id;
            return View();
        }

        /// <summary>
        /// 供应商table绑定数据以及模糊查询的方法
        /// </summary>
        /// <param name="page">当前页</param>
        /// <param name="ShowPageCount">一页显示数</param>
        /// <returns></returns>
        public ActionResult GetGYSGCGLIndex(string page, string ShowPageCount)
        {
            int RowsCount = 0;//总行数
            int pageCount = 0;//总页数
            var GYSGC01 = Request["GYSGC01"];
            var GYSGC02 = Request["GYSGC02"];
            var GYSGC03 = Request["GYSGC03"];
            var GYSGC04 = Request["GYSGC04"];
            //修改时间格式
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy'-'MM'-'dd" };

            //StringBuilder:可增长的字符串数组
            StringBuilder sb = new StringBuilder("1=1");
            if (GYSGC01 != "0")
            {
                sb.Append(" and GYSGC01='" + GYSGC01 + "'");
            }
            if (GYSGC02 != "0")
            {
                sb.Append(" and GYSGC02='" + GYSGC02 + "'");
            }
            if (GYSGC03 != "")
            {
                sb.Append(" and GYSGC03 like'%" + GYSGC03 + "%' ");
            }
            if (GYSGC04 != "0")
            {
                sb.Append(" and GYSGC04='" + GYSGC04 + "'");
            }
            sb.Append(" and status=1");
            sb.Append(" and GYSGC49=1");

            try
            {
                DataTable dt = Common.GetSQLProcList(out RowsCount, out pageCount, "BPM_GYSGCGL", " *,(select GYSGCTypeName from dbo.BPM_GYSGCGLLB s2 where BPM_GYSGCGL.GYSGC02=s2.id ) as 类别 ", "id", ShowPageCount, page, " id desc ", sb.ToString());
                List<BPM_GYSGCGLModels> GYSGCGLList = new List<BPM_GYSGCGLModels>();
                #region 循环查询数据
                foreach (DataRow item in dt.Rows)
                {
                    BPM_GYSGCGLModels model = new BPM_GYSGCGLModels()
                    {
                        id = item["id"].ToString(),
                        GYSGC01 = item["GYSGC01"].ToString(),
                        GYSGC02 = item["类别"].ToString(),
                        GYSGC03 = item["GYSGC03"].ToString(),
                        GYSGC04 = item["GYSGC04"].ToString(),
                        GYSGC05 = item["GYSGC05"].ToString(),
                        GYSGC06 = item["GYSGC06"].ToString(),
                        GYSGC07 = item["GYSGC07"].ToString(),
                        GYSGC08 = item["GYSGC08"].ToString(),
                        GYSGC09 = item["GYSGC09"].ToString(),
                        GYSGC10 = item["GYSGC10"].ToString(),
                        GYSGC11 = item["GYSGC11"].ToString(),
                        GYSGC12 = item["GYSGC12"].ToString(),
                        GYSGC13 = item["GYSGC13"].ToString(),
                        GYSGC14 = item["GYSGC14"].ToString(),
                        GYSGC15 = item["GYSGC15"].ToString(),
                        GYSGC16 = item["GYSGC16"].ToString(),
                        GYSGC17 = item["GYSGC17"].ToString(),
                        GYSGC18 = item["GYSGC18"].ToString(),
                        GYSGC19 = item["GYSGC19"].ToString(),
                        GYSGC20 = item["GYSGC20"].ToString(),
                        GYSGC21 = item["GYSGC21"].ToString(),
                        GYSGC22 = item["GYSGC22"].ToString(),
                        GYSGC23 = item["GYSGC23"].ToString(),
                        GYSGC24 = item["GYSGC24"].ToString(),
                        GYSGC25 = item["GYSGC25"].ToString(),
                        GYSGC26 = item["GYSGC26"].ToString(),
                        GYSGC27 = item["GYSGC27"].ToString(),
                        GYSGC28 = item["GYSGC28"].ToString(),
                        GYSGC29 = item["GYSGC29"].ToString(),
                        GYSGC30 = item["GYSGC30"].ToString(),
                        GYSGC31 = item["GYSGC31"].ToString(),
                        GYSGC32 = string.IsNullOrWhiteSpace(item["GYSGC32"].ToString()) == true ? "" : item["GYSGC32"].ToString(),
                        GYSGC33 = item["GYSGC33"].ToString(),
                        GYSGC34 = item["GYSGC34"].ToString(),
                        GYSGC35 = item["GYSGC35"].ToString(),
                        GYSGC36 = item["GYSGC36"].ToString(),
                        GYSGC37 = item["GYSGC37"].ToString(),
                        GYSGC38 = item["GYSGC38"].ToString(),
                        GYSGC39 = item["GYSGC39"].ToString(),
                        GYSGC40 = item["GYSGC40"].ToString(),
                        GYSGC41 = string.IsNullOrWhiteSpace(item["GYSGC41"].ToString()) == true ? "" : item["GYSGC41"].ToString(),
                        GYSGC42 = string.IsNullOrWhiteSpace(item["GYSGC42"].ToString()) == true ? "" : item["GYSGC42"].ToString(),
                        GYSGC43 = string.IsNullOrWhiteSpace(item["GYSGC43"].ToString()) == true ? "" : item["GYSGC43"].ToString(),
                        GYSGC44 = string.IsNullOrWhiteSpace(item["GYSGC44"].ToString()) == true ? "" : item["GYSGC44"].ToString(),
                        GYSGC45 = string.IsNullOrWhiteSpace(item["GYSGC45"].ToString()) == true ? "" : item["GYSGC45"].ToString(),
                        GYSGC46 = item["GYSGC46"].ToString(),
                        GYSGC47 = item["GYSGC47"].ToString(),
                        GYSGC48 = item["GYSGC48"].ToString(),
                        GYSGC49 = string.IsNullOrWhiteSpace(item["GYSGC49"].ToString()) == true ? "0" : item["GYSGC49"].ToString(),
                        GYSGC50 = item["GYSGC50"].ToString(),
                        PageCount = pageCount.ToString(),
                        RowsCount = RowsCount.ToString(),
                    };
                    GYSGCGLList.Add(model);
                }
                #endregion

                return Content(JsonConvert.SerializeObject(GYSGCGLList));
            }
            catch (Exception ex)
            {
                return Content("error");
            }

        }

        /// <summary>
        /// table绑定供应商产品明细表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetGYSGCGLMXIndex(string id)
        {
            try
            {
                string sql = "select * from BPM_GYSGCGLMX where GYSGCID=@id and status=1";
                DataTable dt = SqlHelper.SelectTable(sql, new SqlParameter("@id", id));
                List<BPM_GYSGCGLMXModels> GYSGCGLMXlist = new List<BPM_GYSGCGLMXModels>();
                foreach (DataRow item in dt.Rows)
                {
                    BPM_GYSGCGLMXModels model = new BPM_GYSGCGLMXModels()
                    {
                        id = item["id"].ToString(),
                        GYSGCID = item["GYSGCID"].ToString(),
                        GYSGCMX01 = item["GYSGCMX01"].ToString(),
                        GYSGCMX02 = item["GYSGCMX02"].ToString(),
                        GYSGCMX03 = item["GYSGCMX03"].ToString(),
                        GYSGCMX04 = item["GYSGCMX04"].ToString(),
                        GYSGCMX05 = item["GYSGCMX05"].ToString(),
                        GYSGCMX06 = item["GYSGCMX06"].ToString(),
                    };
                    GYSGCGLMXlist.Add(model);
                }
                return Content(JsonConvert.SerializeObject(GYSGCGLMXlist));
            }
            catch (Exception ex)
            {
                return Content("error");
            }
        }

        /// <summary>
        /// 供应商编辑和添加的方法以及供应商产品明细添加和编辑的方法
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        [Property(MenuCode = "BPM_GYSGCGL", MenuOperation = "面辅料供应商添加和编辑")]
        public ActionResult EditGYSGCGL(BPM_GYSGCGLModels models)
        {
            try
            {
                var Arr = Request["MXArr"];
                string[] MXid = Request["MXid"].Split(',');
                int MXnum = int.Parse(Request["MXnum"]);
                string[] MXArr = Arr.Split('|');

                #region 判断供应商代码是否重复
                string dsql = "select * from BPM_GYSGCGL where GYSGC50=@GYSGC50 and id!=@id";
                DataTable dt50 = SqlHelper.SelectTable(dsql, new SqlParameter("@GYSGC50", models.GYSGC50), new SqlParameter("id", models.id));
                #endregion

                ///--判断什么时候可以执行“供应商添加与编辑”以及“产品明细添加与编辑”,
                #region 判断条件
                var mxFK = 0;//验证供应商产品明细非空 返回的结果
                for (int i = 0; i < MXnum; i++)
                {
                    string[] mxInfo = MXArr[i].Split(',');
                    if (string.IsNullOrWhiteSpace(mxInfo[0]) == true && string.IsNullOrWhiteSpace(mxInfo[1]) == true && string.IsNullOrWhiteSpace(mxInfo[2]) == true && string.IsNullOrWhiteSpace(mxInfo[3]) == true && string.IsNullOrWhiteSpace(mxInfo[4]) == true && string.IsNullOrWhiteSpace(mxInfo[5]) == true)
                    {
                        mxFK = 2;
                    }
                    else
                    {
                        //验证
                        #region 验证非空
                        if (string.IsNullOrWhiteSpace(mxInfo[0]))
                        {
                            return Content("MessageZY");//验证主要产品非空
                        }
                        else if (string.IsNullOrWhiteSpace(mxInfo[1]))
                        {
                            return Content("MessageGG");//验证产品规格非空
                        }
                        else if (string.IsNullOrWhiteSpace(mxInfo[2]))
                        {
                            return Content("MessageJG");//验证价格范围非空
                        }
                        else if (string.IsNullOrWhiteSpace(mxInfo[3]))
                        {
                            return Content("MessageNCL");//验证年产量非空
                        }
                        else if (string.IsNullOrWhiteSpace(mxInfo[4]))
                        {
                            return Content("MessageBL");//验证占总产量的比例%非空
                        }
                        else if (string.IsNullOrWhiteSpace(mxInfo[5]))
                        {
                            return Content("MessageDX");//验证主要销售对象及地区非空
                        }
                        #endregion
                        else
                        {
                            mxFK = 1;
                        }
                    }
                }
                #endregion

                //var aaa = mxInfo[0];
                int count = 0;
                if (models.id != "0")//进行编辑操作
                {
                    if (mxFK != 0)//执行面辅料供应商编辑
                    {
                        if (dt50.Rows.Count == 0)
                        {
                            #region 修改供应商  SQL语句
                            string sql = "update BPM_GYSGCGL set GYSGC01 =@GYSGC01,GYSGC02 = @GYSGC02,GYSGC03 = @GYSGC03,GYSGC04 = @GYSGC04,GYSGC05 = @GYSGC05,GYSGC06 = @GYSGC06,GYSGC07 = @GYSGC07,GYSGC08 = @GYSGC08,GYSGC09 = @GYSGC09,GYSGC10 = @GYSGC10,GYSGC11 = @GYSGC11,GYSGC12 = @GYSGC12,GYSGC20 = @GYSGC20,GYSGC21 = @GYSGC21,GYSGC22 = @GYSGC22,GYSGC23 = @GYSGC23,GYSGC24 = @GYSGC24,GYSGC25 = @GYSGC25,GYSGC26 = @GYSGC26,GYSGC27 = @GYSGC27,GYSGC28 = @GYSGC28,GYSGC29 = @GYSGC29,GYSGC30 = @GYSGC30,GYSGC31 = @GYSGC31,GYSGC32 = @GYSGC32,GYSGC33 = @GYSGC33,GYSGC34 = @GYSGC34,GYSGC35 = @GYSGC35,GYSGC36 = @GYSGC36,GYSGC37 = @GYSGC37,GYSGC38 = @GYSGC38,GYSGC39 = @GYSGC39,GYSGC40 = @GYSGC40,GYSGC41 = @GYSGC41,GYSGC42 = @GYSGC42,GYSGC43 = @GYSGC43,GYSGC44 = @GYSGC44,GYSGC45 = @GYSGC45,GYSGC46 = @GYSGC46,GYSGC47 = @GYSGC47,GYSGC48 = @GYSGC48,GYSGC50 = @GYSGC50 where id =@id";
                            #endregion
                            SqlParameter[] param = new SqlParameter[]
                            {
                         #region 参数赋值
                            new SqlParameter("@GYSGC01", string.IsNullOrWhiteSpace(models.GYSGC01) == true ? "" : models.GYSGC01),
                            new SqlParameter("@GYSGC02", string.IsNullOrWhiteSpace(models.GYSGC02) == true ? "0" : models.GYSGC02),
                            new SqlParameter("@GYSGC03", string.IsNullOrWhiteSpace(models.GYSGC03) == true ? "" : models.GYSGC03),
                            new SqlParameter("@GYSGC04", string.IsNullOrWhiteSpace(models.GYSGC04) == true ? "" : models.GYSGC04),
                            new SqlParameter("@GYSGC05", string.IsNullOrWhiteSpace(models.GYSGC05) == true ? "" : models.GYSGC05),
                            new SqlParameter("@GYSGC06", string.IsNullOrWhiteSpace(models.GYSGC06) == true ? "" : models.GYSGC06),
                            new SqlParameter("@GYSGC07", string.IsNullOrWhiteSpace(models.GYSGC07) == true ? "" : models.GYSGC07),
                            new SqlParameter("@GYSGC08", string.IsNullOrWhiteSpace(models.GYSGC08) == true ? "" : models.GYSGC08),
                            new SqlParameter("@GYSGC09", string.IsNullOrWhiteSpace(models.GYSGC09) == true ? "" : models.GYSGC09),
                            new SqlParameter("@GYSGC10", string.IsNullOrWhiteSpace(models.GYSGC10) == true ? "" : models.GYSGC10),
                            new SqlParameter("@GYSGC11", string.IsNullOrWhiteSpace(models.GYSGC11) == true ? "" : models.GYSGC11),
                            new SqlParameter("@GYSGC12", string.IsNullOrWhiteSpace(models.GYSGC12) == true ? "" : models.GYSGC12),
                            new SqlParameter("@GYSGC20", string.IsNullOrWhiteSpace(models.GYSGC20) == true ? "" : models.GYSGC20),
                            new SqlParameter("@GYSGC21", string.IsNullOrWhiteSpace(models.GYSGC21) == true ? "" : models.GYSGC21),
                            new SqlParameter("@GYSGC22", string.IsNullOrWhiteSpace(models.GYSGC22) == true ? "" : models.GYSGC22),
                            new SqlParameter("@GYSGC23", string.IsNullOrWhiteSpace(models.GYSGC23) == true ? "" : models.GYSGC23),
                            new SqlParameter("@GYSGC24", string.IsNullOrWhiteSpace(models.GYSGC24) == true ? "" : models.GYSGC24),
                            new SqlParameter("@GYSGC25", string.IsNullOrWhiteSpace(models.GYSGC25) == true ? "" : models.GYSGC25),
                            new SqlParameter("@GYSGC26", string.IsNullOrWhiteSpace(models.GYSGC26) == true ? "" : models.GYSGC26),
                            new SqlParameter("@GYSGC27", string.IsNullOrWhiteSpace(models.GYSGC27) == true ? "" : models.GYSGC27),
                            new SqlParameter("@GYSGC28", string.IsNullOrWhiteSpace(models.GYSGC28) == true ? "" : models.GYSGC28),
                            new SqlParameter("@GYSGC29", string.IsNullOrWhiteSpace(models.GYSGC29) == true ? "" : models.GYSGC29),
                            new SqlParameter("@GYSGC30", string.IsNullOrWhiteSpace(models.GYSGC30) == true ? "" : models.GYSGC30),
                            new SqlParameter("@GYSGC31", string.IsNullOrWhiteSpace(models.GYSGC31) == true ? "" : models.GYSGC31),
                            new SqlParameter("@GYSGC32", string.IsNullOrWhiteSpace(models.GYSGC32) == true ? "" : models.GYSGC32),
                            new SqlParameter("@GYSGC33", string.IsNullOrWhiteSpace(models.GYSGC33) == true ? "" : models.GYSGC33),
                            new SqlParameter("@GYSGC34", string.IsNullOrWhiteSpace(models.GYSGC34) == true ? "" : models.GYSGC34),
                            new SqlParameter("@GYSGC35", string.IsNullOrWhiteSpace(models.GYSGC35) == true ? "" : models.GYSGC35),
                            new SqlParameter("@GYSGC36", string.IsNullOrWhiteSpace(models.GYSGC36) == true ? "" : models.GYSGC36),
                            new SqlParameter("@GYSGC37", string.IsNullOrWhiteSpace(models.GYSGC37) == true ? "" : models.GYSGC37),
                            new SqlParameter("@GYSGC38", string.IsNullOrWhiteSpace(models.GYSGC38) == true ? "" : models.GYSGC38),
                            new SqlParameter("@GYSGC39", string.IsNullOrWhiteSpace(models.GYSGC39) == true ? "" : models.GYSGC39),
                            new SqlParameter("@GYSGC40", string.IsNullOrWhiteSpace(models.GYSGC40) == true ? "" : models.GYSGC40),
                            new SqlParameter("@GYSGC41", string.IsNullOrWhiteSpace(models.GYSGC41) == true ? "" : models.GYSGC41),
                            new SqlParameter("@GYSGC42", string.IsNullOrWhiteSpace(models.GYSGC42) == true ? "" : models.GYSGC42),
                            new SqlParameter("@GYSGC43", string.IsNullOrWhiteSpace(models.GYSGC43) == true ? "" : models.GYSGC43),
                            new SqlParameter("@GYSGC44", string.IsNullOrWhiteSpace(models.GYSGC44) == true ? "" : models.GYSGC44),
                            new SqlParameter("@GYSGC45", string.IsNullOrWhiteSpace(models.GYSGC45) == true ? "" : models.GYSGC45),
                            new SqlParameter("@GYSGC46", string.IsNullOrWhiteSpace(models.GYSGC46) == true ? "" : models.GYSGC46),
                            new SqlParameter("@GYSGC47", string.IsNullOrWhiteSpace(models.GYSGC47) == true ? "" : models.GYSGC47),
                            new SqlParameter("@GYSGC48", string.IsNullOrWhiteSpace(models.GYSGC48) == true ? "" : models.GYSGC48),
                            new SqlParameter("@GYSGC50", string.IsNullOrWhiteSpace(models.GYSGC50) == true ? "" : models.GYSGC50),
                            new SqlParameter("@id", models.id),
                                #endregion
                            };
                            count = SqlHelper.InsertDelUpdate(sql, param);//供应商编辑
                            if (count > 0)//当面辅料供应商编辑成功时
                            {
                                if (mxFK == 1)//执行面辅料供应商产品明细编辑
                                {
                                    for (int i = 0; i < MXnum; i++)
                                    {
                                        string[] mxInfo = MXArr[i].Split(',');
                                        string sqlMX = "";
                                        if (MXid[i] != "0")//进行编辑操作
                                        {
                                            #region 修改供应商品明细  SQL语句
                                            sqlMX = "update BPM_GYSGCGLMX set GYSGCMX01 =@GYSGCMX01,GYSGCMX02 = @GYSGCMX02,GYSGCMX03 = @GYSGCMX03,GYSGCMX04 = @GYSGCMX04,GYSGCMX05 = @GYSGCMX05,GYSGCMX06 = @GYSGCMX06 where id =@MXid";
                                            #endregion

                                        }
                                        else//当MXid[i]==0时说明用户想进行添加该编辑供应商的产品明细
                                        {
                                            #region 添加供应商品明细  SQL语句
                                            sqlMX = "insert into BPM_GYSGCGLMX(GYSGCID,GYSGCMX01,GYSGCMX02,GYSGCMX03,GYSGCMX04,GYSGCMX05,GYSGCMX06) values(@GYSGCID,@GYSGCMX01, @GYSGCMX02,@GYSGCMX03,@GYSGCMX04,@GYSGCMX05,@GYSGCMX06)";
                                            #endregion
                                        }
                                        SqlParameter[] paramMx = new SqlParameter[]
                                            {
                                        #region 供应商品明细参数赋值
                                            new SqlParameter("@GYSGCMX01", string.IsNullOrWhiteSpace(mxInfo[0]) == true ? "" : mxInfo[0]),
                                            new SqlParameter("@GYSGCMX02", string.IsNullOrWhiteSpace(mxInfo[1]) == true ? "" : mxInfo[1]),
                                            new SqlParameter("@GYSGCMX03", string.IsNullOrWhiteSpace(mxInfo[2]) == true ? "" : mxInfo[2]),
                                            new SqlParameter("@GYSGCMX04", string.IsNullOrWhiteSpace(mxInfo[3]) == true ? "" : mxInfo[3]),
                                            new SqlParameter("@GYSGCMX05", string.IsNullOrWhiteSpace(mxInfo[4]) == true ? "" : mxInfo[4]),
                                            new SqlParameter("@GYSGCMX06", string.IsNullOrWhiteSpace(mxInfo[5]) == true ? "" : mxInfo[5]),
                                            new SqlParameter("@MXid", MXid[i]),//供应商产品ID
                                            new SqlParameter("@GYSGCID", models.id),//供应商ID
                                                #endregion
                                            };
                                        count = SqlHelper.InsertDelUpdate(sqlMX, paramMx);//供应商产品明细编辑

                                    }
                                }
                            }
                        }
                        else
                        {
                            return Content("Repeat");//输出重复
                        }
                    }
                }
                else//进行添加操作
                {
                    if (mxFK != 0)//执行面辅料供应商添加
                    {
                        if (dt50.Rows.Count == 0)
                        {
                            #region 添加供应商  SQL语句
                            string sql = "insert into BPM_GYSGCGL(GYSGC01,GYSGC02,GYSGC03,GYSGC04,GYSGC05,GYSGC06,GYSGC07,GYSGC08,GYSGC09,GYSGC10,GYSGC11,GYSGC12,GYSGC13,GYSGC14,GYSGC15,GYSGC16,GYSGC17,GYSGC18,GYSGC19,GYSGC20,GYSGC21,GYSGC22,GYSGC23,GYSGC24,GYSGC25,GYSGC26,GYSGC27,GYSGC28,GYSGC29,GYSGC30,GYSGC31,GYSGC32,GYSGC33,GYSGC34,GYSGC35,GYSGC36,GYSGC37,GYSGC38,GYSGC39,GYSGC40,GYSGC41,GYSGC42,GYSGC43,GYSGC44,GYSGC45,GYSGC46,GYSGC47,GYSGC48,GYSGC49,GYSGC50) values(@GYSGC01,@GYSGC02,@GYSGC03,@GYSGC04,@GYSGC05,@GYSGC06,@GYSGC07,@GYSGC08,@GYSGC09,@GYSGC10,@GYSGC11,@GYSGC12,@GYSGC13,@GYSGC14,@GYSGC15,@GYSGC16,@GYSGC17,@GYSGC18,@GYSGC19,@GYSGC20,@GYSGC21,@GYSGC22,@GYSGC23,@GYSGC24,@GYSGC25,@GYSGC26,@GYSGC27,@GYSGC28,@GYSGC29,@GYSGC30,@GYSGC31,@GYSGC32,@GYSGC33,@GYSGC34,@GYSGC35,@GYSGC36,@GYSGC37,@GYSGC38,@GYSGC39,@GYSGC40,@GYSGC41,@GYSGC42,@GYSGC43,@GYSGC44,@GYSGC45,@GYSGC46,@GYSGC47,@GYSGC48,@GYSGC49,@GYSGC50);select @topid=@@identity";//select @topid=@@identity找出新插入一行的ID
                            #endregion
                            SqlParameter[] param = new SqlParameter[]
                            {
                         #region 参数赋值
                        new SqlParameter("@topid", SqlDbType.Int),//最新添加的ID
                        new SqlParameter("@GYSGC01", string.IsNullOrWhiteSpace(models.GYSGC01) == true ? "" : models.GYSGC01),
                        new SqlParameter("@GYSGC02", string.IsNullOrWhiteSpace(models.GYSGC02) == true ? "0" : models.GYSGC02),
                        new SqlParameter("@GYSGC03", string.IsNullOrWhiteSpace(models.GYSGC03) == true ? "" : models.GYSGC03),
                        new SqlParameter("@GYSGC04", string.IsNullOrWhiteSpace(models.GYSGC04) == true ? "" : models.GYSGC04),
                        new SqlParameter("@GYSGC05", string.IsNullOrWhiteSpace(models.GYSGC05) == true ? "" : models.GYSGC05),
                        new SqlParameter("@GYSGC06", string.IsNullOrWhiteSpace(models.GYSGC06) == true ? "" : models.GYSGC06),
                        new SqlParameter("@GYSGC07", string.IsNullOrWhiteSpace(models.GYSGC07) == true ? "" : models.GYSGC07),
                        new SqlParameter("@GYSGC08", string.IsNullOrWhiteSpace(models.GYSGC08) == true ? "" : models.GYSGC08),
                        new SqlParameter("@GYSGC09", string.IsNullOrWhiteSpace(models.GYSGC09) == true ? "" : models.GYSGC09),
                        new SqlParameter("@GYSGC10", string.IsNullOrWhiteSpace(models.GYSGC10) == true ? "" : models.GYSGC10),
                        new SqlParameter("@GYSGC11", string.IsNullOrWhiteSpace(models.GYSGC11) == true ? "" : models.GYSGC11),
                        new SqlParameter("@GYSGC12", string.IsNullOrWhiteSpace(models.GYSGC12) == true ? "" : models.GYSGC12),
                        new SqlParameter("@GYSGC13", ""),
                        new SqlParameter("@GYSGC14", ""),
                        new SqlParameter("@GYSGC15", ""),
                        new SqlParameter("@GYSGC16", ""),
                        new SqlParameter("@GYSGC17", ""),
                        new SqlParameter("@GYSGC18", ""),
                        new SqlParameter("@GYSGC19", ""),
                        new SqlParameter("@GYSGC20", string.IsNullOrWhiteSpace(models.GYSGC20) == true ? "" : models.GYSGC20),
                        new SqlParameter("@GYSGC21", string.IsNullOrWhiteSpace(models.GYSGC21) == true ? "" : models.GYSGC21),
                        new SqlParameter("@GYSGC22", string.IsNullOrWhiteSpace(models.GYSGC22) == true ? "" : models.GYSGC22),
                        new SqlParameter("@GYSGC23", string.IsNullOrWhiteSpace(models.GYSGC23) == true ? "" : models.GYSGC23),
                        new SqlParameter("@GYSGC24", string.IsNullOrWhiteSpace(models.GYSGC24) == true ? "" : models.GYSGC24),
                        new SqlParameter("@GYSGC25", string.IsNullOrWhiteSpace(models.GYSGC25) == true ? "" : models.GYSGC25),
                        new SqlParameter("@GYSGC26", string.IsNullOrWhiteSpace(models.GYSGC26) == true ? "" : models.GYSGC26),
                        new SqlParameter("@GYSGC27", string.IsNullOrWhiteSpace(models.GYSGC27) == true ? "" : models.GYSGC27),
                        new SqlParameter("@GYSGC28", string.IsNullOrWhiteSpace(models.GYSGC28) == true ? "" : models.GYSGC28),
                        new SqlParameter("@GYSGC29", string.IsNullOrWhiteSpace(models.GYSGC29) == true ? "" : models.GYSGC29),
                        new SqlParameter("@GYSGC30", string.IsNullOrWhiteSpace(models.GYSGC30) == true ? "" : models.GYSGC30),
                        new SqlParameter("@GYSGC31", string.IsNullOrWhiteSpace(models.GYSGC31) == true ? "" : models.GYSGC31),
                        new SqlParameter("@GYSGC32", string.IsNullOrWhiteSpace(models.GYSGC32) == true ? "" : models.GYSGC32),
                        new SqlParameter("@GYSGC33", string.IsNullOrWhiteSpace(models.GYSGC33) == true ? "" : models.GYSGC33),
                        new SqlParameter("@GYSGC34", string.IsNullOrWhiteSpace(models.GYSGC34) == true ? "" : models.GYSGC34),
                        new SqlParameter("@GYSGC35", string.IsNullOrWhiteSpace(models.GYSGC35) == true ? "" : models.GYSGC35),
                        new SqlParameter("@GYSGC36", string.IsNullOrWhiteSpace(models.GYSGC36) == true ? "" : models.GYSGC36),
                        new SqlParameter("@GYSGC37", string.IsNullOrWhiteSpace(models.GYSGC37) == true ? "" : models.GYSGC37),
                        new SqlParameter("@GYSGC38", string.IsNullOrWhiteSpace(models.GYSGC38) == true ? "" : models.GYSGC38),
                        new SqlParameter("@GYSGC39", string.IsNullOrWhiteSpace(models.GYSGC39) == true ? "" : models.GYSGC39),
                        new SqlParameter("@GYSGC40", string.IsNullOrWhiteSpace(models.GYSGC40) == true ? "" : models.GYSGC40),
                        new SqlParameter("@GYSGC41", string.IsNullOrWhiteSpace(models.GYSGC41) == true ? "" : models.GYSGC41),
                        new SqlParameter("@GYSGC42", string.IsNullOrWhiteSpace(models.GYSGC42) == true ? "" : models.GYSGC42),
                        new SqlParameter("@GYSGC43", string.IsNullOrWhiteSpace(models.GYSGC43) == true ? "" : models.GYSGC43),
                        new SqlParameter("@GYSGC44", string.IsNullOrWhiteSpace(models.GYSGC44) == true ? "" : models.GYSGC44),
                        new SqlParameter("@GYSGC45", string.IsNullOrWhiteSpace(models.GYSGC45) == true ? "" : models.GYSGC45),
                        new SqlParameter("@GYSGC46", string.IsNullOrWhiteSpace(models.GYSGC46) == true ? "" : models.GYSGC46),
                        new SqlParameter("@GYSGC47", string.IsNullOrWhiteSpace(models.GYSGC47) == true ? "" : models.GYSGC47),
                        new SqlParameter("@GYSGC48", string.IsNullOrWhiteSpace(models.GYSGC48) == true ? "" : models.GYSGC48),
                        new SqlParameter("@GYSGC49", "1"),
                        new SqlParameter("@GYSGC50", string.IsNullOrWhiteSpace(models.GYSGC50) == true ? "" : models.GYSGC50),
                                #endregion
                            };
                            param[0].Direction = ParameterDirection.Output;
                            count = SqlHelper.InsertDelUpdate(sql, param);
                            int rId = Convert.ToInt32(param[0].Value == null ? 0 : Convert.ToInt32(param[0].Value));
                            if (count > 0)//当面辅料供应商添加成功时
                            {
                                if (mxFK == 1)//执行面辅料供应商产品明细添加
                                {
                                    #region 添加供应商品明细  SQL语句
                                    string sqlMX = "insert into BPM_GYSGCGLMX(GYSGCID,GYSGCMX01,GYSGCMX02,GYSGCMX03,GYSGCMX04,GYSGCMX05,GYSGCMX06) values(@GYSGCID,@GYSGCMX01, @GYSGCMX02,@GYSGCMX03,@GYSGCMX04,@GYSGCMX05,@GYSGCMX06)";
                                    #endregion
                                    for (int i = 0; i < MXnum; i++)
                                    {
                                        string[] mxInfo = MXArr[i].Split(',');
                                        SqlParameter[] paramMx = new SqlParameter[]
                                        {
                                    #region 供应商产品明细参数赋值
                                    new SqlParameter("@GYSGCID", rId),
                                    new SqlParameter("@GYSGCMX01", string.IsNullOrWhiteSpace(mxInfo[0]) == true ? "" : mxInfo[0]),
                                    new SqlParameter("@GYSGCMX02", string.IsNullOrWhiteSpace(mxInfo[1]) == true ? "" : mxInfo[1]),
                                    new SqlParameter("@GYSGCMX03", string.IsNullOrWhiteSpace(mxInfo[2]) == true ? "" : mxInfo[2]),
                                    new SqlParameter("@GYSGCMX04", string.IsNullOrWhiteSpace(mxInfo[3]) == true ? "" : mxInfo[3]),
                                    new SqlParameter("@GYSGCMX05", string.IsNullOrWhiteSpace(mxInfo[4]) == true ? "" : mxInfo[4]),
                                    new SqlParameter("@GYSGCMX06", string.IsNullOrWhiteSpace(mxInfo[5]) == true ? "" : mxInfo[5]),
                                            #endregion
                                        };
                                        count = SqlHelper.InsertDelUpdate(sqlMX, paramMx);//供应商产品明细添加
                                    }
                                }
                            }
                        }
                        else
                        {
                            return Content("Repeat");//输出重复
                        }
                    }
                }
                if (count > 0)
                {
                    return Content("success");
                }
                else
                {
                    return Content("null");
                }
            }

            catch (Exception ex)
            {
                return Content("error");
            }
        }

        /// <summary>
        /// 修改意见的方法
        /// </summary>
        /// <returns></returns>
        [Property(MenuCode = "BPM_GYSGCGL", MenuOperation = "面辅料供应商修改意见")]
        public ActionResult UpdateOpinion()
        {
            var id = Request["id"];
            var str = Request["str"];
            //int index = Request["index"];
            string[] Arr = str.Split(new char[] { ',' });
            string sql = "update BPM_GYSGCGL set GYSGC13=@GYSGC13,GYSGC14=@GYSGC14,GYSGC15=@GYSGC15,GYSGC16=@GYSGC16,GYSGC17=@GYSGC17,GYSGC18=@GYSGC18,GYSGC19=@GYSGC19 where id=@id";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@GYSGC13",Arr[0]),
                new SqlParameter("@GYSGC14",Arr[1]),
                new SqlParameter("@GYSGC15",Arr[2]),
                new SqlParameter("@GYSGC16",Arr[3]),
                new SqlParameter("@GYSGC17",Arr[4]),
                new SqlParameter("@GYSGC18",Arr[5]),
                new SqlParameter("@GYSGC19",Arr[6]),
                new SqlParameter("@id",id)
            };
            int count = SqlHelper.InsertDelUpdate(sql, param);
            if (count > 0)
            {
                return Content("success");
            }
            else
            {
                return Content("error");
            }
        }

        /// <summary>
        /// 绑定产品类别下拉框
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDDLType()
        {
            var LBid = Request["LBid"];
            string sql = "";
            if (LBid == "1")
            {
                sql = "select * from BPM_GYSGCGLLB where status=1";
            }
            else
            {
                sql = "select * from BPM_GYSGCGLLB where status=3";
            }
            DataTable dt = SqlHelper.SelectTable(sql);
            return Content(JsonConvert.SerializeObject(dt));
        }

        /// <summary>
        /// 供应商删除
        /// </summary>
        /// <returns></returns>
        [Property(MenuCode = "BPM_GYSGCGL", MenuOperation = "面辅料供应商删除")]
        public ActionResult DelGYSGCGL()
        {
            var id = Request["id"];
            id = id.Substring(0, id.Length - 1);
            string sql = "update BPM_GYSGCGL set status=0 where id in (" + id + ")";
            int count = SqlHelper.InsertDelUpdate(sql);
            if (count > 0)
            {
                return Content("success");
            }
            else
            {
                return Content("error");
            }
        }

        /// <summary>
        /// 供应商产品明细删除
        /// </summary>
        /// <returns></returns>
        [Property(MenuCode = "BPM_GYSGCGLMX", MenuOperation = "面辅料供应商产品明细删除")]
        public ActionResult DelGYSGCGLMX()
        {
            var id = Request["id"];
            string sql = "update BPM_GYSGCGLMX set status=0 where id=@id";
            int count = SqlHelper.InsertDelUpdate(sql, new SqlParameter("@id", id));
            if (count > 0)
            {
                return Content("success");
            }
            else
            {
                return Content("error");
            }
        }

        /// <summary>
        /// Excel导出
        /// </summary>
        /// <returns></returns>
        //[Property(MenuCode = "BPM_GYSGCGL", MenuOperation = "面辅料供应商Excel导出")]
        public ActionResult ExportExcel()
        {
            string sql = "select GYSGC01 as '面料/辅料',(select GYSGCTypeName from dbo.BPM_GYSGCGLLB s2 where BPM_GYSGCGL.GYSGC02=s2.id ) as 类别,GYSGC03 as 供应商名称,GYSGC50 as 供应商代码,GYSGC04 as 经营性质,GYSGC05 as 开发能力,GYSGC06 as 生产能力,GYSGC07 as 质量管控能力,GYSGC08 as 配合度,GYSGC09 as 价位,GYSGC10 as 贷款结算方式,GYSGC11 as '是否与品牌目标合作（是/否）',GYSGC12 as 采购部初步合作定位,GYSGC13 as '项目配合度（强/中/弱）',GYSGC14 as '项目开发能力（强/中/弱）',GYSGC15 as '项目是否可配合（是/否）',GYSGC16 as '生管配合度（强/中/弱）',GYSGC17 as '生管货期（强/中/弱）',GYSGC18 as '生管是否可配合（是/否）',GYSGC19 as '综合评定是否合作（是/否）',GYSGC20 as 评定等级,GYSGC21 as 地址,GYSGC22 as 法人代表,GYSGC23 as 法人代表职务,GYSGC24 as 法人代表电话,GYSGC25 as 业务代表,GYSGC26 as 业务代表职务,GYSGC27 as 业务代表电话,GYSGC28 as '企业形态（国有/民营/外资/合资/其它）',GYSGC29 as 注册资本,GYSGC30 as '业务种类(生产、贸易、批发、其它)',GYSGC31 as 厂房面积,GYSGC32 as 人员规模,GYSGC33 as 加工所占比例,GYSGC34 as 自营经销所占比例,GYSGC35 as 通过何种认证,GYSGC36 as 年总产量,GYSGC37 as 检验标准,GYSGC38 as 所用染化料,GYSGC39 as 国内主要合作品牌,GYSGC40 as 国际主要合作品牌,GYSGC41 as 打色周期,GYSGC42 as 放样周期有胚布,GYSGC43 as 放样周期无胚布,GYSGC44 as 大货生产周期有胚布,GYSGC45 as 大货生产周期无胚布,GYSGC46 as '纱/胚布品种及产地',GYSGC47 as 生产设备品种及数量,GYSGC48 as 综合评估 from BPM_GYSGCGL where status<>0 and GYSGC49=1";
            HSSFWorkbook book = GetExcel(sql);

            // 写入到客户端    
            return WriteInClient(book);
        }

        /// <summary>
        /// Excel模板导出
        /// </summary>
        /// <returns></returns>
        public ActionResult ExportTExcel()
        {
            string sql = "select GYSGC01 as '面料/辅料',GYSGC02 as 类别,GYSGC03 as 供应商名称,GYSGC50 as 供应商代码,GYSGC04 as 经营性质,GYSGC05 as 开发能力,GYSGC06 as 生产能力,GYSGC07 as 质量管控能力,GYSGC08 as 配合度,GYSGC09 as 价位,GYSGC10 as 贷款结算方式,GYSGC11 as '是否与品牌目标合作（是/否）',GYSGC12 as 采购部初步合作定位,GYSGC13 as '项目配合度（强/中/弱）',GYSGC14 as '项目开发能力（强/中/弱）',GYSGC15 as '项目是否可配合（是/否）',GYSGC16 as '生管配合度（强/中/弱）',GYSGC17 as '生管货期（强/中/弱）',GYSGC18 as '生管是否可配合（是/否）',GYSGC19 as '综合评定是否合作（是/否）',GYSGC20 as 评定等级,GYSGC21 as 地址,GYSGC22 as 法人代表,GYSGC23 as 法人代表职务,GYSGC24 as 法人代表电话,GYSGC25 as 业务代表,GYSGC26 as 业务代表职务,GYSGC27 as 业务代表电话,GYSGC28 as '企业形态（国有/民营/外资/合资/其它）',GYSGC29 as 注册资本,GYSGC30 as '业务种类(生产、贸易、批发、其它)',GYSGC31 as 厂房面积,GYSGC32 as 人员规模,GYSGC33 as 加工所占比例,GYSGC34 as 自营经销所占比例,GYSGC35 as 通过何种认证,GYSGC36 as 年总产量,GYSGC37 as 检验标准,GYSGC38 as 所用染化料,GYSGC39 as 国内主要合作品牌,GYSGC40 as 国际主要合作品牌,GYSGC41 as 打色周期,GYSGC42 as 放样周期有胚布,GYSGC43 as 放样周期无胚布,GYSGC44 as 大货生产周期有胚布,GYSGC45 as 大货生产周期无胚布,GYSGC46 as '纱/胚布品种及产地',GYSGC47 as 生产设备品种及数量,GYSGC48 as 综合评估 from BPM_GYSGCGL  where 1!=1";
            HSSFWorkbook book = GetExcel(sql);

            // 写入到客户端    
            return WriteInClient(book);
        }

        /// <summary>
        /// Excel数据导入
        /// </summary>
        /// <returns></returns>
        [Property(MenuCode = "BPM_GYSGCGL", MenuOperation = "面辅料供应商Excel导入")]
        public ActionResult ImportGysExcel(HttpPostedFileBase filed)
        {
            //Common.SaveExcelFile(filed);
            string filePath = SaveAsLoed(filed);
            string sql = "INSERT INTO BPM_GYSGCGL(GYSGC01,GYSGC02,GYSGC03,GYSGC04,GYSGC05,GYSGC06,GYSGC07,GYSGC08,GYSGC09,GYSGC10,GYSGC11,GYSGC12,GYSGC13,GYSGC14,GYSGC15,GYSGC16,GYSGC17,GYSGC18,GYSGC19,GYSGC20,GYSGC21,GYSGC22,GYSGC23,GYSGC24,GYSGC25,GYSGC26,GYSGC27,GYSGC28,GYSGC29,GYSGC30,GYSGC31,GYSGC32,GYSGC33,GYSGC34,GYSGC35,GYSGC36,GYSGC37,GYSGC38,GYSGC39,GYSGC40,GYSGC41,GYSGC42,GYSGC43,GYSGC44,GYSGC45,GYSGC46,GYSGC47,GYSGC48,GYSGC49,GYSGC50) VALUES(@GYSGC01,@GYSGC02,@GYSGC03,@GYSGC04,@GYSGC05,@GYSGC06,@GYSGC07,@GYSGC08,@GYSGC09,@GYSGC10,@GYSGC11,@GYSGC12,@GYSGC13,@GYSGC14,@GYSGC15,@GYSGC16,@GYSGC17,@GYSGC18,@GYSGC19,@GYSGC20,@GYSGC21,@GYSGC22,@GYSGC23,@GYSGC24,@GYSGC25,@GYSGC26,@GYSGC27,@GYSGC28,@GYSGC29,@GYSGC30,@GYSGC31,@GYSGC32,@GYSGC33,@GYSGC34,@GYSGC35,@GYSGC36,@GYSGC37,@GYSGC38,@GYSGC39,@GYSGC40,@GYSGC41,@GYSGC42,@GYSGC43,@GYSGC44,@GYSGC45,@GYSGC46,@GYSGC47,@GYSGC48,@GYSGC49,@GYSGC50)";
            try
            {
                DataTable dt = Common.ExcelToDataTable(filePath, true, 0);
                foreach (DataRow dr in dt.Rows)
                {
                    string sqlLB = "select id from dbo.BPM_GYSGCGLLB where GYSGCTypeName=@GYSGCTypeName";
                    var LBid = SqlHelper.SelectSinger(sqlLB, new SqlParameter("@GYSGCTypeName", dr["类别"]));
                    if (Convert.ToInt32(LBid) > 0)
                    {
                        SqlParameter[] param = new SqlParameter[]
                        {
                            //循环参数赋值
                            #region 循环参数赋值
                            new SqlParameter("@GYSGC01",dr["面料/辅料"]),
                            new SqlParameter("@GYSGC02",LBid),
                            new SqlParameter("@GYSGC03",dr["供应商名称"]),
                            new SqlParameter("@GYSGC04",dr["经营性质"]),
                            new SqlParameter("@GYSGC05",dr["开发能力"]),
                            new SqlParameter("@GYSGC06",dr["生产能力"]),
                            new SqlParameter("@GYSGC07",dr["质量管控能力"]),
                            new SqlParameter("@GYSGC08",dr["配合度"]),
                            new SqlParameter("@GYSGC09",dr["价位"]),
                            new SqlParameter("@GYSGC10",dr["贷款结算方式"]),
                            new SqlParameter("@GYSGC11",dr["是否与品牌目标合作（是/否）"]),
                            new SqlParameter("@GYSGC12",dr["采购部初步合作定位"]),
                            new SqlParameter("@GYSGC13",dr["项目配合度（强/中/弱）"]),
                            new SqlParameter("@GYSGC14",dr["项目开发能力（强/中/弱）"]),
                            new SqlParameter("@GYSGC15",dr["项目是否可配合（是/否）"]),
                            new SqlParameter("@GYSGC16",dr["生管配合度（强/中/弱）"]),
                            new SqlParameter("@GYSGC17",dr["生管货期（强/中/弱）"]),
                            new SqlParameter("@GYSGC18",dr["生管是否可配合（是/否）"]),
                            new SqlParameter("@GYSGC19",dr["综合评定是否合作（是/否）"]),
                            new SqlParameter("@GYSGC20",dr["评定等级"]),
                            new SqlParameter("@GYSGC21",dr["地址"]),
                            new SqlParameter("@GYSGC22",dr["法人代表"]),
                            new SqlParameter("@GYSGC23",dr["法人代表职务"]),
                            new SqlParameter("@GYSGC24",dr["法人代表电话"]),
                            new SqlParameter("@GYSGC25",dr["业务代表"]),
                            new SqlParameter("@GYSGC26",dr["业务代表职务"]),
                            new SqlParameter("@GYSGC27",dr["业务代表电话"]),
                            new SqlParameter("@GYSGC28",dr["企业形态（国有/民营/外资/合资/其它）"]),
                            new SqlParameter("@GYSGC29",dr["注册资本"]),
                            new SqlParameter("@GYSGC30",dr["业务种类(生产、贸易、批发、其它)"]),
                            new SqlParameter("@GYSGC31",dr["厂房面积"]),
                            new SqlParameter("@GYSGC32",dr["人员规模"]),
                            new SqlParameter("@GYSGC33",dr["加工所占比例"]),
                            new SqlParameter("@GYSGC34",dr["自营经销所占比例"]),
                            new SqlParameter("@GYSGC35",dr["通过何种认证"]),
                            new SqlParameter("@GYSGC36",dr["年总产量"]),
                            new SqlParameter("@GYSGC37",dr["检验标准"]),
                            new SqlParameter("@GYSGC38",dr["所用染化料"]),
                            new SqlParameter("@GYSGC39",dr["国内主要合作品牌"]),
                            new SqlParameter("@GYSGC40",dr["国际主要合作品牌"]),
                            new SqlParameter("@GYSGC41",string.IsNullOrWhiteSpace(dr["打色周期"].ToString()) == true ? "" : dr["打色周期"]),
                            new SqlParameter("@GYSGC42",string.IsNullOrWhiteSpace(dr["放样周期有胚布"].ToString()) == true ? "" : dr["放样周期有胚布"]),
                            new SqlParameter("@GYSGC43",string.IsNullOrWhiteSpace(dr["放样周期无胚布"].ToString()) == true ? "" : dr["放样周期无胚布"]),
                            new SqlParameter("@GYSGC44",string.IsNullOrWhiteSpace(dr["大货生产周期有胚布"].ToString()) == true ? "" : dr["大货生产周期有胚布"]),
                            new SqlParameter("@GYSGC45",string.IsNullOrWhiteSpace(dr["大货生产周期无胚布"].ToString()) == true ? "" : dr["大货生产周期无胚布"]),
                            new SqlParameter("@GYSGC46",dr["纱/胚布品种及产地"]),
                            new SqlParameter("@GYSGC47",dr["生产设备品种及数量"]),
                            new SqlParameter("@GYSGC48",dr["综合评估"]),
                            new SqlParameter("@GYSGC49","1"),
                            new SqlParameter("@GYSGC50",dr["供应商代码"]),
	                        #endregion
                        
                        };
                        SqlHelper.InsertDelUpdate(sql, param);
                    }
                }

            }
            catch (Exception ex)
            {
                return Content("上传失败请检查！");
            }
            return Content("上传成功");
        }

        //--------------------------------------------分割线------------------------------------------------------//

        /// <summary>
        /// 工厂加载
        /// </summary>
        /// <returns></returns>
        public ActionResult FactoryIndex()
        {
            return View();
        }

        /// <summary>
        /// 工厂修改添加,编辑前绑定数据
        /// </summary>
        /// <returns></returns>
        public ActionResult FactoryAddUpdate(string id)
        {
            string sql = "select * from BPM_GYSGCGL where id=@id ";
            DataTable dt = SqlHelper.SelectTable(sql, new SqlParameter("@id", id));
            if (dt.Rows.Count > 0)
            {
                #region 用Viewbag装数据前台调用
                //ViewBag.GYSGC01 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC01"].ToString()) == true ? "" : dt.Rows[0]["GYSGC01"].ToString();
                ViewBag.GYSGC02 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC02"].ToString()) == true ? "0" : dt.Rows[0]["GYSGC02"].ToString();
                ViewBag.GYSGC03 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC03"].ToString()) == true ? "" : dt.Rows[0]["GYSGC03"].ToString();
                ViewBag.GYSGC04 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC04"].ToString()) == true ? "" : dt.Rows[0]["GYSGC04"].ToString();
                ViewBag.GYSGC05 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC05"].ToString()) == true ? "" : dt.Rows[0]["GYSGC05"].ToString();
                ViewBag.GYSGC06 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC06"].ToString()) == true ? "" : dt.Rows[0]["GYSGC06"].ToString();
                ViewBag.GYSGC07 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC07"].ToString()) == true ? "" : dt.Rows[0]["GYSGC07"].ToString();
                ViewBag.GYSGC08 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC08"].ToString()) == true ? "" : dt.Rows[0]["GYSGC08"].ToString();
                ViewBag.GYSGC09 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC09"].ToString()) == true ? "" : dt.Rows[0]["GYSGC09"].ToString();
                ViewBag.GYSGC10 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC10"].ToString()) == true ? "" : dt.Rows[0]["GYSGC10"].ToString();
                ViewBag.GYSGC11 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC11"].ToString()) == true ? "" : dt.Rows[0]["GYSGC11"].ToString();
                ViewBag.GYSGC12 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC12"].ToString()) == true ? "" : dt.Rows[0]["GYSGC12"].ToString();
                ViewBag.GYSGC13 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC13"].ToString()) == true ? "" : dt.Rows[0]["GYSGC13"].ToString();
                ViewBag.GYSGC14 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC14"].ToString()) == true ? "" : dt.Rows[0]["GYSGC14"].ToString();
                ViewBag.GYSGC15 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC15"].ToString()) == true ? "" : dt.Rows[0]["GYSGC15"].ToString();
                ViewBag.GYSGC16 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC16"].ToString()) == true ? "" : dt.Rows[0]["GYSGC16"].ToString();
                ViewBag.GYSGC17 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC17"].ToString()) == true ? "" : dt.Rows[0]["GYSGC17"].ToString();
                ViewBag.GYSGC18 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC18"].ToString()) == true ? "" : dt.Rows[0]["GYSGC18"].ToString();
                ViewBag.GYSGC19 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC19"].ToString()) == true ? "" : dt.Rows[0]["GYSGC19"].ToString();
                ViewBag.GYSGC20 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC20"].ToString()) == true ? "" : dt.Rows[0]["GYSGC20"].ToString();
                ViewBag.GYSGC21 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC21"].ToString()) == true ? "" : dt.Rows[0]["GYSGC21"].ToString();
                ViewBag.GYSGC22 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC22"].ToString()) == true ? "" : dt.Rows[0]["GYSGC22"].ToString();
                ViewBag.GYSGC23 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC23"].ToString()) == true ? "" : dt.Rows[0]["GYSGC23"].ToString();
                ViewBag.GYSGC24 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC24"].ToString()) == true ? "" : dt.Rows[0]["GYSGC24"].ToString();
                ViewBag.GYSGC25 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC25"].ToString()) == true ? "" : dt.Rows[0]["GYSGC25"].ToString();
                ViewBag.GYSGC26 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC26"].ToString()) == true ? "" : dt.Rows[0]["GYSGC26"].ToString();
                ViewBag.GYSGC27 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC27"].ToString()) == true ? "" : dt.Rows[0]["GYSGC27"].ToString();
                ViewBag.GYSGC28 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC28"].ToString()) == true ? "" : dt.Rows[0]["GYSGC28"].ToString();
                ViewBag.GYSGC29 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC29"].ToString()) == true ? "" : dt.Rows[0]["GYSGC29"].ToString();
                ViewBag.GYSGC30 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC30"].ToString()) == true ? "" : dt.Rows[0]["GYSGC30"].ToString();
                ViewBag.GYSGC31 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC31"].ToString()) == true ? "" : dt.Rows[0]["GYSGC31"].ToString();
                ViewBag.GYSGC32 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC32"].ToString()) == true ? "" : dt.Rows[0]["GYSGC32"].ToString();
                ViewBag.GYSGC33 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC33"].ToString()) == true ? "0" : dt.Rows[0]["GYSGC33"].ToString();
                ViewBag.GYSGC34 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC34"].ToString()) == true ? "0" : dt.Rows[0]["GYSGC34"].ToString();
                ViewBag.GYSGC35 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC35"].ToString()) == true ? "" : dt.Rows[0]["GYSGC35"].ToString();
                ViewBag.GYSGC36 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC36"].ToString()) == true ? "" : dt.Rows[0]["GYSGC36"].ToString();
                ViewBag.GYSGC37 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC37"].ToString()) == true ? "" : dt.Rows[0]["GYSGC37"].ToString();
                ViewBag.GYSGC38 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC38"].ToString()) == true ? "" : dt.Rows[0]["GYSGC38"].ToString();
                ViewBag.GYSGC39 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC39"].ToString()) == true ? "" : dt.Rows[0]["GYSGC39"].ToString();
                ViewBag.GYSGC40 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC40"].ToString()) == true ? "" : dt.Rows[0]["GYSGC40"].ToString();
                ViewBag.GYSGC41 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC41"].ToString()) == true ? "" : dt.Rows[0]["GYSGC41"].ToString();
                ViewBag.GYSGC42 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC42"].ToString()) == true ? "" : dt.Rows[0]["GYSGC42"].ToString();
                ViewBag.GYSGC43 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC43"].ToString()) == true ? "" : dt.Rows[0]["GYSGC43"].ToString();
                ViewBag.GYSGC44 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC44"].ToString()) == true ? "" : dt.Rows[0]["GYSGC44"].ToString();
                ViewBag.GYSGC45 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC45"].ToString()) == true ? "" : dt.Rows[0]["GYSGC45"].ToString();
                //ViewBag.GYSGC46 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC46"].ToString()) == true ? "" : dt.Rows[0]["GYSGC46"].ToString();
                ViewBag.GYSGC47 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC47"].ToString()) == true ? "" : dt.Rows[0]["GYSGC47"].ToString();
                ViewBag.GYSGC48 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC48"].ToString()) == true ? "" : dt.Rows[0]["GYSGC48"].ToString();
                ViewBag.GYSGC50 = string.IsNullOrWhiteSpace(dt.Rows[0]["GYSGC50"].ToString()) == true ? "" : dt.Rows[0]["GYSGC50"].ToString();
                #endregion
            }
            else
            {
                ViewBag.GYSGC01 = "";
                ViewBag.GYSGC02 = "0";
                ViewBag.GYSGC03 = "";
                ViewBag.GYSGC04 = "";
                ViewBag.GYSGC05 = "";
                ViewBag.GYSGC06 = "";
                ViewBag.GYSGC07 = "";
                ViewBag.GYSGC08 = "";
                ViewBag.GYSGC09 = "";
                ViewBag.GYSGC10 = "";
                ViewBag.GYSGC11 = "";
                ViewBag.GYSGC12 = "";
                ViewBag.GYSGC13 = "";
                ViewBag.GYSGC14 = "";
                ViewBag.GYSGC15 = "";
                ViewBag.GYSGC16 = "";
                ViewBag.GYSGC17 = "";
                ViewBag.GYSGC18 = "";
                ViewBag.GYSGC19 = "";
                ViewBag.GYSGC20 = "";
                ViewBag.GYSGC21 = "";
                ViewBag.GYSGC22 = "";
                ViewBag.GYSGC23 = "";
                ViewBag.GYSGC24 = "";
                ViewBag.GYSGC25 = "";
                ViewBag.GYSGC26 = "";
                ViewBag.GYSGC27 = "";
                ViewBag.GYSGC28 = "";
                ViewBag.GYSGC29 = "";
                ViewBag.GYSGC30 = "";
                ViewBag.GYSGC31 = "";
                ViewBag.GYSGC32 = "";
                ViewBag.GYSGC33 = "0";
                ViewBag.GYSGC34 = "0";
                ViewBag.GYSGC35 = "";
                ViewBag.GYSGC36 = "";
                ViewBag.GYSGC37 = "";
                ViewBag.GYSGC38 = "";
                ViewBag.GYSGC39 = "";
                ViewBag.GYSGC40 = "";
                ViewBag.GYSGC41 = "";
                ViewBag.GYSGC42 = "";
                ViewBag.GYSGC43 = "";
                ViewBag.GYSGC44 = "";
                ViewBag.GYSGC45 = "";
                ViewBag.GYSGC46 = "";
                ViewBag.GYSGC47 = "";
                ViewBag.GYSGC48 = "";
                ViewBag.GYSGC50 = "";
            }
            ViewBag.id = id;
            return View();
        }

        /// <summary>
        /// 工厂table绑定数据以及模糊查询的方法
        /// </summary>
        /// <param name="page">当前页</param>
        /// <param name="ShowPageCount">一页显示数</param>
        /// <returns></returns>
        public ActionResult GetFactoryIndex(string page, string ShowPageCount)
        {
            int RowsCount = 0;//总行数
            int pageCount = 0;//总页数
            var GYSGC01 = Request["GYSGC01"];
            var GYSGC02 = Request["GYSGC02"];
            var GYSGC03 = Request["GYSGC03"];
            var GYSGC04 = Request["GYSGC04"];
            //修改时间格式
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy'-'MM'-'dd" };

            //StringBuilder:可增长的字符串数组
            StringBuilder sb = new StringBuilder("1=1");
            if (GYSGC01 != "0")
            {
                sb.Append(" and GYSGC01='" + GYSGC01 + "'");
            }
            if (GYSGC02 != "0")
            {
                sb.Append(" and GYSGC02='" + GYSGC02 + "'");
            }
            if (GYSGC03 != "")
            {
                sb.Append(" and GYSGC03 like'%" + GYSGC03 + "%' ");
            }
            if (GYSGC04 != "0")
            {
                sb.Append(" and GYSGC04='" + GYSGC04 + "'");
            }
            sb.Append(" and status=1");
            sb.Append(" and GYSGC49=2");

            try
            {
                DataTable dt = Common.GetSQLProcList(out RowsCount, out pageCount, "BPM_GYSGCGL", " *,(select GYSGCTypeName from dbo.BPM_GYSGCGLLB s2 where BPM_GYSGCGL.GYSGC02=s2.id ) as 类别 ", "id", ShowPageCount, page, " id desc ", sb.ToString());
                List<BPM_GYSGCGLModels> GYSGCGLList = new List<BPM_GYSGCGLModels>();
                #region 循环查询数据
                foreach (DataRow item in dt.Rows)
                {
                    BPM_GYSGCGLModels model = new BPM_GYSGCGLModels()
                    {
                        id = item["id"].ToString(),
                        GYSGC01 = item["GYSGC01"].ToString(),
                        GYSGC02 = item["类别"].ToString(),
                        GYSGC03 = item["GYSGC03"].ToString(),
                        GYSGC04 = item["GYSGC04"].ToString(),
                        GYSGC05 = item["GYSGC05"].ToString(),
                        GYSGC06 = item["GYSGC06"].ToString(),
                        GYSGC07 = item["GYSGC07"].ToString(),
                        GYSGC08 = item["GYSGC08"].ToString(),
                        GYSGC09 = item["GYSGC09"].ToString(),
                        GYSGC10 = item["GYSGC10"].ToString(),
                        GYSGC11 = item["GYSGC11"].ToString(),
                        GYSGC12 = item["GYSGC12"].ToString(),
                        GYSGC13 = item["GYSGC13"].ToString(),
                        GYSGC14 = item["GYSGC14"].ToString(),
                        GYSGC15 = item["GYSGC15"].ToString(),
                        GYSGC16 = item["GYSGC16"].ToString(),
                        GYSGC17 = item["GYSGC17"].ToString(),
                        GYSGC18 = item["GYSGC18"].ToString(),
                        GYSGC19 = item["GYSGC19"].ToString(),
                        GYSGC20 = item["GYSGC20"].ToString(),
                        GYSGC21 = item["GYSGC21"].ToString(),
                        GYSGC22 = item["GYSGC22"].ToString(),
                        GYSGC23 = item["GYSGC23"].ToString(),
                        GYSGC24 = item["GYSGC24"].ToString(),
                        GYSGC25 = item["GYSGC25"].ToString(),
                        GYSGC26 = item["GYSGC26"].ToString(),
                        GYSGC27 = item["GYSGC27"].ToString(),
                        GYSGC28 = item["GYSGC28"].ToString(),
                        GYSGC29 = item["GYSGC29"].ToString(),
                        GYSGC30 = item["GYSGC30"].ToString(),
                        GYSGC31 = item["GYSGC31"].ToString(),
                        GYSGC32 = string.IsNullOrWhiteSpace(item["GYSGC32"].ToString()) == true ? "" : item["GYSGC32"].ToString(),
                        GYSGC33 = item["GYSGC33"].ToString(),
                        GYSGC34 = item["GYSGC34"].ToString(),
                        GYSGC35 = item["GYSGC35"].ToString(),
                        GYSGC36 = item["GYSGC36"].ToString(),
                        GYSGC37 = item["GYSGC37"].ToString(),
                        GYSGC38 = item["GYSGC38"].ToString(),
                        GYSGC39 = item["GYSGC39"].ToString(),
                        GYSGC40 = item["GYSGC40"].ToString(),
                        GYSGC41 = string.IsNullOrWhiteSpace(item["GYSGC41"].ToString()) == true ? "" : item["GYSGC41"].ToString(),
                        GYSGC42 = string.IsNullOrWhiteSpace(item["GYSGC42"].ToString()) == true ? "" : item["GYSGC42"].ToString(),
                        GYSGC43 = string.IsNullOrWhiteSpace(item["GYSGC43"].ToString()) == true ? "" : item["GYSGC43"].ToString(),
                        GYSGC44 = string.IsNullOrWhiteSpace(item["GYSGC44"].ToString()) == true ? "" : item["GYSGC44"].ToString(),
                        GYSGC45 = string.IsNullOrWhiteSpace(item["GYSGC45"].ToString()) == true ? "" : item["GYSGC45"].ToString(),
                        GYSGC46 = item["GYSGC46"].ToString(),
                        GYSGC47 = item["GYSGC47"].ToString(),
                        GYSGC48 = item["GYSGC48"].ToString(),
                        GYSGC49 = string.IsNullOrWhiteSpace(item["GYSGC49"].ToString()) == true ? "0" : item["GYSGC49"].ToString(),
                        GYSGC50 = item["GYSGC50"].ToString(),
                        PageCount = pageCount.ToString(),
                        RowsCount = RowsCount.ToString(),
                    };
                    GYSGCGLList.Add(model);
                }
                #endregion

                return Content(JsonConvert.SerializeObject(GYSGCGLList));
            }
            catch (Exception ex)
            {
                return Content("error");
            }

        }

        /// <summary>
        /// 工厂编辑和添加的方法以及工厂明细添加和编辑的方法
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        [Property(MenuCode = "BPM_GYSGCGL", MenuOperation = "工厂添加和编辑")]
        public ActionResult EditFactoryInfo(BPM_GYSGCGLModels models)
        {
            try
            {

                var Arr = Request["MXArr"];
                string[] MXid = Request["MXid"].Split(',');
                int MXnum = int.Parse(Request["MXnum"]);
                string[] MXArr = Arr.Split('|');

                #region 判断供应商代码是否重复
                string dsql = "select * from BPM_GYSGCGL where GYSGC50=@GYSGC50 and id!=@id";
                DataTable dt50 = SqlHelper.SelectTable(dsql, new SqlParameter("@GYSGC50", models.GYSGC50), new SqlParameter("id", models.id));
                #endregion
                ///--判断什么时候可以执行“供应商添加与编辑”以及“产品明细添加与编辑”,
                #region 判断条件
                var mxFK = 0;//验证供应商产品明细非空 返回的结果
                for (int i = 0; i < MXnum; i++)
                {
                    string[] mxInfo = MXArr[i].Split(',');
                    if (string.IsNullOrWhiteSpace(mxInfo[0]) == true && string.IsNullOrWhiteSpace(mxInfo[1]) == true && string.IsNullOrWhiteSpace(mxInfo[2]) == true && string.IsNullOrWhiteSpace(mxInfo[3]) == true && string.IsNullOrWhiteSpace(mxInfo[4]) == true && string.IsNullOrWhiteSpace(mxInfo[5]) == true)
                    {
                        mxFK = 2;
                    }
                    else
                    {
                        //验证
                        #region 验证非空
                        if (string.IsNullOrWhiteSpace(mxInfo[0]))
                        {
                            return Content("MessageZY");//验证主要产品非空
                        }
                        else if (string.IsNullOrWhiteSpace(mxInfo[1]))
                        {
                            return Content("MessageGG");//验证产品规格非空
                        }
                        else if (string.IsNullOrWhiteSpace(mxInfo[2]))
                        {
                            return Content("MessageJG");//验证价格范围非空
                        }
                        else if (string.IsNullOrWhiteSpace(mxInfo[3]))
                        {
                            return Content("MessageNCL");//验证年产量非空
                        }
                        else if (string.IsNullOrWhiteSpace(mxInfo[4]))
                        {
                            return Content("MessageBL");//验证占总产量的比例%非空
                        }
                        else if (string.IsNullOrWhiteSpace(mxInfo[5]))
                        {
                            return Content("MessageDX");//验证主要销售对象及地区非空
                        }
                        #endregion
                        else
                        {
                            mxFK = 1;
                        }
                    }
                }
                #endregion

                //var aaa = mxInfo[0];
                int count = 0;
                if (models.id != "0")//进行编辑操作
                {
                    if (mxFK != 0)//执行面辅料供应商编辑
                    {
                        if (dt50.Rows.Count == 0)
                        {
                            #region 修改工厂  SQL语句
                            string sql = "update BPM_GYSGCGL set GYSGC02 = @GYSGC02,GYSGC03 = @GYSGC03,GYSGC04 = @GYSGC04,GYSGC05 = @GYSGC05,GYSGC06 = @GYSGC06,GYSGC07 = @GYSGC07,GYSGC08 = @GYSGC08,GYSGC09 = @GYSGC09,GYSGC10 = @GYSGC10,GYSGC11 = @GYSGC11,GYSGC12 = @GYSGC12,GYSGC20 = @GYSGC20,GYSGC21 = @GYSGC21,GYSGC22 = @GYSGC22,GYSGC23 = @GYSGC23,GYSGC24 = @GYSGC24,GYSGC25 = @GYSGC25,GYSGC26 = @GYSGC26,GYSGC27 = @GYSGC27,GYSGC28 = @GYSGC28,GYSGC29 = @GYSGC29,GYSGC30 = @GYSGC30,GYSGC31 = @GYSGC31,GYSGC32 = @GYSGC32,GYSGC33 = @GYSGC33,GYSGC34 = @GYSGC34,GYSGC35 = @GYSGC35,GYSGC36 = @GYSGC36,GYSGC37 = @GYSGC37,GYSGC38 = @GYSGC38,GYSGC39 = @GYSGC39,GYSGC40 = @GYSGC40,GYSGC41 = @GYSGC41,GYSGC42 = @GYSGC42,GYSGC43 = @GYSGC43,GYSGC44 = @GYSGC44,GYSGC45 = @GYSGC45,GYSGC47 = @GYSGC47,GYSGC48 = @GYSGC48,GYSGC50 = @GYSGC50 where id =@id";
                            #endregion
                            SqlParameter[] param = new SqlParameter[]
                            {
                         #region 参数赋值
                            //new SqlParameter("@GYSGC01", string.IsNullOrWhiteSpace(models.GYSGC01) == true ? "" : models.GYSGC01),
                            new SqlParameter("@GYSGC02", string.IsNullOrWhiteSpace(models.GYSGC02) == true ? "0" : models.GYSGC02),
                            new SqlParameter("@GYSGC03", string.IsNullOrWhiteSpace(models.GYSGC03) == true ? "" : models.GYSGC03),
                            new SqlParameter("@GYSGC04", string.IsNullOrWhiteSpace(models.GYSGC04) == true ? "" : models.GYSGC04),
                            new SqlParameter("@GYSGC05", string.IsNullOrWhiteSpace(models.GYSGC05) == true ? "" : models.GYSGC05),
                            new SqlParameter("@GYSGC06", string.IsNullOrWhiteSpace(models.GYSGC06) == true ? "" : models.GYSGC06),
                            new SqlParameter("@GYSGC07", string.IsNullOrWhiteSpace(models.GYSGC07) == true ? "" : models.GYSGC07),
                            new SqlParameter("@GYSGC08", string.IsNullOrWhiteSpace(models.GYSGC08) == true ? "" : models.GYSGC08),
                            new SqlParameter("@GYSGC09", string.IsNullOrWhiteSpace(models.GYSGC09) == true ? "" : models.GYSGC09),
                            new SqlParameter("@GYSGC10", string.IsNullOrWhiteSpace(models.GYSGC10) == true ? "" : models.GYSGC10),
                            new SqlParameter("@GYSGC11", string.IsNullOrWhiteSpace(models.GYSGC11) == true ? "" : models.GYSGC11),
                            new SqlParameter("@GYSGC12", string.IsNullOrWhiteSpace(models.GYSGC12) == true ? "" : models.GYSGC12),
                            new SqlParameter("@GYSGC20", string.IsNullOrWhiteSpace(models.GYSGC20) == true ? "" : models.GYSGC20),
                            new SqlParameter("@GYSGC21", string.IsNullOrWhiteSpace(models.GYSGC21) == true ? "" : models.GYSGC21),
                            new SqlParameter("@GYSGC22", string.IsNullOrWhiteSpace(models.GYSGC22) == true ? "" : models.GYSGC22),
                            new SqlParameter("@GYSGC23", string.IsNullOrWhiteSpace(models.GYSGC23) == true ? "" : models.GYSGC23),
                            new SqlParameter("@GYSGC24", string.IsNullOrWhiteSpace(models.GYSGC24) == true ? "" : models.GYSGC24),
                            new SqlParameter("@GYSGC25", string.IsNullOrWhiteSpace(models.GYSGC25) == true ? "" : models.GYSGC25),
                            new SqlParameter("@GYSGC26", string.IsNullOrWhiteSpace(models.GYSGC26) == true ? "" : models.GYSGC26),
                            new SqlParameter("@GYSGC27", string.IsNullOrWhiteSpace(models.GYSGC27) == true ? "" : models.GYSGC27),
                            new SqlParameter("@GYSGC28", string.IsNullOrWhiteSpace(models.GYSGC28) == true ? "" : models.GYSGC28),
                            new SqlParameter("@GYSGC29", string.IsNullOrWhiteSpace(models.GYSGC29) == true ? "" : models.GYSGC29),
                            new SqlParameter("@GYSGC30", string.IsNullOrWhiteSpace(models.GYSGC30) == true ? "" : models.GYSGC30),
                            new SqlParameter("@GYSGC31", string.IsNullOrWhiteSpace(models.GYSGC31) == true ? "" : models.GYSGC31),
                            new SqlParameter("@GYSGC32", string.IsNullOrWhiteSpace(models.GYSGC32) == true ? "" : models.GYSGC32),
                            new SqlParameter("@GYSGC33", string.IsNullOrWhiteSpace(models.GYSGC33) == true ? "" : models.GYSGC33),
                            new SqlParameter("@GYSGC34", string.IsNullOrWhiteSpace(models.GYSGC34) == true ? "" : models.GYSGC34),
                            new SqlParameter("@GYSGC35", string.IsNullOrWhiteSpace(models.GYSGC35) == true ? "" : models.GYSGC35),
                            new SqlParameter("@GYSGC36", string.IsNullOrWhiteSpace(models.GYSGC36) == true ? "" : models.GYSGC36),
                            new SqlParameter("@GYSGC37", string.IsNullOrWhiteSpace(models.GYSGC37) == true ? "" : models.GYSGC37),
                            new SqlParameter("@GYSGC38", string.IsNullOrWhiteSpace(models.GYSGC38) == true ? "" : models.GYSGC38),
                            new SqlParameter("@GYSGC39", string.IsNullOrWhiteSpace(models.GYSGC39) == true ? "" : models.GYSGC39),
                            new SqlParameter("@GYSGC40", string.IsNullOrWhiteSpace(models.GYSGC40) == true ? "" : models.GYSGC40),
                            new SqlParameter("@GYSGC41", string.IsNullOrWhiteSpace(models.GYSGC41) == true ? "" : models.GYSGC41),
                            new SqlParameter("@GYSGC42", string.IsNullOrWhiteSpace(models.GYSGC42) == true ? "" : models.GYSGC42),
                            new SqlParameter("@GYSGC43", string.IsNullOrWhiteSpace(models.GYSGC43) == true ? "" : models.GYSGC43),
                            new SqlParameter("@GYSGC44", string.IsNullOrWhiteSpace(models.GYSGC44) == true ? "" : models.GYSGC44),
                            new SqlParameter("@GYSGC45", string.IsNullOrWhiteSpace(models.GYSGC45) == true ? "" : models.GYSGC45),
                            //new SqlParameter("@GYSGC46", string.IsNullOrWhiteSpace(models.GYSGC46) == true ? "" : models.GYSGC46),
                            new SqlParameter("@GYSGC47", string.IsNullOrWhiteSpace(models.GYSGC47) == true ? "" : models.GYSGC47),
                            new SqlParameter("@GYSGC48", string.IsNullOrWhiteSpace(models.GYSGC48) == true ? "" : models.GYSGC48),
                            new SqlParameter("@GYSGC50", string.IsNullOrWhiteSpace(models.GYSGC50) == true ? "" : models.GYSGC50),
                            new SqlParameter("@id", models.id),
                                #endregion
                            };
                            count = SqlHelper.InsertDelUpdate(sql, param);//供应商编辑
                            if (count > 0)//当面辅料供应商编辑成功时
                            {
                                if (mxFK == 1)//执行面辅料供应商产品明细编辑
                                {
                                    for (int i = 0; i < MXnum; i++)
                                    {
                                        string[] mxInfo = MXArr[i].Split(',');
                                        string sqlMX = "";
                                        if (MXid[i] != "0")//进行编辑操作
                                        {
                                            #region 修改工厂明细  SQL语句
                                            sqlMX = "update BPM_GYSGCGLMX set GYSGCMX01 =@GYSGCMX01,GYSGCMX02 = @GYSGCMX02,GYSGCMX03 = @GYSGCMX03,GYSGCMX04 = @GYSGCMX04,GYSGCMX05 = @GYSGCMX05,GYSGCMX06 = @GYSGCMX06 where id =@MXid";
                                            #endregion

                                        }
                                        else//当MXid[i]==0时说明用户想进行添加该编辑供应商的产品明细
                                        {
                                            #region 添加工厂明细  SQL语句
                                            sqlMX = "insert into BPM_GYSGCGLMX(GYSGCID,GYSGCMX01,GYSGCMX02,GYSGCMX03,GYSGCMX04,GYSGCMX05,GYSGCMX06) values(@GYSGCID,@GYSGCMX01, @GYSGCMX02,@GYSGCMX03,@GYSGCMX04,@GYSGCMX05,@GYSGCMX06)";
                                            #endregion
                                        }
                                        SqlParameter[] paramMx = new SqlParameter[]
                                            {
                                        #region 工厂明细参数赋值
                                            new SqlParameter("@GYSGCMX01", string.IsNullOrWhiteSpace(mxInfo[0]) == true ? "" : mxInfo[0]),
                                            new SqlParameter("@GYSGCMX02", string.IsNullOrWhiteSpace(mxInfo[1]) == true ? "" : mxInfo[1]),
                                            new SqlParameter("@GYSGCMX03", string.IsNullOrWhiteSpace(mxInfo[2]) == true ? "" : mxInfo[2]),
                                            new SqlParameter("@GYSGCMX04", string.IsNullOrWhiteSpace(mxInfo[3]) == true ? "" : mxInfo[3]),
                                            new SqlParameter("@GYSGCMX05", string.IsNullOrWhiteSpace(mxInfo[4]) == true ? "" : mxInfo[4]),
                                            new SqlParameter("@GYSGCMX06", string.IsNullOrWhiteSpace(mxInfo[5]) == true ? "" : mxInfo[5]),
                                            new SqlParameter("@MXid", MXid[i]),//工厂详细ID
                                            new SqlParameter("@GYSGCID", models.id),//详细ID
                                                #endregion
                                            };
                                        count = SqlHelper.InsertDelUpdate(sqlMX, paramMx);//供应商产品明细编辑

                                    }
                                }
                            }
                        }
                        else
                        {
                            return Content("Repeat");//输出重复
                        }
                    }
                }
                else//进行添加操作
                {
                    if (mxFK != 0)//执行面辅料供应商添加
                    {
                        if (dt50.Rows.Count == 0)
                        {
                            #region 添加工厂  SQL语句
                            string sql = "insert into BPM_GYSGCGL(GYSGC02,GYSGC03,GYSGC04,GYSGC05,GYSGC06,GYSGC07,GYSGC08,GYSGC09,GYSGC10,GYSGC11,GYSGC12,GYSGC13,GYSGC14,GYSGC15,GYSGC16,GYSGC17,GYSGC18,GYSGC19,GYSGC20,GYSGC21,GYSGC22,GYSGC23,GYSGC24,GYSGC25,GYSGC26,GYSGC27,GYSGC28,GYSGC29,GYSGC30,GYSGC31,GYSGC32,GYSGC33,GYSGC34,GYSGC35,GYSGC36,GYSGC37,GYSGC38,GYSGC39,GYSGC40,GYSGC41,GYSGC42,GYSGC43,GYSGC44,GYSGC45,GYSGC47,GYSGC48,GYSGC49,GYSGC50) values(@GYSGC02,@GYSGC03,@GYSGC04,@GYSGC05,@GYSGC06,@GYSGC07,@GYSGC08,@GYSGC09,@GYSGC10,@GYSGC11,@GYSGC12,@GYSGC13,@GYSGC14,@GYSGC15,@GYSGC16,@GYSGC17,@GYSGC18,@GYSGC19,@GYSGC20,@GYSGC21,@GYSGC22,@GYSGC23,@GYSGC24,@GYSGC25,@GYSGC26,@GYSGC27,@GYSGC28,@GYSGC29,@GYSGC30,@GYSGC31,@GYSGC32,@GYSGC33,@GYSGC34,@GYSGC35,@GYSGC36,@GYSGC37,@GYSGC38,@GYSGC39,@GYSGC40,@GYSGC41,@GYSGC42,@GYSGC43,@GYSGC44,@GYSGC45,@GYSGC47,@GYSGC48,@GYSGC49,@GYSGC50);select @topid=@@identity";//select @topid=@@identity找出新插入一行的ID
                            #endregion
                            SqlParameter[] param = new SqlParameter[]
                            {
                         #region 参数赋值
                        new SqlParameter("@topid", SqlDbType.Int),//最新添加的ID
                        //new SqlParameter("@GYSGC01", string.IsNullOrWhiteSpace(models.GYSGC01) == true ? "" : models.GYSGC01),
                        new SqlParameter("@GYSGC02", string.IsNullOrWhiteSpace(models.GYSGC02) == true ? "0" : models.GYSGC02),
                        new SqlParameter("@GYSGC03", string.IsNullOrWhiteSpace(models.GYSGC03) == true ? "" : models.GYSGC03),
                        new SqlParameter("@GYSGC04", string.IsNullOrWhiteSpace(models.GYSGC04) == true ? "" : models.GYSGC04),
                        new SqlParameter("@GYSGC05", string.IsNullOrWhiteSpace(models.GYSGC05) == true ? "" : models.GYSGC05),
                        new SqlParameter("@GYSGC06", string.IsNullOrWhiteSpace(models.GYSGC06) == true ? "" : models.GYSGC06),
                        new SqlParameter("@GYSGC07", string.IsNullOrWhiteSpace(models.GYSGC07) == true ? "" : models.GYSGC07),
                        new SqlParameter("@GYSGC08", string.IsNullOrWhiteSpace(models.GYSGC08) == true ? "" : models.GYSGC08),
                        new SqlParameter("@GYSGC09", string.IsNullOrWhiteSpace(models.GYSGC09) == true ? "" : models.GYSGC09),
                        new SqlParameter("@GYSGC10", string.IsNullOrWhiteSpace(models.GYSGC10) == true ? "" : models.GYSGC10),
                        new SqlParameter("@GYSGC11", string.IsNullOrWhiteSpace(models.GYSGC11) == true ? "" : models.GYSGC11),
                        new SqlParameter("@GYSGC12", string.IsNullOrWhiteSpace(models.GYSGC12) == true ? "" : models.GYSGC12),
                        new SqlParameter("@GYSGC13", ""),
                        new SqlParameter("@GYSGC14", ""),
                        new SqlParameter("@GYSGC15", ""),
                        new SqlParameter("@GYSGC16", ""),
                        new SqlParameter("@GYSGC17", ""),
                        new SqlParameter("@GYSGC18", ""),
                        new SqlParameter("@GYSGC19", ""),
                        new SqlParameter("@GYSGC20", string.IsNullOrWhiteSpace(models.GYSGC20) == true ? "" : models.GYSGC20),
                        new SqlParameter("@GYSGC21", string.IsNullOrWhiteSpace(models.GYSGC21) == true ? "" : models.GYSGC21),
                        new SqlParameter("@GYSGC22", string.IsNullOrWhiteSpace(models.GYSGC22) == true ? "" : models.GYSGC22),
                        new SqlParameter("@GYSGC23", string.IsNullOrWhiteSpace(models.GYSGC23) == true ? "" : models.GYSGC23),
                        new SqlParameter("@GYSGC24", string.IsNullOrWhiteSpace(models.GYSGC24) == true ? "" : models.GYSGC24),
                        new SqlParameter("@GYSGC25", string.IsNullOrWhiteSpace(models.GYSGC25) == true ? "" : models.GYSGC25),
                        new SqlParameter("@GYSGC26", string.IsNullOrWhiteSpace(models.GYSGC26) == true ? "" : models.GYSGC26),
                        new SqlParameter("@GYSGC27", string.IsNullOrWhiteSpace(models.GYSGC27) == true ? "" : models.GYSGC27),
                        new SqlParameter("@GYSGC28", string.IsNullOrWhiteSpace(models.GYSGC28) == true ? "" : models.GYSGC28),
                        new SqlParameter("@GYSGC29", string.IsNullOrWhiteSpace(models.GYSGC29) == true ? "" : models.GYSGC29),
                        new SqlParameter("@GYSGC30", string.IsNullOrWhiteSpace(models.GYSGC30) == true ? "" : models.GYSGC30),
                        new SqlParameter("@GYSGC31", string.IsNullOrWhiteSpace(models.GYSGC31) == true ? "" : models.GYSGC31),
                        new SqlParameter("@GYSGC32", string.IsNullOrWhiteSpace(models.GYSGC32) == true ? "" : models.GYSGC32),
                        new SqlParameter("@GYSGC33", string.IsNullOrWhiteSpace(models.GYSGC33) == true ? "" : models.GYSGC33),
                        new SqlParameter("@GYSGC34", string.IsNullOrWhiteSpace(models.GYSGC34) == true ? "" : models.GYSGC34),
                        new SqlParameter("@GYSGC35", string.IsNullOrWhiteSpace(models.GYSGC35) == true ? "" : models.GYSGC35),
                        new SqlParameter("@GYSGC36", string.IsNullOrWhiteSpace(models.GYSGC36) == true ? "" : models.GYSGC36),
                        new SqlParameter("@GYSGC37", string.IsNullOrWhiteSpace(models.GYSGC37) == true ? "" : models.GYSGC37),
                        new SqlParameter("@GYSGC38", string.IsNullOrWhiteSpace(models.GYSGC38) == true ? "" : models.GYSGC38),
                        new SqlParameter("@GYSGC39", string.IsNullOrWhiteSpace(models.GYSGC39) == true ? "" : models.GYSGC39),
                        new SqlParameter("@GYSGC40", string.IsNullOrWhiteSpace(models.GYSGC40) == true ? "" : models.GYSGC40),
                        new SqlParameter("@GYSGC41", string.IsNullOrWhiteSpace(models.GYSGC41) == true ? "" : models.GYSGC41),
                        new SqlParameter("@GYSGC42", string.IsNullOrWhiteSpace(models.GYSGC42) == true ? "" : models.GYSGC42),
                        new SqlParameter("@GYSGC43", string.IsNullOrWhiteSpace(models.GYSGC43) == true ? "" : models.GYSGC43),
                        new SqlParameter("@GYSGC44", string.IsNullOrWhiteSpace(models.GYSGC44) == true ? "" : models.GYSGC44),
                        new SqlParameter("@GYSGC45", string.IsNullOrWhiteSpace(models.GYSGC45) == true ? "" : models.GYSGC45),
                       // new SqlParameter("@GYSGC46", string.IsNullOrWhiteSpace(models.GYSGC46) == true ? "" : models.GYSGC46),
                        new SqlParameter("@GYSGC47", string.IsNullOrWhiteSpace(models.GYSGC47) == true ? "" : models.GYSGC47),
                        new SqlParameter("@GYSGC48", string.IsNullOrWhiteSpace(models.GYSGC48) == true ? "" : models.GYSGC48),
                        new SqlParameter("@GYSGC49", "2"),
                        new SqlParameter("@GYSGC50", string.IsNullOrWhiteSpace(models.GYSGC50) == true ? "" : models.GYSGC50),
                                #endregion
                            };
                            param[0].Direction = ParameterDirection.Output;
                            count = SqlHelper.InsertDelUpdate(sql, param);
                            int rId = Convert.ToInt32(param[0].Value == null ? 0 : Convert.ToInt32(param[0].Value));
                            if (count > 0)//当面辅料供应商添加成功时
                            {
                                if (mxFK == 1)//执行面辅料供应商产品明细添加
                                {
                                    #region 添加工厂明细  SQL语句
                                    string sqlMX = "insert into BPM_GYSGCGLMX(GYSGCID,GYSGCMX01,GYSGCMX02,GYSGCMX03,GYSGCMX04,GYSGCMX05,GYSGCMX06) values(@GYSGCID,@GYSGCMX01, @GYSGCMX02,@GYSGCMX03,@GYSGCMX04,@GYSGCMX05,@GYSGCMX06)";
                                    #endregion
                                    for (int i = 0; i < MXnum; i++)
                                    {
                                        string[] mxInfo = MXArr[i].Split(',');
                                        SqlParameter[] paramMx = new SqlParameter[]
                                        {
                                    #region 工厂明细参数赋值
                                    new SqlParameter("@GYSGCID", rId),
                                    new SqlParameter("@GYSGCMX01", string.IsNullOrWhiteSpace(mxInfo[0]) == true ? "" : mxInfo[0]),
                                    new SqlParameter("@GYSGCMX02", string.IsNullOrWhiteSpace(mxInfo[1]) == true ? "" : mxInfo[1]),
                                    new SqlParameter("@GYSGCMX03", string.IsNullOrWhiteSpace(mxInfo[2]) == true ? "" : mxInfo[2]),
                                    new SqlParameter("@GYSGCMX04", string.IsNullOrWhiteSpace(mxInfo[3]) == true ? "" : mxInfo[3]),
                                    new SqlParameter("@GYSGCMX05", string.IsNullOrWhiteSpace(mxInfo[4]) == true ? "" : mxInfo[4]),
                                    new SqlParameter("@GYSGCMX06", string.IsNullOrWhiteSpace(mxInfo[5]) == true ? "" : mxInfo[5]),
                                            #endregion
                                        };
                                        count = SqlHelper.InsertDelUpdate(sqlMX, paramMx);//供应商产品明细添加
                                    }
                                }
                            }
                        }
                        else
                        {
                            return Content("Repeat");//输出重复
                        }
                    }
                }
                if (count > 0)
                {
                    return Content("success");
                }
                else
                {
                    return Content("null");
                }
            }

            catch (Exception ex)
            {
                return Content("error");
            }
        }

        /// <summary>
        /// 工厂修改意见的方法
        /// </summary>
        /// <returns></returns>
        [Property(MenuCode = "BPM_GYSGCGL", MenuOperation = "工厂修改意见")]
        public ActionResult UpFactoryOpinion()
        {
            var id = Request["id"];
            var str = Request["str"];
            //int index = Request["index"];
            string[] Arr = str.Split(new char[] { ',' });
            string sql = "update BPM_GYSGCGL set GYSGC13=@GYSGC13,GYSGC14=@GYSGC14,GYSGC15=@GYSGC15,GYSGC16=@GYSGC16,GYSGC17=@GYSGC17,GYSGC18=@GYSGC18,GYSGC19=@GYSGC19 where id=@id";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@GYSGC13",Arr[0]),
                new SqlParameter("@GYSGC14",Arr[1]),
                new SqlParameter("@GYSGC15",Arr[2]),
                new SqlParameter("@GYSGC16",Arr[3]),
                new SqlParameter("@GYSGC17",Arr[4]),
                new SqlParameter("@GYSGC18",Arr[5]),
                new SqlParameter("@GYSGC19",Arr[6]),
                new SqlParameter("@id",id)
            };
            int count = SqlHelper.InsertDelUpdate(sql, param);
            if (count > 0)
            {
                return Content("success");
            }
            else
            {
                return Content("error");
            }
        }

        /// <summary>
        /// 工厂删除
        /// </summary>
        /// <returns></returns>
        [Property(MenuCode = "BPM_GYSGCGL", MenuOperation = "工厂删除")]
        public ActionResult DelFactory()
        {
            var id = Request["id"];
            id = id.Substring(0, id.Length - 1);
            string sql = "update BPM_GYSGCGL set status=0 where id in (" + id + ")";
            int count = SqlHelper.InsertDelUpdate(sql);
            if (count > 0)
            {
                return Content("success");
            }
            else
            {
                return Content("error");
            }
        }

        /// <summary>
        /// 工厂明细删除
        /// </summary>
        /// <returns></returns>
        [Property(MenuCode = "BPM_GYSGCGLMX", MenuOperation = "工厂明细删除")]
        public ActionResult DelFactoryMX()
        {
            var id = Request["id"];
            string sql = "update BPM_GYSGCGLMX set status=0 where id=@id";
            int count = SqlHelper.InsertDelUpdate(sql, new SqlParameter("@id", id));
            if (count > 0)
            {
                return Content("success");
            }
            else
            {
                return Content("error");
            }
        }

        /// <summary>
        /// Excel导出
        /// </summary>
        /// <returns></returns>
        //[Property(MenuCode = "BPM_GYSGCGL", MenuOperation = "工厂Excel导出")]
        public ActionResult ExportFactoryExcel()
        {
            string sql = "select (select GYSGCTypeName from dbo.BPM_GYSGCGLLB s2 where BPM_GYSGCGL.GYSGC02=s2.id ) as 类别,GYSGC03 as 加工厂名称,GYSGC50 as 供应商代码,GYSGC04 as 经营性质,GYSGC05 as 开发能力,GYSGC06 as 生产能力,GYSGC07 as 质量管控能力,GYSGC08 as 配合度,GYSGC09 as 价位,GYSGC10 as 贷款结算方式,GYSGC11 as '是否与品牌目标合作（是/否）',GYSGC12 as 采购部初步合作定位,GYSGC13 as '项目配合度（强/中/弱）',GYSGC14 as '项目开发能力（强/中/弱）',GYSGC15 as '项目是否可配合（是/否）',GYSGC16 as '生管配合度（强/中/弱）',GYSGC17 as '生管货期（强/中/弱）',GYSGC18 as '生管是否可配合（是/否）',GYSGC19 as '综合评定是否合作（是/否）',GYSGC20 as 评定等级,GYSGC21 as 地址,GYSGC22 as 法人代表,GYSGC23 as 法人代表职务,GYSGC24 as 法人代表电话,GYSGC25 as 业务代表,GYSGC26 as 业务代表职务,GYSGC27 as 业务代表电话,GYSGC28 as '企业形态（国有/民营/外资/合资/其它）',GYSGC29 as 注册资本,GYSGC30 as '业务种类(生产、贸易、批发、其它)',GYSGC31 as 厂房面积,GYSGC32 as 人员规模,GYSGC33 as 加工所占比例,GYSGC34 as 自营经销所占比例,GYSGC35 as 通过何种认证,GYSGC36 as 年总产量,GYSGC37 as 检验标准,GYSGC38 as 所用染化料,GYSGC39 as 国内主要合作品牌,GYSGC40 as 国际主要合作品牌,GYSGC41 as 打色周期,GYSGC42 as 放样周期FOB,GYSGC43 as 放样周期CMT,GYSGC44 as 大货生产周期FOB,GYSGC45 as 大货生产周期CMT,GYSGC47 as 生产设备品种及数量,GYSGC48 as 综合评估 from BPM_GYSGCGL where status<>0 and GYSGC49=2";
            HSSFWorkbook book = GetExcel(sql);

            // 写入到客户端    
            return WriteInClient(book);
        }

        /// <summary>
        /// Excel模板导出
        /// </summary>
        /// <returns></returns>
        public ActionResult ExportFactoryTExcel()
        {
            string sql = "select GYSGC02 as 类别,GYSGC03 as 加工厂名称,GYSGC50 as 供应商代码,GYSGC04 as 经营性质,GYSGC05 as 开发能力,GYSGC06 as 生产能力,GYSGC07 as 质量管控能力,GYSGC08 as 配合度,GYSGC09 as 价位,GYSGC10 as 贷款结算方式,GYSGC11 as '是否与品牌目标合作（是/否）',GYSGC12 as 采购部初步合作定位,GYSGC13 as '项目配合度（强/中/弱）',GYSGC14 as '项目开发能力（强/中/弱）',GYSGC15 as '项目是否可配合（是/否）',GYSGC16 as '生管配合度（强/中/弱）',GYSGC17 as '生管货期（强/中/弱）',GYSGC18 as '生管是否可配合（是/否）',GYSGC19 as '综合评定是否合作（是/否）',GYSGC20 as 评定等级,GYSGC21 as 地址,GYSGC22 as 法人代表,GYSGC23 as 法人代表职务,GYSGC24 as 法人代表电话,GYSGC25 as 业务代表,GYSGC26 as 业务代表职务,GYSGC27 as 业务代表电话,GYSGC28 as '企业形态（国有/民营/外资/合资/其它）',GYSGC29 as 注册资本,GYSGC30 as '业务种类(生产、贸易、批发、其它)',GYSGC31 as 厂房面积,GYSGC32 as 人员规模,GYSGC33 as 加工所占比例,GYSGC34 as 自营经销所占比例,GYSGC35 as 通过何种认证,GYSGC36 as 年总产量,GYSGC37 as 检验标准,GYSGC38 as 所用染化料,GYSGC39 as 国内主要合作品牌,GYSGC40 as 国际主要合作品牌,GYSGC41 as 打色周期,GYSGC42 as 放样周期FOB,GYSGC43 as 放样周期CMT,GYSGC44 as 大货生产周期FOB,GYSGC45 as 大货生产周期CMT,GYSGC47 as 生产设备品种及数量,GYSGC48 as 综合评估 from BPM_GYSGCGL where 1!=1";
            HSSFWorkbook book = GetExcel(sql);

            // 写入到客户端    
            return WriteInClient(book);
        }

        /// <summary>
        /// Excel数据导入
        /// </summary>
        /// <returns></returns>
        [Property(MenuCode = "BPM_GYSGCGL", MenuOperation = "工厂Excel导入")]
        public ActionResult ImportFactoryExcel(HttpPostedFileBase filed)
        {
            //Common.SaveExcelFile(filed);
            string filePath = SaveAsLoed(filed);
            string sql = "INSERT INTO BPM_GYSGCGL(GYSGC02,GYSGC03,GYSGC04,GYSGC05,GYSGC06,GYSGC07,GYSGC08,GYSGC09,GYSGC10,GYSGC11,GYSGC12,GYSGC13,GYSGC14,GYSGC15,GYSGC16,GYSGC17,GYSGC18,GYSGC19,GYSGC20,GYSGC21,GYSGC22,GYSGC23,GYSGC24,GYSGC25,GYSGC26,GYSGC27,GYSGC28,GYSGC29,GYSGC30,GYSGC31,GYSGC32,GYSGC33,GYSGC34,GYSGC35,GYSGC36,GYSGC37,GYSGC38,GYSGC39,GYSGC40,GYSGC41,GYSGC42,GYSGC43,GYSGC44,GYSGC45,GYSGC47,GYSGC48,GYSGC49,GYSGC50) VALUES(@GYSGC02,@GYSGC03,@GYSGC04,@GYSGC05,@GYSGC06,@GYSGC07,@GYSGC08,@GYSGC09,@GYSGC10,@GYSGC11,@GYSGC12,@GYSGC13,@GYSGC14,@GYSGC15,@GYSGC16,@GYSGC17,@GYSGC18,@GYSGC19,@GYSGC20,@GYSGC21,@GYSGC22,@GYSGC23,@GYSGC24,@GYSGC25,@GYSGC26,@GYSGC27,@GYSGC28,@GYSGC29,@GYSGC30,@GYSGC31,@GYSGC32,@GYSGC33,@GYSGC34,@GYSGC35,@GYSGC36,@GYSGC37,@GYSGC38,@GYSGC39,@GYSGC40,@GYSGC41,@GYSGC42,@GYSGC43,@GYSGC44,@GYSGC45,@GYSGC47,@GYSGC48,@GYSGC49,@GYSGC50)";
            try
            {
                DataTable dt = Common.ExcelToDataTable(filePath, true, 0);
                foreach (DataRow dr in dt.Rows)
                {
                    string sqlLB = "select id from dbo.BPM_GYSGCGLLB where GYSGCTypeName=@GYSGCTypeName";
                    var LBid = SqlHelper.SelectSinger(sqlLB, new SqlParameter("@GYSGCTypeName", dr["类别"]));
                    SqlParameter[] param = new SqlParameter[]
                    {
                        //循环参数赋值
                        #region 循环参数赋值
                        new SqlParameter("@GYSGC02",LBid),
                        new SqlParameter("@GYSGC03",dr["加工厂名称"]),
                        new SqlParameter("@GYSGC04",dr["经营性质"]),
                        new SqlParameter("@GYSGC05",dr["开发能力"]),
                        new SqlParameter("@GYSGC06",dr["生产能力"]),
                        new SqlParameter("@GYSGC07",dr["质量管控能力"]),
                        new SqlParameter("@GYSGC08",dr["配合度"]),
                        new SqlParameter("@GYSGC09",dr["价位"]),
                        new SqlParameter("@GYSGC10",dr["贷款结算方式"]),
                        new SqlParameter("@GYSGC11",dr["是否与品牌目标合作（是/否）"]),
                        new SqlParameter("@GYSGC12",dr["采购部初步合作定位"]),
                        new SqlParameter("@GYSGC13",dr["项目配合度（强/中/弱）"]),
                        new SqlParameter("@GYSGC14",dr["项目开发能力（强/中/弱）"]),
                        new SqlParameter("@GYSGC15",dr["项目是否可配合（是/否）"]),
                        new SqlParameter("@GYSGC16",dr["生管配合度（强/中/弱）"]),
                        new SqlParameter("@GYSGC17",dr["生管货期（强/中/弱）"]),
                        new SqlParameter("@GYSGC18",dr["生管是否可配合（是/否）"]),
                        new SqlParameter("@GYSGC19",dr["综合评定是否合作（是/否）"]),
                        new SqlParameter("@GYSGC20",dr["评定等级"]),
                        new SqlParameter("@GYSGC21",dr["地址"]),
                        new SqlParameter("@GYSGC22",dr["法人代表"]),
                        new SqlParameter("@GYSGC23",dr["法人代表职务"]),
                        new SqlParameter("@GYSGC24",dr["法人代表电话"]),
                        new SqlParameter("@GYSGC25",dr["业务代表"]),
                        new SqlParameter("@GYSGC26",dr["业务代表职务"]),
                        new SqlParameter("@GYSGC27",dr["业务代表电话"]),
                        new SqlParameter("@GYSGC28",dr["企业形态（国有/民营/外资/合资/其它）"]),
                        new SqlParameter("@GYSGC29",dr["注册资本"]),
                        new SqlParameter("@GYSGC30",dr["业务种类(生产、贸易、批发、其它)"]),
                        new SqlParameter("@GYSGC31",dr["厂房面积"]),
                        new SqlParameter("@GYSGC32",dr["人员规模"]),
                        new SqlParameter("@GYSGC33",dr["加工所占比例"]),
                        new SqlParameter("@GYSGC34",dr["自营经销所占比例"]),
                        new SqlParameter("@GYSGC35",dr["通过何种认证"]),
                        new SqlParameter("@GYSGC36",dr["年总产量"]),
                        new SqlParameter("@GYSGC37",dr["检验标准"]),
                        new SqlParameter("@GYSGC38",dr["所用染化料"]),
                        new SqlParameter("@GYSGC39",dr["国内主要合作品牌"]),
                        new SqlParameter("@GYSGC40",dr["国际主要合作品牌"]),
                        new SqlParameter("@GYSGC41",string.IsNullOrWhiteSpace(dr["打色周期"].ToString()) == true ? "" : dr["打色周期"]),
                        new SqlParameter("@GYSGC42",string.IsNullOrWhiteSpace(dr["放样周期FOB"].ToString()) == true ? "" : dr["放样周期FOB"]),
                        new SqlParameter("@GYSGC43",string.IsNullOrWhiteSpace(dr["放样周期CMT"].ToString()) == true ? "" : dr["放样周期CMT"]),
                        new SqlParameter("@GYSGC44",string.IsNullOrWhiteSpace(dr["大货生产周期FOB"].ToString()) == true ? "" : dr["大货生产周期FOB"]),
                        new SqlParameter("@GYSGC45",string.IsNullOrWhiteSpace(dr["大货生产周期CMT"].ToString()) == true ? "" : dr["大货生产周期CMT"]),
                        new SqlParameter("@GYSGC47",dr["生产设备品种及数量"]),
                        new SqlParameter("@GYSGC48",dr["综合评估"]),
                        new SqlParameter("@GYSGC49","2"),
                        new SqlParameter("@GYSGC50",dr["供应商代码"]),
	                    #endregion
                    };
                    SqlHelper.InsertDelUpdate(sql, param);
                }

            }
            catch (Exception ex)
            {
                return Content("上传失败请检查！");
            }
            return Content("上传成功");
        }

        /// <summary>
        /// Excel导出方法
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private static HSSFWorkbook GetExcel(string sql)
        {
            HSSFWorkbook book = new HSSFWorkbook();
            HSSFSheet sheet = book.CreateSheet("Sheet1") as HSSFSheet;

            #region sqltoExcel  

            DataTable dt = SqlHelper.SelectTable(sql);
            NPOI.SS.UserModel.IRow row1 = sheet.CreateRow(0);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                row1.CreateCell(i).SetCellValue(dt.Columns[i].ColumnName);
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                NPOI.SS.UserModel.IRow row = sheet.CreateRow(i + 1);

                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    Type temp_type = dt.Columns[j].DataType;
                    if (temp_type == typeof(DateTime))
                    {
                        if (string.IsNullOrWhiteSpace(dt.Rows[i][dt.Columns[j].ColumnName].ToString()))
                        {
                            row.CreateCell(j).SetCellValue("1900-01-01");
                        }
                        else
                        {
                            row.CreateCell(j).SetCellValue(Convert.ToDateTime(dt.Rows[i][dt.Columns[j].ColumnName]).ToString("yyyy-MM-dd"));
                        }
                    }
                    else
                    {
                        row.CreateCell(j).SetCellValue(dt.Rows[i][dt.Columns[j].ColumnName].ToString());
                    }
                }
            }
            #endregion
            return book;
        }

        /// <summary>
        /// 写入到客户端
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        private ActionResult WriteInClient(HSSFWorkbook book)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.xls", "Excle" + DateTime.Now.ToString("yyyyMMddHHmmssfff")));
            Response.BinaryWrite(ms.ToArray());
            book = null;
            ms.Close();
            ms.Dispose();
            return Content("OK");
        }

        /// <summary>
        /// 保存Excel文件
        /// <para>Excel的导入导出都会在服务器生成一个文件</para>
        /// <para>路径：UpFiles/ExcelFiles</para>
        /// </summary>
        /// <param name="file">传入的文件对象</param>
        /// <returns>如果保存成功则返回文件的位置;如果保存失败则返回空</returns>
        public string SaveAsLoed(HttpPostedFileBase file)
        {
            var fileName = file.FileName;
            var filePath = Server.MapPath(string.Format("~/{0}", "Upload"));
            file.SaveAs(Path.Combine(filePath, fileName));
            return filePath + "/" + fileName;
        }
    }
}