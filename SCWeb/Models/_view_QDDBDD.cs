using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SqlSugar;

namespace SCWeb.Models
{
    public partial class _view_QDDBDD
    {
        [SugarColumn(Length = 50, IsNullable = true)]
        public string CKMC { get; set; }
        [SugarColumn(Length = 50, IsNullable = true)]
        public string BYZD8 { get; set; }
        [SugarColumn(Length = 50, IsNullable = true)]
        public string BYZD3 { get; set; }
        [SugarColumn(Length = 50, IsNullable = true)]
        public string JJMC { get; set; }
        [SugarColumn(Length = 50, IsNullable = true)]
        public string SXMC { get; set; }
        [SugarColumn(Length = 50, IsNullable = true)]
        public string ZK { get; set; }
        [SugarColumn(Length = 50, IsNullable = true)]
        public string ddsl { get; set; }
        [SugarColumn(Length = 50, IsNullable = true)]
        public string ddje { get; set; }
        [SugarColumn(Length = 50, IsNullable = true)]
        public string ddnfhsl { get; set; }
        [SugarColumn(Length = 50, IsNullable = true)]
        public string ddnfhje { get; set; }
        [SugarColumn(Length = 50, IsNullable = true)]
        public string ddnthsl { get; set; }
        [SugarColumn(Length = 50, IsNullable = true)]
        public string ddnthje { get; set; }
        [SugarColumn(Length = 50, IsNullable = true)]
        public string ddnjfsl { get; set; }
        [SugarColumn(Length = 50, IsNullable = true)]
        public string ddnjfje { get; set; }

        [SugarColumn(Length = 50, IsNullable = true)]
        public string ddwfhsl { get; set; }
        [SugarColumn(Length = 50, IsNullable = true)]
        public string ddwfhje { get; set; }

        [SugarColumn(Length = 50, IsNullable = true)]
        public string ddwthsl { get; set; }
        [SugarColumn(Length = 50, IsNullable = true)]
        public string ddwthje { get; set; }
        [SugarColumn(Length = 50, IsNullable = true)]
        public string ddwjfsl { get; set; }
        [SugarColumn(Length = 50, IsNullable = true)]
        public string ddwjfje { get; set; }
    }

}