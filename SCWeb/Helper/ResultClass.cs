using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCWeb.Helper
{
    public class ResultClass
    {
        public ResultClass()
        {

        }

        public ResultClass(string Result, string Message)
        {
            this.Result = Result;
            this.Message = Message;
        }

        /// <summary>
        /// 返回结果
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// 返回消息
        /// </summary>
        public string Message { get; set; }


    }
}