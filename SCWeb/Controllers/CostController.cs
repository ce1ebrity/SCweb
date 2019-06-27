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

namespace SCWeb.Controllers
{
    public class CostController : Controller
    {
        DataTable dt = null;
        IsoDateTimeConverter timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy'-'MM'-'dd" };
        /// <summary>
        /// 成本核算列表
        /// </summary>
        /// <returns></returns>
        public ActionResult CostIndex()
        {
            return View();
        }


        public ActionResult CostListIndex(string page, string showpage, string stringWhere)
        {
            int rowsCount = 0, pageCount = 0;
            string where = " status<>'0' ";
            if (!string.IsNullOrWhiteSpace(stringWhere))
            {
                var whList = stringWhere.Split('|');
                for (int i = 0; i < whList.Length; i++)
                {
                    var list = whList[i].Split(',');
                    where += " and " + list[0] + " like '%" + list[1] + "%'";
                }
            }

            DataTable dt = Common.GetSQLProcList(out rowsCount, out pageCount, "BPM_COSTADJUST", " * ", "id", showpage, page, " id desc ", where);
            if (dt.Rows.Count > 0)
            {
                dt.Rows[0]["rowsCount"] = rowsCount;
                dt.Rows[0]["pageCount"] = pageCount;
            }
            return Content(JsonConvert.SerializeObject(dt, Formatting.Indented, timeConverter));
        }

        /// <summary>
        /// 成本核算明细
        /// </summary>
        /// <returns></returns>
        public ActionResult CostAddUpdate(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                id = "0";
            }
            ViewBag.id = id;
            return View();
        }

        public ActionResult CostPList(string topid)
        {
            string COST01 = "";//BOM单号
            string COST07 = "";//颜色
            string COST15 = "";//目标成本
            string sql = "select COST01,COST07,COST15 from BPM_COSTADJUST where id=" + topid;
            dt = SqlHelper.SelectTable(sql);
            if (dt.Rows.Count > 0)
            {

                COST01 = dt.Rows[0]["COST01"].ToString();
                COST07 = dt.Rows[0]["COST07"].ToString();
                COST15 = dt.Rows[0]["COST15"].ToString();
                sql = "select a.Sample01 as 'BOM单号',a.Sample06 as '设计款号',a.Sample07 as '款式描述',a.Sample16 as '图片', b.SampleMX01 as '项目',b.SampleMX18 as '供应商',b.SampleMX19 as '物料编号',b.SampleMX04 as '色号',b.SampleMX05 as '物料名称',b.SampleMX06 as '物料规格',b.SampleMX09 as '单位',b.SampleMX11 as '使用部位',b.SampleMX12 as '单件用量',b.SampleMX13 as '损耗(%)',b.SampleMX15 as '单价(元)',b.SampleMX16 as '金额(元)',b.SampleMX07 as '颜色',0 as '目标成本', ";
                if (COST01.Length > 0)
                {
                    string str = COST01.Substring(0, 10);
                    if (str == "BOMQDBPMYY")
                    {
                        sql += @" '样衣BOM' as 'BOM单类型' from BPM_SampleList a inner join BPM_SampleMX b on a.id =b.Sampleid where a.Sample01 = '" + COST01 + "' and b.SampleMX07 in ('" + COST07 + "','')";
                    }
                    else {
                        sql += @" '大货BOM' as 'BOM单类型' from BPM_CargoList a inner join BPM_CargoMX b on a.id =b.Sampleid where a.Sample01 = '" + COST01 + "' and b.SampleMX07 in ('" + COST07 + "','')";
                    }
                    dt = SqlHelper.SelectTable(sql);
                    if (dt.Rows.Count > 0) {
                        dt.Rows[0]["目标成本"]= COST15;
                    }
                    return Content(JsonConvert.SerializeObject(dt));
                }
                else {
                    return Content("NO");
                }
            }
            else {
                return Content("NO");
            }
        }

        public ActionResult CostP2List(string topid)
        {
            string sql = "select * from BPM_COSTOTHER where status=1 and COSTID=" + topid;
            dt = SqlHelper.SelectTable(sql);
            return Content(JsonConvert.SerializeObject(dt));
        }

        /// <summary>
        /// 成本核算打印表单
        /// </summary>
        /// <returns></returns>
        public ActionResult CostAddFrom(string id)
        {
            ViewBag.id = id;

            return View();
        }

        /// <summary>
        /// 成本核算 BOM查询
        /// </summary>
        /// <param name="COST02">BOM单类型</param>
        /// <param name="COST06">设计款号</param>
        /// <param name="COST07">颜色</param>
        /// <returns></returns>
        public ActionResult CostMainList(string str)
        {
            string sql = "";
            dt = SqlHelper.SelectTable(sql);
            return Content(JsonConvert.SerializeObject(dt));
        }

        /// <summary>
        /// 成本核算 颜色下拉
        /// </summary>
        /// <returns></returns>
        public ActionResult CostYSList()
        {
            string sql = "select SampleMX07 from BPM_SampleMX group by SampleMX07";
            dt = SqlHelper.SelectTable(sql);
            return Content(JsonConvert.SerializeObject(dt));
        }

        /// <summary>
        /// 成本核算 BOM查询
        /// </summary>
        /// <param name="COST02">BOM单类型</param>
        /// <param name="COST06">设计款号</param>
        /// <param name="COST07">颜色</param>
        /// <returns></returns>
        public ActionResult CostBOMList(string COST02, string COST06, string COST07, string indexs, string COST01)
        {
            bool returns = true;
            string sql = "select a.Sample01 as 'BOM单号',a.Sample06 as '设计款号',a.Sample07 as '款式描述',a.Sample16 as '图片', b.SampleMX01 as '项目',b.SampleMX18 as '供应商',b.SampleMX19 as '物料编号',b.SampleMX04 as '色号',b.SampleMX05 as '物料名称',b.SampleMX06 as '物料规格',b.SampleMX09 as '单位',b.SampleMX11 as '使用部位',b.SampleMX12 as '单件用量',b.SampleMX13 as '损耗(%)',b.SampleMX15 as '单价(元)',b.SampleMX16 as '金额(元)',b.SampleMX07 as '颜色', ";
            if (!string.IsNullOrWhiteSpace(COST01))
            {
                string str = COST01.Substring(0, 10);
                if (str == "BOMQDBPMYY")
                {
                    sql += " '样衣BOM' as 'BOM单类型' from BPM_SampleList a inner join BPM_SampleMX b on a.id =b.Sampleid where b.SampleMX07 in ('" + COST07 + "','') and a.Sample06 like '%" + COST06 + "%' and  a.Sample01 like '%" + COST01 + "%' and b.status=1 ";
                }
                else
                {
                    sql += " '大货BOM' as 'BOM单类型' from BPM_CargoList a inner join BPM_CargoMX b on a.id =b.Sampleid where b.SampleMX07 in ('" + COST07 + "','') and a.Sample06 like '%" + COST06 + "%' and  a.Sample01 like '%" + COST01 + "%' and b.status=1 ";
                }
            }
            else
            {
                if (COST02 == "" || COST06 == "") {
                    sql += " '大货BOM' as 'BOM单类型' from BPM_CargoList a inner join BPM_CargoMX b on a.id =b.Sampleid where 1!=1";
                }
                else if (COST02 == "样衣BOM")
                {
                    sql += " '样衣BOM' as 'BOM单类型' from BPM_SampleList a inner join BPM_SampleMX b on a.id =b.Sampleid where b.SampleMX07 in ('" + COST07 + "','')  and a.Sample06 = '" + COST06 + "' and  a.Sample01 like '%" + COST01 + "%'  and b.status=1 ";
                }
                else {
                    sql += " '大货BOM' as 'BOM单类型' from BPM_CargoList a inner join BPM_CargoMX b on a.id =b.Sampleid where b.SampleMX07 in ('" + COST07 + "','') and a.Sample06 = '" + COST06 + "' and  a.Sample01 like '%" + COST01 + "%' and b.status=1 ";
                }
            }

            //if (!string.IsNullOrWhiteSpace(COST07)) {
            //    sql += " and b.SampleMX07=''";
            //}
            if (returns)
                dt = SqlHelper.SelectTable(sql);
            else
                dt = null;
            return Content(JsonConvert.SerializeObject(dt));
        }
        
        /// <summary>
        /// 成本核算 BOM修改
        /// </summary>
        /// <returns></returns>
        public ActionResult Costedit(string id, string delid, string COST01, string COST02, string COST07, string COST09, string COST10, string COST11, string COST12, string COST13, string COST14, string COST15, string COSTOTHER)
        {
            int Zid = 0;
            int count = 0;
            string sql = "";
            if (COST02 == "样衣BOM")
            {
                sql = "select * from BPM_SampleList where Sample01 = '" + COST01 + "'";
            }
            else
            {
                sql = "select * from BPM_CargoList where Sample01 = '" + COST01 + "'";
            }
            dt = SqlHelper.SelectTable(sql);
            if (dt.Rows.Count > 0)
            {
                SqlParameter[] para = null;
                if (id == "0")
                {
                    //增加
                    sql = "insert into BPM_COSTADJUST (COST01,COST02,COST03,COST04,COST05,COST06,COST07,COST08,COST09,COST10,COST11,COST12,COST13,COST14,COST15) values(@COST01,@COST02,@COST03,@COST04,@COST05,@COST06,@COST07,@COST08,@COST09,@COST10,@COST11,@COST12,@COST13,@COST14,@COST15); select @topid=@@identity";
                    para = new SqlParameter[] {
                    new SqlParameter("@COST01",COST01),
                    new SqlParameter("@COST02",COST02),
                    new SqlParameter("@COST03",dt.Rows[0]["Sample03"]),
                    new SqlParameter("@COST04",dt.Rows[0]["Sample04"]),
                    new SqlParameter("@COST05",dt.Rows[0]["Sample05"]),
                    new SqlParameter("@COST06",dt.Rows[0]["Sample06"]),
                    new SqlParameter("@COST07",COST07),
                    new SqlParameter("@COST08",dt.Rows[0]["Sample07"]),
                    new SqlParameter("@COST09",COST09),
                    new SqlParameter("@COST10",COST10),
                    new SqlParameter("@COST11",COST11),
                    new SqlParameter("@COST12",COST12),
                    new SqlParameter("@COST13",COST13),
                    new SqlParameter("@COST14",COST14),
                    new SqlParameter("@COST15",COST15),
                    new SqlParameter("@topid", SqlDbType.Int)};
                    para[15].Direction = ParameterDirection.Output;
                    SqlHelper.InsertDelUpdate(sql, para);
                    Zid = Convert.ToInt32(para[15].Value == null ? 0 : Convert.ToInt32(para[15].Value));
                    count = Zid;
                }
                else
                {
                    //修改
                    sql = "update BPM_COSTADJUST set COST01=@COST01,COST02=@COST02,COST03=@COST03,COST04=@COST04,COST05=@COST05,COST06=@COST06,COST07=@COST07,COST08=@COST08,COST09=@COST09,COST10=@COST10,COST11=@COST11,COST12=@COST12,COST13=@COST13,COST14=@COST14,COST15=@COST15 where id=@id";
                    para = new SqlParameter[] {
                    new SqlParameter("@COST01",COST01),
                    new SqlParameter("@COST02",COST02),
                    new SqlParameter("@COST03",dt.Rows[0]["Sample03"]),
                    new SqlParameter("@COST04",dt.Rows[0]["Sample04"]),
                    new SqlParameter("@COST05",dt.Rows[0]["Sample05"]),
                    new SqlParameter("@COST06",dt.Rows[0]["Sample06"]),
                    new SqlParameter("@COST07",COST07),
                    new SqlParameter("@COST08",dt.Rows[0]["Sample07"]),
                    new SqlParameter("@COST09",COST09),
                    new SqlParameter("@COST10",COST10),
                    new SqlParameter("@COST11",COST11),
                    new SqlParameter("@COST12",COST12),
                    new SqlParameter("@COST13",COST13),
                    new SqlParameter("@COST14","0"),
                    new SqlParameter("@COST15",COST15),
                    new SqlParameter("@id",id)};
                    count = SqlHelper.InsertDelUpdate(sql, para);
                    Zid = Convert.ToInt32(id);
                }
                if (count > 0)
                {
                    if (!string.IsNullOrWhiteSpace(COSTOTHER))
                    {
                        string[] rowsList = COSTOTHER.Split('|');//每一行数据
                        for (int i = 0; i < rowsList.Length; i++)
                        {
                            var columnList = rowsList[i].Split(',');//每一列的数据
                            if (columnList[0] == "0")
                            {
                                sql = "INSERT INTO BPM_COSTOTHER(OTHER01,OTHER02,OTHER03,OTHER04,OTHER05,OTHER06,OTHER07,OTHER08,COSTID) VALUES (@OTHER01, @OTHER02, @OTHER03, @OTHER04, @OTHER05, @OTHER06, @OTHER07, @OTHER08, @COSTID)";
                            }
                            else {
                                sql = "update BPM_COSTOTHER set OTHER01=@OTHER01,OTHER02=@OTHER02,OTHER03=@OTHER03,OTHER04=@OTHER04,OTHER05=@OTHER05,OTHER06=@OTHER06,OTHER07=@OTHER07,OTHER08=@OTHER08 where id=@id";
                            }
                            para = new SqlParameter[] {
                            new SqlParameter("@OTHER01",columnList[1]),
                            new SqlParameter("@OTHER02",columnList[2]),
                            new SqlParameter("@OTHER03",columnList[3]),
                            new SqlParameter("@OTHER04",columnList[4]),
                            new SqlParameter("@OTHER05",columnList[5]),
                            new SqlParameter("@OTHER06",columnList[6]),
                            new SqlParameter("@OTHER07",columnList[7]),
                            new SqlParameter("@OTHER08",columnList[8]),
                            new SqlParameter("@id",columnList[0]),
                            new SqlParameter("@COSTID",Zid)};
                            SqlHelper.InsertDelUpdate(sql, para);
                        }
                    }
                    if (delid.Length > 0)
                    {
                        sql = "update BPM_COSTOTHER set status=0 where id in (" + delid + ")";
                        SqlHelper.InsertDelUpdate(sql);
                    }
                    return Content("OK");

                }
                else
                {
                    return Content("NO");
                }
            }
            else {
                return Content("NO");
            }
        }

        /// <summary>
        /// 成本核算 BOM删除
        /// </summary>
        /// <returns></returns>
        public ActionResult CostDel(string ids)
        {
            string sql = "update BPM_COSTADJUST set status=0 where id in ("+ ids + ")";
            SqlHelper.InsertDelUpdate(sql);
            return Content("OK");
        }

    }
}