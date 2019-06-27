using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCWeb.Models
{
    public class TreeMenuPowerModel
    {
        public string id { get; set; }

        //树插件需要的字段
        public string pId { get; set; }
        public string name { get; set; }
        public bool @checked { get; set; }

    }

    public class TreerMainModel
    {
        //角色名称
        public string roleName { get; set; }

        //树插件List集合
        public List<TreeMenuPowerModel> zTreeRow { get; set; }
    }
}