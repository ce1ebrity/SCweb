using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SCWeb.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SqlSugar;
using System.Configuration;
using SCWeb.Models;
using Senparc.NeuChar.App.Entities;
using Senparc.Weixin.MP;

namespace SCWeb.Controllers
{
    [Login(IsCheck = true)]
    /// <summary>
    /// 首页控制器
    /// </summary>
    public class HomePageController : Controller
    {
        // GET: HomePage
        public ActionResult Index()
        {
            string userId = Common.GetCookie("userLogin");
            string sql = "select trueName,(select roleName from dbo.BPM_RoleBase where id=roleId) from BPM_UserBase  where id=@id";
            DataTable dt = SqlHelper.SelectTable(sql, new SqlParameter("@id", userId));
            if (dt.Rows.Count > 0)
            {
                ViewBag.trueName = dt.Rows[0][0].ToString();
                ViewBag.roleName = dt.Rows[0][1].ToString();
            }
            return View();
        }

        // GET: HomePage
        public ActionResult HomeIndex()
        {
            return View();
        }

        // GET: HomeLogin
        [Login(IsCheck = false)]
        public ActionResult HomeLogin()
        {
            return View();
        }

        public ActionResult GoodsViewCount()
        {
            return View();
        }

        public ActionResult GoodsLists()
        {
            return View();
        }

        [HttpPost]
        [Login(IsCheck = false)]
        public ActionResult GetUserLogin()
        {
            ResultClass rc = new ResultClass();

            var loginName = Request["userName"];
            var pwd = Request["pwd"];
            string sql = "select * from BPM_UserBase where loginName=@loginName and passWord=@passWord";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@loginName",loginName),
                new SqlParameter("@passWord",DES.GetMD5(pwd))
            };
            string id = Convert.ToString(SqlHelper.SelectSinger(sql, param));
            if (!string.IsNullOrWhiteSpace(id))
            {
                Common.SetCookie("userLogin", id, 7);
                rc.Result = "success";
                rc.Message = "登录成功，正在跳转页面！";
            }
            else
            {
                rc.Result = "error";
                rc.Message = "登陆失败,请检查用户名和密码!";
            }
            return Content(JsonConvert.SerializeObject(rc));
        }


        /// <summary>
        /// 获取复选框值
        /// </summary>
        /// <returns>所有列</returns>
        [HttpPost]
        public ActionResult GetCboxUL()
        {
            //string sql = "select * from BPM_LieName where status=1 order by sumList ";
            string sql = " Select * From BPM_LieName where status=1 and sumList in(1,102,103,2,3,4,5,6,7,8,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,40,41,42,43,43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59,60,61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,81,82,83,84,85,86,87,88,89,90,91,92,93,94,95,104,105,99,100,101) Order By charindex(',' + CONVERT(varchar, sumList) + ',', ',1,102,103,2,3,4,5,6,7,8,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,40,41,42,43,43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59,60,61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,81,82,83,84,85,86,87,88,89,90,91,92,93,94,95,104,105,99,100,101,')";
            DataTable dt = SqlHelper.SelectTable(sql);
            return Content(JsonConvert.SerializeObject(dt));
        }

        /// <summary>
        /// 大页面查询
        /// </summary>
        /// <param name="page"></param>
        /// <param name="showpage"></param>
        /// <param name="selectWhere"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetTableLists(string page, string showpage, string selectWhere)
        {
            int RowsCount = 0;//总行数
            int pageCount = 0;//总页数
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy'-'MM'-'dd" };

            string stringWhere = " status='1' ";
            if (selectWhere != "")
            {
                string[] arrWhere = selectWhere.Split(',');
                for (int i = 0; i < arrWhere.Length; i++)
                {
                    string sqls = "select nameType from BPM_LieName where status=1 and lieName='" + arrWhere[i].Split('|')[0] + "'";
                    string nameType = (string)SqlHelper.SelectSinger(sqls);

                    if (nameType != "datetime")
                        stringWhere += " and " + arrWhere[i].Split('|')[0] + " like '%" + arrWhere[i].Split('|')[1] + "%' ";
                    else
                        stringWhere += " and " + arrWhere[i].Split('|')[0] + " = '" + arrWhere[i].Split('|')[1] + "' ";
                }
            }

            string SelectLie = @" *,(SCJD08+SCJD09+SCJD10+SCJD11+SCJD12) as 商品合计,(SCJD20-(SCJD08+SCJD09+SCJD10+SCJD11+SCJD12)) as 物控下单欠数,(SCJD42+SCJD43+SCJD44+SCJD45+SCJD46) as 生产下单合计,(SCJD50+SCJD51+SCJD52+SCJD53+SCJD54) as 实裁合计,((SCJD50+SCJD51+SCJD52+SCJD53+SCJD54) - (SCJD42+SCJD43+SCJD44+SCJD45+SCJD46)) as 裁剪差异数 ";
            DataTable dt = Common.GetSQLProcList(out RowsCount, out pageCount, "view_BPM_SCJD", "*", "id", showpage, page, " id desc ", stringWhere);

            foreach (DataRow dr in dt.Rows)
            {
                dr["SCJD69"] = Convert.ToDecimal(dr["入仓S"]) + Convert.ToDecimal(dr["入仓M"]) + Convert.ToDecimal(dr["入仓L"]) + Convert.ToDecimal(dr["入仓均码"]) + Convert.ToDecimal(dr["入仓F"]);
                dr["SCJD81"] = Convert.ToDecimal(dr["入仓S"]) - Convert.ToDecimal(dr["SCJD42"]);
                dr["SCJD82"] = Convert.ToDecimal(dr["入仓M"]) - Convert.ToDecimal(dr["SCJD43"]);
                dr["SCJD83"] = Convert.ToDecimal(dr["入仓L"]) - Convert.ToDecimal(dr["SCJD44"]);
                dr["SCJD84"] = Convert.ToDecimal(dr["入仓均码"]) - Convert.ToDecimal(dr["SCJD45"]);
                dr["SCJD85"] = Convert.ToDecimal(dr["入仓F"]) - Convert.ToDecimal(dr["SCJD46"]);
                dr["出货欠生产合计"] = Convert.ToDecimal(dr["SCJD69"]) - Convert.ToDecimal(dr["生产下单合计"]);
                dr["SCJD76"] = Convert.ToDecimal(dr["入仓S"]) - Convert.ToDecimal(dr["SCJD08"]);
                dr["SCJD77"] = Convert.ToDecimal(dr["入仓M"]) - Convert.ToDecimal(dr["SCJD09"]);
                dr["SCJD78"] = Convert.ToDecimal(dr["入仓L"]) - Convert.ToDecimal(dr["SCJD10"]);
                dr["SCJD79"] = Convert.ToDecimal(dr["入仓均码"]) - Convert.ToDecimal(dr["SCJD11"]);
                dr["SCJD80"] = Convert.ToDecimal(dr["入仓F"]) - Convert.ToDecimal(dr["SCJD12"]);
                dr["出货欠商品合计"] = Convert.ToDecimal(dr["SCJD69"]) - Convert.ToDecimal(dr["商品合计"]);
            }


            if (dt.Rows.Count > 0)
            {
                dt.Rows[0]["RowsCount"] = RowsCount;
                dt.Rows[0]["pageCount"] = pageCount;
                dt.Rows[0]["page"] = page;
            }
            else
            {
                if (Convert.ToInt32(page) != 1)
                {
                    dt = Common.GetSQLProcList(out RowsCount, out pageCount, "view_BPM_SCJD", "*", "id", showpage, "1", " id desc ", stringWhere);

                    foreach (DataRow dr in dt.Rows)
                    {
                        dr["SCJD69"] = Convert.ToDecimal(dr["入仓S"]) + Convert.ToDecimal(dr["入仓M"]) + Convert.ToDecimal(dr["入仓L"]) + Convert.ToDecimal(dr["入仓均码"]) + Convert.ToDecimal(dr["入仓F"]);
                        dr["SCJD81"] = Convert.ToDecimal(dr["入仓S"]) - Convert.ToDecimal(dr["SCJD42"]);
                        dr["SCJD82"] = Convert.ToDecimal(dr["入仓M"]) - Convert.ToDecimal(dr["SCJD43"]);
                        dr["SCJD83"] = Convert.ToDecimal(dr["入仓L"]) - Convert.ToDecimal(dr["SCJD44"]);
                        dr["SCJD84"] = Convert.ToDecimal(dr["入仓均码"]) - Convert.ToDecimal(dr["SCJD45"]);
                        dr["SCJD85"] = Convert.ToDecimal(dr["入仓F"]) - Convert.ToDecimal(dr["SCJD46"]);
                        dr["出货欠生产合计"] = Convert.ToDecimal(dr["SCJD69"]) - Convert.ToDecimal(dr["生产下单合计"]);
                        dr["SCJD76"] = Convert.ToDecimal(dr["入仓S"]) - Convert.ToDecimal(dr["SCJD08"]);
                        dr["SCJD77"] = Convert.ToDecimal(dr["入仓M"]) - Convert.ToDecimal(dr["SCJD09"]);
                        dr["SCJD78"] = Convert.ToDecimal(dr["入仓L"]) - Convert.ToDecimal(dr["SCJD10"]);
                        dr["SCJD79"] = Convert.ToDecimal(dr["入仓均码"]) - Convert.ToDecimal(dr["SCJD11"]);
                        dr["SCJD80"] = Convert.ToDecimal(dr["入仓F"]) - Convert.ToDecimal(dr["SCJD12"]);
                        dr["出货欠商品合计"] = Convert.ToDecimal(dr["SCJD69"]) - Convert.ToDecimal(dr["商品合计"]);
                    }

                    if (dt.Rows.Count > 0)
                    {
                        dt.Rows[0]["RowsCount"] = RowsCount;
                        dt.Rows[0]["pageCount"] = pageCount;
                        dt.Rows[0]["page"] = page;
                    }
                }
            }

            return Content(JsonConvert.SerializeObject(dt, Formatting.Indented, timeConverter));
        }

        /// <summary>
        /// 汇总页查询
        /// </summary>
        /// <param name="selectWhere"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetSCJDCount(string selectWhere)
        {

            decimal L1Count = 0.0M, L2Count = 0.0M, L3Count = 0.0M, L4Count = 0.0M, L5Count = 0.0M,
                L6Count = 0.0M, L7Count = 0.0M, L8Count = 0.0M, L10Count = 0.0M,
                L11Count = 0.0M, L12Count = 0.0M, L13Count = 0.0M, L14Count = 0.0M, L15Count = 0.0M, L16Count = 0.0M;

            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy" };//yyyy'-'MM'-'dd
            string sql = "select * from view_BPM_SCJDCount where 1=1 ";
            if (!string.IsNullOrWhiteSpace(selectWhere))
            {
                string[] arrWhere = selectWhere.Split(',');
                for (int i = 0; i < arrWhere.Length; i++)
                {
                    sql += " and " + arrWhere[i].Split('|')[0] + " like '%" + arrWhere[i].Split('|')[1] + "%'";
                }
            }
            DataTable dt = SqlHelper.SelectTable(sql);
            foreach (DataRow dr in dt.Rows)
            {
                L1Count += Convert.ToDecimal(dr["商品下单款数"]);
                L2Count += Convert.ToDecimal(dr["商品下单总数"]);
                L3Count += Convert.ToDecimal(dr["生产下单总数"]);
                L4Count += Convert.ToDecimal(dr["齐料款数"]);
                L5Count += Convert.ToDecimal(dr["采购完成比例"]);
                L6Count += Convert.ToDecimal(dr["采购欠料款数"]);
                L7Count += Convert.ToDecimal(dr["开裁款数"]);
                L8Count += Convert.ToDecimal(dr["车间上线款数"]);
                L10Count += Convert.ToDecimal(dr["车间下线款数"]);
                L11Count += Convert.ToDecimal(dr["出货款数"]);
                L12Count += Convert.ToDecimal(dr["入库数量"]);

                L14Count += Convert.ToDecimal(dr["未出款数"]);
                L15Count += Convert.ToDecimal(dr["未出总数"]);
                L16Count += Convert.ToDecimal(dr["送仓款数"]);
            }
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.NewRow();
                row["商品下单款数"] = L1Count;
                row["商品下单总数"] = L2Count;
                row["生产下单总数"] = L3Count;
                row["齐料款数"] = L4Count;
                row["采购完成比例"] = L5Count;
                row["采购欠料款数"] = L6Count;
                row["开裁款数"] = L7Count;
                row["车间上线款数"] = L8Count;
                row["车间下线款数"] = L10Count;
                row["出货款数"] = L11Count;
                row["入库数量"] = L12Count;
                row["入库比例"] = L13Count;
                row["未出款数"] = L14Count;
                row["未出总数"] = L15Count;
                row["送仓款数"] = L16Count;
                dt.Rows.Add(row);
            }
            return Content(JsonConvert.SerializeObject(dt, Formatting.Indented, timeConverter));
        }




    }
}