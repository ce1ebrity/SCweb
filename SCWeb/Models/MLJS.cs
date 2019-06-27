using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCWeb.Models
{
    /// <summary>
    /// 面料结算信息
    /// </summary>
    public class MLJS
    {
        [SugarColumn(Length = 50)]
        public string GHSDM { get; set; }
        [SugarColumn(Length = 50)]
        public string GHSMC { get; set; }
        [SugarColumn(IsPrimaryKey = true, Length = 50)]
        public string YDJH { get; set; }
        /// <summary>
        /// 20%定金
        /// </summary>
        [SugarColumn(IsNullable = true, DecimalDigits = 2)]
        public decimal Money_1 { get; set; }
        /// <summary>
        /// 60%中期款
        /// </summary>
        [SugarColumn(IsNullable = true, DecimalDigits = 2)]
        public decimal Money_2 { get; set; }
        /// <summary>
        /// 20%尾款
        /// </summary>
        [SugarColumn(IsNullable = true, DecimalDigits = 2)]
        public decimal Money_3 { get; set; }
        /// <summary>
        /// 80%结算款
        /// </summary>
        [SugarColumn(IsNullable = true, DecimalDigits = 2)]
        public decimal Money_80 { get; set; }
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
        [SugarColumn(Length = 50, IsNullable = true)]
        public string SHzt { get; set; }
        [SugarColumn(Length = 50, IsNullable = true)]
        public string SHzt2 { get; set; }
        //public int SHzt_3 { get; set; }
        [SugarColumn(Length = 50,IsNullable = true)]
        public string TJzt { get; set; }


    }
}