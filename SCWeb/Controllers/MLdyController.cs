using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SCWeb.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCWeb.Controllers
{
    public class MLdyController : BaseController
    {
        // GET: MLdy
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult DYindex()
        {
            var data = Request["data"];
            var page = int.Parse(Request["page"] ?? "1");
            var limit = int.Parse(Request["limit"] ?? "10");
            List<ViewModel_JSOn_ML> l = null;
            if (data == null)
            {
                var list = db.Queryable<ViewModel_JSOn_ML,GONGHUOSHANG>((m,g)=>new object[] {
                    JoinType.Left,m.GHSMC==g.GHSMC
                }).With(SqlWith.NoLock).Select((m,g)=>new {
                    m.GHSMC,
                    m.GHSDM,
                    m.BYZD8,
                    m.JJMC,
                    m.MLMC,
                    m.YDJH,
                    m.RQ,
                    m.YXRQ,
                    m.rq1,
                    m.SL,
                    m.sl1,
                    m.JE,
                    m.Money_1,
                    m.Money_2,
                    m.Money_3,
                    m.SHzt,
                    g.KHH,
                    g.ZH
                }).ToPageList(page, limit);
                db.Deleteable<ViewModel_JSOn_ML>().ExecuteCommand();
                return Json(new { code = 0, msg = "", count = list.Count(), data = list }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                JArray ja = (JArray)JsonConvert.DeserializeObject(data);
                l = ja.ToObject<List<ViewModel_JSOn_ML>>();
                var t5 = db.Insertable(l).ExecuteCommand();
            }
            return Json(new { code = 0, msg = "", count = l.Count(), data = l.Skip((page - 1) * limit).Take(limit).ToList() }, JsonRequestBehavior.AllowGet);

        }
    }
}