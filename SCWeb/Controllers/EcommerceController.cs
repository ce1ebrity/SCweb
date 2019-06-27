using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCWeb.Models;
using SCWeb.Helper;
using System.Data;
using System.IO;

namespace SCWeb.Controllers
{
    public class EcommerceController : Controller
    {
        // GET: Ecommerce
        public ActionResult Index()
        {
            return View();
        }

        //平台数据录入
        public ActionResult PTInfoIN(Models.BPM_PTReportModels models)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(models.PTReport16.ToString()) == true)
                {
                    models.PTReport16 = 0;
                    models.PTReport17 = 0;
                    models.PTReport18 = 0;
                    models.PTReport19 = 0;
                    models.PTReport20 = 0;
                    models.PTReport21 = 0;
                }
                //string sql = "select PreSale from BPM_PreSale where saleDate=@saleDate";
                //int saleDate = int.Parse(SqlHelper.SelectSinger(sql, new SqlParameter("@saleDate", models.saleDate)).ToString());//根据日期求出预销售额
                models.PTReport6 = (models.PTReport4 - models.PTReport5) == 0 ? 1 : (models.PTReport4 - models.PTReport5);

                if (models.PTReport6 <= 0)
                {
                    models.PTReport7 = 0;
                }
                else
                {
                    models.PTReport7 = models.PTReport6 * decimal.Parse(0.055.ToString());
                }



               
                if (models.PTReport4 == 0)
                {
                    models.PTReport8 = 0;
                    models.PTReport10 = 0;
                }
                else
                {
                    models.PTReport8 = models.PTReport5 / models.PTReport4;
                    models.PTReport10 = models.PTReport9 / models.PTReport4;
                }
                if (models.PTReport6 == 0)
                {
                    models.PTReport11 = 0;
                }
                else {
                    models.PTReport11 = models.PTReport9 / models.PTReport6;
                }


                if (models.PTReport13 == 0)
                {
                    models.PTReport14 = 0;
                }
                else {
                    models.PTReport14 = models.PTReport1 / models.PTReport13;
                }


                if (models.PTReport1 == 0)
                {
                    models.PTReport15 = 0;
                }
                else
                {
                    models.PTReport15 = models.PTReport4 / models.PTReport1;
                }
                models.PTReport9 = models.PTReport16 + models.PTReport17 + models.PTReport18;

                models.PTReport12 = 0;//models.PTReport6 / 1; //达成率 //-- PreSale预销售额暂设为0

                
                string sql = "delete BPM_PTReport where saleDate=@saleDate and PTReport30=@PTReport30;;insert into BPM_PTReport(PTReport1,PTReport3,PTReport4,PTReport5,PTReport6,PTReport7,PTReport8,PTReport9,PTReport10,PTReport11,PTReport12,PTReport13,PTReport14,PTReport15,PTReport16,PTReport17,PTReport18,PTReport19,PTReport20,PTReport21,PTReport22,PTReport30,saleDate) values(@PTReport1,@PTReport3,@PTReport4,@PTReport5,@PTReport6,@PTReport7,@PTReport8,@PTReport9,@PTReport10,@PTReport11,@PTReport12,@PTReport13,@PTReport14,@PTReport15,@PTReport16,@PTReport17,@PTReport18,@PTReport19,@PTReport20,@PTReport21,@PTReport22,@PTReport30,@saleDate)";
                SqlParameter[] param = new SqlParameter[]
                {
                    #region 赋值
                    new SqlParameter("@PTReport1",string.IsNullOrWhiteSpace(models.PTReport1.ToString())==true ? 0 :models.PTReport1), //订单数
                    new SqlParameter("@PTReport3",string.IsNullOrWhiteSpace(models.PTReport3.ToString())==true ? 0 :models.PTReport3), //销售件数
                    new SqlParameter("@PTReport4",string.IsNullOrWhiteSpace(models.PTReport4.ToString())==true ? 0 :models.PTReport4), //销售金额
                    new SqlParameter("@PTReport5",string.IsNullOrWhiteSpace(models.PTReport5.ToString())==true ? 0 :models.PTReport5), //退款额
                    new SqlParameter("@PTReport6",string.IsNullOrWhiteSpace(models.PTReport6.ToString())==true ? 0 :models.PTReport6), //净销
                    new SqlParameter("@PTReport7",string.IsNullOrWhiteSpace(models.PTReport7.ToString())==true ? 0 :models.PTReport7), //扣点
                    new SqlParameter("@PTReport8",string.IsNullOrWhiteSpace(models.PTReport8.ToString())==true ? 0 :models.PTReport8), //退款率
                    new SqlParameter("@PTReport9",string.IsNullOrWhiteSpace(models.PTReport9.ToString())==true ? 0 :models.PTReport9), //广告花费
                    new SqlParameter("@PTReport10",string.IsNullOrWhiteSpace(models.PTReport10.ToString())==true ? 0 :models.PTReport10),//广告占比（实销）
                    new SqlParameter("@PTReport11",string.IsNullOrWhiteSpace(models.PTReport11.ToString())==true ? 0 :models.PTReport11),//广告占比（净销）
                    new SqlParameter("@PTReport12",string.IsNullOrWhiteSpace(models.PTReport12.ToString())==true ? 0 :models.PTReport12),//达成率
                    new SqlParameter("@PTReport13",string.IsNullOrWhiteSpace(models.PTReport13.ToString())==true ? 0 :models.PTReport13),//UV
                    new SqlParameter("@PTReport14",string.IsNullOrWhiteSpace(models.PTReport14.ToString())==true ? 0 :models.PTReport14),//转化率
                    new SqlParameter("@PTReport15",string.IsNullOrWhiteSpace(models.PTReport15.ToString())==true ? 0 :models.PTReport15),//客单价
                    new SqlParameter("@PTReport16",string.IsNullOrWhiteSpace(models.PTReport16.ToString())==true ? 0 :models.PTReport16),//直通车
                    new SqlParameter("@PTReport17",string.IsNullOrWhiteSpace(models.PTReport17.ToString())==true ? 0 :models.PTReport17),//钻展
                    new SqlParameter("@PTReport18",string.IsNullOrWhiteSpace(models.PTReport18.ToString())==true ? 0 :models.PTReport18),//品销宝
                    new SqlParameter("@PTReport19",string.IsNullOrWhiteSpace(models.PTReport16.ToString())==true ? 0 :models.PTReport16),//快车
                    new SqlParameter("@PTReport20",string.IsNullOrWhiteSpace(models.PTReport17.ToString())==true ? 0 :models.PTReport17),//直投
                    new SqlParameter("@PTReport21",string.IsNullOrWhiteSpace(models.PTReport18.ToString())==true ? 0 :models.PTReport18),//其它
                    new SqlParameter("@PTReport22",string.IsNullOrWhiteSpace(models.PTReport22)==true ? "0" :models.PTReport22),//活动备注
                    new SqlParameter("@PTReport30",string.IsNullOrWhiteSpace(models.PTReport30.ToString())==true ? 0 :models.PTReport30),//报表分组
                    new SqlParameter("@saleDate",string.IsNullOrWhiteSpace(models.saleDate)==true ? "" :Convert.ToDateTime(models.saleDate).ToString("yyyy-MM-dd")), //日期
	                #endregion        
                };
                int count = SqlHelper.InsertDelUpdate(sql, param);
                if (count < 1)
                {
                    return Content("error");
                }
                return Content("success");
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public ActionResult SetPTExecl(HttpPostedFileBase filed)
        {
            //Common.SaveExcelFile(filed);
            string filePath = SaveAsLoed(filed);
            string sql = "delete BPM_PTReport where saleDate=@saleDate and PTReport30=@PTReport30;insert into BPM_PTReport(PTReport1,PTReport3,PTReport4,PTReport5,PTReport6,PTReport7,PTReport8,PTReport9,PTReport10,PTReport11,PTReport12,PTReport13,PTReport14,PTReport15,PTReport16,PTReport17,PTReport18,PTReport19,PTReport20,PTReport21,PTReport22,PTReport30,saleDate) values(@PTReport1,@PTReport3,@PTReport4,@PTReport5,@PTReport6,@PTReport7,@PTReport8,@PTReport9,@PTReport10,@PTReport11,@PTReport12,@PTReport13,@PTReport14,@PTReport15,@PTReport16,@PTReport17,@PTReport18,@PTReport19,@PTReport20,@PTReport21,@PTReport22,@PTReport30,@saleDate)";
            try
            {
                DataTable dt = Common.ExcelToDataTable(filePath, true, 0);
                foreach (DataRow dr in dt.Rows)
                {
                    BPM_PTReportModels models = new BPM_PTReportModels();
                    models.PTReport30 = setDS(dr["电商"].ToString());
                    models.saleDate = Convert.ToDateTime(dr["日期"]).ToString("yyyy-MM-dd");
                    models.PTReport1 = Convert.ToDecimal(dr["订单数"]);
                    models.PTReport3 = Convert.ToDecimal(dr["销售件数"]);
                    models.PTReport4 = Convert.ToDecimal(dr["销售金额"]);
                    models.PTReport5 = Convert.ToDecimal(dr["退款额"]);
                    models.PTReport13 = Convert.ToDecimal(dr["UV"]);
                    models.PTReport22 = dr["活动备注"].ToString();
                    models.PTReport16 = Convert.ToDecimal(dr["直通车(快车)"]);
                    models.PTReport17 = Convert.ToDecimal(dr["钻展(直投)"]);
                    models.PTReport18 = Convert.ToDecimal(dr["品销宝(其他)"]);
                    models.PTReport6 = (models.PTReport4 - models.PTReport5) == 0 ? 1 : (models.PTReport4 - models.PTReport5);
                    if (models.PTReport6 <= 0)
                    {
                        models.PTReport7 = 0;
                    }
                    else
                    {
                        models.PTReport7 = models.PTReport6 * decimal.Parse(0.055.ToString());
                    }
                    if (models.PTReport4 == 0)
                    {
                        models.PTReport8 = 0;
                        models.PTReport10 = 0;
                    }
                    else
                    {
                        models.PTReport8 = models.PTReport5 / models.PTReport4;
                        models.PTReport10 = models.PTReport9 / models.PTReport4;
                    }
                    if (models.PTReport6 == 0)
                    {
                        models.PTReport11 = 0;
                    }
                    else {
                        models.PTReport11 = models.PTReport9 / models.PTReport6;
                    }


                    if (models.PTReport13 == 0)
                    {
                        models.PTReport14 = 0;
                    }
                    else {
                        models.PTReport14 = models.PTReport1 / models.PTReport13;
                    }


                    if (models.PTReport1 == 0)
                    {
                        models.PTReport15 = 0;
                    }
                    else
                    {
                        models.PTReport15 = models.PTReport4 / models.PTReport1;
                    }
                    models.PTReport9 = models.PTReport16 + models.PTReport17 + models.PTReport18;

                    models.PTReport12 = 0;//models.PTReport6 / 1; //达成率 //-- PreSale预销售额暂设为0
                    SqlParameter[]  param = new SqlParameter[]
                    {
                    #region 赋值
                    new SqlParameter("@PTReport1",string.IsNullOrWhiteSpace(models.PTReport1.ToString())==true ? 0 :models.PTReport1), //订单数
                    new SqlParameter("@PTReport3",string.IsNullOrWhiteSpace(models.PTReport3.ToString())==true ? 0 :models.PTReport3), //销售件数
                    new SqlParameter("@PTReport4",string.IsNullOrWhiteSpace(models.PTReport4.ToString())==true ? 0 :models.PTReport4), //销售金额
                    new SqlParameter("@PTReport5",string.IsNullOrWhiteSpace(models.PTReport5.ToString())==true ? 0 :models.PTReport5), //退款额
                    new SqlParameter("@PTReport6",string.IsNullOrWhiteSpace(models.PTReport6.ToString())==true ? 0 :models.PTReport6), //净销
                    new SqlParameter("@PTReport7",string.IsNullOrWhiteSpace(models.PTReport7.ToString())==true ? 0 :models.PTReport7), //扣点
                    new SqlParameter("@PTReport8",string.IsNullOrWhiteSpace(models.PTReport8.ToString())==true ? 0 :models.PTReport8), //退款率
                    new SqlParameter("@PTReport9",string.IsNullOrWhiteSpace(models.PTReport9.ToString())==true ? 0 :models.PTReport9), //广告花费
                    new SqlParameter("@PTReport10",string.IsNullOrWhiteSpace(models.PTReport10.ToString())==true ? 0 :models.PTReport10),//广告占比（实销）
                    new SqlParameter("@PTReport11",string.IsNullOrWhiteSpace(models.PTReport11.ToString())==true ? 0 :models.PTReport11),//广告占比（净销）
                    new SqlParameter("@PTReport12",string.IsNullOrWhiteSpace(models.PTReport12.ToString())==true ? 0 :models.PTReport12),//达成率
                    new SqlParameter("@PTReport13",string.IsNullOrWhiteSpace(models.PTReport13.ToString())==true ? 0 :models.PTReport13),//UV
                    new SqlParameter("@PTReport14",string.IsNullOrWhiteSpace(models.PTReport14.ToString())==true ? 0 :models.PTReport14),//转化率
                    new SqlParameter("@PTReport15",string.IsNullOrWhiteSpace(models.PTReport15.ToString())==true ? 0 :models.PTReport15),//客单价
                    new SqlParameter("@PTReport16",string.IsNullOrWhiteSpace(models.PTReport16.ToString())==true ? 0 :models.PTReport16),//直通车
                    new SqlParameter("@PTReport17",string.IsNullOrWhiteSpace(models.PTReport17.ToString())==true ? 0 :models.PTReport17),//钻展
                    new SqlParameter("@PTReport18",string.IsNullOrWhiteSpace(models.PTReport18.ToString())==true ? 0 :models.PTReport18),//品销宝
                    new SqlParameter("@PTReport19",string.IsNullOrWhiteSpace(models.PTReport16.ToString())==true ? 0 :models.PTReport16),//快车
                    new SqlParameter("@PTReport20",string.IsNullOrWhiteSpace(models.PTReport17.ToString())==true ? 0 :models.PTReport17),//直投
                    new SqlParameter("@PTReport21",string.IsNullOrWhiteSpace(models.PTReport18.ToString())==true ? 0 :models.PTReport18),//其它
                    new SqlParameter("@PTReport22",string.IsNullOrWhiteSpace(models.PTReport22)==true ? "0" :models.PTReport22),//活动备注
                    new SqlParameter("@PTReport30",string.IsNullOrWhiteSpace(models.PTReport30.ToString())==true ? 0 :models.PTReport30),//报表分组
                    new SqlParameter("@saleDate",string.IsNullOrWhiteSpace(models.saleDate)==true ? "" :Convert.ToDateTime(models.saleDate).ToString("yyyy-MM-dd")), //日期
                        #endregion
                    };
                    SqlHelper.InsertDelUpdate(sql, param);
                }
            }
            catch (Exception)
            {
                return Content("上传失败请检查！");
            }
            return Content("上传成功");
        }
        public string SaveAsLoed(HttpPostedFileBase file)
        {
            var fileName = file.FileName;
            var filePath = Server.MapPath(string.Format("~/{0}", "Upload"));
            file.SaveAs(Path.Combine(filePath, fileName));
            return filePath + "/" + fileName;
        }
        public int setDS(string name)
        {
            if (name == "天猫")
            {
                return 1;
            }
            else if (name == "京东")
            {
                return 2;
            }
            else if (name == "有货")
            {
                return 3;
            }
            else if (name == "小红书")
            {
                return 4;
            }
            else if (name == "天虹")
            {
                return 5;
            }
            else if (name == "聚美")
            {
                return 6;
            }
            else if (name == "6")
            {
                return 7;
            }
            else if (name == "C店")
            {
                return 8;
            }
            else if (name == "爱库存")
            {
                return 9;
            }
            else {
                return 0;
            }
        }
        //总表
        public ActionResult AllPTInfo()
        {
            return View();
        }
        //总表录入
        public ActionResult GetAllPTInfo(BPM_PreSaleModels models)
        {
            try
            {
                string sql = "delete BPM_PreSale where saleDate=@saleDate;insert into BPM_PreSale(saleDate,PreSale,RealSale,Remarks) values(@saleDate,@PreSale,@RealSale,@Remarks)";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@saleDate",string.IsNullOrWhiteSpace(models.saleDate)==true?"":Convert.ToDateTime(models.saleDate).ToString("yyyy-MM-dd")),
                    new SqlParameter("@PreSale",string.IsNullOrWhiteSpace(models.PreSale.ToString())==true? 0 : models.PreSale),
                    new SqlParameter("@RealSale",string.IsNullOrWhiteSpace(models.RealSale.ToString())==true? 0 : models.RealSale),
                    new SqlParameter("@Remarks",string.IsNullOrWhiteSpace(models.Remarks)==true? "" : models.Remarks),
                };
                int count = SqlHelper.InsertDelUpdate(sql, param);
                if (count < 1)
                {
                    return Content("error");
                }
                return Content("success");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}