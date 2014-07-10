using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Senparc.Weixin.MP;
using Shijia.Domain;
using Spring.Context;
using Spring.Context.Support;
using Spring.Web;
namespace Wlniao
{
    public partial class wxapi : System.Web.UI.Page
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger("WebLogger");
        protected string output = "";
        public Shijia.Service.IAccountService AccountService { set; get; }
        public Shijia.Service.IMPWechatService MPWechatService { set; get; }
        public Shijia.Service.IKeyValueDataService KeyValueDataService { get; set; }
        private bool AutoVerify = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Clear();
            string signature = Context.Request.QueryString["signature"];
            string timestamp = Context.Request.QueryString["timestamp"];
            string nonce = Context.Request.QueryString["nonce"];
            string echostr = Context.Request.QueryString["echostr"];
            string acc = Context.Request.QueryString["a"];
            #region 获取帐号和渠道信息  开始
            if (!string.IsNullOrEmpty(acc))
            {
                acc = Context.Request.QueryString["a"].Split('#')[0];
            }
            //using (TextWriter tw = new StreamWriter(Server.MapPath("~/log/Error_" + DateTime.Now.Ticks + ".txt")))
            //{
            //    tw.WriteLine("帐号：" + acc);
            //    tw.WriteLine("signature:" + signature + " timestamp:" + timestamp + " nonce:" + nonce + " echostr:" + echostr);
            //    tw.Flush();
            //    tw.Close();
            //}
            
            logger.Info("帐号："+acc);
            #endregion 获取帐号和渠道信息 结束

            var account = AccountService.Get(acc);
            if (account != null)
            {
                Shijia.Domain.MPWechat mpwechat = MPWechatService.Check(account.Id);
                if (mpwechat != null)
                {
                    try
                    {
                        AutoVerify = KeyValueDataService.GetBool("SysAutoVerify");

                        if (AutoVerify && Request.HttpMethod == "GET")
                        {
                            //get method - 仅在微信后台填写URL验证时触发
                            if (CheckSignature.Check(signature, timestamp, nonce,mpwechat.Token))
                            {
                                mpwechat.VerifyContent = string.Empty;
                                mpwechat.FristAccount = "gh_xxxxxxxxxxxx";
                                MPWechatService.Update(mpwechat);
                                WriteContent(echostr); //返回随机字符串则表示验证通过
                            }
                            else
                            {
                                WriteContent("failed:" + signature + "," + CheckSignature.GetSignature(timestamp, nonce, mpwechat.Token) + "。" +
                                            "如果你在浏览器中看到这句话，说明此地址可以被作为微信公众账号后台的Url，请注意保持Token一致。");
                            }
                            Response.End();
                        }
                        else if (CheckSignature.Check(signature, timestamp, nonce, mpwechat.Token))
                        {
                            //可以设置每个人上下文消息储存的最大数量，防止内存占用过多，如果该参数小于等于0，则不限制
                          
                            string document = new StreamReader(Request.InputStream).ReadToEnd(); 
                            #region 开始解析Post过来的数据
                            //声明一个XMLDoc文档对象，LOAD（）xml字符串
                            //XmlDocument doc = new XmlDocument();
                            //doc.LoadXml(document);
                            //var serverAccount = doc.GetElementsByTagName("ToUserName")[0].InnerText.Trim();
                            //var clientOpenId = doc.GetElementsByTagName("FromUserName")[0].InnerText.Trim();
                            //string MsgType = doc.GetElementsByTagName("MsgType")[0].InnerText.Trim();
                            //string MsgId = "", Event = "", Content = "";
                            //try
                            //{
                            //    MsgId = doc.GetElementsByTagName("MsgId")[0].InnerText;
                            //}
                            //catch { }
                            //try
                            //{
                            //    Content = doc.GetElementsByTagName("Content")[0].InnerText;
                            //}
                            //catch { }
                            //try
                            //{
                            //    Event = doc.GetElementsByTagName("Event")[0].InnerText;
                            //    if (string.IsNullOrEmpty(Content) && !string.IsNullOrEmpty(Event))
                            //    {
                            //        if (Event.ToUpper() == "CLICK")
                            //        {
                            //            Content = doc.GetElementsByTagName("EventKey")[0].InnerText;
                            //        }
                            //        else if (Event == "subscribe")
                            //        {
                            //            Content = "subscribe";
                            //        }
                            //        else
                            //        {
                            //            Content = Event;
                            //        }
                            //    }
                            //}
                            //catch { }
                            #endregion 数据解析结束

                            IApplicationContext cxt = ContextRegistry.GetContext();

                            Hub.Start(mpwechat, document, out output, cxt,ApiUrl);
                            //using (TextWriter tw = new StreamWriter(Server.MapPath("~/log/Error_" + DateTime.Now.Ticks + ".txt")))
                            //{
                            //    tw.WriteLine("处理结束：");
                            //    tw.WriteLine("output:" + output);
                            //    tw.Flush();
                            //    tw.Close();
                            //}
                        }
                        else
                        {
                            WriteContent("参数错误！signature:" + signature + " timestamp:" + timestamp + " nonce:" + nonce + " echostr:" + echostr);
                            return;
                        }
                    }
                    catch(Exception ex)
                    {
                        //using (TextWriter tw = new StreamWriter(Server.MapPath("~/log/Error_" + DateTime.Now.Ticks + ".txt")))
                        //{
                        //    tw.WriteLine(ex.Message);
                        //   // tw.WriteLine(ex.InnerException.Message);
                        //    tw.Flush();
                        //    tw.Close();
                        //}


                        if (CheckSignature.Check(Context.Request.QueryString["signature"],
                            Context.Request.QueryString["timestamp"], Context.Request.QueryString["nonce"],
                            mpwechat.Token))
                        {
                            output = Request.QueryString["echostr"];
                        }
                        else
                        {
                            output = "Token签名错误";
                        }
                    }
                }
                else
                {
                    output = "当前用户暂未启用Shijia服务!";
                }
            }
            else
            {
                output = "您正在使用的API地址有误!";
            }
            Response.Write(output);
        }

        public static string ApiUrl
        {
            get
            {               
                    if (HttpContext.Current.Request.Url.Port == 80)
                    {
                        return "http://" + HttpContext.Current.Request.Url.Host;
                    }
                    else
                    {
                        return "http://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port;
                    }              
               
            }
        }

        private void WriteContent(string str)
        {
            Response.Output.Write(str);
        }
        ///// <summary>
        ///// 根据参数和密码生成签名字符串
        ///// </summary>
        ///// <param name="parameters">API参数</param>
        ///// <param name="secret">密码</param>
        ///// <returns>签名字符串</returns>
        //private static bool CheckSignature(HttpContext context, String WeChatToken)
        //{
        //    if (string.IsNullOrEmpty(WeChatToken))
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        string[] arr = { WeChatToken, context.Request.QueryString["timestamp"], context.Request.QueryString["nonce"] };
        //        Array.Sort(arr);     //字典排序
        //        return System.Encryptor.GetSHA1(string.Join("", arr)).ToLower() == context.Request.QueryString["signature"];
        //    }
        //}
    }
}