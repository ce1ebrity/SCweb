using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using SCWeb.Controllers;
using SCWeb.Models;
using SqlSugar;


namespace SCWeb
{
    public partial class Startup : BaseController
    {
        // 有关配置身份验证的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            //var j = db.Queryable<SHANGPIN>().WithCache().ToList();
            db.CodeFirst.InitTables(
                                    typeof(View_model_fobdaixiao), 
                                    typeof(FOBJS_FK), 
                                    typeof(MLJS), 
                                    typeof(_view_WFzzd), 
                                    typeof(ViewModel_json_fob),typeof(ViewModel_JSOn_ML)
                                    ,typeof(WFZZDfk),typeof(ViewModel_json_wf),typeof(CMT_FK),typeof(ViewModel_json_cmt)
                                    ,typeof(ViewModel_json_QDFH),typeof(_view_QDDBDD));

        }

    }
}