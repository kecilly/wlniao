using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wlniao.Sys
{
    public partial class WeixinAuth : PageLogin
    {
        public Shijia.Service.IMPWechatService MPWechatService { get; set; }
        protected string MPType = "";
        protected string AppId = "";
        protected string AppSecret = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Shijia.Domain.MPWechat weixin = MPWechatService.Check(_CurrentAccount.Id);
            MPType = weixin.MPType.ToString();
            AppId = weixin.AppId;
            AppSecret = weixin.AppSecret;
        }
    }
}