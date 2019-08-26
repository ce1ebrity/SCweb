using Newtonsoft.Json;
using SCWeb.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SCWeb.Controllers
{
    [Login(IsCheck = true)]
    public class ExamineBossController : Controller
    {
        #region 修改
        //生产修改
        [Property(MenuCode = "BPM_P_SPCBYSB_New_Ship", MenuOperation = "生产修改")]
        public ActionResult SCUpdate(string type, string GC, string spdm)
        {
            string sql = null;
            SqlParameter[] sqlp = null;
            if (GC == "")
            {
                GC = "0";
            }
            if (type == "5")
            {
                sql = "update BPM_P_SPCBYSB_New_Ship set GCHJ=@GC where spdm=@spdm";
                sqlp = new SqlParameter[] {
                    new SqlParameter("@GC",GC),
                    new SqlParameter("@spdm",spdm)
                };
            }
            else
            {
                sql = "update BPM_P_SPCBYSB_New_Ship set GC" + type + "=@GC where spdm=@spdm";
                sqlp = new SqlParameter[] {
                    new SqlParameter("@GC",GC),
                    new SqlParameter("@spdm",spdm)
                };
            }

            int i = (int)SqlHelper.InsertDelUpdate(sql, sqlp);
            if (i > 0)
                return Content("success");
            else
                return Content("error");
        }
        //商品修改
        [Property(MenuCode = "BPM_P_SPCBYSB_New_Ship", MenuOperation = "商品修改")]
        public ActionResult SPUpdate(string type, string GC, string spdm)
        {
            string sql = null;
            SqlParameter[] sqlp = null;
            if (GC == "")
            {
                GC = "0";
            }
            if (type == "6")
            {
                sql = "update BPM_P_SPCBYSB_New_Ship set CBQR=@CBQR,PCBJ=@PCBJ where spdm=@spdm";
                sqlp = new SqlParameter[] {
                    new SqlParameter("@CBQR",GC),
                    new SqlParameter("@PCBJ",GC),
                    new SqlParameter("@spdm",spdm)
                };
            }
            else if (type == "7")
            {
                sql = "update BPM_P_SPCBYSB_New_Ship set DDPJ=@DDPJ,B1=@B1,B2=@B2,PDPJ=@PDPJ where spdm=@spdm";
                sqlp = new SqlParameter[] {
                    new SqlParameter("@DDPJ",GC.Split('|')[0]),
                    new SqlParameter("@B1",GC.Split('|')[1]),
                    new SqlParameter("@B2",GC.Split('|')[2]),
                    new SqlParameter("@PDPJ",GC.Split('|')[0]),
                    new SqlParameter("@spdm",spdm)
                };
            }


            int i = (int)SqlHelper.InsertDelUpdate(sql, sqlp);
            if (i > 0)
                return Content("success");
            else
                return Content("error");
        }
        //公司修改
        [Property(MenuCode = "BPM_P_SPCBYSB_New_Ship", MenuOperation = "公司修改")]
        public ActionResult BOSSUpdate(string type, string GC, string spdm)
        {
            string sql = null;
            SqlParameter[] sqlp = null;
            if (GC == "")
            {
                GC = "0";
            }
            if (type == "8")
            {
                sql = "update BPM_P_SPCBYSB_New_Ship set PCBJ=@PCBJ where spdm=@spdm";
                sqlp = new SqlParameter[] {
                    new SqlParameter("@PCBJ",GC),
                    new SqlParameter("@spdm",spdm)
                };
            }
            else
            {
                sql = "update BPM_P_SPCBYSB_New_Ship set PDPJ=@PDPJ where spdm=@spdm";
                sqlp = new SqlParameter[] {
                    new SqlParameter("@PDPJ",GC),
                    new SqlParameter("@spdm",spdm)
                };
            }
            int i = (int)SqlHelper.InsertDelUpdate(sql, sqlp);
            if (i > 0)
                return Content("success");
            else
                return Content("error");
        }
        #endregion
        #region 审核
        //设计师
        [Property(MenuCode = "BPM_P_SPCBYSB_New_Ship", MenuOperation = "设计确认")]
        public ActionResult Auditing(string IsSJ, string spdm)
        {
            string sql = null;

            sql = "select IsBoss from BPM_P_SPCBYSB_New_Ship where SPDM=@SPDM";
            int i = Convert.ToInt32(SqlHelper.SelectSinger(sql, new SqlParameter("@SPDM", spdm)));
            //公司已确认设计师不允许修改
            //if (i == 1)
            //{
            //    return Content("error2");
            //}
            sql = "select IsSP from BPM_P_SPCBYSB_New_Ship where SPDM=@SPDM";
            i = Convert.ToInt32(SqlHelper.SelectSinger(sql, new SqlParameter("@SPDM", spdm)));
            //商品已确认设计师不允许修改
            if (i == 1)
            {
                return Content("error3");
            }
            string userId = Common.GetCookie("userLogin");
            sql = "select trueName from BPM_UserBase where id=@id";
            string trueName = (string)SqlHelper.SelectSinger(sql, new SqlParameter("@id", userId));//登录人姓名
            sql = "update BPM_P_SPCBYSB_New_Ship set IsSJ=@IsSJ,SJName=@SJName where SPDM=@SPDM";
            i = Convert.ToInt32(SqlHelper.InsertDelUpdate(sql, new SqlParameter("@IsSJ", IsSJ), new SqlParameter("@SJName", trueName), new SqlParameter("@SPDM", spdm)));
            if (i > 0)
                return Content("success|" + trueName);
            else
                return Content("error");
        }
        //商品部
        [Property(MenuCode = "BPM_P_SPCBYSB_New_Ship", MenuOperation = "商品确认")]
        public ActionResult Commodity(string IsSP, string spdm, string textrar)
        {
            string sql = null;


            sql = "select IsSJ from BPM_P_SPCBYSB_New_Ship where SPDM=@SPDM";
            int i = Convert.ToInt32(SqlHelper.SelectSinger(sql, new SqlParameter("@SPDM", spdm)));
            //设计师未确认不同意商品部确认
            if (i == 0)
            {
                return Content("error2");
            }
            sql = "select IsBoss from BPM_P_SPCBYSB_New_Ship where SPDM=@SPDM";
            i = Convert.ToInt32(SqlHelper.SelectSinger(sql, new SqlParameter("@SPDM", spdm)));
            //公司确认不同意商品部修改
            //if (i == 1)
            //{
            //    return Content("error3");
            //}


            string userId = Common.GetCookie("userLogin");
            sql = "select trueName from BPM_UserBase where id=@id";
            string trueName = (string)SqlHelper.SelectSinger(sql, new SqlParameter("@id", userId));//登录人姓名
            sql = "update BPM_P_SPCBYSB_New_Ship set IsSP=@IsSP,SPName=@SPName,Remark=@Remark where SPDM=@SPDM";
            i = Convert.ToInt32(SqlHelper.InsertDelUpdate(sql, new SqlParameter("@IsSP", IsSP), new SqlParameter("@SPName", trueName), new SqlParameter("@SPDM", spdm), new SqlParameter("@Remark", textrar)));
            if (i > 0)
                return Content("success|" + trueName);
            else
                return Content("error");
        }
        //公司
        [Property(MenuCode = "BPM_P_SPCBYSB_New_Ship", MenuOperation = "公司确认")]
        public ActionResult Boss(string IsBoss, string spdm)
        {
            string sql = null;


            sql = "select IsSJ from BPM_P_SPCBYSB_New_Ship where SPDM=@SPDM";
            int i = Convert.ToInt32(SqlHelper.SelectSinger(sql, new SqlParameter("@SPDM", spdm)));
            //设计师未确认不同意Boss确认
            //if (i == 0)
            //{
            //    return Content("error2");
            //}

            sql = "select IsSP from BPM_P_SPCBYSB_New_Ship where SPDM=@SPDM";
            i = Convert.ToInt32(SqlHelper.SelectSinger(sql, new SqlParameter("@SPDM", spdm)));
            //商品部未确认不同意Boss确认
            //if (i == 0)
            //{
            //    return Content("error3");
            //}


            string userId = Common.GetCookie("userLogin");
            sql = "select trueName from BPM_UserBase where id=@id";
            string trueName = (string)SqlHelper.SelectSinger(sql, new SqlParameter("@id", userId));//登录人姓名
            sql = "update BPM_P_SPCBYSB_New_Ship set IsBoss=@IsBoss,BossName=@BossName where SPDM=@SPDM";
            i = Convert.ToInt32(SqlHelper.InsertDelUpdate(sql, new SqlParameter("@IsBoss", IsBoss), new SqlParameter("@BossName", trueName), new SqlParameter("@SPDM", spdm)));
            if (i > 0)
                return Content("success|" + trueName);
            else
                return Content("error");
        }
        #endregion

        #region 查询
        // 测试查询
        public ActionResult Index222(string filter)
        {

            string sql = "P_SPCBYSB_New";

            //filter : 值|值
            ViewBag.filter = filter;
            string filters = "( ";
            if (string.IsNullOrWhiteSpace(filter))
            {
                filters += "YearCode='2018' and BrandName in ('Banana Baby') and SeasonName in ('春季') and Property03 in ('衬衫')";
            }
            else
            {
                var li = filter.Split('|');
                if (li.Length > 0)
                {
                    filters += "YearCode='" + li[0] + "' and BrandName in ('" + li[2] + "') and SeasonName in ('" + li[1] + "')";
                    if (li.Length > 3)
                    {
                        filters += " and Property03 in ('" + li[3] + "')";
                    }
                }
            }
            filters += " )";


            string spdmNull = "";

            SqlParameter[] para = new SqlParameter[] {
                new SqlParameter("@filter",filters)};
            DataTable dt = SqlHelper.ProcTable(sql, para);
            dt.Columns.Add("PICurl");//下载的图片url
            dt.Columns.Add("CBHSSUM");//成本核算
            dt.Columns.Add("CKJSUM");//参考价

            dt.Columns.Add("GC1");//工厂1
            dt.Columns.Add("GC2");//工厂2
            dt.Columns.Add("GC3");//工厂3
            dt.Columns.Add("GC4");//工厂4
            dt.Columns.Add("GCHJ");//工厂核价
            dt.Columns.Add("CBQR");//成本确认价
            dt.Columns.Add("DDPJ");//吊牌定价
            dt.Columns.Add("B1");//倍率1
            dt.Columns.Add("B2");//倍率2
            dt.Columns.Add("PCBJ");//批成本价
            dt.Columns.Add("PDPJ");//批吊牌价
            dt.Columns.Add("SJName");//设计师
            dt.Columns.Add("IsSJ");//设计师是否审核
            dt.Columns.Add("BossName");//公司审核人
            dt.Columns.Add("IsBoss");//是否审核
            foreach (DataRow dr in dt.Rows)
            {
                if (!string.IsNullOrWhiteSpace(dr["SPDM"].ToString()))
                {
                    spdmNull = dr["SPDM"].ToString();
                    string sqls = "select count(1) from BPM_P_SPCBYSB_New_Ship where spdm='" + spdmNull + "'";

                    if (Convert.ToInt32(SqlHelper.SelectSinger(sqls)) <= 0)
                    {
                        sqls = "insert into BPM_P_SPCBYSB_New_Ship(SPDM,IsCF,IsSJ,IsBoss) values(@SPDM,@IsCF,0,0)";
                        SqlHelper.InsertDelUpdate(sqls, new SqlParameter("@SPDM", spdmNull), new SqlParameter("@IsCF", dr["ML_SUM"].ToString().Length > 0 ? "CMT" : "FOB"));
                    }
                    else
                    {
                        //将自己保存的值合并datatable
                        sqls = "select * from BPM_P_SPCBYSB_New_Ship where spdm='" + spdmNull + "'";
                        DataTable dts = SqlHelper.SelectTable(sqls);
                        if (dts.Rows.Count > 0)
                        {
                            dr["GC1"] = dts.Rows[0]["GC1"].ToString();
                            dr["GC2"] = dts.Rows[0]["GC2"].ToString();
                            dr["GC3"] = dts.Rows[0]["GC3"].ToString();
                            dr["GC4"] = dts.Rows[0]["GC4"].ToString();
                            dr["GCHJ"] = dts.Rows[0]["GCHJ"].ToString();
                            dr["CBQR"] = dts.Rows[0]["CBQR"].ToString();
                            dr["DDPJ"] = dts.Rows[0]["DDPJ"].ToString();
                            dr["B1"] = dts.Rows[0]["B1"].ToString();
                            dr["B2"] = dts.Rows[0]["B2"].ToString();
                            dr["PCBJ"] = dts.Rows[0]["PCBJ"].ToString();
                            dr["PDPJ"] = dts.Rows[0]["PDPJ"].ToString();
                            dr["SJName"] = dts.Rows[0]["SJName"].ToString();
                            dr["IsSJ"] = dts.Rows[0]["IsSJ"].ToString();
                            dr["BossName"] = dts.Rows[0]["BossName"].ToString();
                            dr["IsBoss"] = dts.Rows[0]["IsBoss"].ToString();
                        }

                    }



                    //成本核算/参考价
                    if ((dr["ML_SUM"].ToString().Length > 0 ? "CMT" : "FOB") == "CMT")
                    {
                        decimal jiege = (Convert.ToDecimal(dr["QT_Cost"]) * Convert.ToDecimal(1.1));
                        dr["CBHSSUM"] = jiege.ToString();
                        dr["CKJSUM"] = (jiege * Convert.ToDecimal(5.8)).ToString();
                    }
                    else
                    {
                        decimal jiege = (Convert.ToDecimal(dr["QT_Cost"]) * Convert.ToDecimal(1.32));
                        dr["CBHSSUM"] = jiege.ToString();
                        dr["CKJSUM"] = (jiege * Convert.ToDecimal(5.8)).ToString();
                    }
                }
                else
                {
                    dr["SPDM"] = spdmNull;
                }

                dr["PICurl"] = "";
                if (dr["PIC"].ToString().Length > 0)
                {
                    byte[] photo = new byte[0];

                    photo = (byte[])dr["PIC"];//读取第一个图片的位流
                    string path = Server.MapPath("~/Upload").TrimEnd('\\') + @"\";//转为物理路径
                    FileStream fs = new FileStream(path + dr["MasterID"].ToString() + ".jpg", System.IO.FileMode.Create);
                    BinaryWriter bw = new BinaryWriter(fs);
                    bw.Write(photo, 0, photo.Length);
                    fs.Flush();//数据写入图片文件
                    fs.Close();
                    dr["PICurl"] = "/Upload/" + dr["MasterID"].ToString() + ".jpg";
                }
            }
            return View(dt);
        }
        // 测试查询
        public ActionResult Index22(string filter)
        {

            string sql = "P_SPCBYSB_New";

            //filter : 值|值

            string filters = "( ";
            if (string.IsNullOrWhiteSpace(filter))
            {
                filters += "YearCode='2018' and BrandName in ('Banana Baby') and SeasonName in ('春季') and Property03 in ('衬衫')";
            }
            else
            {
                var li = filter.Split('|');
                if (li.Length > 0)
                {
                    filters += "YearCode='" + li[0] + "' and BrandName in ('" + li[2] + "') and SeasonName in ('" + li[1] + "')";
                    if (li.Length > 3)
                    {
                        filters += " and Property03 in ('" + li[3] + "')";
                    }
                }
            }
            filters += " )";


            string spdmNull = "";

            SqlParameter[] para = new SqlParameter[] {
                new SqlParameter("@filter",filters)};
            DataTable dt = SqlHelper.ProcTable(sql, para);
            dt.Columns.Add("PICurl");//下载的图片url
            dt.Columns.Add("CBHSSUM");//成本核算
            dt.Columns.Add("CKJSUM");//参考价

            dt.Columns.Add("GC1");//工厂1
            dt.Columns.Add("GC2");//工厂2
            dt.Columns.Add("GC3");//工厂3
            dt.Columns.Add("GC4");//工厂4
            dt.Columns.Add("GCHJ");//工厂核价
            dt.Columns.Add("CBQR");//成本确认价
            dt.Columns.Add("DDPJ");//吊牌定价
            dt.Columns.Add("B1");//倍率1
            dt.Columns.Add("B2");//倍率2
            dt.Columns.Add("PCBJ");//批成本价
            dt.Columns.Add("PDPJ");//批吊牌价
            dt.Columns.Add("SJName");//设计师
            dt.Columns.Add("IsSJ");//设计师是否审核
            dt.Columns.Add("BossName");//公司审核人
            dt.Columns.Add("IsBoss");//是否审核
            foreach (DataRow dr in dt.Rows)
            {
                if (!string.IsNullOrWhiteSpace(dr["SPDM"].ToString()))
                {
                    spdmNull = dr["SPDM"].ToString();
                    string sqls = "select count(1) from BPM_P_SPCBYSB_New_Ship where spdm='" + spdmNull + "'";

                    if (Convert.ToInt32(SqlHelper.SelectSinger(sqls)) <= 0)
                    {
                        sqls = "insert into BPM_P_SPCBYSB_New_Ship(SPDM,IsCF,IsSJ,IsBoss) values(@SPDM,@IsCF,0,0)";
                        SqlHelper.InsertDelUpdate(sqls, new SqlParameter("@SPDM", spdmNull), new SqlParameter("@IsCF", dr["ML_SUM"].ToString().Length > 0 ? "CMT" : "FOB"));
                    }
                    else
                    {
                        //将自己保存的值合并datatable
                        sqls = "select * from BPM_P_SPCBYSB_New_Ship where spdm='" + spdmNull + "'";
                        DataTable dts = SqlHelper.SelectTable(sqls);
                        if (dts.Rows.Count > 0)
                        {
                            dr["GC1"] = dts.Rows[0]["GC1"].ToString();
                            dr["GC2"] = dts.Rows[0]["GC2"].ToString();
                            dr["GC3"] = dts.Rows[0]["GC3"].ToString();
                            dr["GC4"] = dts.Rows[0]["GC4"].ToString();
                            dr["GCHJ"] = dts.Rows[0]["GCHJ"].ToString();
                            dr["CBQR"] = dts.Rows[0]["CBQR"].ToString();
                            dr["DDPJ"] = dts.Rows[0]["DDPJ"].ToString();
                            dr["B1"] = dts.Rows[0]["B1"].ToString();
                            dr["B2"] = dts.Rows[0]["B2"].ToString();
                            dr["PCBJ"] = dts.Rows[0]["PCBJ"].ToString();
                            dr["PDPJ"] = dts.Rows[0]["PDPJ"].ToString();
                            dr["SJName"] = dts.Rows[0]["SJName"].ToString();
                            dr["IsSJ"] = dts.Rows[0]["IsSJ"].ToString();
                            dr["BossName"] = dts.Rows[0]["BossName"].ToString();
                            dr["IsBoss"] = dts.Rows[0]["IsBoss"].ToString();
                        }

                    }



                    //成本核算/参考价
                    if ((dr["ML_SUM"].ToString().Length > 0 ? "CMT" : "FOB") == "CMT")
                    {
                        decimal jiege = (Convert.ToDecimal(dr["QT_Cost"]) * Convert.ToDecimal(1.1));
                        dr["CBHSSUM"] = jiege.ToString();
                        dr["CKJSUM"] = (jiege * Convert.ToDecimal(5.8)).ToString();
                    }
                    else
                    {
                        decimal jiege = (Convert.ToDecimal(dr["QT_Cost"]) * Convert.ToDecimal(1.32));
                        dr["CBHSSUM"] = jiege.ToString();
                        dr["CKJSUM"] = (jiege * Convert.ToDecimal(5.8)).ToString();
                    }
                }
                else
                {
                    dr["SPDM"] = spdmNull;
                }

                dr["PICurl"] = "";
                if (dr["PIC"].ToString().Length > 0)
                {
                    byte[] photo = new byte[0];

                    photo = (byte[])dr["PIC"];//读取第一个图片的位流
                    string path = Server.MapPath("~/Upload").TrimEnd('\\') + @"\";//转为物理路径
                    FileStream fs = new FileStream(path + dr["MasterID"].ToString() + ".jpg", System.IO.FileMode.Create);
                    BinaryWriter bw = new BinaryWriter(fs);
                    bw.Write(photo, 0, photo.Length);
                    fs.Flush();//数据写入图片文件
                    fs.Close();
                    dr["PICurl"] = "/Upload/" + dr["MasterID"].ToString() + ".jpg";
                }
            }
            return View(dt);
        }

        public ActionResult ShowImg(int id)
        {
            string sql = "select SCJD05 from BPM_SCJDB with(nolock) where id='" + id + "'";
            string spdm = "";
            DataTable newDataTable = SqlHelper.SelectTable(sql);
            foreach (DataRow dr in newDataTable.Rows)
            {
                spdm = dr["SCJD05"].ToString();
            }
            string sqlproc = "P_SPCBYSB_New_CBHJ";
            string filters = "( ";
            filters += "SPDM in ('" + spdm + "')";
            filters += " )";
            SqlParameter[] para = new SqlParameter[] {
                new SqlParameter("@filter",filters)};
            DataTable dt = SqlHelper.ProcTable(sqlproc, para);
            string imgurl = "";
            foreach (DataRow dr1 in dt.Rows)
            {
                if (dr1["PIC"].ToString().Length > 0)
                {
                    byte[] photo = new byte[0];
                    photo = (byte[])dr1["PIC"];
                    string path = Server.MapPath("~/Upload").TrimEnd('\\') + @"\";
                    FileStream fs = new FileStream(path + dr1["MasterID"].ToString() + ".jpg", System.IO.FileMode.Create);
                    BinaryWriter bw = new BinaryWriter(fs);
                    bw.Write(photo, 0, photo.Length);
                    fs.Flush();
                    fs.Close();
                    imgurl = "/Upload/" + dr1["MasterID"].ToString() + ".jpg";

                }
            }
            return Content(imgurl);
        }
        // 正式查询
        public ActionResult Index(string filter)
        {
            //查询登录人品牌
            string userId = Common.GetCookie("userLogin");
            string userSql = "select brand from BPM_UserBase where id=@id";
            string brand = (string)SqlHelper.SelectSinger(userSql, new SqlParameter("@id", userId));

            ViewBag.brand = brand;
            string sql = "P_SPCBYSB_New_CBHJ";
            DataTable dt = null;
            ViewBag.filter = filter;
            //filter : 值|值


            string filters = "( ";

            if (string.IsNullOrWhiteSpace(filter))
            {
                return View(dt);
            }
            else
            {
                var li = filter.Split('|');
                if (li.Length > 0)
                {
                    if (li[2] != "1")
                    {
                        brand = li[2];
                    }

                    //如果需要查询款号其他条件舍弃
                    if (li.Length > 8 && li[8].Length > 0)
                    {
                        filters += "SPDM in ('" + li[8] + "')";
                    }
                    else
                    {
                        if (brand == "全部")
                        {
                            filters += "YearCode='" + li[0] + "' and SeasonName in ('" + li[1] + "')";
                        }
                        else
                        {
                            filters += "YearCode='" + li[0] + "' and BrandName in ('" + brand + "') and SeasonName in ('" + li[1] + "')";
                        }
                        if (li.Length > 3 && li[3].Length > 0)
                        {
                            filters += " and Property03 in ('" + li[3] + "')";
                        }
                        if (li.Length > 4 && li[4].Length > 0)
                        {
                            filters += " and BDMC in ('" + li[4] + "')";
                        }
                        //根据设计师确认查询款号
                        if (li.Length > 5 && li[5].Length > 0 || li[6].Length > 0 || li[7].Length > 0)
                        {
                            string sqls = "select spdm from BPM_P_SPCBYSB_New_Ship where 1=1 ";
                            if (li[5].Length > 0)
                            {
                                sqls += " and IsSJ=" + li[5];
                            }
                            if (li[6].Length > 0)
                            {
                                sqls += " and IsSP=" + li[6];
                            }
                            if (li[7].Length > 0)
                            {
                                sqls += " and IsBoss=" + li[7];
                            }

                            DataTable newDataTable = SqlHelper.SelectTable(sqls);
                            if (newDataTable.Rows.Count > 0)
                            {
                                filters += " and SPDM in (";

                                foreach (DataRow dr in newDataTable.Rows)
                                {
                                    filters += "'" + dr["spdm"].ToString() + "',";
                                }
                                //删除最后一个逗号
                                filters = filters.Substring(0, filters.Length - 1);

                                filters += ")";
                            }
                            else
                            {
                                return View(dt);
                            }


                        }
                    }
                }
            }
            filters += " )";


            string spdmNull = "";
            try
            {
                SqlParameter[] para = new SqlParameter[] {
                new SqlParameter("@filter",filters)};
                dt = SqlHelper.ProcTable(sql, para);
                dt.Columns.Add("PICurl");//下载的图片url
                dt.Columns.Add("CBHSSUM");//成本核算
                dt.Columns.Add("CKJSUM");//参考价

                dt.Columns.Add("GC1");//工厂1
                dt.Columns.Add("GC2");//工厂2
                dt.Columns.Add("GC3");//工厂3
                dt.Columns.Add("GC4");//工厂4
                dt.Columns.Add("GCHJ");//工厂核价
                dt.Columns.Add("CBQR");//成本确认价
                dt.Columns.Add("DDPJ");//吊牌定价
                dt.Columns.Add("B1");//倍率1
                dt.Columns.Add("B2");//倍率2
                dt.Columns.Add("PCBJ");//批成本价
                dt.Columns.Add("PDPJ");//批吊牌价
                dt.Columns.Add("SJName");//设计师
                dt.Columns.Add("IsSJ");//设计师是否审核
                dt.Columns.Add("BossName");//公司审核人
                dt.Columns.Add("IsBoss");//是否审核
                dt.Columns.Add("SPName");//商品审核人
                dt.Columns.Add("IsSP");//是否审核
                dt.Columns.Add("Remark");
                dt.Columns.Add("BZcailiao");
                dt.Columns.Add("danjia");
                dt.Columns.Add("jiagongjiajia");
                dt.Columns.Add("danjia1");
                dt.Columns.Add("TeShugy");
                dt.Columns.Add("danjia2");


                int rowindex = 0;
                string id = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    id = dt.Rows[i]["MasterID"].ToString();
                    if (dt.Rows[i]["QT_Item"].ToString() == "包装材料" && dt.Rows[i]["masterid"].ToString() == id)
                    {
                        rowindex = i;//得到索引
                        dt.Rows[i]["BZcailiao"] = dt.Rows[i]["QT_Item"].ToString();
                        dt.Rows[i]["danjia"] = dt.Rows[i]["QT_Price"].ToString();
                    }
                    else if (dt.Rows[i]["QT_Item"].ToString() == "加工工价" && dt.Rows[i]["masterid"].ToString() == id)
                    {
                        rowindex = i;//得到索引
                        dt.Rows[i - 1]["jiagongjiajia"] = dt.Rows[i]["QT_Item"].ToString();
                        dt.Rows[i - 1]["danjia1"] = dt.Rows[i]["QT_Price"].ToString();
                    }
                    else if (dt.Rows[i]["QT_Item"].ToString() != "包装材料" || dt.Rows[i]["QT_Item"].ToString() != "加工工价" && dt.Rows[i]["masterid"].ToString() == id)
                    {
                        rowindex = i;//得到索引
                        dt.Rows[i - 1]["TeShugy"] = dt.Rows[i]["QT_Item"].ToString();
                        dt.Rows[i - 1]["danjia2"] = dt.Rows[i]["QT_Price"].ToString();
                    }

                }

                foreach (DataRow dr in dt.Rows)
                {
                    if (!string.IsNullOrWhiteSpace(dr["SPDM"].ToString()))
                    {
                        spdmNull = dr["SPDM"].ToString();
                        string sqls = "select count(1) from BPM_P_SPCBYSB_New_Ship where spdm='" + spdmNull + "'";

                        if (Convert.ToInt32(SqlHelper.SelectSinger(sqls)) <= 0)
                        {
                            sqls = "insert into BPM_P_SPCBYSB_New_Ship(SPDM,IsCF,IsSJ,IsBoss,YearCode) values(@SPDM,@IsCF,0,0,@YearCode)";
                            SqlHelper.InsertDelUpdate(sqls, new SqlParameter("@SPDM", spdmNull), new SqlParameter("@IsCF", dr["ML_SUM"].ToString().Length > 0 ? "CMT" : "FOB"), new SqlParameter("@YearCode", dr["YearCode"]));
                        }
                        else
                        {
                            //将自己保存的值合并datatable
                            sqls = "select * from BPM_P_SPCBYSB_New_Ship where spdm='" + spdmNull + "'";
                            DataTable dts = SqlHelper.SelectTable(sqls);
                            if (dts.Rows.Count > 0)
                            {
                                dr["GC1"] = dts.Rows[0]["GC1"].ToString();
                                dr["GC2"] = dts.Rows[0]["GC2"].ToString();
                                dr["GC3"] = dts.Rows[0]["GC3"].ToString();
                                dr["GC4"] = dts.Rows[0]["GC4"].ToString();
                                dr["GCHJ"] = dts.Rows[0]["GCHJ"].ToString();
                                dr["CBQR"] = dts.Rows[0]["CBQR"].ToString();
                                dr["DDPJ"] = dts.Rows[0]["DDPJ"].ToString();
                                dr["B1"] = dts.Rows[0]["B1"].ToString();
                                dr["B2"] = dts.Rows[0]["B2"].ToString();
                                dr["PCBJ"] = dts.Rows[0]["PCBJ"].ToString();
                                dr["PDPJ"] = dts.Rows[0]["PDPJ"].ToString();
                                dr["SJName"] = dts.Rows[0]["SJName"].ToString();
                                dr["IsSJ"] = dts.Rows[0]["IsSJ"].ToString();
                                dr["BossName"] = dts.Rows[0]["BossName"].ToString();
                                dr["IsBoss"] = dts.Rows[0]["IsBoss"].ToString();
                                dr["SPName"] = dts.Rows[0]["SPName"].ToString();
                                dr["IsSP"] = dts.Rows[0]["IsSP"].ToString();
                                dr["Remark"] = dts.Rows[0]["Remark"].ToString();
                            }

                        }



                        //成本核算/参考价
                        if ((dr["ML_SUM"].ToString().Length > 0 ? "CMT" : "FOB") == "CMT")
                        {
                            decimal jiege = ((dr["Cost"].ToString() == "" ? 0.00m : Convert.ToDecimal(dr["Cost"])) * Convert.ToDecimal(1.32));
                            dr["CBHSSUM"] = jiege.ToString();
                            dr["CKJSUM"] = (jiege * Convert.ToDecimal(5.8)).ToString();
                        }
                        else
                        {
                            decimal jiege = ((dr["Cost"].ToString() == "" ? 0.00m : Convert.ToDecimal(dr["Cost"])) * Convert.ToDecimal(1.1));
                            dr["CBHSSUM"] = jiege.ToString();
                            dr["CKJSUM"] = (jiege * Convert.ToDecimal(5.8)).ToString();
                        }
                    }
                    else
                    {
                        dr["SPDM"] = spdmNull;
                    }

                    dr["PICurl"] = "";
                    if (dr["PIC"].ToString().Length > 0)
                    {
                        byte[] photo = new byte[0];

                        photo = (byte[])dr["PIC"];//读取第一个图片的位流
                        string path = Server.MapPath("~/Upload").TrimEnd('\\') + @"\";//转为物理路径
                        FileStream fs = new FileStream(path + dr["MasterID"].ToString() + ".jpg", System.IO.FileMode.Create);
                        BinaryWriter bw = new BinaryWriter(fs);
                        bw.Write(photo, 0, photo.Length);
                        fs.Flush();//数据写入图片文件
                        fs.Close();
                        dr["PICurl"] = "/Upload/" + dr["MasterID"].ToString() + ".jpg";
                    }
                }
                return View(dt);
            }
            catch (Exception)
            {

                return View();
            }


        }
        #endregion

        #region 下拉框
        /// <summary>
        /// 绑定款类下拉框
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDDLStyle()
        {
            string sql = "";
            sql = "select SXMC from FJSX3 where len(SXDM) > 1";
            DataTable dt = SqlHelper.SelectTable(sql);
            return Content(JsonConvert.SerializeObject(dt));
        }

        /// <summary>
        /// 绑定波段下拉框--Fjsx2(波段表)
        /// </summary>
        /// <returns></returns>
        public ActionResult GetFjsx2()
        {
            string sql = "";
            sql = "select SXMC from fjsx2 where len(SXDM) > 1 and SXDM>500 or SXDM=000 ";
            DataTable dt = SqlHelper.SelectTable(sql);
            return Content(JsonConvert.SerializeObject(dt));
        }
        #endregion

        #region Execl导入
        public int GetSPDMInfo(string spdm)
        {
            //根据商品代码查询数据库是否有值
            string sql = "select Count(1) from BPM_P_SPCBYSB_New_Ship where SPDM='" + spdm + "'";
            return (int)SqlHelper.SelectSinger(sql);
        }
        //生产导入
        public ActionResult SetSC(HttpPostedFileBase filed1)
        {
            //Common.SaveExcelFile(filed);
            string filePath = SaveAsLoed(filed1);
            string sql = null;
            try
            {
                DataTable dt = Common.ExcelToDataTable(filePath, true, 0);
                foreach (DataRow dr in dt.Rows)
                {
                    if (GetSPDMInfo(dr["商品代码"].ToString()) == 0)
                    {
                        sql = "INSERT INTO BPM_P_SPCBYSB_New_Ship(SPDM,GC1,GC2,GC3,GC4,GCHJ) VALUES(@SPDM,@GC1,@GC2,@GC3,@GC4,@GCHJ)";
                    }
                    else
                    {
                        sql = "update BPM_P_SPCBYSB_New_Ship set GC1=@GC1,GC2=@GC2,GC3=@GC3,GC4=@GC4,GCHJ=@GCHJ where spdm='" + dr["商品代码"] + "'";
                    }
                    SqlHelper.InsertDelUpdate(sql,
                        new SqlParameter("@SPDM", dr["商品代码"]),
                        new SqlParameter("@GC1", dr["工厂1"].ToString() == "" ? 0 : dr["工厂1"]),
                        new SqlParameter("@GC2", dr["工厂2"].ToString() == "" ? 0 : dr["工厂2"]),
                        new SqlParameter("@GC3", dr["工厂3"].ToString() == "" ? 0 : dr["工厂3"]),
                        new SqlParameter("@GC4", dr["工厂4"].ToString() == "" ? 0 : dr["工厂4"]),
                        new SqlParameter("@GCHJ", dr["工厂核价"].ToString() == "" ? 0 : dr["工厂核价"]));
                }
            }
            catch (Exception ex)
            {
                return Content("上传失败请检查！");
            }
            return Content("上传成功");
        }
        //商品导入
        public ActionResult SetSP(HttpPostedFileBase filed2)
        {
            string filePath = SaveAsLoed(filed2);
            string sql = null;
            try
            {
                DataTable dt = Common.ExcelToDataTable(filePath, true, 0);
                foreach (DataRow dr in dt.Rows)
                {
                    if (GetSPDMInfo(dr["商品代码"].ToString()) == 0)
                    {
                        sql = "INSERT INTO BPM_P_SPCBYSB_New_Ship(SPDM,CBQR,DDPJ,B1,B2,PCBJ,PDPJ,Remark) VALUES(@SPDM,@CBQR,@DDPJ,@B1,@B2,@PCBJ,@PDPJ,@Remark)";
                    }
                    else
                    {
                        sql = "update BPM_P_SPCBYSB_New_Ship set CBQR=@CBQR,DDPJ=@DDPJ,B1=@B1,B2=@B2,PCBJ=@PCBJ,PDPJ=@PDPJ,Remark=@Remark where spdm=@SPDM";
                    }
                    SqlHelper.InsertDelUpdate(sql,
                        new SqlParameter("@SPDM", dr["商品代码"]),
                        new SqlParameter("@CBQR", dr["确认成本价"].ToString() == "" ? 0 : dr["确认成本价"]),
                        new SqlParameter("@DDPJ", dr["定吊牌价"].ToString() == "" ? 0 : dr["定吊牌价"]),
                        new SqlParameter("@B1", dr["倍率1"].ToString() == "" ? 0 : dr["倍率1"]),
                        new SqlParameter("@B2", dr["倍率2"].ToString() == "" ? 0 : dr["倍率2"]),
                        new SqlParameter("@PCBJ", dr["确认成本价"].ToString() == "" ? 0 : dr["确认成本价"]),
                        new SqlParameter("@PDPJ", dr["定吊牌价"].ToString() == "" ? 0 : dr["定吊牌价"]),
                        new SqlParameter("@Remark", dr["备注"]));
                }
            }
            catch (Exception ex)
            {
                return Content("上传失败请检查！");
            }
            return Content("上传成功");
        }
        //文件
        public string SaveAsLoed(HttpPostedFileBase file)
        {
            var fileName = file.FileName;
            var filePath = Server.MapPath(string.Format("~/{0}", "Upload"));
            file.SaveAs(Path.Combine(filePath, fileName));
            return filePath + "/" + fileName;
        }
        #endregion
    }
}