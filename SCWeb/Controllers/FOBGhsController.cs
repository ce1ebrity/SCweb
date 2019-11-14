using SCWeb.Helper;
using SCWeb.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SCWeb.Controllers
{
    public class FOBGhsController : BaseController
    {

        // GET: FOBGhs
        private int[] zs = { 1, 2, 3 };
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Edit()
        {
            return View();
        }
        public ActionResult EditFOBlast(string spdm, string HTH)
        {
            FOB(spdm, HTH);
            return View();
        }
        public ActionResult Edit2(string spdm, string HTH)
        {
            FOB(spdm, HTH);
            return View();
        }
        /// <summary>
        /// 面料出库
        /// </summary>
        /// <returns></returns>
        public async Task<JsonResult> MLCK(string SPDM)
        {
            var page = int.Parse(Request["page"] ?? "1");
            var limit = int.Parse(Request["limit"] ?? "10");
            var list1 = await db.Queryable<MLLLD, MLLLDMX>((m, mmx) => new object[] {
                JoinType.Left,m.DJBH==mmx.DJBH //mmx => mmx.SPDM == SPDM
            }).With(SqlWith.NoLock).Where(m => m.SPDM == SPDM).GroupBy((m, mmx) => new { m.SPDM, mmx.MLDM, mmx.DJ, m.RQ })
           .Select((m, mmx) => new
           {
               m.SPDM,
               mmx.MLDM,
               SL = SqlFunc.AggregateSum(mmx.SL),
               mmx.DJ,
               m.RQ

           }).ToListAsync();
            var list2 = await db.Queryable<MLJHD, MLJHDMX>((m, mmx) => new object[] {
                JoinType.Left,m.DJBH==mmx.DJBH //mmx => mmx.SPDM == SPDM
            }).With(SqlWith.NoLock).Where((m, mmx) => mmx.SPDM == SPDM).GroupBy((m, mmx) => new { mmx.SPDM, mmx.MLDM, mmx.DJ, m.RQ })
            .Select((m, mmx) => new
            {
                mmx.SPDM,
                mmx.MLDM,
                SL = SqlFunc.AggregateSum(mmx.SL),
                mmx.DJ,
                m.RQ

            }).ToListAsync();//.ToPageListAsync(page, limit);
            var list = from l1 in list1
                       join l2 in list2
                       on new { l1.SPDM, l1.MLDM } equals new { l2.SPDM, l2.MLDM } into a
                       from r in a.DefaultIfEmpty()
                       select new
                       {
                           l1.SPDM,
                           l1.MLDM,
                           l1.SL,
                           l1.RQ,
                           DJ = r != null ? r.DJ : null,
                       };
            return Json(new { code = 0, msg = "", count = list.Count(), data = list.Skip((page - 1) * limit).Take(limit).ToList() }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 辅料出库
        /// </summary>
        /// <param name="SPDM"></param>
        /// <returns></returns>
        public async Task<JsonResult> FLCK(string SPDM)
        {
            var page = int.Parse(Request["page"] ?? "1");
            var limit = int.Parse(Request["limit"] ?? "10");
            var list = await db.Queryable<FLLLD, FLLLDMX, FULIAO>((m, mmx, f) => new object[] {
                JoinType.Left,m.DJBH==mmx.DJBH,
                JoinType.Left,mmx.FLDM==f.FLDM
            }).With(SqlWith.NoLock).Where(m => m.SPDM == SPDM).GroupBy((m, mmx, f) => new { m.SPDM, mmx.FLDM, f.FLMC, mmx.DJ, m.RQ })
            .Select((m, mmx, f) => new
            {
                m.SPDM,
                mmx.FLDM,
                f.FLMC,
                SL = SqlFunc.AggregateSum(mmx.SL),
                mmx.DJ,
                m.RQ

            }).ToPageListAsync(page, limit);
            return Json(new { code = 0, msg = "", count = list.Count(), data = list }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// fob20定金
        /// </summary>
        /// <returns></returns>
        public async Task<JsonResult> IndexFOB20(string spdm, string hth)
        {

            var list = await db.Queryable<SCZZD, SCZZDMX, GUIGE1, GUIGE2>((s, sz, g1, g2) => new object[] {
                JoinType.Left,s.DJBH==sz.DJBH,
                JoinType.Left,sz.GG1DM==g1.GGDM,
                JoinType.Left,sz.GG2DM==g2.GGDM
            }).Where(s => s.SP != "1")//终止
            .With(SqlWith.NoLock).Where(s => s.SPDM == spdm && s.HTH == hth).GroupBy((s, sz, g1, g2) => new { s.SPDM, s.JGDJ, s.JHRQ, col = g1.GGMC, cm = g2.GGMC }).
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
        /// FOB货期信息
        /// </summary>
        /// <param name="spdm"></param>
        /// <returns></returns>
        public async Task<JsonResult> FOBhqMessage(string spdm)
        {
            var list = await db.Queryable<SCZZD, SCZZDMX, GUIGE1, View_model_fobdaixiao>((s, sc, g, v) => new object[] {
               JoinType.Left,s.DJBH==sc.DJBH,
               JoinType.Left,sc.GG1DM==g.GGDM,
               JoinType.Left,sc.SPDM==v.SPDM && v.GGMC==g.GGMC
            }).With(SqlWith.NoLock).Where((s, sc, g, v) => sc.SPDM == spdm).GroupBy((s, sc, g, v) => new { sc.SPDM, s.JGDJ, s.JHRQ, g.GGMC, v.isDX, v.SJjhrq, v.TYyqrq })
            .Select((s, sc, g, v) => new
            {
                sc.SPDM,
                htsl = SqlFunc.AggregateSum(sc.SL),
                htje = SqlFunc.AggregateSum(sc.BYZD6),
                s.JGDJ,
                g.GGMC,
                s.JHRQ,
                v.isDX,
                v.SJjhrq,
                v.TYyqrq
            }).ToListAsync();
            return Json(new { code = 0, msg = "", count = list.Count(), data = list }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// fob 转代销
        /// </summary>
        /// <returns></returns>
        public ActionResult FOBHQ_daixiao()
        {

            var SPDM = Request["SPDM"];
            var GGMC = Request["GGMC"];
            var JHRQ = Request["JHRQ"];
            var htsl = Request["htsl"];
            var htje = Request["htje"];
            var JGDJ = Request["JGDJ"];
            var HTH = Request["HTH"];
            var GCMC = Request["GCMC"];
            if (db.Queryable<View_model_fobdaixiao>().With(SqlWith.NoLock).Where(u => u.SPDM == SPDM && u.GGMC == GGMC && u.isDX == 1).Count() > 0)
            {
                return Content("N");
            }
            else if (db.Queryable<View_model_fobdaixiao>().With(SqlWith.NoLock).Where(u => u.SPDM == SPDM && u.GGMC == GGMC).Count() <= 0)
            {
                var list = db.Insertable<View_model_fobdaixiao>(new
                {
                    SPDM = SPDM,
                    GGMC = GGMC,
                    JHRQ = JHRQ,
                    htsl = htsl,
                    htje = htje,
                    JGDJ = JGDJ,
                    isDX = 1,
                    //TYyqrq=DateTime.Now,
                    //SJjhrq= DateTime.Now,
                    HTH = HTH,
                    GCMC = GCMC

                }).ExecuteCommand();

                return Content("Y");
            }
            else
            {
                db.Updateable<View_model_fobdaixiao>(new
                {
                    isDX = 1,
                }).Where(u => u.HTH == HTH && u.SPDM == SPDM && u.GGMC == GGMC).ExecuteCommand();
            }
            return Content("Y");
        }
        public ActionResult ShowImg(string spdm)
        {
            string sql1 = @"select s1.Code,s2.SimpleImage,s1.MasterID from VW_ZF_SamplesInfo s1(nolock)
							 left join BS_BUS_SampleImage s2 (nolock)on s1.MasterID=s2.MasterID where s1.Code ='" + spdm + "'";
            DataTable newDataTable = SqlHelper.SelectTable(sql1);
            string imgurl = "";
            foreach (DataRow dr1 in newDataTable.Rows)
            {
                if (dr1["SimpleImage"].ToString().Length > 0)
                {
                    byte[] photo = new byte[0];
                    photo = (byte[])dr1["SimpleImage"];
                    string path = Server.MapPath("~/Upload").TrimEnd('\\') + @"\";
                    FileStream fs = new FileStream(path + dr1["MasterID"].ToString() + ".jpg", System.IO.FileMode.Create);
                    BinaryWriter bw = new BinaryWriter(fs);
                    bw.Write(photo, 0, photo.Length);
                    fs.Flush();
                    fs.Close();
                    imgurl = "/Upload/" + dr1["MasterID"].ToString() + ".jpg";

                }
            }
            return Content(imgurl);
        }
        [HttpPost]
        public ActionResult FOBHQ_daixiao_Update()
        {
            var SPDM = Request["SPDM"];
            var SJjhrq = Request["SJjhrq"];
            var TYyqrq = Request["TYyqrq"];
            var HTH = Request["HTH"];
            var GGMC = Request["GGMC"];
            var jhrq = Request["JHRQ"];
            var htsl = Request["htsl"];
            var htje = Request["htje"];
            var jgdj = Request["JGDJ"];
            var gcmc = Request["GCMC"];
            if (db.Queryable<View_model_fobdaixiao>().With(SqlWith.NoLock).Where(u => u.HTH == HTH && u.SPDM == SPDM && u.GGMC == GGMC).Count() > 0)
            {
                if (db.Updateable<View_model_fobdaixiao>(new
                {
                    SJjhrq = SJjhrq,
                    TYyqrq = TYyqrq
                }).Where(u => u.HTH == HTH && u.SPDM == SPDM && u.GGMC == GGMC).ExecuteCommand() > 0)
                {
                    return Content("Y");
                }
                else
                {
                    return Content("N");
                }
            }
            else
            {
                if (db.Insertable<View_model_fobdaixiao>(new
                {
                    GGMC = GGMC,
                    SPDM = SPDM,
                    GCMC = gcmc,
                    JHRQ = jhrq,
                    SJjhrq = SJjhrq,
                    TYyqrq = TYyqrq,
                    htsl = htsl,
                    htje = htje,
                    jgdj = jgdj,
                    HTH = HTH,
                    isDX = 0


                }).ExecuteCommand() > 0)
                {
                    return Content("Y");
                }
                else
                {
                    return Content("N");
                }

            }


        }
        /// <summary>
        /// fob 退料
        /// </summary>
        /// <param name="spdm"></param>
        /// <returns></returns>
        /// 
        public async Task<JsonResult> FObTl(string spdm)
        {
            var page = int.Parse(Request["page"] ?? "1");
            var limit = int.Parse(Request["limit"] ?? "10");
            var list = await db.Queryable<FLTLD, FLTLDMX, FULIAO>((f, fmx, ff) => new object[] {
                JoinType.Left,f.DJBH==fmx.DJBH,
                JoinType.Left,fmx.FLDM==ff.FLDM
            }).With(SqlWith.NoLock).Where((f, fmx, ff) => fmx.SPDM == spdm).GroupBy((f, fmx, ff) => new { fmx.SPDM, fmx.FLDM, ff.FLMC, fmx.DJ })
            .Select((f, fmx, ff) => new
            {
                fmx.SPDM,
                fmx.FLDM,
                ff.FLMC,
                flsl = SqlFunc.AggregateSum(fmx.SL),
                flje = SqlFunc.AggregateSum(fmx.JE),
                fmx.DJ
            }).ToPageListAsync(page, limit);
            return Json(new { code = 0, msg = "", coutn = list.Count(), data = list }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// FOB 70%
        /// </summary>
        /// <returns></returns>
        public async Task<JsonResult> FOB70(string spdm, string HTH, string GCMC,string GHSDM)
        {
            string gcmc = Server.UrlDecode(GCMC);
            var page = int.Parse(Request["page"] ?? "1");
            var limit = int.Parse(Request["limit"] ?? "10");
            var list = await db.Queryable<SPJHD, SPJHDMX, GUIGE1, GUIGE2, GONGHUOSHANG>((s, sp, g1, g2, ghs) => new object[] {
                JoinType.Left,s.DJBH==sp.DJBH,
                JoinType.Left,sp.GG1DM==g1.GGDM,
                JoinType.Left,sp.GG2DM==g2.GGDM,
                JoinType.Left,s.DM1==ghs.GHSDM
            }).With(SqlWith.NoLock).Where((s, sp, g1, g2, ghs) => sp.SPDM == spdm && ghs.GHSDM == GHSDM).GroupBy((s, sp, g1, g2, ghs) => new
            {
                sp.SPDM,
                ghs.GHSMC,
                col = g1.GGMC,
                cm = g2.GGMC,
                sp.DJ,
                s.RQ,
                s.DM2
            }).Select((s, sp, g1, g2, ghs) => new
            {
                sp.SPDM,
                ghs.GHSMC,
                col = g1.GGMC,
                cm = g2.GGMC,
                RKSL = SqlFunc.AggregateSum(sp.SL),
                s.RQ,
                //hsDJ = sp.DJ,
                //hsje = SqlFunc.AggregateSum(sp.JE),
                s.DM2
            }).ToListAsync();
            var list1 = await db.Queryable<SCZZD>().With(SqlWith.NoLock).Where(s => s.SPDM == spdm && s.HTH == HTH).Select((s) => new
            {
                s.SPDM,
                s.JGDJ,
                s.ZZRQ6
            }).Take(1).ToListAsync();
            var listdata = from l1 in list
                           join l2 in list1 on l1.SPDM equals l2.SPDM into a
                           from r in a.DefaultIfEmpty()
                               //where l1.RQ > r.ZZRQ6
                           select new
                           {
                               l1.SPDM,
                               l1.GHSMC,
                               l1.col,
                               l1.cm,
                               l1.RKSL,
                               l1.RQ,
                               l1.DM2,
                               hsDJ = r.JGDJ,
                               hsje = l1.RKSL * r.JGDJ
                           };

            return Json(new { code = 0, msg = "", count = listdata.Count(), data = listdata.Skip((page - 1) * limit).Take(limit).ToList() }, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> FOB70th(string spdm, string HTH, string GCMC, string GHSDM)
        {
            string gcmc = Server.UrlDecode(GCMC);
            var page = int.Parse(Request["page"] ?? "1");
            var limit = int.Parse(Request["limit"] ?? "10");
            var list = await db.Queryable<SPTHD, SPTHDMX, GUIGE1, GUIGE2, GONGHUOSHANG>((s, sp, g1, g2, ghs) => new object[] {
                JoinType.Left,s.DJBH==sp.DJBH,
                JoinType.Left,sp.GG1DM==g1.GGDM,
                JoinType.Left,sp.GG2DM==g2.GGDM,
                JoinType.Left,s.DM1==ghs.GHSDM
            }).With(SqlWith.NoLock).Where((s, sp, g1, g2, ghs) => sp.SPDM == spdm && ghs.GHSDM == GHSDM).GroupBy((s, sp, g1, g2, ghs) => new
            {
                sp.SPDM,
                ghs.GHSMC,
                col = g1.GGMC,
                cm = g2.GGMC,
                sp.DJ,
                s.RQ,
                s.DM2
            }).Select((s, sp, g1, g2, ghs) => new
            {
                sp.SPDM,
                ghs.GHSMC,
                col = g1.GGMC,
                cm = g2.GGMC,
                RKSL = SqlFunc.AggregateSum(sp.SL),
                s.RQ,
                //hsDJ = sp.DJ,
                //hsje = SqlFunc.AggregateSum(sp.JE),
                s.DM2
            }).ToListAsync();
            var list1 = await db.Queryable<SCZZD>().With(SqlWith.NoLock).Where(s => s.SPDM == spdm && s.HTH == HTH).Select((s) => new
            {
                s.SPDM,
                s.JGDJ,
                s.ZZRQ6
            }).Take(1).ToListAsync();
            var listdata = from l1 in list
                           join l2 in list1 on l1.SPDM equals l2.SPDM into a
                           from r in a.DefaultIfEmpty()
                               //where l1.RQ > r.ZZRQ6
                           select new
                           {
                               l1.SPDM,
                               l1.GHSMC,
                               l1.col,
                               l1.cm,
                               l1.RKSL,
                               l1.RQ,
                               l1.DM2,
                               hsDJ = r.JGDJ,
                               hsje = l1.RKSL * r.JGDJ
                           };

            return Json(new { code = 0, msg = "", count = listdata.Count(), data = listdata.Skip((page - 1) * limit).Take(limit).ToList() }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// fob 20%
        /// </summary>
        /// <param name="fobjs_fk"></param>
        /// <returns></returns>
        public ActionResult FOBFK(FOBJS_FK fobjs_fk)
        {
            var GCMC = Request["GCMC"];
            var HTH = Request["HTH"];
            var SPDM = Request["SPDM"];
            var je = Request["je"];
            var kp = Request["kp"];
            var remark = Request["remark"];
            var c = db.Queryable<FOBJS_FK>().With(SqlWith.NoLock).Where(m => m.HTH == HTH && m.TJzt == "1").Count();
            if (c > 0)
            {
                return Content("D");
            }

            else
            {
                if (db.Insertable<FOBJS_FK>(new
                {
                    SPDM,
                    HTH,
                    Money_1 = SqlFunc.ToDecimal(je),
                    jsRQ = DateTime.Now,
                    ZT = kp,
                    TJzt = 1,
                    GCMC,
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
        /// <param name="fobjs_fk"></param>
        /// <returns></returns>
        public ActionResult FOBSH(FOBJS_FK fobjs_fk)
        {
            string userId = Common.GetCookie("userLogin");
            var GCMC = Request["GCMC"];
            var HTH = Request["HTH"];
            var SPDM = Request["SPDM"];
            var je = Request["je"];
            var kp = Request["kp"];
            var remark = Request["remark"];
            if (db.Ado.SqlQuery<BPM_UserBase>(Usersql, new SugarParameter("@userId", userId)).Count() > 0)//&& m.TJzt == "1"
            {
                if (db.Queryable<FOBJS_FK>().With(SqlWith.NoLock).Where(m => m.HTH == HTH && SqlFunc.ContainsArray(zs, m.TJzt)).Count() > 0)
                {
                    if (db.Updateable<FOBJS_FK>(new
                    {
                        SPDM,
                        HTH,
                        Money_1 = SqlFunc.ToDecimal(je),
                        jsRQ = DateTime.Now,
                        ZT = kp,
                        SHzt2 = 1,
                        SHzt = 1,
                        GCMC,
                        remark
                    }).Where(u => u.HTH == HTH)
                    .ExecuteCommand() > 0)
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

                if (db.Queryable<FOBJS_FK>().With(SqlWith.NoLock).Where(m => m.HTH == HTH && m.TJzt == "1").Count() > 0)
                {
                    if (db.Updateable<FOBJS_FK>(new
                    {
                        SPDM,
                        HTH,
                        Money_1 = SqlFunc.ToDecimal(je),
                        jsRQ = DateTime.Now,
                        ZT = kp,
                        SHzt = 1,
                        GCMC,
                        remark
                    }).Where(u => u.HTH == HTH)
                    .ExecuteCommand() > 0)
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
        /// fob 70 提交
        /// </summary>
        /// <param name="fobjs_fk"></param>
        /// <returns></returns>
        public ActionResult FOBTJ70(FOBJS_FK fobjs_fk)
        {

            int[] zs = { 1 };
            //var SPDM = Request["SPDM"];
            var HTH = Request["HTH"];
            var daixiao = Request["daixiao"];
            var hsje = Request["hsje"];
            var tlkk = Request["tlkk"];
            var hqkk = Request["hqkk"];
            var cPkk = Request["cPkk"];
            var je_90 = Request["je_90"];
            var remark = Request["remark"];
            var lastmoney = Request["lastmoney"];
            if (db.Queryable<FOBJS_FK>().With(SqlWith.NoLock).Where(m => m.HTH == HTH && SqlFunc.ContainsArray(zs, m.TJzt)).Count() <= 0)
            {
                return Content("1");
            }
            else if (db.Updateable<FOBJS_FK>(new
            {
                daixiao,
                hsje = hsje,
                tlkk = tlkk,
                hqkk = hqkk,
                cPkk = cPkk,
                je_90 = je_90,
                Money_2 = lastmoney,
                TJzt = 2,
                jsRQ = DateTime.Now,
                remark = remark

            }).Where(u => u.HTH == HTH).ExecuteCommand() > 0)
            {
                return Content("D");
            }
            else
            {
                return Content("N");
            }
        }
        /// <summary>
        /// fob 70% 审核2
        /// </summary>
        /// <param name="fobjs_fk"></param>
        /// <returns></returns>
        public ActionResult FOBFK70(FOBJS_FK fobjs_fk)
        {
            string userId = Common.GetCookie("userLogin");
            var HTH = Request["HTH"];
            var daixiao = Request["daixiao"];
            var lastmoney = Request["lastmoney"];
            var remark = Request["remark"];
            if (db.Ado.SqlQuery<BPM_UserBase>(Usersql, new SugarParameter("@userId", userId)).Count() > 0)
            {
                if (db.Queryable<FOBJS_FK>().With(SqlWith.NoLock).Where(m => m.HTH == HTH && SqlFunc.ContainsArray(zs, m.TJzt)).Count() > 0)
                {
                    if (db.Updateable<FOBJS_FK>(new
                    {
                        daixiao,
                        hsje = fobjs_fk.hsje,
                        tlkk = fobjs_fk.tlkk,
                        hqkk = fobjs_fk.hqkk,
                        cPkk = fobjs_fk.cpkk,
                        je_90 = fobjs_fk.je_90,
                        Money_2 = lastmoney,
                        SHzt2 = 2,
                        SHzt = 2,
                        jsRQ = DateTime.Now,
                        remark = remark

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
                if (db.Queryable<FOBJS_FK>().With(SqlWith.NoLock).Where(m => m.HTH == HTH && m.TJzt == "2").Count() > 0)
                {
                    if (db.Updateable<FOBJS_FK>(new
                    {
                        daixiao,
                        hsje = fobjs_fk.hsje,
                        tlkk = fobjs_fk.tlkk,
                        hqkk = fobjs_fk.hqkk,
                        cPkk = fobjs_fk.cpkk,
                        je_90 = fobjs_fk.je_90,
                        Money_2 = lastmoney,
                        SHzt = 2,
                        jsRQ = DateTime.Now,
                        remark = remark

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
        /// foB提交10%
        /// </summary>
        /// <param name="fobjs_fk"></param>
        /// <returns></returns>
        public ActionResult FOBFKTJ10(FOBJS_FK fobjs_fk)
        {
            var HTH = Request["HTH"];
            var daixiao = Request["daixiao"];
            var hsje = Request["hsje"];
            var tlkk = Request["tlkk"];
            var hqkk = Request["hqkk"];
            var cPkk = Request["cPkk"];
            var je_90 = Request["je_90"];
            var KPmoney = Request["KPmoney"];
            var remark = Request["remark"];
            if (db.Queryable<FOBJS_FK>().Where(m => m.HTH == HTH && m.TJzt == "2").Count() <= 0)
            {
                return Content("1");
            }
            else if (db.Updateable<FOBJS_FK>(new
            {
                daixiao,
                hsje,
                tlkk,
                hqkk,
                cPkk,
                je_90 = je_90,
                Money_3 = KPmoney,
                TJzt = 3,
                jsRQ = DateTime.Now,
                remark = remark

            }).Where(u => u.HTH == HTH).ExecuteCommand() > 0)
            {
                return Content("y");
            }
            else
            {
                return Content("n");
            }

        }
        /// <summary>
        /// fob 10% 审核3
        /// </summary>
        /// <param name="fobjs_fk"></param>
        /// <returns></returns>
        public ActionResult FOBFK10(FOBJS_FK fobjs_fk)
        {

            string userId = Common.GetCookie("userLogin");
            var HTH = Request["HTH"];
            var daixiao = Request["daixiao"];
            var KPmoney = Request["KPmoney"];
            var remark = Request["remark"];
            if (db.Ado.SqlQuery<BPM_UserBase>(Usersql, new SugarParameter("@userId", userId)).Count() > 0)
            {
                if (db.Queryable<FOBJS_FK>().With(SqlWith.NoLock).Where(m => m.HTH == HTH && SqlFunc.ContainsArray(zs, m.TJzt)).Count() > 0)
                {
                    if (db.Updateable<FOBJS_FK>(new
                    {
                        daixiao,
                        hsje = fobjs_fk.hsje,
                        tlkk = fobjs_fk.tlkk,
                        hqkk = fobjs_fk.hqkk,
                        cPkk = fobjs_fk.cpkk,
                        je_90 = fobjs_fk.je_90,
                        Money_3 = KPmoney,
                        SHzt2 = 3,
                        SHzt = 3,
                        jsRQ = DateTime.Now,
                        remark = remark

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
                if (db.Queryable<FOBJS_FK>().With(SqlWith.NoLock).Where(m => m.HTH == HTH && m.TJzt == "3").Count() > 0)
                {
                    if (db.Updateable<FOBJS_FK>(new
                    {
                        daixiao,
                        hsje = fobjs_fk.hsje,
                        tlkk = fobjs_fk.tlkk,
                        hqkk = fobjs_fk.hqkk,
                        cPkk = fobjs_fk.cpkk,
                        je_90 = fobjs_fk.je_90,
                        Money_3 = KPmoney,
                        SHzt = 3,
                        jsRQ = DateTime.Now,
                        remark = remark

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
        public async Task<JsonResult> IndexFOb()
        {
            int[] jjdm = { 3, 4 };
            var Name = Request["Name"].ToString().Trim();
            var namespdm = Request["namespdm"].ToString().Trim();
            //var selectzt = Request["selectzt"];
            //var selectTJzt = Request["selectTJzt"];
            var zdrfob = Request["zdrfob"].ToString().Trim();
            var nameGC = Request["nameGC"].ToString().Trim();
            var year = Request["year"].ToString().Trim();
            var ji = Request["jijie"];
            var namebd = Request["namebd"].ToString().Trim();
            var page = int.Parse(Request["page"] ?? "1");
            var limit = int.Parse(Request["limit"] ?? "10");
            var list = await db.Queryable<SCZZD, SCZZDMX, SHANGPIN, JIJIE, GONGCHANG, FOBJS_FK, FJSX2>((s, sz, sp, jj, gc, fk, bd) => new object[] {
                JoinType.Left,s.DJBH==sz.DJBH,
                JoinType.Left,sz.SPDM==sp.SPDM,
                JoinType.Left,sp.BYZD5==jj.JJDM,
                JoinType.Left,s.GCDM==gc.GCDM,
                JoinType.Left,s.HTH==fk.HTH && s.SPDM==fk.SPDM,
                JoinType.Left,sp.FJSX2==bd.SXDM
            }).With(SqlWith.NoLock)
            .Where((s, sz, sp, jj, gc, fk) => sp.BYZD8 >= 2019 && SqlFunc.ContainsArray(jjdm, sp.BYZD5) && SqlFunc.StartsWith(s.HTH, "LX-F")|| SqlFunc.StartsWith(s.HTH, "LX-D") || sp.BYZD8 >= 2020 && SqlFunc.StartsWith(s.HTH, "LX-F") || SqlFunc.StartsWith(s.HTH, "LX-D"))
            .Where(s => s.SP != "1")//终止
            .WhereIF(!string.IsNullOrEmpty(Name), s =>SqlFunc.StartsWith(s.HTH,Name))
            //.WhereIF(!string.IsNullOrEmpty(selectzt), (s, sz, sp, jj, gc, fk) => fk.SHzt == selectzt)
            //.WhereIF(!string.IsNullOrEmpty(selectTJzt), (s, sz, sp, jj, gc, fk) => fk.TJzt == selectTJzt) SqlFunc.StartsWith(object thisValue, string parameterValue)
             .WhereIF(!string.IsNullOrEmpty(nameGC), (s, sz, sp, jj, gc, fk) => gc.GCMC.Contains(nameGC)) //gc.GCMC.Contains(nameGC)
             .WhereIF(!string.IsNullOrEmpty(namespdm), (s, sz, sp, jj, gc, fk) => SqlFunc.EndsWith(s.SPDM, namespdm))
              .WhereIF(!string.IsNullOrEmpty(year), (s, sz, sp, jj, gc, fk) => sp.BYZD8 == SqlFunc.ToInt32(year))
               .WhereIF(!string.IsNullOrEmpty(ji), (s, sz, sp, jj, gc, fk) => SqlFunc.StartsWith(sp.BYZD5,ji))
               .WhereIF(!string.IsNullOrEmpty(zdrfob), s => SqlFunc.StartsWith(s.ZDR,zdrfob))
                .WhereIF(!string.IsNullOrEmpty(namebd), (s, sz, sp, jj, gc, fk, bd) =>SqlFunc.StartsWith(bd.SXMC,namebd))
           .GroupBy((s, sz, sp, jj, gc, fk, bd) => new
           {
               s.SPDM,
               jj.JJMC,
               sp.BYZD8,
               s.HTH,
               gc.GCMC,
               gc.GHSDM,
               fk.Money_1,
               fk.Money_2,
               fk.Money_3,
               fk.SHzt,
               fk.ZT,
               fk.TJzt,
               fk.jsRQ,
               fk.SHzt2,
               fk.hsje,
               fk.remark,
               s.JGDJ,
               s.ZDR,
               bd.SXMC

           })
           .Select((s, sz, sp, jj, gc, fk, bd) => new
           {
               s.SPDM,
               jj.JJMC,
               sp.BYZD8,
               s.HTH,
               gc.GCMC,
               gc.GHSDM,
               ZZRQ6 = SqlFunc.AggregateMin(s.ZZRQ6),
               JHRQ = SqlFunc.AggregateMin(s.JHRQ),
               SL = SqlFunc.AggregateSum(sz.SL),
               JE = SqlFunc.AggregateSum(sz.BYZD6),
               CPSL = SqlFunc.AggregateSum(sz.SL_2),
               fk.Money_1,
               fk.Money_2,
               fk.Money_3,
               fk.SHzt,
               fk.ZT,
               fk.TJzt,
               fk.SHzt2,
               fk.hsje,
               fk.remark,
               s.JGDJ,
               s.ZDR,
               bd.SXMC
           }).OrderBy("fk.jsRQ desc").ToListAsync();
            var list2 = await db.Queryable<SPJHD, SPJHDMX, GONGHUOSHANG>((jh, jhmx, ghs) => new object[] {
                JoinType.Left,jh.DJBH==jhmx.DJBH,
                JoinType.Left,jh.DM1==ghs.GHSDM
            }).With(SqlWith.NoLock).Where((jh, jhmx, ghs) => ghs.TZSY == 0).
            GroupBy((jh, jhmx, ghs) => new { jhmx.SPDM, ghs.GHSDM }).Select((jh, jhmx, ghs) => new
            {
                jhmx.SPDM,
                ghs.GHSDM,
                rq = SqlFunc.AggregateMin(jh.RQ),
                sl = SqlFunc.AggregateSum(jhmx.SL),
                //hsje = SqlFunc.IsNull(SqlFunc.AggregateSum(jhmx.JE), 0)
            }).ToListAsync();
            var list2th = await db.Queryable<SPTHD, SPTHDMX, GONGHUOSHANG>((jh, jhmx, ghs) => new object[] {
                JoinType.Left,jh.DJBH==jhmx.DJBH,
                JoinType.Left,jh.DM1==ghs.GHSDM
            }).With(SqlWith.NoLock).Where((jh, jhmx, ghs) => ghs.TZSY == 0).
           GroupBy((jh, jhmx, ghs) => new { jhmx.SPDM, ghs.GHSDM }).Select((jh, jhmx, ghs) => new
           {
               jhmx.SPDM,
               ghs.GHSDM,
               rq = SqlFunc.AggregateMin(jh.RQ),
               sl = SqlFunc.AggregateSum(jhmx.SL),
               //hsje = SqlFunc.IsNull(SqlFunc.AggregateSum(jhmx.JE), 0)
           }).ToListAsync();

            var sdxdsl = await db.SqlQueryable<VIEWMODEL_SDXDSL>(sql3).Select(s => new
            {
                s.SPDM,
                s.Sl
            }).ToListAsync();
            var spjq = await db.SqlQueryable<VIEWMODEL_SPJQ>(SPJQ).Select(s => new
            {
                s.SCJD05,
                s.SCJD01
            }).ToListAsync();
            var listdata = from l1 in list
                           join l2 in list2 on new { l1.SPDM, l1.GHSDM } equals new { l2.SPDM, l2.GHSDM } into a
                           from r in a.DefaultIfEmpty()
                           join l2th in list2th on new { l1.SPDM, l1.GHSDM } equals new { l2th.SPDM, l2th.GHSDM } into ath
                           from rth in ath.DefaultIfEmpty()
                           join l3 in sdxdsl on l1.SPDM equals l3.SPDM into b
                           from r1 in b.DefaultIfEmpty()
                           join l4 in spjq on l1.SPDM equals l4.SCJD05 into c
                           from r2 in c.DefaultIfEmpty()
                               //orderby l1.SHzt descending
                           select new
                           {
                               l1.SPDM,
                               l1.JJMC,
                               l1.BYZD8,
                               l1.HTH,
                               l1.GCMC,
                               l1.GHSDM,
                               l1.ZZRQ6,
                               l1.JHRQ,
                               l1.SL,
                               l1.JE,
                               l1.CPSL,
                               l1.Money_1,
                               l1.Money_2,
                               l1.Money_3,
                               l1.SHzt,
                               l1.ZT,
                               l1.TJzt,
                               l1.SHzt2,
                               l1.hsje,
                               l1.remark,
                               l1.JGDJ,
                               l1.ZDR,
                               l1.SXMC,
                               rkrq = r != null ? r.rq : null,
                               rksl = r != null ? r.sl : null,
                               thsl = rth != null ? rth.sl : null,
                               sdxdsl = r1 != null ? r1.Sl : 0
                               //SCJD01 = r2 != null ? r2.SCJD01 : null
                               //hsje = r != null ? r.hsje : 0
                           };
            return Json(new { code = 0, msg = "", count = listdata.Count(), data = listdata.Skip((page - 1) * limit).Take(limit).ToList() }, JsonRequestBehavior.AllowGet);
        }
    }
}
