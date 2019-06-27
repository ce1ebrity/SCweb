using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCWeb.Models
{
    public class BPM_CargoModels
    {
        public string id { get; set; }
        public string Sample01 { get; set; }
        public string Sample02 { get; set; }
        public string Sample03 { get; set; }
        public string Sample04 { get; set; }
        public string Sample05 { get; set; }
        public string Sample06 { get; set; }
        public string Sample07 { get; set; }
        public string Sample08 { get; set; }
        public string Sample09 { get; set; }
        public string Sample10 { get; set; }
        public string Sample11 { get; set; }
        public string Sample12 { get; set; }
        public string Sample13 { get; set; }   //登录人姓名
        public string Sample14 { get; set; }   //审核人姓名
        public string Sample15 { get; set; }   //对应的样衣BOM单号
        public string Sample16 { get; set; }   //图片
        public string Sample17 { get; set; }   //波段ID
        public string Sample18 { get; set; }   //设计师ID
        public string Sample19 { get; set; }   //款式类别ID
        public string Sample20 { get; set; }   //大类ID
        public string Sample21 { get; set; }   //颜色ID
        public string Sample22 { get; set; }   //bom颜色
        public string pageCount { get; set; }
        public string RowsCount { get; set; }
        public int status { get; set; }
    }

    public class BPM_CargoMXModels
    {
        public string id { get; set; }
        public string Sampleid { get; set; }
        public string SampleMX01 { get; set; }
        //public string SampleMX02 { get; set; }   --组别
        public string SampleMX03 { get; set; }
        public string SampleMX04 { get; set; }
        public string SampleMX05 { get; set; }
        public string SampleMX06 { get; set; }
        public string SampleMX07 { get; set; }
        public string SampleMX08 { get; set; }
        public string SampleMX09 { get; set; }
        public string SampleMX10 { get; set; }
        public string SampleMX11 { get; set; }
        public string SampleMX12 { get; set; }
        public string SampleMX13 { get; set; }
        public decimal SampleMX14 { get; set; }
        public decimal SampleMX15 { get; set; }
        public decimal SampleMX16 { get; set; }
        public string SampleMX17 { get; set; }
        public string SampleMX18 { get; set; }
        public string SampleMX19 { get; set; }
        public int status { get; set; }
    }
}