using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wlniao.Cms
{
    public partial class SiteCopyRight : PageLogin
    {
        protected string msg = "";
        protected string copyright = "";
        protected string _script = "";
        protected void Page_Load(object sender, EventArgs e)
        {
           string _account = GetAccountGuid();
            Wlniao.Model.MiniSite minisite = Wlniao.MiniSite.Get(_account);
            if (minisite == null)
            {
                minisite = new Wlniao.Model.MiniSite();
            }
            copyright = Request["copyright"];
            if (string.IsNullOrEmpty(Request["method"]))
            {
                if (string.IsNullOrEmpty(copyright))
                {
                    copyright = minisite.CopyRight;
                }
            }
            if (Request["method"] == "save")
            {
                Result result = Wlniao.MiniSite.SetCopyright(_account, copyright);
                if (!result.IsValid)
                {
                    msg = result.Errors[0];
                }
                else
                {
                    _script = "<script>parent.showTips('微网站设置成功!',4);</script>";
                }
            }
        }
    }
}