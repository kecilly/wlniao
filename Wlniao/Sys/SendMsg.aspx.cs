using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wlniao.Sys
{
    public partial class SendMsg : PageLogin
    {
        public Shijia.Service.IMPWechatService MPWechatService { get; set; }
        public Shijia.Service.IMPUserInfoService MPUserInfoService { get; set; }
        protected string _ListStr = "";
        protected string _PageBar = "";
        protected string _Keyword = "";
        protected string fansCount = "0";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (helper.GetParam("do") == "SendMsg")
                {
                    Shijia.Domain.MPUserInfo mpuser = MPUserInfoService.Get(helper.GetParamInt("id"));
                    if (mpuser == null || mpuser.AccountId != _CurrentAccount.Id)
                    {
                        helper.Result.Add("Sorry，您指定的用户不存在!");
                    }
                    else
                    {
                        Shijia.Domain.MPWechat mpwechat = MPWechatService.Check(mpuser.AccountId);
                        if (mpwechat != null)
                        {
                            helper.Result.Join(MP.SendText(mpwechat.AppId, mpwechat.AppSecret, mpuser.OpenId, helper.GetParam("text")));
                        }
                    }
                    helper.ResponseResult();
                }
            }
        }

    }
}