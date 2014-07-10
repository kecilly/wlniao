using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wlniao.CMS
{
    public partial class Style : PageLogin
    {
        protected string _script = "";
        protected string _account = "";
        protected string ct = "";
        protected string _liststr = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            _account = GetAccountGuid();
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["style"]))
                {
                    Result rlt = Wlniao.MiniSite.SetStyle(_account, Request["style"]);
                    if (rlt.IsValid)
                    {
                        _script = "<script>parent.showTips('微网站风格应用成功!',4,'/sys/cms/style.aspx');</script>";
                    }
                    else
                    {
                        _script = "<script>parent.showTips('" + rlt.Errors[0] + "',5);</script>";
                    }
                }
                var site = Wlniao.MiniSite.Get(_account);
                var list = Wlniao.MiniSite.GetStyleList(_account);
                if (list != null)
                {
                    string style = "";
                    try
                    {
                        style = site.Style.ToLower();
                    }
                    catch { }
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();

                    foreach (var item in list)
                    {
                        if (item.StylePath.ToLower() == style)
                        {
                            if (true)
                            {
                                sb.AppendFormat("<div class=\"span3\"><div class=\"itemStyle\"><span onclick=\"setStyle('{0}');\"><div class=\"stylepic\"><p>效果图</p><img  class=\"navimg\" src=\"{2}\" /></div><h2>{1}</h2></span></div></div>", item.StylePath, item.StyleName, item.PicPath);
                            }
                            else
                            {
                                sb.AppendFormat("<div class=\"span3\"><div class=\"itemStyle\"><span onclick=\"onUse();\"><div class=\"stylepic\"><p>效果图</p><img  class=\"navimg\" src=\"{2}\" /></div><h2>{1}</h2></span></div></div>", item.StylePath, item.StyleName, item.PicPath);
                            }
                        }
                        else
                        {
                            sb.AppendFormat("<div class=\"span3\"><div class=\"itemStyle\"><span onclick=\"setStyle('{0}');\"><div class=\"stylepic\"><p>效果图</p><img  class=\"navimg\" src=\"{2}\" /></div><h1>{1}</h1></span></div></div>", item.StylePath, item.StyleName, item.PicPath);
                        }
                    }
                    _liststr = sb.ToString();                    
                }
            }
        }

    }
}