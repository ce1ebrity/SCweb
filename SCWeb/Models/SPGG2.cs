using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCWeb.Models
{
    public partial class SPGG2
    {
        public SPGG2()
        {
        }

        private System.String _SPDM;
        /// <summary>
        /// 
        /// </summary>
        public System.String SPDM { get { return this._SPDM; } set { this._SPDM = value; } }

        private System.String _GGDM;
        /// <summary>
        /// 
        /// </summary>
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
        public System.Decimal? BYZD4 { get { return this._BYZD4; } set { this._BYZD4 = value; } }

        private System.Byte[] _LastChanged;
        /// <summary>
        /// 
        /// </summary>
        public System.Byte[] LastChanged { get { return this._LastChanged; } set { this._LastChanged = value; } }
    }
}