using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCWeb.Models
{
    public class UserMainModels
    {
        /// <summary>
        /// 总行数
        /// </summary>
        public string total { get; set; }
        /// <summary>
        /// 当前页记录集合
        /// </summary>
        public List<BPM_UserBase> rows { get; set; }
    }

    public class BPM_UserBase
    {
        public string id { get; set; }
        public string loginName { get; set; }
        public string trueName { get; set; }
        public string passWord { get; set; }
        public string brand { get; set; }
        public string type { get; set; }
        public string status { get; set; }
        public string addTime { get; set; }
        /// <summary>
        /// 该用户角色
        /// </summary>
        public string roleName { get; set; }
        public string rowCount { get; set; } //总行数
        public string pageCount { get; set; } //总页数
    }
}