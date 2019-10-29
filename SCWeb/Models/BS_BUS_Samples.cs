using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SqlSugar;

namespace SCWeb.Models
{
    public partial class BS_BUS_Samples
    {
        public BS_BUS_Samples()
        {
        }

        private System.String _MasterID;
        /// <summary>
        /// 
        /// </summary>
        public System.String MasterID { get { return this._MasterID; } set { this._MasterID = value; } }

        private System.Int32 _FID;
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsIdentity = true)]
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

        private System.String _Alias;
        /// <summary>
        /// 
        /// </summary>
        public System.String Alias { get { return this._Alias; } set { this._Alias = value; } }

        private System.String _UnitCode;
        /// <summary>
        /// 
        /// </summary>
        public System.String UnitCode { get { return this._UnitCode; } set { this._UnitCode = value; } }

        private System.String _UnitName;
        /// <summary>
        /// 
        /// </summary>
        public System.String UnitName { get { return this._UnitName; } set { this._UnitName = value; } }

        private System.Boolean? _IsOutSample;
        /// <summary>
        /// 
        /// </summary>
        public System.Boolean? IsOutSample { get { return this._IsOutSample; } set { this._IsOutSample = value; } }

        private System.String _OutSampleAddress;
        /// <summary>
        /// 
        /// </summary>
        public System.String OutSampleAddress { get { return this._OutSampleAddress; } set { this._OutSampleAddress = value; } }

        private System.String _Designer;
        /// <summary>
        /// 
        /// </summary>
        public System.String Designer { get { return this._Designer; } set { this._Designer = value; } }

        private System.String _DesignID;
        /// <summary>
        /// 
        /// </summary>
        public System.String DesignID { get { return this._DesignID; } set { this._DesignID = value; } }

        private System.String _OrderMeetingCode;
        /// <summary>
        /// 
        /// </summary>
        public System.String OrderMeetingCode { get { return this._OrderMeetingCode; } set { this._OrderMeetingCode = value; } }

        private System.String _OrderMeetingName;
        /// <summary>
        /// 
        /// </summary>
        public System.String OrderMeetingName { get { return this._OrderMeetingName; } set { this._OrderMeetingName = value; } }

        private System.String _BrandCode;
        /// <summary>
        /// 
        /// </summary>
        public System.String BrandCode { get { return this._BrandCode; } set { this._BrandCode = value; } }

        private System.String _BrandName;
        /// <summary>
        /// 
        /// </summary>
        public System.String BrandName { get { return this._BrandName; } set { this._BrandName = value; } }

        private System.String _BigTypeCode;
        /// <summary>
        /// 
        /// </summary>
        public System.String BigTypeCode { get { return this._BigTypeCode; } set { this._BigTypeCode = value; } }

        private System.String _BigTypeName;
        /// <summary>
        /// 
        /// </summary>
        public System.String BigTypeName { get { return this._BigTypeName; } set { this._BigTypeName = value; } }

        private System.String _SerialsCode;
        /// <summary>
        /// 
        /// </summary>
        public System.String SerialsCode { get { return this._SerialsCode; } set { this._SerialsCode = value; } }

        private System.String _SerialsName;
        /// <summary>
        /// 
        /// </summary>
        public System.String SerialsName { get { return this._SerialsName; } set { this._SerialsName = value; } }

        private System.String _CustCode;
        /// <summary>
        /// 
        /// </summary>
        public System.String CustCode { get { return this._CustCode; } set { this._CustCode = value; } }

        private System.String _CustName;
        /// <summary>
        /// 
        /// </summary>
        public System.String CustName { get { return this._CustName; } set { this._CustName = value; } }

        private System.String _SeasonCode;
        /// <summary>
        /// 
        /// </summary>
        public System.String SeasonCode { get { return this._SeasonCode; } set { this._SeasonCode = value; } }

        private System.String _SeasonName;
        /// <summary>
        /// 
        /// </summary>
        public System.String SeasonName { get { return this._SeasonName; } set { this._SeasonName = value; } }

        private System.String _YearCode;
        /// <summary>
        /// 
        /// </summary>
        public System.String YearCode { get { return this._YearCode; } set { this._YearCode = value; } }

        private System.String _YearName;
        /// <summary>
        /// 
        /// </summary>
        public System.String YearName { get { return this._YearName; } set { this._YearName = value; } }

        private System.String _Property01;
        /// <summary>
        /// 
        /// </summary>
        public System.String Property01 { get { return this._Property01; } set { this._Property01 = value; } }

        private System.String _Property02;
        /// <summary>
        /// 
        /// </summary>
        public System.String Property02 { get { return this._Property02; } set { this._Property02 = value; } }

        private System.String _Property03;
        /// <summary>
        /// 
        /// </summary>
        public System.String Property03 { get { return this._Property03; } set { this._Property03 = value; } }

        private System.String _Property04;
        /// <summary>
        /// 
        /// </summary>
        public System.String Property04 { get { return this._Property04; } set { this._Property04 = value; } }

        private System.String _Property05;
        /// <summary>
        /// 
        /// </summary>
        public System.String Property05 { get { return this._Property05; } set { this._Property05 = value; } }

        private System.String _Property06;
        /// <summary>
        /// 
        /// </summary>
        public System.String Property06 { get { return this._Property06; } set { this._Property06 = value; } }

        private System.String _Property07;
        /// <summary>
        /// 
        /// </summary>
        public System.String Property07 { get { return this._Property07; } set { this._Property07 = value; } }

        private System.String _Property08;
        /// <summary>
        /// 
        /// </summary>
        public System.String Property08 { get { return this._Property08; } set { this._Property08 = value; } }

        private System.String _Property09;
        /// <summary>
        /// 
        /// </summary>
        public System.String Property09 { get { return this._Property09; } set { this._Property09 = value; } }

        private System.String _Property10;
        /// <summary>
        /// 
        /// </summary>
        public System.String Property10 { get { return this._Property10; } set { this._Property10 = value; } }

        private System.String _Property11;
        /// <summary>
        /// 
        /// </summary>
        public System.String Property11 { get { return this._Property11; } set { this._Property11 = value; } }

        private System.String _Property12;
        /// <summary>
        /// 
        /// </summary>
        public System.String Property12 { get { return this._Property12; } set { this._Property12 = value; } }

        private System.String _Property13;
        /// <summary>
        /// 
        /// </summary>
        public System.String Property13 { get { return this._Property13; } set { this._Property13 = value; } }

        private System.String _Property14;
        /// <summary>
        /// 
        /// </summary>
        public System.String Property14 { get { return this._Property14; } set { this._Property14 = value; } }

        private System.String _Property15;
        /// <summary>
        /// 
        /// </summary>
        public System.String Property15 { get { return this._Property15; } set { this._Property15 = value; } }

        private System.String _Property16;
        /// <summary>
        /// 
        /// </summary>
        public System.String Property16 { get { return this._Property16; } set { this._Property16 = value; } }

        private System.String _PrintInfo1;
        /// <summary>
        /// 
        /// </summary>
        public System.String PrintInfo1 { get { return this._PrintInfo1; } set { this._PrintInfo1 = value; } }

        private System.String _PrintInfo2;
        /// <summary>
        /// 
        /// </summary>
        public System.String PrintInfo2 { get { return this._PrintInfo2; } set { this._PrintInfo2 = value; } }

        private System.String _PrintInfo3;
        /// <summary>
        /// 
        /// </summary>
        public System.String PrintInfo3 { get { return this._PrintInfo3; } set { this._PrintInfo3 = value; } }

        private System.String _PrintInfo4;
        /// <summary>
        /// 
        /// </summary>
        public System.String PrintInfo4 { get { return this._PrintInfo4; } set { this._PrintInfo4 = value; } }

        private System.String _PrintInfo5;
        /// <summary>
        /// 
        /// </summary>
        public System.String PrintInfo5 { get { return this._PrintInfo5; } set { this._PrintInfo5 = value; } }

        private System.String _PrintInfo6;
        /// <summary>
        /// 
        /// </summary>
        public System.String PrintInfo6 { get { return this._PrintInfo6; } set { this._PrintInfo6 = value; } }

        private System.String _PrintDesc1;
        /// <summary>
        /// 
        /// </summary>
        public System.String PrintDesc1 { get { return this._PrintDesc1; } set { this._PrintDesc1 = value; } }

        private System.String _PrintDesc2;
        /// <summary>
        /// 
        /// </summary>
        public System.String PrintDesc2 { get { return this._PrintDesc2; } set { this._PrintDesc2 = value; } }

        private System.String _PrintDesc3;
        /// <summary>
        /// 
        /// </summary>
        public System.String PrintDesc3 { get { return this._PrintDesc3; } set { this._PrintDesc3 = value; } }

        private System.String _Remark;
        /// <summary>
        /// 
        /// </summary>
        public System.String Remark { get { return this._Remark; } set { this._Remark = value; } }

        private System.String _SizeCode;
        /// <summary>
        /// 
        /// </summary>
        public System.String SizeCode { get { return this._SizeCode; } set { this._SizeCode = value; } }

        private System.String _SizeName;
        /// <summary>
        /// 
        /// </summary>
        public System.String SizeName { get { return this._SizeName; } set { this._SizeName = value; } }

        private System.String _ColorCode;
        /// <summary>
        /// 
        /// </summary>
        public System.String ColorCode { get { return this._ColorCode; } set { this._ColorCode = value; } }

        private System.String _ColorName;
        /// <summary>
        /// 
        /// </summary>
        public System.String ColorName { get { return this._ColorName; } set { this._ColorName = value; } }

        private System.Decimal? _AuditScore;
        /// <summary>
        /// 
        /// </summary>
        public System.Decimal? AuditScore { get { return this._AuditScore; } set { this._AuditScore = value; } }

        private System.Decimal? _SampleScore;
        /// <summary>
        /// 
        /// </summary>
        public System.Decimal? SampleScore { get { return this._SampleScore; } set { this._SampleScore = value; } }

        private System.Decimal? _OrderScore;
        /// <summary>
        /// 
        /// </summary>
        public System.Decimal? OrderScore { get { return this._OrderScore; } set { this._OrderScore = value; } }

        private System.String _PrprCode01;
        /// <summary>
        /// 
        /// </summary>
        public System.String PrprCode01 { get { return this._PrprCode01; } set { this._PrprCode01 = value; } }

        private System.String _PrprCode02;
        /// <summary>
        /// 
        /// </summary>
        public System.String PrprCode02 { get { return this._PrprCode02; } set { this._PrprCode02 = value; } }

        private System.String _PrprCode03;
        /// <summary>
        /// 
        /// </summary>
        public System.String PrprCode03 { get { return this._PrprCode03; } set { this._PrprCode03 = value; } }

        private System.String _PrprCode04;
        /// <summary>
        /// 
        /// </summary>
        public System.String PrprCode04 { get { return this._PrprCode04; } set { this._PrprCode04 = value; } }

        private System.String _PrprCode05;
        /// <summary>
        /// 
        /// </summary>
        public System.String PrprCode05 { get { return this._PrprCode05; } set { this._PrprCode05 = value; } }

        private System.String _PrprCode06;
        /// <summary>
        /// 
        /// </summary>
        public System.String PrprCode06 { get { return this._PrprCode06; } set { this._PrprCode06 = value; } }

        private System.String _PrprCode07;
        /// <summary>
        /// 
        /// </summary>
        public System.String PrprCode07 { get { return this._PrprCode07; } set { this._PrprCode07 = value; } }

        private System.String _PrprCode08;
        /// <summary>
        /// 
        /// </summary>
        public System.String PrprCode08 { get { return this._PrprCode08; } set { this._PrprCode08 = value; } }

        private System.String _PrprCode09;
        /// <summary>
        /// 
        /// </summary>
        public System.String PrprCode09 { get { return this._PrprCode09; } set { this._PrprCode09 = value; } }

        private System.String _PrprCode10;
        /// <summary>
        /// 
        /// </summary>
        public System.String PrprCode10 { get { return this._PrprCode10; } set { this._PrprCode10 = value; } }

        private System.String _PrprCode11;
        /// <summary>
        /// 
        /// </summary>
        public System.String PrprCode11 { get { return this._PrprCode11; } set { this._PrprCode11 = value; } }

        private System.String _PrprCode12;
        /// <summary>
        /// 
        /// </summary>
        public System.String PrprCode12 { get { return this._PrprCode12; } set { this._PrprCode12 = value; } }

        private System.String _PrprCode13;
        /// <summary>
        /// 
        /// </summary>
        public System.String PrprCode13 { get { return this._PrprCode13; } set { this._PrprCode13 = value; } }

        private System.String _PrprCode14;
        /// <summary>
        /// 
        /// </summary>
        public System.String PrprCode14 { get { return this._PrprCode14; } set { this._PrprCode14 = value; } }

        private System.String _PrprCode15;
        /// <summary>
        /// 
        /// </summary>
        public System.String PrprCode15 { get { return this._PrprCode15; } set { this._PrprCode15 = value; } }

        private System.String _PrprCode16;
        /// <summary>
        /// 
        /// </summary>
        public System.String PrprCode16 { get { return this._PrprCode16; } set { this._PrprCode16 = value; } }

        private System.Boolean? _ImpMark;
        /// <summary>
        /// 
        /// </summary>
        public System.Boolean? ImpMark { get { return this._ImpMark; } set { this._ImpMark = value; } }

        private System.String _ChiefDesigner;
        /// <summary>
        /// 
        /// </summary>
        public System.String ChiefDesigner { get { return this._ChiefDesigner; } set { this._ChiefDesigner = value; } }

        private System.String _ChiefDesignID;
        /// <summary>
        /// 
        /// </summary>
        public System.String ChiefDesignID { get { return this._ChiefDesignID; } set { this._ChiefDesignID = value; } }

        private System.String _MainMaterialCode;
        /// <summary>
        /// 
        /// </summary>
        public System.String MainMaterialCode { get { return this._MainMaterialCode; } set { this._MainMaterialCode = value; } }

        private System.String _MainMaterialName;
        /// <summary>
        /// 
        /// </summary>
        public System.String MainMaterialName { get { return this._MainMaterialName; } set { this._MainMaterialName = value; } }

        private System.String _MMaterial;
        /// <summary>
        /// 
        /// </summary>
        public System.String MMaterial { get { return this._MMaterial; } set { this._MMaterial = value; } }

        private System.String _FMaterial;
        /// <summary>
        /// 
        /// </summary>
        public System.String FMaterial { get { return this._FMaterial; } set { this._FMaterial = value; } }

        private System.DateTime? _AuditTime;
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? AuditTime { get { return this._AuditTime; } set { this._AuditTime = value; } }

        private System.Int32? _ImpMarkNum;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? ImpMarkNum { get { return this._ImpMarkNum; } set { this._ImpMarkNum = value; } }

        private System.Int32? _ChangeState;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? ChangeState { get { return this._ChangeState; } set { this._ChangeState = value; } }

        private System.String _ModelCode;
        /// <summary>
        /// 
        /// </summary>
        public System.String ModelCode { get { return this._ModelCode; } set { this._ModelCode = value; } }

        private System.String _ModelName;
        /// <summary>
        /// 
        /// </summary>
        public System.String ModelName { get { return this._ModelName; } set { this._ModelName = value; } }

        private System.String _RetnMode;
        /// <summary>
        /// 
        /// </summary>
        public System.String RetnMode { get { return this._RetnMode; } set { this._RetnMode = value; } }

        private System.String _ChangeCode;
        /// <summary>
        /// 
        /// </summary>
        public System.String ChangeCode { get { return this._ChangeCode; } set { this._ChangeCode = value; } }

        private System.String _ChangeName;
        /// <summary>
        /// 
        /// </summary>
        public System.String ChangeName { get { return this._ChangeName; } set { this._ChangeName = value; } }

        private System.String _Auditor;
        /// <summary>
        /// 
        /// </summary>
        public System.String Auditor { get { return this._Auditor; } set { this._Auditor = value; } }

        private System.String _ChangeStatus;
        /// <summary>
        /// 
        /// </summary>
        public System.String ChangeStatus { get { return this._ChangeStatus; } set { this._ChangeStatus = value; } }

        private System.DateTime? _dProperty1;
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? dProperty1 { get { return this._dProperty1; } set { this._dProperty1 = value; } }

        private System.DateTime? _dProperty2;
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? dProperty2 { get { return this._dProperty2; } set { this._dProperty2 = value; } }

        private System.DateTime? _dProperty3;
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? dProperty3 { get { return this._dProperty3; } set { this._dProperty3 = value; } }

        private System.DateTime? _dProperty4;
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? dProperty4 { get { return this._dProperty4; } set { this._dProperty4 = value; } }

        private System.DateTime? _dProperty5;
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? dProperty5 { get { return this._dProperty5; } set { this._dProperty5 = value; } }

        private System.DateTime? _dProperty6;
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? dProperty6 { get { return this._dProperty6; } set { this._dProperty6 = value; } }

        private System.String _OnDelivery;
        /// <summary>
        /// 
        /// </summary>
        public System.String OnDelivery { get { return this._OnDelivery; } set { this._OnDelivery = value; } }

        private System.String _IsCancelOM;
        /// <summary>
        /// 
        /// </summary>
        public System.String IsCancelOM { get { return this._IsCancelOM; } set { this._IsCancelOM = value; } }
    }
}