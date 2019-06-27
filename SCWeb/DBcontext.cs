using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using SCWeb.Helper;
using SqlSugar;

namespace SCWeb
{
    public class DBcontext
    {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;
        /// <summary>
        /// InitKeyType = InitKeyType.Attribute 
        /// </summary>
        /// <returns></returns>
        public static SqlSugarClient GetContext()
        {
            ICacheService myCache = new RedisCache("127.0.0.1");
            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
            { ConnectionString = ConnectionString,
                DbType = SqlSugar.DbType.SqlServer,
                IsAutoCloseConnection = true, InitKeyType = InitKeyType.Attribute,
                ConfigureExternalServices = new ConfigureExternalServices()
                {
                    DataInfoCacheService = new RedisCache()
                }
            });
            return db;

        }
    }
}