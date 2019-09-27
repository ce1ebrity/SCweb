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
                var list = db.Queryable<ViewModel_json_fob,GONGHUOSHANG,FOBJS_FK>((f,g,fobfk)=>new object[] {
                   JoinType.Left,f.GCMC==g.GHSMC,
                   JoinType.Left,f.HTH==fobfk.HTH && f.SPDM==fobfk.SPDM
                }).With(SqlWith.NoLock)
                .Select((f,g, fobfk) =>new {
                    //f.BYZD8,
                    //f.JJMC,
                    f.GCMC,
                    f.HTH,
                    f.JGDJ,
                    //f.ZZRQ6,
                    f.JHRQ,
                    f.SPDM,
                    f.SL,
                    f.JE,
                    f.rksl,
                    f.thsl,
                    f.rkrq,
                    f.Money_1,
                    f.Money_2,
                    f.Money_3,
                    //f.Sdxdsl,
                    f.SHzt,
                    f.TJZT,
                    f.hsje,
                    f.remark,
                    fobfk.tlkk,
                    fobfk.hqkk,
                    fobfk.cpkk,
                    //f.SCJD01,
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