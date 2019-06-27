using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCWeb.Models
{
    public partial class View_model_fobdaixiao
    {
        [SugarColumn(Length = 50)]
        public string SPDM { get; set; }
        [SugarColumn( Length = 50)]
        public string GGMC { get; set; }
        [SugarColumn(Length = 50)]
        public string GCMC { get; set; }
        public DateTime JHRQ { get; set; }
        /// <summary>
        /// 实际交货日期
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime SJjhrq { get; set; }
        /// <summary>
        /// 同意异常延期日期
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime TYyqrq { get; set; }
        [SugarColumn(IsNullable = true, DecimalDigits = 2)]
        public decimal htsl { get; set; }
        [SugarColumn(IsNullable = true, DecimalDigits = 2)]
        public decimal htje { get; set; }
        [SugarColumn(IsNullable = true, DecimalDigits = 2)]
        public decimal JGDJ { get; set; }
        public int isDX { get; set; }
        [SugarColumn(Length = 50)]
        public string HTH { get; set; }
    }
}