using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCWeb.Models
{
    public class RoleMainModels
    {
        /// <summary>
        /// 总行数
        /// </summary>
        public string total { get; set; }
        /// <summary>
        /// 当前页记录集合
        /// </summary>
        public List<RoleModels> rows { get; set; }
    }

    public class RoleModels
    {

        public string id { get; set; }
        public string roleName { get; set; }
        public string status { get; set; }
        public string addTime { get; set; }
        public string rowCount { get; set; } //总行数
        public string pageCount { get; set; } //总页数
    }
}