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
    public class CMTdyController : BaseController
    {
        // GET: CMTdy
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult CMTindex()
        {
            var data = Request["data"];
            var page = int.Parse(Request["page"] ?? "1");
            var limit = int.Parse(Request["limit"] ?? "10");
            List<ViewModel_json_cmt> l;
            if (data == null)
            {
                var list = db.Queryable<ViewModel_json_cmt, GONGHUOSHANG>((f,g)=>new object[] {
                    JoinType.Left,f.GCMC ==g.GHSMC
                }).With(SqlWith.NoLock).Select((f,g) => new {
                    f.BYZD8,
                    f.JJMC,
                    f.GCMC,
                    f.HTH,
                    f.JGDJ,
                    f.ZZRQ6,
                    f.JHRQ,
                    f.SPDM,
                    f.CPSL,
                    f.thsl,
                    f.HTSL,
                    f.HTJE,
                    f.JHSL,
                    f.JHSL1,
                    f.JHSL2,
                    f.Money_1,
                    f.SHzt,
                    g.KHH,
                    g.ZH,
                })
                .ToPageList(page, limit);
                db.Deleteable<ViewModel_json_cmt>().ExecuteCommand();
                return Json(new { code = 0, msg = "", count = list.Count(), data = list }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                JArray ja = (JArray)JsonConvert.DeserializeObject(data);
                l = ja.ToObject<List<ViewModel_json_cmt>>();
                var t5 = db.Insertable(l).ExecuteCommand();
            }
            return Json(new { code = 0, msg = "", count = l.Count(), data = l.Skip((page - 1) * limit).Take(limit).ToList() }, JsonRequestBehavior.AllowGet);
        }
    }
}