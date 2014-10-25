using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using GotDotNet.ApplicationBlocks.Data;

namespace HappyLibrary
{
    public partial class Regist : System.Web.UI.Page
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected AshxHelper helper = new AshxHelper(HttpContext.Current);
        private string SQLConn = ConfigurationManager.ConnectionStrings["etcConnString"].ToString().Trim();
        private System.Text.Encoding encode = System.Text.Encoding.UTF8;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //try
                //{

                logger.Info("图书证绑定----DO=" + helper.GetParam("do") + "|name=" + helper.GetParam("name").Trim() +
                            "|password=" + helper.GetParam("password").Trim() + "|openid=" +
                            helper.GetParam("openid").Trim());
                if (helper.GetParam("do").ToLower() == "add")
                {
                    logger.Info("图书证绑定----DO=" + helper.GetParam("do") + "|password=" +
                                helper.GetParam("password").Trim() +
                                "|openid=" + openid.Value.ToString().Trim());
                    string str = helper.GetParam("password").Trim().ToUpper();
                    string cn = helper.GetParam("name").Trim();
                    string _openid = helper.GetParam("openid").Trim();
                    
                    if ((str == "") || (str.ToUpper() != base.Session["CheckCode"].ToString().ToUpper()))
                    {
                        helper.Result.Add("亲，你验证码输入错误,请重新输入!");
                        logger.Info("图书证绑定失败---" + helper.Result.ErrorsText);
                    }
                    else if (cn == ""||cn.Length!=11)
                    {
                        helper.Result.Add("亲，手机号不正确,请重新输入!");
                        logger.Info("图书证绑定失败---" + helper.Result.ErrorsText);
                    }
                    else
                    {
                        logger.Info("图书证绑定---GetDataByCNAndPd|cn=" + cn );
                       
                        int user = Convert.ToInt32(SqlHelper.ExecuteScalar(SQLConn, CommandType.Text,
                             "select count(1) from Member where Tel=@Tel",
                             new SqlParameter("@Tel", cn)));
                        if (user == 0)
                        {
                            helper.Result.Add("亲，你输入的手机号系统中不存在!,请核对后重新输入!");
                            logger.Info("图书证绑定失败---" + helper.Result.ErrorsText);
                        }
                        else
                        {
                            int WeiXinCard = Convert.ToInt32(SqlHelper.ExecuteScalar(SQLConn, CommandType.Text,
                              "select count(1) from Member where Tel=@Tel and WeixinID=@WeixinID",
                              new SqlParameter("@Tel", cn), new SqlParameter("@WeixinID", _openid)));
                            if (WeiXinCard > 0)
                            {
                                helper.Result.Add("亲，你输入的图书证卡号已经与其它微信绑定，如想更改绑定，请先解除原有的绑定!");
                                logger.Info("图书证绑定失败---" + helper.Result.ErrorsText);
                                helper.ResponseResult();
                                return;
                            }
                            try
                            {
                                SqlParameter[] prasParameters = new SqlParameter[]
                                    {
                                        new SqlParameter("@Tel", cn),
                                        new SqlParameter("@WeixinID", _openid)
                                    };
                                SqlHelper.ExecuteNonQuery(SQLConn, CommandType.Text,
                                    "update Member  set WeixinID=@WeixinID where Tel=@Tel",
                                    prasParameters
                                    );

                            }
                            catch
                            {
                                helper.Result.Add("亲，你输入的手机号码错误!,请核对后重新输入!");
                                logger.Info("图书证绑定失败---" + helper.Result.ErrorsText);
                            }

                        }
                    }
                    helper.ResponseResult();
                    logger.Info("图书证绑定成功---name=" + helper.GetParam("name").Trim() + "|password=" +
                                helper.GetParam("password").Trim());
                }
                else if (helper.GetParam("do").ToLower() == "first")
                {
                    timestamp.Value = helper.GetParam("timestamp").ToString();
                    openid.Value = helper.GetParam("openid").Trim();
                    firstid.Value = helper.GetParam("firstid").ToString();
                    int WeiXinCard = Convert.ToInt32(SqlHelper.ExecuteScalar(SQLConn, CommandType.Text,
                        "select count(1) from Member where WeixinID=@openid",
                        new SqlParameter("@openid", openid.Value)));

                    if (WeiXinCard > 0)
                    {
                        pnbanding.Visible = false;
                        pninfo.Visible = true;
                    }
                }
                else if (helper.GetParam("do").ToLower() == "del")
                {
                    string msg = string.Empty;
                    using (StreamReader reader = new StreamReader(Request.InputStream, encode))
                    {
                        msg = reader.ReadToEnd();
                    }
                    logger.Info("图书证绑定输入参数---" + msg);
                    Response.Clear();
                    XmlDocument doc = new XmlDocument();
                    try
                    {
                        doc.LoadXml(msg);
                        // timestamp.Value = helper.GetParam("timestamp").ToString();
                        openid.Value = doc.GetElementsByTagName("FromUserName")[0].InnerText.Trim();
                        firstid.Value = doc.GetElementsByTagName("ToUserName")[0].InnerText.Trim();

                        int WeiXinCard = Convert.ToInt32(SqlHelper.ExecuteScalar(SQLConn, CommandType.Text,
                            "select count(1)  from Member where WeixinID=@openid ",
                            new SqlParameter("@openid", openid.Value)));
                        if (WeiXinCard > 0)
                        {
                            WeiXinCard = Convert.ToInt32(SqlHelper.ExecuteScalar(SQLConn, CommandType.Text,
                                "update  WeiXinCard set WXOpenID=''   where WXOpenID=@openid ",
                                new SqlParameter("@openid", openid.Value)));

                            StringBuilder sb = new StringBuilder();
                            sb.AppendFormat("<xml>");
                            sb.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", openid.Value);
                            sb.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", firstid.Value);
                            sb.AppendFormat("<CreateTime>{0}</CreateTime>", Senparc.Weixin.Helpers.DateTimeHelper.GetWeixinDateTime(DateTime.Now));
                            sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                            sb.AppendFormat("<Content><![CDATA[{0}]]></Content>",
                                strUtil.RemoveHtmlTag("您已经成功解除与图书证的绑定！"));
                            sb.AppendFormat("<FuncFlag>0</FuncFlag>");
                            sb.AppendFormat("</xml>");
                            logger.Info("图书证绑定返回---" + sb.ToString());
                            Response.Write(sb.ToString());
                        }
                        else
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.AppendFormat("<xml>");
                            sb.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", openid.Value);
                            sb.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", firstid.Value);
                            sb.AppendFormat("<CreateTime>{0}</CreateTime>", Senparc.Weixin.Helpers.DateTimeHelper.GetWeixinDateTime(DateTime.Now));
                            sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                            sb.AppendFormat("<Content><![CDATA[{0}]]></Content>",
                                strUtil.RemoveHtmlTag("您的微信没有绑定图书证！"));
                            sb.AppendFormat("<FuncFlag>0</FuncFlag>");
                            sb.AppendFormat("</xml>");
                            logger.Info("图书证绑定返回---" + sb.ToString());
                            Response.Write(sb.ToString());
                        }
                    }

                    catch (Exception ex)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendFormat(strUtil.RemoveHtmlTag("访问来源不明，请从微信共众平台访问。"+ex.Message));
                        logger.Info("访问来源不明，请从微信共众平台访问。" + sb.ToString());
                        Response.Write(sb.ToString());

                    }
                    finally
                    {
                        Response.End();
                    }
                }
                else
                {
                    using (StreamReader reader = new StreamReader(Request.InputStream, encode))
                    {
                        string msg = reader.ReadToEnd();
                        logger.Info("图书证绑定输入参数---" + msg);
                        Response.Clear();
                        XmlDocument doc = new XmlDocument();
                        try
                        {
                            doc.LoadXml(msg);
                            openid.Value = doc.GetElementsByTagName("FromUserName")[0].InnerText.Trim();
                            firstid.Value = doc.GetElementsByTagName("ToUserName")[0].InnerText.Trim();

                            int WeiXinCard = Convert.ToInt32(SqlHelper.ExecuteScalar(SQLConn, CommandType.Text,
                                "select count(1) from Member where WeixinID=@openid",
                                new SqlParameter("@openid", openid.Value)));

                            if (WeiXinCard > 0)
                            {
                                StringBuilder sb = new StringBuilder();
                                sb.AppendFormat("<xml>");
                                sb.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", openid.Value);
                                sb.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", firstid.Value);
                                sb.AppendFormat("<CreateTime>{0}</CreateTime>", Senparc.Weixin.Helpers.DateTimeHelper.GetWeixinDateTime(DateTime.Now));
                                sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                                sb.AppendFormat("<Content><![CDATA[{0}]]></Content>",
                                    strUtil.RemoveHtmlTag(
                                        ConfigurationManager.AppSettings["BandedMsg"].ToString().Trim()));
                                sb.AppendFormat("<FuncFlag>0</FuncFlag>");
                                sb.AppendFormat("</xml>");
                                logger.Info("图书证绑定返回---" + sb.ToString());
                                Response.Write(sb.ToString());
                            }
                            else
                            {

                                StringBuilder sb = new StringBuilder();
                                sb.AppendFormat("<xml>");
                                sb.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", openid.Value);
                                sb.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", firstid.Value);
                                sb.AppendFormat("<CreateTime>{0}</CreateTime>", Senparc.Weixin.Helpers.DateTimeHelper.GetWeixinDateTime(DateTime.Now));
                                sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                                sb.AppendFormat("<Content><![CDATA[{0}]]></Content>",
                                    "　　欢迎您绑定图书证，轻松绑定后即可享受以下贴心服务：\n1.查询借阅记录。\n2.预约借书。\n3.预约还书。\n\n" +
                                    "<a href='http://happylibrary.x7.com/regist.aspx?do=first&openid=" +
                                    openid.Value + "'>点击这里，立即绑定</a>");
                                //sb.AppendFormat("<Content><![CDATA[{0}]]></Content>",
                                //    strUtil.RemoveHtmlTag(
                                //        ConfigurationManager.AppSettings["BandedMsg"].ToString().Trim()));
                                sb.AppendFormat("<FuncFlag>0</FuncFlag>");
                                sb.AppendFormat("</xml>");
                                logger.Info("图书证绑定返回---" + sb.ToString());
                                Response.Write(sb.ToString());
                                //Response.End();

                                //string aaa = "<xml>" +
                                //             "<ToUserName><![CDATA[" + openid.Value + "]]></ToUserName>" +
                                //             "<FromUserName><![CDATA[" + firstid.Value + "]]></FromUserName>" +
                                //             "<CreateTime>" + DateTime.Now.Ticks + "</CreateTime>" +
                                //             "<MsgType><![CDATA[news]]></MsgType>" +
                                //             "<ArticleCount>1</ArticleCount>" +
                                //             "<Articles>" +
                                //             "<item><Title><![CDATA[点击绑定图书证]]></Title>" +
                                //             "<Description><![CDATA[]]></Description>" +
                                //             "<PicUrl><![CDATA[http://sht.cntx.net/welcome.png]]></PicUrl>" +
                                //             "<Url><![CDATA[http://218.25.53.5:8081/wechat/CardBanding.aspx?do=first&openid=" +
                                //             openid.Value + "]]></Url>" +
                                //             "<FuncFlag>0</FuncFlag>" +
                                //             "</item></Articles>" +
                                //             "<FuncFlag>0</FuncFlag>" +
                                //             "</xml>";
                                //logger.Info("图书证绑定返回---" + aaa);
                                //Response.Write(aaa);
                                // Response.End();
                            }
                        }
                        catch (Exception)
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.AppendFormat(strUtil.RemoveHtmlTag("访问来源不明，请从微信共众平台访问。"));
                            logger.Info("访问来源不明，请从微信共众平台访问。" + sb.ToString());
                            Response.Write(sb.ToString());

                        }
                        finally
                        {
                            Response.End();
                        }
                    }
                }
                //}
                //catch (Exception ex)
                //{
                //    helper.Result.Add(ex.Message);
                //    logger.Info("图书证绑定失败---" + helper.Result.ErrorsText);
                //    helper.ResponseResult();
                //}
                //finally
                //{
                //    if (helper.GetParam("do").ToLower() == "")
                //        Response.End();
                //}
            }
        }
    }
}