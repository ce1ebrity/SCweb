using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Newtonsoft.Json.Converters;
using SCWeb.Helper;
using SCWeb.Models;
using Newtonsoft.Json;
using System.IO;
using NPOI.HSSF.UserModel;

namespace SCWeb.Controllers
{
    public class InstallController : Controller
    {          
        // GET: Supplier

        /// <summary>
        /// 供应商加载
        /// </summary>
        /// <returns></returns>
        public ActionResult Authority()
        {
            return View();
        }
        public ActionResult Authorit1y()
        {
            return View();
        }
    }
}