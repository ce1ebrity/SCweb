using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCWeb.Models
{
    public class FOBJS_FK
    {
        [SugarColumn(Length = 50)]
        public string GCMC { get; set; }
        [SugarColumn(Length = 50)]
        public string SPDM { get; set; }
        //[SugarColumn(IsNullable = false, IsIdentity = true)]
        [SugarColumn(IsPrimaryKey = true, Length = 50)]
        public string HTH { get; set; }
        /// <summary>
        /// 20%定金
        /// </summary>
        /// 
       [SugarColumn(IsNullable = true, DecimalDigits=2)]
        public decimal Money_1 { get; set; }
        /// <summary>
        /// 70%中期款
        /// </summary>
        [SugarColumn(IsNullable = true, DecimalDigits = 2)]
        public decimal Money_2 { get; set; }
        /// <summary>
        /// 10%尾款
        /// </summary>
        [SugarColumn(IsNullable = true, DecimalDigits = 2)]
        public decimal Money_3 { get; set; }
        public DateTime jsRQ { get; set; }
        /// <summary>
        /// 是否开票 zt:0 '否':1'是'
        /// </summary>
        public int ZT { get; set; }
        /// <summary>
        /// 1 : 20%定金
        /// 2 : 60% 中期款
        /// 3 : 20%尾款
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 50)]
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

        /// <summary>
        /// 是否转为代销 1：是 
        /// 2： 否
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int daixiao { get; set; }
        /// <summary>
        /// 含税金额
        /// </summary>
        [SugarColumn(IsNullable = true, DecimalDigits = 2)]
        public decimal hsje { get; set; }
        /// <summary>
        /// 退料扣款
        /// </summary>
        [SugarColumn(IsNullable = true, DecimalDigits = 2)]
        public decimal tlkk { get; set; }
        /// <summary>
        /// 货期扣款
        /// </summary>
        [SugarColumn(IsNullable = true, DecimalDigits = 2)]
        public decimal hqkk { get; set; }
        /// <summary>
        /// 次品扣款
        /// </summary>
        [SugarColumn(IsNullable = true, DecimalDigits = 2)]
        public decimal cpkk { get; set; }
        /// <summary>
        /// 实际付款90%
        /// </summary>
        [SugarColumn(IsNullable = true, DecimalDigits = 2)]
        public decimal je_90 { get; set; }

    }
}