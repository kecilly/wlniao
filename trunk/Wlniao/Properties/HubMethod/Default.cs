using System;
using System.Collections.Generic;
using System.Xml;
using System.Web;
using System.IO;
using System.Text;
using Senparc.Weixin.MP.Helpers;
using Shijia.Domain;
using Shijia.Process.CommonService.CustomMessageHandler;
using Shijia.Service;
using Spring.Context;
using Spring.Context.Support;
using Spring.Web;

namespace Wlniao.HubMethod
{
    public class Default
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static IApplicationContext IAC { set; get; }
        public static IMPWechatService MPWechat { set; get; }
        public static IMPUserInfoService MPUserInfo { set; get; }
        public static IKeyValueDataService KeyValueDataService { set; get; }
        public static int MaxRecordCount { get; set; }

        public static void Do(object guid)
        {
            //try
            //{
                //IApplicationContext cxt = ContextRegistry.GetContext();
                //MPWechatService MPWechat = (MPWechatService)cxt.GetObject("MPWechatServiceImpl");
                Hub.HubThread s = Hub.Stack[guid.ToString()];
                try
                {
                    //可以设置每个人上下文消息储存的最大数量，防止内存占用过多，如果该参数小于等于0，则不限制
                    MaxRecordCount = 10;

                    
                    IAC = s.IAC;
                    if (MPWechat == null)
                    {
                        MPWechat = (IMPWechatService)IAC.GetObject("MPWechatServiceImpl");
                    }
                    if (MPUserInfo == null)
                    {
                        MPUserInfo = (IMPUserInfoService)IAC.GetObject("MPUserInfoServiceImpl");
                    }

                    if (KeyValueDataService == null)
                    {
                        KeyValueDataService = (IKeyValueDataService)IAC.GetObject("KeyValueDataServiceImpl");
                    }
                    MaxRecordCount = KeyValueDataService.GetInt("MaxRecordCount");
                    byte[] inputs = Encoding.Unicode.GetBytes(s.Input);
                    Stream inputStream = new MemoryStream(inputs); 

                    //自定义MessageHandler，对微信请求的详细判断操作都在这里面。
                    var messageHandler = new CustomMessageHandler(inputStream, MaxRecordCount);
                    messageHandler.MPWechat = MPWechat;
                    messageHandler.MPUserInfo = MPUserInfo;
                    messageHandler.MPWechatInfo = s.MPWechat;
                    messageHandler.ApiUrl = s.APIUrl;
                    //测试时可开启此记录，帮助跟踪数据。
                    logger.Info(messageHandler.RequestDocument.ToString());
                   
                    //执行微信处理过程
                    messageHandler.Execute();
                    //测试时可开启，帮助跟踪数据
                    logger.Info(messageHandler.ResponseDocument.ToString());
                  
                    s.Output = messageHandler.ResponseDocument.ToString();
                    return;
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message );
                    //if (messageHandler.ResponseDocument != null)
                    //{
                    //    logger.Error(messageHandler.ResponseDocument.ToString());
                    //}

                    s.Output = "";
                   // throw ex;
                }

/*

                //声明一个XMLDoc文档对象，LOAD（）xml字符串
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(s.Input);
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
               
                MPUserInfo mpuserinfo = MPUserInfo.Check(clientOpenId, s.MPWechat.AccountId.Value);

                if (mpuserinfo != null)
                {
                    #region 同步用户信息 开始
                    if (mpuserinfo.SubscribeTime > DateTools.ConvertToUnixofLong() - 180 && !string.IsNullOrEmpty(s.MPWechat.AppId))
                    {
                        String UserInfoJson = null;
                        if (!Wlniao.MP.GetUserInfo(s.MPWechat.AppId, s.MPWechat.AppSecret, clientOpenId, out UserInfoJson).IsValid)
                        {
                            UserInfoJson = null;
                        }
                        Dictionary<String, object> map = System.Serialization.JsonParser.Parse(UserInfoJson) as Dictionary<String, object>;
                        mpuserinfo.NickName = Json.GetFieldStr(map, "nickname");
                        try
                        {
                            mpuserinfo.Sex = int.Parse(Json.GetFieldStr(map, "sex"));
                        }
                        catch { }
                        mpuserinfo.Country = Json.GetFieldStr(map, "country");
                        mpuserinfo.Province = Json.GetFieldStr(map, "province");
                        mpuserinfo.City = Json.GetFieldStr(map, "city");
                        mpuserinfo.MPLanguage = Json.GetFieldStr(map, "language");
                        mpuserinfo.HeadImgUrl = Json.GetFieldStr(map, "headimgurl");
                        try
                        {
                            mpuserinfo.SubscribeTime = long.Parse(Json.GetFieldStr(map, "subscribe_time"));
                        }
                        catch { }
                        MPUserInfo.SaveOrUpdate(mpuserinfo);
                        
                    }
                    mpuserinfo.LastVisitTime = DateTime.Now;
                    MPUserInfo.SaveOrUpdate(mpuserinfo);
                    #endregion 同步用户信息 结束
                }
                bool allowGoto = true;
                string extUrl = "";
            startDo:    //消息处理开始处
                logger.Info("事件:" + Event + "|MsgId:" + MsgId + "|Content:" + Content + "|MsgType:" + MsgType);
                switch (MsgType.ToLower())
                {
                    case "event":
                        switch (Content.ToLower())
                        {
                            case "subscribe":
                                logger.Info("关注：openid:" + mpuserinfo.OpenId);
                                if (s.MPWechat.WelcomeMsg.StartsWith("Link:") && allowGoto)
                                {
                                    allowGoto = false;
                                    MsgType = "text";
                                    Content = s.MPWechat.WelcomeMsg.Replace("Link:", "");
                                    goto startDo;   //跳转至消息处理开始处
                                }
                                else
                                {
                                    ResponseMsg(s, strUtil.HtmlDecode(s.MPWechat.WelcomeMsg), clientOpenId, serverAccount);
                                }
                                break;
                            case "unsubscribe":
                                   logger.Info("取消关注：openid:" + mpuserinfo.OpenId);
                                    MPUserInfo.Delete(mpuserinfo.Id);
                                break;
                            default:
                                 allowGoto = true;
                                 MsgType = "text";
                                 goto startDo;
                                break;
                        }
                       
                        break;
                    case "text":
                        if (string.IsNullOrEmpty(Content) || s.MPWechat.VerifyContent != Content)
                        {
                            var keyword = MPWechat.GetByText(s.MPWechat, Content);
                            if (keyword == null)
                            {
                                MsgType = "default";
                                goto startDo;
                            }
                            else if (keyword.MsgType == 4)
                            {
                                allowGoto = false;
                                MsgType = "api";
                                extUrl = keyword.Content.Replace("Api:", "");
                                goto startDo;   //跳转至消息处理开始处
                            }
                            else if (keyword.MsgType == 1)
                            {
                                try
                                {
                                    string[] msgs = keyword.Content.Split(new string[] { "#@@@#" }, StringSplitOptions.RemoveEmptyEntries);
                                    string msg = strUtil.HtmlDecode(msgs[new Random().Next(msgs.Length)]);
                                    if (msg.StartsWith("Api:") && allowGoto)
                                    {
                                        allowGoto = false;
                                        MsgType = "api";
                                        extUrl = msg.Replace("Api:", "");
                                        goto startDo;   //跳转至消息处理开始处
                                    }
                                    else
                                    {
                                        ResponseMsg(s, msg.Replace("<br>", "\n").Replace("<br/>", "\n"), clientOpenId, serverAccount);
                                    }
                                }
                                catch { }
                            }
                            else if (keyword.MsgType == 3)
                            {
                                #region 音乐回复
                                string musictitle = "";
                                string musicurl = "";
                                string musichdurl = "";
                                string musicdesc = "";
                                string[] msgs = keyword.Content.Split(new string[] { "#@@@#" }, StringSplitOptions.RemoveEmptyEntries);
                                foreach (string msg in msgs)
                                {
                                    string[] kv = msg.Split(new string[] { "#@@#" }, StringSplitOptions.None);
                                    if (kv[0] == "musictitle")
                                    {
                                        try
                                        {
                                            musictitle = kv[1];
                                        }
                                        catch { musictitle = ""; }
                                    }
                                    else if (kv[0] == "musicurl")
                                    {
                                        try
                                        {
                                            musicurl = kv[1].Replace("\\", "/");
                                        }
                                        catch { musicurl = ""; }
                                    }
                                    else if (kv[0] == "musichdurl")
                                    {
                                        try
                                        {
                                            musichdurl = kv[1].Replace("\\", "/");
                                        }
                                        catch { musichdurl = ""; }
                                    }
                                    else if (kv[0] == "musicdesc")
                                    {
                                        try
                                        {
                                            musicdesc = kv[1];
                                        }
                                        catch { musicdesc = ""; }
                                    }
                                }
                                ResponseMusicMsg(s, musictitle, musicdesc, musicurl, musichdurl, clientOpenId, serverAccount);
                                #endregion
                            }
                            else if (keyword.MsgType == 2)
                            {
                                #region 图文回复
                                string newstitle = "";
                                string newsdesc = "";
                                string newspicurl = "";
                                string newspiclink = "";
                                string newscontent = "";
                                string[] msgs = keyword.Content.Split(new string[] { "#@@@#" }, StringSplitOptions.RemoveEmptyEntries);
                                int i = 0;
                                List<RuleContent> listTemp = new List<RuleContent>();
                                foreach (string msg in msgs)
                                {
                                    string[] kv = msg.Split(new string[] { "#@@#" }, StringSplitOptions.None);
                                    try
                                    {
                                        newstitle = strUtil.HtmlDecode(kv[0]).Replace("'", "\\'");
                                    }
                                    catch { newstitle = ""; }
                                    try
                                    {
                                        newsdesc = strUtil.HtmlDecode(kv[1]).Replace("'", "\\'");
                                    }
                                    catch { newsdesc = ""; }
                                    try
                                    {
                                        newspicurl = kv[2].Replace("\\", "/").Replace("'", "\\'");
                                    }
                                    catch { newspicurl = ""; }
                                    try
                                    {
                                        newspiclink = kv[3].Replace("\\", "/").Replace("'", "\\'");
                                    }
                                    catch { newspiclink = ""; }
                                    try
                                    {
                                        newscontent = strUtil.HtmlDecode(kv[4]).Replace("'", "\\'");
                                    }
                                    catch { newscontent = ""; }
                                    RuleContent rc = new RuleContent();
                                    rc.Title = newstitle;
                                    rc.ContentType = "pictext";
                                    if (string.IsNullOrEmpty(newspiclink) || newspiclink == "#")
                                    {
                                        newspiclink = "/cms.aspx?kw=" + keyword.Id + "&t=" + strUtil.UrlEncode(newstitle) + "&a=" + s.MPWechat.AccountId;
                                        newscontent = strUtil.Ellipsis(strUtil.RemoveHtmlTag(newscontent), 200, "...");
                                    }
                                    rc.LinkUrl = newspiclink;
                                    rc.PicUrl = newspicurl;
                                    rc.ThumbPicUrl = newspicurl;
                                    rc.TextContent = newscontent;
                                    listTemp.Add(rc);
                                    i++;
                                }
                                ResponsePicTextMsg(s, listTemp, clientOpenId, serverAccount);
                                #endregion
                            }
                        }
                        else if (string.IsNullOrEmpty(s.MPWechat.FristAccount))
                        {
                            MPWechat.Verify(s.MPWechat, serverAccount, clientOpenId);
                            ResponseMsg(s, "恭喜你，该公众帐号初始化成功！", clientOpenId, serverAccount);
                        }
                        break;
                    //case "image":   //图片消息
                    //    Wlniao.Weixin.SaveRequest(account, clientUser, weixinFristAccount, MsgType, PostAndGet.GetResponseString(doc.GetElementsByTagName("PicUrl")[0].InnerText), MsgId);
                    //    Wlniao.WxApi.ResponseMsg("Image Ok", clientUser, weixinFristAccount);
                    //    break;
                    //case "voice":   //语音消息
                    //    Wlniao.Weixin.SaveRequest(account, clientUser, weixinFristAccount, MsgType, "MediaId:" + doc.GetElementsByTagName("MediaId")[0].InnerText);
                    //    break;
                    //case "location":   //地理位置
                    //    Wlniao.Weixin.SaveRequest(account, clientUser, weixinFristAccount, MsgType, string.Format("Location_X:{0},Location_Y:{1},Scale:{2}", doc.GetElementsByTagName("Location_X")[0].InnerText, doc.GetElementsByTagName("Location_Y")[0].InnerText, doc.GetElementsByTagName("Scale")[0].InnerText));
                    //    break;
                    case "api":   //转发到API
                        string apiResult = GetFromUrl(extUrl, Content, s.MPWechat.AccountId.ToString(), serverAccount, clientOpenId, s.MPWechat.Token, s.MPWechat.FristAccount);
                        ResponseMsg(s, apiResult, clientOpenId, serverAccount);
                        break;
                    default:
                        if (s.MPWechat.DefaultMsg.StartsWith("Link:") && allowGoto)
                        {
                            allowGoto = false;
                            MsgType = "text";
                            Content = s.MPWechat.DefaultMsg.Replace("Link:", "");
                            goto startDo;
                        }
                        else if (s.MPWechat.DefaultMsg.StartsWith("Api:") && allowGoto)
                        {
                            allowGoto = false;
                            MsgType = "api";
                            extUrl = s.MPWechat.DefaultMsg.Replace("Api:", "");
                            goto startDo;   //跳转至消息处理开始处
                        }
                        else if (!string.IsNullOrEmpty(s.MPWechat.DefaultMsg))
                        {
                            ResponseMsg(s, strUtil.HtmlDecode(s.MPWechat.DefaultMsg), clientOpenId, serverAccount);
                        }
                        break;
                }

            }
            catch { }
           */
        }

        //private static String GetFromUrl(string apiurl, string content, string accountid = "", string firstid = "", string clientUser = "", string token = "", string setfirstid = "")
        //{
        //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //    sb.AppendFormat("<xml>");
        //    sb.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", string.IsNullOrEmpty(setfirstid) ? firstid : setfirstid);
        //    sb.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", clientUser);
        //    sb.AppendFormat("<CreateTime>{0}</CreateTime>", DateTools.GetNow().Ticks);
        //    sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
        //    sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", content);
        //    sb.AppendFormat("</xml>");

        //    string paramstr = "";
        //    if (apiurl.IndexOf("signature") <= 0)
        //    {
        //        string timestamp = (DateTime.Now.Ticks / 1000000).ToString();
        //        string[] arr = { token, timestamp, timestamp };
        //        Array.Sort(arr);     //字典排序
        //        paramstr = "signature=" + System.Encryptor.GetSHA1(string.Join("", arr)).ToLower() + "&timestamp=" + timestamp + "&nonce=" + timestamp;
        //    }
        //    if (!string.IsNullOrEmpty(paramstr))
        //    {
        //        if (apiurl.IndexOf('?') > 0)
        //        {
        //            apiurl += "&" + paramstr;
        //        }
        //        else
        //        {
        //            apiurl += "?" + paramstr;
        //        }
        //    }
        //    else if (apiurl.IndexOf('?') > 0)
        //    {
        //        apiurl += "&content=" + content + "&accountid=" + accountid + "&firstid=" + firstid + "&openid=" + clientUser;
        //    }
        //    else
        //    {
        //        apiurl += "?content=" + content + "&accountid=" + accountid + "&firstid=" + firstid + "&openid=" + clientUser;
        //    }
        //    System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(apiurl);
        //    request.Method = "POST";
        //    System.Net.HttpWebResponse response = null;
        //    System.Text.Encoding encode = System.Text.Encoding.UTF8;
        //    try
        //    {
        //        StreamWriter swRequestWriter = new StreamWriter(request.GetRequestStream());
        //        swRequestWriter.Write(sb.ToString());
        //        try
        //        {
        //            if (swRequestWriter != null)
        //                swRequestWriter.Close();
        //        }
        //        catch { }
        //        response = (System.Net.HttpWebResponse)request.GetResponse();
        //        using (StreamReader reader = new StreamReader(response.GetResponseStream(), encode))
        //        {
        //            string msg = reader.ReadToEnd();
        //            if (!string.IsNullOrEmpty(setfirstid))
        //            {
        //                msg = msg.Replace(setfirstid, firstid);
        //            }
        //            return msg;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return "Sorry，获取数据失败！";
        //    }
        //    finally
        //    {
        //        if (response != null)
        //            response.Close();
        //    }
        //}


        //private static string _ApiUrl;
        //public static string ApiUrl
        //{
        //    get
        //    {
        //        if (string.IsNullOrEmpty(_ApiUrl))
        //        {
        //            if (HttpContext.Current.Request.Url.Port == 80)
        //            {
        //                _ApiUrl = "http://" + HttpContext.Current.Request.Url.Host;
        //            }
        //            else
        //            {
        //                _ApiUrl = "http://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port;
        //            }
        //        }
        //        return _ApiUrl;
        //    }
        //}
        

    //    /// <summary>
    //    /// 输出内容
    //    /// </summary>
    //    /// <param name="msg"></param>
    //    public static void ResponseMsg(Hub.HubThread s, string msg, string clientUser, string serverUser)
    //    {
    //        XmlDocument doc = new XmlDocument();
    //        try
    //        {
    //            doc.LoadXml(msg);
    //            if (doc.InnerText.Length < 50)
    //            {
    //                doc = null;
    //            }
    //        }
    //        catch
    //        {
    //            doc = null;
    //        }
    //        if (doc == null)
    //        {
    //            StringBuilder sb = new StringBuilder();
    //            sb.AppendFormat("<xml>");                
    //            sb.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", clientUser);
    //            sb.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", serverUser);
    //            sb.AppendFormat("<CreateTime>{0}</CreateTime>", DateTime.Now.Ticks);
    //            sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
    //            sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", strUtil.RemoveHtmlTag(msg));
    //            sb.AppendFormat("<FuncFlag>0</FuncFlag>");
    //            sb.AppendFormat("</xml>");
    //            s.Output = sb.ToString();
    //        }
    //        else
    //        {
    //            s.Output = doc.InnerXml;
    //        }
    //    }

    //    /// <summary>
    //    /// 回复文本内容
    //    /// </summary>
    //    /// <param name="to">接收者</param>
    //    /// <param name="clientUser">消息来源</param>
    //    /// <param name="content">消息内容</param>
    //    /// <returns>生成的输出文本</returns>
    //    public static void ResponseTextMsg(Hub.HubThread s, string content, string clientUser, string serverUser)
    //    {
    //        StringBuilder sb = new StringBuilder();
    //        sb.AppendFormat("<xml>");
    //        sb.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", clientUser);
    //        sb.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", serverUser);
    //        sb.AppendFormat("<CreateTime>{0}</CreateTime>", DateTime.Now.Ticks);
    //        sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
    //        sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", strUtil.RemoveHtmlTag(content));
    //        sb.AppendFormat("<FuncFlag>0</FuncFlag>");
    //        sb.AppendFormat("</xml>");
    //        s.Output = sb.ToString();
    //    }
    //    /// <summary>
    //    /// 回复图文内容
    //    /// </summary>
    //    /// <param name="to">接收者</param>
    //    /// <param name="from">消息来源</param>
    //    /// <param name="content">消息内容</param>
    //    /// <returns>生成的输出文本</returns>
    //    public static void ResponsePicTextMsg(Hub.HubThread s, List<RuleContent> articles, string clientUser, string serverUser)
    //    {
    //        if (articles == null)
    //        {
    //            articles = new List<RuleContent>();
    //        }
    //        int count = 0;
    //        StringBuilder sbItems = new StringBuilder();
    //        string UrlPx = Oss.DataUrl;
    //        if (string.IsNullOrEmpty(UrlPx))
    //        {
    //            if (string.IsNullOrEmpty(_ApiUrl))
    //            {
    //                if (s.ctx.Request.Url.Port == 80)
    //                {
    //                    _ApiUrl = "http://" + s.ctx.Request.Url.Host;
    //                }
    //                else
    //                {
    //                    _ApiUrl = "http://" + s.ctx.Request.Url.Host + ":" + s.ctx.Request.Url.Port;
    //                }
    //            }
    //            UrlPx = _ApiUrl;
    //        }
    //        foreach (RuleContent article in articles)
    //        {
    //            try
    //            {
    //                if (string.IsNullOrEmpty(article.Title) || string.IsNullOrEmpty(article.PicUrl))
    //                {
    //                    continue;
    //                }
    //                StringBuilder sbTemp = new StringBuilder();
    //                sbTemp.AppendFormat("<item>");
    //                sbTemp.AppendFormat("   <Title><![CDATA[{0}]]></Title>", article.Title);
    //                sbTemp.AppendFormat("   <Description><![CDATA[{0}]]></Description>", GetTextByHtml(article.TextContent));
    //                sbTemp.AppendFormat("   <PicUrl><![CDATA[{0}]]></PicUrl>", article.PicUrl.StartsWith("http://") ? article.PicUrl : UrlPx + article.PicUrl);
    //                if (string.IsNullOrEmpty(article.LinkUrl))
    //                {
    //                    sbTemp.AppendFormat("   <Url><![CDATA[{0}]]></Url>", "");
    //                }
    //                else
    //                {
    //                    if (!article.LinkUrl.Contains("openid="))
    //                    {
    //                        if (article.LinkUrl.Contains("?"))
    //                        {
    //                            article.LinkUrl += "&openid=" + clientUser;
    //                        }
    //                        else
    //                        {
    //                            article.LinkUrl += "?openid=" + clientUser;
    //                        }
    //                    }
    //                    sbTemp.AppendFormat("   <Url><![CDATA[{0}]]></Url>", article.LinkUrl.Contains("http://") ? article.LinkUrl : ApiUrl + article.LinkUrl);
    //                }
    //                sbTemp.AppendFormat("   <FuncFlag>0</FuncFlag>");
    //                sbTemp.AppendFormat("</item>");
    //                sbItems.Append(sbTemp.ToString());
    //                count++;
    //                //更新推送次数
    //                if (count == 9)
    //                {
    //                    break;
    //                }
    //            }
    //            catch { }
    //        }


    //        StringBuilder sb = new StringBuilder();
    //        sb.AppendFormat("<xml>");
    //        sb.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", clientUser);
    //        sb.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", serverUser);
    //        sb.AppendFormat("<CreateTime>{0}</CreateTime>", DateTime.Now.Ticks);
    //        sb.AppendFormat("<MsgType><![CDATA[news]]></MsgType>");
    //        sb.AppendFormat("<ArticleCount>{0}></ArticleCount>", count);
    //        sb.AppendFormat("<Articles>");
    //        sb.AppendFormat(sbItems.ToString());
    //        sb.AppendFormat("</Articles>");
    //        sb.AppendFormat("<FuncFlag>0</FuncFlag>");
    //        sb.AppendFormat("</xml>");
    //        s.Output = sb.ToString();
    //    }
    //    /// <summary>
    //    /// 回复音乐内容
    //    /// </summary>
    //    /// <param name="to">接收者</param>
    //    /// <param name="from">消息来源</param>
    //    /// <param name="title">标题</param>
    //    /// <param name="description">描述信息</param>
    //    /// <param name="musicurl">音乐链接</param>
    //    /// <param name="hqmusicurl">高质量音乐链接，WIFI环境优先使用该链接播放音乐</param>
    //    /// <returns>生成的输出文本</returns>
    //    public static void ResponseMusicMsg(Hub.HubThread s, string title, string description, string musicurl, string hqmusicurl, string clientUser, string serverUser)
    //    {
    //        if (string.IsNullOrEmpty(hqmusicurl))
    //        {
    //            hqmusicurl = musicurl;
    //        }
    //        StringBuilder sb = new StringBuilder();
    //        sb.AppendFormat("<xml>");
    //        sb.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", clientUser);
    //        sb.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", serverUser);
    //        sb.AppendFormat("<CreateTime>{0}</CreateTime>", DateTime.Now.Ticks);
    //        sb.AppendFormat("<MsgType><![CDATA[music]]></MsgType>");
    //        sb.AppendFormat("<Music>");
    //        sb.AppendFormat("   <Title><![CDATA[{0}]]></Title>", title);
    //        sb.AppendFormat("   <Description><![CDATA[{0}]]></Description>", description);
    //        sb.AppendFormat("   <MusicUrl><![CDATA[{0}]]></MusicUrl>", (!string.IsNullOrEmpty(musicurl) && musicurl.Contains("http://")) ? musicurl : Oss.DataUrl + musicurl);
    //        sb.AppendFormat("   <HQMusicUrl><![CDATA[{0}]]></HQMusicUrl>", (!string.IsNullOrEmpty(hqmusicurl) && hqmusicurl.Contains("http://")) ? hqmusicurl : Oss.DataUrl + hqmusicurl);
    //        sb.AppendFormat("   <FuncFlag>0</FuncFlag>");
    //        sb.AppendFormat("</Music>");
    //        sb.AppendFormat("</xml>");
    //        s.Output = sb.ToString();
    //    }
    //}


    ///// <summary>
    ///// 规则内容
    ///// </summary>
    //public class RuleContent
    //{
    //    private string _ContentType;
    //    /// <summary>
    //    /// 内容类型 text（文本）,pictext(图文) ,music（音乐）
    //    /// </summary>
    //    public string ContentType
    //    {
    //        get { return _ContentType; }
    //        set { _ContentType = value; }
    //    }
    //    private string _Title;
    //    /// <summary>
    //    /// 标题
    //    /// </summary>
    //    public string Title
    //    {
    //        get { return _Title; }
    //        set { _Title = value; }
    //    }
    //    private string _LinkUrl;
    //    /// <summary>
    //    /// 外链地址
    //    /// </summary>
    //    public string LinkUrl
    //    {
    //        get { return _LinkUrl; }
    //        set { _LinkUrl = value; }
    //    }
    //    private string _PicUrl;
    //    /// <summary>
    //    /// 图片外链地址
    //    /// </summary>
    //    public string PicUrl
    //    {
    //        get { return _PicUrl; }
    //        set { _PicUrl = value; }
    //    }
    //    private string _ThumbPicUrl;
    //    /// <summary>
    //    /// 小图外链地址
    //    /// </summary>
    //    public string ThumbPicUrl
    //    {
    //        get { return _ThumbPicUrl; }
    //        set { _ThumbPicUrl = value; }
    //    }
    //    private string _MusicUrl;
    //    /// <summary>
    //    /// 声音文件外链地址
    //    /// </summary>
    //    public string MusicUrl
    //    {
    //        get { return _MusicUrl; }
    //        set { _MusicUrl = value; }
    //    }
    //    private string _TextContent;
    //    /// <summary>
    //    /// 文本内容
    //    /// </summary>
    //    public string TextContent
    //    {
    //        get { return _TextContent; }
    //        set { _TextContent = value; }
    //    }
    //    private int _PushCount;
    //    /// <summary>
    //    /// 推送次数
    //    /// </summary>
    //    public int PushCount
    //    {
    //        get { return _PushCount; }
    //        set { _PushCount = value; }
    //    }

    //    private DateTime _LastStick;
    //    /// <summary>
    //    /// 最后置顶的时间
    //    /// </summary>
    //    public DateTime LastStick
    //    {
    //        get { return _LastStick; }
    //        set { _LastStick = value; }
    //    }

    }
}