﻿using System;
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
                if (db.Queryable<CMT_FK>().With(SqlWith.NoLock).Where(m => m.HTH == HTH  && m.SHzt == "1").Count() > 0)
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
            var spdm = Request["spdm"];
            var page = int.Parse(Request["page"] ?? "1");
            var limit = int.Parse(Request["limit"] ?? "10");
            string[] name = { "B", "C", "K" };
            var list = await db.Queryable<SCZZD, SCZZDMX, SHANGPIN, JIJIE, GONGCHANG, CMT_FK>((s, sz, sp, jj, gc,cf) => new object[] {
                JoinType.Left,s.DJBH==sz.DJBH,
                JoinType.Left,sz.SPDM==sp.SPDM,
                JoinType.Left,sp.BYZD5==jj.JJDM,
                JoinType.Left,s.GCDM==gc.GCDM , //sp.FJSX6=="CMT"&&sp.BYZD8>=2018 && SqlFunc.ContainsArray(name,sp.BYZD3)
                JoinType.Left,s.HTH ==cf.HTH
            }).With(SqlWith.NoLock).Where((s, sz, sp, jj, gc,cf) => s.HTH.Contains("LX-C19")).WhereIF(!string.IsNullOrEmpty(spdm), s => s.SPDM == spdm)
            .GroupBy((s, sz, sp, jj, gc,cf) => new { s.SPDM, jj.JJMC, sp.BYZD8, s.HTH, gc.GCMC,cf.Money_1,cf.ZT, cf.TJzt,cf.SHzt,cf.SHzt2,cf.Remark,cf.jsRQ
            })
            .Select((s, sz, sp, jj, gc,cf) => new
            {
                s.SPDM,
                jj.JJMC,
                sp.BYZD8,
                s.HTH,
                gc.GCMC,
                cf.Money_1,
                cf.ZT,
                cf.TJzt,
                cf.SHzt,
                cf.SHzt2,
                cf.Remark,
                ZZRQ6 = SqlFunc.AggregateMin(s.ZZRQ6),
                JHRQ = SqlFunc.AggregateMin(s.JHRQ),
                SL = SqlFunc.AggregateSum(sz.SL),
                JE = SqlFunc.AggregateSum(sz.BYZD6),
                CPSL = SqlFunc.AggregateSum(sz.SL_2)
            }).OrderBy("cf.JSrq desc").ToListAsync();

            var listzp = await db.Queryable<SPJHD, SPJHDMX>((s, sz) => new object[] {
                 JoinType.Left,s.DJBH==sz.DJBH,
            }).With(SqlWith.NoLock).Where(s => s.DM2 == "0000").GroupBy((s, sz) => new { sz.SPDM })
            .Select((s, sz) => new
            {
                sz.SPDM,
                RKRQ = SqlFunc.AggregateMin(s.RQ),
                RKSL = SqlFunc.AggregateSum(sz.SL)
            }).ToListAsync();
            var listcp = await db.Queryable<SPJHD, SPJHDMX>((s, sz) => new object[] {
                 JoinType.Left,s.DJBH==sz.DJBH,
            }).With(SqlWith.NoLock).Where(s => s.DM2 == "0300").GroupBy((s, sz) => new { sz.SPDM })
          .Select((s, sz) => new
          {
              sz.SPDM,
              RKRQ1 = SqlFunc.AggregateMin(s.RQ),
              RKSL1 = SqlFunc.AggregateSum(sz.SL)
          }).ToListAsync();
            var datalist = from l1 in list
                           join l2 in listzp on l1.SPDM equals l2.SPDM into a
                           from r in a.DefaultIfEmpty()
                           join l3 in listcp on l1.SPDM equals l3.SPDM into b
                           from r1 in b.DefaultIfEmpty()
                           select new
                           {
                               l1.SPDM,
                               l1.JJMC,
                               l1.BYZD8,
                               l1.HTH,
                               l1.GCMC,
                               l1.Money_1,
                               l1.ZT,
                               l1.TJzt,
                               l1.SHzt,
                               l1.SHzt2,
                               l1.Remark,
                               l1.ZZRQ6,
                               l1.JHRQ,
                               HTSL = l1.SL,
                               HTJE = l1.JE,
                               l1.CPSL,
                               RKRQ = r != null ? r.RKRQ : null,
                               JHSL = r != null ? r.RKSL : null,
                               JHSL1 = r1 != null ? r1.RKSL1 : null
                           };
            var c = datalist.Skip((page - 1) * limit).Take(limit).ToList();
            return Json(new { code = 0, msg = "", count = datalist.Count(), data = c }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 入库
        /// </summary>
        /// <returns></returns>
        public async Task<JsonResult> IndexCMTRK(string spdm)
        {
            var page = int.Parse(Request["page"] ?? "1");
            var limit = int.Parse(Request["limit"] ?? "10");
            var list = await db.Queryable<SPJHD, SPJHDMX, SHANGPIN, GUIGE1>((sp, spjh, s, g) => new object[] {
                JoinType.Left,sp.DJBH==spjh.DJBH,
                JoinType.Left,spjh.SPDM==s.SPDM,
                JoinType.Left,spjh.GG1DM ==g.GGDM
            }).With(SqlWith.NoLock).Where((sp, spjh, s, g) => spjh.SPDM == spdm)
            .GroupBy((sp, spjh, s, g) => new { spjh.SPDM, s.SPMC, g.GGMC,sp.RQ })
            .Select((sp, spjh, s, g) => new
            {
                spjh.SPDM,
                s.SPMC,
                g.GGMC,
                RKsl = SqlFunc.AggregateSum(spjh.SL),
                sp.RQ
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