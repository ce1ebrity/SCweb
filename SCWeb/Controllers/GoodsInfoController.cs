using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using SCWeb.Helper;
using SCWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SCWeb.Controllers
{
    [Login(IsCheck = true)]
    /// <summary>
    /// 商品进度控制器
    /// </summary>
    public class GoodsInfoController : BaseController
    {
        #region 商品部方法
        // GET: GoodsInfo
        public ActionResult GoodsIndex()
        {
            return View();
        }

        public ActionResult GoodsIndex1111()
        {
            return View();
        }
        // GET: GoodsUpdate
        /// <summary>
        /// 传数据到前台，编辑前绑定数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GoodsUpdate(string id)
        {

            string sql = "select * from BPM_SCJDB where id=@id";
            DataTable dt = SqlHelper.SelectTable(sql, new SqlParameter("@id", id));
            if (dt.Rows.Count > 0)
            {
                ViewBag.SCJD01 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD01"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["SCJD01"]).ToString("yyyy-MM-dd");
                ViewBag.SCJD02 = dt.Rows[0]["SCJD02"].ToString();
                ViewBag.SCJD03 = dt.Rows[0]["SCJD03"].ToString();
                ViewBag.SCJD04 = dt.Rows[0]["SCJD04"].ToString();
                ViewBag.SCJD05 = dt.Rows[0]["SCJD05"].ToString();
                ViewBag.SCJD06 = dt.Rows[0]["SCJD06"].ToString();
                ViewBag.SCJD07 = dt.Rows[0]["SCJD07"].ToString();
                ViewBag.SCJD08 = dt.Rows[0]["SCJD08"].ToString();
                ViewBag.SCJD09 = dt.Rows[0]["SCJD09"].ToString();
                ViewBag.SCJD10 = dt.Rows[0]["SCJD10"].ToString();
                ViewBag.SCJD11 = dt.Rows[0]["SCJD11"].ToString();
                ViewBag.SCJD12 = dt.Rows[0]["SCJD12"].ToString();
                ViewBag.QXK = dt.Rows[0]["QXK"].ToString();
                ViewBag.huoqi70day = string.IsNullOrWhiteSpace(dt.Rows[0]["huoqi70day"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["huoqi70day"]).ToString("yyyy-MM-dd");
                ViewBag.SCJD92 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD92"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["SCJD92"]).ToString("yyyy-MM-dd");
                ViewBag.years = string.IsNullOrWhiteSpace(dt.Rows[0]["years"].ToString()) == true ? "" : dt.Rows[0]["years"].ToString();//("yyyy");
            }
            ViewBag.id = id;
            return View();
        }

        // GET: GoodsAdd
        public ActionResult GoodsAdd()
        {
            return View();
        }

        /// <summary>
        /// table绑定数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetIndexList(string page)
        {
            int RowsCount = 0;//总行数
            int pageCount = 0;//总页数
            DataTable dt = Common.GetSQLProcList(out RowsCount, out pageCount, "BPM_SCJDB", " * ", "id", "10", page, " id desc ", " status<>'0' ");
            List<BPMSCJDBModels> SCJlist = new List<BPMSCJDBModels>();
            foreach (DataRow item in dt.Rows)
            {
                BPMSCJDBModels u = new BPMSCJDBModels()
                {
                    id = item["id"].ToString(),
                    SCJD01 = Convert.ToDateTime(item["SCJD01"]).ToString("yyyy-MM-dd"),
                    SCJD02 = item["SCJD02"].ToString(),
                    SCJD03 = item["SCJD03"].ToString(),
                    SCJD04 = item["SCJD04"].ToString(),
                    SCJD05 = item["SCJD05"].ToString(),
                    SCJD06 = item["SCJD06"].ToString(),
                    SCJD07 = item["SCJD07"].ToString(),
                    SCJD13 = (Convert.ToDecimal(item["SCJD08"]) + Convert.ToDecimal(item["SCJD09"]) + Convert.ToDecimal(item["SCJD10"]) + Convert.ToDecimal(item["SCJD11"]) + Convert.ToDecimal(item["SCJD12"])).ToString(),
                    SCJD20 = item["SCJD20"].ToString(),
                    SCJD21 = item["SCJD21"].ToString(),
                    SCJD22 = item["SCJD22"].ToString(),
                    SCJD23 = Convert.ToDateTime(item["SCJD23"].ToString()).ToString("yyyy-MM-dd"),
                    SCJD24 = item["SCJD24"].ToString(),
                    SCJD34 = Convert.ToDateTime(item["SCJD34"].ToString()).ToString("yyyy-MM-dd"),
                    SCJD35 = item["SCJD35"].ToString(),
                    SCJD36 = item["SCJD36"].ToString(),
                    SCJD37 = Convert.ToDateTime(item["SCJD37"].ToString()).ToString("yyyy-MM-dd"),
                    SCJD38 = Convert.ToDateTime(item["SCJD38"].ToString()).ToString("yyyy-MM-dd"),
                    SCJD39 = Convert.ToDateTime(item["SCJD39"].ToString()).ToString("yyyy-MM-dd"),
                    SCJD40 = Convert.ToDateTime(item["SCJD40"].ToString()).ToString("yyyy-MM-dd"),

                };
                SCJlist.Add(u);
            }
            return Content(JsonConvert.SerializeObject(SCJlist));
        }

        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <returns></returns>
        public ActionResult SelSCJDBInfo()
        {
            
            int RowsCount = 0;//总行数
            int pageCount = 0;//总页数
            var page = Request["page"];
            var DDLbo = Request["DDLbo"];
            var DDLpp = Request["DDLpp"];
            var DDLmode = Request["DDLmode"];
            var QXK = Request["QXK"];
            var txtStyle = Request["txtStyle"];
            var txtGirard = Request["txtGirard"];
            var txtColor = Request["txtColor"];
            var txtTime = Request["txtTime"];
            var OnePageC = Request["OnePageC"];
            var year = Request["year"];

            var DDLjidu = Request["DDLjd"];//添加季度搜索 
            var txtgc = Request["txtFactory"];//添加工厂搜索
            var txtGD = Request["txtGD"];//添加跟单员搜索
            var txtMlcg = Request["txtMlcg"];//添加面料采购员搜索
            var txtFlcg = Request["txtFlcg"];//添加辅料采购员搜索
            var txtPG = Request["txtPG"];//添加品管人搜索
            var kh_id = Request["kh_id"];//款号id

            //string cachejijie = Server.UrlDecode(Request.Cookies["jijie"].Value);
            //string cacheboduan = Server.UrlDecode(Request.Cookies["boduan"].Value);
            //StringBuilder:可增长的字符串数组。
            StringBuilder sb = new StringBuilder(" 1=1 ");
            if (DDLbo != "0")
            {
                sb.Append(" and SCJD02 = '" + DDLbo + "'");
            }
            if (DDLpp != "0")
            {
                sb.Append(" and SCJD03='" + DDLpp + "'");
            }
            if (DDLmode != "0")
            {
                sb.Append(" and SCJD04='" + DDLmode + "'");
            }

            if (!string.IsNullOrEmpty(QXK))
            {
                sb.Append(" and QXK = '" + QXK + "'");
            }


            if (txtStyle != "")
            {
                sb.Append(" and SCJD06 like'%" + txtStyle + "%'");
            }
            if (txtGirard != "")
            {
                if (txtGirard.Split('\'').Length > 1)
                {
                    sb.Append(" and SCJD05 in (" + txtGirard + ")");
                }
                else
                {
                    sb.Append(" and SCJD05 like '%" + txtGirard + "%'");
                }
            }
            if (txtColor != "")
            {
                sb.Append(" and SCJD07 like'%" + txtColor + "%'");
            }
            if (!string.IsNullOrWhiteSpace(txtTime))
            {
                sb.Append(" and SCJD01 = '" + txtTime + "'");
            }
            if (!string.IsNullOrWhiteSpace(year))
            {
                sb.Append(" and years like '%" + year + "%'");
            }
            //添加季度搜索
            if (DDLjidu != "0")
            {
                sb.Append(" and JIJie like'%" + DDLjidu + "%'");
            }
            //添加工厂搜索
            if (txtgc != "")
            {
                sb.Append(" and SCJD22 like'%" + txtgc + "%'");
            }
            //添加跟单员搜索
            if (txtGD != "")
            {
                sb.Append(" and SCJD24 like'%" + txtGD + "%'");
            }
            //添加面料采购员搜索
            if (txtMlcg != "")
            {
                sb.Append(" and SCJD14 like'%" + txtMlcg + "%'");
            }
            //添加辅料采购员搜索
            if (txtFlcg != "")
            {
                sb.Append(" and SCJD103 like'%" + txtFlcg + "%'");
            }
            //添加品管人搜索
            if (txtPG != "")
            {
                sb.Append(" and SCJD31 like'%" + txtPG + "%'");
            }

            //当款号多选时根据款号ID查询
            if (!string.IsNullOrWhiteSpace(kh_id))
            {
                sb.Append(" and id in (" + kh_id + ")");
            }

            sb.Append(" and status = 1");
            try
            {
                DataTable dt = Common.GetSQLProcList(out RowsCount, out pageCount, "BPM_SCJDB", " * ", "id", OnePageC, page, " id desc ", sb.ToString());
                List<BPMSCJDBModels> SCJlist = new List<BPMSCJDBModels>();
                string time = "1900-01-01";
                #region 循环查询数据
                foreach (DataRow item in dt.Rows)
                {
                    BPMSCJDBModels u = new BPMSCJDBModels()
                    {
                        id = item["id"].ToString(),
                        SCJD01 = string.IsNullOrWhiteSpace(item["SCJD01"].ToString()) == true ? "" : Convert.ToDateTime(item["SCJD01"]).ToString("yyyy-MM-dd"),
                        SCJD02 = item["SCJD02"].ToString(),
                        SCJD03 = item["SCJD03"].ToString(),
                        SCJD04 = item["SCJD04"].ToString(),

                        SCJD06 = item["SCJD06"].ToString(),
                        SCJD05 = item["SCJD05"].ToString(),
                        SCJD07 = item["SCJD07"].ToString(),
                        SCJD08 = item["SCJD08"].ToString(),
                        SCJD09 = item["SCJD09"].ToString(),
                        SCJD10 = item["SCJD10"].ToString(),
                        SCJD11 = item["SCJD11"].ToString(),
                        SCJD12 = item["SCJD12"].ToString(),
                        SCJD13 = (Convert.ToDecimal(item["SCJD08"]) + Convert.ToDecimal(item["SCJD09"]) + Convert.ToDecimal(item["SCJD10"]) + Convert.ToDecimal(item["SCJD11"]) + Convert.ToDecimal(item["SCJD12"])).ToString(),
                        SCJD14 = item["SCJD14"].ToString(),
                        SCJD15 = string.IsNullOrWhiteSpace(item["SCJD15"].ToString()) == true ? "" : Convert.ToDateTime(item["SCJD15"]).ToString("yyyy-MM-dd"),
                        SCJD16 = string.IsNullOrWhiteSpace(item["SCJD16"].ToString()) == true ? "" : Convert.ToDateTime(item["SCJD16"]).ToString("yyyy-MM-dd"),
                        SCJD18 = string.IsNullOrWhiteSpace(item["SCJD18"].ToString()) == true ? "" : Convert.ToDateTime(item["SCJD18"]).ToString("yyyy-MM-dd"),
                        SCJD19 = string.IsNullOrWhiteSpace(item["SCJD19"].ToString()) == true ? "" : Convert.ToDateTime(item["SCJD19"]).ToString("yyyy-MM-dd"),
                        SCJD22 = item["SCJD22"].ToString(),
                        SCJD23 = string.IsNullOrWhiteSpace(item["SCJD23"].ToString()) == true ? "" : Convert.ToDateTime(item["SCJD23"]).ToString("yyyy-MM-dd"),
                        SCJD24 = item["SCJD24"].ToString(),
                        SCJD25 = string.IsNullOrWhiteSpace(item["SCJD25"].ToString()) == true ? "" : Convert.ToDateTime(item["SCJD25"]).ToString("yyyy-MM-dd"),
                        SCJD26 = string.IsNullOrWhiteSpace(item["SCJD26"].ToString()) == true ? "" : Convert.ToDateTime(item["SCJD26"]).ToString("yyyy-MM-dd"),
                        SCJD27 = string.IsNullOrWhiteSpace(item["SCJD27"].ToString()) == true ? "" : Convert.ToDateTime(item["SCJD27"]).ToString("yyyy-MM-dd"),
                        SCJD28 = string.IsNullOrWhiteSpace(item["SCJD28"].ToString()) == true ? "" : Convert.ToDateTime(item["SCJD28"]).ToString("yyyy-MM-dd"),
                        SCJD29 = string.IsNullOrWhiteSpace(item["SCJD29"].ToString()) == true ? "" : Convert.ToDateTime(item["SCJD29"]).ToString("yyyy-MM-dd"),
                        SCJD30 = string.IsNullOrWhiteSpace(item["SCJD30"].ToString()) == true ? "" : Convert.ToDateTime(item["SCJD30"]).ToString("yyyy-MM-dd"),
                        SCJD31 = item["SCJD31"].ToString(),
                        SCJD32 = string.IsNullOrWhiteSpace(item["SCJD32"].ToString()) == true ? "" : Convert.ToDateTime(item["SCJD32"]).ToString("yyyy-MM-dd"),
                        SCJD33 = string.IsNullOrWhiteSpace(item["SCJD33"].ToString()) == true ? "" : Convert.ToDateTime(item["SCJD33"]).ToString("yyyy-MM-dd"),

                        SCJD34 = string.IsNullOrWhiteSpace(item["SCJD34"].ToString()) == true ? "" : Convert.ToDateTime(item["SCJD34"]).ToString("yyyy-MM-dd"),
                        SCJD35 = string.IsNullOrWhiteSpace(item["SCJD35"].ToString()) == true ? "" : Convert.ToDateTime(item["SCJD35"]).ToString("yyyy-MM-dd"),
                        SCJD36 = string.IsNullOrWhiteSpace(item["SCJD36"].ToString()) == true ? "" : Convert.ToDateTime(item["SCJD36"]).ToString("yyyy-MM-dd"),
                        SCJD37 = string.IsNullOrWhiteSpace(item["SCJD37"].ToString()) == true ? "" : Convert.ToDateTime(item["SCJD37"]).ToString("yyyy-MM-dd"),
                        SCJD38 = string.IsNullOrWhiteSpace(item["SCJD38"].ToString()) == true ? "" : Convert.ToDateTime(item["SCJD38"]).ToString("yyyy-MM-dd"),
                        SCJD39 = string.IsNullOrWhiteSpace(item["SCJD39"].ToString()) == true ? "" : Convert.ToDateTime(item["SCJD39"]).ToString("yyyy-MM-dd"),
                        SCJD40 = string.IsNullOrWhiteSpace(item["SCJD40"].ToString()) == true ? "" : Convert.ToDateTime(item["SCJD40"]).ToString("yyyy-MM-dd"),
                        SCJD41 = string.IsNullOrWhiteSpace(item["SCJD41"].ToString()) == true ? "" : Convert.ToDateTime(item["SCJD41"]).ToString("yyyy-MM-dd"),
                        SCJD42 = item["SCJD42"].ToString(),
                        SCJD43 = item["SCJD43"].ToString(),
                        SCJD44 = item["SCJD44"].ToString(),
                        SCJD45 = item["SCJD45"].ToString(),
                        SCJD46 = item["SCJD46"].ToString(),
                        SCJD47 = string.IsNullOrWhiteSpace(item["SCJD47"].ToString()) == true ? "" : Convert.ToDateTime(item["SCJD47"]).ToString("yyyy-MM-dd"),
                        SCJD48 = string.IsNullOrWhiteSpace(item["SCJD48"].ToString()) == true ? "" : Convert.ToDateTime(item["SCJD48"]).ToString("yyyy-MM-dd"),
                        SCJD49 = string.IsNullOrWhiteSpace(item["SCJD49"].ToString()) == true ? "" : Convert.ToDateTime(item["SCJD49"]).ToString("yyyy-MM-dd"),
                        SCJD50 = item["SCJD50"].ToString(),
                        SCJD51 = item["SCJD51"].ToString(),
                        SCJD52 = item["SCJD52"].ToString(),
                        SCJD53 = item["SCJD53"].ToString(),
                        SCJD54 = item["SCJD54"].ToString(),
                        SCJD55 = item["SCJD55"].ToString(),
                        SCJD56 = item["SCJD56"].ToString(),
                        SCJD57 = item["SCJD57"].ToString(),
                        SCJD58 = string.IsNullOrWhiteSpace(item["SCJD58"].ToString()) == true ? "" : Convert.ToDateTime(item["SCJD58"]).ToString("yyyy-MM-dd"),
                        SCJD59 = string.IsNullOrWhiteSpace(item["SCJD59"].ToString()) == true ? "" : Convert.ToDateTime(item["SCJD59"]).ToString("yyyy-MM-dd"),
                        SCJD60 = string.IsNullOrWhiteSpace(item["SCJD60"].ToString()) == true ? "" : Convert.ToDateTime(item["SCJD60"]).ToString("yyyy-MM-dd"),
                        SCJD61 = string.IsNullOrWhiteSpace(item["SCJD61"].ToString()) == true ? "" : Convert.ToDateTime(item["SCJD61"]).ToString("yyyy-MM-dd"),
                        SCJD62 = string.IsNullOrWhiteSpace(item["SCJD62"].ToString()) == true ? "" : Convert.ToDateTime(item["SCJD62"]).ToString("yyyy-MM-dd"),
                        SCJD63 = string.IsNullOrWhiteSpace(item["SCJD63"].ToString()) == true ? "" : Convert.ToDateTime(item["SCJD63"]).ToString("yyyy-MM-dd"),
                        SCJD64 = string.IsNullOrWhiteSpace(item["SCJD64"].ToString()) == true ? "" : Convert.ToDateTime(item["SCJD64"]).ToString("yyyy-MM-dd"),
                        SCJD65 = string.IsNullOrWhiteSpace(item["SCJD65"].ToString()) == true ? "" : Convert.ToDateTime(item["SCJD65"]).ToString("yyyy-MM-dd"),
                        SCJD67 = string.IsNullOrWhiteSpace(item["SCJD67"].ToString()) == true ? "" : Convert.ToDateTime(item["SCJD67"]).ToString("yyyy-MM-dd"),//换成品控签单日期
                        SCJD75 = string.IsNullOrWhiteSpace(item["SCJD75"].ToString()) == true ? "" : Convert.ToDateTime(item["SCJD75"]).ToString("yyyy-MM-dd"),
                        SCJD86 = item["SCJD86"].ToString(),
                        SCJD87 = item["SCJD87"].ToString() == "1" ? "是" : "否",
                        shifouFK = item["shifouFK"].ToString() == "1" ? "是" : "否",
                        fukuanzt = item["fukuanzt"].ToString() == "1" ? "已付头款" : (item["fukuanzt"].ToString() == "2" ? "已付中期款" : (item["fukuanzt"].ToString() == "3" ? "已付尾款" : "未付款")),
                        SL = item["SL"].ToString(),
                        QXK = item["QXK"].ToString() == "0" || item["QXK"].ToString() == "" ? "否" : "是",
                        huoqi70day = string.IsNullOrWhiteSpace(item["huoqi70day"].ToString()) == true ? "" : Convert.ToDateTime(item["huoqi70day"]).ToString("yyyy-MM-dd"),
                        GG1DM = item["GG1DM"].ToString(),

                        //return a == 10 ? 10 : (a == 20 ? 20 : '未知')

                        SCJD88 = item["SCJD88"].ToString(),
                        SCJD90 = string.IsNullOrWhiteSpace(item["SCJD90"].ToString()) == true ? "" : Convert.ToDateTime(item["SCJD90"]).ToString("yyyy-MM-dd"),
                        SCJD91 = string.IsNullOrWhiteSpace(item["SCJD91"].ToString()) == true ? "" : Convert.ToDateTime(item["SCJD91"]).ToString("yyyy-MM-dd"),
                        SCJD92 = string.IsNullOrWhiteSpace(item["SCJD92"].ToString()) == true ? "" : Convert.ToDateTime(item["SCJD92"]).ToString("yyyy-MM-dd"),
                        years = string.IsNullOrWhiteSpace(item["years"].ToString()) == true ? "" : item["years"].ToString(),//Convert.ToDateTime(item["years"]).ToString(),
                        SCJD94 = item["SCJD94"].ToString(),
                        SCJD95 = item["SCJD95"].ToString(),
                        SCJD96 = item["SCJD96"].ToString(),
                        SCJD97 = item["SCJD97"].ToString(),
                        SCJD98 = item["SCJD98"].ToString(),
                        SCJD20 = (Convert.ToDecimal(item["SCJD94"]) + Convert.ToDecimal(item["SCJD95"]) + Convert.ToDecimal(item["SCJD96"]) + Convert.ToDecimal(item["SCJD97"]) + Convert.ToDecimal(item["SCJD98"])).ToString(),
                        SCJD99 = string.IsNullOrWhiteSpace(item["SCJD99"].ToString()) == true ? "" : Convert.ToDateTime(item["SCJD99"]).ToString("yyyy-MM-dd"),
                        SCJD100 = string.IsNullOrWhiteSpace(item["SCJD100"].ToString()) == true ? "" : Convert.ToDateTime(item["SCJD100"]).ToString("yyyy-MM-dd"),
                        SCJD101 = string.IsNullOrWhiteSpace(item["SCJD101"].ToString()) == true ? "" : Convert.ToDateTime(item["SCJD101"]).ToString("yyyy-MM-dd"),
                        SCJD102 = string.IsNullOrWhiteSpace(item["SCJD102"].ToString()) == true ? "" : Convert.ToDateTime(item["SCJD102"]).ToString("yyyy-MM-dd"),
                        SCJD103 = item["SCJD103"].ToString(),
                        SCJD104 = string.IsNullOrWhiteSpace(item["SCJD104"].ToString()) == true ? "" : Convert.ToDateTime(item["SCJD104"]).ToString("yyyy-MM-dd"),
                        SCJD105 = string.IsNullOrWhiteSpace(item["SCJD105"].ToString()) == true ? "" : Convert.ToDateTime(item["SCJD105"]).ToString("yyyy-MM-dd"),
                        SCJD106 = item["SCJD106"].ToString(),//添加一列’工厂送货总数‘
                        SCJD107 = string.IsNullOrWhiteSpace(item["SCJD107"].ToString()) == true ? "" : Convert.ToDateTime(item["SCJD107"]).ToString("yyyy-MM-dd"),//工厂送货日期

                        GCSHSUM = string.IsNullOrWhiteSpace(item["GCSHSUM"].ToString()) == true ? "" : Convert.ToDateTime(item["GCSHSUM"]).ToString("yyyy-MM-dd HH:mm:ss"),//仓库签收时间
                        RowsCount = RowsCount.ToString(),
                        pageCount = pageCount.ToString(),
                        CutAdd = (Convert.ToDecimal(item["SCJD50"]) + Convert.ToDecimal(item["SCJD51"]) + Convert.ToDecimal(item["SCJD52"]) + Convert.ToDecimal(item["SCJD53"]) + Convert.ToDecimal(item["SCJD54"])).ToString(),
                        ProAdd = (Convert.ToDecimal(item["SCJD42"]) + Convert.ToDecimal(item["SCJD43"]) + Convert.ToDecimal(item["SCJD44"]) + Convert.ToDecimal(item["SCJD45"]) + Convert.ToDecimal(item["SCJD46"])).ToString()
                    };
                    u.SCJD21 = (Convert.ToDecimal(u.SCJD20) - Convert.ToDecimal(u.SCJD13)).ToString();
                    u.SCJD55 = (Convert.ToDecimal(u.CutAdd) - Convert.ToDecimal(u.ProAdd)).ToString();
                    SCJlist.Add(u);
                }
                #endregion
                return Content(JsonConvert.SerializeObject(SCJlist));
            }
            catch (Exception ex)
            {
                return Content("error");
            }
        }

        /// <summary>
        /// 查询款号
        /// </summary>
        /// <returns></returns>
        public ActionResult SelSCJDBByGirard()
        {
            var SCJD05 = Request["SCJD05"];
            var SCJD06 = Request["SCJD06"];
            var yyyy = Request["yyyy"];

            StringBuilder sb = new StringBuilder("1=1");
            if (SCJD05 != "")
            {
                sb.Append(" and SCJD05 like '%" + SCJD05 + "%'");
            }
            if (SCJD06 != "")
            {
                sb.Append(" and SCJD06 like '%" + SCJD06 + "%'");
            }
            sb.Append(" and status = 1");
            string sql = "select id,SCJD04,SCJD05,SCJD06,SCJD07 from BPM_SCJDB where " + sb.ToString() + "";
            DataTable dt = SqlHelper.SelectTable(sql);
            return Content(JsonConvert.SerializeObject(dt));
        }


        /// <summary>
        /// 添加商品
        /// </summary>
        /// <returns></returns>
        [Property(MenuCode = "BPM_SCJDB", MenuOperation = "下单增加")]
        public ActionResult GetAddSCJDB(Models.BPMSCJDBModels SModel)
        {

            string sql = "insert into BPM_SCJDB(SCJD01,SCJD02,SCJD03,SCJD04,SCJD05,SCJD06,SCJD07,SCJD08,SCJD09,SCJD10,SCJD11,SCJD12,SCJD93)  values (@SCJD01,@SCJD02,@SCJD03,@SCJD04,@SCJD05,@SCJD06,@SCJD07,@SCJD08,@SCJD09,@SCJD10,@SCJD11,@SCJD12,@SCJD93) ";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@SCJD01", string.IsNullOrWhiteSpace(SModel.SCJD01) == true ? "" : SModel.SCJD01),
                new SqlParameter("@SCJD02", SModel.SCJD02),
                new SqlParameter("@SCJD03", SModel.SCJD03),
                new SqlParameter("@SCJD04", SModel.SCJD04),
                new SqlParameter("@SCJD05", string.IsNullOrWhiteSpace(SModel.SCJD05) == true ? "" : SModel.SCJD05),
                new SqlParameter("@SCJD06", string.IsNullOrWhiteSpace(SModel.SCJD06) == true ? "" : SModel.SCJD06),
                new SqlParameter("@SCJD07", string.IsNullOrWhiteSpace(SModel.SCJD07) == true ? "" : SModel.SCJD07),
                new SqlParameter("@SCJD08", string.IsNullOrWhiteSpace(SModel.SCJD08) == true ? "0" : SModel.SCJD08),
                new SqlParameter("@SCJD09", string.IsNullOrWhiteSpace(SModel.SCJD09) == true ? "0" : SModel.SCJD09),
                new SqlParameter("@SCJD10", string.IsNullOrWhiteSpace(SModel.SCJD10) == true ? "0" : SModel.SCJD10),
                new SqlParameter("@SCJD11", string.IsNullOrWhiteSpace(SModel.SCJD11) == true ? "0" : SModel.SCJD11),
                new SqlParameter("@SCJD12", string.IsNullOrWhiteSpace(SModel.SCJD12) == true ? "0" : SModel.SCJD12),
                new SqlParameter("@years", string.IsNullOrWhiteSpace(SModel.years) == true ? "" : SModel.years)
            };
            int count = SqlHelper.InsertDelUpdate(sql, param);
            if (count > 0)
            {
                return Content("OK");
            }
            else
            {
                return Content("KO");
            }

        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <returns></returns>
        [Property(MenuCode = "BPM_SCJDB", MenuOperation = "下单修改")]
        public ActionResult GetEditSCJDB(Models.BPMSCJDBModels SModel)
        {
            string sql = @"update dbo.BPM_SCJDB set SCJD01=@SCJD01,SCJD02=@SCJD02,SCJD03=@SCJD03,SCJD04=@SCJD04,SCJD05=@SCJD05,SCJD06=@SCJD06,
                              SCJD07 = @SCJD07,SCJD08 = @SCJD08,SCJD09 = @SCJD09,SCJD10 = @SCJD10,SCJD11 = @SCJD11,SCJD12 = @SCJD12,years = @years,QXK =@QXK where id = @id";
            SqlParameter[] param = new SqlParameter[]
            {
                    new SqlParameter("@SCJD01", string.IsNullOrWhiteSpace(SModel.SCJD01) == true ? "" : SModel.SCJD01),
                    new SqlParameter("@SCJD02", string.IsNullOrWhiteSpace(SModel.SCJD02) == true ? "" : SModel.SCJD02),
                    new SqlParameter("@SCJD03", string.IsNullOrWhiteSpace(SModel.SCJD03) == true ? "" : SModel.SCJD03),
                    new SqlParameter("@SCJD04", string.IsNullOrWhiteSpace(SModel.SCJD04) == true ? "" : SModel.SCJD04),
                    new SqlParameter("@SCJD05", string.IsNullOrWhiteSpace(SModel.SCJD05) == true ? "" : SModel.SCJD05),
                    new SqlParameter("@SCJD06", string.IsNullOrWhiteSpace(SModel.SCJD06) == true ? "" : SModel.SCJD06),
                    new SqlParameter("@SCJD07", string.IsNullOrWhiteSpace(SModel.SCJD07) == true ? "" : SModel.SCJD07),
                    new SqlParameter("@SCJD08", string.IsNullOrWhiteSpace(SModel.SCJD08) == true ? "0" : SModel.SCJD08),
                    new SqlParameter("@SCJD09", string.IsNullOrWhiteSpace(SModel.SCJD09) == true ? "0" : SModel.SCJD09),
                    new SqlParameter("@SCJD10", string.IsNullOrWhiteSpace(SModel.SCJD10) == true ? "0" : SModel.SCJD10),
                    new SqlParameter("@SCJD11", string.IsNullOrWhiteSpace(SModel.SCJD11) == true ? "0" : SModel.SCJD11),
                    new SqlParameter("@SCJD12", string.IsNullOrWhiteSpace(SModel.SCJD12) == true ? "0" : SModel.SCJD12),
                    new SqlParameter("@years", string.IsNullOrWhiteSpace(SModel.years) == true ? "" : SModel.years),
                    new SqlParameter("@QXK", string.IsNullOrWhiteSpace(SModel.QXK) == true ? "0" : SModel.QXK),
                    //new SqlParameter("@huoqi70day", string.IsNullOrWhiteSpace(SModel.huoqi70day) == true ? "" : SModel.huoqi70day),
                    new SqlParameter("@id", SModel.id)
            };
            int count = SqlHelper.InsertDelUpdate(sql, param);
            if (count > 0)
            {
                return Content("OK");
            }
            else
            {
                return Content("No");
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [Property(MenuCode = "BPM_SCJDB", MenuOperation = "下单删除")]
        public ActionResult DelSCJDB()
        {
            var id = Request["id"];
            id = id.Substring(0, id.Length - 1);
            string sql = "update BPM_SCJDB set status=0 where id in (" + id + ")";
            int count = SqlHelper.InsertDelUpdate(sql);
            if (count > 0)
            {
                return Content("OK");
            }
            else
            {
                return Content("NO");
            }
        }

        /// <summary>
        /// //批量修改时间
        /// </summary>
        [Property(MenuCode = "BPM_SCJDB", MenuOperation = "下单批量修改")]
        public ActionResult EditSCJDB()
        {
            var data = Request["data"];
            var id = Request["id"];
            var ckID = Request["ckval"];
            ckID = ckID.Substring(0, ckID.Length - 1);
            string sql = "update BPM_SCJDB set " + id + " = @data where id in (" + ckID + ") ";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@data",data)
            };
            int count = SqlHelper.InsertDelUpdate(sql, param);
            if (count > 0)
            {
                return Content("OK");
            }
            else
            {
                return Content("NO");
            }
        }

        /// <summary>
        /// 绑定波段下拉框
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDDLSCJD02()
        {
            string DDLjidu = Request["DDLjidu"];
            string sql = "";
            if (DDLjidu == "0")
            {
                sql = "select SXMC from FJSX2";
            }
            else
            {
                sql = "select SXMC from FJSX2 where SXMC like'%" + DDLjidu + "%'";
            }
            switch (DDLjidu)
            {
                case "春":
                    sql = @"select distinct(f.SXMC) from FJSX2 f with(nolock)
                        left join SHANGPIN s with(nolock) on f.SXDM = s.FJSX2 where s.BYZD8 >= 2018 and s.BYZD5 = 1
                        order by f.SXMC";
                    break;
                case "夏":
                    sql = @"select distinct(f.SXMC) from FJSX2 f with(nolock)
                        left join SHANGPIN s with(nolock) on f.SXDM = s.FJSX2 where s.BYZD8 >= 2018 and s.BYZD5 = 2
                        order by f.SXMC;";
                    break;
                case "秋":
                    sql = @"select distinct(f.SXMC) from FJSX2 f with(nolock)
                        left join SHANGPIN s with(nolock) on f.SXDM = s.FJSX2 where s.BYZD8 >= 2018 and s.BYZD5 = 3
                        order by f.SXMC;";
                    break;
                case "冬":
                    sql = @"select distinct(f.SXMC) from FJSX2 f with(nolock)
                        left join SHANGPIN s with(nolock) on f.SXDM = s.FJSX2 where s.BYZD8 >= 2018 and s.BYZD5 = 4
                    order by f.SXMC;";
                    break;
            }
           
            //  string sql1 = @"select distinct(f.SXMC) from FJSX2 with(nolock) f
            //            left join SHANGPIN with(nolock) s on f.SXDM = s.FJSX2 where s.BYZD8 >= 2018 and s.BYZD5 = 3";
            //}
            DataTable dt = SqlHelper.SelectTable(sql);
            return Content(JsonConvert.SerializeObject(dt));
        }

        /// <summary>
        /// Excel导出
        /// </summary>
        /// <returns></returns>
        //[Property(MenuCode = "BPM_SCJDB", MenuOperation = "下单Excel数据导出")]
        public ActionResult ExcelINTest()
        {
            var DDLjd = Request["DDLjd"];//添加季度搜索 txtGirard
            var txtGirard = Request["txtGirard"];
            var DDLpp = Request["DDLpp"];
            var DDLbo = Request["DDLbo"];
            var DDLmode = Request["DDLmode"];
            var txtStyle = Request["txtStyle"];
            var txtColor = Request["txtColor"];
            var txtTime = Request["txtTime"];
            var year = Request["year"];
            var ss = Request["sss"];
            StringBuilder sb = new StringBuilder("1=1");
            if (DDLjd != "0")
            {
                sb.Append(" and JIJie like'%" + DDLjd + "%'");
            }
            if (DDLmode != "0")
            {
                sb.Append(" and SCJD04='" + DDLmode + "'");
            }
            if (txtStyle != "")
            {
                sb.Append(" and SCJD06 like'%" + txtStyle + "%'");
            }
            if (txtGirard != "")
            {
                //if (txtGirard.Split('\'').Length > 1)
                //{
                //    sb.Append(" and SCJD05 in (" + txtGirard + ")");
                //}

                sb.Append(" and SCJD05 like '%" + txtGirard + "%'");
            }
            if (DDLbo != "0")
            {
                sb.Append(" and SCJD02 = '" + DDLbo + "'");
            }
            if (ss != "0" && ss!="")
            {
                sb.Append(" and QXK = '" + ss + "'");
            }
            if (DDLpp != "0")
            {
                sb.Append(" and SCJD03='" + DDLpp + "'");
            }
            if (txtColor != "")
            {
                sb.Append(" and SCJD07 like'%" + txtColor + "%'");
            }
            if (!string.IsNullOrWhiteSpace(txtTime))
            {
                sb.Append(" and SCJD01 = '" + txtTime + "'");
            }
            if (!string.IsNullOrWhiteSpace(year))
            {
                sb.Append(" and years like '%" + year + "%'");
            }
            sb.Append(" and status<>0 ");
            string sql = @"select SCJD02 as 波段,SCJD03 as 品牌,SCJD04 as 加工方式,SCJD06 as 款式,SCJD05 as 款号,SCJD07 as 颜色,GG1DM as 颜色代码,SCJD08 as '110/S',SCJD09 as '120/M',
                            SCJD10 as '130/L',SCJD11 as '140/XL',SCJD12 as '150/均码',(SCJD08+SCJD09+SCJD10+SCJD11+SCJD12) as 合计,SCJD01 as 商品交期,years as 商品年份
                        ,huoqi70day as 货期前70天商品下单 ,QXK as 是否取消款 from BPM_SCJDB where " + sb.ToString() + "";
            HSSFWorkbook book = GetExcel(sql);

            // 写入到客户端    
            return WriteInClient(book);
        }

        /// <summary>
        /// Excel模板导出
        /// </summary>
        /// <returns></returns>
        public ActionResult ExcelTINTest()
        {
            HSSFWorkbook book = new HSSFWorkbook();
            HSSFSheet sheet = book.CreateSheet("Sheet1") as HSSFSheet;
            //设置列宽  
            sheet.SetColumnWidth(0, 10 * 256);
            sheet.SetColumnWidth(1, 10 * 256);
            sheet.SetColumnWidth(2, 10 * 256);
            sheet.SetColumnWidth(3, 15 * 256);
            sheet.SetColumnWidth(4, 25 * 256);
            sheet.SetColumnWidth(5, 10 * 256);
            sheet.SetColumnWidth(11, 15 * 256);
            sheet.SetColumnWidth(12, 15 * 256);

            #region 标题部分  
            //给sheet添加第一行的头部标题
            NPOI.SS.UserModel.IRow row1 = sheet.CreateRow(0);

            row1.CreateCell(0).SetCellValue("波段");
            row1.CreateCell(1).SetCellValue("品牌");
            row1.CreateCell(2).SetCellValue("加工方式");
            row1.CreateCell(3).SetCellValue("款式");
            row1.CreateCell(4).SetCellValue("款号");
            row1.CreateCell(5).SetCellValue("颜色");
            row1.CreateCell(6).SetCellValue("颜色代码");
            row1.CreateCell(7).SetCellValue("110/S");
            row1.CreateCell(8).SetCellValue("120/M");
            row1.CreateCell(9).SetCellValue("130/L");
            row1.CreateCell(10).SetCellValue("140/XL");
            row1.CreateCell(11).SetCellValue("150/均码");
            row1.CreateCell(12).SetCellValue("商品交期");
            row1.CreateCell(13).SetCellValue("商品年份");
            row1.CreateCell(14).SetCellValue("季节");
            row1.CreateCell(15).SetCellValue("货期前70天商品下单");
            row1.CreateCell(16).SetCellValue("是否取消款");
            #endregion

            // 写入到客户端    
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.xls", "Excle" + DateTime.Now.ToString("yyyyMMddHHmmssfff")));
            Response.BinaryWrite(ms.ToArray());
            book = null;
            ms.Close();
            ms.Dispose();
            return Content("OK");
        }
        #endregion

        #region 生产部生产排单

        //Get PDInfo
        public ActionResult PDIndex()
        {
            return View();
        }

        //Get PDUpdate
        /// <summary>
        /// 编辑前绑定数据
        /// </summary>
        /// <returns></returns>
        public ActionResult PDUpdate(string id)
        {
            string sql = "select * from BPM_SCJDB where id=@id";
            DataTable dt = SqlHelper.SelectTable(sql, new SqlParameter("@id", id));
            if (dt.Rows.Count > 0)
            {
                ViewBag.SCJD02 = dt.Rows[0]["SCJD02"].ToString();
                ViewBag.SCJD03 = dt.Rows[0]["SCJD03"].ToString();
                ViewBag.SCJD04 = dt.Rows[0]["SCJD04"].ToString();
                ViewBag.SCJD05 = dt.Rows[0]["SCJD05"].ToString();
                ViewBag.SCJD06 = dt.Rows[0]["SCJD06"].ToString();
                ViewBag.SCJD07 = dt.Rows[0]["SCJD07"].ToString();
                //ViewBag.SCJD20 = dt.Rows[0]["SCJD20"].ToString();
                ViewBag.SCJD21 = dt.Rows[0]["SCJD21"].ToString();
                ViewBag.SCJD22 = dt.Rows[0]["SCJD22"].ToString();
                ViewBag.SCJD23 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD23"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["SCJD23"]).ToString("yyyy-MM-dd");
                ViewBag.SCJD24 = dt.Rows[0]["SCJD24"].ToString();
                ViewBag.SCJD34 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD34"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["SCJD34"]).ToString("yyyy-MM-dd");
                ViewBag.SCJD35 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD35"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["SCJD35"]).ToString("yyyy-MM-dd");
                ViewBag.SCJD36 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD36"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["SCJD36"]).ToString("yyyy-MM-dd");
                ViewBag.SCJD37 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD37"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["SCJD37"]).ToString("yyyy-MM-dd");
                ViewBag.SCJD38 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD38"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["SCJD38"]).ToString("yyyy-MM-dd");
                ViewBag.SCJD39 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD39"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["SCJD39"]).ToString("yyyy-MM-dd");
                ViewBag.SCJD40 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD40"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["SCJD40"]).ToString("yyyy-MM-dd");
                ViewBag.SCJD94 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD94"].ToString()) == true ? "0" : Convert.ToDecimal(dt.Rows[0]["SCJD94"]).ToString();
                ViewBag.SCJD95 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD95"].ToString()) == true ? "0" : Convert.ToDecimal(dt.Rows[0]["SCJD95"]).ToString();
                ViewBag.SCJD96 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD96"].ToString()) == true ? "0" : Convert.ToDecimal(dt.Rows[0]["SCJD96"]).ToString();
                ViewBag.SCJD97 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD97"].ToString()) == true ? "0" : Convert.ToDecimal(dt.Rows[0]["SCJD97"]).ToString();
                ViewBag.SCJD98 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD98"].ToString()) == true ? "0" : Convert.ToDecimal(dt.Rows[0]["SCJD98"]).ToString();
                ViewBag.SCJD99 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD99"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["SCJD99"]).ToString("yyyy-MM-dd");
                ViewBag.SCJD100 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD100"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["SCJD100"]).ToString("yyyy-MM-dd");
                ViewBag.SCJD102 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD102"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["SCJD102"]).ToString("yyyy-MM-dd");
                ViewBag.SCJD104 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD104"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["SCJD104"]).ToString("yyyy-MM-dd");
                ViewBag.SCJD41 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD41"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["SCJD41"]).ToString("yyyy-MM-dd");
                //ViewBag.SCJD08 = dt.Rows[0]["SCJD08"].ToString();
                //ViewBag.SCJD09 = dt.Rows[0]["SCJD09"].ToString();
                //ViewBag.SCJD10 = dt.Rows[0]["SCJD10"].ToString();
                //ViewBag.SCJD11 = dt.Rows[0]["SCJD11"].ToString();
                //ViewBag.SCJD12 = dt.Rows[0]["SCJD12"].ToString();
                ViewBag.SCJD47 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD47"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["SCJD47"]).ToString("yyyy-MM-dd");
                ViewBag.SCJD48 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD48"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["SCJD48"]).ToString("yyyy-MM-dd");
                ViewBag.SCJD50 = dt.Rows[0]["SCJD50"].ToString();
                ViewBag.SCJD51 = dt.Rows[0]["SCJD51"].ToString();
                ViewBag.SCJD52 = dt.Rows[0]["SCJD52"].ToString();
                ViewBag.SCJD53 = dt.Rows[0]["SCJD53"].ToString();
                ViewBag.SCJD54 = dt.Rows[0]["SCJD54"].ToString();
                ViewBag.SCJD55 = dt.Rows[0]["SCJD55"].ToString();
                ViewBag.SCJD56 = dt.Rows[0]["SCJD56"].ToString();
                ViewBag.SCJD57 = dt.Rows[0]["SCJD57"].ToString();
                ViewBag.SCJD67 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD67"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["SCJD67"]).ToString("yyyy-MM-dd");
                ViewBag.SCJD75 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD75"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["SCJD75"]).ToString("yyyy-MM-dd");
                ViewBag.SCJD86 = dt.Rows[0]["SCJD86"].ToString();
                ViewBag.SCJD87 = dt.Rows[0]["SCJD87"].ToString();

                ViewBag.SCJD106 = dt.Rows[0]["SCJD106"].ToString();//添加一列工厂送货总数
                ViewBag.SCJD107 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD107"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["SCJD107"]).ToString("yyyy-MM-dd"); ;//添加一列工厂送货日期
                ViewBag.SCJD49 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD49"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["SCJD49"]).ToString("yyyy-MM-dd");
                ViewBag.SCJD59 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD59"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["SCJD59"]).ToString("yyyy-MM-dd");
                ViewBag.SCJD61 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD61"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["SCJD61"]).ToString("yyyy-MM-dd");
                ViewBag.SL = dt.Rows[0]["SL"].ToString();
                ViewBag.GCSHSUM = string.IsNullOrWhiteSpace(dt.Rows[0]["GCSHSUM"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["GCSHSUM"]).ToString("yyyy-MM-dd HH:mm:ss");

            }
            ViewBag.id = id;
            return View();
        }

        ////--- table绑定数据，直接用商品部的GetIndexList“GetIndexList（）”方法 ---////


        /// <summary>
        /// 编辑的方法
        /// </summary>
        /// <param name="PDSModel"></param>
        /// <returns></returns>
        [Property(MenuCode = "BPM_SCJDB", MenuOperation = "排单修改")]
        public ActionResult GetEditPDSCJDB(Models.BPMSCJDBModels PDSModel)
        {
            string sql = @"update dbo.BPM_SCJDB set SCJD21=@SCJD21,SCJD94=@SCJD94,SCJD95=@SCJD95,SCJD96=@SCJD96,SCJD97=@SCJD97,SCJD98=@SCJD98,SCJD22=@SCJD22,SCJD23=@SCJD23,SCJD24=@SCJD24,SCJD34=@SCJD34,
                              SCJD35 = @SCJD35,SCJD36 = @SCJD36,SCJD37 = @SCJD37,SCJD38 = @SCJD38,SCJD39 = @SCJD39,SCJD40 = @SCJD40,SCJD99 = @SCJD99,SCJD100 = @SCJD100,SCJD104 = @SCJD104,SCJD41=@SCJD41,SCJD47=@SCJD47,SCJD48=@SCJD48,SCJD50 = @SCJD50
                          ,SCJD51 = @SCJD51,SCJD52 = @SCJD52,SCJD53 = @SCJD53,SCJD54=@SCJD54,SCJD55 = @SCJD55,SCJD56 = @SCJD56,SCJD57 = @SCJD57,
                           SCJD67 = @SCJD67,SCJD75 = @SCJD75,SCJD86 = @SCJD86,SCJD87 = @SCJD87,SCJD106 = @SCJD106,SCJD107 = @SCJD107,SCJD49=@SCJD49,SCJD59=@SCJD59,SCJD61=@SCJD61,SL=@SL,GCSHSUM=@GCSHSUM where id=@id ";
            //,SCJD08=@SCJD08,SCJD09=@SCJD09,SCJD10=@SCJD10,SCJD11=@SCJD11,SCJD12=@SCJD12
            SqlParameter[] param = new SqlParameter[]
            {
                //new SqlParameter("@SCJD20", string.IsNullOrWhiteSpace(PDSModel.SCJD20) == true ? "0" : PDSModel.SCJD20),
                new SqlParameter("@SCJD21", string.IsNullOrWhiteSpace(PDSModel.SCJD21) == true ? "" : PDSModel.SCJD21),
                new SqlParameter("@SCJD22", string.IsNullOrWhiteSpace(PDSModel.SCJD22) == true ? "" : PDSModel.SCJD22),
                new SqlParameter("@SCJD23", string.IsNullOrWhiteSpace(PDSModel.SCJD23) == true ? "" : PDSModel.SCJD23),
                new SqlParameter("@SCJD24", string.IsNullOrWhiteSpace(PDSModel.SCJD24) == true ? "" : PDSModel.SCJD24),
                new SqlParameter("@SCJD34", string.IsNullOrWhiteSpace(PDSModel.SCJD34) == true ? "" : PDSModel.SCJD34),
                new SqlParameter("@SCJD35", string.IsNullOrWhiteSpace(PDSModel.SCJD35) == true ? "" : PDSModel.SCJD35),
                new SqlParameter("@SCJD36", string.IsNullOrWhiteSpace(PDSModel.SCJD36) == true ? "" : PDSModel.SCJD36),
                new SqlParameter("@SCJD37", string.IsNullOrWhiteSpace(PDSModel.SCJD37) == true ? "" : PDSModel.SCJD37),
                new SqlParameter("@SCJD38", string.IsNullOrWhiteSpace(PDSModel.SCJD38) == true ? "" : PDSModel.SCJD38),
                new SqlParameter("@SCJD39", string.IsNullOrWhiteSpace(PDSModel.SCJD39) == true ? "" : PDSModel.SCJD39),
                new SqlParameter("@SCJD40", string.IsNullOrWhiteSpace(PDSModel.SCJD40) == true ? "" : PDSModel.SCJD40),
                new SqlParameter("@SCJD94", string.IsNullOrWhiteSpace(PDSModel.SCJD94) == true ? "" : PDSModel.SCJD94),
                new SqlParameter("@SCJD95", string.IsNullOrWhiteSpace(PDSModel.SCJD95) == true ? "" : PDSModel.SCJD95),
                new SqlParameter("@SCJD96", string.IsNullOrWhiteSpace(PDSModel.SCJD96) == true ? "" : PDSModel.SCJD96),
                new SqlParameter("@SCJD97", string.IsNullOrWhiteSpace(PDSModel.SCJD97) == true ? "" : PDSModel.SCJD97),
                new SqlParameter("@SCJD98", string.IsNullOrWhiteSpace(PDSModel.SCJD98) == true ? "" : PDSModel.SCJD98),
                new SqlParameter("@SCJD99", string.IsNullOrWhiteSpace(PDSModel.SCJD99) == true ? "" : PDSModel.SCJD99),
                new SqlParameter("@SCJD100", string.IsNullOrWhiteSpace(PDSModel.SCJD100) == true ? "" : PDSModel.SCJD100),
                new SqlParameter("@SCJD102", string.IsNullOrWhiteSpace(PDSModel.SCJD102) == true ? "" : PDSModel.SCJD102),
                new SqlParameter("@SCJD104", string.IsNullOrWhiteSpace(PDSModel.SCJD104) == true ? "" : PDSModel.SCJD104),
                new SqlParameter("@SCJD41", string.IsNullOrWhiteSpace(PDSModel.SCJD41) == true ? "" : PDSModel.SCJD41),
                //new SqlParameter("@SCJD08", string.IsNullOrWhiteSpace(PDSModel.SCJD08) == true ? "" : PDSModel.SCJD08),
                //new SqlParameter("@SCJD09", string.IsNullOrWhiteSpace(PDSModel.SCJD09) == true ? "" : PDSModel.SCJD09),
                //new SqlParameter("@SCJD10", string.IsNullOrWhiteSpace(PDSModel.SCJD10) == true ? "" : PDSModel.SCJD10),
                //new SqlParameter("@SCJD11", string.IsNullOrWhiteSpace(PDSModel.SCJD11) == true ? "" : PDSModel.SCJD11),
                //new SqlParameter("@SCJD12", string.IsNullOrWhiteSpace(PDSModel.SCJD12) == true ? "" : PDSModel.SCJD12),
                new SqlParameter("@SCJD47", string.IsNullOrWhiteSpace(PDSModel.SCJD47) == true ? "" : PDSModel.SCJD47),
                new SqlParameter("@SCJD48", string.IsNullOrWhiteSpace(PDSModel.SCJD48) == true ? "" : PDSModel.SCJD48),
                new SqlParameter("@SCJD50", string.IsNullOrWhiteSpace(PDSModel.SCJD50) == true ? "0" : PDSModel.SCJD50),
                new SqlParameter("@SCJD51", string.IsNullOrWhiteSpace(PDSModel.SCJD51) == true ? "0" : PDSModel.SCJD51),
                new SqlParameter("@SCJD52", string.IsNullOrWhiteSpace(PDSModel.SCJD52) == true ? "0" : PDSModel.SCJD52),
                new SqlParameter("@SCJD53", string.IsNullOrWhiteSpace(PDSModel.SCJD53) == true ? "0" : PDSModel.SCJD53),
                new SqlParameter("@SCJD54", string.IsNullOrWhiteSpace(PDSModel.SCJD54) == true ? "0" : PDSModel.SCJD54),
                new SqlParameter("@SCJD55", string.IsNullOrWhiteSpace(PDSModel.SCJD55) == true ? "" : PDSModel.SCJD55),
                new SqlParameter("@SCJD56", string.IsNullOrWhiteSpace(PDSModel.SCJD56) == true ? "" : PDSModel.SCJD56),
                new SqlParameter("@SCJD57", string.IsNullOrWhiteSpace(PDSModel.SCJD57) == true ? "" : PDSModel.SCJD57),
                new SqlParameter("@SCJD67", string.IsNullOrWhiteSpace(PDSModel.SCJD67) == true ? "" : PDSModel.SCJD67),
                new SqlParameter("@SCJD75", string.IsNullOrWhiteSpace(PDSModel.SCJD75) == true ? "" : PDSModel.SCJD75),
                new SqlParameter("@SCJD86", string.IsNullOrWhiteSpace(PDSModel.SCJD86) == true ? "" : PDSModel.SCJD86),
                new SqlParameter("@SCJD87", PDSModel.SCJD87),
                //添加一列工厂送货总数
                new SqlParameter("@SCJD106", string.IsNullOrWhiteSpace(PDSModel.SCJD106) == true ? "0" : PDSModel.SCJD106),
                new SqlParameter("@SCJD107", string.IsNullOrWhiteSpace(PDSModel.SCJD107) == true ? "" : PDSModel.SCJD107),
                new SqlParameter("@SCJD49", string.IsNullOrWhiteSpace(PDSModel.SCJD49) == true ? "" : PDSModel.SCJD49),
                new SqlParameter("@SCJD59", string.IsNullOrWhiteSpace(PDSModel.SCJD59) == true ? "" : PDSModel.SCJD59),
                new SqlParameter("@SCJD61", string.IsNullOrWhiteSpace(PDSModel.SCJD61) == true ? "" : PDSModel.SCJD61),
                new SqlParameter("@SL", string.IsNullOrWhiteSpace(PDSModel.SL) == true ? "0" : PDSModel.SL),
                 new SqlParameter("@GCSHSUM", string.IsNullOrWhiteSpace(PDSModel.GCSHSUM) == true ? "" : PDSModel.GCSHSUM),
                new SqlParameter("@id", PDSModel.id)
            };
            int count = SqlHelper.InsertDelUpdate(sql, param);
            if (count > 0)
            {
                //EditCGMan(PDSModel.SCJD22, PDSModel.id);//当加工方式为“FOB”时修改工厂会同步使面料/辅料采购员变更为工厂名称
                return Content("OK");
            }
            else
            {
                return Content("No");
            }
        }

        /// <summary>
        /// //批量修改时间
        /// </summary>
        [Property(MenuCode = "BPM_SCJDB", MenuOperation = "排单批量修改")]
        public ActionResult EditPDTime()
        {
            var data = Request["data"];
            var id = Request["id"];
            var ckID = Request["ckval"];
            ckID = ckID.Substring(0, ckID.Length - 1);
            string sql = "update BPM_SCJDB set " + id + " = @data where id in (" + ckID + ") ";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@data",data)
            };
            int count = SqlHelper.InsertDelUpdate(sql, param);
            if (count > 0)
            {
                return Content("OK");
            }
            else
            {
                return Content("NO");
            }
        }

        /// <summary>
        /// //批量修改工厂
        /// </summary>
        [Property(MenuCode = "BPM_SCJDB", MenuOperation = "排单批量修改")]
        public ActionResult EditPDfactory()
        {

            var Gc = Request["Gc"];
            var id = Request["id"];
            var ckID = Request["ckval"];
            ckID = ckID.Substring(0, ckID.Length - 1);
            string sql = "update BPM_SCJDB set " + id + " = @Gc where id in (" + ckID + ") ";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Gc",Gc)
            };
            int count = SqlHelper.InsertDelUpdate(sql, param);
            if (count > 0)
            {
                EditCGMan(Gc, ckID);//当加工方式为“FOB”时修改工厂会同步使面料/辅料采购员变更为工厂名称
                return Content("OK");
            }
            else
            {
                return Content("NO");
            }
        }

        ///// <summary>
        ///// //批量修改商品下单
        ///// </summary>
        //[Property(MenuCode = "BPM_SCJDB", MenuOperation = "商品下单修改")]
        //public ActionResult EditPDfactory1()
        //{

        //    var Gc = Request["Gc"];
        //    var id = Request["id"];
        //    var ckID = Request["ckval"];
        //    ckID = ckID.Substring(0, ckID.Length - 1);
        //    string sql = "update BPM_SCJDB set " + id + " = @Gc where id in (" + ckID + ") ";
        //    SqlParameter[] param = new SqlParameter[]
        //    {
        //        new SqlParameter("@Gc",Gc)
        //    };
        //    int count = SqlHelper.InsertDelUpdate(sql, param);
        //    if (count > 0)
        //    {
        //        EditCGMan(Gc, ckID);//当加工方式为“FOB”时修改工厂会同步使面料/辅料采购员变更为工厂名称
        //        return Content("OK");
        //    }
        //    else
        //    {
        //        return Content("NO");
        //    }
        //}

        /// <summary>
        /// Excel导出
        /// </summary>
        /// <returns></returns>
        //[Property(MenuCode = "BPM_SCJDB", MenuOperation = "排单Excel数据导出")]
        public ActionResult ExcelINPDTest()
        {
            var DDLjidu = Request["DDLjd"];//添加季度搜索 txtGirard
            var txtGirard = Request["txtGirard"];
            var DDLpp = Request["DDLpp"];
            var DDLbo = Request["DDLbo"];
            var DDLmode = Request["DDLmode"];
            var txtgc = Request["txtFactory"];//添加工厂搜索
            var txtGD = Request["txtGD"];//添加跟单员搜索
            var txtStyle = Request["txtStyle"];
            var txtColor = Request["txtColor"];
            var txtTime = Request["txtTime"];
            var year = Request["year"];
            StringBuilder sb = new StringBuilder("1=1");
            if (DDLjidu != "0")
            {
                sb.Append(" and JIJie like'%" + DDLjidu + "%'");
            }
            if (DDLmode != "0")
            {
                sb.Append(" and SCJD04='" + DDLmode + "'");
            }
            if (txtStyle != "")
            {
                sb.Append(" and SCJD06 like'%" + txtStyle + "%'");
            }
            if (txtGirard != "")
            {
                //if (txtGirard.Split('\'').Length > 1)
                //{
                //    sb.Append(" and SCJD05 in (" + txtGirard + ")");
                //}

                sb.Append(" and SCJD05 like '%" + txtGirard + "%'");
            }
            if (DDLbo != "0")
            {
                sb.Append(" and SCJD02 = '" + DDLbo + "'");
            }
            if (DDLpp != "0")
            {
                sb.Append(" and SCJD03='" + DDLpp + "'");
            }
            if (txtgc != "")
            {
                sb.Append(" and SCJD22 like'%" + txtgc + "%'");
            }
            if (txtGD != "")
            {
                sb.Append(" and SCJD24 like'%" + txtGD + "%'");
            }
            if (txtColor != "")
            {
                sb.Append(" and SCJD07 like'%" + txtColor + "%'");
            }
            if (!string.IsNullOrWhiteSpace(txtTime))
            {
                sb.Append(" and SCJD01 = '" + txtTime + "'");
            }
            if (!string.IsNullOrWhiteSpace(year))
            {
                sb.Append(" and years like '%" + year + "%'");
            }
            sb.Append(" and status<>0 ");
            string sql = @"select SCJD01 as 商品交期,years as 年份,JiJie as 季节,SCJD02 as 波段,SCJD03 as 品牌,SCJD04 as 加工方式,SCJD06 as 款式,SCJD05 as 款号,SCJD07 as 颜色,
                        (SCJD08+SCJD09+SCJD10+SCJD11+SCJD12) as 商品下单,SCJD94 as '110/S',SCJD95 as '120/M',SCJD96 as '130/L',SCJD97 as '140/XL',SCJD98 as '150/均码',
                        (SCJD94+SCJD95+SCJD96+SCJD97+SCJD98) as 已发数量,((SCJD94+SCJD95+SCJD96+SCJD97+SCJD98)-(SCJD08+SCJD09+SCJD10+SCJD11+SCJD12)) as 物控下单欠数,SCJD22 as 工厂,
                        SCJD23 as 合同货期,SCJD24 as 跟单员,SCJD99 as 技术部提供用量,SCJD100 as 物控下采购建议单,SCJD34 as 技术部下单期,SCJD35 as 批颜色,SCJD36 as 批产前办,SCJD37 as 提供检测报告日期,
                        SCJD38 as 洗水唛时间,SCJD104 as 贴纸时间,SCJD39 as 物控下单日期,SCJD40 as 齐料日期,SCJD102 as 财务部付款时间,SCJD41 as 外发下单日期,SCJD47 as 产前样提供日期,
                        SCJD48 as 产前OK日期,SCJD49 as 开裁日期,SCJD59 as 车间上线日期,SCJD61 as 车间下线日期,SL as 车间成品数量,SCJD08 as '110/S下单',SCJD09 as '120/M下单',
                        SCJD10 as '130/L下单',SCJD11 as '140/XL下单',SCJD12 as '150/均码下单',(SCJD08+SCJD09+SCJD10+SCJD11+SCJD12) as 生产下单总数,SCJD50 as '110/S实裁',SCJD51 as '120/M实裁',
                        SCJD52 as '130/L实裁',SCJD53 as '140/XL实裁',SCJD54 as '150/均码实裁',(SCJD50+SCJD51+SCJD52+SCJD53+SCJD54) as 实裁合计,SCJD55 as 裁剪差异数,SCJD56 as 差异原因,SCJD57 as 生产异常,
                        SCJD67 as 品控签单日期,SCJD107 as 工厂送货日期,SCJD106 as 工厂送货总数,GCSHSUM as 仓库签收时间,SCJD75 as 对账时间,SCJD86 as 延期备注,SCJD87 as 尾数状态,shifouFK as 是否付款,fukuanzt as 付款状态 from BPM_SCJDB where " + sb.ToString() + "";
            HSSFWorkbook book = GetExcelPD(sql);
            // 写入到客户端    
            return WriteInClient(book);
        }

        /// <summary>
        /// Excel模板导出
        /// </summary>
        /// <returns></returns>
        public ActionResult ExcelMbINPDTest()
        {
            string sql = "select SCJD02 as 波段,SCJD03 as 品牌,SCJD04 as 加工方式,SCJD06 as 款式,SCJD05 as 款号,SCJD07 as 颜色,SCJD94 as '110/S',SCJD95 as '120/M',SCJD96 as '130/L',SCJD97 as '140/XL',SCJD98 as '150/均码',SCJD22 as 工厂,SCJD23 as 合同货期,SCJD24 as 跟单员,SCJD99 as 技术部提供用量,SCJD100 as 物控下采购建议单,SCJD34 as 技术部下单期,SCJD35 as 批颜色,SCJD36 as 批产前办,SCJD37 as 提供检测报告日期,SCJD38 as 洗水唛时间,SCJD104 as 贴纸时间,SCJD39 as 物控下单日期,SCJD40 as 齐料日期,SCJD102 as 财务部付款时间,SCJD41 as 外发下单日期,SCJD47 as 产前样提供日期,SCJD48 as 产前OK日期,SCJD50 as '110/S实裁',SCJD51 as '120/M实裁',SCJD52 as '130/L实裁',SCJD53 as '140/XL实裁',SCJD54 as '150/均码实裁',SCJD55 as 裁剪差异数,SCJD56 as 差异原因,SCJD57 as 生产异常,SCJD67 as 品控签单日期,SCJD107 as 工厂送货日期,SCJD75 as 对账时间,SCJD86 as 延期备注,SCJD87 as 尾数状态 from BPM_SCJDB where 1!=1 ";
            HSSFWorkbook book = GetExcel(sql);//,SCJD08 as '110/S下单',SCJD09 as '120/M下单',SCJD10 as '130/L下单',SCJD11 as '140/XL下单',SCJD12 as '150/均码下单'

            // 写入到客户端    
            return WriteInClient(book);
        }
        #endregion

        #region 生产进度报工

        //Get PsJobIndex
        public ActionResult PsJobIndex()
        {
            return View();
        }

        //Get  PsJobIndex
        /// <summary>
        /// 编辑前绑定数据
        /// </summary>
        /// <returns></returns>
        public ActionResult PsJobUpdate(string id)
        {
            string sql = "select * from BPM_SCJDB where id=@id";
            DataTable dt = SqlHelper.SelectTable(sql, new SqlParameter("@id", id));
            if (dt.Rows.Count > 0)
            {
                ViewBag.SCJD02 = dt.Rows[0]["SCJD02"].ToString();
                ViewBag.SCJD03 = dt.Rows[0]["SCJD03"].ToString();
                ViewBag.SCJD04 = dt.Rows[0]["SCJD04"].ToString();
                ViewBag.SCJD05 = dt.Rows[0]["SCJD05"].ToString();
                ViewBag.SCJD06 = dt.Rows[0]["SCJD06"].ToString();
                ViewBag.SCJD07 = dt.Rows[0]["SCJD07"].ToString();
                ViewBag.SCJD41 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD41"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["SCJD41"]).ToString("yyyy-MM-dd");
                ViewBag.SCJD42 = dt.Rows[0]["SCJD42"].ToString();
                ViewBag.SCJD43 = dt.Rows[0]["SCJD43"].ToString();
                ViewBag.SCJD44 = dt.Rows[0]["SCJD44"].ToString();
                ViewBag.SCJD45 = dt.Rows[0]["SCJD45"].ToString();
                ViewBag.SCJD46 = dt.Rows[0]["SCJD46"].ToString();
                ViewBag.SCJD47 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD47"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["SCJD47"]).ToString("yyyy-MM-dd");
                ViewBag.SCJD48 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD48"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["SCJD48"]).ToString("yyyy-MM-dd");
                ViewBag.SCJD50 = dt.Rows[0]["SCJD50"].ToString();
                ViewBag.SCJD51 = dt.Rows[0]["SCJD51"].ToString();
                ViewBag.SCJD52 = dt.Rows[0]["SCJD52"].ToString();
                ViewBag.SCJD53 = dt.Rows[0]["SCJD53"].ToString();
                ViewBag.SCJD54 = dt.Rows[0]["SCJD54"].ToString();
                ViewBag.SCJD55 = dt.Rows[0]["SCJD55"].ToString();
                ViewBag.SCJD56 = dt.Rows[0]["SCJD56"].ToString();
                ViewBag.SCJD57 = dt.Rows[0]["SCJD57"].ToString();
                ViewBag.SCJD67 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD67"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["SCJD67"]).ToString("yyyy-MM-dd");
                ViewBag.SCJD75 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD75"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["SCJD75"]).ToString("yyyy-MM-dd");
                ViewBag.SCJD86 = dt.Rows[0]["SCJD86"].ToString();
                ViewBag.SCJD87 = dt.Rows[0]["SCJD87"].ToString();
            }
            ViewBag.id = id;
            return View();
        }

        /// <summary>
        /// 编辑的方法
        /// </summary>
        /// <param name="PDSModel"></param>
        /// <returns></returns>
        [Property(MenuCode = "BPM_SCJDB", MenuOperation = "生产进度修改")]
        public ActionResult GetEditPsJobSCJDB(Models.BPMSCJDBModels PsJobSModel)
        {
            string sql = @"update dbo.BPM_SCJDB set SCJD41=@SCJD41,SCJD42=@SCJD42,SCJD43=@SCJD43,SCJD44=@SCJD44,SCJD45=@SCJD45,SCJD46=@SCJD46,SCJD47=@SCJD47,SCJD48=@SCJD48,SCJD50 = @SCJD50
                          ,SCJD51 = @SCJD51,SCJD52 = @SCJD52,SCJD53 = @SCJD53,SCJD54=@SCJD54,SCJD55 = @SCJD55,SCJD56 = @SCJD56,SCJD57 = @SCJD57,
                           SCJD67 = @SCJD67,SCJD75 = @SCJD75,SCJD86 = @SCJD86,SCJD87 = @SCJD87 where id = @id";
            SqlParameter[] param = new SqlParameter[]
            {
                    new SqlParameter("@SCJD41", string.IsNullOrWhiteSpace(PsJobSModel.SCJD41) == true ? "" : PsJobSModel.SCJD41),
                    new SqlParameter("@SCJD42", string.IsNullOrWhiteSpace(PsJobSModel.SCJD42) == true ? "" : PsJobSModel.SCJD42),
                    new SqlParameter("@SCJD43", string.IsNullOrWhiteSpace(PsJobSModel.SCJD43) == true ? "" : PsJobSModel.SCJD43),
                    new SqlParameter("@SCJD44", string.IsNullOrWhiteSpace(PsJobSModel.SCJD44) == true ? "" : PsJobSModel.SCJD44),
                    new SqlParameter("@SCJD45", string.IsNullOrWhiteSpace(PsJobSModel.SCJD45) == true ? "" : PsJobSModel.SCJD45),
                    new SqlParameter("@SCJD46", string.IsNullOrWhiteSpace(PsJobSModel.SCJD46) == true ? "" : PsJobSModel.SCJD46),
                    new SqlParameter("@SCJD47", string.IsNullOrWhiteSpace(PsJobSModel.SCJD47) == true ? "" : PsJobSModel.SCJD47),
                    new SqlParameter("@SCJD48", string.IsNullOrWhiteSpace(PsJobSModel.SCJD48) == true ? "" : PsJobSModel.SCJD48),
                    new SqlParameter("@SCJD50", string.IsNullOrWhiteSpace(PsJobSModel.SCJD50) == true ? "0" : PsJobSModel.SCJD50),
                    new SqlParameter("@SCJD51", string.IsNullOrWhiteSpace(PsJobSModel.SCJD51) == true ? "0" : PsJobSModel.SCJD51),
                    new SqlParameter("@SCJD52", string.IsNullOrWhiteSpace(PsJobSModel.SCJD52) == true ? "0" : PsJobSModel.SCJD52),
                    new SqlParameter("@SCJD53", string.IsNullOrWhiteSpace(PsJobSModel.SCJD53) == true ? "0" : PsJobSModel.SCJD53),
                    new SqlParameter("@SCJD54", string.IsNullOrWhiteSpace(PsJobSModel.SCJD54) == true ? "0" : PsJobSModel.SCJD54),
                    new SqlParameter("@SCJD55", string.IsNullOrWhiteSpace(PsJobSModel.SCJD55) == true ? "" : PsJobSModel.SCJD55),
                    new SqlParameter("@SCJD56", string.IsNullOrWhiteSpace(PsJobSModel.SCJD56) == true ? "" : PsJobSModel.SCJD56),
                    new SqlParameter("@SCJD57", string.IsNullOrWhiteSpace(PsJobSModel.SCJD57) == true ? "" : PsJobSModel.SCJD57),
                    new SqlParameter("@SCJD67", string.IsNullOrWhiteSpace(PsJobSModel.SCJD67) == true ? "" : PsJobSModel.SCJD67),
                    new SqlParameter("@SCJD75", string.IsNullOrWhiteSpace(PsJobSModel.SCJD75) == true ? "" : PsJobSModel.SCJD75),
                    new SqlParameter("@SCJD86", string.IsNullOrWhiteSpace(PsJobSModel.SCJD86) == true ? "" : PsJobSModel.SCJD86),
                    new SqlParameter("@SCJD87", PsJobSModel.SCJD87),
                    new SqlParameter("@id", PsJobSModel.id)
            };
            int count = SqlHelper.InsertDelUpdate(sql, param);
            if (count > 0)
            {
                return Content("OK");
            }
            else
            {
                return Content("No");
            }
        }

        /// <summary>
        /// //批量修改时间
        /// </summary>
        [Property(MenuCode = "BPM_SCJDB", MenuOperation = "生产进度批量修改")]
        public ActionResult EditPsJobTime()
        {
            var data = Request["data"];
            var id = Request["id"];
            var ckID = Request["ckval"];
            ckID = ckID.Substring(0, ckID.Length - 1);
            string sql = "update BPM_SCJDB set " + id + " = @data where id in (" + ckID + ") ";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@data",data)
            };
            int count = SqlHelper.InsertDelUpdate(sql, param);
            if (count > 0)
            {
                return Content("OK");
            }
            else
            {
                return Content("NO");
            }
        }

        /// <summary>
        /// Excel导出
        /// </summary>
        /// <returns></returns>
        [Property(MenuCode = "BPM_SCJDB", MenuOperation = "生产进度Excel数据导出")]
        public ActionResult ExcelINPsJobTest()
        {
            string sql = "select SCJD02 as 波段,SCJD03 as 品牌,SCJD04 as 加工方式,SCJD06 as 款式,SCJD05 as 款号,SCJD07 as 颜色,SCJD41 as 外发下单日期,SCJD47 as 产前样提供日期,SCJD48 as 产前OK日期,SCJD42 as '110/S下单',SCJD43 as '120/M下单',SCJD44 as '130/L下单',SCJD45 as '140/XL下单',SCJD46 as '150/均码下单',(SCJD42+SCJD43+SCJD44+SCJD45+SCJD46) as 生产下单总数,SCJD50 as '110/S实裁',SCJD51 as '120/M实裁',SCJD52 as '130/L实裁',SCJD53 as '140/XL实裁',SCJD54 as '150/均码实裁',(SCJD50+SCJD51+SCJD52+SCJD53+SCJD54) as 实裁合计,SCJD55 as 裁剪差异数,SCJD56 as 差异原因,SCJD57 as 生产异常,SCJD67 as 工厂送货日期,SCJD75 as 对账时间,SCJD86 as 延期备注,SCJD87 as 尾数状态 from BPM_SCJDB where status<>0 ";
            HSSFWorkbook book = GetExcel(sql);

            // 写入到客户端    
            return WriteInClient(book);
        }

        /// <summary>
        /// Excel模板导出
        /// </summary>
        /// <returns></returns>
        public ActionResult ExcelMbINPsJobTest()
        {
            HSSFWorkbook book = new HSSFWorkbook();
            HSSFSheet sheet = book.CreateSheet("Sheet1") as HSSFSheet;

            #region 标题部分  
            //给sheet添加第一行的头部标题
            NPOI.SS.UserModel.IRow row1 = sheet.CreateRow(0);

            row1.CreateCell(0).SetCellValue("波段");
            row1.CreateCell(1).SetCellValue("品牌");
            row1.CreateCell(2).SetCellValue("加工方式");
            row1.CreateCell(3).SetCellValue("款式");
            row1.CreateCell(4).SetCellValue("款号");
            row1.CreateCell(5).SetCellValue("颜色");
            row1.CreateCell(6).SetCellValue("外发下单日期");
            row1.CreateCell(7).SetCellValue("产前样提供日期");
            row1.CreateCell(8).SetCellValue("产前OK日期");
            row1.CreateCell(9).SetCellValue("110/S下单");
            row1.CreateCell(10).SetCellValue("120/M下单");
            row1.CreateCell(11).SetCellValue("130/L下单");
            row1.CreateCell(12).SetCellValue("140/XL下单");
            row1.CreateCell(13).SetCellValue("150/均码下单");
            row1.CreateCell(14).SetCellValue("110/S实裁");
            row1.CreateCell(15).SetCellValue("120/M实裁");
            row1.CreateCell(16).SetCellValue("130/L实裁");
            row1.CreateCell(17).SetCellValue("140/XL实裁");
            row1.CreateCell(18).SetCellValue("150/均码实裁");
            row1.CreateCell(19).SetCellValue("裁剪差异数");
            row1.CreateCell(20).SetCellValue("差异原因");
            row1.CreateCell(21).SetCellValue("生产异常");
            row1.CreateCell(22).SetCellValue("工厂送货日期");
            row1.CreateCell(23).SetCellValue("对账时间");
            row1.CreateCell(24).SetCellValue("延期备注");
            row1.CreateCell(25).SetCellValue("尾数状态");

            #endregion


            // 写入到客户端    
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.xls", "Excel" + DateTime.Now.ToString("yyyyMMddHHmmssfff")));
            Response.BinaryWrite(ms.ToArray());
            book = null;
            ms.Close();
            ms.Dispose();
            return Content("OK");
        }

        #endregion

        #region 采购部原料排期

        //Get CGIndex
        public ActionResult CGIndex()
        {
            return View();
        }

        //Get CGUpdate
        /// <summary>
        /// 传数据到前台，编辑前绑定数据
        /// </summary>
        /// <returns></returns>
        public ActionResult CGUpdate(string id)
        {
            string sql = "select * from BPM_SCJDB where id=@id";
            DataTable dt = SqlHelper.SelectTable(sql, new SqlParameter("@id", id));
            if (dt.Rows.Count > 0)
            {
                ViewBag.SCJD02 = dt.Rows[0]["SCJD02"].ToString();
                ViewBag.SCJD03 = dt.Rows[0]["SCJD03"].ToString();
                ViewBag.SCJD04 = dt.Rows[0]["SCJD04"].ToString();
                ViewBag.SCJD05 = dt.Rows[0]["SCJD05"].ToString();
                ViewBag.SCJD06 = dt.Rows[0]["SCJD06"].ToString();
                ViewBag.SCJD07 = dt.Rows[0]["SCJD07"].ToString();
                ViewBag.SCJD14 = dt.Rows[0]["SCJD14"].ToString();
                ViewBag.SCJD32 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD32"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["SCJD32"]).ToString("yyyy-MM-dd");
                ViewBag.SCJD15 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD15"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["SCJD15"]).ToString("yyyy-MM-dd");
                ViewBag.SCJD16 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD16"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["SCJD16"]).ToString("yyyy-MM-dd");
                ViewBag.SCJD90 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD90"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["SCJD90"]).ToString("yyyy-MM-dd");
                ViewBag.SCJD19 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD19"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["SCJD19"]).ToString("yyyy-MM-dd");
                ViewBag.SCJD18 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD18"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["SCJD18"]).ToString("yyyy-MM-dd");
                ViewBag.SCJD88 = dt.Rows[0]["SCJD88"].ToString();
                ViewBag.SCJD101 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD101"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["SCJD101"]).ToString("yyyy-MM-dd");
                ViewBag.SCJD33 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD33"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["SCJD33"]).ToString("yyyy-MM-dd");
                ViewBag.SCJD91 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD91"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["SCJD91"]).ToString("yyyy-MM-dd");
                ViewBag.SCJD103 = dt.Rows[0]["SCJD103"].ToString();
            }
            ViewBag.id = id;
            return View();
        }

        /// <summary>
        /// 编辑的方法
        /// </summary>
        /// <param name="CGModel"></param>
        /// <returns></returns>
        [Property(MenuCode = "BPM_SCJDB", MenuOperation = "排期修改")]
        public ActionResult GetEditCGSCJDB(Models.BPMSCJDBModels CGModel)
        {
            string sql = @"update dbo.BPM_SCJDB set SCJD14=@SCJD14,SCJD32=@SCJD32,SCJD15=@SCJD15,SCJD16=@SCJD16,SCJD90=@SCJD90,SCJD19=@SCJD19,
                              SCJD18 = @SCJD18,SCJD88 = @SCJD88,SCJD101 = @SCJD101,SCJD33=@SCJD33,SCJD91=@SCJD91,SCJD103=@SCJD103 where id = @id";
            SqlParameter[] param = new SqlParameter[]
            {
                    new SqlParameter("@SCJD14", string.IsNullOrWhiteSpace(CGModel.SCJD14) == true ? "" : CGModel.SCJD14),
                    new SqlParameter("@SCJD32", string.IsNullOrWhiteSpace(CGModel.SCJD32) == true ? "" : CGModel.SCJD32),
                    new SqlParameter("@SCJD15", string.IsNullOrWhiteSpace(CGModel.SCJD15) == true ? "" : CGModel.SCJD15),
                    new SqlParameter("@SCJD16", string.IsNullOrWhiteSpace(CGModel.SCJD16) == true ? "" : CGModel.SCJD16),
                    new SqlParameter("@SCJD90", string.IsNullOrWhiteSpace(CGModel.SCJD90) == true ? "" : CGModel.SCJD90),
                    new SqlParameter("@SCJD19", string.IsNullOrWhiteSpace(CGModel.SCJD19) == true ? "" : CGModel.SCJD19),
                    new SqlParameter("@SCJD18", string.IsNullOrWhiteSpace(CGModel.SCJD18) == true ? "" : CGModel.SCJD18),
                    new SqlParameter("@SCJD88", string.IsNullOrWhiteSpace(CGModel.SCJD88) == true ? "" : CGModel.SCJD88),
                    new SqlParameter("@SCJD101", string.IsNullOrWhiteSpace(CGModel.SCJD101) == true ? "" : CGModel.SCJD101),
                    new SqlParameter("@SCJD33",string.IsNullOrWhiteSpace(CGModel.SCJD33) == true ? "" : CGModel.SCJD33),
                    new SqlParameter("@SCJD91",string.IsNullOrWhiteSpace(CGModel.SCJD91) == true ? "" : CGModel.SCJD91),
                    new SqlParameter("@SCJD103",string.IsNullOrWhiteSpace(CGModel.SCJD103) == true ? "" : CGModel.SCJD103),
                    new SqlParameter("@id", CGModel.id)
            };
            int count = SqlHelper.InsertDelUpdate(sql, param);
            if (count > 0)
            {
                return Content("OK");
            }
            else
            {
                return Content("No");
            }
        }

        /// <summary>
        /// //批量修改时间
        /// </summary>
        [Property(MenuCode = "BPM_SCJDB", MenuOperation = "排期批量修改")]
        public ActionResult EditCGTime()
        {
            var data = Request["data"];
            var id = Request["id"];
            var ckID = Request["ckval"];
            ckID = ckID.Substring(0, ckID.Length - 1);
            string sql = "update BPM_SCJDB set " + id + " = @data where id in (" + ckID + ") ";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@data",data)
            };
            int count = SqlHelper.InsertDelUpdate(sql, param);
            if (count > 0)
            {
                return Content("OK");
            }
            else
            {
                return Content("NO");
            }
        }

        /// <summary>
        /// //批量修改采购员
        /// </summary>
        [Property(MenuCode = "BPM_SCJDB", MenuOperation = "排期批量修改")]
        public ActionResult EditBuyer()
        {
            var Gc = Request["Gc"];
            var id = Request["id"];
            var ckID = Request["ckval"];
            ckID = ckID.Substring(0, ckID.Length - 1);
            string sql = "update BPM_SCJDB set " + id + " = @Gc where id in (" + ckID + ") ";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Gc",Gc)
            };
            int count = SqlHelper.InsertDelUpdate(sql, param);
            if (count > 0)
            {
                return Content("OK");
            }
            else
            {
                return Content("NO");
            }
        }

        /// <summary>
        /// Excel导出
        /// </summary>
        /// <returns></returns>
        //[Property(MenuCode = "BPM_SCJDB", MenuOperation = "排期Excel数据导出")]
        public ActionResult ExcelINCGTest()
        {
            string sql = "select SCJD01 as 商品交期,SCJD02 as 波段,SCJD03 as 品牌,SCJD04 as 加工方式,SCJD06 as 款式,SCJD05 as 款号,SCJD07 as 颜色,(SCJD08+SCJD09+SCJD10+SCJD11+SCJD12) as 商品下单量,SCJD14 as 面料采购员,SCJD103 as 辅料采购员,SCJD101 as 采购下发合同,SCJD32 as 面料计划交期,SCJD15 as 采购复期,SCJD16 as 面料到仓日期,SCJD90 as 辅料计划交期,SCJD19 as 辅料实际交期,SCJD18 as 采购下单日期,SCJD88 as 面料编号,SCJD33 as 面料验货报告日期,SCJD91 as 辅料验货报告日期 from BPM_SCJDB where status<>0 ";
            HSSFWorkbook book = GetExcel1(sql);

            // 写入到客户端    
            return WriteInClient(book);
        }

        /// <summary>
        /// Excel模板导出
        /// </summary>
        /// <returns></returns>
        public ActionResult ExcelMbINCGTest()
        {
            HSSFWorkbook book = new HSSFWorkbook();
            HSSFSheet sheet = book.CreateSheet("Sheet1") as HSSFSheet;

            #region 标题部分  
            //给sheet添加第一行的头部标题
            NPOI.SS.UserModel.IRow row1 = sheet.CreateRow(0);
            row1.CreateCell(0).SetCellValue("波段");
            row1.CreateCell(1).SetCellValue("品牌");
            row1.CreateCell(2).SetCellValue("加工方式");
            row1.CreateCell(3).SetCellValue("款式");
            row1.CreateCell(4).SetCellValue("款号");
            row1.CreateCell(5).SetCellValue("颜色");
            row1.CreateCell(6).SetCellValue("面料采购员");
            row1.CreateCell(7).SetCellValue("辅料采购员");
            row1.CreateCell(8).SetCellValue("采购下发合同");
            row1.CreateCell(9).SetCellValue("面料计划交期");
            row1.CreateCell(10).SetCellValue("采购复期");
            row1.CreateCell(11).SetCellValue("面料到仓日期");
            row1.CreateCell(12).SetCellValue("辅料计划交期");
            row1.CreateCell(13).SetCellValue("辅料实际交期");
            row1.CreateCell(14).SetCellValue("采购下单日期");
            row1.CreateCell(15).SetCellValue("面料验货报告日期");
            row1.CreateCell(16).SetCellValue("辅料验货报告日期");
            row1.CreateCell(17).SetCellValue("面料编号");

            #endregion

            // 写入到客户端    
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.xls", "Excel" + DateTime.Now.ToString("yyyyMMddHHmmssfff")));
            Response.BinaryWrite(ms.ToArray());
            book = null;
            ms.Close();
            ms.Dispose();
            return Content("OK");
        }

        #endregion

        #region 打样进度

        //Get PullIndex
        public ActionResult PullIndex()
        {
            return View();
        }

        //Get PullUpdate
        /// <summary>
        /// 传数据到前台，编辑前绑定数据
        /// </summary>
        /// <returns></returns>
        public ActionResult PullUpdate(string id)
        {
            string sql = "select * from BPM_SCJDB where id=@id";
            DataTable dt = SqlHelper.SelectTable(sql, new SqlParameter("@id", id));
            if (dt.Rows.Count > 0)
            {
                ViewBag.SCJD02 = dt.Rows[0]["SCJD02"].ToString();
                ViewBag.SCJD03 = dt.Rows[0]["SCJD03"].ToString();
                ViewBag.SCJD04 = dt.Rows[0]["SCJD04"].ToString();
                ViewBag.SCJD05 = dt.Rows[0]["SCJD05"].ToString();
                ViewBag.SCJD06 = dt.Rows[0]["SCJD06"].ToString();
                ViewBag.SCJD07 = dt.Rows[0]["SCJD07"].ToString();
                ViewBag.SCJD25 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD25"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["SCJD25"]).ToString("yyyy-MM-dd");
                ViewBag.SCJD26 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD26"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["SCJD26"]).ToString("yyyy-MM-dd");
                ViewBag.SCJD27 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD27"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["SCJD27"]).ToString("yyyy-MM-dd");
                ViewBag.SCJD28 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD28"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["SCJD28"]).ToString("yyyy-MM-dd");
                ViewBag.SCJD29 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD29"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["SCJD29"]).ToString("yyyy-MM-dd");
                ViewBag.SCJD30 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD30"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["SCJD30"]).ToString("yyyy-MM-dd");
            }
            ViewBag.id = id;
            return View();
        }

        /// <summary>
        /// 编辑的方法
        /// </summary>
        /// <param name="PullModel"></param>
        /// <returns></returns>
        [Property(MenuCode = "BPM_SCJDB", MenuOperation = "打样修改")]
        public ActionResult GetEditPullSCJDB(Models.BPMSCJDBModels PullModel)
        {
            string sql = "update dbo.BPM_SCJDB set SCJD25=@SCJD25,SCJD26=@SCJD26,SCJD27=@SCJD27,SCJD28=@SCJD28,SCJD29=@SCJD29,SCJD30=@SCJD30 where id = @id";
            SqlParameter[] param = new SqlParameter[]
            {
                    new SqlParameter("@SCJD25", string.IsNullOrWhiteSpace(PullModel.SCJD25) == true ? "" : PullModel.SCJD25),
                    new SqlParameter("@SCJD26", string.IsNullOrWhiteSpace(PullModel.SCJD26) == true ? "" : PullModel.SCJD26),
                    new SqlParameter("@SCJD27", string.IsNullOrWhiteSpace(PullModel.SCJD27) == true ? "" : PullModel.SCJD27),
                    new SqlParameter("@SCJD28", string.IsNullOrWhiteSpace(PullModel.SCJD28) == true ? "" : PullModel.SCJD28),
                    new SqlParameter("@SCJD29", string.IsNullOrWhiteSpace(PullModel.SCJD29) == true ? "" : PullModel.SCJD29),
                    new SqlParameter("@SCJD30", string.IsNullOrWhiteSpace(PullModel.SCJD30) == true ? "" : PullModel.SCJD30),
                    new SqlParameter("@id", PullModel.id)
            };
            int count = SqlHelper.InsertDelUpdate(sql, param);
            if (count > 0)
            {
                return Content("OK");
            }
            else
            {
                return Content("No");
            }
        }

        /// <summary>
        /// //批量修改时间
        /// </summary>
        [Property(MenuCode = "BPM_SCJDB", MenuOperation = "打样批量修改")]
        public ActionResult EditPullTime()
        {
            var data = Request["data"];
            var id = Request["id"];
            var ckID = Request["ckval"];
            ckID = ckID.Substring(0, ckID.Length - 1);
            string sql = "update BPM_SCJDB set " + id + " = @data where id in (" + ckID + ") ";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@data",data)
            };
            int count = SqlHelper.InsertDelUpdate(sql, param);
            if (count > 0)
            {
                return Content("OK");
            }
            else
            {
                return Content("NO");
            }
        }

        /// <summary>
        /// Excel导出
        /// </summary>
        /// <returns></returns>
        [Property(MenuCode = "BPM_SCJDB", MenuOperation = "打样Excel数据导出")]
        public ActionResult ExcelINPullTest()
        {
            string sql = "select SCJD01 as 商品交期,SCJD02 as 波段,SCJD03 as 品牌,SCJD04 as 加工方式,SCJD06 as 款式,SCJD05 as 款号,SCJD07 as 颜色,SCJD25 as 资料接收日期,SCJD26 as 一次样接收时间,SCJD27 as 二次样下单时间,SCJD28 as 齐色样下单时间,SCJD29 as 成本提供时间,SCJD30 as '齐色面、辅料实样提供时间' from BPM_SCJDB where status<>0 ";
            HSSFWorkbook book = GetExcel(sql);

            // 写入到客户端    
            return WriteInClient(book);
        }

        /// <summary>
        /// Excel模板导出
        /// </summary>
        /// <returns></returns>
        public ActionResult ExcelMbINPullTest()
        {
            HSSFWorkbook book = new HSSFWorkbook();
            HSSFSheet sheet = book.CreateSheet("Sheet1") as HSSFSheet;

            #region 标题部分  
            //给sheet添加第一行的头部标题
            NPOI.SS.UserModel.IRow row1 = sheet.CreateRow(0);
            row1.CreateCell(0).SetCellValue("波段");
            row1.CreateCell(1).SetCellValue("品牌");
            row1.CreateCell(2).SetCellValue("加工方式");
            row1.CreateCell(3).SetCellValue("款式");
            row1.CreateCell(4).SetCellValue("款号");
            row1.CreateCell(5).SetCellValue("颜色");
            row1.CreateCell(6).SetCellValue("资料接收日期");
            row1.CreateCell(7).SetCellValue("一次样接收时间");
            row1.CreateCell(8).SetCellValue("二次样下单时间");
            row1.CreateCell(9).SetCellValue("齐色样下单时间");
            row1.CreateCell(10).SetCellValue("成本提供时间");
            row1.CreateCell(11).SetCellValue("齐色面、辅料实样提供时间");

            #endregion

            // 写入到客户端    
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.xls", "Excel" + DateTime.Now.ToString("yyyyMMddHHmmssfff")));
            Response.BinaryWrite(ms.ToArray());
            book = null;
            ms.Close();
            ms.Dispose();
            return Content("OK");
        }

        #endregion

        #region 品控质检报工

        //Get QACheckIndex
        public ActionResult QACheckIndex()
        {
            return View();
        }

        //Get QACheckUpdate
        /// <summary>
        /// 传数据到前台，编辑前绑定数据
        /// </summary>
        /// <returns></returns>
        public ActionResult QACheckUpdate(string id)
        {
            string sql = "select * from BPM_SCJDB where id=@id";
            DataTable dt = SqlHelper.SelectTable(sql, new SqlParameter("@id", id));
            if (dt.Rows.Count > 0)
            {
                ViewBag.SCJD02 = dt.Rows[0]["SCJD02"].ToString();
                ViewBag.SCJD03 = dt.Rows[0]["SCJD03"].ToString();
                ViewBag.SCJD04 = dt.Rows[0]["SCJD04"].ToString();
                ViewBag.SCJD05 = dt.Rows[0]["SCJD05"].ToString();
                ViewBag.SCJD06 = dt.Rows[0]["SCJD06"].ToString();
                ViewBag.SCJD07 = dt.Rows[0]["SCJD07"].ToString();
                ViewBag.SCJD31 = dt.Rows[0]["SCJD31"].ToString();
                ViewBag.SCJD58 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD58"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["SCJD58"]).ToString("yyyy-MM-dd");
                ViewBag.SCJD59 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD59"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["SCJD59"]).ToString("yyyy-MM-dd");
                ViewBag.SCJD60 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD60"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["SCJD60"]).ToString("yyyy-MM-dd");
                ViewBag.SCJD61 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD61"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["SCJD61"]).ToString("yyyy-MM-dd");
                ViewBag.SCJD62 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD62"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["SCJD62"]).ToString("yyyy-MM-dd");
                ViewBag.SCJD63 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD63"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["SCJD63"]).ToString("yyyy-MM-dd");
                ViewBag.SCJD64 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD64"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["SCJD64"]).ToString("yyyy-MM-dd");
                ViewBag.SCJD65 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD65"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["SCJD65"]).ToString("yyyy-MM-dd");
                ViewBag.SCJD92 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD92"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["SCJD92"]).ToString("yyyy-MM-dd");
                ViewBag.SCJD49 = string.IsNullOrWhiteSpace(dt.Rows[0]["SCJD49"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["SCJD49"]).ToString("yyyy-MM-dd");
            }
            ViewBag.id = id;
            return View();
        }

        /// <summary>
        /// 编辑的方法
        /// </summary>
        /// <param name="QACheckModel"></param>
        /// <returns></returns>
        [Property(MenuCode = "BPM_SCJDB", MenuOperation = "品控修改")]
        public ActionResult GetEditQACheckSCJDB(Models.BPMSCJDBModels QACheckModel)
        {
            string sql = @"update dbo.BPM_SCJDB set SCJD31=@SCJD31,SCJD58=@SCJD58,SCJD59=@SCJD59,SCJD60=@SCJD60,
                              SCJD61 = @SCJD61,SCJD62 = @SCJD62,SCJD63 = @SCJD63,SCJD64 = @SCJD64,SCJD65 = @SCJD65,SCJD49=@SCJD49 where id = @id";
            SqlParameter[] param = new SqlParameter[]
            {
                    new SqlParameter("@SCJD05",string.IsNullOrWhiteSpace(QACheckModel.SCJD05) == true ? "" : QACheckModel.SCJD05),
                    new SqlParameter("@SCJD31",string.IsNullOrWhiteSpace(QACheckModel.SCJD31) == true ? "" : QACheckModel.SCJD31),
                    new SqlParameter("@SCJD58",string.IsNullOrWhiteSpace(QACheckModel.SCJD58) == true ? "" : QACheckModel.SCJD58),
                    new SqlParameter("@SCJD59",string.IsNullOrWhiteSpace(QACheckModel.SCJD59) == true ? "" : QACheckModel.SCJD59),
                    new SqlParameter("@SCJD60",string.IsNullOrWhiteSpace(QACheckModel.SCJD60) == true ? "" : QACheckModel.SCJD60),
                    new SqlParameter("@SCJD61",string.IsNullOrWhiteSpace(QACheckModel.SCJD61) == true ? "" : QACheckModel.SCJD61),
                    new SqlParameter("@SCJD62",string.IsNullOrWhiteSpace(QACheckModel.SCJD62) == true ? "" : QACheckModel.SCJD62),
                    new SqlParameter("@SCJD63",string.IsNullOrWhiteSpace(QACheckModel.SCJD63) == true ? "" : QACheckModel.SCJD63),
                    new SqlParameter("@SCJD64",string.IsNullOrWhiteSpace(QACheckModel.SCJD64) == true ? "" : QACheckModel.SCJD64),
                    new SqlParameter("@SCJD65",string.IsNullOrWhiteSpace(QACheckModel.SCJD65) == true ? "" : QACheckModel.SCJD65),
                    new SqlParameter("@SCJD49", string.IsNullOrWhiteSpace(QACheckModel.SCJD49) == true ? "" : QACheckModel.SCJD49),
                    new SqlParameter("@id", QACheckModel.id)
            };
            int count = SqlHelper.InsertDelUpdate(sql, param);
            sql = @"update dbo.BPM_SCJDB set SCJD92 = @SCJD92 where SCJD05 = @SCJD05";
            count = SqlHelper.InsertDelUpdate(sql, new SqlParameter("@SCJD92", string.IsNullOrWhiteSpace(QACheckModel.SCJD92) == true ? "" : QACheckModel.SCJD92), new SqlParameter("@SCJD05", string.IsNullOrWhiteSpace(QACheckModel.SCJD05) == true ? "" : QACheckModel.SCJD05));
            if (count > 0)
            {
                return Content("OK");
            }
            else
            {
                return Content("No");
            }
        }

        /// <summary>
        /// //批量修改时间
        /// </summary>
        [Property(MenuCode = "BPM_SCJDB", MenuOperation = "品控批量修改")]
        public ActionResult EditQACheckTime()
        {
            var data = Request["data"];
            var id = Request["id"];
            var ckID = Request["ckval"];
            ckID = ckID.Substring(0, ckID.Length - 1);
            string sql = "update BPM_SCJDB set " + id + " = @data where id in (" + ckID + ") ";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@data",data)
            };
            int count = SqlHelper.InsertDelUpdate(sql, param);
            if (count > 0)
            {
                return Content("OK");
            }
            else
            {
                return Content("NO");
            }
        }

        /// <summary>
        /// //批量修改品管人
        /// </summary>
        [Property(MenuCode = "BPM_SCJDB", MenuOperation = "品控批量修改")]
        public ActionResult EditQCman()
        {
            var Gc = Request["Gc"];
            var id = Request["id"];
            var ckID = Request["ckval"];
            ckID = ckID.Substring(0, ckID.Length - 1);
            string sql = "update BPM_SCJDB set " + id + " = @Gc where id in (" + ckID + ") ";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Gc",Gc)
            };
            int count = SqlHelper.InsertDelUpdate(sql, param);
            if (count > 0)
            {
                return Content("OK");
            }
            else
            {
                return Content("NO");
            }
        }

        /// <summary>
        /// Excel导出
        /// </summary>
        /// <returns></returns>
        //[Property(MenuCode = "BPM_SCJDB", MenuOperation = "品控Excel数据导出")]
        public ActionResult ExcelINQACheckTest()
        {
            string sql = "select SCJD01 as 商品交期,SCJD02 as 波段,SCJD03 as 品牌,SCJD04 as 加工方式,SCJD06 as 款式,SCJD05 as 款号,SCJD07 as 颜色,SCJD31 as 品管人,SCJD58 as 产前会议日期,SCJD49 as 开裁日期,SCJD59 as 车间上线时间,SCJD60 as 中期检验时间,SCJD61 as 车间下线时间,SCJD62 as 尾查一次,SCJD63 as 尾查二次,SCJD64 as 尾查三次,SCJD65 as 尾期检验时间,SCJD92 as 红皮资料日期 from BPM_SCJDB where status<>0 ";
            HSSFWorkbook book = GetExcel(sql);

            // 写入到客户端    
            return WriteInClient(book);
        }

        /// <summary>
        /// Excel模板导出
        /// </summary>
        /// <returns></returns>
        public ActionResult ExcelMbINQACheckTest()
        {
            HSSFWorkbook book = new HSSFWorkbook();
            HSSFSheet sheet = book.CreateSheet("Sheet1") as HSSFSheet;

            #region 标题部分  
            //给sheet1添加第一行的头部标题
            NPOI.SS.UserModel.IRow row1 = sheet.CreateRow(0);
            row1.CreateCell(0).SetCellValue("波段");
            row1.CreateCell(1).SetCellValue("品牌");
            row1.CreateCell(2).SetCellValue("加工方式");
            row1.CreateCell(3).SetCellValue("款式");
            row1.CreateCell(4).SetCellValue("款号");
            row1.CreateCell(5).SetCellValue("颜色");
            row1.CreateCell(6).SetCellValue("品管人");
            row1.CreateCell(7).SetCellValue("产前会议日期");
            row1.CreateCell(8).SetCellValue("开裁日期");
            row1.CreateCell(9).SetCellValue("车间上线时间");
            row1.CreateCell(10).SetCellValue("中期检验时间");
            row1.CreateCell(11).SetCellValue("车间下线时间");
            row1.CreateCell(12).SetCellValue("尾查一次");
            row1.CreateCell(13).SetCellValue("尾查二次");
            row1.CreateCell(14).SetCellValue("尾查三次");
            row1.CreateCell(15).SetCellValue("尾期检验时间");
            row1.CreateCell(16).SetCellValue("红皮资料日期");

            #endregion


            // 写入到客户端    
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.xls", "Excel" + DateTime.Now.ToString("yyyyMMddHHmmssfff")));
            Response.BinaryWrite(ms.ToArray());
            book = null;
            ms.Close();
            ms.Dispose();
            return Content("OK");
        }

        #endregion


        /// <summary>
        /// 当加工方式为FOB时进行编辑工厂操作时（生产部生产排单编辑/批量修改工厂）面料、辅料采购员的值全部变为工厂名称
        /// </summary>
        /// <returns></returns>
        private ActionResult EditCGMan(string SCJD22, string id)
        {
            string sql = "update BPM_SCJDB set SCJD14=@SCJD14,SCJD103=@SCJD103 where SCJD04='FOB' and id in (" + id + ")";
            SqlHelper.InsertDelUpdate(sql, new SqlParameter("@SCJD14", SCJD22), new SqlParameter("@SCJD103", SCJD22));
            return Content("OK");
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
                        if (string.IsNullOrWhiteSpace(dt.Rows[i][dt.Columns[j].ColumnName].ToString()) || Convert.ToDateTime(dt.Rows[i][dt.Columns[j].ColumnName]).ToString("yyyy-MM-dd") == "1900-01-01")
                        {
                            row.CreateCell(j).SetCellValue("");
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
        private static HSSFWorkbook GetExcel1(string sql)
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
                        if (string.IsNullOrWhiteSpace(dt.Rows[i][dt.Columns[j].ColumnName].ToString()) || Convert.ToDateTime(dt.Rows[i][dt.Columns[j].ColumnName]).ToString("yyyy-MM-dd") == "1900-01-01")
                        {
                            row.CreateCell(j).SetCellValue("");
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
        private static HSSFWorkbook GetExcelPD(string sql)
        {
            HSSFWorkbook book = new HSSFWorkbook();
            HSSFSheet sheet = book.CreateSheet("Sheet1") as HSSFSheet;

            #region sqltoExcel  

            DataTable dt = SqlHelper.SelectTable(sql);
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["是否付款"].ToString() == "1")
                {
                    dr["是否付款"] = "是";
                }
                else if (dr["是否付款"].ToString() == "0")
                {
                    dr["是否付款"] = "否";
                }
                if (dr["尾数状态"].ToString() == "1")
                {
                    dr["尾数状态"] = "是";
                }
                else if (dr["尾数状态"].ToString() == "2" || dr["尾数状态"].ToString() == "0")
                {
                    dr["尾数状态"] = "否";
                }
                else if (dr["付款状态"].ToString() == "1")
                {
                    dr["付款状态"] = "已付头款";
                }
                if (dr["付款状态"].ToString() == "2")
                {
                    dr["付款状态"] = "已付中期款";
                }
                else if (dr["付款状态"].ToString() == "3")
                {
                    dr["付款状态"] = "已付尾款";
                }
                else if (dr["付款状态"].ToString() == "0")
                {
                    dr["付款状态"] = "未付款";
                }
            }
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
                        if (string.IsNullOrWhiteSpace(dt.Rows[i][dt.Columns[j].ColumnName].ToString()) || Convert.ToDateTime(dt.Rows[i][dt.Columns[j].ColumnName]).ToString("yyyy-MM-dd") == "1900-01-01")
                        {
                            row.CreateCell(j).SetCellValue("");
                        }
                        else
                        {
                            row.CreateCell(j).SetCellValue(Convert.ToDateTime(dt.Rows[i][dt.Columns[j].ColumnName]).ToString("yyyy-MM-dd HH:mm:ss"));
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
        private static HSSFWorkbook GetExcelPDD(string sql, string txtGirard)
        {
            HSSFWorkbook book = new HSSFWorkbook();
            HSSFSheet sheet = book.CreateSheet("Sheet1") as HSSFSheet;

            #region sqltoExcel  

            DataTable dt = SqlHelper.SelectTable(sql, new SqlParameter("SCJD05", txtGirard));
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["是否付款"].ToString() == "1")
                {
                    dr["是否付款"] = "是";
                }
                else if (dr["是否付款"].ToString() == "0")
                {
                    dr["是否付款"] = "否";
                }
                if (dr["尾数状态"].ToString() == "1")
                {
                    dr["尾数状态"] = "是";
                }
                else if (dr["尾数状态"].ToString() == "2" || dr["尾数状态"].ToString() == "0")
                {
                    dr["尾数状态"] = "否";
                }
                else if (dr["付款状态"].ToString() == "1")
                {
                    dr["付款状态"] = "已付头款";
                }
                if (dr["付款状态"].ToString() == "2")
                {
                    dr["付款状态"] = "已付中期款";
                }
                else if (dr["付款状态"].ToString() == "3")
                {
                    dr["付款状态"] = "已付尾款";
                }
                else if (dr["付款状态"].ToString() == "0")
                {
                    dr["付款状态"] = "未付款";
                }
            }
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
                        if (string.IsNullOrWhiteSpace(dt.Rows[i][dt.Columns[j].ColumnName].ToString()) || Convert.ToDateTime(dt.Rows[i][dt.Columns[j].ColumnName]).ToString("yyyy-MM-dd") == "1900-01-01")
                        {
                            row.CreateCell(j).SetCellValue("");
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

        #region Excel总导出
        [Property(MenuCode = "BPM_SCJDB", MenuOperation = "明细表导出")]
        public ActionResult GetExcelOut()
        {
            string sql = "select name,lieName from BPM_LieName where status =1 and name <> '是否取消款' and name <> '颜色代码' order by sumList ";
            DataTable dt = SqlHelper.SelectTable(sql);
            string lie = "/";
            string lieTow = "、";
            string sqls = "select ";
            foreach (DataRow dr in dt.Rows)
            {
                string lieCopyName = dr["name"].ToString();
                if (lieCopyName.Contains(lie) || lieCopyName.Contains(lieTow))
                {
                    lieCopyName = "'" + lieCopyName + "'";
                }
                sqls += " " + dr["lieName"].ToString() + " as " + lieCopyName + ",";
            }
            sqls = sqls.Substring(0, sqls.Length - 1);
            sqls += " from view_BPM_SCJD";
            HSSFWorkbook book = GetExcelPD(sqls);

            // 写入到客户端    
            return WriteInClient(book);
        }
        #endregion



    }
}