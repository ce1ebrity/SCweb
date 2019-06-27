using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SqlSugar;

namespace SCWeb.Models
{
    public class CMT_FK
    {
        [SugarColumn(Length = 50)]
        public string GCMC { get; set; }
        [SugarColumn(Length = 50)]
        public string SPDM { get; set; }
        //[SugarColumn(IsNullable = false, IsIdentity = true)]
        [SugarColumn(IsPrimaryKey = true, Length = 50)]
        public string HTH { get; set; }
        /// <summary>
        ///
        /// </summary>
        /// 
        [SugarColumn(IsNullable = true, DecimalDigits = 2)]
        public decimal Money_1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsNullable = true, DecimalDigits = 2)]
        public decimal Money_2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsNullable = true, DecimalDigits = 2)]
        public decimal Money_3 { get; set; }
        public DateTime jsRQ { get; set; }
        /// <summary>
        /// 是否开票 zt:0 '否':1'是'
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 50)]
        public string SHzt { get; set; }
        [SugarColumn(IsNullable = true, Length = 50)]
        public string SHzt2 { get; set; }
        /// <summary>
        /// 提交状态
        /// 1
        /// 2
        /// 3
        /// </summary>
        [SugarColumn(Length = 50)]
        public string TJzt { get; set; }

        [SugarColumn(Length = 500, IsNullable = true)]
        public string Remark { get; set; }
        [SugarColumn(IsNullable = true, Length = 50)]
        public string ZT { get; set; }
    }
}