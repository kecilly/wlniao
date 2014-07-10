using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wlniao.Sys
{
    public partial class MenuSet : PageLogin
    {
        public Shijia.Service.IMPMenuService MPMenuService { set; get; }
        protected string treedata = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            treedata = MPMenuService.GetTreeData(_CurrentAccount.Id);
        }
    }

}