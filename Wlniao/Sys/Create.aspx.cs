using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wlniao.Sys
{
    public partial class Create : System.Web.UI.Page
    {
        public Shijia.Service.IMPWechatService MPWechatService { get; set; }
        protected string msg = "";
        protected string username = "";
        protected string password = "";
        protected string repassword = "";
        protected string email = "";
        protected string mobile = "";
        public Shijia.Service.IAccountService AccountService { set; get; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                username = Request["username"];
                password = Request["password"];
                repassword = Request["repassword"];
                email = Request["email"];
                mobile = Request["mobile"];
                if (string.IsNullOrEmpty(username))
                {
                    msg = "";
                }
                else if (string.IsNullOrEmpty(password))
                {
                    msg = "请填设置登录密码";
                }
                else if (password!=repassword)
                {
                    msg = "两次输入的密码不一致";
                }
                else
                {
                    Result result = AccountService.Add(username, password, email, mobile);
                    if (result.IsValid)
                    {
                        var account = AccountService.Get(username);
                        result.Join(MPWechatService.Init(account.Id));
                        if (result.IsValid)
                        {
                            Response.Cookies["login"].Values["account"] = username;
                            Response.Redirect("/Default.aspx");
                        }
                        else
                        {
                            msg = result.Errors[0];
                        }
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