using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wlniao.Rest.WX
{
    public partial class SetAppIdAndSecret : PageLogin
    {
        public Shijia.Service.IMPWechatService MPWechatService { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            bool Clear = helper.GetParam("Clear").ToLower() == "true";
            int MPType = helper.GetParamInt("MPType");
            string AppId = helper.GetParam("AppId");
            string AppSecret = helper.GetParam("AppSecret");
            if (Clear)
            {
                MPWechatService.SetAppIdAndSecret(_CurrentAccount.Id, "", "");
            }
            else
            {
                if (string.IsNullOrEmpty(AppId) || string.IsNullOrEmpty(AppSecret))
                {
                    helper.Result.Add("AppId或AppSecret未填写！");
                }
                else
                {
                    string token = "";
                    Result result = Wlniao.MP.Init(AppId, AppSecret, out token);
                    if (result.IsValid)
                    {
                        helper.Result.Join(MPWechatService.SetAppIdAndSecret(_CurrentAccount.Id, AppId, AppSecret, MPType));
                    }
                    else
                    {
                        helper.Result.Add("AppId或AppSecret错误，验证失败，设置未保存！");
                    }
                }
            }
            helper.ResponseResult();
        }
    }
}