using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SqlSugar;

namespace SCWeb.Models
{
    public partial class ViewModel_json_cmt
    { /// <summary>
      /// 款号
      /// </summary>
        [SugarColumn(Length = 50, IsNullable = true)]
        public string SPDM { get; set; }
        /// <summary>
        /// 季节
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true)]
        public string JJMC { get; set; }
        /// <summary>
        /// 年份
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true)]
        public string BYZD8 { get; set; }
        /// <summary>
        /// 合同号
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true)]
        public string HTH { get; set; }
        /// <summary>
        /// 工厂名称
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true)]
        public string GCMC { get; set; }
        [SugarColumn(IsNullable = true)]
        public DateTime? ZZRQ6 { get; set; }
        [SugarColumn(IsNullable = true)]
        public DateTime? JHRQ { get; set; }
        [SugarColumn(IsNullable = true)]
        public DateTime? RKRQ { get; set; }
        [SugarColumn(IsNullable = true)]

        public decimal? HTSL { get; set; }
        [SugarColumn(IsNullable = true)]
        public decimal? HTJE { get; set; }
        [SugarColumn(IsNullable = true)]
        public decimal? JHSL { get; set; }
        [SugarColumn(IsNullable = true)]
        public decimal? JHSL1 { get; set; }
        [SugarColumn(IsNullable = true)]
        public decimal? JHSL2 { get; set; }
        [SugarColumn(IsNullable = true)]
        public decimal? CPSL { get; set; }
        [SugarColumn(IsNullable = true)]
    
        public decimal? Money_1 { get; set; }

        /// <summary>
        /// 供货商开户行
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = true)]
        public string KHH { get; set; }
        /// <summary>
        /// 供货商名称账号
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = true)]
        public string ZH { get; set; }
        [SugarColumn(IsNullable = true)]
        public int? SHzt { get; set; }


    }
}