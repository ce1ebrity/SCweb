using Newtonsoft.Json;
using SCWeb.Helper;
using SCWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCWeb.Controllers
{
   
    public class op
    {
        public string total { get; set; }
        public op2[] rows { get; set; }
    }
    public class op2
    {
        public string itemid { get; set; }
        public string productid { get; set; }
        public string listprice { get; set; }
        public string unitcost { get; set; }
        public string status { get; set; }
        public string attr1 { get; set; }
        public string operate { get; set; }
        public string button { get; set; }
    }
    [Login(IsCheck = false)]
    public class HomeJsonController : Controller
    {
        // GET: HomeJson
        DataTable dt;

        [HttpPost]
        public ActionResult GetView(string page, string rows)
        {
            op o = new op()
            {
                total = "24",
                rows = new op2[] {
                    new op2() {
                         itemid= "EST-9",
                         productid= "FI-SW-9",
                         listprice= "16.50",
                         unitcost= "100.00",
                         status= "s",
                         attr1= "Large",
                         operate= "1",
                         button= "1"
                    },
                     new op2() {
                          itemid= "EST-10",
                          productid= "K9-DL-10",
                          listprice= "18.50",
                          unitcost= "17.00",
                          status= "P",
                          attr1= "Spotted Adult Female",
                          operate= "10",
                          button= "2"
                    },
                      new op2() {
                          itemid= "EST-11",
                          productid= "K9-DL-11",
                          listprice= "18.50",
                          unitcost= "65.00",
                          status= "P",
                          attr1= "Spotted Adult Female",
                          operate= "10",
                          button= "3"
                    },
                       new op2() {
                          itemid= "EST-12",
                          productid= "K9-DL-12",
                          listprice= "18.50",
                          unitcost= "50.00",
                          status= "P",
                          attr1= "Spotted Adult Female",
                          operate= "10",
                          button= "4"
                    },
                        new op2() {
                          itemid= "EST-13",
                          productid= "K9-DL-13",
                          listprice= "18.50",
                          unitcost= "45.00",
                          status= "P",
                          attr1= "Spotted Adult Female",
                          operate= "10",
                          button= "5"
                    },
                         new op2() {
                          itemid= "EST-14",
                          productid= "K9-DL-14",
                          listprice= "18.50",
                          unitcost= "17.00",
                          status= "P",
                          attr1= "Spotted Adult Female",
                          operate= "10",
                          button= "6"
                    },
                          new op2() {
                          itemid= "EST-15",
                          productid= "K9-DL-15",
                          listprice= "18.50",
                          unitcost= "17.00",
                          status= "P",
                          attr1= "Spotted Adult Female",
                          operate= "10",
                          button= "1"
                    },
                           new op2() {
                          itemid= "EST-16",
                          productid= "K9-DL-16",
                          listprice= "18.50",
                          unitcost= "17.00",
                          status= "P",
                          attr1= "Spotted Adult Female",
                          operate= "10",
                          button= "1"
                    },
                            new op2() {
                          itemid= "EST-17",
                          productid= "K9-DL-17",
                          listprice= "18.50",
                          unitcost= "17.00",
                          status= "P",
                          attr1= "Spotted Adult Female",
                          operate= "10",
                          button= "1"
                    },
                             new op2() {
                          itemid= "EST-18",
                          productid= "K9-DL-18",
                          listprice= "18.50",
                          unitcost= "17.00",
                          status= "P",
                          attr1= "Spotted Adult Female",
                          operate= "10",
                          button= "1"
                    },
                              new op2() {
                          itemid= "EST-19",
                          productid= "K9-DL-19",
                          listprice= "18.50",
                          unitcost= "17.00",
                          status= "P",
                          attr1= "Spotted Adult Female",
                          operate= "10",
                          button= "1"
                    },
                               new op2() {
                          itemid= "EST-20",
                          productid= "K9-DL-20",
                          listprice= "18.50",
                          unitcost= "17.00",
                          status= "P",
                          attr1= "Rattleless",
                          operate= "10",
                          button= "1"
                    },new op2() {
                         itemid= "EST-9",
                         productid= "FI-SW-9",
                         listprice= "16.50",
                         unitcost= "5.00",
                         status= "s",
                         attr1= "Large",
                         operate= "1",
                         button= "1"
                    },
                     new op2() {
                          itemid= "EST-10",
                          productid= "K9-DL-10",
                          listprice= "18.50",
                          unitcost= "17.00",
                          status= "P",
                          attr1= "Spotted Adult Female",
                          operate= "10",
                          button= "1"
                    },
                      new op2() {
                          itemid= "EST-11",
                          productid= "K9-DL-11",
                          listprice= "18.50",
                          unitcost= "17.00",
                          status= "P",
                          attr1= "Spotted Adult Female",
                          operate= "10",
                          button= "1"
                    }
                }
            };

            if (page == "2")
            {
                o = new op()
                {
                    total = "24",
                    rows = new op2[] {
                    new op2() {
                         itemid= "EST-9",
                         productid= "FI-SW-9",
                         listprice= "16.50",
                         unitcost= "5.00",
                         status= "s",
                         attr1= "Large",
                         operate= "1",
                         button= "1"
                    },
                     new op2() {
                          itemid= "EST-10",
                          productid= "K9-DL-10",
                          listprice= "18.50",
                          unitcost= "17.00",
                          status= "P",
                          attr1= "Spotted Adult Female",
                          operate= "10",
                          button= "1"
                    },
                      new op2() {
                          itemid= "EST-11",
                          productid= "K9-DL-11",
                          listprice= "18.50",
                          unitcost= "17.00",
                          status= "P",
                          attr1= "Spotted Adult Female",
                          operate= "10",
                          button= "1"
                    },
                       new op2() {
                          itemid= "EST-12",
                          productid= "K9-DL-12",
                          listprice= "18.50",
                          unitcost= "17.00",
                          status= "P",
                          attr1= "Spotted Adult Female",
                          operate= "10",
                          button= "1"
                    },
                        new op2() {
                          itemid= "EST-13",
                          productid= "K9-DL-13",
                          listprice= "18.50",
                          unitcost= "17.00",
                          status= "P",
                          attr1= "Spotted Adult Female",
                          operate= "10",
                          button= "1"
                    },
                         new op2() {
                          itemid= "EST-14",
                          productid= "K9-DL-14",
                          listprice= "18.50",
                          unitcost= "17.00",
                          status= "P",
                          attr1= "Spotted Adult Female",
                          operate= "10",
                          button= "1"
                    },
                          new op2() {
                          itemid= "EST-15",
                          productid= "K9-DL-15",
                          listprice= "18.50",
                          unitcost= "17.00",
                          status= "P",
                          attr1= "Spotted Adult Female",
                          operate= "10",
                          button= "1"
                    },
                           new op2() {
                          itemid= "EST-16",
                          productid= "K9-DL-16",
                          listprice= "18.50",
                          unitcost= "17.00",
                          status= "P",
                          attr1= "Spotted Adult Female",
                          operate= "10",
                          button= "1"
                    },
                            new op2() {
                          itemid= "EST-17",
                          productid= "K9-DL-17",
                          listprice= "18.50",
                          unitcost= "17.00",
                          status= "P",
                          attr1= "Spotted Adult Female",
                          operate= "10",
                          button= "1"
                    }

                    }

                };
            }
            return Content(JsonConvert.SerializeObject(o));
        }

        [HttpPost]
        public ActionResult GetViews(string page, string rows)
        {
            op o = new op()
            {
                total = "1",
                rows = new op2[] {
                    new op2() {
                         itemid= "EST-9",
                         productid= "FI-SW-9",
                         listprice= "16.50",
                         unitcost= "100.00",
                         status= "s",
                         attr1= "Large",
                         operate= "1",
                         button= "1"
                    }
                }
            };
            return Content(JsonConvert.SerializeObject(o));
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="loginName">用户名</param>
        /// <param name="passWord">密码</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetLogin(string loginName, string passWord, string type)
        {
            ResultClass rc = new ResultClass();

            string sql = "select id from BPM_UserBase where loginName=@loginName and passWord=@passWord and type=@type";
            string id = Convert.ToString(SqlHelper.SelectSinger(sql, new SqlParameter("@loginName", loginName), new SqlParameter("@passWord", DES.GetMD5(passWord)), new SqlParameter("@type", type)));
            if (!string.IsNullOrWhiteSpace(id))
            {
                Common.SetCookie("userLogin", id, 7);
                rc.Result = "success";
                rc.Message = "登陆成功,正在跳转主页面!";
            }
            else
            {
                rc.Result = "error";
                rc.Message = "登陆失败,请检查用户名和密码!";
            }
            return Content(JsonConvert.SerializeObject(rc));
        }

        /// <summary>
        /// 用户列表页数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetUserList(string page, string rows)
        {
            //总行数
            string sql = "select Count(1) from BPM_UserBase where status<>0";
            string pageCount = SqlHelper.SelectSinger(sql).ToString();

            sql = " SELECT TOP " + rows + " *,(select roleName from BPM_RoleBase where id = a.roleId) as 'roleName' FROM ";
            sql += " (SELECT ROW_NUMBER() OVER(ORDER BY id) AS RowNumber, * FROM BPM_UserBase) A ";
            sql += " WHERE RowNumber > " + rows + " * (" + page + " - 1)  and status<>0";
            dt = SqlHelper.SelectTable(sql);
            List<BPM_UserBase> umList = new List<BPM_UserBase>();
            foreach (DataRow item in dt.Rows)
            {
                BPM_UserBase ums = new BPM_UserBase()
                {
                    addTime = Convert.ToDateTime(item["addTime"]).ToString("f"),
                    brand = item["brand"].ToString(),
                    id = item["id"].ToString(),
                    loginName = item["loginName"].ToString(),
                    passWord = item["passWord"].ToString(),
                    status = Common.GetStatusCN(item["status"].ToString()),
                    trueName = item["trueName"].ToString(),
                    type = item["type"].ToString() == "1" ? "管理用户" : "普通用户",
                    roleName = item["roleName"].ToString()
                };
                umList.Add(ums);
            }
            UserMainModels userMainModels = new UserMainModels()
            {
                total = pageCount,
                rows = umList
            };
            //JsonConvert.SerializeObject(o)
            return Content(JsonConvert.SerializeObject(userMainModels));
        }

        /// <summary>
        /// 角色列表页数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetRoleList(string page, string rows)
        {
            //总行数
            string sql = "select Count(1) from BPM_RoleBase where status<>2";
            string pageCount = SqlHelper.SelectSinger(sql).ToString();

            sql = " SELECT TOP " + rows + " * FROM ";
            sql += " (SELECT ROW_NUMBER() OVER(ORDER BY id) AS RowNumber, * FROM BPM_RoleBase) A ";
            sql += " WHERE RowNumber > " + rows + " * (" + page + " - 1)  and status<>2";
            dt = SqlHelper.SelectTable(sql);
            List<RoleModels> rmList = new List<RoleModels>();
            foreach (DataRow item in dt.Rows)
            {
                RoleModels ums = new RoleModels()
                {
                    addTime = Convert.ToDateTime(item["addTime"]).ToString("f"),
                    id = item["id"].ToString(),
                    status = Common.GetStatusCN(item["status"].ToString()),
                    roleName = item["roleName"].ToString()
                };
                rmList.Add(ums);
            }
            RoleMainModels roleMainModels = new RoleMainModels()
            {
                total = pageCount,
                rows = rmList
            };

            //JsonConvert.SerializeObject(o)
            return Content(JsonConvert.SerializeObject(roleMainModels));
        }

        /// <summary>
        /// 根据id获得角色名称
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetRoleName(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                string sql = "select roleName from BPM_RoleBase where id=@id";
                string roleName = Convert.ToString(SqlHelper.SelectSinger(sql, new SqlParameter("@id", id)));
                return Content(roleName);
            }
            else
            {
                return Content("");
            }
        }
        /// <summary>
        /// 添加或者修改角色和权限
        /// </summary>
        /// <param name="role">权限id</param>
        /// <param name="roleName"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpPost]
        [Property(MenuCode = "BPM_RoleBase", MenuOperation = "增加")]
        public ActionResult GetAddRole(string role, string roleName, string roleId)
        {
            string sql = "";
            bool ret = false;
            if (roleId == "0")
            {
                //角色增加操作
                sql = "insert into BPM_RoleBase(roleName) values(@roleName); select @topid=@@identity";
                SqlParameter[] para = new SqlParameter[] {
                        new SqlParameter("@roleName",SqlDbType.NVarChar,50),
                        new SqlParameter("@topid", SqlDbType.Int)};
                para[0].Value = roleName;
                para[1].Direction = ParameterDirection.Output;
                SqlHelper.InsertDelUpdate(sql, para);
                int rId = Convert.ToInt32(para[1].Value == null ? 0 : Convert.ToInt32(para[1].Value));
                if (rId > 0)
                {
                    ret = true;
                    if (!string.IsNullOrWhiteSpace(role))
                    {
                        string[] menuList = role.Split(',');
                        sql = "insert into BPM_PropertyManagr(roleId,menuPId) values(@roleId,@menuPId)";
                        for (int i = 0; i < menuList.Length; i++)
                        {
                            SqlParameter[] param = new SqlParameter[] {
                                    new SqlParameter("@roleId",rId),
                                    new SqlParameter("@menuPId",menuList[i])};
                            SqlHelper.InsertDelUpdate(sql, param);
                        }
                    }

                }
            }
            else
            {
                //角色修改操作
                sql = "update BPM_RoleBase set roleName=@roleName where id=@roleId";
                int k = SqlHelper.InsertDelUpdate(sql, new SqlParameter("@roleId", roleId), new SqlParameter("@roleName", roleName));
                if (k > 0)
                {
                    ret = true;
                    sql = "update BPM_PropertyManagr set status=0 where roleId=@roleId";
                    SqlHelper.InsertDelUpdate(sql, new SqlParameter("@roleId", roleId));
                    string[] menuList = role.Split(',');
                    //判断数据库中是否有数据
                    sql = "select count(1) from BPM_PropertyManagr where roleId=@roleId and menuPId=@menuPId";
                    for (int i = 0; i < menuList.Length; i++)
                    {
                        SqlParameter[] param = new SqlParameter[] {
                                    new SqlParameter("@roleId",roleId),
                                    new SqlParameter("@menuPId",menuList[i])};
                        int count = SqlHelper.InsertDelUpdate(sql, param);
                        if (count > 0)
                        {
                            sql = "update BPM_PropertyManagr set status=1 where roleId=@roleId and menuPId=@menuPId";
                        }
                        else
                        {
                            sql = "insert into BPM_PropertyManagr(roleId,menuPId) values(@roleId,@menuPId)";
                        }
                        SqlHelper.InsertDelUpdate(sql, param);
                    }
                }
            }
            if (ret)
            {
                return Content("success");
            }
            else
            {
                return Content("error");
            }
        }

        /// <summary>
        /// 角色权限获取
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetMenuPowerTreeList(string roleId)
        {
            //string userId = Common.GetCookie("userLogin");
            //string sql = "select roleid from BPM_UserBase where id=@id";
            //int roleId = Convert.ToInt32(SqlHelper.SelectSinger(sql, new SqlParameter("@id", userId)));


            List<MenuPowerModel> mpmList = new List<MenuPowerModel>();
            List<MenuPowerModel> mpmList2;
            string sql = "select * from BPM_MenuPowerBase where menuSon=0 ";
            dt = SqlHelper.SelectTable(sql);
            foreach (DataRow item in dt.Rows)
            {
                MenuPowerModel mp = new MenuPowerModel();
                mp.id = item["id"].ToString();
                mp.text = item["menuName"].ToString();
                mp.date = Convert.ToDateTime(item["addTime"]).ToString("f");
                sql = "select * from BPM_MenuPowerBase where menuSon=@menuSon ";
                DataTable dt1 = SqlHelper.SelectTable(sql, new SqlParameter("@menuSon", item["id"]));
                mpmList2 = new List<MenuPowerModel>();
                foreach (DataRow dr in dt1.Rows)
                {
                    MenuPowerModel mp2 = new MenuPowerModel();
                    mp2.id = dr["id"].ToString();
                    mp2.text = dr["menuName"].ToString();
                    mp2.date = Convert.ToDateTime(dr["addTime"]).ToString("f");


                    mp2.@checked = GetIsMenu(Convert.ToInt32(roleId), Convert.ToInt32(dr["id"]));
                    mpmList2.Add(mp2);
                }
                mp.children = mpmList2;
                mpmList.Add(mp);
            }
            return Content(JsonConvert.SerializeObject(mpmList));
        }


        public bool GetIsMenu(int roleId, int menuId)
        {
            bool ret = false;
            if (roleId != 0)
            {
                string sql = "select count(1) from BPM_PropertyManagr where roleId=@roleId and menuPId=@menuPId and status=1 ";
                int i = Convert.ToInt32(SqlHelper.SelectSinger(sql, new SqlParameter("@menuPId", menuId), new SqlParameter("@roleId", roleId)));
                if (i > 0)
                {
                    ret = true;
                }
            }
            return ret;
        }


    }
}