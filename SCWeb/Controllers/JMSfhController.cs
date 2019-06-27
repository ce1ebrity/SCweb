using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCWeb.Models;
using SqlSugar;

namespace SCWeb.Controllers
{
    public class JMSfhController : BaseController
    {
        // GET: JMSfh
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 加盟商发货
        /// </summary>
        /// <returns></returns>
        public JsonResult IndexList(string Name,string JMS)
        {
            var page = int.Parse(Request["page"] ?? "1");
            var limit = int.Parse(Request["limit"] ?? "10");
            var list = db.Queryable<QDDBD, QDDBDMX, SHANGPIN, CANGKU>((q, qmx, sp, ck) => new object[] {
                JoinType.Left,q.DJBH==qmx.DJBH,
                JoinType.Left,qmx.SPDM==sp.SPDM,
                JoinType.Left,q.DM1==ck.CKDM
            }).With(SqlWith.NoLock).Where((q, qmx, sp, ck) => sp.BYZD8 >= 2019)
            .WhereIF(!string.IsNullOrEmpty(Name),q=>q.DJBH.Contains(Name))
            .WhereIF(!string.IsNullOrEmpty(JMS),(q, qmx, sp, ck)=>ck.CKMC.Contains(JMS))
            .GroupBy((q, qmx, sp, ck) => new {
                ck.CKMC,
                q.DJBH,
                qmx.ZK,
                sp.BZSJ,
                q.BZ,
                q.RQ
            }).Select((q, qmx, sp, ck) => new
            {
                ck.CKMC,
                q.DJBH,
                qmx.ZK,
                SL = SqlFunc.AggregateSum(qmx.SL),
                sp.BZSJ,
                JE = SqlFunc.AggregateSum(qmx.JE),
                q.BZ,
                q.RQ
            }).OrderBy(q => q.RQ, OrderByType.Desc);
            return Json(new { code = 0, msg = "", count = list.Count(), data = list.ToPageList(page, limit) }, JsonRequestBehavior.AllowGet);
        }
    }
}