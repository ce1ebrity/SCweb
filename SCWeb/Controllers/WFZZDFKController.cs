using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCWeb.Models;
using SqlSugar;
namespace SCWeb.Controllers
{
    public class WFZZDFKController : BaseController
    {
        // GET: WFZZDFK
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult INdexList()
        {
            var page = int.Parse(Request["page"] ?? "1");
            var limit = int.Parse(Request["limit"] ?? "10");
            var list = db.Queryable<WFZZDfk>().With(SqlWith.NoLock).ToPageList(page,limit);
            return Json(new { code = 0, msg = "", count = db.Queryable<WFZZDfk>().Count(), data = list }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ADD()
        {
            return View();
        }
        public ActionResult UpdateInfoList(WFZZDfk _wf)
        {
            //var Id = int.Parse(Request["Id"]);
            //var GHS = Request["GHS"];
            //var GHSDZ = Request["GHSDZ"];
            //var GHSPhone = Request["GHSPhone"];
            //var GHSKHH = Request["GHSKHH"];
            //var GHSZH = Request["GHSZH"];
            if (db.Updateable(_wf).ExecuteCommand() > 0)
            {
                return Content("1");
            }
            else
            {
                return Content("n");
            }
        }
        public ActionResult SUBmit(WFZZDfk _wffk)
        {
            var ghsmc = Request["ghsmc"];
            var dz = Request["dz"];
            var phone = Request["phone"];
            var khh = Request["khh"];
            var zh = Request["zh"];
            if (db.Insertable<WFZZDfk>(new
            {
                GHS = ghsmc,
                GHSDZ = dz,
                GHSPhone = phone,
                GHSKHH = khh,
                GHSZH = zh
            }).ExecuteCommand() > 0)
            {
                return Content("y");
            }
            else
            {
                return Content("n");
            }
        }
        public JsonResult QueryList()
        {

           var list = db.Queryable<WFZZDfk>().Select(u => new
            {
                u.Id,
                u.GHSDZ,
                u.GHSKHH,
                u.GHSPhone,u.GHSZH
            }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}