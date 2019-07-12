using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCWeb.Models
{
    public partial class ViewModel_JSOn_ML
    {
        [SugarColumn(Length = 50, IsNullable = true)]
        public string GHSMC { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true)]
        public string GHSDM { get; set; }
        /// <summary>
        /// 
        /// </summary>

        [SugarColumn(Length = 50, IsNullable = true)]
        public string BYZD8 { get; set; }
        [SugarColumn(Length = 50, IsNullable = true)]
        public string JJMC { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true)]
        public string MLMC { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true)]
        public string YDJH { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime? RQ { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime? YXRQ { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime? rq1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(DecimalDigits = 4, IsNullable = true)]
        public decimal? SL { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(DecimalDigits = 4, IsNullable = true)]
        public decimal? sl1 { get; set; }
        [SugarColumn(DecimalDigits = 4, IsNullable = true)]
        public decimal? JE { get; set; }
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
        public int? TJzt { get; set; }
        [SugarColumn(Length = 50, IsNullable = true)]
        public string KHH { get; set; }
        [SugarColumn(Length = 50, IsNullable = true)]
        public string ZH { get; set; }
    }

}