﻿using System;
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
        public void FOB(string spdm )
        {
            ViewData.Model = db.Queryable<FOBJS_FK>().Where(u => u.SPDM == spdm).Select(u => new FOBJS_FK
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
            ///重写ExecueResult方法  
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
								        SELECT S1.SPDM,SUM(S1.SL_2)AS CPSL FROM SCZZD S LEFT JOIN SCZZDMX S1 ON S.DJBH=S1.DJBH
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

        public static string sql3 = @" Select a.SPDM,SUM(a.SL)as sl from(
		                               SELECT DMX.SPDM,DMX.SL FROM DBJRD (nolock) D LEFT JOIN DBJRDMX (nolock) DMX ON D.DJBH=DMX.DJBH
		                               UNION ALL
		                               SELECT PMX.SPDM,PMX.SL AS SL FROM PHJRD (nolock) P  LEFT JOIN PHJRDMX (nolock) PMX ON P.DJBH = PMX.DJBH
                                       WHERE left(p.DJBH,3) = 'PA1'
		                               )a group by a.SPDM";

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
    }
}