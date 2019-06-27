using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Newtonsoft.Json.Converters;
using SCWeb.Helper;
using SCWeb.Models;
using Newtonsoft.Json;
using System.IO;
using NPOI.HSSF.UserModel;
using SqlSugar;
using System.Configuration;

namespace SCWeb.Controllers
{
    [Login(IsCheck = true)]
    public class InstallController : BaseController
    {
        #region 权限管理

        /// <summary>
        /// 权限管理
        /// </summary>
        /// <returns></returns>
        public ActionResult AuthorityManager()
        {

            return View();
        }
        /// <summary>
        /// 查询权限
        /// </summary>
        /// <returns></returns>
        public ActionResult ZTreeAuthorityInfo()
        {
            try
            {
                string sql = "select * from BPM_MenuPowerBase where status=1";
                DataTable dt = SqlHelper.SelectTable(sql);
                List<TreeMenuPowerModel> MPlist = new List<TreeMenuPowerModel>();
                foreach (DataRow item in dt.Rows)
                {
                    var menuSon = item["menuSon"].ToString();
                    TreeMenuPowerModel MP = new TreeMenuPowerModel();
                    if (menuSon == "0")
                    {
                        MP.id = item["id"].ToString();
                        MP.pId = "0";
                    }
                    else
                    {
                        MP.id = item["id"].ToString(); //menuSon + index.ToString();
                        MP.pId = menuSon;
                    }
                    MP.name = item["menuName"].ToString();

                    MPlist.Add(MP);
                }
                return Content(JsonConvert.SerializeObject(MPlist));
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// 添加权限树
        /// </summary>
        /// <returns></returns>
        public ActionResult addTree(string menuSon, string menuName)
        {
            string sql = "select MenuCode from BPM_MenuPowerBase where id=" + menuSon + "";
            var MenuC = SqlHelper.SelectSinger(sql);
            sql = "insert into BPM_MenuPowerBase(menuName,menuSon,MenuCode) values(@menuName,@menuSon,@MenuCode);select @Tid= @@IDENTITY";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@menuName", menuName),
                new SqlParameter("@menuSon", menuSon),
                new SqlParameter("@Tid", SqlDbType.Int),
                new SqlParameter("@MenuCode", MenuC),
            };
            //指示是返回值
            param[2].Direction = ParameterDirection.Output;
            int count = SqlHelper.InsertDelUpdate(sql, param);
            var Tid = param[2].Value.ToString();
            if (count > 0)
            {
                return Content(Tid);
            }
            else
            {
                return Content("error");
            }
        }
        /// <summary>
        /// 权限修改
        /// </summary>
        /// <param name="menuid">权限ID</param>
        /// <param name="menuName">权限名称</param>
        /// <returns></returns>
        public ActionResult UpdateTree(string menuid, string menuName)
        {
            string sql = "update BPM_MenuPowerBase set menuName=@menuName where id=@id";
            int count = SqlHelper.InsertDelUpdate(sql, new SqlParameter("@menuName", menuName), new SqlParameter("@id", menuid));
            if (count > 0)
            {
                return Content("success");
            }
            else
            {
                return Content("error");
            }
        }
        /// <summary>
        /// 权限删除
        /// </summary>
        /// <returns></returns>
        public ActionResult DelTree(string menuid)
        {
            string sql = "update BPM_MenuPowerBase set status=0 where id=" + menuid + "";
            int count = SqlHelper.InsertDelUpdate(sql);
            if (count > 0)
            {
                return Content("success");
            }
            else
            {
                return Content("error");
            }
        }

        #endregion

        #region 角色管理
        /// <summary>
        /// 角色管理
        /// </summary>
        /// <returns></returns>
        public ActionResult RoleMananger()
        {
            return View();
        }

        /// <summary>
        /// 角色查询
        /// </summary>
        /// <returns></returns>
        public ActionResult RoleSelPaging(string pageI, string pageS)
        {
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy'-'MM'-'dd" };
            try
            {
                var pageIndex = string.IsNullOrWhiteSpace(pageI) == true ? 1 : Convert.ToInt32(pageI); //当前页
                var pageSize = string.IsNullOrWhiteSpace(pageS) == true ? 16 : Convert.ToInt32(pageS); //一页显示多少行
                string sql = "select count(*) from BPM_RoleBase where status=1";
                var rowCount = Convert.ToInt32(SqlHelper.SelectSinger(sql));//总行数
                double pageCount = rowCount / pageSize; //总页数
                double aaa = rowCount % pageSize; //去余数
                if (aaa > 0) //如果总页数有余数，则总行数+1
                {
                    pageCount = pageCount + 1;//总页数
                }
                sql = "select * from (select *,ROW_NUMBER() over(order by id desc) as rownumber from BPM_RoleBase  where status=1) as a where rownumber between (@pageIndex-1)*@pageSize+1 and @pageIndex*@pageSize";
                DataTable dt = SqlHelper.SelectTable(sql, new SqlParameter("@pageIndex", pageIndex), new SqlParameter("@pageSize", pageSize));
                List<RoleModels> Rlist = new List<RoleModels>();
                foreach (DataRow item in dt.Rows)
                {
                    RoleModels r = new RoleModels();
                    r.id = item["id"].ToString();
                    r.roleName = item["roleName"].ToString();
                    r.addTime = item["addTime"].ToString();
                    r.rowCount = rowCount.ToString();
                    r.pageCount = pageCount.ToString();
                    Rlist.Add(r);
                }
                return Content(JsonConvert.SerializeObject(Rlist, Formatting.Indented, timeConverter));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 角色编辑前绑定权限ztree
        /// </summary>
        /// <returns></returns>
        public ActionResult RoleZTreeBind(string flag)
        {
            try
            {
                List<TreeMenuPowerModel> MPlist = new List<TreeMenuPowerModel>();
                string sql = "select * from BPM_MenuPowerBase where status=1";
                DataTable dt = SqlHelper.SelectTable(sql);
                //循环生成zTree
                foreach (DataRow item in dt.Rows)
                {
                    #region 循环
                    var menuSon = item["menuSon"].ToString();
                    var menuPId = item["id"].ToString();

                    bool Tckd = false;
                    sql = "select * from BPM_PropertyManagr where menuPId=@menuPId and roleId=@roleId and status=1";
                    DataTable Tdt = SqlHelper.SelectTable(sql, new SqlParameter("@menuPId", menuPId), new SqlParameter("@roleId", flag));
                    //判断Tree是否选中
                    if (Tdt.Rows.Count > 0)
                    {
                        Tckd = true;
                    }

                    TreeMenuPowerModel MP = new TreeMenuPowerModel();
                    if (menuSon == "0")
                    {
                        MP.id = item["id"].ToString();
                        MP.pId = "0";
                        MP.@checked = Tckd;
                    }
                    else
                    {
                        MP.id = item["id"].ToString(); //menuSon + index.ToString();
                        MP.pId = menuSon;
                        MP.@checked = Tckd;
                    }
                    MP.name = item["menuName"].ToString();

                    MPlist.Add(MP);
                    #endregion

                }
                sql = "select roleName from BPM_RoleBase where id=" + flag + "";
                TreerMainModel tmodel = new TreerMainModel();
                tmodel.roleName = flag == "0" ? "" : SqlHelper.SelectSinger(sql).ToString();
                tmodel.zTreeRow = MPlist;
                return Content(JsonConvert.SerializeObject(tmodel));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 角色添加和修改的方法
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="id"></param>
        /// <param name="Rmodels"></param>
        /// <returns></returns>
        [Property(MenuCode = "BPM_RoleBase", MenuOperation = "增加")]
        public ActionResult RoleAddUpdate(string flag, string id, RoleModels Rmodels)
        {
            string[] ArrC = { };
            //获取选中节点的数组
            if (!string.IsNullOrWhiteSpace(flag))
            {
                ArrC = flag.Substring(0, flag.Length - 1).Split(',');
            }
            int count = 0;
            if (int.Parse(id) > 0)
            {
                //角色编辑
                string sql = "update BPM_RoleBase set roleName=@roleName where id=@id";
                count = SqlHelper.InsertDelUpdate(sql, new SqlParameter("@roleName", Rmodels.roleName), new SqlParameter("@id", id));
                if (count > 0)
                {
                    //将所有权限取消选中
                    sql = "update BPM_PropertyManagr set status=0 where roleId=" + id + "";
                    SqlHelper.InsertDelUpdate(sql);
                    for (int i = 0; i < ArrC.Length; i++)
                    {
                        sql = "select count(*) from BPM_PropertyManagr where roleId=" + id + " and menuPId=" + ArrC[i] + "";
                        count = (int)SqlHelper.SelectSinger(sql);
                        if (count == 0)
                        {
                            //原来没有的权限则添加
                            sql = "insert into BPM_PropertyManagr(roleId,menuPId) values(" + id + ",@menuPId)";
                            count = SqlHelper.InsertDelUpdate(sql, new SqlParameter("@menuPId", ArrC[i]));
                        }
                        else
                        {
                            //原来有的权限则修改
                            sql = "update BPM_PropertyManagr set status=1 where menuPId=@menuPId and roleId=" + id + "";
                            count = SqlHelper.InsertDelUpdate(sql, new SqlParameter("@menuPId", ArrC[i]));
                        }
                    }

                }
            }
            else
            {
                //角色添加
                string sql = "insert into BPM_RoleBase(roleName) values(@roleName);select @topid=@@identity";
                SqlParameter[] para = new SqlParameter[] {
                new SqlParameter("@roleName",Rmodels.roleName),
                new SqlParameter("@topid",SqlDbType.Int),
                };
                para[1].Direction = ParameterDirection.Output;
                count = SqlHelper.InsertDelUpdate(sql, para);
                if (count > 0 && ArrC.Length>0)
                {
                    //角色权限关联操作,添加BPM_PropertyManagr表数据
                    sql = "insert into BPM_PropertyManagr(roleId,menuPId) values(@roleId,@menuPId)";
                    for (int i = 0; i < ArrC.Length; i++)
                    {
                        count = SqlHelper.InsertDelUpdate(sql, new SqlParameter("@roleId", para[1].Value), new SqlParameter("@menuPId", ArrC[i]));
                    }
                }
            }

            if (count > 0)
            {
                return Content("success");
            }
            else
            {
                return Content("error");
            }
        }

        /// <summary>
        /// 角色删除
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        [Property(MenuCode = "BPM_RoleBase", MenuOperation = "删除")]
        public ActionResult DelRole(string flag)
        {
            var str = flag.Substring(0, flag.Length - 1);//.Split(',')
            string sql = "update BPM_RoleBase set status=0 where id in (" + str + ")";
            int count = SqlHelper.InsertDelUpdate(sql);
            if (count > 0)
            {
                return Content("success");
            }
            else
            {
                return Content("error");
            }
        }

        #endregion


        #region 用户管理
        public ActionResult SellInfoUser()
        {
            string name = Request["Name"];
            var list = db.Ado.SqlQuery<BPM_UserBase>("select * from BPM_UserBase where loginName like @loginName", new SugarParameter("@loginName", "%" + name + "%")).ToList();
            if (list.Count > 0)
            {

                return Content("1");
            }
            else
            {
                return Content("2");
            }
        }


        /// <summary>
        /// 用户管理
        /// </summary>
        /// <returns></returns>
        public ActionResult UserManager()
        {
            //var pageIndex = 1;
            //var pageSize = 16;
            //string sql = "select *,(select roleName from BPM_RoleBase s2 where s1.roleid=s2.id) as roleName from BPM_UserBase s1 where status=1";
            //string sql = "select * from (select *,(select roleName from BPM_RoleBase s2 where s1.roleId=s2.id) as roleName,ROW_NUMBER() over(order by id desc) as rownumber from BPM_UserBase s1) as aaa where rownumber between (@pageIndex-1)*@pageSize+1 and @pageIndex*@pageSize and status=1";
            //DataTable dt = SqlHelper.SelectTable(sql, new SqlParameter("@pageIndex", pageIndex), new SqlParameter("@pageSize", pageSize));
            return View();
        }

        /// <summary>
        /// 用户查询和分页
        /// </summary>
        /// <returns></returns>
        public ActionResult UserSelPaging(string pageI, string pageS)
        {
            string name = Request["Name"];
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy'-'MM'-'dd" };
            try
            {
                var pageIndex = string.IsNullOrWhiteSpace(pageI) == true ? 1 : Convert.ToInt32(pageI); //当前页
                var pageSize = string.IsNullOrWhiteSpace(pageS) == true ? 16 : Convert.ToInt32(pageS); //一页显示多少行
                string sql = "select count(*) from BPM_UserBase where status=1";
                var rowCount = Convert.ToInt32(SqlHelper.SelectSinger(sql));//总行数
                double pageCount = rowCount / pageSize; //总页数
                double aaa = rowCount % pageSize; //去余数
                if (aaa > 0) //如果总页数有余数，则总行数+1
                {
                    pageCount = pageCount + 1;//总页数
                }

                sql = "select * from (select *,(select roleName from BPM_RoleBase s2 where s1.roleId=s2.id) as roleName,ROW_NUMBER() over(order by id desc) as rownumber from BPM_UserBase s1 where status=1 and loginName like @loginName) as aaa where rownumber between (@pageIndex-1)*@pageSize+1 and @pageIndex*@pageSize";
                DataTable dt = SqlHelper.SelectTable(sql, new SqlParameter("@pageIndex", pageIndex), new SqlParameter("@pageSize", pageSize),new SqlParameter("@loginName", "%" + name + "%"));
                List<BPM_UserBase> Ulist = new List<BPM_UserBase>();
                foreach (DataRow item in dt.Rows)
                {
                    BPM_UserBase u = new BPM_UserBase();
                    u.id = item["id"].ToString();
                    u.loginName = item["loginName"].ToString();
                    u.trueName = item["trueName"].ToString();
                    u.passWord = item["passWord"].ToString();
                    u.brand = item["brand"].ToString();
                    u.type = item["type"].ToString();
                    u.roleName = item["roleName"].ToString();
                    u.addTime = item["addTime"].ToString();
                    u.rowCount = rowCount.ToString();
                    u.pageCount = pageCount.ToString();
                    Ulist.Add(u);
                }
                return Content(JsonConvert.SerializeObject(Ulist, Formatting.Indented, timeConverter));
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        /// <summary>
        /// 绑定账户身份下拉框
        /// </summary>
        /// <returns></returns>
        public ActionResult RoleNameBind()
        {
            try
            {
                string sql = "select * from BPM_RoleBase where status=1";
                DataTable dt = SqlHelper.SelectTable(sql);
                return Content(JsonConvert.SerializeObject(dt));
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// 用户编辑前绑定数据
        /// </summary>
        /// <returns></returns>
        public ActionResult AUUserBind(string flag)
        {
            try
            {
                string sql = "";
                if (flag != "0")
                {
                    sql = "select * from BPM_UserBase where id=@id";
                    DataTable dt = SqlHelper.SelectTable(sql, new SqlParameter("@id", flag));
                    return Content(JsonConvert.SerializeObject(dt));
                }
                else
                {
                    return Content("null");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// 用户添加和编辑界面
        /// </summary>
        /// <returns></returns>
        //[Property(MenuCode = "BPM_UserBase", MenuOperation = "增加")]
        public ActionResult AddUpdateUser(string flag, BPM_UserBase models)
        {
            //获取登录人ID
            string userID = Common.GetCookie("userLogin");

            string sql = "";//用于存放SQL语句
            int count = 0;
            if (flag == "0")//执行添加操作
            {
                //判断是否是管理员登录
                if (userID == "1")
                {
                    sql = "insert into BPM_UserBase(loginName,trueName,passWord,brand,roleid) values(@loginName,@trueName,@passWord,@brand,@roleid)";
                    SqlParameter[] paramA = new SqlParameter[] {
                    new SqlParameter("@loginName",string.IsNullOrWhiteSpace(models.loginName)? "" : models.loginName),
                    new SqlParameter("@trueName",string.IsNullOrWhiteSpace(models.trueName)? "" : models.trueName),
                    new SqlParameter("@passWord",string.IsNullOrWhiteSpace(models.passWord)? "" : DES.GetMD5(models.passWord)),
                    new SqlParameter("@brand",string.IsNullOrWhiteSpace(models.brand)? "" : models.brand),
                    new SqlParameter("@roleid",string.IsNullOrWhiteSpace(models.roleName)? "" : models.roleName),
                    };
                    count = SqlHelper.InsertDelUpdate(sql, paramA);
                }
                else
                {
                    return Content("Operation");
                }

            }
            else//执行编辑操作
            {
                //判断除管理员之外 ，自己只能改自己的权限
                if (userID == flag || userID == "1")
                {
                    sql = "select passWord from BPM_UserBase where id=" + flag + "";
                    var pwd = SqlHelper.SelectSinger(sql);
                    sql = "update BPM_UserBase set loginName=@loginName,trueName=@trueName,passWord=@passWord,brand=@brand,roleid=@roleid where id=@id";
                    SqlParameter[] paramU = new SqlParameter[]
                    {
                    new SqlParameter("@loginName",string.IsNullOrWhiteSpace(models.loginName)? "" : models.loginName),
                    new SqlParameter("@trueName",string.IsNullOrWhiteSpace(models.trueName)? "" : models.trueName),
                    new SqlParameter("@passWord",string.IsNullOrWhiteSpace(models.passWord)? pwd : DES.GetMD5(models.passWord)),
                    new SqlParameter("@brand",string.IsNullOrWhiteSpace(models.brand)? "" : models.brand),
                    new SqlParameter("@roleid",string.IsNullOrWhiteSpace(models.roleName)? "" : models.roleName),
                    new SqlParameter("@id",flag),
                    };
                    count = SqlHelper.InsertDelUpdate(sql, paramU);
                }
                else
                {
                    return Content("Operation");
                }
            }
            if (count > 0)
            {
                return Content("success");
            }
            else
            {
                return Content("error");
            }
        }
        /// <summary>
        /// 用户删除
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        [Property(MenuCode = "BPM_UserBase", MenuOperation = "删除")]
        public ActionResult DelUser(string flag)
        {
            var str = flag.Substring(0, flag.Length - 1);//.Split(',')
            string sql = "update BPM_UserBase set status=0 where id in (" + str + ")";
            int count = SqlHelper.InsertDelUpdate(sql);
            if (count > 0)
            {
                return Content("success");
            }
            else
            {
                return Content("error");
            }
        }

        #endregion

    }
}