using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using SCWeb.Helper;
using SCWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace MPMS.Ajax
{
    /// <summary>
    /// Handler1 的摘要说明
    /// </summary>
    public class Handler1 : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            HttpPostedFile filePost = context.Request.Files["filed"]; // 获取上传的文件
            string filePath = SaveExcelFile(filePost); // 保存文件并获取文件路径
            string msg = ExcelToDataTable(filePath, true, context);
            context.Response.Write(msg);
        }

        /// <summary>
        /// 上传读取文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="isColumnName"></param>
        public string ExcelToDataTable(string filePath, bool isColumnName, HttpContext context)
        {
            string fName = context.Request["fName"];
            string Oper = "";
            if (fName == "Goodsfiled")
            {
                Oper = "下单Excel数据导入";
            }
            else if (fName == "PDfiled")//生产部生产排单
            {
                Oper = "排单Excel数据导入";
            }
            else if (fName == "PsJobfiled")//生产进度报工
            {
                Oper = "生产进度Excel数据导入";
            }
            else if (fName == "CGfiled")//采购部原料排期
            {
                Oper = "排期Excel数据导入";
            }
            else if (fName == "Pullfiled")//打样进度
            {
                Oper = "打样Excel数据导入";
            }
            else if (fName == "QACheckfiled")//品控质检报工
            {
                Oper = "品控Excel数据导入";
            }
            else if (fName == "PTExecl")//品控质检报工
            {
                Oper = "平台Excel数据导入";
            }
            else
            {
                return "数据有误";
            }

            string userId = Common.GetCookie("userLogin");
            string sql = "select roleId from BPM_UserBase where id=@id";
            int roleId = Convert.ToInt32(SqlHelper.SelectSinger(sql, new SqlParameter("@id", userId)));
            sql = "select COUNT(1) from dbo.BPM_MenuPowerBase ";
            sql += "where id in ( ";
            sql += "select menuPId from dbo.BPM_PropertyManagr ";
            sql += "where roleId = @roleId and status = 1) and MenuCode = @MenuCode and menuName = @menuName and status=1 ";
            //判断用户是否有权限进行操作
            int counts = Convert.ToInt32(SqlHelper.SelectSinger(sql, new SqlParameter("@roleId", roleId), new SqlParameter("@MenuCode", "BPM_SCJDB"), new SqlParameter("@menuName", Oper)));
            if (counts <= 0)
            {
                return "您没有权限请和管理员联系!";
            }
            try
            {
                DataTable dt = Common.ExcelToDataTable(filePath, true, 0);
                dt.Columns.Add("id");
                SqlParameter[] param = null;
                foreach (DataRow dr in dt.Rows)
                {
                    if (fName == "Goodsfiled")//商品部下单导入
                    {
                        string boduan = dr["波段"].ToString();
                        string pinpai = dr["品牌"].ToString();
                        string jg = dr["加工方式"].ToString();
                        string kuanhao = dr["款号"].ToString();
                        string kuanshi = dr["款式"].ToString();
                        string cor = dr["颜色"].ToString();
                        string cm1 = dr["110/S"].ToString();
                        string cm2 = dr["120/M"].ToString();
                        string cm3 = dr["130/L"].ToString();
                        string cm4 = dr["140/XL"].ToString();
                        string cm5 = dr["150/均码"].ToString();
                        string cm6 = dr["商品交期"].ToString();
                        string year = dr["商品年份"].ToString();
                        string JIJie = dr["季节"].ToString();
                        string gg1dm = dr["颜色代码"].ToString();
                        string huoqi70day = dr["货期前70天商品下单"].ToString();//huoqi70day
                        string qxk = dr["是否取消款"].ToString();
                        //List<int> a = new List<int>();
                        string sqltest = "select id,SCJD01,SCJD02,SCJD03,SCJD04,SCJD05,SCJD06,SCJD07,SCJD08,SCJD09,SCJD10,SCJD11,SCJD12,SCJD93,huoqi70day from BPM_SCJDB where SCJD05=@SCJD05 and SCJD07=@SCJD07";
                        DataTable dttest = SqlHelper.SelectTable(sqltest,
                        new SqlParameter("@SCJD05", kuanhao), new SqlParameter("@SCJD07", cor)
                        );
                        int id = 0;
                        foreach (DataRow item in dttest.Rows)
                        {
                            id = Convert.ToInt32(item["id"]);
                        }
                        DataRow drr = dttest.NewRow();
                        if (dttest.Rows.Count > 0)
                        {
                            //for (int i = 0; i < dttest.Rows.Count; i++)
                            //{
                            //    a.Add(Convert.ToInt32(dttest.Rows[i]["id"]));
                            //    //dt.Rows.Remove(dt.Rows[0]);
                            //}

                            //修改
                            //SCJD01=@SCJD01 and SCJD02=@SCJD02 and SCJD03=@SCJD03 and SCJD04=@SCJD04 and SCJD05=@SCJD05 and SCJD06=@SCJD06 and SCJD07=@SCJD07 
                            //and SCJD08 = @SCJD08 and SCJD09 = @SCJD09 and SCJD10 = @SCJD10 and SCJD11 = @SCJD11 and SCJD12 = @SCJD12 and SCJD93 = @SCJD93

                            //GG1DM=@GG1DM,huoqi70day=@huoqi70day
                            sql = @"update BPM_SCJDB set SCJD01=@SCJD01,SCJD02=@SCJD02,SCJD03=@SCJD03,SCJD04=@SCJD04,SCJD05=@SCJD05,SCJD06=@SCJD06,
                                    SCJD07=@SCJD07,SCJD08=@SCJD08,SCJD09=@SCJD09,SCJD10=@SCJD10,SCJD11=@SCJD11,SCJD12=@SCJD12,years=@years,JIJie=@JIJie,GG1DM=@GG1DM,huoqi70day=@huoqi70day,qxk=@qxk where id=@id ";
                            param = new SqlParameter[]{
                                new SqlParameter("@SCJD02", boduan),
                                new SqlParameter("@SCJD03", pinpai),
                                new SqlParameter("@SCJD04", jg),
                                new SqlParameter("@SCJD05", kuanhao),
                                new SqlParameter("@SCJD06", kuanshi),
                                new SqlParameter("@SCJD07", cor),
                                new SqlParameter("@SCJD08", cm1),
                                new SqlParameter("@SCJD09", cm2),
                                new SqlParameter("@SCJD10", cm3),
                                new SqlParameter("@SCJD11", cm4),
                                new SqlParameter("@SCJD12", cm5),
                                new SqlParameter("@SCJD01", cm6),
                                new SqlParameter("@years", year),
                                new SqlParameter("@JIJie", JIJie),
                                new SqlParameter("@GG1DM", gg1dm),
                                new SqlParameter("@huoqi70day", huoqi70day),
                                new SqlParameter("@qxk", qxk),
                                new SqlParameter("@id",id)};

                        }
                        else
                        {
                            //Models.MemCards model = new Models.MemCards();
                            sql = "insert into BPM_SCJDB(SCJD01,SCJD02,SCJD03,SCJD04,SCJD05,SCJD06,SCJD07,SCJD08,SCJD09,SCJD10,SCJD11,SCJD12,years,GG1DM,JIJie)  values (@SCJD01,@SCJD02,@SCJD03,@SCJD04,@SCJD05,@SCJD06,@SCJD07,@SCJD08,@SCJD09,@SCJD10,@SCJD11,@SCJD12,@years,@GG1DM,@JIJie)";
                            param = new SqlParameter[]{
                                new SqlParameter("@SCJD02", dr["波段"]),
                                new SqlParameter("@SCJD03", dr["品牌"]),
                                new SqlParameter("@SCJD04", dr["加工方式"]),
                                new SqlParameter("@SCJD05", dr["款号"]),
                                new SqlParameter("@SCJD06", dr["款式"]),
                                new SqlParameter("@SCJD07", dr["颜色"]),
                                new SqlParameter("@SCJD08", dr["110/S"]),
                                new SqlParameter("@SCJD09", dr["120/M"]),
                                new SqlParameter("@SCJD10", dr["130/L"]),
                                new SqlParameter("@SCJD11", dr["140/XL"]),
                                new SqlParameter("@SCJD12", dr["150/均码"]),
                                new SqlParameter("@SCJD01", dr["商品交期"]),
                                new SqlParameter("@years", dr["商品年份"]),
                                new SqlParameter("@GG1DM", dr["颜色代码"]),
                                new SqlParameter("@JIJie", dr["季节"])};
                        }
                    }
                    else if (fName == "PDfiled")//生产部生产排单
                    {
                        sql = @"update dbo.BPM_SCJDB set SCJD22=@SCJD22,SCJD23=@SCJD23,SCJD24=@SCJD24,SCJD99=@SCJD99,SCJD100=@SCJD100,SCJD34=@SCJD34,
                                                SCJD35 = @SCJD35,SCJD36 = @SCJD36,SCJD37 = @SCJD37,SCJD38 = @SCJD38,SCJD39 = @SCJD39,SCJD40 = @SCJD40,SCJD94 = @SCJD94
                                                ,SCJD95 = @SCJD95,SCJD96 = @SCJD96,SCJD97 = @SCJD97,SCJD98 = @SCJD98,SCJD102 = @SCJD102,SCJD104 = @SCJD104,SCJD41=@SCJD41,SCJD47=@SCJD47,SCJD48=@SCJD48,SCJD50 = @SCJD50,
                                              SCJD51 = @SCJD51,SCJD52 = @SCJD52,SCJD53 = @SCJD53,SCJD54=@SCJD54,SCJD55 = @SCJD55,SCJD56 = @SCJD56,SCJD57 = @SCJD57,
                                              SCJD67 = @SCJD67,SCJD75 = @SCJD75,SCJD86 = @SCJD86,SCJD87 = @SCJD87,SCJD106 = @SCJD106,SCJD107 = @SCJD107,SCJD49=@SCJD49,SCJD59=@SCJD59,SCJD61=@SCJD61,SL=@SL where SCJD02 = @SCJD02
                                                and SCJD03 = @SCJD03 and SCJD04 = @SCJD04 and SCJD05 = @SCJD05 and SCJD06 = @SCJD06 and SCJD07 = @SCJD07";
                        //,SCJD42=@SCJD42,SCJD43=@SCJD43,SCJD44=@SCJD44,SCJD45=@SCJD45,SCJD46=@SCJD46
                        param = new SqlParameter[]{
                                new SqlParameter("@SCJD02", dr["波段"]),
                                new SqlParameter("@SCJD03", dr["品牌"]),
                                new SqlParameter("@SCJD04", dr["加工方式"]),
                                new SqlParameter("@SCJD05", dr["款号"]),
                                new SqlParameter("@SCJD06", dr["款式"]),
                                new SqlParameter("@SCJD07", dr["颜色"]),
                                new SqlParameter("@SCJD94", dr["110/S"]),
                                new SqlParameter("@SCJD95", dr["120/M"]),
                                new SqlParameter("@SCJD96", dr["130/L"]),
                                new SqlParameter("@SCJD97", dr["140/XL"]),
                                new SqlParameter("@SCJD98", dr["150/均码"]),
                                new SqlParameter("@SCJD22", dr["工厂"]),
                                new SqlParameter("@SCJD23", dr["合同货期"]),
                                new SqlParameter("@SCJD24", dr["跟单员"]),
                                new SqlParameter("@SCJD99", dr["技术部提供用量"]),
                                new SqlParameter("@SCJD100", dr["物控下采购建议单"]),
                                new SqlParameter("@SCJD34", dr["技术部下单期"]),
                                new SqlParameter("@SCJD35", dr["批颜色"]),
                                new SqlParameter("@SCJD36", dr["批产前办"]),
                                new SqlParameter("@SCJD37", dr["提供检测报告日期"]),
                                new SqlParameter("@SCJD38", dr["洗水唛时间"]),
                                new SqlParameter("@SCJD104", dr["贴纸时间"]),
                                new SqlParameter("@SCJD39", dr["物控下单日期"]),
                                new SqlParameter("@SCJD40", dr["齐料日期"]),
                                new SqlParameter("@SCJD102", dr["财务部付款时间"]),
                                new SqlParameter("@SCJD41",dr["外发下单日期"]),
                                new SqlParameter("@SCJD47",dr["产前样提供日期"]),
                                new SqlParameter("@SCJD48",dr["产前OK日期"]),
                                //new SqlParameter("@SCJD42",dr["110/S下单"]),
                                //new SqlParameter("@SCJD43",dr["120/M下单"]),
                                //new SqlParameter("@SCJD44",dr["130/L下单"]),
                                //new SqlParameter("@SCJD45",dr["140/XL下单"]),
                                //new SqlParameter("@SCJD46",dr["150/均码下单"]),
                                new SqlParameter("@SCJD50",dr["110/S实裁"]),
                                new SqlParameter("@SCJD51",dr["120/M实裁"]),
                                new SqlParameter("@SCJD52",dr["130/L实裁"]),
                                new SqlParameter("@SCJD53",dr["140/XL实裁"]),
                                new SqlParameter("@SCJD54",dr["150/均码实裁"]),
                                new SqlParameter("@SCJD55",dr["裁剪差异数"]),
                                new SqlParameter("@SCJD56",dr["差异原因"]),
                                new SqlParameter("@SCJD57",dr["生产异常"]),
                                new SqlParameter("@SCJD67",dr["品控签单日期"]),
                                new SqlParameter("@SCJD106",dr["工厂送货总数"]),
                                new SqlParameter("@SCJD107",dr["工厂送货日期"]),
                                new SqlParameter("@SCJD75",dr["对账时间"]),
                                new SqlParameter("@SCJD86",dr["延期备注"]),
                                new SqlParameter("@SCJD87",dr["尾数状态"]),
                                new SqlParameter("@SCJD49",dr["开裁日期"]),
                                new SqlParameter("@SCJD59",dr["车间上线日期"]),
                                new SqlParameter("@SCJD61",dr["车间下线日期"]),
                                new SqlParameter("@SL",dr["车间成品数量"])
                        };
                    }
                    else if (fName == "PsJobfiled")//生产进度报工
                    {
                        sql = @"update dbo.BPM_SCJDB set SCJD41=@SCJD41,SCJD42=@SCJD42,SCJD43=@SCJD43,SCJD44=@SCJD44,SCJD45=@SCJD45,SCJD46=@SCJD46,SCJD47=@SCJD47,SCJD48=@SCJD48,SCJD50 = @SCJD50,
                                              SCJD51 = @SCJD51,SCJD52 = @SCJD52,SCJD53 = @SCJD53,SCJD54=@SCJD54,SCJD55 = @SCJD55,SCJD56 = @SCJD56,SCJD57 = @SCJD57,
                                              SCJD67 = @SCJD67,SCJD75 = @SCJD75,SCJD86 = @SCJD86,SCJD87 = @SCJD87 where SCJD02 = @SCJD02 and SCJD03 = @SCJD03 and 
                                              SCJD04 = @SCJD04 and SCJD05 = @SCJD05 and SCJD06 = @SCJD06 and SCJD07 = @SCJD07";
                        param = new SqlParameter[]{
                                new SqlParameter("@SCJD02",dr["波段"]),
                                new SqlParameter("@SCJD03",dr["品牌"]),
                                new SqlParameter("@SCJD04",dr["加工方式"]),
                                new SqlParameter("@SCJD05",dr["款号"]),
                                new SqlParameter("@SCJD06",dr["款式"]),
                                new SqlParameter("@SCJD07",dr["颜色"]),
                                new SqlParameter("@SCJD41",dr["外发下单日期"]),
                                new SqlParameter("@SCJD47",dr["产前样提供日期"]),
                                new SqlParameter("@SCJD48",dr["产前OK日期"]),
                                new SqlParameter("@SCJD42",dr["110/S下单"]),
                                new SqlParameter("@SCJD43",dr["120/M下单"]),
                                new SqlParameter("@SCJD44",dr["130/L下单"]),
                                new SqlParameter("@SCJD45",dr["140/XL下单"]),
                                new SqlParameter("@SCJD46",dr["150/均码下单"]),
                                new SqlParameter("@SCJD50",dr["110/S实裁"]),
                                new SqlParameter("@SCJD51",dr["120/M实裁"]),
                                new SqlParameter("@SCJD52",dr["130/L实裁"]),
                                new SqlParameter("@SCJD53",dr["140/XL实裁"]),
                                new SqlParameter("@SCJD54",dr["150/均码实裁"]),
                                new SqlParameter("@SCJD55",dr["裁剪差异数"]),
                                new SqlParameter("@SCJD56",dr["差异原因"]),
                                new SqlParameter("@SCJD57",dr["生产异常"]),
                                new SqlParameter("@SCJD67",dr["工厂送货日期"]),
                                new SqlParameter("@SCJD75",dr["对账时间"]),
                                new SqlParameter("@SCJD86",dr["延期备注"]),
                                new SqlParameter("@SCJD87",dr["尾数状态"])};
                    }

                    else if (fName == "CGfiled")//采购部原料排期
                    {
                        sql = @"update dbo.BPM_SCJDB set SCJD14=@SCJD14,SCJD101=@SCJD101,SCJD32=@SCJD32,SCJD15=@SCJD15,SCJD16=@SCJD16,SCJD90=@SCJD90,SCJD19=@SCJD19,
                                SCJD18 = @SCJD18,SCJD88 = @SCJD88,SCJD33=@SCJD33,SCJD91=@SCJD91,SCJD103=@SCJD103 where SCJD02 = @SCJD02 and SCJD03 = @SCJD03 and SCJD04 = @SCJD04 and SCJD05 = @SCJD05
                                 and SCJD06 = @SCJD06 and SCJD07 = @SCJD07";
                        param = new SqlParameter[]{
                                new SqlParameter("@SCJD02",dr["波段"]),
                                new SqlParameter("@SCJD03",dr["品牌"]),
                                new SqlParameter("@SCJD04",dr["加工方式"]),
                                new SqlParameter("@SCJD05",dr["款号"]),
                                new SqlParameter("@SCJD06",dr["款式"]),
                                new SqlParameter("@SCJD07",dr["颜色"]),
                                new SqlParameter("@SCJD14",dr["面料采购员"]),
                                new SqlParameter("@SCJD103",dr["辅料采购员"]),
                                new SqlParameter("@SCJD101",dr["采购下发合同"]),
                                new SqlParameter("@SCJD32",dr["面料计划交期"]),
                                new SqlParameter("@SCJD15",dr["采购复期"]),
                                new SqlParameter("@SCJD16",dr["面料到仓日期"]),
                                new SqlParameter("@SCJD90",dr["辅料计划交期"]),
                                new SqlParameter("@SCJD19",dr["辅料实际交期"]),
                                new SqlParameter("@SCJD18",dr["采购下单日期"]),
                                new SqlParameter("@SCJD88",dr["面料编号"]),
                                new SqlParameter("@SCJD33",dr["面料验货报告日期"]),
                                new SqlParameter("@SCJD91",dr["辅料验货报告日期"])
                        };
                    }
                    else if (fName == "Pullfiled")//打样进度
                    {
                        sql = @"update dbo.BPM_SCJDB set SCJD25=@SCJD25,SCJD26=@SCJD26,SCJD27=@SCJD27,SCJD28=@SCJD28,SCJD29=@SCJD29,SCJD30=@SCJD30 where 
                                            SCJD02 = @SCJD02 and SCJD03 = @SCJD03 and SCJD04 = @SCJD04 and SCJD05 = @SCJD05 and SCJD06 = @SCJD06 and SCJD07 = @SCJD07";
                        param = new SqlParameter[]{
                                new SqlParameter("@SCJD02",dr["波段"]),
                                new SqlParameter("@SCJD03",dr["品牌"]),
                                new SqlParameter("@SCJD04",dr["加工方式"]),
                                new SqlParameter("@SCJD05",dr["款号"]),
                                new SqlParameter("@SCJD06",dr["款式"]),
                                new SqlParameter("@SCJD07",dr["颜色"]),
                                new SqlParameter("@SCJD25",dr["资料接收日期"]),
                                new SqlParameter("@SCJD26",dr["一次样接收时间"]),
                                new SqlParameter("@SCJD27",dr["二次样下单时间"]),
                                new SqlParameter("@SCJD28",dr["齐色样下单时间"]),
                                new SqlParameter("@SCJD29",dr["成本提供时间"]),
                                new SqlParameter("@SCJD30",dr["齐色面、辅料实样提供时间"])};
                    }

                    else if (fName == "QACheckfiled")//品控质检报工
                    {
                        sql = @"update dbo.BPM_SCJDB set SCJD31=@SCJD31,SCJD58=@SCJD58,SCJD49=@SCJD49,SCJD59=@SCJD59,SCJD60=@SCJD60,
                                                SCJD61 = @SCJD61,SCJD62 = @SCJD62,SCJD63 = @SCJD63,SCJD64 = @SCJD64,SCJD65 = @SCJD65,SCJD92 = @SCJD92 where SCJD02 = @SCJD02 and 
                                                SCJD03 = @SCJD03 and SCJD04 = @SCJD04 and SCJD05 = @SCJD05 and SCJD06 = @SCJD06 and SCJD07 = @SCJD07";
                        param = new SqlParameter[]{
                                new SqlParameter("@SCJD02",dr["波段"]),
                                new SqlParameter("@SCJD03",dr["品牌"]),
                                new SqlParameter("@SCJD04",dr["加工方式"]),
                                new SqlParameter("@SCJD05",dr["款号"]),
                                new SqlParameter("@SCJD06",dr["款式"]),
                                new SqlParameter("@SCJD07",dr["颜色"]),
                                new SqlParameter("@SCJD31",dr["品管人"]),
                                new SqlParameter("@SCJD58",dr["产前会议日期"]),
                                new SqlParameter("@SCJD49",dr["开裁日期"]),
                                new SqlParameter("@SCJD59",dr["车间上线时间"]),
                                new SqlParameter("@SCJD60",dr["中期检验时间"]),
                                new SqlParameter("@SCJD61",dr["车间下线时间"]),
                                new SqlParameter("@SCJD62",dr["尾查一次"]),
                                new SqlParameter("@SCJD63",dr["尾查二次"]),
                                new SqlParameter("@SCJD64",dr["尾查三次"]),
                                new SqlParameter("@SCJD65",dr["尾期检验时间"]),
                                new SqlParameter("@SCJD92",dr["红皮资料日期"]),
                        };
                    }
                    else if (fName == "PTExecl")
                    {
                        BPM_PTReportModels models = new BPM_PTReportModels();
                        models.PTReport30 = Convert.ToInt32(dr["电商"]);
                        models.saleDate = Convert.ToDateTime(dr["日期"]).ToString("yyyy-MM-dd");
                        models.PTReport1 = Convert.ToDecimal(dr["订单数"]);
                        models.PTReport3 = Convert.ToDecimal(dr["销售件数"]);
                        models.PTReport4 = Convert.ToDecimal(dr["销售金额"]);
                        models.PTReport5 = Convert.ToDecimal(dr["退款额"]);
                        models.PTReport13 = Convert.ToDecimal(dr["UV"]);
                        models.PTReport22 = dr["活动备注"].ToString();
                        models.PTReport16 = Convert.ToDecimal(dr["直通车"]);
                        models.PTReport17 = Convert.ToDecimal(dr["钻展"]);
                        models.PTReport18 = Convert.ToDecimal(dr["品销宝"]);
                        models.PTReport6 = (models.PTReport4 - models.PTReport5) == 0 ? 1 : (models.PTReport4 - models.PTReport5);
                        models.PTReport7 = models.PTReport4 * decimal.Parse(0.55.ToString());
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
                        else
                        {
                            models.PTReport11 = models.PTReport9 / models.PTReport6;
                        }


                        if (models.PTReport13 == 0)
                        {
                            models.PTReport14 = 0;
                        }
                        else
                        {
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



                        sql = "delete BPM_PTReport where saleDate=@saleDate;insert into BPM_PTReport(PTReport1,PTReport3,PTReport4,PTReport5,PTReport6,PTReport7,PTReport8,PTReport9,PTReport10,PTReport11,PTReport12,PTReport13,PTReport14,PTReport15,PTReport16,PTReport17,PTReport18,PTReport19,PTReport20,PTReport21,PTReport22,PTReport30,saleDate) values(@PTReport1,@PTReport3,@PTReport4,@PTReport5,@PTReport6,@PTReport7,@PTReport8,@PTReport9,@PTReport10,@PTReport11,@PTReport12,@PTReport13,@PTReport14,@PTReport15,@PTReport16,@PTReport17,@PTReport18,@PTReport19,@PTReport20,@PTReport21,@PTReport22,@PTReport30,@saleDate)";
                        param = new SqlParameter[]
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

                    }
                    SqlHelper.InsertDelUpdate(sql, param);
                }
            }
            catch (Exception ex)
            {
                return "导入失败！";
            }
            return "导入成功";
        }


        /// <summary>
        /// 保存Excel文件
        /// <para>Excel的导入导出都会在服务器生成一个文件</para>
        /// <para>路径：UpFiles/ExcelFiles</para>
        /// </summary>
        /// <param name="file">传入的文件对象</param>
        /// <returns>如果保存成功则返回文件的位置;如果保存失败则返回空</returns>
        public static string SaveExcelFile(HttpPostedFile file)
        {
            try
            {
                var fileName = file.FileName.Insert(file.FileName.LastIndexOf('.'), "-" + DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                var filePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Upload"), fileName);
                string directoryName = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directoryName))
                {
                    Directory.CreateDirectory(directoryName);
                }
                file.SaveAs(filePath);
                return filePath;
            }
            catch
            {
                return string.Empty;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
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
            else
            {
                return 0;
            }
        }
    }
}