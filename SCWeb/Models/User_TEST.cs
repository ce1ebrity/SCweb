using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SCWeb.Models
{
    public partial class User_TEST
    {
        public int SU_ID { get; set; }
        public string SU_CD { get; set; }
        public string SU_Name { get; set; }
        public string SU_PWD { get; set; }
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime SU_PWDSetDate { get; set; }


    }
}