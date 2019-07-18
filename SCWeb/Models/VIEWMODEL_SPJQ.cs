using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SqlSugar;

namespace SCWeb.Models
{
    public class VIEWMODEL_SPJQ
    {
        [SugarColumn(Length = 50,IsNullable =true)]
        public string SCJD05 { get; set; }
        [SugarColumn(Length = 100,IsNullable = true)]
        public string SCJD01 { get; set; }
    }
}