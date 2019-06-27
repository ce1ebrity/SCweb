using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NPOI.HSSF.UserModel;
using SCWeb.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCWeb.Controllers
{
    public class MoneyManageController : Controller
    {
        IsoDateTimeConverter timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy" };
        // GET: MoneyManage
        public ActionResult MoneyIndex()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetYCList(string page, string showRows, string where)
        {
            if (string.IsNullOrWhiteSpace(page))
            {
                page = "1";
            }
            string wheres = "";
            if (!string.IsNullOrWhiteSpace(where))
            {
                var whers = where.Split(',');
                for (int i = 0; i < whers.Length; i++)
                {
                    var ty = whers[i].Split('|');
                    wheres += " and " + ty[0] + " like '%" + ty[1] + "%' ";
                }
            }

            string conStr = ConfigurationManager.ConnectionStrings["sqlOA"].ConnectionString;
            string sql = @"SELECT COUNT(1) FROM (select ROW_NUMBER() OVER (ORDER BY a.lcbh) AS RowNumber,a.lcbh as 异常单号, 
            case when bb = '1' then ' BB ' else '' end + case when cc = '1' then ' CC ' else '' end + case when kids = '1' then ' KIDS ' else '' end as 品牌, 
            case when fob = '1' then ' FOB ' else '' end + case when cmt = '1' then ' CMT ' else '' end as 加工方式, 
            jj as 季节,sccj as 生产厂家,kh as 款号,spyq as 商品要求,htqd as 合同签订,bd as 波段,ys as 颜色,xdscl as 下单生产量,
            cm as 尺码,scxdl as 生产下单量,scsl as 实裁数量,zqchl as 正确出货量, ycqksm as 异常情况说明
            from formtable_main_16 a inner join formtable_main_16_dt1 b on a.id = b.mainid) A where 1=1 " + wheres;
            int rowsCount = (int)SqlHelper.SelectSinger(sql, conStr);
            int pageCount = rowsCount % Convert.ToInt32(showRows) == 0 ? rowsCount / Convert.ToInt32(showRows) : rowsCount / Convert.ToInt32(showRows) + 1;
            sql = "SELECT TOP " + showRows + @" *,'' as rowsCount,'' as pageCount FROM (select ROW_NUMBER() OVER (ORDER BY a.lcbh) AS RowNumber,a.lcbh as 异常单号, 
            case when bb = '1' then ' BB ' else '' end + case when cc = '1' then ' CC ' else '' end + case when kids = '1' then ' KIDS ' else '' end as 品牌, 
            case when fob = '1' then ' FOB ' else '' end + case when cmt = '1' then ' CMT ' else '' end as 加工方式, 
            jj as 季节,sccj as 生产厂家,kh as 款号,spyq as 商品要求,htqd as 合同签订,bd as 波段,ys as 颜色,xdscl as 下单生产量,
            cm as 尺码,scxdl as 生产下单量,scsl as 实裁数量,zqchl as 正确出货量, ycqksm as 异常情况说明
            from formtable_main_16 a inner join formtable_main_16_dt1 b on a.id = b.mainid) A WHERE RowNumber > " + showRows + "*(" + page + "-1) " + wheres;

            DataTable dt = SqlHelper.SelectTable(sql, conStr);
            if (dt.Rows.Count > 0)
            {
                dt.Rows[0]["rowsCount"] = rowsCount;
                dt.Rows[0]["pageCount"] = pageCount;
            }
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy'-'MM'-'dd" };
            return Content(JsonConvert.SerializeObject(dt, Formatting.Indented, timeConverter));
        }

        /// <summary>
        /// 扣款管理首页
        /// </summary>
        /// <returns></returns>
        public ActionResult CKIndex(string page)
        {
            if (string.IsNullOrWhiteSpace(page))
            {
                page = "1";
            }
            ViewBag.page = page;
            return View();
        }
        /// <summary>
        /// 扣款管理添加修改
        /// </summary>
        /// <returns></returns>
        public ActionResult CKEdit(string oid, string page)
        {
            ViewBag.oid = oid;
            ViewBag.page = page;
            if (oid != "0" && !string.IsNullOrWhiteSpace(oid))
            {
                DataTable dt = SqlHelper.SelectTable("select * from BPM_CKGL where id=" + oid);
                if (dt.Rows.Count > 0)
                {
                    ViewBag.GYSJ = dt.Rows[0]["GYSJ"].ToString();
                    ViewBag.DJLX = dt.Rows[0]["DJLX"].ToString();
                    ViewBag.DJBH = dt.Rows[0]["DJBH"].ToString();
                    ViewBag.CKYY = dt.Rows[0]["CKYY"].ToString();
                    ViewBag.CKJE = dt.Rows[0]["CKJE"].ToString();
                    ViewBag.GYSM = dt.Rows[0]["GYSM"].ToString();
                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult GetGYSGC03(string GYSGC50)
        {
            string sql = "select isnull(GYSGC03,'') from dbo.BPM_GYSGCGL where GYSGC50='" + GYSGC50 + "'";
            string GYSGC03 = (string)SqlHelper.SelectSinger(sql);
            if (string.IsNullOrWhiteSpace(GYSGC03))
                return Content("NO");
            else
                return Content(GYSGC03);
        }
        
        /// <summary>
        /// 扣款管理列表查询
        /// </summary>
        /// <param name="page">当前页</param>
        /// <param name="showpage">显示页数</param>
        /// <param name="stringWhere">查询条件</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetCKList(string page, string showpage, string stringWhere)
        {
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy'-'MM'-'dd" };
            if (string.IsNullOrWhiteSpace(page))
            {
                page = "1";
            }
            int rowsCount = 0;
            int pageCount = 0;

            string where = " status=1 ";
            if (!string.IsNullOrWhiteSpace(stringWhere))
            {
                var whSum = stringWhere.Split(',');
                for (int i = 0; i < whSum.Length; i++)
                {
                    where += " and " + whSum[i].Split('|')[0] + " like '%" + whSum[i].Split('|')[1] + "%'";
                }
            }
            DataTable dt = Common.GetSQLProcList(out rowsCount, out pageCount, "BPM_CKGL", "*,'' as rowsCount,'' as pageCount ", "id", showpage, page, " id desc ", where);

            if (dt.Rows.Count > 0)
            {
                dt.Rows[0]["rowsCount"] = rowsCount;
                dt.Rows[0]["pageCount"] = pageCount;
            }
            return Content(JsonConvert.SerializeObject(dt, Formatting.Indented, timeConverter));
        }

        /// <summary>
        /// 扣款管理编辑
        /// </summary>
        /// <param name="GYSJ">供应商/加工厂</param>
        /// <param name="DJLX">单据类型</param>
        /// <param name="DJBH">单据编号</param>
        /// <param name="CKYY">扣款原因</param>
        /// <param name="CKJE">扣款金额</param>
        /// <returns></returns>
        [HttpPost]
        [Property(MenuCode = "BPM_CKGL", MenuOperation = "扣款管理操作")]
        public ActionResult SetBpmCKGL(string id, string GYSJ, string DJLX, string DJBH, string CKYY, string CKJE, string GYSM)
        {
            string sql = "";
            if (id == "0")
            {
                sql = "INSERT INTO BPM_CKGL(GYSJ,GYSM,DJLX,DJBH,CKYY,CKJE) VALUES(@GYSJ,@GYSM,@DJLX,@DJBH,@CKYY,@CKJE)";
            }
            else {
                sql = "update BPM_CKGL set GYSJ=@GYSJ,GYSM=@GYSM,DJLX=@DJLX,DJBH=@DJBH,CKYY=@CKYY,CKJE=@CKJE where id=@id";
            }

            int count = SqlHelper.InsertDelUpdate(sql, new SqlParameter("@GYSJ", GYSJ), new SqlParameter("@GYSM", GYSM), new SqlParameter("@DJLX", DJLX), new SqlParameter("@DJBH", DJBH), new SqlParameter("@CKYY", CKYY), new SqlParameter("@CKJE", CKJE), new SqlParameter("@id", id));
            if (count > 0)
                return Content("OK");
            else
                return Content("NO");
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [Property(MenuCode = "BPM_CKGL", MenuOperation = "扣款管理批量删除")]
        public ActionResult SetBpmCKGLDel(string ids)
        {
            string sql = "update BPM_CKGL set status=0 where id in (" + ids + ")";
            int count = SqlHelper.InsertDelUpdate(sql);
            if (count > 0)
                return Content("OK");
            else
                return Content("NO");
        }

        /// <summary>
        ///  扣款管理模板导出
        /// </summary>
        /// <returns></returns>
        public ActionResult GetBpmCKGLDomeExecl()
        {
            return WriteInClient(GetBpmCKGLDataTable());
        }

        /// <summary>
        ///  扣款管理数据导出
        /// </summary>
        /// <returns></returns>
        [Property(MenuCode = "BPM_CKGL", MenuOperation = "扣款管理Excel导出")]
        public ActionResult GetBpmCKGLExecl(string isPot)
        {

            string sql = "";
            if (isPot == "1")
            {
                sql = "select GYSJ as '供应商/加工厂', DJLX as '单据类型', DJBH as '单据编号', CKYY as '扣款原因', CKJE as '扣款金额' from BPM_CKGL where 1!=1";
            }
            else
            {
                sql = "select GYSJ as '供应商/加工厂', DJLX as '单据类型', DJBH as '单据编号', CKYY as '扣款原因', CKJE as '扣款金额' from BPM_CKGL where status=1";
            }
            return WriteInClient(SqlHelper.SelectTable(sql));
        }


        /// <summary>
        ///  扣款管理数据导入
        /// </summary>
        /// <returns></returns>
        [Property(MenuCode = "BPM_CKGL", MenuOperation = "扣款管理Excel导入")]
        public ActionResult SetBpmCKGLExecl(HttpPostedFileBase filed)
        {
            //Common.SaveExcelFile(filed);
            string filePath = SaveAsLoed(filed);
            string sql = "INSERT INTO BPM_CKGL(GYSJ,DJLX,DJBH,CKYY,CKJE) VALUES(@GYSJ,@DJLX,@DJBH,@CKYY,@CKJE)";
            try
            {
                DataTable dt = Common.ExcelToDataTable(filePath, true, 0);
                foreach (DataRow dr in dt.Rows)
                {
                    SqlHelper.InsertDelUpdate(sql, new SqlParameter("@GYSJ", dr["供应商/加工厂"]), new SqlParameter("@DJLX", dr["单据类型"]),new SqlParameter("@DJBH", dr["单据编号"]), new SqlParameter("@CKYY", dr["扣款原因"]), new SqlParameter("@CKJE", dr["扣款金额"]));
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

        /// <summary>
        /// 导出execl
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private ActionResult WriteInClient(DataTable dt)
        {
            HSSFWorkbook book = new HSSFWorkbook();
            HSSFSheet sheet = book.CreateSheet("Sheet1") as HSSFSheet;
            #region sqltoExcel  
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
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.xls", "Excle" + DateTime.Now.ToString("yyyyMMddHHmmssfff")));
            Response.BinaryWrite(ms.ToArray());
            book = null;
            ms.Close();
            ms.Dispose();
            return Content("OK");
        }

        private static DataTable GetBpmCKGLDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("供应商/加工厂");
            dt.Columns.Add("单据类型");
            dt.Columns.Add("单据编号");
            dt.Columns.Add("扣款原因");
            dt.Columns.Add("扣款金额");
            return dt;
        }






        public ActionResult SetBpmFKSQIndex()
        {
            return View();
        }
        public ActionResult Reconciliation()
        {
            return View();
        }


        [HttpPost]
        public ActionResult SetBpmFKSQList1(string page, string showpage, string stringWhere)
        {
            int rowsCount = 0;
            int pageCount = 0;
            string where = " status=1 and FKSQ15=1 ";
            if (!string.IsNullOrWhiteSpace(stringWhere))
            {
                var whSum = stringWhere.Split(',');
                for (int i = 0; i < whSum.Length; i++)
                {
                    where += " and " + whSum[i].Split('|')[0] + " like '%" + whSum[i].Split('|')[1] + "%'";
                }
            }

            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy" };
            DataTable dt = Common.GetSQLProcList(out rowsCount, out pageCount, "BPM_FKSQ", "*,'' as rowsCount,'' as pageCount ", "id", showpage, page, " id desc ", where);
            if (dt.Rows.Count > 0)
            {
                dt.Rows[0]["rowsCount"] = rowsCount;
                dt.Rows[0]["pageCount"] = pageCount;
            }
            return Content(JsonConvert.SerializeObject(dt, Formatting.Indented, timeConverter));
        }

        [HttpPost]
        public ActionResult SetBpmFKSQList2(string page, string showpage, string stringWhere)
        {
            int rowsCount = 0;
            int pageCount = 0;
            string where = " status=1 and FKSQ15=2 ";
            if (!string.IsNullOrWhiteSpace(stringWhere))
            {
                var whSum = stringWhere.Split(',');
                for (int i = 0; i < whSum.Length; i++)
                {
                    where += " and " + whSum[i].Split('|')[0] + " like '%" + whSum[i].Split('|')[1] + "%'";
                }
            }
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy" };
            DataTable dt = Common.GetSQLProcList(out rowsCount, out pageCount, "BPM_FKSQ", "*,'' as rowsCount,'' as pageCount ", "id", showpage, page, " id desc ", where);
            if (dt.Rows.Count > 0)
            {
                dt.Rows[0]["rowsCount"] = rowsCount;
                dt.Rows[0]["pageCount"] = pageCount;
            }
            return Content(JsonConvert.SerializeObject(dt, Formatting.Indented, timeConverter));
        }

        [HttpPost]
        public ActionResult SetBpmFKSQList3(string page, string showpage, string stringWhere)
        {
            int rowsCount = 0;
            int pageCount = 0;
            string where = " status=1 and FKSQ15=3 ";
            if (!string.IsNullOrWhiteSpace(stringWhere))
            {
                var whSum = stringWhere.Split(',');
                for (int i = 0; i < whSum.Length; i++)
                {
                    where += " and " + whSum[i].Split('|')[0] + " like '%" + whSum[i].Split('|')[1] + "%'";
                }
            }
            DataTable dt = Common.GetSQLProcList(out rowsCount, out pageCount, "BPM_FKSQ", "*,'' as rowsCount,'' as pageCount ", "id", showpage, page, " id desc ", where);
            if (dt.Rows.Count > 0)
            {
                dt.Rows[0]["rowsCount"] = rowsCount;
                dt.Rows[0]["pageCount"] = pageCount;
            }
            return Content(JsonConvert.SerializeObject(dt, Formatting.Indented, timeConverter));
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [Property(MenuCode = "BPM_FKSQ", MenuOperation = "付款申请批量删除")]
        public ActionResult SetBpmFKSQDel(string ids)
        {
            string sql = "update BPM_FKSQ set status=0 where id in (" + ids + ")";
            int count = SqlHelper.InsertDelUpdate(sql);
            if (count > 0)
                return Content("OK");
            else
                return Content("NO");
        }
        /// <summary>
        /// 批量审批成功
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [Property(MenuCode = "BPM_FKSQ", MenuOperation = "付款申请审批")]
        public ActionResult SetBpmFKSQYes(string ids)
        {
            string sql = "update BPM_FKSQ set FKSQ14='已通过审核' where id in (" + ids + ")";
            int count = SqlHelper.InsertDelUpdate(sql);
            if (count > 0)
                return Content("OK");
            else
                return Content("NO");
        }

        /// <summary>
        /// 批量审批失败
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [Property(MenuCode = "BPM_FKSQ", MenuOperation = "付款申请审批")]
        public ActionResult SetBpmFKSQNo(string ids)
        {
            string sql = "update BPM_FKSQ set FKSQ14='未通过审核' where id in (" + ids + ")";
            int count = SqlHelper.InsertDelUpdate(sql);
            if (count > 0)
                return Content("OK");
            else
                return Content("NO");
        }

        /// <summary>
        /// 付款申请修改
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult SetBpmFKSQ(string id, string FKSQ12, string FKSQ13)
        {
            string sql = "update BPM_FKSQ set FKSQ12=@FKSQ12,FKSQ13=@FKSQ13 where id=@id";
            int count = SqlHelper.InsertDelUpdate(sql, new SqlParameter("@FKSQ12", FKSQ12),
                new SqlParameter("@FKSQ13", FKSQ13), new SqlParameter("@id", id));
            if (count > 0)
                return Content("OK");
            else
                return Content("NO");
        }
        /// <summary>
        /// 获得面料合同明细
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult GetMianLiao(string DJBH)
        {
            string sql = @"SELECT  MLJRD.DJBH AS 单据编号,mianliao.MLMC AS 面料编号, MLJRDmx.mldm AS 面料名称,mljrd.YDJH AS 合同号,
                            mljrd.dm1 AS 供应商代码,GONGHUOSHANG.GHSMC AS 供应商名称,
                            sum(mljrdmx.sl) AS 合同数量, sum(mljrdmx.je) AS 合同金额 ,mljrdmx.dj AS 合同单价
                            FROM MLJRDmx
                            INNER JOIN MLJRD ON MLJRD.DJBH=MLJRDmx.DJBH
                            INNER JOIN mianliao  ON mianliao.MLDM=mljrdmx.MLDM
                            INNER JOIN GONGHUOSHANG ON GONGHUOSHANG.GHSDM=mljrd.dm1
                            WHERE MLJRD.rq>='2016-01-01' and MLJRD.DJBH='" + DJBH + "'";


            string sqls = "select count(1) from BPM_FKSQ where FKSQ02='" + DJBH + "' and status=1";
            if ((int)SqlHelper.SelectSinger(sqls) > 0)
                sql += " and 1!=1";

            sql += " group by MLJRD.DJBH,mianliao.MLMC, MLJRDmx.mldm, mljrd.YDJH, mljrd.dm1, GONGHUOSHANG.GHSMC,mljrdmx.dj";
            DataTable dt = SqlHelper.SelectTable(sql);
            return Content(JsonConvert.SerializeObject(dt, Formatting.Indented, timeConverter));
        }

        /// <summary>
        /// 获得辅料名称
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult GetFuLiao(string DJBH)
        {
            string sql = @"select FLMC from FULIAO where FLDM = '" + DJBH + "'";
            DataTable dt = SqlHelper.SelectTable(sql);
            return Content(JsonConvert.SerializeObject(dt, Formatting.Indented, timeConverter));
        }

        /// <summary>
        /// 获得加工合同明细
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult GetJiaGong(string DJBH)
        {
            string sql = @" SELECT  spjhd.DJBH AS 单据编号,
                            spjhdmx.spdm AS 款式编号, shangpin.spmc AS 款式名称,guige1.GGmc AS 颜色,
                            sum(spjhdmx.sl) AS 数量,
                            '' AS 合同号,spjhd.dm1 AS 供应商代码,GONGCHANG.GCMC AS 供应商名称,
                            sum(spjhdmx.dj) AS 合同单价, sum(spjhdmx.je) AS 合同金额 
                            FROM spjhdmx
                            INNER JOIN spjhd ON spjhd.DJBH=spjhdmx.DJBH
                            INNER JOIN shangpin  ON shangpin.spdm=spjhdmx.spdm
                            INNER JOIN dbo.GUIGE1 ON spjhdmx.GG1DM=guige1.GGDM
                            INNER JOIN GONGCHANG ON GONGCHANG.GCDM=spjhd.dm1
                            WHERE spjhd.rq>='2016-01-01' and spjhd.DJBH = '" + DJBH + "' group by spjhd.DJBH,spjhdmx.spdm,shangpin.spmc,guige1.GGmc,spjhd.dm1,GONGCHANG.GCMC";
            DataTable dt = SqlHelper.SelectTable(sql);
            return Content(JsonConvert.SerializeObject(dt, Formatting.Indented, timeConverter));
        }

        /// <summary>
        ///  付款管理数据导出
        /// </summary>
        /// <returns></returns>
        public ActionResult GetBpmFKSQExecl1()
        {
            string sql = "select FKSQ01 as 面料付款单号,FKSQ02 as 单据编号,FKSQ03 as 面料编号,FKSQ04 as 面料名称,FKSQ07 as 合同号,FKSQ05 as 供应商代码,FKSQ06 as 供应商名称,FKSQ09 as 合同数量,FKSQ10 as 合同单价,FKSQ11 as 合同金额,FKSQ12 as 付款比例,FKSQ13 as 付款金额 from BPM_FKSQ where status=1 and FKSQ15=1";
            return WriteInClient(SqlHelper.SelectTable(sql));
        }
        /// <summary>
        ///  付款管理数据导出
        /// </summary>
        /// <returns></returns>
        public ActionResult GetBpmFKSQExecl2()
        {
            string sql = "select FKSQ01 as 辅料付款单号,FKSQ02 as 合同编号,FKSQ05 as 供应商代码,FKSQ06 as 供应商名称,FKSQ03 as 辅料编号,FKSQ04 as 辅料名称,FKSQ09 as 合同数量,FKSQ10 as 合同单价,FKSQ11 as 合同金额,FKSQ12 as 付款比例,FKSQ13 as 付款金额 from BPM_FKSQ where status=1 and FKSQ15=2";
            return WriteInClient(SqlHelper.SelectTable(sql));
        }
        /// <summary>                                        
        ///  付款管理数据导出                                
        /// </summary>                                       
        /// <returns></returns>                              
        public ActionResult GetBpmFKSQExecl3()
        {
            string sql = "select FKSQ01 as 加工厂付款单号,FKSQ02 as 单据编号,FKSQ03 as 款式编号,FKSQ04 as 款式名称,FKSQ08 as 颜色,FKSQ09 as 数量,FKSQ07 as合同号 ,FKSQ05 as 供应商代码,FKSQ06 as 供应商名称,FKSQ10 as 合同单价,FKSQ11 as 合同金额,FKSQ12 as 付款比例,FKSQ13 as 付款金额 from BPM_FKSQ where status=1 and FKSQ15=3";
            return WriteInClient(SqlHelper.SelectTable(sql));
        }


        public ActionResult SetBpmFKSQEdit(string oid)
        {
            if (string.IsNullOrWhiteSpace(oid))
            {
                oid = "1";
            }
            ViewBag.oid = oid;
            return View();
        }



        public ActionResult SetBpmCWDZIndex(string page)
        {
            if (string.IsNullOrWhiteSpace(page))
            {
                page = "1";
            }
            ViewBag.page = page;
            return View();
        }

        public ActionResult SetBpmCWDZEdit(string oid)
        {
            if (string.IsNullOrWhiteSpace(oid))
            {
                oid = "0";
                ViewBag.startDate = DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(-1).ToString("yyyy-MM-dd");
                ViewBag.endDate = DateTime.Now.AddDays(1 - DateTime.Now.Day).AddDays(-1).ToString("yyyy-MM-dd");
                ViewBag.CWDZ04 = "";
                ViewBag.CWDZ05 = "";
            }
            else
            {
                string sql = "select * from BPM_CWDZ where id=" + oid;
                DataTable dt = SqlHelper.SelectTable(sql);
                if (dt.Rows.Count > 0)
                {
                    ViewBag.startDate = Convert.ToDateTime(dt.Rows[0]["CWDZ01"]).ToString("yyyy-MM-dd");
                    ViewBag.endDate = Convert.ToDateTime(dt.Rows[0]["CWDZ02"]).ToString("yyyy-MM-dd");
                    ViewBag.CWDZ03 = dt.Rows[0]["CWDZ03"];
                    ViewBag.CWDZ04 = dt.Rows[0]["CWDZ04"];
                    ViewBag.CWDZ05 = dt.Rows[0]["CWDZ05"];
                }
            }

            ViewBag.oid = oid;
            return View();
        }


        public ActionResult SetBpmCWDZEditAjax(string Tvalues, string FKSQ15)
        {

            if (string.IsNullOrWhiteSpace(Tvalues) || string.IsNullOrWhiteSpace(FKSQ15))
            {
                return Content("数据有误！");
            }
            else
            {
                string sql = "";
                string[] valueRows = Tvalues.Split('|');
                if (FKSQ15 == "1")
                {
                    sql = "insert into BPM_FKSQ (FKSQ01,FKSQ02,FKSQ03,FKSQ04,FKSQ07,FKSQ05,FKSQ06,FKSQ09,FKSQ10,FKSQ11,FKSQ12,FKSQ13,FKSQ14,FKSQ15) values(@FKSQ01,@FKSQ02,@FKSQ03,@FKSQ04,@FKSQ07,@FKSQ05,@FKSQ06,@FKSQ09,@FKSQ10,@FKSQ11,@FKSQ12,@FKSQ13,@FKSQ14,@FKSQ15)";
                    for (int i = 0; i < valueRows.Length; i++)
                    {
                        SqlParameter[] para = new SqlParameter[] {
                            new SqlParameter("@FKSQ01",GetDH(FKSQ15,GetFKSQ01(FKSQ15))),
                            new SqlParameter("@FKSQ02",valueRows[i].Split(',')[0]),
                            new SqlParameter("@FKSQ03",valueRows[i].Split(',')[1]),
                            new SqlParameter("@FKSQ04",valueRows[i].Split(',')[2]),
                            new SqlParameter("@FKSQ07",valueRows[i].Split(',')[3]),
                            new SqlParameter("@FKSQ05",valueRows[i].Split(',')[4]),
                            new SqlParameter("@FKSQ06",valueRows[i].Split(',')[5]),
                            new SqlParameter("@FKSQ09",valueRows[i].Split(',')[6]),
                            new SqlParameter("@FKSQ10",valueRows[i].Split(',')[7]),
                            new SqlParameter("@FKSQ11",valueRows[i].Split(',')[8]),
                            new SqlParameter("@FKSQ12",valueRows[i].Split(',')[9]==""?"0":valueRows[i].Split(',')[9]),
                            new SqlParameter("@FKSQ13",valueRows[i].Split(',')[10]==""?"0":valueRows[i].Split(',')[10]),
                            new SqlParameter("@FKSQ14","未审核"),
                            new SqlParameter("@FKSQ15",FKSQ15)};
                        SqlHelper.InsertDelUpdate(sql, para);
                    }

                }
                else if (FKSQ15 == "2")
                {

                    sql = "insert into BPM_FKSQ (FKSQ01,FKSQ02,FKSQ05,FKSQ06,FKSQ03,FKSQ04,FKSQ09,FKSQ10,FKSQ11,FKSQ12,FKSQ13,FKSQ14,FKSQ15) values(@FKSQ01,@FKSQ02,@FKSQ05,@FKSQ06,@FKSQ03,@FKSQ04,@FKSQ09,@FKSQ10,@FKSQ11,@FKSQ12,@FKSQ13,@FKSQ14,@FKSQ15)";
                    for (int i = 0; i < valueRows.Length; i++)
                    {
                        SqlParameter[] para = new SqlParameter[] {
                            new SqlParameter("@FKSQ01",GetDH(FKSQ15,GetFKSQ01(FKSQ15))),
                            new SqlParameter("@FKSQ02",valueRows[i].Split(',')[0]),
                            new SqlParameter("@FKSQ05",valueRows[i].Split(',')[1]),
                            new SqlParameter("@FKSQ06",valueRows[i].Split(',')[2]),
                            new SqlParameter("@FKSQ03",valueRows[i].Split(',')[3]),
                            new SqlParameter("@FKSQ04",valueRows[i].Split(',')[4]),
                            new SqlParameter("@FKSQ09",valueRows[i].Split(',')[5]),
                            new SqlParameter("@FKSQ10",valueRows[i].Split(',')[6]),
                            new SqlParameter("@FKSQ11",valueRows[i].Split(',')[7]),
                            new SqlParameter("@FKSQ12",valueRows[i].Split(',')[8]==""?"0":valueRows[i].Split(',')[8]),
                            new SqlParameter("@FKSQ13",valueRows[i].Split(',')[9]==""?"0":valueRows[i].Split(',')[9]),
                            new SqlParameter("@FKSQ14","未审核"),
                            new SqlParameter("@FKSQ15",FKSQ15)};
                        SqlHelper.InsertDelUpdate(sql, para);
                    }
                }
                else if (FKSQ15 == "3")
                {
                    sql = "insert into BPM_FKSQ (FKSQ01,FKSQ02,FKSQ03,FKSQ04,FKSQ08,FKSQ10,FKSQ09,FKSQ07,FKSQ05,FKSQ06,FKSQ11,FKSQ12,FKSQ13,FKSQ14,FKSQ15) values(@FKSQ01,@FKSQ02,@FKSQ03,@FKSQ04,@FKSQ08,@FKSQ10,@FKSQ09,@FKSQ07,@FKSQ05,@FKSQ06,@FKSQ11,@FKSQ12,@FKSQ13,@FKSQ14,@FKSQ15)";
                    for (int i = 0; i < valueRows.Length; i++)
                    {
                        SqlParameter[] para = new SqlParameter[] {
                            new SqlParameter("@FKSQ01",GetDH(FKSQ15,GetFKSQ01(FKSQ15))),
                            new SqlParameter("@FKSQ02",valueRows[i].Split(',')[0]),
                            new SqlParameter("@FKSQ03",valueRows[i].Split(',')[1]),
                            new SqlParameter("@FKSQ04",valueRows[i].Split(',')[2]),
                            new SqlParameter("@FKSQ08",valueRows[i].Split(',')[3]),
                            new SqlParameter("@FKSQ09",valueRows[i].Split(',')[4]),
                            new SqlParameter("@FKSQ07",valueRows[i].Split(',')[5]),
                            new SqlParameter("@FKSQ05",valueRows[i].Split(',')[6]),
                            new SqlParameter("@FKSQ06",valueRows[i].Split(',')[7]),
                            new SqlParameter("@FKSQ10",valueRows[i].Split(',')[8]),
                            new SqlParameter("@FKSQ11",valueRows[i].Split(',')[9]),
                            new SqlParameter("@FKSQ12",valueRows[i].Split(',')[10]==""?"0":valueRows[i].Split(',')[10]),
                            new SqlParameter("@FKSQ13",valueRows[i].Split(',')[11]==""?"0":valueRows[i].Split(',')[11]),
                            new SqlParameter("@FKSQ14","未审核"),
                            new SqlParameter("@FKSQ15",FKSQ15)};
                        SqlHelper.InsertDelUpdate(sql, para);
                    }
                }
                else {
                    return Content("数据有误！");
                }
            }
            return Content("添加成功");
        }
        public string GetDH(string type, string maxDH)
        {
            string retDJBH = "";
            string djbh = "";

            if (type == "1")
            {
                djbh = "ML" + DateTime.Now.ToString("yyyyMMdd");
            }
            else if (type == "2")
            {
                djbh = "FL" + DateTime.Now.ToString("yyyyMMdd");
            }
            else
            {
                djbh = "JG" + DateTime.Now.ToString("yyyyMMdd");
            }
            if (maxDH == "" || maxDH.Substring(2, 8) != DateTime.Now.ToString("yyyyMMdd"))
            {
                retDJBH = djbh + "0001";
            }
            else
            {
                int sumMin = Convert.ToInt32(maxDH.Substring(maxDH.Length - 4, 4));
                sumMin++;
                if (sumMin.ToString().Length == 1)
                {
                    retDJBH = djbh + "000" + sumMin;
                }
                else if (sumMin.ToString().Length == 2)
                {
                    retDJBH = djbh + "00" + sumMin;
                }
                else if (sumMin.ToString().Length == 3)
                {
                    retDJBH = djbh + "0" + sumMin;
                }
                else if (sumMin.ToString().Length == 4)
                {
                    retDJBH = djbh + sumMin;
                }

            }
            return retDJBH;
        }
        public string GetFKSQ01(string type)
        {
            string djbh = "";
            if (type == "1")
            {
                djbh = "ML";
            }
            else if (type == "2")
            {
                djbh = "FL";
            }
            else
            {
                djbh = "JG";
            }
            string sql = "SELECT isnull(max(FKSQ01),'') from dbo.BPM_FKSQ where FKSQ01 like '" + djbh + "%' ";
            string pp = (string)SqlHelper.SelectSinger(sql);
            return string.IsNullOrWhiteSpace(pp) ? "" : pp;
        }

        /// <summary>
        /// 财务对账列表查询
        /// </summary>
        /// <param name="page">当前页</param>
        /// <param name="showpage">显示页数</param>
        /// <param name="stringWhere">查询条件</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetCWDZLists(string page, string showpage, string stringWhere)
        {
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy'-'MM'-'dd" };
            if (string.IsNullOrWhiteSpace(page))
            {
                page = "1";
            }
            int rowsCount = 0;
            int pageCount = 0;
            stringWhere = " status=1 ";
            DataTable dt = Common.GetSQLProcList(out rowsCount, out pageCount, "BPM_CWDZ", "*,'' as rowsCount,'' as pageCount ", "id", showpage, page, " id desc ", stringWhere);

            if (dt.Rows.Count > 0)
            {
                dt.Rows[0]["rowsCount"] = rowsCount;
                dt.Rows[0]["pageCount"] = pageCount;
            }
            return Content(JsonConvert.SerializeObject(dt, Formatting.Indented, timeConverter));
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult SetBpmCWDZDel(string ids)
        {
            string sql = "update BPM_CWDZ set status=0 where id in (" + ids + ")";
            int count = SqlHelper.InsertDelUpdate(sql);
            if (count > 0)
                return Content("OK");
            else
                return Content("NO");
        }

        /// <summary>
        /// 供应商查询
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetGYSList(string GYSGC50, string GYSGC03)
        {
            string sql = "select GYSGC03,GYSGC50 from dbo.BPM_GYSGCGL WHERE status=1 and GYSGC50 like '%" + GYSGC50 + "%' and GYSGC03 like '%" + GYSGC03 + "%'";
            DataTable dt = SqlHelper.SelectTable(sql);
            return Content(JsonConvert.SerializeObject(dt, Formatting.Indented, timeConverter));
        }
        /// <summary>
        /// 供应商查询
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetCWDZList(string startDate, string endDate, string yelx, string GYSGC50, string GYSGC03)
        {
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy'-'MM'-'dd" };
            string sql = "select *,'' as 统计 from dbo.view_BPM_CWDZS where 1=1 ";
            string sqlSumRK = "select isnull(sum(金额),0) from dbo.view_BPM_CWDZS where 1=1 ";
            string sqlSumKK = "select isnull(sum(金额),0) from dbo.view_BPM_CWDZS where 1=1 ";
            string sqlSumFK = "select isnull(sum(金额),0) from dbo.view_BPM_CWDZS where 1=1 ";
            if (!string.IsNullOrWhiteSpace(startDate))
            {
                sql += " and 交易时间 >= '" + startDate + "' ";
                sqlSumRK += " and 交易时间 >= '" + startDate + "' ";
                sqlSumKK += " and 交易时间 >= '" + startDate + "' ";
                sqlSumFK += " and 交易时间 >= '" + startDate + "' ";
            }
            if (!string.IsNullOrWhiteSpace(endDate))
            {
                sql += " and 交易时间 <= '" + endDate + "' ";
                sqlSumRK += " and 交易时间 <= '" + endDate + "' ";
                sqlSumKK += " and 交易时间 <= '" + endDate + "' ";
                sqlSumFK += " and 交易时间 <= '" + endDate + "' ";
            }
            if (!string.IsNullOrWhiteSpace(yelx))
            {
                if (yelx == "加工费用")
                {
                    sql += " and 交易类型 in ('进货单','加工厂付款单','成品进货扣款单')";
                    sqlSumRK += " and 交易类型 in ('进货单','加工厂付款单','成品进货扣款单') and 交易类型 like '%进货单%' ";
                    sqlSumKK += " and 交易类型 in ('进货单','加工厂付款单','成品进货扣款单') and 交易类型 like '%扣款单%' ";
                    sqlSumFK += " and 交易类型 in ('进货单','加工厂付款单','成品进货扣款单') and 交易类型 like '%付款单%' ";
                }
                else
                {
                    //原料费用
                    sql += " and 交易类型 not in ('进货单','加工厂付款单','成品进货扣款单')";
                    sqlSumRK += " and 交易类型 not in ('进货单','加工厂付款单','成品进货扣款单') and 交易类型 like '%进货单%' ";
                    sqlSumKK += " and 交易类型 not in ('进货单','加工厂付款单','成品进货扣款单') and 交易类型 like '%扣款单%' ";
                    sqlSumFK += " and 交易类型 not in ('进货单','加工厂付款单','成品进货扣款单') and 交易类型 like '%付款单%' ";
                }
            }
            if (!string.IsNullOrWhiteSpace(GYSGC50))
            {
                sql += " and 供应商代码 = '" + GYSGC50 + "' ";
                sqlSumRK += " and 供应商代码 = '" + GYSGC50 + "' ";
                sqlSumKK += " and 供应商代码 = '" + GYSGC50 + "' ";
                sqlSumFK += " and 供应商代码 = '" + GYSGC50 + "' ";
            }
            if (!string.IsNullOrWhiteSpace(GYSGC03))
            {
                sql += " and 供应商全称 = '" + GYSGC03 + "' ";
                sqlSumRK += " and 供应商全称 = '" + GYSGC03 + "' ";
                sqlSumKK += " and 供应商全称 = '" + GYSGC03 + "' ";
                sqlSumFK += " and 供应商全称 = '" + GYSGC03 + "' ";
            }
            DataTable dt = SqlHelper.SelectTable(sql);
            var sumRk = Convert.ToDecimal(SqlHelper.SelectSinger(sqlSumRK));
            var sumKK = Convert.ToDecimal(SqlHelper.SelectSinger(sqlSumKK));
            var sumFK = Convert.ToDecimal(SqlHelper.SelectSinger(sqlSumFK));
            var QM = sumRk - sumKK - sumFK;
            if (dt.Rows.Count > 0)
                dt.Rows[0]["统计"] = sumRk + "|" + sumKK + "|" + sumFK + "|" + QM;
            return Content(JsonConvert.SerializeObject(dt, Formatting.Indented, timeConverter));
        }
        /// <summary>
        /// 供应商保存
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetCWDZEditAjax(string startDate, string endDate, string yelx, string GYSGC50, string GYSGC03, string oid, string jine1, string jine2, string jine3, string jine4)
        {
            string sql = "";
            if (oid == "0")
            {
                sql = @"INSERT INTO BPM_CWDZ(CWDZ01, CWDZ02, CWDZ03, CWDZ04, CWDZ05, CWDZ06, CWDZ07, CWDZ08, CWDZ09, CWDZ10)VALUES(@CWDZ01, @CWDZ02, @CWDZ03, @CWDZ04, @CWDZ05, @CWDZ06, @CWDZ07, @CWDZ08, @CWDZ09, @CWDZ10)";
            }
            else
            {
                sql = "update BPM_CWDZ set CWDZ01=@CWDZ01, CWDZ02=@CWDZ02, CWDZ03=@CWDZ03, CWDZ04=@CWDZ04, CWDZ05=@CWDZ05, CWDZ06=@CWDZ06, CWDZ07=@CWDZ07, CWDZ08=@CWDZ08, CWDZ09=@CWDZ09, CWDZ10=@CWDZ10 where id=@id";
            }
            SqlParameter[] para = new SqlParameter[] {
                new SqlParameter("@id",oid),
                new SqlParameter("@CWDZ01",startDate),
                new SqlParameter("@CWDZ02",endDate),
                new SqlParameter("@CWDZ03",yelx),
                new SqlParameter("@CWDZ04",GYSGC50),
                new SqlParameter("@CWDZ05",GYSGC03),
                new SqlParameter("@CWDZ06","0"),
                new SqlParameter("@CWDZ07",jine1),
                new SqlParameter("@CWDZ08",jine2),
                new SqlParameter("@CWDZ09",jine3),
                new SqlParameter("@CWDZ10",jine4)};

            int count = SqlHelper.InsertDelUpdate(sql, para);
            if (count > 0)
                return Content("OK");
            else
                return Content("NO");
        }
    }
}