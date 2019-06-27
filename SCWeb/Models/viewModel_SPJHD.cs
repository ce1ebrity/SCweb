using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCWeb.Models
{
    public partial class viewModel_SPJHD
    {
        public int BYZD8 { get; set; }
        public string JJMC { get; set; }
        public string YDJH { get; set; }
        public string GCMC { get; set; }
        public string SPDM { get; set; }
        /// <summary>
        /// 合同签订日期
        /// </summary>
        public DateTime ? ZZRQ6 { get; set; }
        /// <summary>
        /// 合同货期
        /// </summary>
        public DateTime ? JHRQ { get; set; }
        public decimal sl { get; set; }
        public decimal je { get; set; }
        public decimal cpsl { get; set; }
        public decimal jhsl { get; set; }
        public decimal jhsl1 { get; set; }
        public DateTime ? rkRQ { get; set; }
    }
}