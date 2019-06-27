using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCWeb.Helper
{
    /// <summary>
    /// 权限管理
    /// </summary>
    public class PropertyAttribute : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //throw new NotImplementedException();
        }

        //使用的菜单所关联的表
        public string MenuCode { get; set; }
        //操作
        public string MenuOperation { get; set; }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!string.IsNullOrWhiteSpace(MenuCode))
            {
                string userId = Common.GetCookie("userLogin");
                string sql = "select roleId from BPM_UserBase where id=@id";
                int roleId = Convert.ToInt32(SqlHelper.SelectSinger(sql, new SqlParameter("@id", userId)));

                sql = "select COUNT(1) from dbo.BPM_MenuPowerBase ";
                sql += "where id in ( ";
                sql += "select menuPId from dbo.BPM_PropertyManagr ";
                sql += "where roleId = @roleId and status = 1) and MenuCode = @MenuCode and menuName = @menuName and status=1 ";
                //判断用户是否有权限进行操作
                int count = Convert.ToInt32(SqlHelper.SelectSinger(sql, new SqlParameter("@roleId", roleId), new SqlParameter("@MenuCode", MenuCode), new SqlParameter("@menuName", MenuOperation)));
                if (count <= 0)
                {
                    filterContext.Result = new ContentResult()
                    {
                        Content = "Operation"
                    };
                }

            }
        }
    }
}