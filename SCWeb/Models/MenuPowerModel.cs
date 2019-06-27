using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCWeb.Models
{
    public class MenuPowerModel
    {
        public string id { get; set; }
        public string text { get; set; }
        public string date { get; set; }
        public string checkState { get; set; }
        public bool @checked { get; set; }
        
        public List<MenuPowerModel> children { get; set; }

        public string menuName { get; set; }
        public string menuSon { get; set; }

        //树插件需要的字段
        public string pId { get; set; }
        public string name { get; set; }
    }
}