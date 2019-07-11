using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SqlSugar;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using SCWeb.Models;
using SCWeb.Helper;

namespace SCWeb.Controllers
{
    public class FOBdyController : BaseController
    {
        // GET: FOBdy
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult DYindex()
        {
            var data = Request["data"];
            var page = int.Parse(Request["page"] ?? "1");
            var limit = int.Parse(Request["limit"] ?? "10");
            List<ViewModel_json_fob> l;
            if (data == null)
            {
                var list = db.Queryable<ViewModel_json_fob,GONGHUOSHANG>((f,g)=>new object[] {
                   JoinType.Left,f.GCMC==g.GHSMC
                }).With(SqlWith.NoLock)
                .Select((f,g)=>new {
                    f.BYZD8,
                    f.JJMC,
                    f.GCMC,
                    f.HTH,
                    f.ZZRQ6,
                    f.JHRQ,
                    f.SPDM,
                    f.SL,
                    f.JE,
                    f.rksl,
                    f.rkrq,
                    f.Money_1,
                    f.Money_2,
                    f.Money_3,
                    f.SHzt,
                    f.TJZT,
                    f.hsje,
                    g.KHH,
                    g.ZH
                })
                .ToPageList(page,limit); 
                db.Deleteable<ViewModel_json_fob>().ExecuteCommand();
                return Json(new { code = 0, msg = "", count = list.Count(), data = list }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                JArray ja = (JArray)JsonConvert.DeserializeObject(data);
                l = ja.ToObject<List<ViewModel_json_fob>>();
                var t5 = db.Insertable(l).ExecuteCommand();
            }
            return Json(new { code = 0, msg = "", count = l.Count(), data = l.Skip((page - 1) * limit).Take(limit).ToList() }, JsonRequestBehavior.AllowGet);
        }

    }
}