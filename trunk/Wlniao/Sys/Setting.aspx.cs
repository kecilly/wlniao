using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wlniao.Sys
{
    public partial class Setting : PageLogin
    {
        public Shijia.Service.IMPWechatService MPWechatService { get; set; }
        protected string _website = "";
        protected string WeixinName = "";
        protected string WeixinToken = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Url.Port == 80)
            {
                _website = Request.Url.Host;
            }
            else
            {
                _website = Request.Url.Host + ":" + Request.Url.Port;
            }

            Shijia.Domain.MPWechat mpwechat = MPWechatService.Check(_CurrentAccount.Id);
            WeixinName = mpwechat.WeixinName;
            WeixinToken = mpwechat.Token;
        }
    }
}