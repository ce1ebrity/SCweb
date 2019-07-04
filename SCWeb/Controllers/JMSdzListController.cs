using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCWeb.Models;
using SqlSugar;
namespace SCWeb.Controllers
{
    public class JMSdzListController : BaseController
    {
        // GET: JMSdzList
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult IndexList(string name, string PP, string jijie, string year, string boduan)
        {
            var page = int.Parse(Request["page"] ?? "1");
            var limit = int.Parse(Request["limit"] ?? "10");
            var list = db.SqlQueryable<_view_QDDBDD>(qddbdd)
                .WhereIF(!string.IsNullOrEmpty(name), s => s.CKMC.Contains(name))
                .WhereIF(!string.IsNullOrEmpty(PP), s => s.BYZD3.Contains(PP))
                .WhereIF(!string.IsNullOrEmpty(jijie), s => s.JJMC.Contains(jijie))
                .WhereIF(!string.IsNullOrEmpty(year), s => s.BYZD8.Contains(year))
                .WhereIF(!string.IsNullOrEmpty(boduan), s => s.SXMC.Contains(boduan))
                .Select(s => new
                {
                    s.CKMC,
                    s.BYZD8,
                    s.BYZD3,
                    s.JJMC,
                    s.SXMC,
                    s.ZK,
                    s.ddsl,         //订单数
                    s.ddje,         //订单金额
                    s.ddnfhsl,      //订单内发货数量
                    s.ddnfhje,      //订单内发货金额
                    s.ddnthsl,      //订单内退货数量
                    s.ddnthje,      //订单内退货金额
                    s.ddnjfsl,      //订单内净发数量
                    s.ddnjfje,      //订单内净发金额
                    s.ddwfhsl,      //订单外发货数量
                    s.ddwfhje,      //订单外发货金额
                    s.ddwthsl,      //订单外退货数量
                    s.ddwthje,      //订单外退货金额
                    s.ddwjfsl,      //订单外净发数量
                    s.ddwjfje       //订单外净发金额
                });
            return Json(new { code = 0, msg = "", count = list.Count(), data = list.ToPageList(page, limit) }, JsonRequestBehavior.AllowGet);
            //var list = db.Queryable<DBJRD, DBJRDMX, SHANGPIN, JIJIE, FJSX2, CANGKU>((d, dmx, sp, jj, f2, ck) => new object[] {
            //    JoinType.Left,d.DJBH==dmx.DJBH,
            //    JoinType.Left,dmx.SPDM==sp.SPDM,
            //    JoinType.Left,jj.JJDM==sp.BYZD5,
            //    JoinType.Left,f2.SXDM==sp.FJSX2,
            //    JoinType.Left,d.DM1==ck.CKDM
            //}).With(SqlWith.NoLock)
            //.Where((d, dmx, sp, jj, f2, ck)=>sp.BYZD8>=2019).
            //WhereIF(!string.IsNullOrEmpty(name),(d, dmx, sp, jj, f2, ck)=>ck.CKMC.Contains(name)).
            //WhereIF(!string.IsNullOrEmpty(date),(d)=>d.RQ==SqlFunc.ToDate(date)).
            //GroupBy((d, dmx, sp, jj, f2, ck) => new
            //{
            //    ck.CKMC,
            //    sp.BYZD8,
            //    sp.BYZD3,
            //    jj.JJMC,
            //    f2.SXMC,
            //    dmx.ZK
            //})
            //.Select((d, dmx, sp, jj, f2, ck) => new
            //{
            //    ck.CKMC,
            //    sp.BYZD8,
            //    sp.BYZD3,
            //    jj.JJMC,
            //    f2.SXMC,
            //    dmx.ZK,
            //    sl = SqlFunc.AggregateSum(dmx.SL),
            //    je = SqlFunc.AggregateSum(dmx.JE)

            //});

        }
    }
}