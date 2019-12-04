using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
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
        public async Task<JsonResult> FOB70(string spdm, string HTH, string GCMC, string GHSDM, string ggmc)
        {
            string gcmc = Server.UrlDecode(GCMC);
            var page = int.Parse(Request["page"] ?? "1");
            var limit = int.Parse(Request["limit"] ?? "10");
            var list = await db.Queryable<SPJHD, SPJHDMX, GUIGE1, GUIGE2, GONGHUOSHANG>((s, sp, g1, g2, ghs) => new object[] {
                JoinType.Left,s.DJBH==sp.DJBH,
                JoinType.Left,sp.GG1DM==g1.GGDM,
                JoinType.Left,sp.GG2DM==g2.GGDM,
                JoinType.Left,s.DM1==ghs.GHSDM
            }).With(SqlWith.NoLock).Where((s, sp, g1, g2, ghs) => sp.SPDM == spdm && ghs.GHSDM == GHSDM && g1.GGMC==ggmc).GroupBy((s, sp, g1, g2, ghs) => new
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

        //public async Task<JsonResult> FOBdxrk(string spdm, string ggmc, string GHSDM, string HTH)
        //{
        //    var page = int.Parse(Request["page"] ?? "1");
        //    var limit = int.Parse(Request["limit"] ?? "10");
        //    var list = await db.Queryable<SPJHD, SPJHDMX, GUIGE1, GONGHUOSHANG>((s, sp, g,ghs) => new object[] {
        //        JoinType.Left,s.DJBH==sp.DJBH,
        //        JoinType.Left,sp.GG1DM==g.GGDM,
        //        JoinType.Left,s.DM1==ghs.GHSDM
        //    }).With(SqlWith.NoLock).Where((s, sp, g,ghs) => s.DM2 == "0000" && sp.SPDM == spdm && g.GGMC == ggmc && ghs.GHSDM== GHSDM)
        //    .GroupBy((s, sp, g,ghs) => new { sp.SPDM, g.GGMC, sp.DJ, ghs.GHSDM }).Select((s, sp, g) => new
        //    {
        //        sp.SPDM,
        //        g.GGMC,
        //        RKSL = SqlFunc.AggregateSum(sp.SL),
        //        RQ = SqlFunc.AggregateMin(s.RQ),
        //        hsDJ = sp.DJ,
        //        //hsje = SqlFunc.AggregateSum(sp.JE)
        //    }).ToPageListAsync(page, limit);
        //    return Json(new { code = 0, msg = "", count = list.Count(), data = list }, JsonRequestBehavior.AllowGet);
        //}
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
            var list = await db.Queryable<View_model_fobdaixiao, SHANGPIN, JIJIE, FJSX2,GONGHUOSHANG>((v, sp, jj, f2,ghs) => new object[] {
                JoinType.Left,v.SPDM==sp.SPDM,
                JoinType.Left,sp.BYZD5 == jj.JJDM,
                JoinType.Left,sp.FJSX2==f2.SXDM,
                JoinType.Left,v.GCMC==ghs.GHSMC
            }).With(SqlWith.NoLock).WhereIF(!string.IsNullOrEmpty(name), v => v.HTH == name).Select((v, sp, jj, f2,ghs) => new
            {
                sp.BYZD8,
                jj.JJMC,
                f2.SXMC,
                v.SPDM,
                v.GCMC,
                ghs.GHSDM,
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
                           l1.GHSDM,
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
            if (string.IsNullOrWhiteSpace(Request["id"]))
            {
                return Json(new { code = 0, msg = "请选择导入类型", data = "", count="" }, JsonRequestBehavior.AllowGet);
            }
            else if (Request["id"] == "1")
            {
                HttpPostedFileBase File = Request.Files["file"];
                if (File.ContentLength > 0)
                {
                    var Isxls = System.IO.Path.GetExtension(File.FileName).ToString().ToLower();
                    if (Isxls != ".xls" && Isxls != ".xlsx")
                    {
                        return Json(new { code = 0, msg = "请上传excel文件", data = "", count = "" });
                    }
                    var FileName = File.FileName;
                    var path = Server.MapPath("~/Upload/" + FileName);
                    File.SaveAs(path);
                    DataTable dt = ExcelToTable(path);
                    try
                    {
                        db.Ado.BeginTran();
                        var YF = dt.AsEnumerable();
                        List<BS_BUS_Samples> listaaa = YF.Select(
                                   x => new BS_BUS_Samples
                                   {
                                       Code = x.Field<string>("款号"),
                                       Name = x.Field<string>("款号描述"),
                                       SimpleName = SqlFunc.ToUpper(GetPYstring(x.Field<string>("款号描述"))),
                                       Designer = x.Field<string>("设计师"),
                                       ChiefDesigner = x.Field<string>("首席"),
                                       BrandCode = x.Field<string>("品牌"),
                                       YearCode = x.Field<string>("年度"),
                                       SeasonCode = x.Field<string>("季节代码"),
                                       SeasonName = x.Field<string>("季节名称"),
                                       UnitName = x.Field<string>("单位"),
                                       Property01 = x.Field<string>("性别"),
                                       Property02 = x.Field<string>("波段"),
                                       BigTypeCode = x.Field<string>("大类代码"),
                                       BigTypeName = x.Field<string>("大类名称"),
                                       PrprCode03 = x.Field<string>("款式类别代码"),
                                       Property03 = x.Field<string>("款式类别名称"),
                                       Property04 = x.Field<string>("订货类型"),
                                       Property05 = x.Field<string>("加工方式"),
                                       ColorCode = x.Field<string>("颜色"),
                                       SizeName = x.Field<string>("尺码"),
                                       MasterID = getGUID(),
                                       CreateTime = DateTime.Now,
                                       YearName = "年",
                                       Operator = "系统管理员",
                                       OperatorID = "8299572F-3939-4E28-95DE-80AC88D606C2",
                                       State = 1
                                   }).ToList();
                        if (db.Insertable(listaaa).IgnoreColumns(it => new { it.LastUpdateTime }).ExecuteCommand() > 0)
                        {
                            //foreach (var item in listaaa)
                            //{
                            //    var im = db.Insertable<BS_BUS_SampleImage>(new
                            //    {
                            //        DetailID = getGUID(),
                            //        MasterID = item.MasterID,
                            //        Code = item.Code,
                            //        Name = item.Name,
                            //        SimpleName = item.SimpleName,
                            //        CreateTime = DateTime.Now,
                            //        state = 1,
                            //        Operator = "系统管理员",
                            //        OperatorID = "8299572F-3939-4E28-95DE-80AC88D606C2"
                            //    }).ExecuteCommand();
                            //}
                            foreach (var item in listaaa)
                            {
                                string[] sArraycolor = item.ColorCode.Split(new char[1] { ',' });
                                string[] sArraysize = item.SizeName.Split(new char[1] { ',' });
                                var a = sArraycolor.Count();
                                var b = sArraysize.Count();
                                for (int i = 0; i < a; i++)
                                {
                                    if (!string.IsNullOrWhiteSpace(sArraycolor[i]))
                                    {

                                        var color = sArraycolor[i];
                                        var list = db.Queryable<GUIGE1>().Where(u => u.GGDM == color).Select((u => new { Name = u.GGMC })).ToList();
                                        foreach (var itemcolor in list)
                                        {

                                            var Namecolor = itemcolor.Name;
                                            var co = db.Insertable<BS_BUS_SampleColor>(new
                                            {
                                                DetailID = getGUID(),
                                                MasterID = item.MasterID,
                                                Code = sArraycolor[i],
                                                Name = Namecolor,
                                                SimpleName = SqlFunc.ToUpper(GetPYstring(Namecolor)),
                                                CreateTime = DateTime.Now,
                                                state = 1,
                                                Operator = "系统管理员",
                                                OperatorID = "8299572F-3939-4E28-95DE-80AC88D606C2"
                                            }).ExecuteCommand();

                                        }

                                    }
                                }
                                for (int j = 0; j < b; j++)
                                {
                                    if (!string.IsNullOrWhiteSpace(sArraysize[j]))
                                    {
                                        var size = sArraysize[j];
                                        var list = db.Queryable<GUIGE2>().Where(u => u.GGMC == size).Select((u => new { Name = u.GGDM })).ToList();
                                        foreach (var itemsize in list)
                                        {
                                            var NameSize = itemsize.Name;
                                            var si = db.Insertable<BS_BUS_SampleSize>(new
                                            {
                                                DetailID = getGUID(),
                                                MasterID = item.MasterID,
                                                Code = sArraysize[j],
                                                Name = NameSize,
                                                SimpleName = size,
                                                CreateTime = DateTime.Now,
                                                state = 1,
                                                Operator = "系统管理员",
                                                OperatorID = "8299572F-3939-4E28-95DE-80AC88D606C2"
                                            }).ExecuteCommand();
                                        }

                                    }
                                }
                            }

                        }
                        else
                        {
                            db.Ado.RollbackTran();
                            return Json(new { code = 0, msg = "Error:：", data = "" }, JsonRequestBehavior.AllowGet);
                        }
                        db.Ado.CommitTran();
                    }
                    catch (Exception ex)
                    {
                        
                        db.Ado.RollbackTran();
                        return Json(new { code = 0, msg = "Error:：" + ex.Message, data = "" }, JsonRequestBehavior.AllowGet);
                        //throw ex;
                    }
                }

            }
            else if (Request["id"] == "2")
            {
                HttpPostedFileBase File = Request.Files["file"];
                if (File.ContentLength > 0)
                {
                    var Isxls = System.IO.Path.GetExtension(File.FileName).ToString().ToLower();
                    if (Isxls != ".xls" && Isxls != ".xlsx")
                    {
                        return Json(new { code = 0, msg = "请上传excel文件", data = "", count = "" });
                    }
                    var FileName = File.FileName;
                    var path = Server.MapPath("~/Upload/" + FileName);
                    File.SaveAs(path);
                    DataTable dt = ExcelToTable(path);
                    try
                    {
                        db.Ado.BeginTran();
                        var sp = dt.AsEnumerable();
                        List<SHANGPIN> listaaa = sp.Select(
                                   x => new SHANGPIN
                                   {
                                       SPDM = x.Field<string>("款号"),
                                       SPMC = x.Field<string>("款号描述"),
                                       //SimpleName = SqlFunc.ToUpper(GetPYstring(x.Field<string>("款号描述"))),
                                       BYZD3 = x.Field<string>("品牌"),
                                       BYZD8 = Convert.ToInt32(x.Field<string>("年度")),
                                       BYZD5 = x.Field<string>("季节代码"),
                                       DWMC = x.Field<string>("单位"),
                                       FJSX1 = XBDM(x.Field<string>("性别")),
                                       FJSX2 = BBdm(x.Field<string>("波段")),
                                       BYZD4 = x.Field<string>("大类代码"),
                                       FJSX3 = x.Field<string>("款式类别代码"),
                                       FJSX4 = DHLX(x.Field<string>("订货类型")),
                                       FJSX5 = "000",
                                       FJSX6 = x.Field<string>("加工方式"),
                                       FJSX8 = KXDM(x.Field<string>("廓形")),
                                       FJSX9 = XLDM(x.Field<string>("系列")),
                                       FJSX10 = CZDM(x.Field<string>("材质")),
                                       FJSX11 = CDDM(x.Field<string>("长短")),
                                       FJSX12 = SCOC(x.Field<string>("生产批次")),
                                       FJSX13 = JQ(x.Field<string>("交期")),
                                       FJSX14 = XXFL(x.Field<string>("细小分类")),
                                       BZSJ = x.Field<decimal>("单价"),
                                       TZSY = "0",
                                       ONLINE = "0",
                                       FJSX15 = "000",
                                       FJSX16 = "000",
                                       FJSX7 = "000",
                                       BYZD1 = "0",
                                       BYZD2 = "2",
                                       BYZD10 = 0,
                                       BYZD11 = 0,
                                       BYZD12 = 1,
                                       BYZD13 = 1,
                                       BYZD14 = DateTime.Now,
                                       BYZD15 = DateTime.Now

                                   }).ToList();
                        //var result = db.Ado.UseTran(() =>
                        //{
                        //    var a = db.Insertable(listaaa).IgnoreColumns(it => new { it.LastChanged }).ExecuteCommand();
                        //    db.Ado.RollbackTran();
                        //    //throw new Exception("error haha"); 测试代码
                        //});
                        if (db.Insertable(listaaa).IgnoreColumns(it => new { it.LastChanged }).ExecuteCommand() > 0)
                        {
                            foreach (var item in listaaa)
                            {
                                var im = db.Insertable<SHANGPIN_A>(new
                                {
                                    SPDM = item.SPDM,
                                    DHAO = 0.0000,
                                    FK = 0.0000,
                                    DJ = 0.0000,
                                    DJ1 = 0.0000,
                                    DJ2 = 0.0000,
                                    DJ4 = 0.0000,
                                    GJSD = "0",
                                    GJHHK = 0.0000,
                                    BYZD3 = "系统导入",
                                    BYZD4 = "系统导入",
                                    BYZD8 = 0,
                                    BYZD9 = 0,
                                    BYZD10 = 1
                                }).ExecuteCommand();
                            }
                            ////////////////////////////////////////
                            var GG_color = dt.AsEnumerable();
                            List<SPGG1> list_color = GG_color.Select(
                                       x => new SPGG1
                                       {
                                           SPDM = x.Field<string>("款号"),
                                           GGDM = x.Field<string>("颜色"),
                                           //SizeName = x.Field<string>("尺码"),
                                       }).ToList();
                            foreach (var item in list_color)
                            {
                                string[] sArraycolor = item.GGDM.Split(new char[1] { ',' });
                                var a = sArraycolor.Count();
                                for (int i = 0; i < a; i++)
                                {
                                    if (!string.IsNullOrWhiteSpace(sArraycolor[i]))
                                    {

                                        var color = sArraycolor[i];
                                        DateTime d = DateTime.Now;
                                        var list = db.Queryable<SPGG1>().Where(u => u.GGDM == color).ToList();
                                        var co = db.Insertable<SPGG1>(new
                                        {

                                            SPDM = item.SPDM,
                                            GGDM = sArraycolor[i],
                                            BYZD2 = "000",
                                            BYZD3 = 1,
                                            BYZD4 = (1.00 / a).ToString("0.00####"),
                                            BYZD6 = "系统导入",
                                        }).ExecuteCommand();
                                    }
                                }
                            }

                            var GG_size = dt.AsEnumerable();
                            List<SPGG2> list_size = GG_size.Select(
                                       x => new SPGG2
                                       {
                                           SPDM = x.Field<string>("款号"),
                                           GGDM = x.Field<string>("尺码"),
                                           //SizeName = x.Field<string>("尺码"),
                                       }).ToList();
                            foreach (var item in list_size)
                            {
                                string[] sArraysize = item.GGDM.Split(new char[1] { ',' });
                                var a = sArraysize.Count();
                                for (int i = 0; i < a; i++)
                                {
                                    if (!string.IsNullOrWhiteSpace(sArraysize[i]))
                                    {
                                        var color = sArraysize[i];
                                        var list = db.Queryable<SPGG2>().Where(u => u.GGDM == color).ToList();
                                        var co = db.Insertable<SPGG2>(new
                                        {
                                            SPDM = item.SPDM,
                                            GGDM = GUIGE2(sArraysize[i]),
                                            BYZD2 = "000",
                                            BYZD3 = 1,
                                            BYZD4 = (1.00 / a).ToString("0.00####"),
                                        }).ExecuteCommand();
                                    }
                                }
                            }
                        }
                        else
                        {
                            db.Ado.RollbackTran();
                            return Json(new { code = 0, msg = "error", data = "", count = "" }, JsonRequestBehavior.AllowGet);
                        }
                        db.Ado.CommitTran();
                    }
                    catch (Exception ex)
                    {

                        db.Ado.RollbackTran();
                        return Json(new { code = 0, msg = "Error:：" + ex.Message, data = "" }, JsonRequestBehavior.AllowGet);
                        //throw ex;
                    }
                }
            }
            else if (Request["id"] == "3")
            {
                HttpPostedFileBase File = Request.Files["file"];
                if (File.ContentLength > 0)
                {
                    var Isxls = System.IO.Path.GetExtension(File.FileName).ToString().ToLower();
                    if (Isxls != ".xls" && Isxls != ".xlsx")
                    {
                        return Json(new { code = 0, msg = "请上传excel文件", data = "", count = "" });
                    }
                    var FileName = File.FileName;
                    var path = Server.MapPath("~/Upload/" + FileName);
                    File.SaveAs(path);
                    DataTable dt = ExcelToTable(path);
                    try
                    {
                        db.Ado.BeginTran();
                        var YF = dt.AsEnumerable();
                        List<BS_BUS_Samples> listaaa = YF.Select(
                                   x => new BS_BUS_Samples
                                   {
                                       Code = x.Field<string>("款号"),
                                       Name = x.Field<string>("款号描述"),
                                       SimpleName = SqlFunc.ToUpper(GetPYstring(x.Field<string>("款号描述"))),
                                       Designer = x.Field<string>("设计师"),
                                       ChiefDesigner = x.Field<string>("首席"),
                                       BrandCode = x.Field<string>("品牌"),
                                       YearCode = x.Field<string>("年度"),
                                       SeasonCode = x.Field<string>("季节代码"),
                                       SeasonName = x.Field<string>("季节名称"),
                                       UnitName = x.Field<string>("单位"),
                                       Property01 = x.Field<string>("性别"),
                                       Property02 = x.Field<string>("波段"),
                                       BigTypeCode = x.Field<string>("大类代码"),
                                       BigTypeName = x.Field<string>("大类名称"),
                                       PrprCode03 = x.Field<string>("款式类别代码"),
                                       Property03 = x.Field<string>("款式类别名称"),
                                       Property04 = x.Field<string>("订货类型"),
                                       Property05 = x.Field<string>("加工方式"),
                                       ColorCode = x.Field<string>("颜色"),
                                       SizeName = x.Field<string>("尺码"),
                                       MasterID = getGUID(),
                                       CreateTime = DateTime.Now,
                                       YearName = "年",
                                       Operator = "系统管理员",
                                       OperatorID = "8299572F-3939-4E28-95DE-80AC88D606C2",
                                       State = 1
                                   }).ToList();
                        if (db.Insertable(listaaa).IgnoreColumns(it => new { it.LastUpdateTime }).ExecuteCommand() > 0)
                        {
                            //foreach (var item in listaaa)
                            //{
                            //    var im = db.Insertable<BS_BUS_SampleImage>(new
                            //    {
                            //        DetailID = getGUID(),
                            //        MasterID = item.MasterID,
                            //        Code = item.Code,
                            //        Name = item.Name,
                            //        SimpleName = item.SimpleName,
                            //        CreateTime = DateTime.Now,
                            //        state = 1,
                            //        Operator = "系统管理员",
                            //        OperatorID = "8299572F-3939-4E28-95DE-80AC88D606C2"
                            //    }).ExecuteCommand();
                            //}
                            foreach (var item in listaaa)
                            {
                                string[] sArraycolor = item.ColorCode.Split(new char[1] { ',' });
                                string[] sArraysize = item.SizeName.Split(new char[1] { ',' });
                                var a = sArraycolor.Count();
                                var b = sArraysize.Count();
                                for (int i = 0; i < a; i++)
                                {
                                    if (!string.IsNullOrWhiteSpace(sArraycolor[i]))
                                    {

                                        var color = sArraycolor[i];
                                        var list = db.Queryable<GUIGE1>().Where(u => u.GGDM == color).Select((u => new { Name = u.GGMC })).ToList();
                                        foreach (var itemcolor in list)
                                        {

                                            var Namecolor = itemcolor.Name;
                                            var co = db.Insertable<BS_BUS_SampleColor>(new
                                            {
                                                DetailID = getGUID(),
                                                MasterID = item.MasterID,
                                                Code = sArraycolor[i],
                                                Name = Namecolor,
                                                SimpleName = SqlFunc.ToUpper(GetPYstring(Namecolor)),
                                                CreateTime = DateTime.Now,
                                                state = 1,
                                                Operator = "系统管理员",
                                                OperatorID = "8299572F-3939-4E28-95DE-80AC88D606C2"
                                            }).ExecuteCommand();

                                        }

                                    }
                                }
                                for (int j = 0; j < b; j++)
                                {
                                    if (!string.IsNullOrWhiteSpace(sArraysize[j]))
                                    {
                                        var size = sArraysize[j];
                                        var list = db.Queryable<GUIGE2>().Where(u => u.GGMC == size).Select((u => new { Name = u.GGDM })).ToList();
                                        foreach (var itemsize in list)
                                        {
                                            var NameSize = itemsize.Name;
                                            var si = db.Insertable<BS_BUS_SampleSize>(new
                                            {
                                                DetailID = getGUID(),
                                                MasterID = item.MasterID,
                                                Code = sArraysize[j],
                                                Name = NameSize,
                                                SimpleName = size,
                                                CreateTime = DateTime.Now,
                                                state = 1,
                                                Operator = "系统管理员",
                                                OperatorID = "8299572F-3939-4E28-95DE-80AC88D606C2"
                                            }).ExecuteCommand();
                                        }

                                    }
                                }
                            }

                        }
                        else
                        {
                            db.Ado.RollbackTran();
                            return Json(new { code = 0, msg = "Error:：", data = "" }, JsonRequestBehavior.AllowGet);
                        }
                        db.Ado.CommitTran();
                    }
                    catch (Exception ex)
                    {
                        db.Ado.RollbackTran();
                        return Json(new { code = 0, msg = "Error:：" + ex.Message, data = "" }, JsonRequestBehavior.AllowGet);
                        //throw ex;
                    }
                    try
                    {
                        db.Ado.BeginTran();
                        var sp = dt.AsEnumerable();
                        List<SHANGPIN> listaaa = sp.Select(
                                   x => new SHANGPIN
                                   {
                                       SPDM = x.Field<string>("款号"),
                                       SPMC = x.Field<string>("款号描述"),
                                       //SimpleName = SqlFunc.ToUpper(GetPYstring(x.Field<string>("款号描述"))),
                                       BYZD3 = x.Field<string>("品牌"),
                                       BYZD8 = Convert.ToInt32(x.Field<string>("年度")),
                                       BYZD5 = x.Field<string>("季节代码"),
                                       DWMC = x.Field<string>("单位"),
                                       FJSX1 = XBDM(x.Field<string>("性别")),
                                       FJSX2 = BBdm(x.Field<string>("波段")),
                                       BYZD4 = x.Field<string>("大类代码"),
                                       FJSX3 = x.Field<string>("款式类别代码"),
                                       FJSX4 = DHLX(x.Field<string>("订货类型")),
                                       FJSX5 = "000",
                                       FJSX6 = x.Field<string>("加工方式"),
                                       FJSX8 = KXDM(x.Field<string>("廓形")),
                                       FJSX9 = XLDM(x.Field<string>("系列")),
                                       FJSX10 = CZDM(x.Field<string>("材质")),
                                       FJSX11 = CDDM(x.Field<string>("长短")),
                                       FJSX12 = SCOC(x.Field<string>("生产批次")),
                                       FJSX13 = JQ(x.Field<string>("交期")),
                                       FJSX14 = XXFL(x.Field<string>("细小分类")),
                                       BZSJ = x.Field<decimal>("单价"),
                                       TZSY = "0",
                                       ONLINE = "0",
                                       FJSX15 = "000",
                                       FJSX16 = "000",
                                       FJSX7 = "000",
                                       BYZD1 = "0",
                                       BYZD2 = "2",
                                       BYZD10 = 0,
                                       BYZD11 = 0,
                                       BYZD12 = 1,
                                       BYZD13 = 1,
                                       BYZD14 = DateTime.Now,
                                       BYZD15 = DateTime.Now

                                   }).ToList();
                        //var result = db.Ado.UseTran(() =>
                        //{
                        //    var a = db.Insertable(listaaa).IgnoreColumns(it => new { it.LastChanged }).ExecuteCommand();
                        //    db.Ado.RollbackTran();
                        //    //throw new Exception("error haha"); 测试代码
                        //});
                        if (db.Insertable(listaaa).IgnoreColumns(it => new { it.LastChanged }).ExecuteCommand() > 0)
                        {
                            foreach (var item in listaaa)
                            {
                                var im = db.Insertable<SHANGPIN_A>(new
                                {
                                    SPDM = item.SPDM,
                                    DHAO = 0.0000,
                                    FK = 0.0000,
                                    DJ = 0.0000,
                                    DJ1 = 0.0000,
                                    DJ2 = 0.0000,
                                    DJ4 = 0.0000,
                                    GJSD = "0",
                                    GJHHK = 0.0000,
                                    BYZD3 = "系统导入",
                                    BYZD4 = "系统导入",
                                    BYZD8 = 0,
                                    BYZD9 = 0,
                                    BYZD10 = 1
                                }).ExecuteCommand();
                            }
                            ////////////////////////////////////////
                            var GG_color = dt.AsEnumerable();
                            List<SPGG1> list_color = GG_color.Select(
                                       x => new SPGG1
                                       {
                                           SPDM = x.Field<string>("款号"),
                                           GGDM = x.Field<string>("颜色"),
                                           //SizeName = x.Field<string>("尺码"),
                                       }).ToList();
                            foreach (var item in list_color)
                            {
                                string[] sArraycolor = item.GGDM.Split(new char[1] { ',' });
                                var a = sArraycolor.Count();
                                for (int i = 0; i < a; i++)
                                {
                                    if (!string.IsNullOrWhiteSpace(sArraycolor[i]))
                                    {

                                        var color = sArraycolor[i];
                                        DateTime d = DateTime.Now;
                                        var list = db.Queryable<SPGG1>().Where(u => u.GGDM == color).ToList();
                                        var co = db.Insertable<SPGG1>(new
                                        {

                                            SPDM = item.SPDM,
                                            GGDM = sArraycolor[i],
                                            BYZD2 = "000",
                                            BYZD3 = 1,
                                            BYZD4 = (1.00 / a).ToString("0.00####"),
                                            BYZD6 = "系统导入",
                                        }).ExecuteCommand();
                                    }
                                }
                            }

                            var GG_size = dt.AsEnumerable();
                            List<SPGG2> list_size = GG_size.Select(
                                       x => new SPGG2
                                       {
                                           SPDM = x.Field<string>("款号"),
                                           GGDM = x.Field<string>("尺码"),
                                           //SizeName = x.Field<string>("尺码"),
                                       }).ToList();
                            foreach (var item in list_size)
                            {
                                string[] sArraysize = item.GGDM.Split(new char[1] { ',' });
                                var a = sArraysize.Count();
                                for (int i = 0; i < a; i++)
                                {
                                    if (!string.IsNullOrWhiteSpace(sArraysize[i]))
                                    {
                                        var color = sArraysize[i];
                                        var list = db.Queryable<SPGG2>().Where(u => u.GGDM == color).ToList();
                                        var co = db.Insertable<SPGG2>(new
                                        {
                                            SPDM = item.SPDM,
                                            GGDM = GUIGE2(sArraysize[i]),
                                            BYZD2 = "000",
                                            BYZD3 = 1,
                                            BYZD4 = (1.00 / a).ToString("0.00####"),
                                        }).ExecuteCommand();
                                    }
                                }
                            }
                        }
                        else
                        {
                            db.Ado.RollbackTran();
                            return Json(new { code = 0, msg = "error", data = "", count = "" }, JsonRequestBehavior.AllowGet);
                        }
                        db.Ado.CommitTran();
                    }
                    catch (Exception ex)
                    {

                        db.Ado.RollbackTran();
                        return Json(new { code = 0, msg = "Error:：" + ex.Message, data = "" }, JsonRequestBehavior.AllowGet);
                        //throw ex;
                    }
                }
            }
            return Json(new { code = 0, msg = "Success", data = "", count = "" }, JsonRequestBehavior.AllowGet);
        }
     
    }
}