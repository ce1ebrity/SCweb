using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        public JsonResult IndexList(string Name, string JMS,string boduan)
        {
            var page = int.Parse(Request["page"] ?? "1");
            var limit = int.Parse(Request["limit"] ?? "10");
            var rq = Request["rq"];
            var list = db.Queryable<QDDBD, QDDBDMX, SHANGPIN, CANGKU, FJSX2>((q, qmx, sp, ck, f2) => new object[] {
                JoinType.Left,q.DJBH==qmx.DJBH,
                JoinType.Left,qmx.SPDM==sp.SPDM,
                JoinType.Left,q.DM1==ck.CKDM,
                JoinType.Left,sp.FJSX2==f2.SXDM
            }).With(SqlWith.NoLock).Where((q, qmx, sp, ck, f2) => sp.BYZD8 >= 2019)
            .WhereIF(!string.IsNullOrEmpty(Name), (q, qmx, sp, ck, f2)=>SqlFunc.StartsWith(qmx.DJBH,Name))
            .WhereIF(!string.IsNullOrEmpty(JMS), (q, qmx, sp, ck, f2) =>SqlFunc.StartsWith(ck.CKMC,JMS))
             .WhereIF(!string.IsNullOrEmpty(rq), q => q.RQ == SqlFunc.ToDate(rq))
             .WhereIF(!string.IsNullOrEmpty(boduan), (q, qmx, sp, ck, f2) => SqlFunc.StartsWith(f2.SXMC,boduan))

            .GroupBy((q, qmx, sp, ck, f2) => new
            {
                ck.CKMC,
                qmx.DJBH,
                f2.SXMC,
                sp.BYZD3,
                sp.BYZD8,
                qmx.ZK,
                q.BZ,
                q.RQ
            }).OrderBy(q => q.RQ, OrderByType.Desc)
            .Select((q, qmx, sp, ck, f2) => new
            {
                ck.CKMC,
                qmx.DJBH,
                f2.SXMC,
                sp.BYZD3,
                sp.BYZD8,
                qmx.ZK,
                SL = SqlFunc.AggregateSum(qmx.SL),
                BZSJ = SqlFunc.AggregateSum(sp.BZSJ),
                JE = SqlFunc.AggregateSum(qmx.JE),
                q.BZ,
                q.RQ
            });
            return Json(new { code = 0, msg = "", count = list.Count(), data = list.ToPageList(page, limit) }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FHlx()
        {
            return View();
        }
        public ActionResult FHlxIndex(string uname, string selectlx, string dd1, string dd2, string dd3, string remark)
        {
            string[] strArray = uname.Split(',');
            //uname = string.Empty;
            //uname = string.Join(",", strArray);
            if (db.Updateable<ViewModel_json_QDFH>(new
            {
                selectlx,
                dd1,
                dd2,
                dd3,
                remark,
                LB="发货"
                
            }).Where(u => SqlFunc.ContainsArray(strArray, u.DJBH))
             .ExecuteCommand() > 0)
            {
                return Content("1");
            }
            else
            {
                return Content("N");
            }
            
        }
        public ActionResult FHlxAdd()
        {
            var data = Request["data"];
            //List<string> strList = new List<string>();
            List<ViewModel_json_QDFH> l;
            JArray ja = (JArray)JsonConvert.DeserializeObject(data);
            l = ja.ToObject<List<ViewModel_json_QDFH>>();
            //foreach (var item in l)
            //{
            //    strList.Add(item.DJBH);
            //}
            //string[] str = strList.ToArray();
            var t5 = db.Insertable(l).ExecuteCommand();
            return Content("1");
        }
    }
}