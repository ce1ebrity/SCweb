using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SCWeb.Models;
using SqlSugar;

namespace SCWeb.Controllers
{
    public class TTTTTTController : BaseController
    {
        // GET: TTTTTT
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Edit()
        {
            return View();
        }
        public ActionResult EDitIndex(string spdm,string bd,string jijie)
        {
            //var t17 = db.Updateable<BS_BUS_Samples>().UpdateColumns(it =>
            //new BS_BUS_Samples()
            //{
            //    StageName = SqlFunc.Subqueryable<School>().Where(s => s.Id == it.SchoolId).Select(s => s.Id),
            //    Name = "newname"
            //}).Where(it => it.Id == 1).ExecuteCommand();
            var  bddm = BBdm(bd);
            var jjdm = 0;
            switch (jijie)
            {
                case "春季":
                    jjdm = 1;
                    break;
                case "夏季":
                    jjdm =2;
                    break;
                case "秋季":
                    jjdm = 3;
                    break;
                case "冬季":
                    jjdm = 4;
                    break;
            }
            try
            {
                db.Ado.BeginTran();
                if (db.Updateable<BS_BUS_Samples>(new { SeasonName = jijie, SeasonCode = jjdm, Property02 = bd, PrprCode02 = bddm })
                    .Where(u=>u.Code==spdm).ExecuteCommand() > 0)
                {
                    if (db.Updateable<BS_BUS_ColorBOMMaster>(new { SeasonName = jijie, SeasonCode = jjdm, StageName = bd, StageCode = bddm })
                      .Where(u => u.GoodsCode == spdm).ExecuteCommand() > 0)
                    {
                        return Content("y");
                    };
                }
                 
            }
            catch (Exception ex)
            {
                db.Ado.RollbackTran();
                throw ex;
            }
            return Content("n");
        }
        
        public async Task<JsonResult> List()
        {
            var page = int.Parse(Request["page"] ?? "1");
            var limit = int.Parse(Request["limit"] ?? "10");
            var year = Request["year"].ToString().Trim();
            var jijie = Request["jijie"].ToString().Trim();
            var namespdm = Request["namespdm"].ToString().Trim();
            var list = await db.Queryable<BS_BUS_Samples>()
            .Where(s => SqlFunc.ToInt32(s.YearCode) >= 2020)
            .WhereIF(!string.IsNullOrEmpty(year),s=>s.YearCode==year)
            .WhereIF(!string.IsNullOrEmpty(jijie),s => s.SeasonName == jijie)
            .WhereIF(!string.IsNullOrEmpty(namespdm), s => SqlFunc.StartsWith(s.Code,namespdm) || SqlFunc.EndsWith(s.Code, namespdm))
            .Select((s) => new
            {
                s.YearCode,
                s.SeasonName,
                s.Property02,
                s.Code,
                s.BigTypeName,
                s.Name
            }).ToListAsync();
            return Json(new { code = 0, data = list.Skip((page - 1) * limit).Take(limit), count=list.Count()}, JsonRequestBehavior.AllowGet);
        }
    }
}