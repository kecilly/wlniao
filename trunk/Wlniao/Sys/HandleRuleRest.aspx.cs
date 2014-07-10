using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shijia.Domain;

namespace Wlniao.Sys
{
    public partial class HandleRuleRest : PageLogin
    {
        public Shijia.Service.IHandleRuleService HandleRuleService { get; set; }
        public Shijia.Service.IMPWechatService MPWechatService { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            HandleRule model = null;
            if (string.IsNullOrEmpty(helper.GetParam("KeyWord")))
            {
                helper.Result.Add("请先填写关键词!");    //关键词未填写
            }
            if (helper.Result.IsValid)
            {
                if (!string.IsNullOrEmpty(helper.GetParam("Id")))
                {
                    model = HandleRuleService.HandleRuleDao.Get(helper.GetParamInt("Id"));
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
                        HandleRule old = HandleRuleService.LoadAllByKeyWord(_CurrentAccount.Id, helper.GetParam("KeyWord")).ToList().FirstOrDefault();
                          //  db.find<Wlniao.Sys.HandleRule>("AccountId=" + _CurrentAccount.Id + " and KeyWord='" + helper.GetParam("KeyWord") + "'").first();
                        if (old != null && old.Id != model.Id)
                        {
                            helper.Result.Add("关键词“" + helper.GetParam("KeyWord") + "”已存在，请勿重复添加!");    //非当前用户的数据
                        }
                    }
                }
                else
                {
                    HandleRule old = HandleRuleService.LoadAllByKeyWord(_CurrentAccount.Id, helper.GetParam("KeyWord")).ToList().FirstOrDefault();
                    if (old != null)
                    {
                        helper.Result.Add("关键词“" + helper.GetParam("KeyWord") + "”已存在，请勿重复添加!");    //非当前用户的数据
                    }
                    else
                    {
                        try
                        {
                            model = new HandleRule();
                            model.AccountId = _CurrentAccount.Id;
                            model.KeyWord = helper.GetParam("KeyWord");
                            HandleRuleService.Save(model);
                        }
                        catch (Exception ex)
                        {
                            helper.Result.Add(ex.Message);
                        }
                    }
                }
            }
            if (helper.Result.IsValid)
            {
                switch (helper.GetParam("do"))
                {
                    case "Basic":
                    case "Api":
                    case "News":
                    case "Music":
                        model.Content = helper.GetParam("Content");
                        break;
                }
                 try
                        {
                model.KeyWord = helper.GetParam("KeyWord");
                model.Description = helper.GetParam("Description");
                model.GetMode = helper.GetParamInt("GetMode");
                model.MsgType = helper.GetParamInt("MsgType");
                HandleRuleService.Save(model);
                        }
                 catch (Exception ex)
                 {
                     helper.Result.Add(ex.Message);
                 }

                 MPWechatService.UpdateKeyWordsList(_CurrentAccount.Id);
            }
            helper.ResponseResult();
        }

    }
}