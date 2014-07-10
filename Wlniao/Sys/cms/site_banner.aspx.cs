using System;
using System.Collections.Generic;

using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wlniao.Cms
{
    public partial class SiteBanner : PageLogin
    {
        protected string msg = "";
        protected string _script = "";
        protected string _account = "";
        protected string _website = "";
        protected string banner1src = "";
        protected string banner2src = "";
        protected string banner3src = "";
        protected string banner4src = "";
        protected string banner5src = "";
        protected string _dataurl = Oss.DataUrl;
        protected void Page_Load(object sender, EventArgs e)
        {
            _account = GetAccountGuid();
            if (string.IsNullOrEmpty(Request["method"]))
            {
                #region 加载数据
                if (Request.Url.Port == 80)
                {
                    _website = Request.Url.Host;
                }
                else
                {
                    _website = Request.Url.Host + ":" + Request.Url.Port;
                }
                string t1 = "UsersData/" + _account + "/MiniSite/banner1";
                string t2 = "UsersData/" + _account + "/MiniSite/banner2";
                string t3 = "UsersData/" + _account + "/MiniSite/banner3";
                string t4 = "UsersData/" + _account + "/MiniSite/banner4";
                string t5 = "UsersData/" + _account + "/MiniSite/banner5";
                if (Oss.Exists(t1 + ".jpg"))
                {
                    banner1src = t1 + ".jpg";
                }
                else if (Oss.Exists(t1 + ".gif"))
                {
                    banner1src = t1 + ".gif";
                }
                else if (Oss.Exists(t1 + ".png"))
                {
                    banner1src = t1 + ".png";
                }

                if (Oss.Exists(t2 + ".jpg"))
                {
                    banner2src = t2 + ".jpg";
                }
                else if (Oss.Exists(t2 + ".gif"))
                {
                    banner2src = t2 + ".gif";
                }
                else if (Oss.Exists(t2 + ".png"))
                {
                    banner2src = t2 + ".png";
                }

                if (Oss.Exists(t3 + ".jpg"))
                {
                    banner3src = t3 + ".jpg";
                }
                else if (Oss.Exists(t3 + ".gif"))
                {
                    banner3src = t3 + ".gif";
                }
                else if (Oss.Exists(t3 + ".png"))
                {
                    banner3src = t3 + ".png";
                }

                if (Oss.Exists(t4 + ".jpg"))
                {
                    banner4src = t4 + ".jpg";
                }
                else if (Oss.Exists(t4 + ".gif"))
                {
                    banner4src = t4 + ".gif";
                }
                else if (Oss.Exists(t4 + ".png"))
                {
                    banner4src = t4 + ".png";
                }

                if (Oss.Exists(t5 + ".jpg"))
                {
                    banner5src = t5 + ".jpg";
                }
                else if (Oss.Exists(t5 + ".gif"))
                {
                    banner5src = t5 + ".gif";
                }
                else if (Oss.Exists(t5 + ".png"))
                {
                    banner5src = t5 + ".png";
                }

                #endregion
            }
            else
            {
                string t1 = "UsersData/" + _account + "/MiniSite/banner1";
                string t2 = "UsersData/" + _account + "/MiniSite/banner2";
                string t3 = "UsersData/" + _account + "/MiniSite/banner3";
                string t4 = "UsersData/" + _account + "/MiniSite/banner4";
                string t5 = "UsersData/" + _account + "/MiniSite/banner5";
                switch (Request["method"])
                {
                    case "del1":
                        Oss.Delete(t1 + ".jpg");
                        Oss.Delete(t1 + ".png");
                        Oss.Delete(t1 + ".gif");
                        break;
                    case "del2":
                        Oss.Delete(t2 + ".jpg");
                        Oss.Delete(t2 + ".png");
                        Oss.Delete(t2 + ".gif");
                        break;
                    case "del3":
                        Oss.Delete(t3 + ".jpg");
                        Oss.Delete(t3 + ".png");
                        Oss.Delete(t3 + ".gif");
                        break;
                    case "del4":
                        Oss.Delete(t4 + ".jpg");
                        Oss.Delete(t4 + ".png");
                        Oss.Delete(t4 + ".gif");
                        break;
                    case "del5":
                        Oss.Delete(t5 + ".jpg");
                        Oss.Delete(t5 + ".png");
                        Oss.Delete(t5 + ".gif");
                        break;
                }
                Response.Redirect("site_banner.aspx");
            }
        }
    }
}