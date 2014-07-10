using System;
using System.Collections.Generic;
using System.Xml;
using System.Web;
using System.IO;

namespace Wlniao.App
{
    public class WxApi : System.Web.UI.Page
    {
        protected string weixinFristAccount = "", clientOpenId = "", MsgType = "", MsgId = "", Event = "", Content = "";
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                #region 开始解析Post过来的数据
                var document = new StreamReader(Request.InputStream).ReadToEnd();
                //声明一个XMLDoc文档对象，LOAD（）xml字符串
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(document);
                weixinFristAccount = doc.GetElementsByTagName("ToUserName")[0].InnerText.Trim();
                clientOpenId = doc.GetElementsByTagName("FromUserName")[0].InnerText.Trim();
                MsgType = doc.GetElementsByTagName("MsgType")[0].InnerText.Trim();
                try
                {
                    MsgId = doc.GetElementsByTagName("MsgId")[0].InnerText;
                }
                catch { }
                try
                {
                    Content = doc.GetElementsByTagName("Content")[0].InnerText;
                }
                catch { }
                try
                {
                    Event = doc.GetElementsByTagName("Event")[0].InnerText;
                    if (string.IsNullOrEmpty(Content) && !string.IsNullOrEmpty(Event))
                    {
                        Content = Event;
                    }
                }
                catch { }
                #endregion 数据解析结束
                base.OnLoad(e);
            }
            catch (Exception ex)
            {
                Response.Write(Request.QueryString["echostr"]);
            }
        }
    }
}