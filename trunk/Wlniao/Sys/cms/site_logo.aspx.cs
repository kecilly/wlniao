using System;
using System.Collections.Generic;

using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wlniao.Cms
{
    public partial class SiteLogo : PageLogin
    {
        protected string msg = "";
        protected string _script = "";
        protected string _account = "";
        protected string _website = "";
        protected string logosrc = "";
        protected string _dataurl = Oss.DataUrl;
        protected void Page_Load(object sender, EventArgs e)
        {
            _account = GetAccountGuid();
            if (Request.Url.Port == 80)
            {
                _website = Request.Url.Host;
            }
            else
            {
                _website = Request.Url.Host + ":" + Request.Url.Port;
            }
            string t = "UsersData/" + _account + "/MiniSite/Logo";
            if (Oss.Exists(t + ".jpg"))
            {
                logosrc = t + ".jpg";
            }
            else if (Oss.Exists(t + ".gif"))
            {
                logosrc = t + ".gif";
            }
            else if (Oss.Exists(t + ".png"))
            {
                logosrc = t + ".png";
            }
        }
    }
}