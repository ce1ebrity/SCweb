using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SqlSugar;

namespace SCWeb.Models
{
    public partial class _view_WFzzd
    {
        [SugarColumn(Length = 50, IsNullable = true)]
        public string GCMC { get; set; }
        [SugarColumn(IsPrimaryKey = true, Length = 50, IsNullable = false)]
        public string HTH { get; set; }
        [SugarColumn(Length = 50)]
        public string SPDM { get; set; }
        [SugarColumn(IsNullable = true)]
        public string ISfk { get; set; }
        [SugarColumn(IsNullable = true)]
        public string FKzt { get; set; }
        [SugarColumn(IsNullable = true)]
        public string TJzt { get; set; }
        [SugarColumn(IsNullable = true, Length = 50)]
        public string SHzt2 { get; set; }
        [SugarColumn(IsNullable = true)]
        public decimal Money_1 { get; set; }
        [SugarColumn(IsNullable = true)]
        public decimal Money_2 { get; set; }
        [SugarColumn(IsNullable = true)]
        public decimal Money_3 { get; set; }
        [SugarColumn(Length = 200, IsNullable = true)]
        public string Remark { get; set; }
        [SugarColumn(Length = 200, IsNullable = true)]
        public string KHH { get; set; }
        [SugarColumn(Length = 200, IsNullable = true)]
        public string ZH { get; set; }
        [SugarColumn(Length = 200, IsNullable = true)]
        public string DZ { get; set; }
        [SugarColumn(Length = 200, IsNullable = true)]
        public string Phone { get; set; }
        public DateTime? JSrq { get; set; }

    }
}