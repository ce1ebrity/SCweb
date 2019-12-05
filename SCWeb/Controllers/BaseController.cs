using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using SCWeb.Models;
using SqlSugar;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.SS.Util;
using System.IO;

namespace SCWeb.Controllers
{

    public class BaseController : Controller
    {
        public SqlSugarClient db
        {
            get
            {
                return DBcontext.GetContext();
            }
        }

        public void FOB(string spdm, string HTH)
        {
            ViewData.Model = db.Queryable<FOBJS_FK>().Where(u => u.SPDM == spdm && u.HTH == HTH).Select(u => new FOBJS_FK
            {
                SPDM = u.SPDM,
                Money_1 = u.Money_1,
                Money_2 = u.Money_2,
                Money_3 = u.Money_3,
                ZT = u.ZT,
                SHzt = u.SHzt,
                daixiao = u.daixiao,
                hsje = u.hsje,
                tlkk = u.tlkk,
                hqkk = u.hqkk,
                cpkk = u.cpkk,
                je_90 = u.je_90
            }).First();
        }
        public void ML(string YDJH)
        {
            ViewData.Model = db.Queryable<MLJS>().Where(u => u.YDJH == YDJH).First();
        }
        public string getGUID()
        {
            System.Guid guid = new Guid();
            guid = Guid.NewGuid();
            string str = guid.ToString();
            return str;
        }
        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new ToJsonResult
            {
                Data = data,
                ContentEncoding = contentEncoding,
                ContentType = contentType,
                JsonRequestBehavior = behavior,
                FormateStr = "yyyy-MM-dd"
                //yyyy-MM-dd HH:mm:ss
            };
        }
        protected JsonResult MyJson(object data)
        {
            return new ToJsonResult
            {
                Data = data,
                FormateStr = "yyyy-MM-dd"
            };
        }
        public string XBDM(string xbdm)
        {
            switch (xbdm)
            {
                case "未定义":
                    xbdm= "000";
                    break;
                case "不分性别":
                    xbdm= "0";
                    break;
                case "女童":
                    xbdm= "2";
                    break;
                case "女装":
                    xbdm= "3";
                    break;
                case "男童":
                    xbdm= "1";
                    break;
                case "男":
                    xbdm= "4";
                    break;
                case "女":
                    xbdm= "5";
                    break;
                case "中性":
                    xbdm= "6";
                    break;
                default:
                    xbdm = "";
                    break;
            }
            return xbdm;
        }

        public string JGFS(string JGFSDM)
        {
            switch (JGFSDM)
            {
                case "未定义":
                    JGFSDM = "000";
                    break;
                case "FOB":
                    JGFSDM = "FOB";
                    break;
                case "CMT":
                    JGFSDM = "CMT";
                    break;
                case "外采":
                    JGFSDM = "WC";
                    break;
                default:
                    JGFSDM = "000";
                    break;
            }
            return JGFSDM;
        }
        public string DHLX(string lxmc)
        {
            switch (lxmc)
            {
                case "订货-普通(含童装羽绒)":
                    lxmc = "000";
                    break;
                case "P-皮草":
                    lxmc = "200";
                    break;
                case "Y/S-羽绒/饰品":
                    lxmc = "300";
                    break;
                case "G-工衣":
                    lxmc = "100";
                    break;
                default:
                    lxmc = "";
                    break;

            }
            return lxmc;
        }
        public string BBdm(string bbmc)
        {
          var bbdm = "";
          var list = db.Queryable<FJSX2>().Where(u => u.SXMC == bbmc).Select(u => new { BBdm = u.SXDM }).ToList();
            foreach (var item in list)
            {
              bbdm = item.BBdm;
            }
          return bbdm;
        }
        public string KXDM(string kxmc)
        {
            var kxdm = "";
            var list = db.Queryable<FJSX8>().Where(u => u.SXMC == kxmc).Select(u => new { kxmc = u.SXDM }).ToList();
            foreach (var item in list)
            {
                kxdm = item.kxmc;
            }
            return kxdm;
        }
        public string XLDM(string xlmc)
        {
            var xldm = "";
            var list = db.Queryable<FJSX9>().Where(u => u.SXMC == xlmc).Select(u => new { xlmc = u.SXDM }).ToList();
            foreach (var item in list)
            {
                xldm = item.xlmc;
            }
            return xldm;
        }
        public string CZDM(string CZmc)
        {
            var czdm = "";
            var list = db.Queryable<FJSX10>().Where(u => u.SXMC == CZmc).Select(u => new { czmx = u.SXDM }).ToList();
            foreach (var item in list)
            {
                czdm = item.czmx;
            }
            return czdm;
        }
        public string CDDM(string cdmc)
        {
            var cddm = "";
            var list = db.Queryable<FJSX11>().Where(u => u.SXMC == cdmc).Select(u => new { cdmc = u.SXDM }).ToList();
            foreach (var item in list)
            {
                cddm = item.cdmc;
            }
            return cddm;
        }
        public string SCOC(string pcmc)
        {
            switch (pcmc)
            {
                case "未定义":
                    pcmc = "000";
                    break;
                case "首单":
                    pcmc = "0";
                    break;
                case "第一次翻单":
                    pcmc = "1";
                    break;
                case "第二次翻单":
                    pcmc = "2";
                    break;
                default:
                    pcmc = "";
                    break;

            }
            return pcmc;
        }
        public string JQ(string jqmc)
        {
            var jqdm = "";
            var list = db.Queryable<FJSX13>().Where(u => u.SXMC == jqmc).Select(u => new { jqmc = u.SXDM }).ToList();
            foreach (var item in list)
            {
                jqdm = item.jqmc;
            }
            return jqdm;
        }
        public string GUIGE2(string ggdm2)
        {
            var gg2mc = "";
            var list = db.Queryable<GUIGE2>().Where(u => u.GGMC == ggdm2).Select(u => new { gg2mc = u.GGDM }).ToList();
            foreach (var item in list)
            {
                gg2mc = item.gg2mc;
            }
            return gg2mc;
        }
        public string XXFL(string flmc)
        {
            var fldm = "";
            var list = db.Queryable<FJSX14>().Where(u => u.SXMC == flmc).Select(u => new { flmc = u.SXDM }).ToList();
            foreach (var item in list)
            {
                fldm = item.flmc;
            }
            return fldm;
        }
        public class ToJsonResult : JsonResult
        {
            const string error = "该请求已被封锁，因为敏感信息透露给第三方网站，这是一个GET请求时使用的。为了可以GET请求，请设置JsonRequestBehavior AllowGet。";
            /// <summary>
            /// 格式化字符串
            /// </summary>
            public string FormateStr
            {
                get;
                set;
            }
            /// <summary>
            /// </summary>
            /// <param name="context"></param>
            public override void ExecuteResult(ControllerContext context)
            {
                if (context == null)
                {
                    throw new ArgumentNullException("context");
                }
                if (JsonRequestBehavior == JsonRequestBehavior.DenyGet &&
                    String.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                {
                    throw new InvalidOperationException(error);
                }
                HttpResponseBase response = context.HttpContext.Response;

                if (!String.IsNullOrEmpty(ContentType))
                {
                    response.ContentType = ContentType;
                }
                else
                {
                    response.ContentType = "application/json";
                }
                if (ContentEncoding != null)
                {
                    response.ContentEncoding = ContentEncoding;
                }
                if (Data != null)
                {
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    string jsonstring = serializer.Serialize(Data);
                    string p = @"\\/Date\(\d+\)\\/";
                    MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertJsonDateToDateString);
                    Regex reg = new Regex(p);
                    jsonstring = reg.Replace(jsonstring, matchEvaluator);
                    response.Write(jsonstring);
                }
            }

            /// <summary>
            /// 说明：将Json序列化的时间由/Date(1294499956278+0800)转为字符串
            /// </summary>
            private string ConvertJsonDateToDateString(Match m)
            {
                string result = string.Empty;
                string p = @"\d";
                var cArray = m.Value.ToCharArray();
                StringBuilder sb = new StringBuilder();
                Regex reg = new Regex(p);
                for (int i = 0; i < cArray.Length; i++)
                {
                    if (reg.IsMatch(cArray[i].ToString()))
                    {
                        sb.Append(cArray[i]);
                    }
                }
                DateTime dt = new DateTime(1970, 1, 1);
                dt = dt.AddMilliseconds(long.Parse(sb.ToString()));
                dt = dt.ToLocalTime();
                result = dt.ToString(this.FormateStr);
                return result;
            }
        }
        public class MyAuthorizeAttribute : AuthorizeAttribute
        {
            protected override bool AuthorizeCore(HttpContextBase httpContext)
            {
                bool Pass = true;
                Uri UrlReferrer = httpContext.Request.UrlReferrer;
                if (UrlReferrer == null)
                {
                    httpContext.Response.StatusCode = 401;

                    Pass = false;
                }
                else
                {
                    Uri ThisUrl = httpContext.Request.Url;
                    if (UrlReferrer.Authority != ThisUrl.Authority)
                    {
                        httpContext.Response.StatusCode = 401;
                        Pass = false;
                    }

                }
                return Pass;
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="filterContext"></param>
            protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
            {
                base.HandleUnauthorizedRequest(filterContext);
                if (filterContext.HttpContext.Response.StatusCode == 401)
                    filterContext.HttpContext.Response.Redirect("~/HomePage/HomeLogin");
                //base.HandleUnauthorizedRequest(filterContext);
            }

        }
        /// <summary>
        /// 类型枚举
        /// </summary>
        private enum ModelType
        {
            //值类型
            Struct,
            Enum,
            //引用类型
            String,
            Object,
            Else
        }
        /// <summary>
        /// //db.CodeFirst.InitTables(typeof(viewModel_SPJHD));
        //var list123 = await db.SqlQueryable<viewModel_SPJHD>(sql).WhereIF(!string.IsNullOrEmpty(spdm), U => U.SPDM == spdm)//.Where(u => u.RQ > u.jhRQ)
        //    .Select((u) => new
        //    {
        //        u.BYZD8,
        //        u.JJMC,
        //        u.YDJH,
        //        u.GCMC,
        //        u.SPDM,
        //        u.ZZRQ6,
        //        u.JHRQ,
        //        HTSL = u.sl,
        //        HTJE = u.je,
        //        CPSL = u.cpsl,
        //        JHSL = u.jhsl,
        //        JHSL1 = u.jhsl1,
        //        RKRQ = u.rkRQ
        //    }).ToPageListAsync(page,limit);
        /// </summary>
        /// 
        public static string sqlimg = @"select TOP 50 s.MasterID,s.Code,s1.SimpleImage,ship.YearCode AS BYZD8,
	                        mm.SeasonName,mm.StageName from VW_ZF_SamplesInfo s
	                        left join BS_BUS_SampleImage s1 on s.MasterID=s1.MasterID
	                        left join SHANGPIN sp(nolock) on sp.SPDM=s.Code
	                        left join BPM_P_SPCBYSB_New_Ship ship on s.Code=ship.SPDM
	                        left join BS_BUS_ColorBOMMaster mm on s.Code=mm.GoodsCode
	                        where s.YearCode>=2020";
        public static string sql = @"SELECT A.BYZD8,A.JJMC,A.YDJH,A.GCMC,A.SPDM,A.ZZRQ6,A.JHRQ,A.SL,A.JE,A.CPSL,B.JHSL,C.JHSL1,B.RKRQ from (
                                     SELECT SP.BYZD8,JJ.JJMC,S.YDJH,GC.GCMC,S.SPDM,MIN(S.ZZRQ6)AS ZZRQ6,MIN(S.JHRQ)AS JHRQ,SUM(SMX.SL) as SL,SUM(SMX.BYZD6)as JE,sum(SMX.SL_2)as CPSL from SCZZD(nolock) S 
                                     LEFT JOIN SCZZDMX(nolock) SMX on S.DJBH = SMX.DJBH
                                     LEFT JOIN SHANGPIN(nolock) SP on SMX.SPDM = SP.SPDM
                                     LEFT JOIN JIJIE(nolock) JJ on SP.BYZD5 = JJ.JJDM
                                     LEFT JOIN GONGCHANG(nolock) GC on S.GCDM=GC.GCDM
                                     WHERE SP.FJSX6='CMT' AND SP.BYZD3 IN('B','C','K') and SP.BYZD8>='2018'
                                     GROUP BY SP.BYZD8,JJ.JJMC,S.YDJH,GC.GCMC,S.SPDM)A
                                     left join 
                                     (SELECT JHMX.SPDM,sum(JHMX.SL)as JHSL,MIN(JH.RQ)AS RKRQ from SPJHD(nolock)JH 
	                                    LEFT JOIN SPJHDMX(nolock) JHMX on JH.DJBH=JHMX.DJBH
	                                    WHERE JH.DM2='0000' 
	                                    GROUP BY JHMX.SPDM)B on A.SPDM =B.SPDM
                                      left join 
                                     ( SELECT JHMX.SPDM,sum(JHMX.SL)as JHSL1,MIN(JH.RQ)AS RKRQ1 from SPJHD(nolock)JH 
	                                    LEFT JOIN SPJHDMX(nolock) JHMX on JH.DJBH=JHMX.DJBH
	                                    WHERE JH.DM2='0300' 
	                                    GROUP BY JHMX.SPDM)C on A.SPDM =C.SPDM";

        public static string sql2 = @"SELECT A.SPDM,A.CPSL,B.JHSL,C.JHSL1 FROM(
								        SELECT S1.SPDM,SUM(S1.SL_2)AS CPSL FROM SCZZD(nolock) S LEFT JOIN SCZZDMX(nolock) S1 ON S.DJBH=S1.DJBH
								        GROUP BY S1.SPDM) A
								        LEFT JOIN 
								        (
									        SELECT JHMX.SPDM,sum(JHMX.SL)as JHSL,MIN(JH.RQ) AS RQ from SPJHD(nolock) JH 
									        LEFT JOIN SPJHDMX(nolock) JHMX on JH.DJBH=JHMX.DJBH
									        where JH.DM2='0000' 
									        group by JHMX.SPDM
								        )B ON A.SPDM=B.SPDM
								        LEFT JOIN 
								        (
									        SELECT JHMX.SPDM,sum(JHMX.SL)as JHSL1,MIN(JH.RQ) AS RQ from SPJHD(nolock) JH 
									        LEFT JOIN SPJHDMX(nolock) JHMX on JH.DJBH=JHMX.DJBH
									        where JH.DM2='0300' 
									        group by JHMX.SPDM
								        )C ON A.SPDM=C.SPDM";

        public static string sql3 = @" Select s.SPDM,SUM(s.SL)as sl from(
                                        SELECT DMX.SPDM,DMX.SL FROM DBJRD (nolock) D 
                                        LEFT JOIN DBJRDMX (nolock) DMX ON D.DJBH=DMX.DJBH
                                        LEFT JOIN SHANGPIN(NOLOCK) sp on DMX.SPDM=sp.SPDM
                                        where sp.BYZD8>=2019
                                        UNION ALL
                                        SELECT PMX.SPDM,PMX.SL AS SL FROM PHJRD (nolock) P  
                                        LEFT JOIN PHJRDMX (nolock) PMX ON P.DJBH = PMX.DJBH
                                        LEFT JOIN SHANGPIN(NOLOCK) sp on PMX.SPDM=sp.SPDM
                                        where sp.BYZD8>=2019)s
                                        group by s.SPDM";

        public static string sql33 = @"Select a.SPDM,a.col,a.cm,SUM(a.SL)as sl from(
		                               SELECT DMX.SPDM,DMX.SL,gg1.GGMC as col,gg2.GGMC as cm FROM DBJRD (nolock) D 
									   LEFT JOIN DBJRDMX (nolock) DMX ON D.DJBH=DMX.DJBH
									   left join GUIGE1 gg1 on DMX.GG1DM=gg1.GGDM
										left join GUIGE2 gg2 on DMX.GG2DM=gg2.GGDM
		                               UNION ALL
		                               SELECT PMX.SPDM,PMX.SL AS SL,gg1.GGMC as col,gg2.GGMC as cm FROM PHJRD (nolock) P  
									   LEFT JOIN PHJRDMX (nolock) PMX ON P.DJBH = PMX.DJBH
									    left join GUIGE1 gg1 on PMX.GG1DM=gg1.GGDM
										left join GUIGE2 gg2 on PMX.GG2DM=gg2.GGDM
                                       WHERE left(p.DJBH,3) = 'PA1'
		                               )a group by a.SPDM,a.col,a.cm";

        public static string sql5 = @" Select a.SPDM,A.GGMC,SUM(a.SL)as sl from(
		                               SELECT DMX.SPDM,G.GGMC,DMX.SL FROM DBJRD (nolock) D LEFT JOIN DBJRDMX (nolock) DMX ON D.DJBH=DMX.DJBH
									   LEFT JOIN GUIGE1 (nolock) G ON DMX.GG1DM =G.GGDM
		                               UNION ALL
		                               SELECT PMX.SPDM,G.GGMC,PMX.SL AS SL FROM PHJRD (nolock) P  LEFT JOIN PHJRDMX (nolock) PMX ON P.DJBH = PMX.DJBH
									    LEFT JOIN GUIGE1 (nolock) G ON PMX.GG1DM =G.GGDM
                                       WHERE left(p.DJBH,3) = 'PA1'
		                               )a group by a.SPDM,A.GGMC";

        public static string kucun = @"select  d.BYZD3,d.年份,d.季节,d.SPDM,d.波段,sum(d.SL2 + d.SL3)as KCSl,sum((d.SL2 + d.SL3)*d.BZSJ)as KCje,d.GGMC from (
                                        select sp.BYZD3,sp.BYZD8 as 年份,jj.JJMC as 季节,sp.SPDM,FJSX2.SXMC as 波段,sp.BZSJ,
                                        SUM(CASE WHEN  QR='1'  THEN SL ELSE 0 END) AS SL2,
                                        SUM(CASE WHEN  QR='0' AND YS='1'  THEN SL ELSE 0 END) AS SL3,g.GGMC
                                        from VW_CKJXCMX(nolock) vm
                                        inner join SHANGPIN(nolock) sp on vm.SPDM = sp.SPDM
                                        inner join GUIGE1(nolock) g on vm.GG1DM = g.GGDM
                                        inner join JIJIE(nolock) jj on sp.BYZD5 = jj.JJDM
                                        inner join FJSX2(nolock) on sp.FJSX2 = FJSX2.SXDM
                                        inner join CANGKU(nolock) ck on vm.CKDM = ck.CKDM and ck.LBDM in('000','001')
                                        group by sp.BYZD3,sp.BYZD8 ,jj.JJMC,sp.SPDM,FJSX2.SXMC,sp.BZSJ,g.GGMC)d
                                        group by d.BYZD3,d.年份,d.季节,d.SPDM,d.波段,d.GGMC";

        public static string zpspthd = @"		select smx.SPDM,g.GGMC,SUM(smx.SL) as zpthsl from SPTHD(nolock) s
						                    left join SPTHDMX (nolock)smx on s.DJBH=smx.DJBH
						                    left join GUIGE1(nolock)g on smx.GG1DM = g.GGDM
						                    where s.DM2='0000'
						                    group by  smx.SPDM,g.GGMC";
        public static string cpspthd = @"		select smx.SPDM,g.GGMC,SUM(smx.SL) as cpthsl from SPTHD(nolock) s
						                    left join SPTHDMX (nolock)smx on s.DJBH=smx.DJBH
						                    left join GUIGE1(nolock)g on smx.GG1DM = g.GGDM
						                    where s.DM2='0300'
						                    group by  smx.SPDM,g.GGMC";

        public static string Usersql = "select id,trueName from BPM_UserBase where id = @userId and trueName ='顾晓棠'";

        public static string qddbdd = @"select a.CKMC,a.BYZD8,a.BYZD3,
                                       a.JJMC,a.SXMC,a.ZK,
	                                   a.ddsl,a.ddje,b.ddnfhsl,b.ddnfhje,c.ddnthsl,c.ddnthje,
	                                (b.ddnfhsl-c.ddnthsl)as ddnjfsl,(b.ddnfhje-c.ddnthje)as ddnjfje,d.ddwfhsl,d.ddwfhje,
									e.ddwthsl,e.ddwthje, (d.ddwfhsl-e.ddwthsl)as ddwjfsl,(d.ddwfhje-e.ddwthje)as ddwjfje
	                                    from (
                                select ck.CKMC,
                                       sp.BYZD8,
                                       sp.BYZD3,
                                       jj.JJMC,
                                       f2.SXMC,
                                       dmx.ZK,
	                                   sum(dmx.SL)as ddsl,
	                                   sum(dmx.JE)as ddje
                                from DBJRD d with(nolock) left join DBJRDMX dmx with(nolock) on d.DJBH = dmx.DJBH
                                left join SHANGPIN sp with(nolock) on dmx.SPDM=sp.SPDM
                                left join  JIJIE jj with(nolock) on jj.JJDM=sp.BYZD5
                                left join FJSX2 f2 with(nolock) on f2.SXDM=sp.FJSX2
                                left join  CANGKU ck with(nolock) on d.DM1=ck.CKDM
                                where sp.BYZD8>='2019'
                                group by ck.CKMC,sp.BYZD8,sp.BYZD3, jj.JJMC,f2.SXMC,dmx.ZK)a
                                left join
		                                (select CKMC,SXMC,sum(SL) as ddnfhsl, 
				                                sum(JE) as ddnfhje, BYZD3,BYZD8 from ViewModel_json_QDFH with(nolock) where LB='发货' and selectlx in('订单内非买断','订单内买断','补货','当季代销','过季代销')
				                                group by CKMC, SXMC,BYZD3,BYZD8)b  
		                                on a.CKMC=b.CKMC and a.BYZD3=b.BYZD3 and a.SXMC=b.SXMC  and a.BYZD8=b.BYZD8
                                left join
		                                (select CKMC,SXMC,sum(SL) as ddnthsl, 
				                                sum(JE) as ddnthje, BYZD3,BYZD8 from ViewModel_json_QDFH with(nolock) where LB='退货' and selectlx in('订单退货','当季代销退货','过季代销退货')
				                                group by CKMC, SXMC,BYZD3,BYZD8)c 
		                                on a.CKMC=c.CKMC and a.BYZD3=c.BYZD3 and a.SXMC=c.SXMC and a.BYZD8 = c.BYZD8
								 left join
		                                (select CKMC,SXMC,sum(SL) as ddwfhsl, 
				                                sum(JE) as ddwfhje, BYZD3,BYZD8 from ViewModel_json_QDFH with(nolock) where LB='发货' and selectlx not in('订单内非买断','订单内买断','补货','当季代销','过季代销')
				                                group by CKMC, SXMC,BYZD3,BYZD8)d 
		                                on a.CKMC=d.CKMC and a.BYZD3=d.BYZD3 and a.SXMC=d.SXMC  and a.BYZD8=d.BYZD8
								  left join
		                                (select CKMC,SXMC,sum(SL) as ddwthsl, 
				                                sum(JE) as ddwthje, BYZD3,BYZD8 from ViewModel_json_QDFH with(nolock) where LB='退货' and selectlx not in('订单退货','当季代销退货','过季代销退货')
				                                group by CKMC, SXMC,BYZD3,BYZD8)e
		                                on a.CKMC=e.CKMC and a.BYZD3=e.BYZD3 and a.SXMC=e.SXMC and a.BYZD8 = e.BYZD8";
        public static string SPJQ = @"select top(1) CONVERT(varchar,SCJD01,23)as SCJD01,SCJD05 from BPM_SCJDB WITH(NOLOCK)";


        /// <summary>
        /// Excel导入成DataTble
        /// </summary>
        /// <param name="file">导入路径(包含文件名与扩展名)</param>
        /// <returns></returns>
        public static DataTable ExcelToTable(string file)
        {
            DataTable dt = new DataTable();
            IWorkbook workbook;
            string fileExt = Path.GetExtension(file).ToLower();
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                if (fileExt == ".xlsx") { workbook = new XSSFWorkbook(fs); } else if (fileExt == ".xls") { workbook = new HSSFWorkbook(fs); } else { workbook = null; }
                if (workbook == null) { return null; }
                ISheet sheet = workbook.GetSheetAt(0);

                //表头  
                IRow header = sheet.GetRow(sheet.FirstRowNum);
                List<int> columns = new List<int>();
                for (int i = 0; i < header.LastCellNum; i++)
                {
                    object obj = GetValueType(header.GetCell(i));
                    if (obj == null || obj.ToString() == string.Empty)
                    {
                        dt.Columns.Add(new DataColumn("Columns" + i.ToString()));
                    }
                    else
                        dt.Columns.Add(new DataColumn(obj.ToString()));
                    columns.Add(i);
                }
                //数据  
                for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
                {
                    DataRow dr = dt.NewRow();
                    bool hasValue = false;
                    foreach (int j in columns)
                    {
                        dr[j] = GetValueType(sheet.GetRow(i).GetCell(j));
                        if (dr[j] != null && dr[j].ToString() != string.Empty)
                        {
                            hasValue = true;
                        }
                    }
                    if (hasValue)
                    {
                        dt.Rows.Add(dr);
                    }
                }
            }
            return dt;
        }
        /// <summary>
        /// 获取单元格类型
        /// </summary>
        /// <param name="cell">目标单元格</param>
        /// <returns></returns>
        private static object GetValueType(ICell cell)
        {
            if (cell == null)
                return null;
            switch (cell.CellType)
            {
                case CellType.Blank:
                    return null;
                case CellType.Boolean:
                    return cell.BooleanCellValue;
                case CellType.Numeric: //NUMERIC:  
                    short format = cell.CellStyle.DataFormat;
                    if (format != 0) { return cell.DateCellValue; } else { return cell.NumericCellValue; }
                case CellType.String:
                    return cell.StringCellValue;
                case CellType.Error:
                    return cell.ErrorCellValue;

                case CellType.Formula:
                default:
                    return "=" + cell.CellFormula;
            }
        }
        /// <summary>
        /// 设置单元格数据类型
        /// </summary>
        /// <param name="cell">目标单元格</param>
        /// <param name="obj">数据值</param>
        /// <returns></returns>
        public static void SetCellValue(ICell cell, object obj)
        {
            if (obj.GetType() == typeof(int))
            {
                cell.SetCellValue((int)obj);
            }
            else if (obj.GetType() == typeof(double))
            {
                cell.SetCellValue((double)obj);
            }
            else if (obj.GetType() == typeof(IRichTextString))
            {
                cell.SetCellValue((IRichTextString)obj);
            }
            else if (obj.GetType() == typeof(string))
            {
                cell.SetCellValue(obj.ToString());
            }
            else if (obj.GetType() == typeof(DateTime))
            {
                cell.SetCellValue((DateTime)obj);
            }
            else if (obj.GetType() == typeof(bool))
            {
                cell.SetCellValue((bool)obj);
            }
            else
            {
                cell.SetCellValue(obj.ToString());
            }
        }
        private static ModelType GetModelType(Type modelType)
        {
            //值类型
            if (modelType.IsEnum)
            {
                return ModelType.Enum;
            }
            //值类型
            if (modelType.IsValueType)
            {
                return ModelType.Struct;
            }
            //引用类型 特殊类型处理
            if (modelType == typeof(string))
            {
                return ModelType.String;
            }
            //引用类型 特殊类型处理
            return modelType == typeof(object) ? ModelType.Object : ModelType.Else;
        }
        /// <summary>
        /// datatable转换为List<T>集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static List<T> DataTableToList<T>(DataTable table)
        {
            var list = new List<T>();
            foreach (DataRow item in table.Rows)
            {
                list.Add(DataRowToModel<T>(item));
            }
            return list;
        }

        public static T DataRowToModel<T>(DataRow row)
        {
            T model;
            var type = typeof(T);
            var modelType = GetModelType(type);
            switch (modelType)
            {
                //值类型
                case ModelType.Struct:
                    {
                        model = default(T);
                        if (row[0] != null)
                            model = (T)row[0];
                    }
                    break;
                //值类型
                case ModelType.Enum:
                    {
                        model = default(T);
                        if (row[0] != null)
                        {
                            var fiType = row[0].GetType();
                            if (fiType == typeof(int))
                            {
                                model = (T)row[0];
                            }
                            else if (fiType == typeof(string))
                            {
                                model = (T)Enum.Parse(typeof(T), row[0].ToString());
                            }
                        }
                    }
                    break;
                //引用类型 c#对string也当做值类型处理
                case ModelType.String:
                    {
                        model = default(T);
                        if (row[0] != null)
                            model = (T)row[0];
                    }
                    break;
                //引用类型 直接返回第一行第一列的值
                case ModelType.Object:
                    {
                        model = default(T);
                        if (row[0] != null)
                            model = (T)row[0];
                    }
                    break;
                //引用类型
                case ModelType.Else:
                    {
                        //引用类型 必须对泛型实例化
                        model = Activator.CreateInstance<T>();
                        //获取model中的属性
                        var modelPropertyInfos = type.GetProperties();
                        //遍历model每一个属性并赋值DataRow对应的列
                        foreach (var pi in modelPropertyInfos)
                        {
                            //获取属性名称
                            var name = pi.Name;
                            if (!row.Table.Columns.Contains(name) || row[name] == null) continue;
                            var piType = GetModelType(pi.PropertyType);
                            switch (piType)
                            {
                                case ModelType.Struct:
                                    {
                                        var value = Convert.ChangeType(row[name], pi.PropertyType);
                                        pi.SetValue(model, value, null);
                                    }
                                    break;
                                case ModelType.Enum:
                                    {
                                        var fiType = row[0].GetType();
                                        if (fiType == typeof(int))
                                        {
                                            pi.SetValue(model, row[name], null);
                                        }
                                        else if (fiType == typeof(string))
                                        {
                                            var value = (T)Enum.Parse(typeof(T), row[name].ToString());
                                            if (value != null)
                                                pi.SetValue(model, value, null);
                                        }
                                    }
                                    break;
                                case ModelType.String:
                                    {
                                        var value = Convert.ChangeType(row[name], pi.PropertyType);
                                        pi.SetValue(model, value, null);
                                    }
                                    break;
                                case ModelType.Object:
                                    {
                                        pi.SetValue(model, row[name], null);
                                    }
                                    break;
                                case ModelType.Else:
                                    throw new Exception("不支持该类型转换");
                                default:
                                    throw new Exception("未知类型");
                            }
                        }
                    }
                    break;
                default:
                    model = default(T);
                    break;
            }
            return model;
        }
        public static String GetPYstring(string str)
        {
            string temStr = "";
            foreach (char c in str)
            {
                if ((int)c > 33 && (int)c <= 126)
                {
                    temStr += c.ToString();
                }
                else
                {
                    temStr += GetPYChar(c.ToString());
                }
            }
            return temStr;
        }

        public static String GetPYChar(string c)
        {
            try
            {
                if (c.Equals(" "))
                {
                    return c;
                }
                if (c.Equals("行"))
                {
                    return "x";
                }
                byte[] array = new byte[2];
                array = Encoding.Default.GetBytes(c);
                int i = (short)(array[0] - '\0') * 256 + ((short)(array[1] - '\0'));
                if (i < 0xB0A1) return "*";
                if (i < 0xB0C5) return "a";
                if (i < 0xB2C1) return "b";
                if (i < 0xB4EE) return "c";
                if (i < 0xB6EA) return "d";
                if (i < 0xB7A2) return "e";
                if (i < 0xB8C1) return "f";
                if (i < 0xB9FE) return "g";
                if (i < 0xBBF7) return "h";

                if (i < 0xBFA6) return "j";
                if (i < 0xC0AC) return "k";
                if (i < 0xC2E8) return "l";
                if (i < 0xC4C3) return "m";
                if (i < 0xC5B6) return "n";
                if (i < 0xC5BE) return "o";
                if (i < 0xC6DA) return "p";
                if (i < 0xC8BB) return "q";
                if (i < 0xC8F6) return "r";
                if (i < 0xCBFA) return "s";
                if (i < 0xCDDA) return "t";

                if (i < 0xCEF4) return "w";
                if (i < 0xD1B9) return "x";
                if (i < 0xD4D1) return "y";
                if (i < 0xD7FA) return "z";
                return c;
            }
            catch (Exception)
            {
                return c;
            }
        }
    }
}