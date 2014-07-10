using System;
using System.Collections.Generic;
using System.Xml;
using System.Web;

namespace Wlniao.HubMethod
{
    public class MsgLog
    {
        public static void Do(object guid)
        {
            try
            {
                string text = Hub.Stack[guid.ToString()].Input;

                #region 开始解析Post过来的数据
                //声明一个XMLDoc文档对象，LOAD（）xml字符串
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(text);
                var serverAccount = doc.GetElementsByTagName("ToUserName")[0].InnerText.Trim();
                var clientOpenId = doc.GetElementsByTagName("FromUserName")[0].InnerText.Trim();
                string MsgType = doc.GetElementsByTagName("MsgType")[0].InnerText.Trim();
                string MsgId = "", Event = "", Content = "";
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
                        if (Event.ToUpper() == "CLICK")
                        {
                            Content = doc.GetElementsByTagName("EventKey")[0].InnerText;
                        }
                        else
                        {
                            Content = Event;
                        }
                    }
                }
                catch { }
                #endregion 数据解析结束

                switch (MsgType.ToLower())
                {
                }
            }
            catch { }
        }
    }
}