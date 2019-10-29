using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCWeb.Models
{
    public partial class FJSX14
    {
        public FJSX14()
        {
        }

        private System.String _SXDM;
        /// <summary>
        /// 
        /// </summary>
        public System.String SXDM { get { return this._SXDM; } set { this._SXDM = value; } }

        private System.String _SXMC;
        /// <summary>
        /// 
        /// </summary>
        public System.String SXMC { get { return this._SXMC; } set { this._SXMC = value; } }

        private System.String _XTMR;
        /// <summary>
        /// 
        /// </summary>
        public System.String XTMR { get { return this._XTMR; } set { this._XTMR = value; } }

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
        public System.Decimal? BYZD4 { get { return this._BYZD4; } set { this._BYZD4 = value; } }

        private System.Byte[] _LastChanged;
        /// <summary>
        /// 
        /// </summary>
        public System.Byte[] LastChanged { get { return this._LastChanged; } set { this._LastChanged = value; } }

        private System.String _upload;
        /// <summary>
        /// 
        /// </summary>
        public System.String upload { get { return this._upload; } set { this._upload = value; } }
    }
}