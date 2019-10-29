using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SqlSugar;

namespace SCWeb.Models
{
    public partial class SPGG1
    {
        public SPGG1()
        {
        }

        private System.String _SPDM;
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.String SPDM { get { return this._SPDM; } set { this._SPDM = value; } }

        private System.String _GGDM;
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.String GGDM { get { return this._GGDM; } set { this._GGDM = value; } }

        private System.String _BYZD1;
        /// <summary>
        /// 
        /// </summary>
        public System.String BYZD1 { get { return this._BYZD1; } set { this._BYZD1 = value; } }

        private System.String _BYZD2;
        /// <summary>
        /// 
        /// </summary>
        public System.String BYZD2 { get { return this._BYZD2; } set { this._BYZD2 = value; } }

        private System.Int32? _BYZD3;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? BYZD3 { get { return this._BYZD3; } set { this._BYZD3 = value; } }

        private System.Decimal? _BYZD4;
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(DecimalDigits = 2)] 
        public System.Decimal? BYZD4 { get { return this._BYZD4; } set { this._BYZD4 = value; } }

        private System.String _BYZD5;
        /// <summary>
        /// 
        /// </summary>
        public System.String BYZD5 { get { return this._BYZD5; } set { this._BYZD5 = value; } }

        private System.String _BYZD6;
        /// <summary>
        /// 
        /// </summary>
        public System.String BYZD6 { get { return this._BYZD6; } set { this._BYZD6 = value; } }

        private System.Byte[] _LastChanged;
        /// <summary>
        /// 
        /// </summary>
        public System.Byte[] LastChanged { get { return this._LastChanged; } set { this._LastChanged = value; } }

        private System.String _MS1;
        /// <summary>
        /// 
        /// </summary>
        public System.String MS1 { get { return this._MS1; } set { this._MS1 = value; } }

        private System.String _MS2;
        /// <summary>
        /// 
        /// </summary>
        public System.String MS2 { get { return this._MS2; } set { this._MS2 = value; } }

        private System.String _MS3;
        /// <summary>
        /// 
        /// </summary>
        public System.String MS3 { get { return this._MS3; } set { this._MS3 = value; } }
    }
}