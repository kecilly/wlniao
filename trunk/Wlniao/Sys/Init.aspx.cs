using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wlniao.Sys
{
    public partial class Init : PageLogin
    {
        protected string _website = "";
        protected string weixinname = "";
        protected string weixinaccount = "";
        protected string weixintoken = "";
        protected string verifycontent = "";
        protected bool showMsg = false;
        public Shijia.Service.IMPWechatService MPWechatService { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            Shijia.Domain.MPWechat mpwechat = MPWechatService.Check(_CurrentAccount.Id);
            weixinaccount = mpwechat.FristAccount;
            weixinname = mpwechat.WeixinName;
            weixintoken = mpwechat.Token;
            verifycontent = mpwechat.VerifyContent;
            if (Request.Url.Port == 80)
            {
                _website = Request.Url.Host;
            }
            else
            {
                _website = Request.Url.Host + ":" + Request.Url.Port;
            }
            try
            {
                if (Request.QueryString[0] == "nosend")
                {
                    showMsg = true;
                    if (string.IsNullOrEmpty(verifycontent) || string.IsNullOrEmpty(weixintoken))
                    {
                        verifycontent = Rand.Number(6).ToLower();
                        if (string.IsNullOrEmpty(weixintoken))
                        {
                            weixintoken = Rand.Str(12).ToLower();
                            MPWechatService.NewVerify(_CurrentAccount.Id, verifycontent, weixintoken);
                        }
                        else
                        {
                            MPWechatService.NewVerify(_CurrentAccount.Id, verifycontent);
                        }
                    }
                }
            }
            catch { }
        }
    }
}