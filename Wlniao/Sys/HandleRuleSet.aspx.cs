using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shijia.Domain;

namespace Wlniao.Sys
{
    public partial class HandleRuleSet : PageLogin
    {
        public Shijia.Service.IHandleRuleService HandleRuleService { get; set; }
        public Shijia.Service.IMPWechatService MPWechatService { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(helper.GetParam("Id")))
            {
                HandleRule model = HandleRuleService.Get(helper.GetParamInt("Id"));
                if (model == null)
                {
                    helper.Result.Add("规则不存在或已删除!");    //数据未查到
                }
                else if (model.AccountId != _CurrentAccount.Id)
                {
                    helper.Result.Add("规则不存在或已删除!");    //非当前用户的数据
                }
                else
                {
                    switch (helper.GetParam("do"))
                    {
                        case "Welcome":
                            helper.Result.Join(MPWechatService.SetWelcomeMsg(_CurrentAccount.Id, "Link:" + model.KeyWord));
                            break;
                        case "Default":
                            helper.Result.Join(MPWechatService.SetDefaultMsg(_CurrentAccount.Id, "Link:" + model.KeyWord));
                            break;
                        case "Del":
                            try
                            {
                                MPWechatService.UpdateKeyWordsList(_CurrentAccount.Id);
                            }
                            catch                                                        
                            {
                                helper.Result.Add("规则删除失败，请稍后再试!");    //非当前用户的数据
                            }                            
                            break;
                    }
                }
            }
            else
            {
                helper.Result.Add("未指定需要操作的规则!");    //数据未查到
            }
            helper.ResponseResult();
        }

    }
}