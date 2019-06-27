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
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SCWeb.Controllers
{
    public class BomController : Controller
    {
        /// <summary>
        /// BOM加载
        /// </summary>
        /// <returns></returns>
        public ActionResult SplBomIndex()
        {
            return View();
        }





        /// <summary>
        /// 大货BOM列表
        /// </summary>
        /// <returns></returns>
        public ActionResult BigBomIndex()
        {
            return View();
        }

        /// <summary>
        /// 大货BOM列表
        /// </summary>
        /// <returns></returns>
        public ActionResult BigBomAddUpdate(string id)
        {
            string sql = "select * from BPM_CargoList where id=@id";
            DataTable dt = SqlHelper.SelectTable(sql, new SqlParameter("@id", id));
            if (dt.Rows.Count > 0)
            {
                #region 用Viewbag装数据前台调用
                ViewBag.Sample01 = string.IsNullOrWhiteSpace(dt.Rows[0]["Sample01"].ToString()) == true ? "" : dt.Rows[0]["Sample01"].ToString();
                ViewBag.Sample02 = string.IsNullOrWhiteSpace(dt.Rows[0]["Sample02"].ToString()) == true ? "" : dt.Rows[0]["Sample02"].ToString();
                ViewBag.Sample03 = string.IsNullOrWhiteSpace(dt.Rows[0]["Sample03"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["Sample03"]).ToString("yyyy");
                ViewBag.Sample04 = string.IsNullOrWhiteSpace(dt.Rows[0]["Sample04"].ToString()) == true ? "" : dt.Rows[0]["Sample04"].ToString();
                ViewBag.Sample05 = string.IsNullOrWhiteSpace(dt.Rows[0]["Sample05"].ToString()) == true ? "" : dt.Rows[0]["Sample05"].ToString();
                ViewBag.Sample06 = string.IsNullOrWhiteSpace(dt.Rows[0]["Sample06"].ToString()) == true ? "" : dt.Rows[0]["Sample06"].ToString();
                ViewBag.Sample07 = string.IsNullOrWhiteSpace(dt.Rows[0]["Sample07"].ToString()) == true ? "" : dt.Rows[0]["Sample07"].ToString();
                ViewBag.Sample08 = string.IsNullOrWhiteSpace(dt.Rows[0]["Sample08"].ToString()) == true ? "" : dt.Rows[0]["Sample08"].ToString();
                ViewBag.Sample09 = string.IsNullOrWhiteSpace(dt.Rows[0]["Sample09"].ToString()) == true ? "" : dt.Rows[0]["Sample09"].ToString();
                ViewBag.Sample10 = string.IsNullOrWhiteSpace(dt.Rows[0]["Sample10"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["Sample10"]).ToString("yyyy-MM-dd");
                ViewBag.Sample11 = string.IsNullOrWhiteSpace(dt.Rows[0]["Sample11"].ToString()) == true ? "" : dt.Rows[0]["Sample11"].ToString();
                ViewBag.Sample12 = string.IsNullOrWhiteSpace(dt.Rows[0]["Sample12"].ToString()) == true ? "0" : dt.Rows[0]["Sample12"].ToString();
                ViewBag.Sample16 = string.IsNullOrWhiteSpace(dt.Rows[0]["Sample16"].ToString()) == true ? "" : dt.Rows[0]["Sample16"].ToString();
                ViewBag.Sample17 = string.IsNullOrWhiteSpace(dt.Rows[0]["Sample17"].ToString()) == true ? "" : dt.Rows[0]["Sample17"].ToString();
                ViewBag.Sample18 = string.IsNullOrWhiteSpace(dt.Rows[0]["Sample18"].ToString()) == true ? "" : dt.Rows[0]["Sample18"].ToString();
                ViewBag.Sample19 = string.IsNullOrWhiteSpace(dt.Rows[0]["Sample19"].ToString()) == true ? "" : dt.Rows[0]["Sample19"].ToString();
                ViewBag.Sample20 = string.IsNullOrWhiteSpace(dt.Rows[0]["Sample20"].ToString()) == true ? "" : dt.Rows[0]["Sample20"].ToString();
                ViewBag.Sample21 = string.IsNullOrWhiteSpace(dt.Rows[0]["Sample21"].ToString()) == true ? "" : dt.Rows[0]["Sample21"].ToString();
                ViewBag.Sample22 = string.IsNullOrWhiteSpace(dt.Rows[0]["Sample22"].ToString()) == true ? "" : dt.Rows[0]["Sample22"].ToString();
                #endregion
            }
            else
            {
                ViewBag.Sample01 = "";
                ViewBag.Sample02 = "";
                ViewBag.Sample03 = "";
                ViewBag.Sample04 = "";
                ViewBag.Sample05 = "";
                ViewBag.Sample06 = "";
                ViewBag.Sample07 = "";
                ViewBag.Sample08 = "";
                ViewBag.Sample09 = "";
                ViewBag.Sample10 = "";
                ViewBag.Sample11 = "";
                ViewBag.Sample12 = "0";
                ViewBag.Sample16 = "";
                ViewBag.Sample17 = "";
                ViewBag.Sample18 = "";
                ViewBag.Sample19 = "";
                ViewBag.Sample20 = "";
                ViewBag.Sample21 = "";
                ViewBag.Sample22 = "";
            }
            ViewBag.id = id;
            return View();
        }

        /// <summary>
        /// 倍率监控
        /// </summary>
        /// <returns></returns>
        public ActionResult MrmIndex()
        {
            return View();
        }


        /// <summary>
        /// bom编辑前绑定数据
        /// </summary>
        /// <returns></returns>
        public ActionResult SplBomyAddUpdate(string id)
        {
            string sql = "select * from BPM_SampleList where id=@id";
            DataTable dt = SqlHelper.SelectTable(sql, new SqlParameter("@id", id));
            if (dt.Rows.Count > 0)
            {
                #region 用Viewbag装数据前台调用
                ViewBag.Sample01 = string.IsNullOrWhiteSpace(dt.Rows[0]["Sample01"].ToString()) == true ? "" : dt.Rows[0]["Sample01"].ToString();
                ViewBag.Sample02 = string.IsNullOrWhiteSpace(dt.Rows[0]["Sample02"].ToString()) == true ? "" : dt.Rows[0]["Sample02"].ToString();
                ViewBag.Sample03 = string.IsNullOrWhiteSpace(dt.Rows[0]["Sample03"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["Sample03"]).ToString("yyyy");
                ViewBag.Sample04 = string.IsNullOrWhiteSpace(dt.Rows[0]["Sample04"].ToString()) == true ? "" : dt.Rows[0]["Sample04"].ToString();
                ViewBag.Sample05 = string.IsNullOrWhiteSpace(dt.Rows[0]["Sample05"].ToString()) == true ? "" : dt.Rows[0]["Sample05"].ToString();
                ViewBag.Sample06 = string.IsNullOrWhiteSpace(dt.Rows[0]["Sample06"].ToString()) == true ? "" : dt.Rows[0]["Sample06"].ToString();
                ViewBag.Sample07 = string.IsNullOrWhiteSpace(dt.Rows[0]["Sample07"].ToString()) == true ? "" : dt.Rows[0]["Sample07"].ToString();
                ViewBag.Sample08 = string.IsNullOrWhiteSpace(dt.Rows[0]["Sample08"].ToString()) == true ? "" : dt.Rows[0]["Sample08"].ToString();
                ViewBag.Sample09 = string.IsNullOrWhiteSpace(dt.Rows[0]["Sample09"].ToString()) == true ? "" : dt.Rows[0]["Sample09"].ToString();
                ViewBag.Sample10 = string.IsNullOrWhiteSpace(dt.Rows[0]["Sample10"].ToString()) == true ? "" : Convert.ToDateTime(dt.Rows[0]["Sample10"]).ToString("yyyy-MM-dd");
                ViewBag.Sample11 = string.IsNullOrWhiteSpace(dt.Rows[0]["Sample11"].ToString()) == true ? "" : dt.Rows[0]["Sample11"].ToString();
                ViewBag.Sample12 = string.IsNullOrWhiteSpace(dt.Rows[0]["Sample12"].ToString()) == true ? "0" : dt.Rows[0]["Sample12"].ToString();
                ViewBag.Sample16 = string.IsNullOrWhiteSpace(dt.Rows[0]["Sample16"].ToString()) == true ? "" : dt.Rows[0]["Sample16"].ToString();
                ViewBag.Sample17 = string.IsNullOrWhiteSpace(dt.Rows[0]["Sample17"].ToString()) == true ? "" : dt.Rows[0]["Sample17"].ToString();
                ViewBag.Sample18 = string.IsNullOrWhiteSpace(dt.Rows[0]["Sample18"].ToString()) == true ? "" : dt.Rows[0]["Sample18"].ToString();
                ViewBag.Sample19 = string.IsNullOrWhiteSpace(dt.Rows[0]["Sample19"].ToString()) == true ? "" : dt.Rows[0]["Sample19"].ToString();
                ViewBag.Sample20 = string.IsNullOrWhiteSpace(dt.Rows[0]["Sample20"].ToString()) == true ? "" : dt.Rows[0]["Sample20"].ToString();
                ViewBag.Sample21 = string.IsNullOrWhiteSpace(dt.Rows[0]["Sample21"].ToString()) == true ? "" : dt.Rows[0]["Sample21"].ToString();
                ViewBag.Sample22 = string.IsNullOrWhiteSpace(dt.Rows[0]["Sample22"].ToString()) == true ? "" : dt.Rows[0]["Sample22"].ToString();
                #endregion
            }
            else
            {
                ViewBag.Sample01 = "";
                ViewBag.Sample02 = "";
                ViewBag.Sample03 = "";
                ViewBag.Sample04 = "";
                ViewBag.Sample05 = "";
                ViewBag.Sample06 = "";
                ViewBag.Sample07 = "";
                ViewBag.Sample08 = "";
                ViewBag.Sample09 = "";
                ViewBag.Sample10 = "";
                ViewBag.Sample11 = "";
                ViewBag.Sample12 = "0";
                ViewBag.Sample16 = "";
                ViewBag.Sample17 = "";
                ViewBag.Sample18 = "";
                ViewBag.Sample19 = "";
                ViewBag.Sample21 = "";
                ViewBag.Sample22 = "";
            }
            ViewBag.id = id;
            return View();
        }

        /// <summary>
        /// Bom的table数据绑定以及模糊查询
        /// </summary>
        /// <param name="page"></param>
        /// <param name="ShowPageCount"></param>
        /// <returns></returns>
        public ActionResult GetBomIndex(string page, string ShowPageCount, string where)
        {
            int RowsCount = 0;//总行数
            int pageCount = 0;//总页数

            var sqlAdd = "status =1";
            if (string.IsNullOrWhiteSpace(where) != true)
            {
                //var str = "";
                var Arr = where.Split(',');
                for (int i = 0; i < Arr.Length; i++)
                {
                    var str1 = Arr[i].Split('|');
                    if (str1[0] == "Sample10")
                    {
                        sqlAdd += " and " + str1[0] + " = '" + str1[1] + "'";
                    }
                    else
                    {
                        sqlAdd += " and " + str1[0] + " like  '%" + str1[1] + "%'";
                    }
                }
                //else//只有一个查询条件时
                //{
                //    var str = where.Split('|');
                //    sqlAdd += " and " + str[0] + " like'%" + str[1] + "%'";
                //}

            }

            //修改时间格式
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy'-'MM'-'dd" };

            try
            {
                DataTable dt = Common.GetSQLProcList(out RowsCount, out pageCount, "BPM_SampleList", "*", "id", ShowPageCount, page, " id desc ", sqlAdd.ToString());
                List<BPM_SampleModels> SampleList = new List<BPM_SampleModels>();
                foreach (DataRow item in dt.Rows)
                {
                    BPM_SampleModels model = new BPM_SampleModels()
                    {
                        id = item["id"].ToString(),
                        Sample01 = item["Sample01"].ToString(),
                        Sample02 = item["Sample02"].ToString(),
                        Sample03 = string.IsNullOrWhiteSpace(item["Sample03"].ToString()) == true ? "" : Convert.ToDateTime(item["Sample03"]).ToString("yyyy"),
                        Sample04 = item["Sample04"].ToString(),
                        Sample05 = item["Sample05"].ToString(),
                        Sample06 = item["Sample06"].ToString(),
                        Sample07 = item["Sample07"].ToString(),
                        Sample08 = item["Sample08"].ToString(),
                        Sample09 = item["Sample09"].ToString(),
                        Sample10 = string.IsNullOrWhiteSpace(item["Sample10"].ToString()) == true ? "" : Convert.ToDateTime(item["Sample10"]).ToString("yyyy-MM-dd"),
                        Sample11 = item["Sample11"].ToString(),
                        Sample12 = item["Sample12"].ToString() == "0" ? "待审" : item["Sample12"].ToString() == "1" ? "审核" : item["Sample12"].ToString() == "2" ? "作废" : "生成大货BOM",
                        Sample13 = item["Sample13"].ToString(),
                        Sample16 = string.IsNullOrWhiteSpace(item["Sample16"].ToString()) == true ? "" : item["Sample16"].ToString(),
                        Sample22 = string.IsNullOrWhiteSpace(item["Sample22"].ToString()) == true ? "" : item["Sample22"].ToString(),

                        pageCount = pageCount.ToString(),
                        RowsCount = RowsCount.ToString(),
                    };
                    SampleList.Add(model);
                }
                return Content(JsonConvert.SerializeObject(SampleList));
            }
            catch (Exception ex)
            {
                return Content("error");
            }

        }

        /// <summary>
        /// 样衣Bom明细表查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetBomMXIndex(string id)
        {
            try
            {
                string sql = "select * from BPM_SampleMX where Sampleid=@id and status=1 order by charindex(SampleMX01,'面料配料里料一般辅料包装辅料工艺工序')";
                DataTable dt = SqlHelper.SelectTable(sql, new SqlParameter("@id", id));
                List<BPM_SampleMXModels> SampleMXlist = new List<BPM_SampleMXModels>();
                #region 循环查询数据
                foreach (DataRow item in dt.Rows)
                {
                    BPM_SampleMXModels model = new BPM_SampleMXModels()
                    {
                        id = item["id"].ToString(),
                        Sampleid = item["Sampleid"].ToString(),
                        SampleMX01 = item["SampleMX01"].ToString(),
                        SampleMX02 = item["SampleMX02"].ToString(),
                        SampleMX03 = item["SampleMX03"].ToString(),
                        SampleMX04 = item["SampleMX04"].ToString(),
                        SampleMX05 = item["SampleMX05"].ToString(),
                        SampleMX06 = item["SampleMX06"].ToString(),
                        SampleMX07 = item["SampleMX07"].ToString(),
                        SampleMX08 = item["SampleMX08"].ToString(),
                        SampleMX09 = item["SampleMX09"].ToString(),
                        SampleMX10 = item["SampleMX10"].ToString(),
                        SampleMX11 = item["SampleMX11"].ToString(),
                        SampleMX12 = item["SampleMX12"].ToString(),
                        SampleMX13 = item["SampleMX13"].ToString(),
                        SampleMX14 = string.IsNullOrWhiteSpace(item["SampleMX14"].ToString()) == true ? 0 : Convert.ToDecimal(item["SampleMX14"]),
                        SampleMX15 = string.IsNullOrWhiteSpace(item["SampleMX15"].ToString()) == true ? 0 : Convert.ToDecimal(item["SampleMX15"]),
                        SampleMX16 = string.IsNullOrWhiteSpace(item["SampleMX16"].ToString()) == true ? 0 : Convert.ToDecimal(item["SampleMX16"]),
                        SampleMX17 = item["SampleMX17"].ToString(),
                        SampleMX18 = item["SampleMX18"].ToString(),
                        SampleMX19 = item["SampleMX19"].ToString(),

                    };
                    SampleMXlist.Add(model);
                }
                #endregion

                return Content(JsonConvert.SerializeObject(SampleMXlist));
            }
            catch (Exception ex)
            {
                return Content("error");
            }

        }

        /// <summary>
        /// 样衣Bom添加和修改的方法
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public ActionResult AddUpdateBom(BPM_SampleModels models)
        {
            try
            {
                var Arr = Request["MXArr"];  //明细数据
                var MXids = Request["MXid"];    //样衣Bom明细id组(用于删除)
                var MXid = MXids.Split(',');   //样衣Bom明细id
                int MXnum = int.Parse(Request["MXnum"]);  //数据条数
                var MxArr = Arr.Split('|');

                int count = 0;
                if (models.id == "0")//进行添加操作
                {

                    string sql = "insert into BPM_SampleList(Sample01,Sample02,Sample03,Sample04,Sample05,Sample06,Sample07,Sample08,Sample09,Sample10,Sample11,Sample12,Sample16,Sample17,Sample18,Sample19,Sample20,Sample21,Sample22) values(@Sample01,@Sample02,@Sample03,@Sample04,@Sample05,@Sample06,@Sample07,@Sample08,@Sample09,@Sample10,@Sample11,@Sample12,@Sample16,@Sample17,@Sample18,@Sample19,@Sample20,@Sample21,@Sample22);select @topid=@@identity";//select @topid=@@identity找出新插入一行的ID
                    SqlParameter[] param = new SqlParameter[]
                    {
                        #region 参数赋值
                        new SqlParameter("@topid",SqlDbType.Int),//获取最新添加的ID
                        new SqlParameter("@Sample01",GetSample01("0",GetStartNum("0"))),
                        new SqlParameter("@Sample02",models.Sample02),
                        new SqlParameter("@Sample03",string.IsNullOrWhiteSpace(models.Sample03) == true ? "" : models.Sample03),
                        new SqlParameter("@Sample04",models.Sample04),
                        new SqlParameter("@Sample05",models.Sample05),
                        new SqlParameter("@Sample06",models.Sample06),
                        new SqlParameter("@Sample07",models.Sample07),
                        new SqlParameter("@Sample08",models.Sample08),
                        new SqlParameter("@Sample09",models.Sample09),
                        new SqlParameter("@Sample10",string.IsNullOrWhiteSpace(models.Sample10) == true ? "" : Convert.ToDateTime(models.Sample10).ToString("yyyy-MM-dd")),
                        new SqlParameter("@Sample11",models.Sample11),
                        new SqlParameter("@Sample12",string.IsNullOrWhiteSpace(models.Sample12) == true ? "0" : models.Sample12),
                        new SqlParameter("@Sample16",string.IsNullOrWhiteSpace(models.Sample16) == true ? "/skins/img/photo_icon.png" : models.Sample16),
                        new SqlParameter("@Sample17",models.Sample17),
                        new SqlParameter("@Sample18",models.Sample18),
                        new SqlParameter("@Sample19",models.Sample19),
                        new SqlParameter("@Sample20",models.Sample20),
                        new SqlParameter("@Sample21",models.Sample21),
                        new SqlParameter("@Sample22",models.Sample22),
	                    #endregion
                    };
                    param[0].Direction = ParameterDirection.Output;
                    count = SqlHelper.InsertDelUpdate(sql, param);
                    int rID = Convert.ToInt32(param[0].Value == null ? 0 : Convert.ToInt32(param[0].Value));
                    if (count > 0)
                    {
                        string sqlMX = "insert into BPM_SampleMX(Sampleid,SampleMX01,SampleMX02,SampleMX03,SampleMX04,SampleMX05,SampleMX06,SampleMX07,SampleMX08,SampleMX09,SampleMX10,SampleMX11,SampleMX12,SampleMX13,SampleMX14,SampleMX15,SampleMX16,SampleMX17,SampleMX18,SampleMX19) values(@Sampleid,@SampleMX01,@SampleMX02,@SampleMX03,@SampleMX04,@SampleMX05,@SampleMX06,@SampleMX07,@SampleMX08,@SampleMX09,@SampleMX10,@SampleMX11,@SampleMX12,@SampleMX13,@SampleMX14,@SampleMX15,@SampleMX16,@SampleMX17,@SampleMX18,@SampleMX19)";
                        for (int i = 0; i < MXnum; i++)
                        {
                            var mxInfo = MxArr[i].Split(',');
                            SqlParameter[] paramMX = new SqlParameter[]
                            {
                                #region 参数赋值
                                new SqlParameter("@Sampleid",rID),
                                new SqlParameter("@SampleMX01",string.IsNullOrWhiteSpace(mxInfo[0]) ? "" : mxInfo[0]),
                                new SqlParameter("@SampleMX02",string.IsNullOrWhiteSpace(mxInfo[1]) ? "" : mxInfo[1]),
                                new SqlParameter("@SampleMX03",string.IsNullOrWhiteSpace(mxInfo[2]) ? "" : mxInfo[2]),
                                new SqlParameter("@SampleMX04",string.IsNullOrWhiteSpace(mxInfo[3]) ? "" : mxInfo[3]),
                                new SqlParameter("@SampleMX05",string.IsNullOrWhiteSpace(mxInfo[4]) ? "" : mxInfo[4]),
                                new SqlParameter("@SampleMX06",string.IsNullOrWhiteSpace(mxInfo[5]) ? "" : mxInfo[5]),
                                new SqlParameter("@SampleMX07",string.IsNullOrWhiteSpace(mxInfo[6]) ? "" : mxInfo[6]),
                                new SqlParameter("@SampleMX08",string.IsNullOrWhiteSpace(mxInfo[7]) ? "" : mxInfo[7]),
                                new SqlParameter("@SampleMX09",string.IsNullOrWhiteSpace(mxInfo[8]) ? "" : mxInfo[8]),
                                new SqlParameter("@SampleMX10",string.IsNullOrWhiteSpace(mxInfo[9]) ? "" : mxInfo[9]),
                                new SqlParameter("@SampleMX11",string.IsNullOrWhiteSpace(mxInfo[10]) ? "" : mxInfo[10]),
                                new SqlParameter("@SampleMX12",string.IsNullOrWhiteSpace(mxInfo[11]) ? "" : mxInfo[11]),
                                new SqlParameter("@SampleMX13",string.IsNullOrWhiteSpace(mxInfo[12]) ? "" : mxInfo[12]),
                                new SqlParameter("@SampleMX14",string.IsNullOrWhiteSpace(mxInfo[13]) ? "" : mxInfo[13]),
                                new SqlParameter("@SampleMX15",string.IsNullOrWhiteSpace(mxInfo[14]) ? "" : mxInfo[14]),
                                new SqlParameter("@SampleMX16",string.IsNullOrWhiteSpace(mxInfo[15]) ? "" : mxInfo[15]),
                                new SqlParameter("@SampleMX17",string.IsNullOrWhiteSpace(mxInfo[16]) ? "" : mxInfo[16]),
                                new SqlParameter("@SampleMX18",string.IsNullOrWhiteSpace(mxInfo[17]) ? "" : mxInfo[17]),
                                new SqlParameter("@SampleMX19",string.IsNullOrWhiteSpace(mxInfo[18]) ? "" : mxInfo[18]),
	                            #endregion
                            };
                            if (mxInfo[0] != "undefined")
                            {
                                count = SqlHelper.InsertDelUpdate(sqlMX, paramMX);
                            }
                        }
                    }
                }
                else//进行编辑操作
                {
                    string sql = "update BPM_SampleList set Sample02=@Sample02,Sample03=@Sample03,Sample04=@Sample04,Sample05=@Sample05,Sample06=@Sample06,Sample07=@Sample07,Sample08=@Sample08,Sample09=@Sample09,Sample10=@Sample10,Sample11=@Sample11,Sample12=@Sample12,Sample16=@Sample16,Sample17=@Sample17,Sample18=@Sample18,Sample19=@Sample19,Sample20=@Sample20,Sample21=@Sample21,Sample22=@Sample22 where id=@id";
                    SqlParameter[] param = new SqlParameter[]
                    {
                        #region 参数赋值
                        new SqlParameter("@Sample02",models.Sample02),
                        new SqlParameter("@Sample03",string.IsNullOrWhiteSpace(models.Sample03) == true ? "" : models.Sample03),
                        new SqlParameter("@Sample04",models.Sample04),
                        new SqlParameter("@Sample05",models.Sample05),
                        new SqlParameter("@Sample06",models.Sample06),
                        new SqlParameter("@Sample07",models.Sample07),
                        new SqlParameter("@Sample08",models.Sample08),
                        new SqlParameter("@Sample09",models.Sample09),
                        new SqlParameter("@Sample10",string.IsNullOrWhiteSpace(models.Sample10) == true ? "" : Convert.ToDateTime(models.Sample10).ToString("yyyy-MM-dd")),
                        new SqlParameter("@Sample11",models.Sample11),
                        new SqlParameter("@Sample12",string.IsNullOrWhiteSpace(models.Sample12) == true ? "0" : models.Sample12),
                        new SqlParameter("@Sample16",string.IsNullOrWhiteSpace(models.Sample16) == true ? "/skins/img/photo_icon.png" : models.Sample16),
                        new SqlParameter("@Sample17",models.Sample17),
                        new SqlParameter("@Sample18",models.Sample18),
                        new SqlParameter("@Sample19",models.Sample19),
                        new SqlParameter("@Sample20",models.Sample20),
                        new SqlParameter("@Sample21",models.Sample21),
                        new SqlParameter("@Sample22",models.Sample22),
                        new SqlParameter("@id",models.id),
	                   #endregion
                        
                    };
                    count = SqlHelper.InsertDelUpdate(sql, param);
                    if (count > 0)
                    {
                        #region 删除操作
                        string sqlDel = "update BPM_SampleMX set status=0 where id not in(" + MXids + ") and Sampleid=" + models.id + "";
                        SqlHelper.InsertDelUpdate(sqlDel);
                        #endregion

                        for (int i = 0; i < MXnum; i++)
                        {
                            var mxInfo = MxArr[i].Split(',');
                            string sqlMX = "";
                            if (MXid[i] != "0")//进行明细编辑操作
                            {
                                sqlMX = "update BPM_SampleMX set SampleMX01=@SampleMX01,SampleMX02=@SampleMX02,SampleMX03=@SampleMX03,SampleMX04=@SampleMX04,SampleMX05=@SampleMX05,SampleMX06=@SampleMX06,SampleMX07=@SampleMX07,SampleMX08=@SampleMX08,SampleMX09=@SampleMX09,SampleMX10=@SampleMX10,SampleMX11=@SampleMX11,SampleMX12=@SampleMX12,SampleMX13=@SampleMX13,SampleMX14=@SampleMX14,SampleMX15=@SampleMX15,SampleMX16=@SampleMX16,SampleMX17=@SampleMX17,SampleMX18=@SampleMX18,SampleMX19=@SampleMX19 where id=@id";
                            }
                            else//进行明细添加操作
                            {
                                sqlMX = "insert into BPM_SampleMX(Sampleid,SampleMX01,SampleMX02,SampleMX03,SampleMX04,SampleMX05,SampleMX06,SampleMX07,SampleMX08,SampleMX09,SampleMX10,SampleMX11,SampleMX12,SampleMX13,SampleMX14,SampleMX15,SampleMX16,SampleMX17,SampleMX18,SampleMX19) values(@Sampleid,@SampleMX01,@SampleMX02,@SampleMX03,@SampleMX04,@SampleMX05,@SampleMX06,@SampleMX07,@SampleMX08,@SampleMX09,@SampleMX10,@SampleMX11,@SampleMX12,@SampleMX13,@SampleMX14,@SampleMX15,@SampleMX16,@SampleMX17,@SampleMX18,@SampleMX19)";
                            }
                            SqlParameter[] paramMX = new SqlParameter[]
                            {
                                #region 参数赋值
                                new SqlParameter("@Sampleid",models.id),//样衣Bom  id
                                new SqlParameter("@SampleMX01",string.IsNullOrWhiteSpace(mxInfo[0]) ? "" : mxInfo[0]),
                                new SqlParameter("@SampleMX02",string.IsNullOrWhiteSpace(mxInfo[1]) ? "" : mxInfo[1]),
                                new SqlParameter("@SampleMX03",string.IsNullOrWhiteSpace(mxInfo[2]) ? "" : mxInfo[2]),
                                new SqlParameter("@SampleMX04",string.IsNullOrWhiteSpace(mxInfo[3]) ? "" : mxInfo[3]),
                                new SqlParameter("@SampleMX05",string.IsNullOrWhiteSpace(mxInfo[4]) ? "" : mxInfo[4]),
                                new SqlParameter("@SampleMX06",string.IsNullOrWhiteSpace(mxInfo[5]) ? "" : mxInfo[5]),
                                new SqlParameter("@SampleMX07",string.IsNullOrWhiteSpace(mxInfo[6]) ? "" : mxInfo[6]),
                                new SqlParameter("@SampleMX08",string.IsNullOrWhiteSpace(mxInfo[7]) ? "" : mxInfo[7]),
                                new SqlParameter("@SampleMX09",string.IsNullOrWhiteSpace(mxInfo[8]) ? "" : mxInfo[8]),
                                new SqlParameter("@SampleMX10",string.IsNullOrWhiteSpace(mxInfo[9]) ? "" : mxInfo[9]),
                                new SqlParameter("@SampleMX11",string.IsNullOrWhiteSpace(mxInfo[10]) ? "" : mxInfo[10]),
                                new SqlParameter("@SampleMX12",string.IsNullOrWhiteSpace(mxInfo[11]) ? "" : mxInfo[11]),
                                new SqlParameter("@SampleMX13",string.IsNullOrWhiteSpace(mxInfo[12]) ? "" : mxInfo[12]),
                                new SqlParameter("@SampleMX14",string.IsNullOrWhiteSpace(mxInfo[13]) ? "" : mxInfo[13]),
                                new SqlParameter("@SampleMX15",string.IsNullOrWhiteSpace(mxInfo[14]) ? "" : mxInfo[14]),
                                new SqlParameter("@SampleMX16",string.IsNullOrWhiteSpace(mxInfo[15]) ? "" : mxInfo[15]),
                                new SqlParameter("@SampleMX17",string.IsNullOrWhiteSpace(mxInfo[16]) ? "" : mxInfo[16]),
                                new SqlParameter("@SampleMX18",string.IsNullOrWhiteSpace(mxInfo[17]) ? "" : mxInfo[17]),
                                new SqlParameter("@SampleMX19",string.IsNullOrWhiteSpace(mxInfo[18]) ? "" : mxInfo[18]),
                                new SqlParameter("@id",MXid[i]) //明细ID
	                            #endregion
                            };
                            if (mxInfo[0] != "undefined")
                            {
                                count = SqlHelper.InsertDelUpdate(sqlMX, paramMX);
                            }
                        }

                    }
                }
                if (models.Sample12 == "3") //生成大货bom
                {
                    makeCargoBom(0, models.id);
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
        /// 删除样衣Bom的方法
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DelBomTable(string id)
        {
            string sql = "update BPM_SampleList set status=0 where id in (" + id + ")";
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
        /// 批量修改样衣Bom状态
        /// </summary>
        /// <returns></returns>
        public ActionResult BatchEditState()
        {
            try
            {
                var data = Request["data"];     //要修改的状态
                var id = Request["id"];         //修改的列名
                var ckval = Request["ckval"].Substring(0, Request["ckval"].Length - 1);   //要修改的数据id

                #region 生成大货Bom
                if (data == "3")//生成大货Bom
                {
                    int DHcount = 0;
                    var aBack = GetAddBS_BUS_Sample(ckval);
                    if (aBack == "CG" || aBack == "未执行")
                    {
                        DHcount = makeCargoBom(DHcount, ckval);//生成大货bom的方法
                    }
                    else if (aBack == "null")
                    {
                        return Content("null");
                    }
                    else
                    {
                        return Content("error");
                    }

                    if (DHcount > 0)
                    {
                        #region 修改样衣Bom状态
                        string sqlDh = "update BPM_SampleList set " + id + " = @data where id in (" + ckval + ")";
                        SqlHelper.InsertDelUpdate(sqlDh, new SqlParameter("@data", data));
                        #endregion
                        return Content("yes");
                    }
                }
                #endregion

                #region 修改样衣Bom状态
                string sql = "update BPM_SampleList set " + id + " = @data where id in (" + ckval + ")";
                int count = SqlHelper.InsertDelUpdate(sql, new SqlParameter("@data", data));
                #endregion

                if (count > 0)
                {
                    return Content("success");
                }
                else
                {
                    return Content("error");
                }
            }
            catch (Exception ex)
            {
                return Content("error");
                throw;
            }

        }

        /// <summary>
        /// 获取单号
        /// </summary>
        /// <param name="obj">样衣Bom(0)/大货Bom(1)</param>
        /// <returns></returns>
        public string GetSample01(string obj, string str)
        {
            var Sample01 = "";
            var DocumentNum = "";
            if (obj == "0")
            {
                DocumentNum = "BOMQDBPM" + "YY" + DateTime.Now.ToString("yyyyMMdd");
            }
            else if (obj == "1")
            {
                DocumentNum = "BOMQDBPM" + "DH" + DateTime.Now.ToString("yyyyMMdd");
            }

            if (str == "" || str.Substring(10, 8) != DateTime.Now.ToString("yyyyMMdd"))
            {
                Sample01 = DocumentNum + "0001";
            }
            else
            {
                int num = Convert.ToInt32(str.Substring(str.Length - 4, 4));
                num++;
                if (num.ToString().Length == 1)
                {
                    Sample01 = DocumentNum + "000" + num;
                }
                else if (num.ToString().Length == 2)
                {
                    Sample01 = DocumentNum + "00" + num;
                }
                else if (num.ToString().Length == 3)
                {
                    Sample01 = DocumentNum + "0" + num;
                }
                else if (num.ToString().Length == 4)
                {
                    Sample01 = DocumentNum + num;
                }
            }
            return Sample01;
        }

        /// <summary>
        /// 获取开始加的单据编号
        /// </summary>
        /// <param name="obj">样衣Bom(0)/大货Bom(1)</param>
        /// <returns></returns>
        public string GetStartNum(string obj)
        {
            string type = "";
            string tab = "";
            if (obj == "0")
            {
                type = "YY";
                tab = "BPM_SampleList";
            }
            else if (obj == "1")
            {
                type = "DH";
                tab = "BPM_CargoList";
            }
            string sql = "select ISNULL(MAX(Sample01),'') from " + tab + " where Sample01 like'%" + type + "%'";
            string str = SqlHelper.SelectSinger(sql).ToString();
            return string.IsNullOrWhiteSpace(str) ? "" : str;
        }

        /// <summary>
        /// 生成大货bom的方法
        /// </summary>
        /// <param name="DHcount">返回值 大于0 则生成成功</param>
        /// <param name="ckval">要修改的数据id</param>
        /// <returns></returns>
        public int makeCargoBom(int DHcount, string ckval)
        {
            string sqlDH = "select * from BPM_SampleList where id in(" + ckval + ") and status=1";      //生成大货Bom  SQL
            string sqlDHmx = "select * from BPM_SampleMX where Sampleid in(" + ckval + ") and status=1";   //生成大货Bom明细  SQL
            DataTable dt = SqlHelper.SelectTable(sqlDH);
            DataTable dtMX = SqlHelper.SelectTable(sqlDHmx);

            foreach (DataRow item in dt.Rows)
            {
                string inSqlDH = "insert into BPM_CargoList(Sample01,Sample02,Sample03,Sample04,Sample05,Sample06,Sample07,Sample08,Sample09,Sample10,Sample11,Sample12,Sample15,Sample16,Sample17,Sample18,Sample19,Sample20,Sample21,Sample22) values(@Sample01,@Sample02,@Sample03,@Sample04,@Sample05,@Sample06,@Sample07,@Sample08,@Sample09,@Sample10,@Sample11,@Sample12,@Sample15,@Sample16,@Sample17,@Sample18,@Sample19,@Sample20,@Sample21,@Sample22);select @topid=@@identity";//select @topid=@@identity找出新插入一行的ID
                SqlParameter[] param = new SqlParameter[]
                {
                        #region 大货Bom参数赋值
                        new SqlParameter("@topid",SqlDbType.Int),//获取最新添加的ID
                        new SqlParameter("@Sample01",GetSample01("1",GetStartNum("1"))),
                        new SqlParameter("@Sample02",item["Sample02"]),
                        new SqlParameter("@Sample03",string.IsNullOrWhiteSpace(item["Sample03"].ToString()) == true ? "" : item["Sample03"]),
                        new SqlParameter("@Sample04",item["Sample04"]),
                        new SqlParameter("@Sample05",item["Sample05"]),
                        new SqlParameter("@Sample06",item["Sample06"]),
                        new SqlParameter("@Sample07",item["Sample07"]),
                        new SqlParameter("@Sample08",item["Sample08"]),
                        new SqlParameter("@Sample09",item["Sample09"]),
                        new SqlParameter("@Sample10",string.IsNullOrWhiteSpace(item["Sample10"].ToString()) == true ? "" : Convert.ToDateTime(item["Sample10"]).ToString("yyyy-MM-dd")),
                        new SqlParameter("@Sample11",item["Sample11"]),
                        new SqlParameter("@Sample12","0"),
                        new SqlParameter("@Sample15",item["Sample01"]),
                        new SqlParameter("@Sample16",string.IsNullOrWhiteSpace(item["Sample16"].ToString()) == true ? "/skins/img/photo_icon.png" : item["Sample16"]),
                        new SqlParameter("@Sample17",item["Sample17"]),
                        new SqlParameter("@Sample18",item["Sample18"]),
                        new SqlParameter("@Sample19",item["Sample19"]),
                        new SqlParameter("@Sample20",item["Sample20"]),
                        new SqlParameter("@Sample21",item["Sample21"]),
                        new SqlParameter("@Sample22",item["Sample22"]),

                    #endregion
                };
                param[0].Direction = ParameterDirection.Output;
                DHcount = SqlHelper.InsertDelUpdate(inSqlDH, param);
                //获取大货bom新插入一行的ID
                int rID = Convert.ToInt32(param[0].Value == null ? 0 : Convert.ToInt32(param[0].Value));

                /////----------------------生成大货Bom明细----------------------//////

                if (DHcount > 0)
                {
                    foreach (DataRow itemMX in dtMX.Rows)
                    {
                        if (item["id"].ToString() == itemMX["Sampleid"].ToString())
                        {
                            string inSqlDHmx = "insert into dbo.BPM_CargoMX(Sampleid,SampleMX01,SampleMX03,SampleMX04,SampleMX05,SampleMX06,SampleMX07,SampleMX08,SampleMX09,SampleMX10,SampleMX11,SampleMX12,SampleMX13,SampleMX14,SampleMX15,SampleMX16,SampleMX17,SampleMX18,SampleMX19) values(@Sampleid,@SampleMX01,@SampleMX03,@SampleMX04,@SampleMX05,@SampleMX06,@SampleMX07,@SampleMX08,@SampleMX09,@SampleMX10,@SampleMX11,@SampleMX12,@SampleMX13,@SampleMX14,@SampleMX15,@SampleMX16,@SampleMX17,@SampleMX18,@SampleMX19)";
                            SqlParameter[] MXparam = new SqlParameter[]
                            {
                                        #region 大货Bom明细参数赋值
                                        new SqlParameter("@Sampleid",rID),
                                        new SqlParameter("@SampleMX01",string.IsNullOrWhiteSpace(itemMX["SampleMX01"].ToString()) ? "" : itemMX["SampleMX01"]),
                                        new SqlParameter("@SampleMX03",string.IsNullOrWhiteSpace(itemMX["SampleMX03"].ToString()) ? "" : itemMX["SampleMX03"]),
                                        new SqlParameter("@SampleMX04",string.IsNullOrWhiteSpace(itemMX["SampleMX04"].ToString()) ? "" : itemMX["SampleMX04"]),
                                        new SqlParameter("@SampleMX05",string.IsNullOrWhiteSpace(itemMX["SampleMX05"].ToString()) ? "" : itemMX["SampleMX05"]),
                                        new SqlParameter("@SampleMX06",string.IsNullOrWhiteSpace(itemMX["SampleMX06"].ToString()) ? "" : itemMX["SampleMX06"]),
                                        new SqlParameter("@SampleMX07",string.IsNullOrWhiteSpace(itemMX["SampleMX07"].ToString()) ? "" : itemMX["SampleMX07"]),
                                        new SqlParameter("@SampleMX08",string.IsNullOrWhiteSpace(itemMX["SampleMX08"].ToString()) ? "" : itemMX["SampleMX08"]),
                                        new SqlParameter("@SampleMX09",string.IsNullOrWhiteSpace(itemMX["SampleMX09"].ToString()) ? "" : itemMX["SampleMX09"]),
                                        new SqlParameter("@SampleMX10",string.IsNullOrWhiteSpace(itemMX["SampleMX10"].ToString()) ? "" : itemMX["SampleMX10"]),
                                        new SqlParameter("@SampleMX11",string.IsNullOrWhiteSpace(itemMX["SampleMX11"].ToString()) ? "" : itemMX["SampleMX11"]),
                                        new SqlParameter("@SampleMX12",string.IsNullOrWhiteSpace(itemMX["SampleMX12"].ToString()) ? "" : itemMX["SampleMX12"]),
                                        new SqlParameter("@SampleMX13",string.IsNullOrWhiteSpace(itemMX["SampleMX13"].ToString()) ? "" : itemMX["SampleMX13"]),
                                        new SqlParameter("@SampleMX14",string.IsNullOrWhiteSpace(itemMX["SampleMX14"].ToString()) ? "" : itemMX["SampleMX14"]),
                                        new SqlParameter("@SampleMX15",string.IsNullOrWhiteSpace(itemMX["SampleMX15"].ToString()) ? "" : itemMX["SampleMX15"]),
                                        new SqlParameter("@SampleMX16",string.IsNullOrWhiteSpace(itemMX["SampleMX16"].ToString()) ? "" : itemMX["SampleMX16"]),
                                        new SqlParameter("@SampleMX17",string.IsNullOrWhiteSpace(itemMX["SampleMX17"].ToString()) ? "" : itemMX["SampleMX17"]),
                                        new SqlParameter("@SampleMX18",string.IsNullOrWhiteSpace(itemMX["SampleMX18"].ToString()) ? "" : itemMX["SampleMX18"]),
                                        new SqlParameter("@SampleMX19",string.IsNullOrWhiteSpace(itemMX["SampleMX19"].ToString()) ? "" : itemMX["SampleMX19"]),
                                #endregion
                            };
                            DHcount = SqlHelper.InsertDelUpdate(inSqlDHmx, MXparam);
                        }
                    }
                }

            }
            return DHcount;
        }

        //-------------------------------------------------大货bom-------------------------------------------------------------//

        /// <summary>
        /// Bom的table数据绑定以及模糊查询
        /// </summary>
        /// <param name="page"></param>
        /// <param name="ShowPageCount"></param>
        /// <returns></returns>
        public ActionResult GetCargoIndex(string page, string ShowPageCount, string where)
        {
            int RowsCount = 0;//总行数
            int pageCount = 0;//总页数

            var sqlAdd = "status =1";
            if (string.IsNullOrWhiteSpace(where) != true)
            {
                //var str = "";
                var Arr = where.Split(',');
                for (int i = 0; i < Arr.Length; i++)
                {
                    var str1 = Arr[i].Split('|');
                    if (str1[0] == "Sample10")
                    {
                        sqlAdd += " and " + str1[0] + " = '" + str1[1] + "'";
                    }
                    else
                    {
                        sqlAdd += " and " + str1[0] + " like  '%" + str1[1] + "%'";
                    }
                }
                //else//只有一个查询条件时
                //{
                //    var str = where.Split('|');
                //    sqlAdd += " and " + str[0] + " like'%" + str[1] + "%'";
                //}

            }

            //修改时间格式
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy'-'MM'-'dd" };

            try
            {
                DataTable dt = Common.GetSQLProcList(out RowsCount, out pageCount, "BPM_CargoList", "*", "id", ShowPageCount, page, " id desc ", sqlAdd.ToString());
                List<BPM_CargoModels> CargoList = new List<BPM_CargoModels>();
                foreach (DataRow item in dt.Rows)
                {
                    BPM_CargoModels model = new BPM_CargoModels()
                    {
                        id = item["id"].ToString(),
                        Sample01 = item["Sample01"].ToString(),
                        Sample02 = item["Sample02"].ToString(),
                        Sample03 = string.IsNullOrWhiteSpace(item["Sample03"].ToString()) == true ? "" : Convert.ToDateTime(item["Sample03"]).ToString("yyyy"),
                        Sample04 = item["Sample04"].ToString(),
                        Sample05 = item["Sample05"].ToString(),
                        Sample06 = item["Sample06"].ToString(),
                        Sample07 = item["Sample07"].ToString(),
                        Sample08 = item["Sample08"].ToString(),
                        Sample09 = item["Sample09"].ToString(),
                        Sample10 = string.IsNullOrWhiteSpace(item["Sample10"].ToString()) == true ? "" : Convert.ToDateTime(item["Sample10"]).ToString("yyyy-MM-dd"),
                        Sample11 = item["Sample11"].ToString(),
                        Sample12 = item["Sample12"].ToString() == "0" ? "待审" : item["Sample12"].ToString() == "1" ? "审核" : item["Sample12"].ToString() == "2" ? "作废" : "生成大货BOM",
                        Sample13 = item["Sample13"].ToString(),
                        Sample15 = item["Sample15"].ToString(), //对应的样衣BOM单号
                        Sample16 = string.IsNullOrWhiteSpace(item["Sample16"].ToString()) == true ? "" : item["Sample16"].ToString(),
                        Sample22 = string.IsNullOrWhiteSpace(item["Sample22"].ToString()) == true ? "" : item["Sample22"].ToString(),

                        pageCount = pageCount.ToString(),
                        RowsCount = RowsCount.ToString(),
                    };
                    CargoList.Add(model);
                }
                return Content(JsonConvert.SerializeObject(CargoList));
            }
            catch (Exception ex)
            {
                return Content("error");
            }

        }

        /// <summary>
        /// 大货Bom明细表查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetCargoMXIndex(string id)
        {
            try
            {
                string sql = "select * from BPM_CargoMX where Sampleid=@id and status=1 order by charindex(SampleMX01,'面料配料里料一般辅料包装辅料工艺工序')";
                DataTable dt = SqlHelper.SelectTable(sql, new SqlParameter("@id", id));
                List<BPM_CargoMXModels> CargoMXlist = new List<BPM_CargoMXModels>();
                #region 循环查询数据
                foreach (DataRow item in dt.Rows)
                {
                    BPM_CargoMXModels model = new BPM_CargoMXModels()
                    {
                        id = item["id"].ToString(),
                        Sampleid = item["Sampleid"].ToString(),
                        SampleMX01 = item["SampleMX01"].ToString(),
                        //SampleMX02 = item["SampleMX02"].ToString(),
                        SampleMX03 = item["SampleMX03"].ToString(),
                        SampleMX04 = item["SampleMX04"].ToString(),
                        SampleMX05 = item["SampleMX05"].ToString(),
                        SampleMX06 = item["SampleMX06"].ToString(),
                        SampleMX07 = item["SampleMX07"].ToString(),
                        SampleMX08 = item["SampleMX08"].ToString(),
                        SampleMX09 = item["SampleMX09"].ToString(),
                        SampleMX10 = item["SampleMX10"].ToString(),
                        SampleMX11 = item["SampleMX11"].ToString(),
                        SampleMX12 = item["SampleMX12"].ToString(),
                        SampleMX13 = item["SampleMX13"].ToString(),
                        SampleMX14 = string.IsNullOrWhiteSpace(item["SampleMX14"].ToString()) == true ? 0 : Convert.ToDecimal(item["SampleMX14"]),
                        SampleMX15 = string.IsNullOrWhiteSpace(item["SampleMX15"].ToString()) == true ? 0 : Convert.ToDecimal(item["SampleMX15"]),
                        SampleMX16 = string.IsNullOrWhiteSpace(item["SampleMX16"].ToString()) == true ? 0 : Convert.ToDecimal(item["SampleMX16"]),
                        SampleMX17 = item["SampleMX17"].ToString(),
                        SampleMX18 = item["SampleMX18"].ToString(),
                        SampleMX19 = item["SampleMX19"].ToString(),

                    };
                    CargoMXlist.Add(model);
                }
                #endregion

                return Content(JsonConvert.SerializeObject(CargoMXlist));
            }
            catch (Exception ex)
            {
                return Content("error");
            }

        }

        /// <summary>
        /// 大货Bom添加和修改的方法
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public ActionResult AddUpdateCargo(BPM_CargoModels models)
        {
            try
            {
                var Arr = Request["MXArr"];  //明细数据
                var MXids = Request["MXid"];    //样衣Bom明细id组(用于删除)
                var MXid = MXids.Split(',');   //样衣Bom明细id
                int MXnum = int.Parse(Request["MXnum"]);  //数据条数
                var MxArr = Arr.Split('|');

                int count = 0;
                if (models.id == "0")//进行添加操作
                {

                    string sql = "insert into BPM_CargoList(Sample01,Sample02,Sample03,Sample04,Sample05,Sample06,Sample07,Sample08,Sample09,Sample10,Sample11,Sample12,Sample16,Sample17,Sample18,Sample19,Sample20,Sample21,Sample22) values(@Sample01,@Sample02,@Sample03,@Sample04,@Sample05,@Sample06,@Sample07,@Sample08,@Sample09,@Sample10,@Sample11,@Sample12,@Sample16,@Sample17,@Sample18,@Sample19,@Sample20,@Sample21,@Sample22);select @topid=@@identity";//select @topid=@@identity找出新插入一行的ID
                    SqlParameter[] param = new SqlParameter[]
                    {
                        #region 参数赋值
                        new SqlParameter("@topid",SqlDbType.Int),//获取最新添加的ID
                        new SqlParameter("@Sample01",GetSample01("1",GetStartNum("1"))),
                        new SqlParameter("@Sample02",models.Sample02),
                        new SqlParameter("@Sample03",string.IsNullOrWhiteSpace(models.Sample03) == true ? "" : models.Sample03),
                        new SqlParameter("@Sample04",models.Sample04),
                        new SqlParameter("@Sample05",models.Sample05),
                        new SqlParameter("@Sample06",models.Sample06),
                        new SqlParameter("@Sample07",models.Sample07),
                        new SqlParameter("@Sample08",models.Sample08),
                        new SqlParameter("@Sample09",models.Sample09),
                        new SqlParameter("@Sample10",string.IsNullOrWhiteSpace(models.Sample10) == true ? "" : Convert.ToDateTime(models.Sample10).ToString("yyyy-MM-dd")),
                        new SqlParameter("@Sample11",models.Sample11),
                        new SqlParameter("@Sample12",string.IsNullOrWhiteSpace(models.Sample12) == true ? "0" : models.Sample12),
                        new SqlParameter("@Sample16",string.IsNullOrWhiteSpace(models.Sample16) == true ? "/skins/img/photo_icon.png" : models.Sample16),
                        new SqlParameter("@Sample17",models.Sample17),
                        new SqlParameter("@Sample18",models.Sample18),
                        new SqlParameter("@Sample19",models.Sample19),
                        new SqlParameter("@Sample20",models.Sample20),
                        new SqlParameter("@Sample21",models.Sample21),
                        new SqlParameter("@Sample22",models.Sample22),
	                    #endregion
                    };
                    param[0].Direction = ParameterDirection.Output;
                    count = SqlHelper.InsertDelUpdate(sql, param);
                    int rID = Convert.ToInt32(param[0].Value == null ? 0 : Convert.ToInt32(param[0].Value));
                    if (count > 0)
                    {
                        string sqlMX = "insert into BPM_CargoMX(Sampleid,SampleMX01,SampleMX03,SampleMX04,SampleMX05,SampleMX06,SampleMX07,SampleMX08,SampleMX09,SampleMX10,SampleMX11,SampleMX12,SampleMX13,SampleMX14,SampleMX15,SampleMX16,SampleMX17,SampleMX18,SampleMX19) values(@Sampleid,@SampleMX01,@SampleMX03,@SampleMX04,@SampleMX05,@SampleMX06,@SampleMX07,@SampleMX08,@SampleMX09,@SampleMX10,@SampleMX11,@SampleMX12,@SampleMX13,@SampleMX14,@SampleMX15,@SampleMX16,@SampleMX17,@SampleMX18,@SampleMX19)";
                        for (int i = 0; i < MXnum; i++)
                        {
                            var mxInfo = MxArr[i].Split(',');
                            SqlParameter[] paramMX = new SqlParameter[]
                            {
                                #region 参数赋值
                                new SqlParameter("@Sampleid",rID),
                                new SqlParameter("@SampleMX01",string.IsNullOrWhiteSpace(mxInfo[0]) ? "" : mxInfo[0]),
                                new SqlParameter("@SampleMX03",string.IsNullOrWhiteSpace(mxInfo[1]) ? "" : mxInfo[1]),
                                new SqlParameter("@SampleMX04",string.IsNullOrWhiteSpace(mxInfo[2]) ? "" : mxInfo[2]),
                                new SqlParameter("@SampleMX05",string.IsNullOrWhiteSpace(mxInfo[3]) ? "" : mxInfo[3]),
                                new SqlParameter("@SampleMX06",string.IsNullOrWhiteSpace(mxInfo[4]) ? "" : mxInfo[4]),
                                new SqlParameter("@SampleMX07",string.IsNullOrWhiteSpace(mxInfo[5]) ? "" : mxInfo[5]),
                                new SqlParameter("@SampleMX08",string.IsNullOrWhiteSpace(mxInfo[6]) ? "" : mxInfo[6]),
                                new SqlParameter("@SampleMX09",string.IsNullOrWhiteSpace(mxInfo[7]) ? "" : mxInfo[7]),
                                new SqlParameter("@SampleMX10",string.IsNullOrWhiteSpace(mxInfo[8]) ? "" : mxInfo[8]),
                                new SqlParameter("@SampleMX11",string.IsNullOrWhiteSpace(mxInfo[9]) ? "" : mxInfo[9]),
                                new SqlParameter("@SampleMX12",string.IsNullOrWhiteSpace(mxInfo[10]) ? "" : mxInfo[10]),
                                new SqlParameter("@SampleMX13",string.IsNullOrWhiteSpace(mxInfo[11]) ? "" : mxInfo[11]),
                                new SqlParameter("@SampleMX14",string.IsNullOrWhiteSpace(mxInfo[12]) ? "" : mxInfo[12]),
                                new SqlParameter("@SampleMX15",string.IsNullOrWhiteSpace(mxInfo[13]) ? "" : mxInfo[13]),
                                new SqlParameter("@SampleMX16",string.IsNullOrWhiteSpace(mxInfo[14]) ? "" : mxInfo[14]),
                                new SqlParameter("@SampleMX17",string.IsNullOrWhiteSpace(mxInfo[15]) ? "" : mxInfo[15]),
                                new SqlParameter("@SampleMX18",string.IsNullOrWhiteSpace(mxInfo[16]) ? "" : mxInfo[16]),
                                new SqlParameter("@SampleMX19",string.IsNullOrWhiteSpace(mxInfo[17]) ? "" : mxInfo[17]),
	                            #endregion
                            };
                            if (mxInfo[0] != "undefined")
                            {
                                count = SqlHelper.InsertDelUpdate(sqlMX, paramMX);
                            }
                        }
                    }
                }
                else//进行编辑操作
                {
                    string sql = "update BPM_CargoList set Sample02=@Sample02,Sample03=@Sample03,Sample04=@Sample04,Sample05=@Sample05,Sample06=@Sample06,Sample07=@Sample07,Sample08=@Sample08,Sample09=@Sample09,Sample10=@Sample10,Sample11=@Sample11,Sample12=@Sample12,Sample16=@Sample16,Sample17=@Sample17,Sample18=@Sample18,Sample19=@Sample19,Sample20=@Sample20,Sample21=@Sample21,Sample22=@Sample22 where id=@id";
                    SqlParameter[] param = new SqlParameter[]
                    {
                        #region 参数赋值
                        new SqlParameter("@Sample02",models.Sample02),
                        new SqlParameter("@Sample03",string.IsNullOrWhiteSpace(models.Sample03) == true ? "" : models.Sample03),
                        new SqlParameter("@Sample04",models.Sample04),
                        new SqlParameter("@Sample05",models.Sample05),
                        new SqlParameter("@Sample06",models.Sample06),
                        new SqlParameter("@Sample07",models.Sample07),
                        new SqlParameter("@Sample08",models.Sample08),
                        new SqlParameter("@Sample09",models.Sample09),
                        new SqlParameter("@Sample10",string.IsNullOrWhiteSpace(models.Sample10) == true ? "" : Convert.ToDateTime(models.Sample10).ToString("yyyy-MM-dd")),
                        new SqlParameter("@Sample11",models.Sample11),
                        new SqlParameter("@Sample12",string.IsNullOrWhiteSpace(models.Sample12) == true ? "0" : models.Sample12),
                        new SqlParameter("@Sample16",string.IsNullOrWhiteSpace(models.Sample16) == true ? "/skins/img/photo_icon.png" : models.Sample16),
                        new SqlParameter("@Sample17",models.Sample17),
                        new SqlParameter("@Sample18",models.Sample18),
                        new SqlParameter("@Sample19",models.Sample19),
                        new SqlParameter("@Sample20",models.Sample20),
                        new SqlParameter("@Sample21",models.Sample21),
                        new SqlParameter("@Sample22",models.Sample22),
                        new SqlParameter("@id",models.id),
	                   #endregion
                        
                    };
                    count = SqlHelper.InsertDelUpdate(sql, param);
                    if (count > 0)
                    {
                        #region 删除操作
                        string sqlDel = "update BPM_CargoMX set status=0 where id not in(" + MXids + ") and Sampleid=" + models.id + "";
                        SqlHelper.InsertDelUpdate(sqlDel);
                        #endregion

                        for (int i = 0; i < MXnum; i++)
                        {
                            var mxInfo = MxArr[i].Split(',');
                            string sqlMX = "";
                            if (MXid[i] != "0")//进行明细编辑操作
                            {
                                sqlMX = "update BPM_CargoMX set SampleMX01=@SampleMX01,SampleMX03=@SampleMX03,SampleMX04=@SampleMX04,SampleMX05=@SampleMX05,SampleMX06=@SampleMX06,SampleMX07=@SampleMX07,SampleMX08=@SampleMX08,SampleMX09=@SampleMX09,SampleMX10=@SampleMX10,SampleMX11=@SampleMX11,SampleMX12=@SampleMX12,SampleMX13=@SampleMX13,SampleMX14=@SampleMX14,SampleMX15=@SampleMX15,SampleMX16=@SampleMX16,SampleMX17=@SampleMX17,SampleMX18=@SampleMX18,SampleMX19=@SampleMX19 where id=@id";
                            }
                            else//进行明细添加操作
                            {
                                sqlMX = "insert into BPM_CargoMX(Sampleid,SampleMX01,SampleMX03,SampleMX04,SampleMX05,SampleMX06,SampleMX07,SampleMX08,SampleMX09,SampleMX10,SampleMX11,SampleMX12,SampleMX13,SampleMX14,SampleMX15,SampleMX16,SampleMX17,SampleMX18,SampleMX19) values(@Sampleid,@SampleMX01,@SampleMX03,@SampleMX04,@SampleMX05,@SampleMX06,@SampleMX07,@SampleMX08,@SampleMX09,@SampleMX10,@SampleMX11,@SampleMX12,@SampleMX13,@SampleMX14,@SampleMX15,@SampleMX16,@SampleMX17,@SampleMX18,@SampleMX19)";
                            }
                            SqlParameter[] paramMX = new SqlParameter[]
                            {
                                #region 参数赋值
                                new SqlParameter("@Sampleid",models.id),//样衣Bom  id
                                new SqlParameter("@SampleMX01",string.IsNullOrWhiteSpace(mxInfo[0]) ? "" : mxInfo[0]),
                                new SqlParameter("@SampleMX03",string.IsNullOrWhiteSpace(mxInfo[1]) ? "" : mxInfo[1]),
                                new SqlParameter("@SampleMX04",string.IsNullOrWhiteSpace(mxInfo[2]) ? "" : mxInfo[2]),
                                new SqlParameter("@SampleMX05",string.IsNullOrWhiteSpace(mxInfo[3]) ? "" : mxInfo[3]),
                                new SqlParameter("@SampleMX06",string.IsNullOrWhiteSpace(mxInfo[4]) ? "" : mxInfo[4]),
                                new SqlParameter("@SampleMX07",string.IsNullOrWhiteSpace(mxInfo[5]) ? "" : mxInfo[5]),
                                new SqlParameter("@SampleMX08",string.IsNullOrWhiteSpace(mxInfo[6]) ? "" : mxInfo[6]),
                                new SqlParameter("@SampleMX09",string.IsNullOrWhiteSpace(mxInfo[7]) ? "" : mxInfo[7]),
                                new SqlParameter("@SampleMX10",string.IsNullOrWhiteSpace(mxInfo[8]) ? "" : mxInfo[8]),
                                new SqlParameter("@SampleMX11",string.IsNullOrWhiteSpace(mxInfo[9]) ? "" : mxInfo[9]),
                                new SqlParameter("@SampleMX12",string.IsNullOrWhiteSpace(mxInfo[10]) ? "" : mxInfo[10]),
                                new SqlParameter("@SampleMX13",string.IsNullOrWhiteSpace(mxInfo[11]) ? "" : mxInfo[11]),
                                new SqlParameter("@SampleMX14",string.IsNullOrWhiteSpace(mxInfo[12]) ? "" : mxInfo[12]),
                                new SqlParameter("@SampleMX15",string.IsNullOrWhiteSpace(mxInfo[13]) ? "" : mxInfo[13]),
                                new SqlParameter("@SampleMX16",string.IsNullOrWhiteSpace(mxInfo[14]) ? "" : mxInfo[14]),
                                new SqlParameter("@SampleMX17",string.IsNullOrWhiteSpace(mxInfo[15]) ? "" : mxInfo[15]),
                                new SqlParameter("@SampleMX18",string.IsNullOrWhiteSpace(mxInfo[16]) ? "" : mxInfo[16]),
                                new SqlParameter("@SampleMX19",string.IsNullOrWhiteSpace(mxInfo[17]) ? "" : mxInfo[17]),
                                new SqlParameter("@id",MXid[i]) //明细ID
	                            #endregion
                            };
                            if (mxInfo[0] != "undefined")
                            {
                                count = SqlHelper.InsertDelUpdate(sqlMX, paramMX);
                            }
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
        /// 删除大货Bom的方法
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DelCargoTable(string id)
        {
            string sql = "update BPM_CargoList set status=0 where id in (" + id + ")";
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
        /// 批量修改大货bom状态
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdateState()
        {
            var data = Request["data"];   //要修改的状态
            var id = Request["id"];        //修改的列名
            var ckval = Request["ckval"];  //修改的id
            ckval = ckval.Substring(0, ckval.Length - 1);
            string sql = "update BPM_CargoList set " + id + " = @data  where id in(" + ckval + ")";
            int count = SqlHelper.InsertDelUpdate(sql, new SqlParameter("@data", data));

            if (count > 0)
            {
                return Content("success");
            }
            else
            {
                return Content("error");
            }
        }

        //-----------------------------------------部分文本框连接其它数据库绑定------------------------------------------------------------//

        public ActionResult SelectOther()
        {
            var Did = Request["Did"];           //ID文本框输入值
            var DName = Request["DName"];        //名称文本框输入值
            var HtmlName = Request["HtmlName"];   //文本框名字

            var selXM = Request["selXM"];  //项目类型 面料/辅料/工艺工序等

            var strID = "";   //数据库ID
            var strName = "";  //数据库名称
            string sql = "";
            if (HtmlName == "设计师")
            {
                sql = "select * from BS_BUS_ColorBOMMaster where 1=1";
                strID = "DesignID";
                strName = "Designer";
            }
            else if (HtmlName == "款式类别")
            {
                sql = "select * from fjsx3 where 1=1";
                strID = "SXDM";
                strName = "SXMC";
            }
            else if (HtmlName == "大类")
            {
                sql = "select * from BS_BUS_ColorBOMMaster where 1=1";
                strID = "BigType";
                strName = "BigTypeName";
            }
            else if (HtmlName == "波段") //波段
            {
                sql = "select * from fjsx2 where 1=1";
                strID = "SXDM";
                strName = "SXMC";
            }
            else if (HtmlName == "颜色")
            {
                if (selXM == "颜色") //guige1
                {
                    sql = "select * from guige1 where 1=1";
                    strID = "GGDM";
                    strName = "GGMC";
                }
                else //HGUIGE1
                {
                    sql = "select * from HGUIGE1 where 1=1";
                    strID = "GGDM";
                    strName = "GGMC";
                }
            }
            else if (HtmlName == "单位")
            {
                sql = "select * from DANWEI where 1=1";
                strID = "DWDM";
                strName = "DWMC";
            }
            else if (HtmlName == "使用部位")
            {
                sql = "select * from BUWEI where 1=1";
                strID = "BWDM";
                strName = "BWMC";
            }
            else
            {
                if (selXM == "工艺工序")
                {
                    sql = "select * from BS_BUS_ArtPart where 1=1";
                    strID = "Code";
                    strName = "Name";
                }
                else if (selXM == "一般辅料" || selXM == "包装辅料")
                {
                    sql = "select * from FULIAO where 1=1";
                    strID = "FLDM";
                    strName = "FLMC";
                }
                else
                {
                    sql = "select * from MIANLIAO where 1=1";
                    strID = "MLDM";
                    strName = "MLMC";
                }
            }

            if (!string.IsNullOrWhiteSpace(Did))
            {
                sql += " and " + strID + " like'%" + Did + "%'";
            }
            if (!string.IsNullOrWhiteSpace(DName))
            {
                sql += " and " + strName + " like'%" + DName + "%'";
            }
            DataTable dt = SqlHelper.SelectTable(sql);
            return Content(JsonConvert.SerializeObject(dt));
        }

        //-----------------------------------------连接外部数据库并操作------------------------------------------------------------//

        public string GetAddBS_BUS_Sample(string id)
        {

            string conStr = ConfigurationManager.ConnectionStrings["look"].ConnectionString;
            if (conStr == "1")
            {
                #region MyRegion
                try
                {
                    int MXcount = 0;  //用于判断明细表有无数据
                    string sql = "select * from BPM_SampleList where id in(" + id + ") and status=1";    //样衣bom
                    var str = id.Split(',');
                    for (int i = 0; i < str.Length; i++)
                    {
                        string sqlMX1 = "select * from BPM_SampleMX where Sampleid=" + str[i] + " and status=1";  //样衣明细bom
                        if (SqlHelper.SelectTable(sqlMX1).Rows.Count == 0)
                        {
                            return "null";
                        }

                    }

                    DataTable dt = SqlHelper.SelectTable(sql);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            string sqlMX = "select * from BPM_SampleMX where Sampleid=" + item["id"] + " and status=1";  //样衣明细bom
                            DataTable dtMX = SqlHelper.SelectTable(sqlMX); //MXcount = dtMX.Rows.Count;

                            if (dtMX.Rows.Count > 0)
                            {
                                string guid = Guid.NewGuid().ToString();
                                string NewGuid = Guid.NewGuid().ToString();
                                #region 表添加Sql语句
                                //BS_BUS_Samples表添加
                                string sqlS = "INSERT INTO BS_BUS_Samples(MasterID,state,code ,name,unitcode , unitname, designid, designer, chiefdesignid, chiefdesigner, brandcode, brandname, bigtypecode, bigtypename, seasoncode, seasonname, yearcode, yearname, property01, property02, property03,property04,property05,createtime,updatetime,operatorid,operator)VALUES(@MasterID,@state,@code ,@name,@unitcode , @unitname, @designid, @designer, '111023', '朱虹', @brandcode , @brandname, @bigtypecode, @bigtypename, @seasoncode, @seasonname, @yearcode, '年', '女装', @property02, '棉衣','订货-普通(含童装羽绒)','000',@createtime,'2015-04-09 19:17:27.000','59BF735F-74B5-461A-BB0E-29B29D57B7A0','Fitch')";
                                //BS_BUS_SampleSize表添加
                                string sqlSize = "INSERT INTO BS_BUS_SampleSize(MasterID, DetailID, state,code,name,createtime,updatetime,operatorid,operator )VALUES(@MasterID, @DetailID, 1, 23, 'M', @createtime, '2018-02-01 15:14:02.000', '59BF735F-74B5-461A-BB0E-29B29D57B7A0', 'Fitch')";
                                //BS_BUS_SampleColor颜色表添加
                                string sqlColor = "INSERT INTO BS_BUS_SampleColor(MasterID, DetailID,state,code,name,createtime,updatetime,operatorid,operator )VALUES(@MasterCID, @ColorDetailID, 1, @code , @name,  @createCtime, '2018-02-01 15:14:02.000' , '59BF735F-74B5-461A-BB0E-29B29D57B7A0', 'Fitch')";

                                //BS_BUS_ColorBOMMaster表添加
                                string sqlMaste = "insert into BS_BUS_ColorBOMMaster(MasterID, state, Code, BrandCode, BrandName, SeasonCode, SeasonName, StageCode, StageName, GoodsCode, GoodsName, CreateTime, DesignID, Designer, BigType, BigTypeName) values(@MasterT1, @MasterT2, @MasterT3, @MasterT4, @MasterT5, @MasterT6, @MasterT7, @MasterT8, @MasterT9, @MasterT10, @MasterT11, @MasterT12, @MasterT13, @MasterT14, @MasterT15, @MasterT16)";

                                //BS_BUS_ColorBOMMaster表添加
                                string sqlDetail = "insert into BS_BUS_ColorBOMDetail(MasterID,state,DetailID,TypeCode,TypeName,CreateTime,Code,ConfigSizeName,Name,Specification,ColorCode,ColorName,MaterialUnit,PartCode,PartName,RequestQty,LossRate,MaterialQty,MaterialPrice,MaterialAMT,Remark,GHSDM,Class)values(@DetailT1,@DetailT2,@DetailT3,@DetailT4,@DetailT5,@DetailT6,@DetailT7,@DetailT8,@DetailT9,@DetailT10,@DetailT11,@DetailT12,@DetailT13,@DetailT14,@DetailT15,@DetailT16,@DetailT17,@DetailT18,@DetailT19,@DetailT20,@DetailT21,@DetailT22,@DetailT23)";

                                //BS_BUS_ColorBOMCraftStep表添加
                                string sqlCraftStep = "insert into BS_BUS_ColorBOMCraftStep(MasterID,DetailID,state,CreateTime,Code,Name,CraftPrice,CraftAmt) values(@CraftStep1,@CraftStep2,@CraftStep3,@CraftStep4,@CraftStep5,@CraftStep6,@CraftStep7,@CraftStep8)";
                                #endregion

                                string selDW = "select DWDM from DANWEI where DWMC = '" + dtMX.Rows[0]["SampleMX09"] + "'";//查询单位代码
                                string selPP = "select PPDM from PINPAI where PPMC = '" + item["Sample02"] + "'";//查询品牌代码
                                string selJJ = "select JJDM from jijie where JJMC = '" + item["Sample04"] + "'";//查询季节代码


                                SqlParameter[] param = new SqlParameter[]
                                {
                             #region 循环赋值
                                new SqlParameter("@MasterID",guid),
                                new SqlParameter("@state","1"),
                                new SqlParameter("@code",string.IsNullOrWhiteSpace(item["Sample06"].ToString())==true ? "" : item["Sample06"]),//设计款号
                                new SqlParameter("@name",item["Sample08"]),
                                new SqlParameter("@unitcode",string.IsNullOrWhiteSpace(dtMX.Rows[0]["SampleMX09"].ToString()) == true ? "0000" : SqlHelper.SelectSinger(selDW)),  //单位代码
                                new SqlParameter("@unitname",string.IsNullOrWhiteSpace(dtMX.Rows[0]["SampleMX09"].ToString()) == true ? "件" : dtMX.Rows[0]["SampleMX09"]),  //单位
                                new SqlParameter("@designid",string.IsNullOrWhiteSpace(item["Sample18"].ToString())==true ? "" : item["Sample18"]),
                                new SqlParameter("@designer",string.IsNullOrWhiteSpace(item["Sample11"].ToString())==true ? "" : item["Sample11"]),
                                new SqlParameter("@brandcode",string.IsNullOrWhiteSpace(item["Sample02"].ToString())==true ? "" : SqlHelper.SelectSinger(selPP)),      //品牌代码
                                new SqlParameter("@brandname",string.IsNullOrWhiteSpace(item["Sample02"].ToString())==true ? "" :item["Sample02"]),
                                new SqlParameter("@bigtypecode",string.IsNullOrWhiteSpace(item["Sample20"].ToString())==true ? "" :item["Sample20"]),
                                new SqlParameter("@bigtypename",string.IsNullOrWhiteSpace(item["Sample09"].ToString())==true ? "" :item["Sample09"]),
                                new SqlParameter("@seasoncode",string.IsNullOrWhiteSpace(item["Sample04"].ToString())==true ? "" :SqlHelper.SelectSinger(selJJ)),    //季节代码
                                new SqlParameter("@seasonname",string.IsNullOrWhiteSpace(item["Sample04"].ToString())==true ? "" :item["Sample04"]),
                                new SqlParameter("@yearcode",string.IsNullOrWhiteSpace(item["Sample03"].ToString())==true ? "" :Convert.ToDateTime(item["Sample03"]).ToString("yyyy")),
                                new SqlParameter("@property02",string.IsNullOrWhiteSpace(item["Sample05"].ToString())==true ? "" :item["Sample05"]),
                                new SqlParameter("@createtime",Convert.ToDateTime(DateTime.Now)),
                                new SqlParameter("@DetailID",NewGuid),
                                //------------------BS_BUS_ColorBOMMaster------------------------//
                                new SqlParameter("@MasterT1",guid), //@MasterID
                                new SqlParameter("@MasterT2","1"),  //state
                                new SqlParameter("@MasterT3",string.IsNullOrWhiteSpace(item["Sample01"].ToString())==true ? "" :item["Sample01"]),
                                new SqlParameter("@MasterT4",string.IsNullOrWhiteSpace(item["Sample02"].ToString())==true ? "" :SqlHelper.SelectSinger(selPP)),
                                new SqlParameter("@MasterT5",string.IsNullOrWhiteSpace(item["Sample02"].ToString())==true ? "" :item["Sample02"]),
                                new SqlParameter("@MasterT6",string.IsNullOrWhiteSpace(item["Sample04"].ToString())==true ? "" :SqlHelper.SelectSinger(selJJ)),
                                new SqlParameter("@MasterT7",string.IsNullOrWhiteSpace(item["Sample04"].ToString())==true ? "" :item["Sample04"]),
                                new SqlParameter("@MasterT8",string.IsNullOrWhiteSpace(item["Sample17"].ToString())==true ? "" :item["Sample17"]),
                                new SqlParameter("@MasterT9",string.IsNullOrWhiteSpace(item["Sample05"].ToString())==true ? "" :item["Sample05"]),
                                new SqlParameter("@MasterT10",string.IsNullOrWhiteSpace(item["Sample06"].ToString())==true ? "" :item["Sample06"]),
                                new SqlParameter("@MasterT11",string.IsNullOrWhiteSpace(item["Sample07"].ToString())==true ? "" :item["Sample07"]),
                                new SqlParameter("@MasterT12",DateTime.Now),
                                new SqlParameter("@MasterT13",string.IsNullOrWhiteSpace(item["Sample18"].ToString())==true ? "" :item["Sample18"]),
                                new SqlParameter("@MasterT14",string.IsNullOrWhiteSpace(item["Sample11"].ToString())==true ? "" :item["Sample11"]),
                                new SqlParameter("@MasterT15",string.IsNullOrWhiteSpace(item["Sample20"].ToString())==true ? "" :item["Sample20"]),
                                new SqlParameter("@MasterT16",string.IsNullOrWhiteSpace(item["Sample09"].ToString())==true ? "" :item["Sample09"]),

                                    #endregion
                                };
                                SqlHelper.InsertDelUpdate(sqlS, param); //主表添加
                                                                        //SqlHelper.InsertDelUpdate(sqlSize, param); //尺码表添加
                                SqlHelper.InsertDelUpdate(sqlMaste, param); //BS_BUS_ColorBOMMaster添加

                                //--------------------------BS_BUS_SampleColor----------------------------------//
                                var Tcolor = item["Sample22"].ToString().Split(',');
                                var TcoID = item["Sample21"].ToString().Split(',');
                                for (int i = 0; i < TcoID.Length; i++)
                                {
                                    string CGuid = Guid.NewGuid().ToString();
                                    SqlParameter[] pColor = new SqlParameter[]
                                    {
                                    new SqlParameter("@MasterCID",guid),//ColorDetailID
                                    new SqlParameter("@code",string.IsNullOrWhiteSpace(TcoID[i])==true ? "00" : TcoID[i]),
                                    new SqlParameter("@name",string.IsNullOrWhiteSpace(Tcolor[i])==true ? "通色" :Tcolor[i]),
                                    new SqlParameter("@createCtime",DateTime.Now),
                                    new SqlParameter("@ColorDetailID",CGuid),
                                    };
                                    SqlHelper.InsertDelUpdate(sqlColor, pColor); //颜色表添加
                                }


                                foreach (DataRow itemC in dtMX.Rows)
                                {
                                    string Nguid = Guid.NewGuid().ToString();
                                    string NewGuidTwo = Guid.NewGuid().ToString();

                                    string selYS = "select GGDM from HGUIGE1 where GGMC = '" + itemC["SampleMX07"] + "'";//查询颜色代码
                                    string selSYBW = "select BWDM from BUWEI where BWMC = '" + itemC["SampleMX11"] + "'";//查询部位代码

                                    var Lclass = 0;

                                    //判断class
                                    if (itemC["SampleMX01"].ToString() == "面料" || itemC["SampleMX01"].ToString() == "配料" || itemC["SampleMX01"].ToString() == "里料")
                                    {
                                        Lclass = 0;
                                    }
                                    else if (itemC["SampleMX01"].ToString() != "工艺工序")
                                    {
                                        Lclass = 1;
                                    }
                                    else
                                    {
                                        Lclass = 2;
                                    }
                                    if (Lclass != 2)
                                    {
                                        SqlParameter[] paramC = new SqlParameter[]
                                        {
                                    #region 循环赋值
                                    //new SqlParameter("@MasterCID",Nguid),
                                    //new SqlParameter("@code",string.IsNullOrWhiteSpace(itemC["SampleMX07"].ToString())==true ? "00" : SqlHelper.SelectSinger(selYS)),
                                    //new SqlParameter("@name",string.IsNullOrWhiteSpace(itemC["SampleMX07"].ToString())==true ? "通色" :itemC["SampleMX07"]),
                                    //new SqlParameter("@createCtime",Convert.ToDateTime(DateTime.Now.ToString())),
                                    //new SqlParameter("@ColorDetailID",NewGuidTwo),

                                    //--------------------------BS_BUS_ColorBOMDetail----------------------------------//
                                    new SqlParameter("@DetailT1",guid),
                                    new SqlParameter("@DetailT2","1"),
                                    new SqlParameter("@DetailT3",NewGuidTwo),
                                    new SqlParameter("@DetailT4",itemC["SampleMX01"].ToString()=="面料" ? "ML001" :itemC["SampleMX01"].ToString()=="配料" ? "PL001" :itemC["SampleMX01"].ToString()=="里料" ? "LL001" :itemC["SampleMX01"].ToString()=="一般辅料" ? "YBFL001" : "BZFL001"),
                                    new SqlParameter("@DetailT5",string.IsNullOrWhiteSpace(itemC["SampleMX01"].ToString())==true ? "" :itemC["SampleMX01"]),
                                    new SqlParameter("@DetailT6",DateTime.Now),
                                    new SqlParameter("@DetailT7",string.IsNullOrWhiteSpace(itemC["SampleMX03"].ToString())==true ? "" :itemC["SampleMX03"]),
                                    new SqlParameter("@DetailT8",string.IsNullOrWhiteSpace(itemC["SampleMX04"].ToString())==true ? "" :itemC["SampleMX04"]),
                                    new SqlParameter("@DetailT9",string.IsNullOrWhiteSpace(itemC["SampleMX05"].ToString())==true ? "" :itemC["SampleMX05"]),
                                    new SqlParameter("@DetailT10",string.IsNullOrWhiteSpace(itemC["SampleMX06"].ToString())==true ? "" :itemC["SampleMX06"]),
                                    new SqlParameter("@DetailT11",string.IsNullOrWhiteSpace(itemC["SampleMX07"].ToString())==true ? "00" : SqlHelper.SelectSinger(selYS)),
                                    new SqlParameter("@DetailT12",string.IsNullOrWhiteSpace(itemC["SampleMX07"].ToString())==true ? "通色" :itemC["SampleMX07"]),
                                    new SqlParameter("@DetailT13",string.IsNullOrWhiteSpace(itemC["SampleMX09"].ToString())==true ? "" :itemC["SampleMX09"]),
                                    new SqlParameter("@DetailT14",string.IsNullOrWhiteSpace(itemC["SampleMX11"].ToString())==true ? "" :SqlHelper.SelectSinger(selSYBW)),
                                    new SqlParameter("@DetailT15",string.IsNullOrWhiteSpace(itemC["SampleMX11"].ToString())==true ? "" :itemC["SampleMX11"]),
                                    new SqlParameter("@DetailT16",string.IsNullOrWhiteSpace(itemC["SampleMX12"].ToString())==true ? "" :itemC["SampleMX12"]),
                                    new SqlParameter("@DetailT17",string.IsNullOrWhiteSpace(itemC["SampleMX13"].ToString())==true ? "0" :itemC["SampleMX13"]),
                                    new SqlParameter("@DetailT18",string.IsNullOrWhiteSpace(itemC["SampleMX14"].ToString())==true ? "0" : itemC["SampleMX14"]),
                                    new SqlParameter("@DetailT19",string.IsNullOrWhiteSpace(itemC["SampleMX15"].ToString())==true ? "0" : itemC["SampleMX15"]),
                                    new SqlParameter("@DetailT20",string.IsNullOrWhiteSpace(itemC["SampleMX16"].ToString())==true ? "0" : itemC["SampleMX16"]),
                                    new SqlParameter("@DetailT21",string.IsNullOrWhiteSpace(itemC["SampleMX17"].ToString())==true ? "" :itemC["SampleMX17"]),
                                    new SqlParameter("@DetailT22",string.IsNullOrWhiteSpace(itemC["SampleMX19"].ToString())==true ? "" :itemC["SampleMX19"]),
                                    new SqlParameter("@DetailT23",Lclass),


                                            #endregion
                                        };

                                        SqlHelper.InsertDelUpdate(sqlDetail, paramC); //BS_BUS_ColorBOMDetai表添加
                                    }
                                    else
                                    {
                                        SqlParameter[] paramC = new SqlParameter[]
                                        {
                                    #region 循环赋值
                                    //--------------------------BS_BUS_ColorBOMDetail----------------------------------//
                                    new SqlParameter("@CraftStep1", guid),
                                    new SqlParameter("@CraftStep2", NewGuidTwo),
                                    new SqlParameter("@CraftStep3", "1"),
                                    new SqlParameter("@CraftStep4", DateTime.Now),
                                    new SqlParameter("@CraftStep5", string.IsNullOrWhiteSpace(itemC["SampleMX03"].ToString()) == true ? "" : itemC["SampleMX03"]),
                                    new SqlParameter("@CraftStep6", string.IsNullOrWhiteSpace(itemC["SampleMX05"].ToString()) == true ? "" : itemC["SampleMX05"]),
                                    new SqlParameter("@CraftStep7", string.IsNullOrWhiteSpace(itemC["SampleMX15"].ToString()) == true ? "0" : itemC["SampleMX15"]),
                                    new SqlParameter("@CraftStep8", string.IsNullOrWhiteSpace(itemC["SampleMX16"].ToString()) == true ? "0" : itemC["SampleMX16"]),
                                            #endregion
                                        };
                                        SqlHelper.InsertDelUpdate(sqlCraftStep, paramC); //BS_BUS_ColorBOMCraftStep表添加
                                    }

                                }
                            }

                        }
                    }
                    //if (MXcount == 0)
                    //{
                    //    return "null";
                    //}
                    //else
                    //{
                    //}
                    return "CG";
                }
                catch (Exception ex)
                {

                    return "SB";
                }

                #endregion
            }
            else
            {
                return "未执行";
            }
        }

    }
}


