using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SqlSugar;

namespace SCWeb.Models
{
    public partial class WFZZDfk
    {
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// 供货商名称
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = false)]
        public string GHS { get; set; }
        /// <summary>
        /// 供货商地址
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = false)]
        public string GHSDZ { get; set; }
        /// <summary>
        /// 供货商电话
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = false)]
        public string GHSPhone { get; set; }
        /// <summary>
        /// 供货商开户行
        /// </summary>
        [SugarColumn(Length = 100,IsNullable = false)]
        public string GHSKHH { get; set; }
        /// <summary>
        /// 供货商名称账号
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = false)]
        public string GHSZH { get; set; }

    }
}