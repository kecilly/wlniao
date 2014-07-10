using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Shijia.Service;
using Spring.Context;
using Spring.Context.Support;
using Spring.Web;

namespace Wlniao
{
    public class Global : System.Web.HttpApplication
    {
        
        protected void Application_Start(object sender, EventArgs e)
        {
            log4net.Config.BasicConfigurator.Configure(); 
            IApplicationContext cxt = ContextRegistry.GetContext();
            IKeyValueDataService KvTableUtil1 = (IKeyValueDataService)cxt.GetObject("KeyValueDataServiceImpl");

            if (!KvTableUtil1.GetBool("install"))
            {
                KvTableUtil1.Save("install", "true");
                KvTableUtil1.Save("SiteName", "世佳微信管家（www.shijia.com.cn）");     //网站显示的名称

                KvTableUtil1.Save("AppCenter", "false");     //是否启用应用中心，关闭时为false
                KvTableUtil1.Save("AppClientId", "");        //应用中心ID，如不知道您的AppClientId，请联系QQ：1469946999
                KvTableUtil1.Save("AppSercet", "");          //应用中心密钥
                KvTableUtil1.Save("AppSercet", "");          //应用中心密钥
                KvTableUtil1.Save("SysAutoVerify", "true");  //是否开启自动认证
                KvTableUtil1.Save("MaxRecordCount", "10");   //设置每个人上下文消息储存的最大数量
              
                IAccountService manger = (IAccountService)cxt.GetObject("AccountServiceImpl");
                Result result = manger.Add("demo", "123456", "", "");
                if (result.IsValid)
                {
                    IMPWechatService MPWechat = (IMPWechatService)cxt.GetObject("MPWechatServiceImpl");

                    MPWechat.ClearVerify(1);
                }
            }
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}