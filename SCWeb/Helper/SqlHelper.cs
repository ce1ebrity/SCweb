using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Data;


public static class SqlHelper
{

    //private static string conStr = ConfigurationManager.
    //    ConnectionStrings["sql"].ConnectionString;

    /// <summary>
    /// 增删改的方法
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public static int InsertDelUpdate(string sql)
    {
        int result = 0;
        string conStr = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;
        using (SqlConnection cn = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand(sql, cn);
            try
            {
                cn.Open();
                result = cmd.ExecuteNonQuery();
              
            }
            catch (Exception ex)
            {
                using (StreamWriter sw = new StreamWriter("错误信息.txt"))
                {
                    sw.WriteLine("错误内容：{0}\n错误时间:{1}", ex.Message, DateTime.Now);
                }
            }
        }
        return result;
    }

    public static int InsertDelUpdate(string sql, params SqlParameter[] paras)
    {
        int result = 0;
        string conStr = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;
        using (SqlConnection cn = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddRange(paras);
            try
            {
                cn.Open();
                result = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
            catch (Exception ex)
            {
                using (StreamWriter sw = new StreamWriter("错误信息.txt"))
                {
                    sw.WriteLine("错误内容：{0}\n错误时间:{1}", ex.Message, DateTime.Now);
                }
            }
        }
        return result;
    }

    /// <summary>
    /// 查询单个值
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public static object SelectSinger(string sql)
    {

        object obj = null;
        string conStr = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;
        using (SqlConnection cn = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand(sql, cn);
            try
            {
                cn.Open();
                obj = cmd.ExecuteScalar();
               
            }
            catch (Exception ex)
            {
                using (StreamWriter sw = new StreamWriter("错误信息.txt", true))
                {
                    sw.WriteLine("错误信息为：" + ex.Message);
                    sw.WriteLine("错误时间为:" + DateTime.Now.ToString());
                }
            }
        }
        return obj;

    }

    public static object SelectSinger(string sql, params SqlParameter[] paras)
    {

        object obj = null;
        string conStr = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;
        using (SqlConnection cn = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddRange(paras);
            try
            {
                cn.Open();
                obj = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
            }
            catch (Exception ex)
            {
                using (StreamWriter sw = new StreamWriter("错误信息.txt", true))
                {
                    sw.WriteLine("错误信息为：" + ex.Message);
                    sw.WriteLine("错误时间为:" + DateTime.Now.ToString());
                }
            }
        }
        return obj;

    }

    /// <summary>
    /// 查询只进只读
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public static SqlDataReader SelectReader(string sql)
    {
        string conStr = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;
        SqlConnection cn = new SqlConnection(conStr);
        SqlCommand cmd = new SqlCommand(sql, cn);
        SqlDataReader reader = null;
        try
        {
            cn.Open();
            reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
        catch (Exception ex)
        {
            using (StreamWriter sw = new StreamWriter("错误信息.txt", true))
            {
                sw.WriteLine("错误信息为：" + ex.Message);
                sw.WriteLine("错误时间为:" + DateTime.Now.ToString());
            }
        }
        return reader;
    }

    public static SqlDataReader SelectReader(string sql, params SqlParameter[] paras)
    {
        string conStr = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;
        SqlConnection cn = new SqlConnection(conStr);
        SqlCommand cmd = new SqlCommand(sql, cn);
        cmd.Parameters.AddRange(paras);
        SqlDataReader reader = null;
        try
        {
            cn.Open();
            reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
        catch (Exception ex)
        {
            using (StreamWriter sw = new StreamWriter("错误信息.txt", true))
            {
                sw.WriteLine("错误信息为：" + ex.Message);
                sw.WriteLine("错误时间为:" + DateTime.Now.ToString());
            }
        }
        return reader;
    }

    /// <summary>
    /// 查询临时数据库
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public static DataTable SelectTable(string sql)
    {
        string conStr = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;
        using (SqlConnection cn = new SqlConnection(conStr))
        {
            SqlDataAdapter ad = new SqlDataAdapter(sql, cn);
            ad.SelectCommand.CommandTimeout = 0;
            DataTable dt = new DataTable();
            try
            {
                ad.Fill(dt);
            }
            catch (Exception ex)
            {
                using (StreamWriter sw = new StreamWriter("错误信息.txt", true))
                {
                    sw.WriteLine("错误信息为：" + ex.Message);
                    sw.WriteLine("错误时间为:" + DateTime.Now.ToString());
                }
                dt = null;
            }
            return dt;
        }
    }

    public static DataTable SelectTable(string sql, params SqlParameter[] paras)
    {
        string conStr = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;
        using (SqlConnection cn = new SqlConnection(conStr))
        {
            SqlDataAdapter ad = new SqlDataAdapter(sql, cn);
            ad.SelectCommand.CommandTimeout = 0;
            ad.SelectCommand.Parameters.AddRange(paras);
            DataTable dt = new DataTable();
            try
            {
                ad.Fill(dt);
            }
            catch (Exception ex)
            {
                using (StreamWriter sw = new StreamWriter("错误信息.txt", true))
                {
                    sw.WriteLine("错误信息为：" + ex.Message);
                    sw.WriteLine("错误时间为:" + DateTime.Now.ToString());
                }
            }
            return dt;
        }
    }

    public static DataTable ProcTable(string sql, params SqlParameter[] paras)
    {
        string conStr = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;
        using (SqlConnection cn = new SqlConnection(conStr))
        {
            SqlDataAdapter ad = new SqlDataAdapter(sql, cn);
            ad.SelectCommand.CommandType = CommandType.StoredProcedure;
            ad.SelectCommand.Parameters.AddRange(paras);
            DataTable dt = new DataTable();
            try
            {
                ad.Fill(dt);
            }
            catch (Exception ex)
            {
                using (StreamWriter sw = new StreamWriter("错误信息.txt", true))
                {
                    sw.WriteLine("错误信息为：" + ex.Message);
                    sw.WriteLine("错误时间为:" + DateTime.Now.ToString());
                }
            }
            return dt;
        }
    }


    public static DataTable SelectTable(string sql, string ctr, params SqlParameter[] paras)
    {
        string conStr = ctr;
        using (SqlConnection cn = new SqlConnection(conStr))
        {
            SqlDataAdapter ad = new SqlDataAdapter(sql, cn);
            ad.SelectCommand.CommandTimeout = 0;
            ad.SelectCommand.Parameters.AddRange(paras);
            DataTable dt = new DataTable();
            try
            {
                ad.Fill(dt);
            }
            catch (Exception ex)
            {
                using (StreamWriter sw = new StreamWriter("错误信息.txt", true))
                {
                    sw.WriteLine("错误信息为：" + ex.Message);
                    sw.WriteLine("错误时间为:" + DateTime.Now.ToString());
                }
            }
            return dt;
        }
    }

    /// <summary>
    /// 查询单个值
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public static object SelectSinger(string sql, string ctr)
    {

        object obj = null;
        string conStr = ctr;
        using (SqlConnection cn = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand(sql, cn);
            try
            {
                cn.Open();
                obj = cmd.ExecuteScalar();

            }
            catch (Exception ex)
            {
                using (StreamWriter sw = new StreamWriter("错误信息.txt", true))
                {
                    sw.WriteLine("错误信息为：" + ex.Message);
                    sw.WriteLine("错误时间为:" + DateTime.Now.ToString());
                }
            }
        }
        return obj;

    }
    public static DataTable ProcTable(string sql ,string ctr, params SqlParameter[] paras)
    {
        string conStr = ctr;
        using (SqlConnection cn = new SqlConnection(conStr))
        {
            SqlDataAdapter ad = new SqlDataAdapter(sql, cn);
            ad.SelectCommand.CommandType = CommandType.StoredProcedure;
            ad.SelectCommand.Parameters.AddRange(paras);
            DataTable dt = new DataTable();
            try
            {
                ad.Fill(dt);
            }
            catch (Exception ex)
            {
                using (StreamWriter sw = new StreamWriter("错误信息.txt", true))
                {
                    sw.WriteLine("错误信息为：" + ex.Message);
                    sw.WriteLine("错误时间为:" + DateTime.Now.ToString());
                }
            }
            return dt;
        }
    }
}

