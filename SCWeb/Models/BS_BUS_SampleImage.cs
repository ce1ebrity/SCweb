using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SqlSugar;

namespace SCWeb.Models
{
    public partial class BS_BUS_SampleImage
    {
        public BS_BUS_SampleImage()
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

        private System.Int32 _FID;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32 FID { get { return this._FID; } set { this._FID = value; } }

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

        private System.String _SimpleName;
        /// <summary>
        /// 
        /// </summary>
        public System.String SimpleName { get { return this._SimpleName; } set { this._SimpleName = value; } }

        private System.Byte[] _SimpleImage;
        /// <summary>
        /// 
        /// </summary>
        public System.Byte[] SimpleImage { get { return this._SimpleImage; } set { this._SimpleImage = value; } }

        private System.String _Remark;
        /// <summary>
        /// 
        /// </summary>
        public System.String Remark { get { return this._Remark; } set { this._Remark = value; } }

        private System.String _ImgType;
        /// <summary>
        /// 
        /// </summary>
        public System.String ImgType { get { return this._ImgType; } set { this._ImgType = value; } }

        private System.Int32? _ImgX;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? ImgX { get { return this._ImgX; } set { this._ImgX = value; } }

        private System.Int32? _ImgY;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? ImgY { get { return this._ImgY; } set { this._ImgY = value; } }

        private System.Boolean? _IsAppend;
        /// <summary>
        /// 
        /// </summary>
        public System.Boolean? IsAppend { get { return this._IsAppend; } set { this._IsAppend = value; } }

        private System.String _ImgSize;
        /// <summary>
        /// 
        /// </summary>
        public System.String ImgSize { get { return this._ImgSize; } set { this._ImgSize = value; } }

        private System.String _AppendType;
        /// <summary>
        /// 
        /// </summary>
        public System.String AppendType { get { return this._AppendType; } set { this._AppendType = value; } }

        private System.String _SampleType;
        /// <summary>
        /// 
        /// </summary>
        public System.String SampleType { get { return this._SampleType; } set { this._SampleType = value; } }

        private System.Boolean? _SampleDefault;
        /// <summary>
        /// 
        /// </summary>
        public System.Boolean? SampleDefault { get { return this._SampleDefault; } set { this._SampleDefault = value; } }

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