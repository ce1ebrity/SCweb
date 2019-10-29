using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCWeb.Controllers
{
    public class BasicDataController : Controller
    {
        // GET: BasicData
        public ActionResult Index()
        {
            return View();
        }
    }
}