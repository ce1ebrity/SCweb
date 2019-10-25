using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SCWeb.Models;
using SqlSugar;
namespace SCWeb.Controllers
{
    public class FOBdxController : BaseController
    {
        // GET: FOBdx
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Edit()
        {
            return View();
        }

        public async Task<JsonResult> FOBdxrk(string spdm, string ggmc)
        {
            var page = int.Parse(Request["page"] ?? "1");
            var limit = int.Parse(Request["limit"] ?? "10");
            var list = await db.Queryable<SPJHD, SPJHDMX, GUIGE1>((s, sp, g) => new object[] {
                JoinType.Left,s.DJBH==sp.DJBH,
                JoinType.Left,sp.GG1DM==g.GGDM
            }).With(SqlWith.NoLock).Where((s, sp, g) => s.DM2 == "0000" && sp.SPDM == spdm && g.GGMC == ggmc).GroupBy((s, sp, g) => new { sp.SPDM, g.GGMC, sp.DJ }).Select((s, sp, g) => new
            {
                sp.SPDM,
                g.GGMC,
                RKSL = SqlFunc.AggregateSum(sp.SL),
                RQ = SqlFunc.AggregateMin(s.RQ),
                hsDJ = sp.DJ,
                hsje = SqlFunc.AggregateSum(sp.JE)
            }).ToPageListAsync(page, limit);
            return Json(new { code = 0, msg = "", count = list.Count(), data = list }, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> FOBkucun(string spdm, string ggmc)
        {
            var page = int.Parse(Request["page"] ?? "1");
            var limit = int.Parse(Request["limit"] ?? "10");
            var list = await db.Queryable<SPJHD, SPJHDMX, GUIGE1>((s, sp, g) => new object[] {
                JoinType.Left,s.DJBH==sp.DJBH,
                JoinType.Left,sp.GG1DM==g.GGDM
            }).With(SqlWith.NoLock).Where((s, sp, g) => s.DM2 == "0000" && sp.SPDM == spdm && g.GGMC == ggmc).GroupBy((s, sp, g) => new { sp.SPDM, g.GGMC, sp.DJ }).Select((s, sp, g) => new
            {
                sp.SPDM,
                g.GGMC,
                RKSL = SqlFunc.AggregateSum(sp.SL),
                RQ = SqlFunc.AggregateMin(s.RQ),
                hsDJ = sp.DJ,
                hsje = SqlFunc.AggregateSum(sp.JE)
            }).ToListAsync();

            var zpthsl = await db.SqlQueryable<_view_zpthsl>(zpspthd).Where(s => s.SPDM == spdm && s.GGMC == ggmc).Select(s => new
            {
                s.SPDM,
                s.GGMC,
                s.zpthsl
            }).ToListAsync();
            var cpthsl = await db.SqlQueryable<_view_cpthsl>(cpspthd).Where(s => s.SPDM == spdm && s.GGMC == ggmc).Select(s => new
            {
                s.SPDM,
                s.GGMC,
                s.cpthsl
            }).ToListAsync();

            var list1 = await db.SqlQueryable<view_model_kucun>(kucun).Where(s => s.SPDM == spdm && s.GGMC == ggmc).Select(s => new
            {
                s.SPDM,
                s.GGMC,
                s.KCSl
            }).ToListAsync();
            var listdata = from li1 in list
                           join li2 in list1 on new { li1.SPDM, li1.GGMC } equals new { li2.SPDM, li2.GGMC } into a
                           from r in a.DefaultIfEmpty()
                           join li3 in zpthsl on new { li1.SPDM, li1.GGMC } equals new { li3.SPDM, li3.GGMC } into b
                           from r1 in b.DefaultIfEmpty()
                           join li4 in cpthsl on new { li1.SPDM, li1.GGMC } equals new { li4.SPDM, li4.GGMC } into c
                           from r2 in c.DefaultIfEmpty()
                           select new
                           {
                               li1.SPDM,
                               li1.RKSL,
                               li1.GGMC,
                               KCSl = r != null ? r.KCSl : 0,
                               zpthsl = r1 != null ? r1.zpthsl : 0,
                               cpthsl = r2 != null ? r2.cpthsl : 0,
                           };
            return Json(new { code = 0, msg = "", count = listdata.Count(), data = listdata.Skip((page - 1) * limit).Take(limit).ToList() }, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> IndexFOBdx(string spdm, string GGMC)
        {
            var page = int.Parse(Request["page"] ?? "1");
            var limit = int.Parse(Request["limit"] ?? "10");
            var list = await db.Queryable<SCZZD, SCZZDMX, GUIGE1>((s, sz, g) => new object[] {
                JoinType.Left,s.DJBH==sz.DJBH,
                JoinType.Left,sz.GG1DM==g.GGDM
            }).With(SqlWith.NoLock).Where((s, sz, g) => s.SPDM == spdm && g.GGMC == GGMC).GroupBy((s, sz, g) => new { s.SPDM, s.JGDJ, s.JHRQ, g.GGMC }).
            Select((s, sz, g) => new
            {
                s.SPDM,
                g.GGMC,
                s.JHRQ,
                htsl = SqlFunc.AggregateSum(sz.SL),
                DJ = s.JGDJ,
                htje = SqlFunc.AggregateSum(sz.BYZD6)

            }).ToListAsync();
            var sdxdsl = await db.SqlQueryable<VIEWMODEL_SDXDSL>(sql5).Where(s => s.SPDM == spdm && s.GGMC == GGMC).Select(s => new
            {
                s.SPDM,
                s.GGMC,
                s.Sl
            }).ToListAsync();
            var listdata = from l1 in list
                           join l2 in sdxdsl on new { l1.SPDM, l1.GGMC } equals new { l2.SPDM, l2.GGMC } into a
                           from r in a.DefaultIfEmpty()
                           select new
                           {
                               l1.SPDM,
                               l1.GGMC,
                               l1.JHRQ,
                               l1.htsl,
                               l1.DJ,
                               l1.htje,
                               XDSL = r != null ? r.Sl : 0,
                           };

            return Json(new { code = 0, msg = "", count = listdata.Count(), data = listdata.Skip((page - 1) * limit).Take(limit).ToList() }, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> IndexList()
        {
            var name = Request["Name"];
            var page = int.Parse(Request["page"] ?? "1");
            var limit = int.Parse(Request["limit"] ?? "10");
            var list = await db.Queryable<View_model_fobdaixiao, SHANGPIN, JIJIE, FJSX2>((v, sp, jj, f2) => new object[] {
                JoinType.Left,v.SPDM==sp.SPDM,
                JoinType.Left,sp.BYZD5 == jj.JJDM,
                JoinType.Left,sp.FJSX2==f2.SXDM,
            }).With(SqlWith.NoLock).WhereIF(!string.IsNullOrEmpty(name), v => v.HTH == name).Select((v, sp, jj, f2) => new
            {
                sp.BYZD8,
                jj.JJMC,
                f2.SXMC,
                v.SPDM,
                v.GCMC,
                v.GGMC,
                v.HTH,
                v.htsl,
                v.htje
            }).ToListAsync();
            var list1 = await db.Queryable<SPJHD, SPJHDMX, GUIGE1>((s, sp, g) => new object[] {
                JoinType.Left,s.DJBH==sp.DJBH,
                JoinType.Left,sp.GG1DM ==g.GGDM
            }).With(SqlWith.NoLock).Where((s, sp, g) => s.DM2 == "0000").GroupBy((s, sp, g) => new { sp.SPDM, g.GGMC }).Select((s, sp, g) => new
            {
                sp.SPDM,
                g.GGMC,
                RKSL = SqlFunc.AggregateSum(sp.SL),
            }).ToListAsync();
            var data = from l1 in list
                       join l2 in list1 on new { l1.SPDM, l1.GGMC } equals new { l2.SPDM, l2.GGMC } into a
                       from r in a.DefaultIfEmpty()
                       select new
                       {
                           l1.BYZD8,
                           l1.SPDM,
                           l1.JJMC,
                           l1.SXMC,
                           l1.GCMC,
                           l1.GGMC,
                           l1.HTH,
                           l1.htsl,
                           l1.htje,
                           JHSL = r != null ? r.RKSL : null
                       };
            return Json(new { code = 0, msg = "", count = data.Count(), data = data.Skip((page - 1) * limit).Take(limit).ToList() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ImportExcel()
        {
            HttpPostedFileBase File = Request.Files["file"];
            string content = "";
            if (File.ContentLength > 0)
            {
                var Isxls = System.IO.Path.GetExtension(File.FileName).ToString().ToLower();
                if (Isxls != ".xls" && Isxls != ".xlsx")
                {
                    Content("请上传Excel文件");
                }
                var FileName = File.FileName;
                var path = Server.MapPath("~/Upload/" + FileName);
                File.SaveAs(path);
                DataTable dt = ExcelToTable(path);
                try
                {
                    db.Ado.BeginTran();
                    var students = dt.AsEnumerable();
                    List<Use111> listaaa = students.Select(
                               x => new Use111
                               {
                                   Name = x.Field<string>("姓名"),
                                   Age = Convert.ToInt32(x.Field<string>("年龄")),
                                   Sex = x.Field<string>("性别"),
                                   Phone = x.Field<string>("爱好"),
                                   Remark = x.Field<string>("地址"),
                                   Rq = Convert.ToDateTime(x.Field<string>("日期"))
                               }).ToList();
                    //
                    //db.Insertable(listaaa).InsertColumns(i => new { i.Name, i.Age }).ExecuteReturnIdentity() >


                    if (db.Insertable(listaaa).ExecuteCommand() > 0)
                    {
                        return Json(new { code = 0, msg = "success", data = "",count =listaaa.Count() }, JsonRequestBehavior.AllowGet);
                    }
                    db.Ado.CommitTran();
                }
                catch (Exception ex)
                {
                    db.Ado.RollbackTran();
                    return Json(new { code = 0, msg = "error", data = "" }, JsonRequestBehavior.AllowGet);
                    throw ex;

                }
                
            }
            
            //return Content(content);
            return Json(new { code = 0, msg = "success", data = "" }, JsonRequestBehavior.AllowGet);
        }
    }
}