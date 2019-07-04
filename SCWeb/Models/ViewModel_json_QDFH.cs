using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SqlSugar;
namespace SCWeb.Models
{
    public partial class ViewModel_json_QDFH
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int id { get; set; }
        [SugarColumn(Length = 50, IsNullable = true)]
        public string CKMC { get; set; }
        [SugarColumn(Length = 50, IsNullable = true)]
        public string DJBH { get; set; }
        [SugarColumn(Length = 50, IsNullable = true)]
        public string SXMC { get; set; }
        [SugarColumn(DecimalDigits = 2, IsNullable = true)]
        public decimal ZK { get; set; }
        [SugarColumn(DecimalDigits = 2, IsNullable = true)]
        public decimal SL { get; set; }
        [SugarColumn(DecimalDigits = 2, IsNullable = true)]
        public decimal BZSJ { get; set; }
        [SugarColumn(DecimalDigits = 2, IsNullable = true)]
        public decimal JE { get; set; }
        [SugarColumn(IsNullable = true)]
        public DateTime RQ { get; set; }
        [SugarColumn(Length = 5000, IsNullable = true)]
        public string BZ { get; set; }
        [SugarColumn(Length = 50, IsNullable = true)]
        public string selectlx { get; set; }
        [SugarColumn(Length = 50, IsNullable = true)]
        public string dd1 { get; set; }
        [SugarColumn(Length = 50, IsNullable = true)]
        public string dd2 { get; set; }
        [SugarColumn(Length = 50, IsNullable = true)]
        public string dd3 { get; set; }
        [SugarColumn(Length = 5000, IsNullable = true)]
        public string remark { get; set; }
        [SugarColumn(Length = 50, IsNullable = true)]
        public string LB { get; set; }
        [SugarColumn(Length = 50, IsNullable = true)]
        public string BYZD3 { get; set; }
        [SugarColumn(Length = 50, IsNullable = true)]
        public string BYZD8 { get; set; }
    }
}