using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Wlniao
{
    public partial class Login : System.Web.UI.Page
    {
        protected string SiteName = string.Empty;
        protected string msg = "";
        protected string username = "";
        protected string password = "";
        public Shijia.Service.IAccountService AccountService { set; get; }
        public Shijia.Service.IKeyValueDataService KeyValueDataService { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var siteName = ViewState["SiteName"];
                if (siteName == null)
                {
                    ViewState["SiteName"] = KeyValueDataService.GetString("SiteName");
                }

                SiteName = ViewState["SiteName"].ToString();

                username = Request["username"];
                password = Request["password"];
                if (Request["f"] == "qd")
                {
                    Response.Cookies["login"].Values["account"] = "";
                    password = Encryptor.DesDecrypt(password, "weback");
                }
                if (string.IsNullOrEmpty(username))
                {
                    msg = "";
                }
                else if (string.IsNullOrEmpty(password))
                {
                    msg = "请填写登录密码";
                }
                else
                {
                    username = username.Trim();
                    password = password.Trim();
                    Result result = AccountService.CheckLogin(username, password);
                    if (result.IsValid)
                    {   
                        msg = "<font color=\"green\">登录成功!</font>";
                        Response.Cookies["login"].Values["account"] = username;
                        Response.Redirect("/default.aspx");
                    }
                    else
                    {
                        msg = result.Errors[0];
                    }
                }
            }
        }
    }
}