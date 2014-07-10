using System;
using System.Collections.Generic;
using Shijia.Service;
using Spring.Context;
using Spring.Context.Support;
using Spring.Web;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shijia.Domain;

namespace Wlniao
{
    public partial class cms1 : MiniPage
    {
        protected string date = "";
        protected string wxname = "";
        protected string title = "";
        protected string pic = "";
        protected string content = "";
        public Shijia.Service.IMPWechatService MPWechatService { set; get; }
        public Shijia.Service.IHandleRuleService HandleRuleService { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            string account = MiniSiteAccount;
            if (!string.IsNullOrEmpty(account))
            {
                //IApplicationContext cxt = ContextRegistry.GetContext();
                //HandleRuleService manger = (HandleRuleService)cxt.GetObject("HandleRuleServiceImpl");

                HandleRule kw = HandleRuleService.Get(int.Parse(Request["kw"]));
                if (kw != null && kw.MsgType == 2)
                {
                    //MPWechatService MPWechat = (MPWechatService)cxt.GetObject("MPWechatServiceImpl");
                    MPWechat mpwechat = MPWechatService.Check(kw.AccountId.Value);
                    date = DateTime.Now.ToString("yyyy-MM-dd");
                    wxname = mpwechat.WeixinName;

                    string[] msgs = kw.Content.Split(new string[] { "#@@@#" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string msg in msgs)
                    {
                        string[] kv = msg.Split(new string[] { "#@@#" }, StringSplitOptions.None);
                        if (kv[0] == Request["t"])
                        {
                            title=kv[0];
                            pic=kv[2].Replace("\\", "/");
                            content=strUtil.HtmlDecode(kv[4]);
                            break;
                        }
                    }
                }
            }
        }
    }
}