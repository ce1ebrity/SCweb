using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SqlSugar;

namespace SCWeb.Models
{
    public partial class Use111
    {
        [SugarColumn(Length = 50, IsNullable = true)]
        public string Name { get; set; }
        [SugarColumn(IsNullable = true)]
        public int Age { get; set; }
        [SugarColumn( Length = 50, IsNullable = true)]
        public string Sex { get; set; }
        [SugarColumn(Length = 50, IsNullable = true)]
        public string Phone { get; set; }
        [SugarColumn(Length = 50, IsNullable = true)]
        public string Remark { get; set; }
        [SugarColumn(IsNullable = true)]
        public DateTime? Rq { get; set; }
    }
}