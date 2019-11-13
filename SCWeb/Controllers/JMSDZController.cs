using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SCWeb.Models;
using SqlSugar;

namespace SCWeb.Controllers
{
    public class JMSDZController : BaseController
    {
        // GET: JMSDZ
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult IndexList(string Name, string JMS)
        {
            var rq = Request["rq"];
            var page = int.Parse(Request["page"] ?? "1");
            var limit = int.Parse(Request["limit"] ?? "10");
            var list = db.Queryable<ViewModel_json_QDFH>().With(SqlWith.NoLock)
            .Where(u => u.selectlx != null)
            .WhereIF(!string.IsNullOrEmpty(Name), u => SqlFunc.StartsWith(u.DJBH,Name))
            .WhereIF(!string.IsNullOrEmpty(JMS), u => SqlFunc.StartsWith(u.CKMC,JMS))
            .WhereIF(!string.IsNullOrEmpty(rq), u => u.RQ == SqlFunc.ToDate(rq))
            .Select(u => new
            {
                u.BYZD3,
                u.SXMC,
                u.CKMC,
                u.BYZD8,
                u.RQ,
                u.BZ,
                u.DJBH,
                u.ZK,
                u.SL,
                u.BZSJ,
                u.JE,
                u.selectlx,
                u.LB,
                u.dd1,
                u.dd2,
                u.dd3
            }).PartitionBy(u => new { u.CKMC, u.DJBH,u.SXMC }).Take(1);
            return Json(new { code = 0, msg = "", count = list.Count(), data = list.ToPageList(page, limit) }, JsonRequestBehavior.AllowGet);
        }
    }
}