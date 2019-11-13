using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SCWeb.Helper;
using SCWeb.Models;
using SqlSugar;

namespace SCWeb.Controllers
{
    public class CPCMTController : BaseController
    {
        // GET: CPCMT
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Edit()
        {
            return View();
        }
        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="CMT_FL"></param>
        /// <returns></returns>
        public ActionResult CMT_TJ(CMT_FK CMT_FL)
        {
            var kp = Request["kp"];
            var je = Request["je"];
            var GCMC = Request["GCMC"];
            var HTH = Request["HTH"];
            var SPDM = Request["SPDM"];
            var remark = Request["remark"];
            if (db.Queryable<CMT_FK>().With(SqlWith.NoLock).Where(m => m.HTH == HTH && m.TJzt == "1").Count() > 0)
            {
                return Content("D");
            }
            else
            {
                db.Insertable<CMT_FK>(new
                {
                    GCMC = GCMC,
                    HTH = HTH,
                    SPDM = SPDM,
                    ZT = kp,
                    Money_1 = je,
                    TJzt = 1,
                    Remark = remark,
                    JSrq = DateTime.Now
                }).ExecuteCommand();
                return Content("y");
            }
        }
        /// <summary>
        /// 审核1
        /// </summary>
        /// <param name="wf"></param>
        /// <returns></returns>
        public ActionResult CMT_sh(CMT_FK wf)
        {
            string userId = Common.GetCookie("userLogin");
            var kp = Request["kp"];
            var je = Request["je"];
            var GCMC = Request["GCMC"];
            var HTH = Request["HTH"];
            var SPDM = Request["SPDM"];
            var remark = Request["remark"];
            if (db.Ado.SqlQuery<BPM_UserBase>(Usersql, new SugarParameter("@userId", userId)).Count() > 0)//&& m.TJzt == "1"
            {
                if (db.Queryable<CMT_FK>().With(SqlWith.NoLock).Where(m => m.HTH == HTH && m.SHzt == "1").Count() > 0)
                {
                    if (db.Updateable<CMT_FK>(new
                    {
                        GCMC = GCMC,
                        HTH = HTH,
                        SPDM = SPDM,
                        ZT = kp,
                        Money_1 = je,
                        SHzt2 = 1,
                        Remark = remark,
                        JSrq = DateTime.Now
                    }).Where(u => u.HTH == HTH).ExecuteCommand() > 0)
                    {
                        return Content("y");
                    }
                    else
                    {
                        return Content("n");
                    }
                }
                else
                {

                    return Content("1");
                }
            }
            else
            {
                if (db.Queryable<CMT_FK>().With(SqlWith.NoLock).Where(m => m.HTH == HTH && m.TJzt == "1").Count() > 0)
                {
                    if (db.Updateable<CMT_FK>(new
                    {
                        GCMC = GCMC,
                        HTH = HTH,
                        SPDM = SPDM,
                        ZT = kp,
                        Money_1 = je,
                        SHzt = 1,
                        Remark = remark,
                        JSrq = DateTime.Now
                    }).Where(u => u.HTH == HTH).ExecuteCommand() > 0)
                    {
                        return Content("y");
                    }
                    else
                    {
                        return Content("n");
                    }
                }
                else
                {
                    return Content("1");
                }

            }

        }
        public async Task<JsonResult> IndexCMTList()
        {
            var spdm = Request["spdm"].ToString().Trim(); ;
            var nameGC = Request["nameGC"].ToString().Trim(); ;
            var year = Request["year"].ToString().Trim(); ;
            var ji = Request["jijie"];
            var Name = Request["Name"].ToString().Trim(); ;
            var cmtzdr = Request["cmtzdr"].ToString().Trim(); ;
            var namebd = Request["namebd"].ToString().Trim(); ;
            var page = int.Parse(Request["page"] ?? "1");
            var limit = int.Parse(Request["limit"] ?? "10");
            string[] name = { "B", "C", "K" };
            var list = await db.Queryable<SCZZD, SCZZDMX, SHANGPIN, JIJIE, GONGCHANG, CMT_FK,FJSX2>((s, sz, sp, jj, gc, cf,bd) => new object[] {
                JoinType.Left,s.DJBH==sz.DJBH,
                JoinType.Left,sz.SPDM==sp.SPDM,
                JoinType.Left,sp.BYZD5==jj.JJDM,
                JoinType.Left,s.GCDM==gc.GCDM , //sp.FJSX6=="CMT"&&sp.BYZD8>=2018 && SqlFunc.ContainsArray(name,sp.BYZD3) SqlFunc.StartsWith(object thisValue, string parameterValue)
                JoinType.Left,s.HTH ==cf.HTH,
                JoinType.Left,sp.FJSX2==bd.SXDM
            }).With(SqlWith.NoLock).Where((s, sz, sp, jj, gc, cf) =>SqlFunc.StartsWith(s.HTH, "LX-C"))
            .WhereIF(!string.IsNullOrEmpty(spdm), s =>SqlFunc.EndsWith(s.SPDM,spdm))
             .WhereIF(!string.IsNullOrEmpty(Name), s => SqlFunc.StartsWith(s.HTH,Name))
              .WhereIF(!string.IsNullOrEmpty(nameGC), (s, sz, sp, jj, gc, cf) => SqlFunc.StartsWith(gc.GCMC,nameGC))
               .WhereIF(!string.IsNullOrEmpty(year), (s, sz, sp, jj, gc, cf) => sp.BYZD8 == SqlFunc.ToInt32(year))
               .WhereIF(!string.IsNullOrEmpty(ji), (s, sz, sp, jj, gc, cf) => SqlFunc.StartsWith(sp.BYZD5,ji))
               .WhereIF(!string.IsNullOrEmpty(cmtzdr),s=>SqlFunc.StartsWith(s.ZDR,cmtzdr))
                .WhereIF(!string.IsNullOrEmpty(namebd), (s, sz, sp, jj, gc, cf,bd) =>SqlFunc.StartsWith(bd.SXMC,namebd))
            .GroupBy((s, sz, sp, jj, gc, cf,bd) => new
            {
                s.SPDM,
                jj.JJMC,
                sp.BYZD8,
                s.HTH,
                gc.GCMC,
                gc.GHSDM,
                cf.Money_1,
                cf.ZT,
                cf.TJzt,
                cf.SHzt,
                cf.SHzt2,
                cf.Remark,
                cf.jsRQ,
                s.JGDJ,
                s.ZDR,
                bd.SXMC
            })
            .Select((s, sz, sp, jj, gc, cf,bd) => new
            {
                s.SPDM,
                jj.JJMC,
                sp.BYZD8,
                s.HTH,
                gc.GCMC,
                gc.GHSDM,
                cf.Money_1,
                cf.ZT,
                cf.TJzt,
                cf.SHzt,
                cf.SHzt2,
                cf.Remark,
                s.JGDJ,
                s.ZDR,
                bd.SXMC,
                ZZRQ6 = SqlFunc.AggregateMin(s.ZZRQ6),
                JHRQ = SqlFunc.AggregateMin(s.JHRQ),
                SL = SqlFunc.AggregateSum(sz.SL),
                JE = SqlFunc.AggregateSum(sz.BYZD6),
                CPSL = SqlFunc.AggregateSum(sz.SL_2)
            }).OrderBy("cf.JSrq desc").ToListAsync();

            var listzp = await db.Queryable<SPJHD, SPJHDMX, GONGHUOSHANG>((s, sz, ghs) => new object[] {
                 JoinType.Left,s.DJBH==sz.DJBH,
                 JoinType.Left,s.DM1==ghs.GHSDM
            }).With(SqlWith.NoLock).Where((s, sz, ghs) => s.DM2 == "0000" && ghs.TZSY == 0).GroupBy((s, sz, ghs) => new { sz.SPDM, ghs.GHSDM })
            .Select((s, sz, ghs) => new
            {
                sz.SPDM,
                ghs.GHSDM,
                RKRQ = SqlFunc.AggregateMin(s.RQ),
                RKSL = SqlFunc.AggregateSum(sz.SL)
            }).ToListAsync();
            var listcp = await db.Queryable<SPJHD, SPJHDMX, GONGHUOSHANG>((s, sz, ghs) => new object[] {
                 JoinType.Left,s.DJBH==sz.DJBH,
                 JoinType.Left,s.DM1==ghs.GHSDM
            }).With(SqlWith.NoLock).Where((s, sz, ghs) => s.DM2 == "0300" && ghs.TZSY == 0).GroupBy((s, sz, ghs) => new { sz.SPDM, ghs.GHSDM })
          .Select((s, sz, ghs) => new
          {
              sz.SPDM,
              ghs.GHSDM,
              RKRQ1 = SqlFunc.AggregateMin(s.RQ),
              RKSL1 = SqlFunc.AggregateSum(sz.SL)
          }).ToListAsync();
            var listcpfc = await db.Queryable<SPJHD, SPJHDMX, GONGHUOSHANG>((s, sz, ghs) => new object[] {
                 JoinType.Left,s.DJBH==sz.DJBH,
                 JoinType.Left,s.DM1==ghs.GHSDM
            }).With(SqlWith.NoLock).Where((s, sz, ghs) => s.DM2 == "0006" && ghs.TZSY == 0).GroupBy((s, sz, ghs) => new { sz.SPDM, ghs.GHSDM })
         .Select((s, sz, ghs) => new
         {
             sz.SPDM,
             ghs.GHSDM,
             RKRQ2 = SqlFunc.AggregateMin(s.RQ),
             RKSL2 = SqlFunc.AggregateSum(sz.SL)
         }).ToListAsync();

           var listth = await db.Queryable<SPTHD, SPTHDMX, GONGHUOSHANG>((s, sz, ghs) => new object[] {
                 JoinType.Left,s.DJBH==sz.DJBH,
                 JoinType.Left,s.DM1==ghs.GHSDM
            }).With(SqlWith.NoLock).Where((s, sz, ghs) => ghs.TZSY == 0).GroupBy((s, sz, ghs) => new { sz.SPDM, ghs.GHSDM })
        .Select((s, sz, ghs) => new
        {
            sz.SPDM,
            ghs.GHSDM,
            RKRQ2 = SqlFunc.AggregateMin(s.RQ),
            thsl = SqlFunc.AggregateSum(sz.SL)
        }).ToListAsync();
            var datalist = from l1 in list
                           join l2 in listzp on new { l1.SPDM, l1.GHSDM } equals new { l2.SPDM, l2.GHSDM } into a
                           from r in a.DefaultIfEmpty()
                           join l3 in listcp on new { l1.SPDM, l1.GHSDM } equals new { l3.SPDM, l3.GHSDM } into b
                           from r1 in b.DefaultIfEmpty()
                           join l4 in listcpfc on new { l1.SPDM, l1.GHSDM } equals new { l4.SPDM, l4.GHSDM } into cc
                           from r2 in cc.DefaultIfEmpty()
                           join l5 in listth on new { l1.SPDM, l1.GHSDM } equals new {l5.SPDM,l5.GHSDM } into ccth
                           from r3 in ccth.DefaultIfEmpty()
                           select new
                           {
                               l1.SPDM,
                               l1.JJMC,
                               l1.BYZD8,
                               l1.HTH,
                               l1.GCMC,
                               l1.GHSDM,
                               l1.Money_1,
                               l1.ZT,
                               l1.TJzt,
                               l1.SHzt,
                               l1.SHzt2,
                               l1.Remark,
                               l1.ZZRQ6,
                               l1.JHRQ,
                               l1.JGDJ,
                               l1.ZDR,
                               l1.SXMC,
                               HTSL = l1.SL,
                               HTJE = l1.JE,
                               l1.CPSL,
                               RKRQ = r != null ? r.RKRQ : null,
                               JHSL = r != null ? r.RKSL : null,
                               JHSL1 = r1 != null ? r1.RKSL1 : null,
                               JHSL2 = r2 != null ? r2.RKSL2 : null,
                               thsl = r3 != null ? r3.thsl : null
                           };
            return Json(new { code = 0, msg = "", count = datalist.Count(), data = datalist.Skip((page - 1) * limit).Take(limit).ToList() }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// cmt合同资料
        /// </summary>
        /// <param name="spdm"></param>
        /// <param name="hth"></param>
        /// <returns></returns>
        public async Task<JsonResult> IndexCMTHT(string spdm, string hth)
        {

            var list = await db.Queryable<SCZZD, SCZZDMX, GUIGE1, GUIGE2>((s, sz, g1, g2) => new object[] {
                JoinType.Left,s.DJBH==sz.DJBH,
                JoinType.Left,sz.GG1DM==g1.GGDM,
                JoinType.Left,sz.GG2DM==g2.GGDM
            }).With(SqlWith.NoLock).Where(s => s.SPDM == spdm && s.HTH == hth).GroupBy((s, sz, g1, g2) => new { s.SPDM, s.JGDJ, s.JHRQ, col = g1.GGMC, cm = g2.GGMC }).
            Select((s, sz, g1, g2) => new
            {
                s.SPDM,
                s.JHRQ,
                col = g1.GGMC,
                cm = g2.GGMC,
                htsl = SqlFunc.AggregateSum(sz.SL),
                DJ = s.JGDJ,
                htje = SqlFunc.AggregateSum(sz.BYZD6)

            }).ToListAsync();
            var sdxdsl = await db.SqlQueryable<VIEWMODEL_SDXDSL>(sql33).Where(s => s.SPDM == spdm).Select(s => new
            {
                s.SPDM,
                s.Sl,
                s.col,
                s.cm
            }).ToListAsync();
            var listdata = from l1 in list
                           join l2 in sdxdsl on new { l1.SPDM, l1.col, l1.cm } equals new { l2.SPDM, l2.col, l2.cm } into a
                           from r in a.DefaultIfEmpty()
                           select new
                           {
                               l1.SPDM,
                               l1.JHRQ,
                               l1.col,
                               l1.cm,
                               l1.htsl,
                               l1.DJ,
                               l1.htje,
                               XDSL = r != null ? r.Sl : 0,
                           };

            return Json(new { code = 0, msg = "", count = listdata.Count(), data = listdata.ToList() }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 入库
        /// </summary>
        /// <returns></returns>
        public async Task<JsonResult> IndexCMTRK(string spdm, string GCMC,string GHSDM)
        {
            string gcmc = Server.UrlDecode(GCMC);
            var page = int.Parse(Request["page"] ?? "1");
            var limit = int.Parse(Request["limit"] ?? "10");
            var list = await db.Queryable<SPJHD, SPJHDMX, SHANGPIN, GUIGE1, GUIGE2, GONGHUOSHANG>((sp, spjh, s, g, g2, ghs) => new object[] {
                JoinType.Left,sp.DJBH==spjh.DJBH,
                JoinType.Left,spjh.SPDM==s.SPDM,
                JoinType.Left,spjh.GG1DM ==g.GGDM,
                JoinType.Left,spjh.GG2DM==g2.GGDM,
                JoinType.Left,sp.DM1==ghs.GHSDM
            }).With(SqlWith.NoLock).Where((sp, spjh, s, g, g2, ghs) => spjh.SPDM == spdm && ghs.GHSDM == GHSDM)
            .GroupBy((sp, spjh, s, g, g2, ghs) => new
            {
                spjh.SPDM,
                ghs.GHSMC,
                s.SPMC,
                col = g.GGMC,
                cm = g2.GGMC,
                sp.RQ,
                sp.DM2
            })
            .Select((sp, spjh, s, g, g2, ghs) => new
            {
                spjh.SPDM,
                ghs.GHSMC,
                s.SPMC,
                col = g.GGMC,
                cm = g2.GGMC,
                RKsl = SqlFunc.AggregateSum(spjh.SL),
                sp.RQ,
                sp.DM2
            }).ToPageListAsync(page, limit);
            return Json(new { code = 0, msg = "", count = list.Count(), data = list }, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> IndexCMTth(string spdm, string GCMC,string GHSDM)
        {
            string gcmc = Server.UrlDecode(GCMC);
            var page = int.Parse(Request["page"] ?? "1");
            var limit = int.Parse(Request["limit"] ?? "10");
            var list = await db.Queryable<SPTHD, SPTHDMX, SHANGPIN, GUIGE1, GUIGE2, GONGHUOSHANG>((sp, spjh, s, g, g2, ghs) => new object[] {
                JoinType.Left,sp.DJBH==spjh.DJBH,
                JoinType.Left,spjh.SPDM==s.SPDM,
                JoinType.Left,spjh.GG1DM ==g.GGDM,
                JoinType.Left,spjh.GG2DM==g2.GGDM,
                JoinType.Left,sp.DM1==ghs.GHSDM
            }).With(SqlWith.NoLock).Where((sp, spjh, s, g, g2, ghs) => spjh.SPDM == spdm && ghs.GHSDM == GHSDM)
            .GroupBy((sp, spjh, s, g, g2, ghs) => new
            {
                spjh.SPDM,
                ghs.GHSMC,
                s.SPMC,
                col = g.GGMC,
                cm = g2.GGMC,
                sp.RQ,
                sp.DM2
            })
            .Select((sp, spjh, s, g, g2, ghs) => new
            {
                spjh.SPDM,
                ghs.GHSMC,
                s.SPMC,
                col = g.GGMC,
                cm = g2.GGMC,
                RKsl = SqlFunc.AggregateSum(spjh.SL),
                sp.RQ,
                sp.DM2
            }).ToPageListAsync(page, limit);
            return Json(new { code = 0, msg = "", count = list.Count(), data = list }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="spdm"></param>
        /// <returns></returns>
        public async Task<JsonResult> IndexCMTCPCP(string spdm)
        {
            var page = int.Parse(Request["page"] ?? "1");
            var limit = int.Parse(Request["limit"] ?? "10");
            //db.CodeFirst.InitTables(typeof(viewModel_SPJHD));
            var list123 = await db.SqlQueryable<viewModel_SPJHD>(sql2).WhereIF(!string.IsNullOrEmpty(spdm), U => U.SPDM == spdm)//.Where(u => u.RQ > u.jhRQ)
                .Select((u) => new
                {
                    u.SPDM,
                    u.cpsl,
                    u.jhsl,
                    u.jhsl1,
                }).ToPageListAsync(page, limit);
            return Json(new { code = 0, msg = "", count = list123.Count(), data = list123 }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 面料领料单
        /// </summary>
        /// <returns></returns>
        public async Task<JsonResult> IndexCMTmL(string SPDM)
        {
            var page = int.Parse(Request["page"] ?? "1");
            var limit = int.Parse(Request["limit"] ?? "10");
            var list = await db.Queryable<MLLLD, MLLLDMX, MIANLIAO>((m, mx, ml) => new object[] {
                JoinType.Left,m.DJBH==mx.DJBH,
                JoinType.Left,mx.MLDM ==ml.MLDM
            }).With(SqlWith.NoLock).Where(m => m.SPDM == SPDM)
            .GroupBy((m, mx, ml) => new { mx.MLDM, ml.MLMC }).Select((m, mx, ml) => new
            {
                mx.MLDM,
                ml.MLMC,
                MLSL = SqlFunc.AggregateSum(mx.SL),
            }).ToListAsync();
            var list2 = await db.Queryable<GYPLD, GYPLDML1, MIANLIAO>((m, mx, ml) => new object[] {
                JoinType.Left,m.DJBH==mx.DJBH,
                JoinType.Left,mx.MLDM ==ml.MLDM
            }).With(SqlWith.NoLock).Where(m => m.SPDM == SPDM)
            .GroupBy((m, mx, ml) => new { mx.MLDM, ml.MLMC }).Select((m, mx, ml) => new
            {
                mx.MLDM,
                ml.MLMC,
                DJYLSL = SqlFunc.AggregateSum(mx.SL),
            }).ToListAsync();
            var list3 = await db.Queryable<MLTLD, MLTLDMX, MIANLIAO>((m, mx, ml) => new object[] {
                JoinType.Left,m.DJBH==mx.DJBH,
                JoinType.Left,mx.MLDM ==ml.MLDM
            }).With(SqlWith.NoLock).Where(m => m.SPDM == SPDM)
          .GroupBy((m, mx, ml) => new { mx.MLDM, ml.MLMC }).Select((m, mx, ml) => new
          {
              mx.MLDM,
              ml.MLMC,
              MLTSL = SqlFunc.AggregateSum(mx.SL),
          }).ToListAsync();
            var listdata = from l1 in list
                           join l2 in list2 on l1.MLDM equals l2.MLDM into a
                           from r in a.DefaultIfEmpty()
                           join l3 in list3 on l1.MLDM equals l3.MLDM into b
                           from r1 in b.DefaultIfEmpty()
                           select new
                           {
                               l1.MLDM,
                               l1.MLMC,
                               l1.MLSL,
                               DJYLSL = r != null ? r.DJYLSL : null,
                               MLTSL = r1 != null ? r1.MLTSL : null,

                           };
            return Json(new { code = 0, msg = "", count = listdata.Count(), data = listdata.Skip((page - 1) * limit).Take(limit).ToList() }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 辅料领料单
        /// </summary>
        /// <param name="SPDM"></param>
        /// <returns></returns>
        public async Task<JsonResult> IndexCMTmLfl(string SPDM)
        {
            var page = int.Parse(Request["page"] ?? "1");
            var limit = int.Parse(Request["limit"] ?? "10");
            var list = await db.Queryable<FLLLD, FLLLDMX, FULIAO>((m, mx, ml) => new object[] {
                JoinType.Left,m.DJBH==mx.DJBH,
                JoinType.Left,mx.FLDM ==ml.FLDM
            }).With(SqlWith.NoLock).Where(m => m.SPDM == SPDM)
            .GroupBy((m, mx, ml) => new { mx.FLDM, ml.FLMC }).Select((m, mx, ml) => new
            {
                mx.FLDM,
                ml.FLMC,
                FLSL = SqlFunc.AggregateSum(mx.SL),
            }).ToListAsync();
            var list3 = await db.Queryable<FLTLD, FLTLDMX, FULIAO>((m, mx, ml) => new object[] {
                JoinType.Left,m.DJBH==mx.DJBH,
                JoinType.Left,mx.FLDM ==ml.FLDM
            }).With(SqlWith.NoLock).Where(m => m.SPDM == SPDM)
          .GroupBy((m, mx, ml) => new { mx.FLDM, ml.FLMC }).Select((m, mx, ml) => new
          {
              mx.FLDM,
              ml.FLMC,
              FLTSL = SqlFunc.AggregateSum(mx.SL),
          }).ToListAsync();
            var listdata = from l1 in list
                           join l3 in list3 on l1.FLDM equals l3.FLDM into b
                           from r1 in b.DefaultIfEmpty()
                           select new
                           {
                               l1.FLDM,
                               l1.FLMC,
                               l1.FLSL,
                               FLTSL = r1 != null ? r1.FLTSL : null,

                           };
            return Json(new { code = 0, msg = "", count = listdata.Count(), data = listdata.Skip((page - 1) * limit).Take(limit).ToList() }, JsonRequestBehavior.AllowGet);
        }
    }
}