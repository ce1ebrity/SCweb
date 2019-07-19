using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCWeb.Models
{
    public partial class ViewModel_json_fob
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        [SugarColumn(Length = 50, IsNullable = true)]
        public string SPDM { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true)]
        public string JJMC { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true)]
        public string BYZD8 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true)]
        public string HTH { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true)]
        public string GCMC { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime? ZZRQ6 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime? JHRQ { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(DecimalDigits = 4, IsNullable = true)]
        public decimal? SL { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(DecimalDigits = 4, IsNullable = true)]
        public decimal? JE { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(DecimalDigits = 4, IsNullable = true)]
        public decimal? CPSL { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(DecimalDigits = 4, IsNullable = true)]
        public decimal? Money_1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(DecimalDigits = 4, IsNullable = true)]
        public decimal? Money_2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(DecimalDigits = 4, IsNullable = true)]
        public decimal? Money_3 { get; set; }
        [SugarColumn(DecimalDigits = 4, IsNullable = true)]
        public decimal? Sdxdsl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int? SHzt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int? ZT { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime? rkrq { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(DecimalDigits = 4, IsNullable = true)]
        public decimal? rksl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(DecimalDigits = 4, IsNullable = true)]
        public decimal? hsje { get; set; }
        [SugarColumn(Length = 50, IsNullable = true)]
        public string KHH { get; set; } //SCJD01
        [SugarColumn(Length = 50, IsNullable = true)]
        public string ZH { get; set; }
        public int? TJZT { get; set; }
        [SugarColumn(Length = 50, IsNullable = true)]
        public string SCJD01 { get; set; }
    }
}