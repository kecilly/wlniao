using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wlniao.Cms
{
    public partial class SiteJs : PageLogin
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AshxHelper helper = new AshxHelper(Context);
            string curr = helper.GetParam("curr");
            if (string.IsNullOrEmpty(curr))
            {
                curr = "base";
            }
            Wlniao.Model.MiniSite minisite = Wlniao.MiniSite.Get(GetAccountGuid());
            Wlniao.Model.MiniSiteStyle style = Wlniao.MiniSite.GetStyle(GetAccountGuid());
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (style != null)
            {
                if (curr == "style")
                {
                    sb.AppendFormat("<button class=\"btn btn-info\" onclick=\"Goto('style.aspx');\"><i class=\"icon-hand-right\"></i> 风格选择</button>");
                }
                else
                {
                    sb.AppendFormat("<button class=\"btn\" onclick=\"Goto('style.aspx');\"> 风格选择</button>");
                }
                if (curr == "base")
                {
                    sb.AppendFormat("<button class=\"btn btn-info\" onclick=\"Goto('site.aspx');\"><i class=\"icon-hand-right\"></i> 基础设置(封面)</button>");
                }
                else
                {
                    sb.AppendFormat("<button class=\"btn\" onclick=\"Goto('site.aspx');\"> 基础设置(封面)</button>");
                }
            }
            if (style.Logo)
            {
                if (curr == "logo")
                {
                    sb.AppendFormat("<button class=\"btn btn-info\" onclick=\"Goto('site_logo.aspx');\"><i class=\"icon-hand-right\"></i> LOGO上传</button>");
                }
                else
                {
                    sb.AppendFormat("<button class=\"btn\" onclick=\"Goto('site_logo.aspx');\"> LOGO上传</button>");
                }
            }
            if (style.Banner)
            {
                if (curr == "banner")
                {
                    sb.AppendFormat("<button class=\"btn btn-info\" onclick=\"Goto('site_banner.aspx');\"><i class=\"icon-hand-right\"></i> 广告栏管理(Banner)</button>");
                }
                else
                {
                    sb.AppendFormat("<button class=\"btn\" onclick=\"Goto('site_banner.aspx');\"> 广告栏管理(Banner)</button>");
                }
            }
            if (style.Color)
            {
                if (curr == "color")
                {
                    sb.AppendFormat("<button class=\"btn btn-info\" onclick=\"Goto('site_color.aspx');\"><i class=\"icon-hand-right\"></i> 主题色选择</button>");
                }
                else
                {
                    sb.AppendFormat("<button class=\"btn\" onclick=\"Goto('site_color.aspx');\"> 主题色选择</button>");
                }
            }
            if (style.MiniNav)
            {
                if (curr == "mininav")
                {
                    sb.AppendFormat("<button class=\"btn btn-info\" onclick=\"Goto('nav.aspx');\"><i class=\"icon-hand-right\"></i> 微导航设置</button>");
                }
                else
                {
                    sb.AppendFormat("<button class=\"btn\" onclick=\"Goto('nav.aspx');\"> 微导航设置</button>");
                }
            }
            if (style.CopyRight)
            {
                if (curr == "copyright")
                {
                    sb.AppendFormat("<button class=\"btn btn-info\" onclick=\"Goto('site_copyright.aspx');\"><i class=\"icon-hand-right\"></i> 底部设置</button>");
                }
                else
                {
                    sb.AppendFormat("<button class=\"btn\" onclick=\"Goto('site_copyright.aspx');\"> 底部设置</button>");
                }
            }
            sb.Append("style" + style.StyleName);
            helper.Response(sb.ToString());
        }
    }
}