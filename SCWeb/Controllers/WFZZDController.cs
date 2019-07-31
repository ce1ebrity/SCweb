﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SCWeb.Helper;
using SCWeb.Models;
using SqlSugar;

namespace SCWeb.Controllers
{
    public class WFZZDController : BaseController
    {
        // GET: WFZZD
        int[] zs = { 1, 2, 3 };
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Edit()
        {
            return View();
        }
        public ActionResult Edit2()
        {
            return View();
        }
        public ActionResult Edit3()
        {
            return View();
        }
        //private void GetParam(HttpContext context)
        //{
        //    ViewModel_json_fob param = new ViewModel_json_fob();

        //    //接收参数(反序列化JSON数据)
        //    ViewModel_json_fob _param = new JavaScriptSerializer().Deserialize<ViewModel_json_fob>(context.Request["param"]);
        //}
        /// <summary>
        /// 外发入库
        /// </summary>
        /// <param name="spdm"></param>
        /// <returns></returns>
        public async Task<JsonResult> Wfrkd(string spdm)
        {
            var page = int.Parse(Request["page"] ?? "1");
            var limit = int.Parse(Request["limit"] ?? "10");
            var list = await db.Queryable<SPJHD, SPJHDMX>((s, sp) => new object[] {
                JoinType.Left,s.DJBH==sp.DJBH //s.DM2 == "0000" &&
            }).With(SqlWith.NoLock).Where((s, sp) => sp.SPDM == spdm).GroupBy((s, sp) => new { sp.SPDM, sp.DJ, s.RQ, s.DM2 }).Select((s, sp) => new
            {
                sp.SPDM,
                RKSL = SqlFunc.AggregateSum(sp.SL),
                s.RQ,
                hsDJ = sp.DJ,
                hsje = SqlFunc.AggregateSum(sp.JE),
                s.DM2
            }).ToPageListAsync(page, limit);
            return Json(new { code = 0, msg = "", count = list.Count(), data = list }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult WF_10TJ(_view_WFzzd wf)
        {
            var kp = Request["kp"];
            var je_10 = Request["je"];
            var GCMC = Request["GCMC"];
            var HTH = Request["HTH"];
            var SPDM = Request["SPDM"];
            var remark = Request["remark"];
            var selectkhh = Request["selectkhh"];
            var selectzh = Request["selectzh"];
            var selectdz = Request["selectdz"];
            var selectphone = Request["selectphone"];
            if (db.Queryable<_view_WFzzd>().With(SqlWith.NoLock).Where(m => m.HTH == HTH && m.TJzt == "1").Count() > 0)
            {
                return Content("D");
            }
            else
            {
                db.Insertable<_view_WFzzd>(new
                {
                    GCMC = GCMC,
                    HTH = HTH,
                    SPDM = SPDM,
                    ISfk = kp,
                    Money_1 = je_10,
                    TJzt = 1,
                    Remark = remark,
                    KHH = selectkhh,
                    ZH = selectzh,
                    DZ = selectdz,
                    Phone = selectphone,
                    JSrq = DateTime.Now
                }).ExecuteCommand();
                return Content("y");
            }
        }
        /// <summary>
        /// 尾款
        /// </summary>
        /// <param name="wf"></param>
        /// <returns></returns>
        public ActionResult WF_10TJ_1(_view_WFzzd wf)
        {
            var kp = Request["kp"];
            var je_10 = Request["je"];
            var GCMC = Request["GCMC"];
            var HTH = Request["HTH"];
            var SPDM = Request["SPDM"];
            var remark = Request["remark"];
            var selectkhh = Request["selectkhh"];
            var selectzh = Request["selectzh"];
            var selectdz = Request["selectdz"];
            var selectphone = Request["selectphone"];
            if (db.Queryable<_view_WFzzd>().With(SqlWith.NoLock).Where(m => m.HTH == HTH && m.TJzt == "2").Count() > 0)
            {
                return Content("D");
            }
            else
            {
                db.Updateable<_view_WFzzd>(new
                {
                    GCMC = GCMC,
                    HTH = HTH,
                    SPDM = SPDM,
                    ISfk = kp,
                    Money_2 = je_10,
                    TJzt = 2,
                    Remark = remark,
                    KHH = selectkhh,
                    ZH = selectzh,
                    DZ = selectdz,
                    Phone = selectphone,
                    JSrq = DateTime.Now
                }).Where(u => u.HTH == HTH).ExecuteCommand();
                return Content("y");
            }
        }
        /// <summary>
        /// 报账
        /// </summary>
        /// <param name="wf"></param>
        /// <returns></returns>
        public ActionResult WF_10TJ_2(_view_WFzzd wf)
        {
            var kp = Request["kp"];
            var dj = Request["dj"];
            var summoney = Request["summoney"];
            var je_10 = Request["je"];
            var GCMC = Request["GCMC"];
            var HTH = Request["HTH"];
            var SPDM = Request["SPDM"];
            var remark = Request["remark"];
            var selectkhh = Request["selectkhh"];
            var selectzh = Request["selectzh"];
            var selectdz = Request["selectdz"];
            var selectphone = Request["selectphone"];
            if (db.Queryable<_view_WFzzd>().With(SqlWith.NoLock).Where(m => m.HTH == HTH && m.TJzt == "3").Count() > 0)
            {
                return Content("D");
            }
            else
            {
                db.Updateable<_view_WFzzd>(new
                {
                    GCMC = GCMC,
                    HTH = HTH,
                    SPDM = SPDM,
                    ISfk = kp,
                    Money_1 = dj,
                    Money_2 = je_10,
                    Money_3 = summoney,
                    TJzt = 3,
                    Remark = remark,
                    KHH = selectkhh,
                    ZH = selectzh,
                    DZ = selectdz,
                    Phone = selectphone,
                    JSrq = DateTime.Now
                }).Where(u => u.HTH == HTH).ExecuteCommand();
                return Content("y");
            }
        }
        /// <summary>
        /// 审核2
        /// </summary>
        /// <param name="wf"></param>
        /// <returns></returns>
        public ActionResult WF_10_1(_view_WFzzd wf)
        {
            string userId = Common.GetCookie("userLogin");
            var kp = Request["kp"];
            var je_10 = Request["je"];
            var GCMC = Request["GCMC"];
            var HTH = Request["HTH"];
            var SPDM = Request["SPDM"];
            var remark = Request["remark"];
            var selectkhh = Request["selectkhh"];
            var selectzh = Request["selectzh"];
            var selectdz = Request["selectdz"];
            var selectphone = Request["selectphone"];
            if (db.Ado.SqlQuery<BPM_UserBase>(Usersql, new SugarParameter("@userId", userId)).Count() > 0)//m.TJzt=="2" &&
            {
                if (db.Queryable<_view_WFzzd>().With(SqlWith.NoLock).Where(m => m.HTH == HTH && m.TJzt == "2").Count() > 0)
                {
                    if (db.Updateable<_view_WFzzd>(new
                    {
                        GCMC = GCMC,
                        HTH = HTH,
                        SPDM = SPDM,
                        ISfk = kp,
                        Money_2 = je_10,
                        SHzt2 = 2,
                        FKzt = 2,
                        Remark = remark,
                        KHH = selectkhh,
                        ZH = selectzh,
                        DZ = selectdz,
                        Phone = selectphone,
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

                    return Content("D");
                }
            }
            else
            {
                if (db.Queryable<_view_WFzzd>().With(SqlWith.NoLock).Where(m => m.HTH == HTH && m.TJzt == "2").Count() > 0)
                {
                    if (db.Updateable<_view_WFzzd>(new
                    {
                        GCMC = GCMC,
                        HTH = HTH,
                        SPDM = SPDM,
                        ISfk = kp,
                        Money_2 = je_10,
                        FKzt = 2,
                        Remark = remark,
                        KHH = selectkhh,
                        ZH = selectzh,
                        DZ = selectdz,
                        Phone = selectphone,
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
                    return Content("D");
                }
            }


        }
        /// <summary>
        /// 审核3
        /// </summary>
        /// <param name="wf"></param>
        /// <returns></returns>
        public ActionResult WF_10_2(_view_WFzzd wf)
        {
            string userId = Common.GetCookie("userLogin");
            var kp = Request["kp"];
            var dj = Request["dj"];
            var summoney = Request["summoney"];
            var je_10 = Request["je"];
            var GCMC = Request["GCMC"];
            var HTH = Request["HTH"];
            var SPDM = Request["SPDM"];
            var remark = Request["remark"];
            var selectkhh = Request["selectkhh"];
            var selectzh = Request["selectzh"];
            var selectdz = Request["selectdz"];
            var selectphone = Request["selectphone"];
            if (db.Ado.SqlQuery<BPM_UserBase>(Usersql, new SugarParameter("@userId", userId)).Count() > 0)
            {
                if (db.Queryable<_view_WFzzd>().With(SqlWith.NoLock).Where(m => m.HTH == HTH && m.TJzt == "3").Count() > 0)
                {
                    if (db.Updateable<_view_WFzzd>(new
                    {
                        GCMC = GCMC,
                        HTH = HTH,
                        SPDM = SPDM,
                        ISfk = kp,
                        Money_1 = dj,
                        Money_2 = je_10,
                        Money_3 = summoney,
                        SHzt2 = 3,
                        FKzt = 3,
                        Remark = remark,
                        KHH = selectkhh,
                        ZH = selectzh,
                        DZ = selectdz,
                        Phone = selectphone,
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

                    return Content("D");
                }
            }
            else
            {
                if (db.Queryable<_view_WFzzd>().With(SqlWith.NoLock).Where(m => m.HTH == HTH && m.TJzt == "3").Count() > 0)
                {
                    if (db.Updateable<_view_WFzzd>(new
                    {
                        GCMC = GCMC,
                        HTH = HTH,
                        SPDM = SPDM,
                        ISfk = kp,
                        Money_1 = dj,
                        Money_2 = je_10,
                        Money_3 = summoney,
                        FKzt = 3,
                        Remark = remark,
                        KHH = selectkhh,
                        ZH = selectzh,
                        DZ = selectdz,
                        Phone = selectphone,
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
                    return Content("D");
                }
            }


        }
        /// <summary>
        /// 审核1
        /// </summary>
        /// <param name="wf"></param>
        /// <returns></returns>
        public ActionResult WF_10(_view_WFzzd wf)
        {
            string userId = Common.GetCookie("userLogin");
            var kp = Request["kp"];
            var je_10 = Request["je"];
            var GCMC = Request["GCMC"];
            var HTH = Request["HTH"];
            var SPDM = Request["SPDM"];
            var remark = Request["remark"];
            var selectkhh = Request["selectkhh"];
            var selectzh = Request["selectzh"];
            var selectdz = Request["selectdz"];
            var selectphone = Request["selectphone"];
            if (db.Ado.SqlQuery<BPM_UserBase>(Usersql, new SugarParameter("@userId", userId)).Count() > 0)
            {
                if (db.Queryable<_view_WFzzd>().With(SqlWith.NoLock).Where(m => m.HTH == HTH && SqlFunc.ContainsArray(zs, m.TJzt)).Count() > 0)
                {
                    if (db.Updateable<_view_WFzzd>(new
                    {
                        GCMC = GCMC,
                        HTH = HTH,
                        SPDM = SPDM,
                        ISfk = kp,
                        Money_1 = je_10,
                        SHzt2 = 1,
                        FKzt = 1,
                        Remark = remark,
                        KHH = selectkhh,
                        ZH = selectzh,
                        DZ = selectdz,
                        Phone = selectphone,
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
                if (db.Queryable<_view_WFzzd>().With(SqlWith.NoLock).Where(m => m.HTH == HTH && m.TJzt == "1").Count() > 0)
                {
                    if (db.Updateable<_view_WFzzd>(new
                    {
                        GCMC = GCMC,
                        HTH = HTH,
                        SPDM = SPDM,
                        ISfk = kp,
                        Money_1 = je_10,
                        FKzt = 1,
                        Remark = remark,
                        KHH = selectkhh,
                        ZH = selectzh,
                        DZ = selectdz,
                        Phone = selectphone,
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
        public async Task<JsonResult> WfzzdHT(string spdm)
        {
            var list = await db.Queryable<Wfzzd, WFzzdmx>((s, sz) => new object[] {
                JoinType.Left,s.djbh==sz.djbh
            }).With(SqlWith.NoLock).Where((s, sz) => sz.spdm == spdm)
            .GroupBy((s, sz) => new { sz.spdm, sz.jgdj, s.zzrq4 }).
           Select((s, sz) => new
           {
               sz.spdm,
               s.zzrq4,
               htsl = SqlFunc.AggregateSum(sz.sl),
               DJ = sz.jgdj,
               htje = SqlFunc.AggregateSum(sz.jgje)

           }).ToListAsync();
            var sdxdsl = await db.SqlQueryable<VIEWMODEL_SDXDSL>(sql3).Where(s => s.SPDM == spdm).Select(s => new
            {
                s.SPDM,
                s.Sl
            }).ToListAsync();
            var listdata = from l1 in list
                           join l2 in sdxdsl on l1.spdm equals l2.SPDM into a
                           from r in a.DefaultIfEmpty()
                           select new
                           {
                               l1.spdm,
                               l1.zzrq4,
                               l1.htsl,
                               l1.DJ,
                               l1.htje,
                               XDSL = r != null ? r.Sl : 0,
                           };

            return Json(new { code = 0, msg = "", count = listdata.Count(), data = listdata.ToList() }, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> WFIndex()
        {
            var Name = Request["Name"];
            var SPdm = Request["SPdm"];
            var spgc = Request["spgc"];
            var spyear = Request["spyear"];
            var spjijie = Request["spjijie"];
            var selectzt = Request["selectzt"];
            var page = int.Parse(Request["page"] ?? "1");
            var limit = int.Parse(Request["limit"] ?? "10");
            var list = await db.Queryable<Wfzzd, WFzzdmx, SHANGPIN, JIJIE, GONGCHANG, _view_WFzzd>((s, sz, sp, jj, gc, wf) => new object[] {
                JoinType.Left,s.djbh==sz.djbh,
                JoinType.Left,sz.spdm==sp.SPDM,
                JoinType.Left,sp.BYZD5==jj.JJDM,
                JoinType.Left,s.gcdm==gc.GCDM,
                JoinType.Left,s.hth == wf.HTH
            }).With(SqlWith.NoLock).Where((s, sz, sp, jj, gc, wf) => s.hth.Contains("LX-W") || s.hth.Contains("Dg-W") || s.hth.Contains("LX-D"))
            .WhereIF(!string.IsNullOrEmpty(Name), s => s.hth.Contains(Name))
             .WhereIF(!string.IsNullOrEmpty(SPdm), (s, sz, sp, jj, gc, wf) => sz.spdm.Contains(SPdm))
             .WhereIF(!string.IsNullOrEmpty(spgc), (s, sz, sp, jj, gc, wf) => gc.GCMC.Contains(spgc))
             .WhereIF(!string.IsNullOrEmpty(spyear), (s, sz, sp, jj, gc, wf) => sp.BYZD8.ToString().Contains(spyear))
             .WhereIF(!string.IsNullOrEmpty(spjijie), (s, sz, sp, jj, gc, wf) => jj.JJMC.Contains(spjijie))
            //.WhereIF(!string.IsNullOrEmpty(selectzt), (s, sz, sp, jj, gc) => fk.SHzt == selectzt)
           .GroupBy((s, sz, sp, jj, gc, wf) => new
           {
               sz.spdm,
               jj.JJMC,
               sp.BYZD8,
               s.hth,
               gc.GCMC,
               wf.FKzt,
               wf.ISfk,
               wf.TJzt,
               wf.Money_1,
               wf.Money_2,
               wf.Money_3,
               wf.KHH,
               wf.ZH,
               wf.DZ,
               wf.Phone,
               wf.Remark,
               wf.JSrq,
               wf.SHzt2

           })
           .Select((s, sz, sp, jj, gc, wf) => new
           {
               sz.spdm,
               jj.JJMC,
               sp.BYZD8,
               s.hth,
               gc.GCMC,
               ZZRQ3 = SqlFunc.AggregateMin(s.zzrq3),
               JHRQ = SqlFunc.AggregateMin(s.zzrq4),
               SL = SqlFunc.AggregateSum(sz.sl),
               JE = SqlFunc.AggregateSum(sz.jgje),
               wf.FKzt,
               wf.ISfk,
               wf.TJzt,
               wf.Money_1,
               wf.Money_2,
               wf.Money_3,
               wf.KHH,
               wf.ZH,
               wf.DZ,
               wf.Phone,
               wf.Remark,
               wf.SHzt2
           }).OrderBy("wf.JSrq desc").ToListAsync();
            var list2 = await db.Queryable<SPJHD, SPJHDMX>((jh, jhmx) => new object[] {
                JoinType.Left,jh.DJBH==jhmx.DJBH
            }).With(SqlWith.NoLock).GroupBy((jh, jhmx) => new { jhmx.SPDM }).Select((jh, jhmx) => new
            {
                jhmx.SPDM,
                rq = SqlFunc.AggregateMin(jh.RQ),
                sl = SqlFunc.AggregateSum(jhmx.SL),
                hsje = SqlFunc.AggregateSum(jhmx.JE)
            }).ToListAsync();
            var sdxdsl = await db.SqlQueryable<VIEWMODEL_SDXDSL>(sql3).Select(s => new
            {
                s.SPDM,
                s.Sl
            }).ToListAsync();
            var listdata = from l1 in list
                           join l2 in list2 on l1.spdm equals l2.SPDM into a
                           from r in a.DefaultIfEmpty()
                           join l3 in sdxdsl on l1.spdm equals l3.SPDM into b
                           from r1 in b.DefaultIfEmpty()
                               //orderby l1.FKzt descending
                           select new
                           {
                               l1.spdm,
                               l1.JJMC,
                               l1.BYZD8,
                               l1.hth,
                               l1.GCMC,
                               l1.ZZRQ3,
                               l1.JHRQ,
                               l1.SL,
                               l1.JE,
                               l1.FKzt,
                               l1.ISfk,
                               l1.TJzt,
                               l1.Money_1,
                               l1.Money_2,
                               l1.Money_3,
                               l1.KHH,
                               l1.ZH,
                               l1.DZ,
                               l1.Phone,
                               l1.Remark,
                               l1.SHzt2,
                               //l1.Money_1,
                               //l1.Money_2,
                               //l1.Money_3,
                               //l1.SHzt,
                               //l1.ZT,
                               rkrq = r != null ? r.rq : null,
                               rksl = r != null ? r.sl : null,
                               hsje = r != null ? r.hsje : null,
                               sdxdsl = r1 != null ? r1.Sl : 0,
                           };
            return Json(new { code = 0, msg = "", count = listdata.Count(), data = listdata.Skip((page - 1) * limit).Take(limit).ToList() }, JsonRequestBehavior.AllowGet);
        }

    }
}