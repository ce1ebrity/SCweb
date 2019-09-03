using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SCWeb.Models;
using SqlSugar;
namespace SCWeb.Controllers
{
    public class WFDYController : BaseController
    {
        // GET: WFDY
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult WFindex()
        {
            var data = Request["data"];
            var page = int.Parse(Request["page"] ?? "1");
            var limit = int.Parse(Request["limit"] ?? "10");
            List<ViewModel_json_wf> l;
            if (data == null)
            {
                var list = db.Queryable<ViewModel_json_wf>().With(SqlWith.NoLock)
                .Select((f) => new {
                    f.BYZD8,
                    f.JJMC,
                    f.GCMC,
                    f.HTH,
                    f.jgdj,
                    f.ZZRQ3,
                    f.JHRQ,
                    f.SPDM,
                    f.SL,
                    f.JE,
                    f.rksl,
                    f.rkrq,
                    f.Money_1,
                    f.Money_2,
                    f.Money_3,
                    f.Sdxdsl,
                    f.FKzt,
                    f.KHH,
                    f.ZH,f.DZ,f.Phone
                }).ToPageList(page, limit);
                db.Deleteable<ViewModel_json_wf>().ExecuteCommand();
                return Json(new { code = 0, msg = "", count = list.Count(), data = list }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                JArray ja = (JArray)JsonConvert.DeserializeObject(data);
                l = ja.ToObject<List<ViewModel_json_wf>>();
                var t5 = db.Insertable(l).ExecuteCommand();
            }
            return Json(new { code = 0, msg = "", count = l.Count(), data = l.Skip((page - 1) * limit).Take(limit).ToList() }, JsonRequestBehavior.AllowGet);
        }
    }
}