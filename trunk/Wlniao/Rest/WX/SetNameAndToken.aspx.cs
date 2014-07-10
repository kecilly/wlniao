using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wlniao.Rest.WX
{
    public partial class SetNameAndToken : PageLogin
    {
        public Shijia.Service.IMPWechatService MPWechatService { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            string WeixinName = helper.GetParam("WeixinName");
            string Token = helper.GetParam("WeixinToken");
            helper.Result.Join(MPWechatService.SetNameAndToken(_CurrentAccount.Id, WeixinName, Token));
            helper.ResponseResult();
        }
    }
}