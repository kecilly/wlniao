using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wlniao.Cms
{
    public partial class SiteColor : PageLogin
    {
        protected string msg = "";
        protected string color = "";
        protected string _script = "";
        protected void Page_Load(object sender, EventArgs e)
        {
           string _account = GetAccountGuid();
            Wlniao.Model.MiniSite minisite = Wlniao.MiniSite.Get(_account);
            if (minisite == null)
            {
                minisite = new Wlniao.Model.MiniSite();
            }
            color = Request["color"];
            if (string.IsNullOrEmpty(Request["method"]))
            {
                if (string.IsNullOrEmpty(color))
                {
                    color = minisite.Color;
                }
            }
            if (string.IsNullOrEmpty(color))
            {
                color = "#E48803;#111111";
            }
            if (Request["method"] == "save")
            {
                Result result = Wlniao.MiniSite.SetColor(_account, color);
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