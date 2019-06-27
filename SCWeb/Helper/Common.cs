using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace SCWeb.Helper
{
    public static class Common
    {

        /// <summary>
        /// 设置Cookie
        /// </summary>
        /// <param name="CookieName">名字</param>
        /// <param name="CookieValue">值</param>
        /// <param name="CookieDay">保存[天]数</param>
        public static void SetCookie(string CookieName, string CookieValue, int CookieDay)
        {
            DelCookie(CookieName);
            if (CookieDay > 0)
            {
                HttpContext.Current.Response.Cookies[CookieName].Expires = DateTime.Now.AddDays(CookieDay);
            }
            HttpContext.Current.Response.Cookies[CookieName].Value = HttpUtility.UrlEncode(CookieValue, System.Text.Encoding.GetEncoding("utf-8"));
        }

        /// <summary>
        /// 删除Cookie
        /// </summary>
        /// <param name="CookieName"></param>
        public static void DelCookie(string CookieName)
        {
            HttpCookie cok = HttpContext.Current.Request.Cookies[CookieName];
            if (cok != null)
            {
                TimeSpan ts = new TimeSpan(-1, 0, 0, 0);
                cok.Expires = DateTime.Now.Add(ts);
                HttpContext.Current.Response.AppendCookie(cok);
            }
        }

        /// <summary>
        /// 获取Cookie
        /// </summary>
        /// <param name="CookieName"></param>
        /// <returns></returns>
        public static string GetCookie(string CookieName)
        {
            if (HttpContext.Current.Request.Cookies[CookieName] != null)
            {
                return HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies[CookieName].Value, System.Text.Encoding.GetEncoding("utf-8"));
            }
            return "";
        }

        /// <summary>
        /// 状态转换为中文
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static string GetStatusCN(string status)
        {
            string staCn = "";
            switch (status)
            {
                case "0": staCn = "删除"; break;
                case "1": staCn = "激活"; break;
                case "2": staCn = "冻结"; break;
            }
            return staCn;
        }

        /// <summary>
        /// 用存储过程进行分页显示
        /// </summary>
        /// <param name="rowsCount">输出参数总行数</param>
        /// <param name="pageCount">输出参数总页数</param>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">列</param>
        /// <param name="keyId">索引列</param>
        /// <param name="showRows">显示的行数</param>
        /// <param name="page">当前页</param>
        /// <param name="orderWhere">排序条件</param>
        /// <param name="Where">查询条件</param>
        /// <returns></returns>
        public static DataTable GetSQLProcList(out int rowsCount, out int pageCount, string tableName, string columnName, string keyId, string showRows, string page, string orderWhere, string Where)
        {
            //string sql = "exec proc_GridViewPager  @pageCount output,@tableName,@columnName,@keyId,@showRows,@page,@orderWhere,@Where";
            string sql = "proc_GridViewPager";
            SqlParameter sqlp = new SqlParameter("@recordTotal", SqlDbType.Int);
            sqlp.Direction = ParameterDirection.Output;
            SqlParameter[] para = new SqlParameter[] {
                sqlp,
                new SqlParameter("@viewName",tableName),
                new SqlParameter("@fieldName",columnName),
                new SqlParameter("@keyName",keyId),
                new SqlParameter("@pageSize",showRows),
                new SqlParameter("@pageNo",page),
                new SqlParameter("@orderString",orderWhere),
                new SqlParameter("@whereString",Where)};
            DataTable dt = SqlHelper.ProcTable(sql, para);
            if (dt.Rows.Count > 0)
            {
                rowsCount = (int)para[0].Value;
                pageCount = Convert.ToInt32(rowsCount) % Convert.ToInt32(showRows) == 0 ? Convert.ToInt32(rowsCount) / Convert.ToInt32(showRows) : (Convert.ToInt32(rowsCount) / Convert.ToInt32(showRows)) + 1;
            }
            else {
                rowsCount = 0;
                pageCount = 0;
            }


            return dt;
        }


        //excel导入
        public static DataTable ExcelToDataTable(string filePath, bool isColumnName, int oop)
        {
            DataTable dataTable = null;
            FileStream fs = null;
            DataColumn column = null;
            DataRow dataRow = null;
            IWorkbook workbook = null;
            ISheet sheet = null;
            IRow row = null;
            ICell cell = null;
            int startRow = 0;
            try
            {
                using (fs = File.OpenRead(filePath))
                {
                    // 2007版本  
                    if (filePath.IndexOf(".xlsx") > 0)
                        workbook = new XSSFWorkbook(fs);
                    // 2003版本  
                    else if (filePath.IndexOf(".xls") > 0)
                        workbook = new HSSFWorkbook(fs);

                    if (workbook != null)
                    {
                        sheet = workbook.GetSheetAt(oop);//读取第一个sheet，当然也可以循环读取每个sheet  
                        dataTable = new DataTable();
                        if (sheet != null)
                        {
                            int rowCount = sheet.LastRowNum;//总行数  
                            if (rowCount > 0)
                            {
                                IRow firstRow = sheet.GetRow(0);//第一行  
                                int cellCount = firstRow.LastCellNum;//列数  

                                //构建datatable的列  
                                if (isColumnName)
                                {
                                    startRow = 1;//如果第一行是列名，则从第二行开始读取  
                                    for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                                    {
                                        cell = firstRow.GetCell(i);
                                        if (cell != null)
                                        {
                                            if (cell.StringCellValue != null)
                                            {
                                                column = new DataColumn(cell.StringCellValue);
                                                dataTable.Columns.Add(column);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                                    {
                                        column = new DataColumn("column" + (i + 1));
                                        dataTable.Columns.Add(column);
                                    }
                                }

                                //填充行  
                                for (int i = startRow; i <= rowCount; ++i)
                                {
                                    row = sheet.GetRow(i);
                                    if (row == null) continue;

                                    dataRow = dataTable.NewRow();
                                    for (int j = row.FirstCellNum; j < cellCount; ++j)
                                    {
                                        cell = row.GetCell(j);
                                        if (cell != null)
                                        {
                                            if (cell.CellType == CellType.Numeric)
                                            {
                                                //NPOI中数字和日期都是NUMERIC类型的，这里对其进行判断是否是日期类型
                                                if (HSSFDateUtil.IsCellDateFormatted(cell))//日期类型
                                                {
                                                    dataRow[j] = cell.DateCellValue.ToString("yyyy-MM-dd");
                                                }
                                                else//其他数字类型
                                                {
                                                    dataRow[j] = cell.NumericCellValue;
                                                }
                                            }
                                            else if (cell.CellType == CellType.Blank)//空数据类型
                                            {
                                                dataRow[j] = "";
                                            }
                                            else if (cell.CellType == CellType.Formula)//公式类型
                                            {
                                                //XSSFFormulaEvaluator eva = new XSSFFormulaEvaluator(workbook);
                                                ////dataRow[j] = eva.Evaluate(cell).StringValue;
                                                //dataRow[j] = eva.EvaluateInCell(cell).DateCellValue.ToString("yyyy-MM-dd");

                                                XSSFFormulaEvaluator eva = new XSSFFormulaEvaluator(workbook);
                                                dataRow[j] = eva.EvaluateInCell(cell).StringCellValue;

                                                //HSSFFormulaEvaluator eva = new HSSFFormulaEvaluator(workbook);
                                                //dataRow[j] = eva.Evaluate(cell).StringValue;

                                            }
                                            else //其他类型都按字符串类型来处理
                                            {
                                                dataRow[j] = cell.StringCellValue;
                                            }
                                        }
                                        else {
                                            dataRow[j] = "";
                                        }
                                    }
                                    dataTable.Rows.Add(dataRow);
                                }
                            }
                        }
                    }
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                if (fs != null)
                {
                    fs.Close();
                }
                return null;
            }
        }

      

    }
}