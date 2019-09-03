using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SqlSugar;

namespace SCWeb.Models
{
    public partial class ViewModel_json_wf
    {
        /// <summary>
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
        public DateTime? ZZRQ3 { get; set; }
        [SugarColumn(IsNullable = true)]
        public DateTime? JHRQ { get; set; }
        [SugarColumn(DecimalDigits = 4, IsNullable = true)]
        public decimal? SL { get; set; }
        [SugarColumn(DecimalDigits = 4, IsNullable = true)]
        public decimal? JE { get; set; }
        [SugarColumn(DecimalDigits = 4, IsNullable = true)]
        public decimal? rksl { get; set; }
        [SugarColumn(DecimalDigits = 4, IsNullable = true)]
        public DateTime? rkrq { get; set; }
        [SugarColumn(IsNullable = true)]
        public decimal? Money_1 { get; set; }
        [SugarColumn(DecimalDigits = 4, IsNullable = true)]
        public decimal? Money_2 { get; set; }
        [SugarColumn(DecimalDigits = 4, IsNullable = true)]
        public decimal? Money_3 { get; set; }
        [SugarColumn(DecimalDigits = 4, IsNullable = true)]
        public decimal? Sdxdsl { get; set; }
        [SugarColumn(IsNullable = true)]
        public string FKzt { get; set; }
        /// <summary>
        /// 供货商地址
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = true)]
        public string DZ { get; set; }
        /// <summary>
        /// 供货商电话
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = true)]
        public string Phone { get; set; }
        /// <summary>
        /// 供货商开户行
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = true)]
        public string KHH { get; set; }
        /// <summary>
        /// 供货商名称账号 jgdj
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = true)]
        public string ZH { get; set; }

        [SugarColumn(DecimalDigits = 4, IsNullable = true)]
        public decimal? jgdj { get; set; }
    }
}