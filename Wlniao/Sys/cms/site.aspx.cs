using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wlniao.Cms
{
    public partial class Site : PageLogin
    {
        protected string msg = "";
        protected string _script = "";
        protected string _account = "";
        protected string _website = "";
        protected string sitename = "";
        protected string msgtitle = "";
        protected string keyword = "";
        protected string logosrc = "";
        protected string _dataurl = Oss.DataUrl;
        public Shijia.Service.IHandleRuleService HandleRuleService { set; get; }
        public Shijia.Service.IMPWechatService MPWechatService { set; get; }
        protected void Page_Load(object sender, EventArgs e)
        {
            _account = GetAccountGuid();
            Wlniao.Model.MiniSite minisite = Wlniao.MiniSite.Get(_account);
            if (minisite == null)
            {
                minisite = new Wlniao.Model.MiniSite();
            }
            sitename = Request["sitename"];
            msgtitle = Request["msgtitle"];
            keyword = Request["keyword"];
            logosrc = Request["logosrc"];
            if (string.IsNullOrEmpty(Request["method"]))
            {
                if (string.IsNullOrEmpty(sitename))
                {
                    sitename = minisite.SiteName;
                }
                if (string.IsNullOrEmpty(msgtitle))
                {
                    msgtitle = minisite.MsgTitle;
                }
                if (string.IsNullOrEmpty(keyword))
                {
                    keyword = minisite.KeyWords;
                }
                if (string.IsNullOrEmpty(logosrc))
                {
                    logosrc = minisite.LogoSrc;
                }
            }
            if (Request["method"] == "save")
            {
                string setwelcome = Request["setwelcome"];
                string setdefault = Request["setdefault"];
                string url = "";
                if (HttpContext.Current.Request.Url.Port == 80)
                {
                    url = "http://" + HttpContext.Current.Request.Url.Host;
                }
                else
                {
                    url = "http://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port;
                }
                url += "/mobile.aspx?a=" + _account;
                Result result = Wlniao.MiniSite.Set(base._CurrentAccount.Id, _account, sitename, msgtitle, HandleRuleService, MPWechatService, url, keyword, logosrc, setwelcome, setdefault);
                if (!result.IsValid)
                {
                    msg = result.Errors[0];
                }
                else
                {
                    _script = "<script>parent.showTips('微网站设置成功!',4);</script>";
                }
            }
            if (Request.Url.Port == 80)
            {
                _website = Request.Url.Host;
            }
            else
            {
                _website = Request.Url.Host + ":" + Request.Url.Port;
            }
        }
    }
}