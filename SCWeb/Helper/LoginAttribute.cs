using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCWeb.Helper
{
    public class LoginAttribute : FilterAttribute, IActionFilter
    {
        //表示是否检查登录
        public bool IsCheck { get; set; }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (IsCheck)
            {
                if (string.IsNullOrWhiteSpace(Common.GetCookie("userLogin")))
                {
                    filterContext.HttpContext.Response.Redirect("/HomePage/HomeLogin");
                }
            }
        }
    }
}