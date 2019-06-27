using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCWeb.Models
{
    public partial class DANWEI
    {
        public string DWDM { get; set; }
        public string DWMC { get; set; }
        public string BYZD1 { get; set; }
        public string BYZD2 { get; set; }
        public string BYZD3 { get; set; }
        public decimal BYZD4 { get; set; }
        public TimeSpan LastChanged { get; set; }
        public string HSXS { get; set; }
    }
}