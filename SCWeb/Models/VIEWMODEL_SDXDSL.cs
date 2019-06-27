using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCWeb.Models
{
    /// <summary>
    /// 商店下单数量（订单）
    /// </summary>
    public partial class VIEWMODEL_SDXDSL
    {
        [SugarColumn(Length = 50)]
        public string SPDM { get; set; }
        [SugarColumn(Length = 50)]
        public string GGMC { get; set; }
        public decimal Sl { get; set; }

    }
}