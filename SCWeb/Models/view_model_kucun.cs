using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCWeb.Models
{
    public partial class view_model_kucun
    {
        [SugarColumn(Length = 50)]
        public string SPDM { get; set; }
        [SugarColumn(Length = 50)]
        public string GGMC { get; set; }
        public decimal KCSl { get; set; }
        public decimal RKSl { get; set; }

    }

}