using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wlniao.Sys
{
    public partial class ResponseMsg : PageLogin
    {
        public Shijia.Service.IMPWechatService MPWechatService { get; set; }
        protected string _account = "";
        protected string _script = "";
        protected string welcomemsg = "";
        protected string defaultmsg = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _account = GetAccountGuid();
                Shijia.Domain.MPWechat weixin = MPWechatService.Check(_CurrentAccount.Id);
                if (weixin == null)
                {
                    Response.Redirect("setting.aspx");
                }
                else
                {
                    if (Request["method"] == "save")
                    {
                        welcomemsg = Request["welcome"];
                        defaultmsg = Request["default"];
                        MPWechatService.SetWelcomeAndDefault(_CurrentAccount.Id, welcomemsg, defaultmsg);
                        _script = "<script>parent.showTips('恭喜你,操作已保存',4);</script>";
                    }
                    else
                    {
                        welcomemsg = weixin.WelcomeMsg;
                        defaultmsg = weixin.DefaultMsg;
                    }
                }
            }
        }

    }
}