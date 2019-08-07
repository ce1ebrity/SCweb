
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SCWeb.Helper;
using SCWeb.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SCWeb.Controllers
{
    public class MlghsMessageController : BaseController
    {
        int[] zs = { 1, 2, 3 };

        // GET: MlghsMessage
        public ActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> IndexList()
        {
            //var b1111 = Regex.Replace(a111, a111.Substring(3, 4), "****");
            var page = int.Parse(Request["page"] ?? "1");
            var limit = int.Parse(Request["limit"] ?? "10");
            var name = Request["Name"];
            var namemldm = Request["namemldm"];
            var selectzt = Request["selectzt"];
            var selecttj = Request["selecttj"];
            var nameghs = Request["nameghs"];
            var year = Request["year"];
            var jj = Request["jijie"];
            var list = await db.Queryable<MLJRD, MLJRDMX, MIANLIAO, SHANGPIN, JIJIE, FJSX2, GONGHUOSHANG, MLJS>((m, mmx, ml, sp, jijie, f2, go, mjs) => new object[]
            {
                JoinType.Left,m.DJBH == mmx.DJBH,
                JoinType.Left,mmx.MLDM == ml.MLDM,
                JoinType.Left,mmx.SPDM ==sp.SPDM,
                JoinType.Left,sp.BYZD5 ==jijie.JJDM,
                JoinType.Left,sp.FJSX2==f2.SXDM,
                JoinType.Left,m.DM1==go.GHSDM,
                JoinType.Left,m.YDJH==mjs.YDJH
            }).With(SqlWith.NoLock).Where(m => m.YDJH.Contains("LX-M")).
            WhereIF(!string.IsNullOrEmpty(name), (m, mmx, ml, sp, jijie, f2, go, mjs) => m.YDJH.Contains(name)).
            WhereIF(!string.IsNullOrEmpty(namemldm), (m, mmx, ml, sp, jijie, f2, go, mjs) => ml.MLDM.Contains(namemldm))
            .WhereIF(!string.IsNullOrEmpty(selectzt), (m, mmx, ml, sp, jijie, f2, go, mjs) => mjs.SHzt == selectzt)
            .WhereIF(!string.IsNullOrEmpty(selecttj), (m, mmx, ml, sp, jijie, f2, go, mjs) => mjs.TJzt == selecttj)
            .WhereIF(!string.IsNullOrEmpty(nameghs), (m, mmx, ml, sp, jijie, f2, go, mjs) => go.GHSMC.Contains(nameghs))
            .WhereIF(!string.IsNullOrEmpty(year), (m, mmx, ml, sp, jijie, f2, go, mjs) => sp.BYZD8==SqlFunc.ToInt32(year))
            .WhereIF(!string.IsNullOrEmpty(jj), (m, mmx, ml, sp, jijie, f2, go, mjs) => SqlFunc.ToString(sp.BYZD5).Contains(jj))
            .GroupBy((m, mmx, ml, sp, jijie, f2, go, mjs) => new { sp.BYZD8, jijie.JJMC, m.YDJH, ml.MLMC, ml.MLDM, m.RQ, m.YXRQ, go.GHSMC, go.GHSDM, mjs.Money_1, mjs.Money_2, mjs.Money_3, mjs.Money_80, mjs.SHzt, mjs.ZT, mjs.TJzt, mjs.jsRQ, mjs.SHzt2,mjs.remark }).
            Select((m, mmx, ml, sp, jijie, f2, go, mjs) => new
            {
                sp.BYZD8,
                jijie.JJMC,
                m.YDJH,
                ml.MLMC,
                ml.MLDM,
                m.RQ,
                m.YXRQ,
                SL = SqlFunc.AggregateSum(mmx.SL),
                JE = SqlFunc.AggregateSum(mmx.JE),
                go.GHSMC,
                go.GHSDM,
                mjs.Money_1,
                mjs.Money_2,
                mjs.Money_3,
                mjs.Money_80,
                mjs.SHzt,
                mjs.ZT,
                mjs.TJzt,
                mjs.SHzt2,
                mjs.remark
            }).OrderBy("mjs.jsRQ desc").ToListAsync();
            var list2 = await db.Queryable<MLJHD, MLJHDMX>((jh, jhmx) => new object[] {
                JoinType.Left,jh.DJBH==jhmx.DJBH
            }).With(SqlWith.NoLock).Where(jh => jh.YDJH.Contains("LX-M")).GroupBy((jh, jhmx) => new { jh.YDJH }).Select((jh, jhmx) => new
            {
                jh.YDJH,
                rq = SqlFunc.AggregateMax(jh.RQ),
                sl = SqlFunc.AggregateSum(jhmx.SL)
            }).ToListAsync();
            var list3 = from a in list
                        join b in list2 on a.YDJH equals (b.YDJH) into AB
                        from r in AB.DefaultIfEmpty()
                            // orderby a.SHzt descending
                        select
                        new
                        {
                            a.BYZD8,
                            a.JJMC,
                            a.YDJH,
                            a.MLMC,
                            a.MLDM,
                            a.RQ,
                            a.YXRQ,
                            a.SL,
                            a.JE,
                            sl1 = r != null ? r.sl : null,
                            rq1 = r != null ? r.rq : null,
                            a.GHSMC,
                            a.GHSDM,
                            a.Money_1,
                            a.Money_2,
                            a.Money_3,
                            a.Money_80,
                            a.SHzt,
                            a.ZT,
                            a.TJzt,
                            a.SHzt2,
                            a.remark

                        };
            return Json(new { code = 0, msg = "", count = list3.Count(), data = list3.Skip((page - 1) * limit).Take(limit).ToList() }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Edit()
        {
            return View();
        }
        public ActionResult Edit2()
        {
            return View();
        }
        public ActionResult Edit3(string YDJH)
        {
            ML(YDJH);
            return View();
        }

        public JsonResult IndexInfoML(string YDJH)
        {

            var page = int.Parse(Request["page"] ?? "1");
            var limit = int.Parse(Request["limit"] ?? "10");
            var list = db.Queryable<MLJRD, MLJRDMX, HGUIGE1, DANWEI, GONGHUOSHANG, MIANLIAO, SHANGPIN, FJSX2>((m, mx, hg, da, go, ml, sp, f2) => new object[] {
                        JoinType.Left,m.DJBH == mx.DJBH,
                        JoinType.Left,mx.GGDM == hg.GGDM,
                        JoinType.Left,mx.BZDW==da.DWDM,
                        JoinType.Left,m.DM1==go.GHSDM,
                        JoinType.Left,mx.MLDM ==ml.MLDM,
                        JoinType.Left,mx.SPDM==sp.SPDM,
                        JoinType.Left,sp.FJSX2 == f2.SXDM }).With(SqlWith.NoLock)
                        .WhereIF(!string.IsNullOrEmpty(YDJH), (m, mx, hg, da, go, ml, sp, f2) => m.YDJH == YDJH)
                        .GroupBy((m, mx, hg, da, go, ml, sp, f2) => new { go.GHSMC, m.YDJH, mx.MLDM, ml.MLMC, f2.SXMC, mx.FK, hg.GGMC, da.DWMC, mx.DJ })
                        .Select((m, mx, hg, da, go, ml, sp, f2) => new
                        {
                            go.GHSMC,
                            m.YDJH,
                            mx.MLDM,
                            ml.MLMC,
                            f2.SXMC,
                            mx.FK,
                            hg.GGMC,
                            da.DWMC,
                            SL = SqlFunc.AggregateSum(mx.SL),
                            DJ = SqlFunc.ToDecimal(mx.DJ),
                            JE = SqlFunc.AggregateSum(mx.JE)
                        }).ToPageList(page, limit);
            return Json(new { code = 0, msg = "", count = list.Count(), data = list }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult IndexInfoGhs(string GHSDM, string GCMC)
        {
            var list = db.Queryable<GONGHUOSHANG>().With(SqlWith.NoLock).Where(g => g.GHSDM == GHSDM || g.GHSMC == GCMC).Select((g) => new
            {
                g.GHSMC,
                g.DZ,
                g.DH1,
                g.SH,
                g.KHH,
                g.ZH
            }).ToList();
            return Json(new { code = 0, msg = "", count = list.Count, data = list }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 面料20%定金
        /// </summary>
        /// <param name="mljs"></param>
        /// <returns></returns>
        public ActionResult InsertMLFK(MLJS mljs)
        {
            int[] zs = { 1, 2, 3 };
            var GHSDM = Request["GHSDM"];
            var GHSMC = Request["GHSMC"];
            var YDJH = Request["YDJH"];
            var je = Request["je"];
            var kp = Request["kp"];
            var remark = Request["remark"];
            if (db.Queryable<MLJS>().With(SqlWith.NoLock).Where(m => m.YDJH == YDJH && m.TJzt == "1").Count() > 0)
            {
                return Content("D");
            }
            else
            {
                if (db.Insertable<MLJS>(new
                {
                    GHSDM,
                    GHSMC,
                    YDJH,
                    Money_1 = je,
                    ZT = kp,
                    TJzt = 1,
                    jsRQ = DateTime.Now,
                    remark
                }).ExecuteCommand() > 0)
                {
                    return Content("y");
                }
                else
                {
                    return Content("n");
                }
            }

        }
        /// <summary>
        /// 审核1
        /// </summary>
        /// <param name="mljs"></param>
        /// <returns></returns>
        public ActionResult InsertMLFKTJ(MLJS mljs)
        {
            string userId = Common.GetCookie("userLogin");
            var GHSDM = Request["GHSDM"];
            var GHSMC = Request["GHSMC"];
            var YDJH = Request["YDJH"];
            var je = Request["je"];
            var kp = Request["kp"];
            var remark = Request["remark"];
            if (db.Ado.SqlQuery<BPM_UserBase>(Usersql, new SugarParameter("@userId", userId)).Count() > 0) //&& m.TJzt == "1"
            {
                var c = db.Queryable<MLJS>().With(SqlWith.NoLock).Where(m => m.YDJH == YDJH && SqlFunc.ContainsArray(zs,m.TJzt)).Count();
                if (c > 0)
                {
                    if (db.Updateable<MLJS>(new
                    {
                        GHSDM,
                        GHSMC,
                        YDJH,
                        Money_1 = je,
                        SHzt2 = 1,
                        SHzt=1,
                        ZT = kp,
                        jsRQ = DateTime.Now,
                        remark = remark
                    }).Where(u => u.YDJH == YDJH).ExecuteCommand() > 0)
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
                var c = db.Queryable<MLJS>().With(SqlWith.NoLock).Where(m => m.YDJH == YDJH && m.TJzt == "1").Count();
                if (c > 0)
                {
                    if (db.Updateable<MLJS>(new
                    {
                        GHSDM,
                        GHSMC,
                        YDJH,
                        Money_1 = je,
                        SHzt = 1,
                        ZT = kp,
                        jsRQ = DateTime.Now,
                        remark = remark
                    }).Where(u => u.YDJH == YDJH).ExecuteCommand() > 0)
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
        /// <summary>
        /// 审核2
        /// </summary>
        /// <param name="mljs"></param>
        /// <returns></returns>
        public ActionResult InsertMLFK2SH(MLJS mljs)
        {
            string userId = Common.GetCookie("userLogin");
            var GHSDM = Request["GHSDM"];
            var GHSMC = Request["GHSMC"];
            var YDJH = Request["YDJH"];
            var je = Request["je"];
            var money_80 = Request["money_80"];
            var kp = Request["kp"];
            var remark = Request["remark"];
            if (db.Ado.SqlQuery<BPM_UserBase>(Usersql, new SugarParameter("@userId", userId)).Count() > 0)
            {
                var c = db.Queryable<MLJS>().With(SqlWith.NoLock).Where(m => m.YDJH == YDJH && m.TJzt=="2").Count();
                if (c > 0)
                {
                    if (db.Updateable<MLJS>(new
                    {
                        Money_2 = je,
                        money_80 = money_80,
                        SHzt2 = 2,
                        SHzt = 2,
                        jsRQ = DateTime.Now,
                        ZT = kp,
                        remark = remark
                    }).Where(u => u.YDJH == YDJH).ExecuteCommand() > 0)
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

                if (db.Queryable<MLJS>().With(SqlWith.NoLock).Where(m => m.YDJH == YDJH && m.TJzt == "2").Count() > 0)
                {
                    if (db.Updateable<MLJS>(new
                    {
                        Money_2 = je,
                        money_80 = money_80,
                        SHzt = 2,
                        jsRQ = DateTime.Now,
                        ZT = kp,
                        remark = remark
                    }).Where(u => u.YDJH == YDJH).ExecuteCommand() > 0)
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
        /// 面料60%定金
        /// </summary>
        /// <param name="mljs"></param>
        /// <returns></returns>
        public ActionResult InsertMLFK2(MLJS mljs)
        {
            var GHSDM = Request["GHSDM"];
            var GHSMC = Request["GHSMC"];
            var YDJH = Request["YDJH"];
            var je = Request["je"];
            var kp = Request["kp"];
            var money_80 = Request["money_80"];
            var remark = Request["remark"];
            if (db.Queryable<MLJS>().With(SqlWith.NoLock).Where(m => m.YDJH == YDJH && m.TJzt == "1").Count() <= 0)
            {
                return Content("1");
            }
            else if (db.Updateable<MLJS>(new
            {
                Money_2 = je,
                money_80 = money_80,
                TJzt = 2,
                jsRQ = DateTime.Now,
                ZT = kp,
                remark = remark
            }).Where(u => u.YDJH == YDJH).ExecuteCommand() > 0)
            {
                return Content("y");
            }
            else
            {
                return Content("N");
            }


        }
        /// <summary>
        /// 面料尾款提交
        /// </summary>
        /// <param name="mljs"></param>
        /// <returns></returns>
        public ActionResult InsertMLFK3(MLJS mljs)
        {
            var GHSDM = Request["GHSDM"];
            var GHSMC = Request["GHSMC"];
            var YDJH = Request["YDJH"];
            var je = Request["je"];
            var kp = Request["kp"];
            var remark = Request["remark"];
            if (db.Queryable<MLJS>().With(SqlWith.NoLock).Where(m => m.YDJH == YDJH && m.TJzt == "2").Count() <= 0)
            {
                return Content("1");
            }
            else if (db.Updateable<MLJS>(new
            {
                Money_3 = je,
                TJzt = 3,
                jsRQ = DateTime.Now,
                ZT = kp,
                mljs.je1,
                mljs.je2,
                mljs.je3,
                mljs.je4,
                mljs.je5,
                mljs.je6,
                mljs.je7,
                remark= remark
            }).Where(u => u.YDJH == YDJH).ExecuteCommand() > 0)
            {
                return Content("y");
            }
            else
            {
                return Content("N");
            }


        }
        /// <summary>
        /// 审核3
        /// </summary>
        /// <param name="mljs"></param>
        /// <returns></returns>
        public ActionResult InsertMLFK3SH(MLJS mljs)
        {
            var GHSDM = Request["GHSDM"];
            var GHSMC = Request["GHSMC"];
            var YDJH = Request["YDJH"];
            var je = Request["je"];
            var kp = Request["kp"];
            var remark = Request["remark"];
            string userId = Common.GetCookie("userLogin");
            if (db.Ado.SqlQuery<BPM_UserBase>(Usersql, new SugarParameter("@userId", userId)).Count() > 0)
            {
                var c = db.Queryable<MLJS>().With(SqlWith.NoLock).Where(m => m.YDJH == YDJH && m.TJzt == "3").Count();
                if (c > 0)
                {
                    if (db.Updateable<MLJS>(new
                    {
                        Money_3 = je,
                        SHzt2 = 3,
                        SHzt = 3,
                        jsRQ = DateTime.Now,
                        ZT = kp,
                        mljs.je1,
                        mljs.je2,
                        mljs.je3,
                        mljs.je4,
                        mljs.je5,
                        mljs.je6,
                        mljs.je7,
                        remark= remark
                    }).Where(u => u.YDJH == YDJH).ExecuteCommand() > 0)
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
                
               if (db.Queryable<MLJS>().With(SqlWith.NoLock).Where(m => m.YDJH == YDJH && m.TJzt == "3").Count() > 0)
                {
                    if (db.Updateable<MLJS>(new
                    {
                        Money_3 = je,
                        SHzt = 3,
                        jsRQ = DateTime.Now,
                        ZT = kp,
                        mljs.je1,
                        mljs.je2,
                        mljs.je3,
                        mljs.je4,
                        mljs.je5,
                        mljs.je6,
                        mljs.je7,
                        remark = remark
                    }).Where(u => u.YDJH == YDJH).ExecuteCommand() > 0)
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
        /// /裁后扣损面料结算信息
        /// </summary>
        /// <param name="YDJH"></param>
        /// <returns></returns>
        public async Task<JsonResult> IndexMLCHKSInfo(string YDJH)
        {
            var page = int.Parse(Request["page"] ?? "1");
            var limit = int.Parse(Request["limit"] ?? "10");
            var list = await db.Queryable<MLJHD, MLJHDMX>((m, mx) => new object[] {
                JoinType.Left,m.DJBH==mx.DJBH
            }).With(SqlWith.NoLock).Where(m => m.YDJH == YDJH).GroupBy((m, mx) => new { m.YDJH, mx.SPDM })
            .Select((m, mx) => new
            {
                sl1 = SqlFunc.AggregateSum(mx.SL),
                mx.SPDM
            }).ToListAsync();

            var listsc = await db.Queryable<SCZZD, SCZZDMX, GONGCHANG>((m, mx,gc) => new object[] {
                JoinType.Left,m.DJBH==mx.DJBH,
                JoinType.Left,m.GCDM==gc.GCDM
            }).With(SqlWith.NoLock).GroupBy((m, mx,gc) => new {mx.SPDM,gc.GCMC })
           .Select((m, mx,gc) => new
           {
               sl1 = SqlFunc.AggregateSum(mx.SL),
               mx.SPDM,
               gc.GCMC
           }).ToListAsync();
            //System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            //watch.Start();  //开始监视代码运行时间 mltld
            var list1 = await db.Queryable<MLTLD, MLTLDMX>((m, mx) => new object[] {
                JoinType.Left,m.DJBH==mx.DJBH,
                //JoinType.Left,m.DM1 ==gc.GCDM
            }).With(SqlWith.NoLock).GroupBy((m, mx) => new { m.SPDM})
            .Select((m, mx) => new
            {
                RQ = SqlFunc.AggregateMax(m.RQ),
                CPSL = SqlFunc.AggregateSum(mx.SL),
                m.SPDM,
                //gc.GCMC,
            }).ToListAsync();
            //需要测试的代码
            /* watch.Stop();*/ //停止监视
                               //TimeSpan timespan = watch.Elapsed;  //获取当前实例测量得出的总时间
                               //System.Diagnostics.Debug.WriteLine("执行时间：{0}(毫秒)", timespan.TotalMilliseconds);  //总毫秒数
            var list3 = await db.Queryable<GYPLD, GYPLDML1>((g, gml) => new object[] {
                JoinType.Left,g.DJBH==gml.DJBH
            }).With(SqlWith.NoLock).GroupBy((g, gml) => g.SPDM).Select((g, gml) => new
            {
                g.SPDM,
                SL3 = SqlFunc.AggregateSum(gml.SL)
            }).ToListAsync();
            var list4ZP = await db.Queryable<SPJHD, SPJHDMX>((s, sp) => new object[] {
                JoinType.Left,s.DJBH==sp.DJBH
            }).With(SqlWith.NoLock).Where(s => s.DM2 == "0000")
            .GroupBy((s, sp) => sp.SPDM).Select((s, sp) => new { sp.SPDM, zpSL = SqlFunc.AggregateSum(sp.SL) }).ToListAsync();
            var list5CP = await db.Queryable<SPJHD, SPJHDMX>((s, sp) => new object[] {
                JoinType.Left,s.DJBH==sp.DJBH
            }).With(SqlWith.NoLock).Where(s => s.DM2 == "0300").GroupBy((s, sp) => sp.SPDM).Select((s, sp) => new
            {
                sp.SPDM,
                chipsl = SqlFunc.AggregateSum(sp.SL)
            }).ToListAsync();
            var result = from l1 in list
                         join l2 in list1 on new { l1.SPDM } equals new { l2.SPDM } into ac
                         from r in ac.DefaultIfEmpty()
                         join l3 in list3 on l1.SPDM equals l3.SPDM into ac1
                         from r1 in ac1.DefaultIfEmpty()
                         join l4 in list4ZP on l1.SPDM equals l4.SPDM into ac2
                         from r2 in ac2.DefaultIfEmpty()
                         join l5 in list5CP on l1.SPDM equals l5.SPDM into ac3
                         from r3 in ac3.DefaultIfEmpty()
                         join l6 in listsc on l1.SPDM equals l6.SPDM into ac4
                         from r4 in ac4.DefaultIfEmpty()
                         select new
                         {
                             l1.SPDM,
                             GCMC = r4 != null ? r4.GCMC : null,
                             zpsl = r2 != null ? r2.zpSL : null,
                             chipsl = r3 != null ? r3.chipsl : null,
                             cpsl = r != null ? r.CPSL : null,
                             sl3 = r1 != null ? r1.SL3 : null
                         };
            return Json(new { code = 0, msg = "", count = result.Count(), data = result.Skip((page - 1) * limit).Take(limit).ToList() }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult IndexInfoMljhd(string YDJH)
        {
            if (string.IsNullOrEmpty(YDJH))
            {
                return Json(new { code = 0, msg = "", count = "", data = "" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var list = db.Queryable<MLJHD, MLJHDMX, MIANLIAO, HGUIGE1>((m, mmx, ml, h) => new object[] {
                JoinType.Left,m.DJBH==mmx.DJBH,
                JoinType.Left,mmx.MLDM==ml.MLDM,
                JoinType.Left,mmx.GGDM==h.GGDM,
            }).With(SqlWith.NoLock).Where(m => m.YDJH == YDJH).GroupBy((m, mmx, ml, h) => new { ml.MLMC, mmx.MLDM, mmx.FK, h.GGMC, mmx.BZ, m.RQ })
                .Select((m, mmx, ml, h) => new
                {
                    ml.MLMC,
                    mmx.MLDM,
                    mmx.FK,
                    h.GGMC,
                    SL = SqlFunc.AggregateSum(mmx.SL),
                    BZ = mmx.BZ,
                    RQ = m.RQ,
                }).ToList();
                return Json(new { code = 0, msg = "", count = list.Count, data = list }, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<ActionResult> IndexInfoJGCRKD(string YDJH)
        {
            var page = int.Parse(Request["page"] ?? "1");
            var limit = int.Parse(Request["limit"] ?? "10");
            var list = await db.Queryable<JGCKD, JGCKDML, MIANLIAO, HGUIGE1>((j, jml, ml, hg) => new object[] {
                JoinType.Left,j.DJBH==jml.DJBH,
                JoinType.Left,jml.MLDM==ml.MLDM,
                JoinType.Left,jml.GGDM==hg.GGDM,
            }).With(SqlWith.NoLock).Where(j => j.BZ == YDJH)
            .GroupBy((j, jml, ml, hg) => new { j.BZ, jml.MLDM, ml.MLMC, hg.GGMC }).Select((j, jml, ml, hg) => new
            {
                j.BZ,
                jml.MLDM,
                ml.MLMC,
                hg.GGMC,
                CKSL = SqlFunc.AggregateSum(jml.SL)
            }).ToListAsync();

            var list2 = await db.Queryable<JGRKD, JGRKDML, MIANLIAO, HGUIGE1>((j1, jml1, ml1, hg1) => new object[] {
                JoinType.Left,j1.DJBH==jml1.DJBH,
                JoinType.Left,jml1.MLDM==ml1.MLDM,
                JoinType.Left,jml1.GGDM==hg1.GGDM
            }).With(SqlWith.NoLock).Where(j1 => j1.BZ.Contains("LX-M"))
            .GroupBy((j1, jml1, ml1, hg1) => new { j1.BZ, jml1.MLDM, ml1.MLMC, hg1.GGMC }).Select((j1, jml1, ml1, hg1) => new viewModel2
            {

                BZ = j1.BZ,
                MLDM = jml1.MLDM,
                MLMC = ml1.MLMC,
                GGMC = hg1.GGMC,
                RKSL = SqlFunc.AggregateSum(jml1.SL)

            }).ToListAsync();
            var listdata = from a in list
                           join b in list2
                           on new { a.BZ, a.GGMC } equals new { b.BZ, b.GGMC } into Ab
                           from r in Ab.DefaultIfEmpty()
                           select new
                           {
                               a.BZ,
                               a.MLDM,
                               a.MLMC,
                               a.GGMC,
                               a.CKSL,
                               RKSL = r != null ? r.RKSL : null
                           };
            return Json(new { code = 0, msg = "", count = listdata.Count(), data = listdata.Skip((page - 1) * limit).Take(limit).ToList() }, JsonRequestBehavior.AllowGet);
        }
    }
}