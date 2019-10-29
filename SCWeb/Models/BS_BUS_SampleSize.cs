using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCWeb.Models
{
    public partial class BS_BUS_SampleSize
    {
        public BS_BUS_SampleSize()
        {
        }

        private System.String _MasterID;
        /// <summary>
        /// 
        /// </summary>
        public System.String MasterID { get { return this._MasterID; } set { this._MasterID = value; } }

        private System.String _DetailID;
        /// <summary>
        /// 
        /// </summary>
        public System.String DetailID { get { return this._DetailID; } set { this._DetailID = value; } }

        private System.Int64 _FID;
        /// <summary>
        /// 
        /// </summary>
        public System.Int64 FID { get { return this._FID; } set { this._FID = value; } }

        private System.String _OperatorID;
        /// <summary>
        /// 
        /// </summary>
        public System.String OperatorID { get { return this._OperatorID; } set { this._OperatorID = value; } }

        private System.String _Operator;
        /// <summary>
        /// 
        /// </summary>
        public System.String Operator { get { return this._Operator; } set { this._Operator = value; } }

        private System.DateTime? _CreateTime;
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? CreateTime { get { return this._CreateTime; } set { this._CreateTime = value; } }

        private System.DateTime? _UpdateTime;
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? UpdateTime { get { return this._UpdateTime; } set { this._UpdateTime = value; } }

        private System.Byte[] _LastUpdateTime;
        /// <summary>
        /// 
        /// </summary>
        public System.Byte[] LastUpdateTime { get { return this._LastUpdateTime; } set { this._LastUpdateTime = value; } }

        private System.Int32? _State;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? State { get { return this._State; } set { this._State = value; } }

        private System.String _Remark;
        /// <summary>
        /// 
        /// </summary>
        public System.String Remark { get { return this._Remark; } set { this._Remark = value; } }

        private System.String _Code;
        /// <summary>
        /// 
        /// </summary>
        public System.String Code { get { return this._Code; } set { this._Code = value; } }

        private System.String _Name;
        /// <summary>
        /// 
        /// </summary>
        public System.String Name { get { return this._Name; } set { this._Name = value; } }

        private System.String _Groups;
        /// <summary>
        /// 
        /// </summary>
        public System.String Groups { get { return this._Groups; } set { this._Groups = value; } }

        private System.String _SimpleName;
        /// <summary>
        /// 
        /// </summary>
        public System.String SimpleName { get { return this._SimpleName; } set { this._SimpleName = value; } }

        private System.Boolean? _IsDefault;
        /// <summary>
        /// 
        /// </summary>
        public System.Boolean? IsDefault { get { return this._IsDefault; } set { this._IsDefault = value; } }

        private System.String _FidIdx;
        /// <summary>
        /// 
        /// </summary>
        public System.String FidIdx { get { return this._FidIdx; } set { this._FidIdx = value; } }

        private System.String _Auditor;
        /// <summary>
        /// 
        /// </summary>
        public System.String Auditor { get { return this._Auditor; } set { this._Auditor = value; } }

        private System.DateTime? _AuditTime;
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? AuditTime { get { return this._AuditTime; } set { this._AuditTime = value; } }
    }
}