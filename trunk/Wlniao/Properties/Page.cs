using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using Shijia.Domain;

namespace Wlniao
{
    public class PageLogin : System.Web.UI.Page
    {
        protected static string AppClientId = string.Empty;
        protected AshxHelper helper = new AshxHelper(HttpContext.Current);
        protected Account _CurrentAccount = null;
        public Shijia.Service.IAccountService AccountService { set; get; }
        public Shijia.Service.IKeyValueDataService KeyValueDataService { get; set; }
        public PageLogin()
        {
        }

        protected override void OnLoad(EventArgs e)
        {
            try
            {
                AppClientId = KeyValueDataService.GetString("AppClientId");
            }
            catch
            {

                AppClientId = string.Empty;
            }
            String username = GetAccountGuid();
            _CurrentAccount = AccountService.Get(username);
            if (_CurrentAccount==null)
            {
                if (string.IsNullOrEmpty(helper.GetParam("do")))
                {
                    Response.Clear();
                    Response.Write("<script>top.location.href='/login.aspx';</script>");
                    Response.End();
                }
                else
                {
                    Result result = new Result();
                    result.Add("Sorry,您尚未登录或登录已经超时！");
                    helper.Result = result;
                    helper.ResponseResult();
                }
            }
            else
            {
                base.OnLoad(e);
            }
        }
        protected string GetAccountGuid()
        {
            string account = "";
            try
            {
                account = Request.Cookies["login"].Values["account"];
            }
            catch { 
                account = "";
            //TODO: "2014-03-05";
            }
            return account;
        }

    }
    public class MiniPage : System.Web.UI.Page
    {
        protected Account _CurrentAccount = null;
        public Shijia.Service.IAccountService AccountService { set; get; }
        private static string _Root = "";
        protected string RootPath
        {
            get
            {
                if (string.IsNullOrEmpty(_Root))
                {
                    _Root = System.Web.Configuration.WebConfigurationManager.AppSettings["DataPath"];
                    if (string.IsNullOrEmpty(_Root))
                    {
                        _Root += AppDomain.CurrentDomain.BaseDirectory + "Data\\";
                    }
                    if (!_Root.EndsWith("\\"))
                    {
                        _Root += "\\";
                    }
                }
                return _Root;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            String username = MiniSiteAccount;
            _CurrentAccount = AccountService.Get(username);

            base.OnLoad(e);
        }
        private static string _DataUrl = Oss.DataUrl;
        protected string DataUrl
        {
            get
            {
                if (string.IsNullOrEmpty(_DataUrl))
                {
                    _DataUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["DataUrl"];
                    if (string.IsNullOrEmpty(_DataUrl))
                    {
                        if (Request.Url.Port == 80)
                        {
                            _DataUrl = "http://" + Request.Url.Host;
                        }
                        else
                        {
                            _DataUrl = "http://" + Request.Url.Host + ":" + Request.Url.Port;
                        }
                    }
                }
                if (!_DataUrl.StartsWith("http://"))
                {
                    _DataUrl = "http://" + _DataUrl;
                }
                return _DataUrl;
            }
        }
        protected string MiniSiteAccount
        {
            get
            {
                return Request["a"];
            }
        }

        protected string CopyRight
        {
            get { return "本功能由Weback提供技术支持"; }
        }
    }
}