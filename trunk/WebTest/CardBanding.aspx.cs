using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using GotDotNet.ApplicationBlocks.Data;

namespace WebTest
{
    public partial class CardBanding : System.Web.UI.Page
    {
        //private log4net.ILog logger = log4net.LogManager.GetLogger(typeof (Page));
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private System.Text.Encoding encode = System.Text.Encoding.UTF8;
        private string SQLConn = ConfigurationManager.ConnectionStrings["etcConnString"].ToString().Trim();
        private string token = ConfigurationManager.AppSettings["token"].ToString().Trim();

        protected AshxHelper helper = new AshxHelper(HttpContext.Current);

        private byte[] iv = new byte[] {0x12, 0x34, 0x56, 120, 0x90, 0xab, 0xcd, 0xef};
        private byte[] key = Encoding.ASCII.GetBytes("WikeSoft");

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //try
                //{

                    logger.Info("辽通卡绑定----DO=" + helper.GetParam("do") + "|name=" + helper.GetParam("name").Trim() +
                                "|password=" + helper.GetParam("password").Trim() + "|openid=" +
                                helper.GetParam("openid").Trim());
                    if (helper.GetParam("do").ToLower() == "add")
                    {
                        logger.Info("辽通卡绑定----DO=" + helper.GetParam("do") + "|message=" +
                                    helper.GetParam("message").Trim() +
                                    "|openid=" + openid.Value.ToString().Trim());
                        string str = helper.GetParam("message").Trim().ToUpper();
                        string cn = helper.GetParam("name").Trim();
                        string _openid = helper.GetParam("openid").Trim();
                        if (cn.Length == 20)
                        {
                            cn = cn.Substring(4);
                        }
                        string pd = helper.GetParam("password").Trim();
                        if ((str == "") || (str.ToUpper() != base.Session["CheckCode"].ToString().ToUpper()))
                        {
                            helper.Result.Add("亲，你验证码输入错误,请重新输入!");
                            logger.Info("辽通卡绑定失败---" + helper.Result.ErrorsText);
                        }
                        else if (cn == "")
                        {
                            helper.Result.Add("亲，辽通卡卡号不能为空,请重新输入!");
                            logger.Info("辽通卡绑定失败---" + helper.Result.ErrorsText);
                        }
                        else if (pd == "")
                        {
                            helper.Result.Add("亲，密码不能为空,请重新输入!");
                            logger.Info("辽通卡绑定失败---" + helper.Result.ErrorsText);
                        }
                        else
                        {
                            logger.Info("辽通卡绑定---GetDataByCNAndPd|cn=" + cn + "|pd=" + pd);
                            DataTable table = GetDataByCNAndPd(cn, pd);
                          
                            if (table == null)
                            {
                                helper.Result.Add("亲，你输入的辽通卡卡号或密码错误!,请核对后重新输入!");
                                logger.Info("辽通卡绑定失败---" + helper.Result.ErrorsText);
                            }
                            else
                            {
                                int WeiXinCard = Convert.ToInt32(SqlHelper.ExecuteScalar(SQLConn, CommandType.Text,
                                  "select count(1) from WeiXinCard where Card_NO=@Card_NO",
                                  new SqlParameter("@Card_NO", cn)));
                                if(WeiXinCard>0)
                                {
                                    helper.Result.Add("亲，你输入的辽通卡卡号已经与其它微信绑定，如想更改绑定，请先解除原有的绑定!");
                                    logger.Info("辽通卡绑定失败---" + helper.Result.ErrorsText);
                                    helper.ResponseResult();
                                    return;
                                }

                                string AccountNo = table.Rows[0]["ACCOUNT_NO"].ToString();
                                string CardNo = table.Rows[0]["CARD_NO"].ToString();
                                try
                                {
                                    SqlParameter[] prasParameters = new SqlParameter[]
                                    {
                                        new SqlParameter("@ID", Guid.NewGuid().ToString()),
                                        new SqlParameter("@WXOpenID", _openid),
                                        new SqlParameter("@Card_NO", CardNo),
                                        new SqlParameter("@Account_NO", AccountNo),
                                        new SqlParameter("@Token",
                                            ConfigurationManager.AppSettings["token"].ToString().Trim())
                                    };
                                    SqlHelper.ExecuteNonQuery(SQLConn, CommandType.Text,
                                        "insert into WeiXinCard ([ID],[WXOpenID],[Card_NO],[Account_NO],[Token]) values (@ID,@WXOpenID,@Card_NO,@Account_NO,@Token)",
                                        prasParameters
                                        );

                                }
                                catch
                                {
                                    helper.Result.Add("亲，你输入的用户名或密码错误!,请核对后重新输入!");
                                    logger.Info("辽通卡绑定失败---" + helper.Result.ErrorsText);
                                }

                            }
                        }
                        helper.ResponseResult();
                        logger.Info("辽通卡绑定成功---name=" + helper.GetParam("name").Trim() + "|password=" +
                                    helper.GetParam("password").Trim());
                    }
                    else if (helper.GetParam("do").ToLower() == "first")
                    {
                        timestamp.Value = helper.GetParam("timestamp").ToString();
                        openid.Value = helper.GetParam("openid").Trim();
                        firstid.Value = helper.GetParam("firstid").ToString();
                        int WeiXinCard = Convert.ToInt32(SqlHelper.ExecuteScalar(SQLConn, CommandType.Text,
                            "select count(1) from WeiXinCard where WXOpenID=@openid",
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
                        logger.Info("辽通卡绑定输入参数---" + msg);
                        Response.Clear();
                        XmlDocument doc = new XmlDocument();
                        try
                        {
                            doc.LoadXml(msg);
                           // timestamp.Value = helper.GetParam("timestamp").ToString();
                            openid.Value = doc.GetElementsByTagName("FromUserName")[0].InnerText.Trim();
                            firstid.Value = doc.GetElementsByTagName("ToUserName")[0].InnerText.Trim();

                            int WeiXinCard = Convert.ToInt32(SqlHelper.ExecuteScalar(SQLConn, CommandType.Text,
                                "select count(1)  from WeiXinCard where WXOpenID=@openid ",
                                new SqlParameter("@openid", openid.Value)));
                            if (WeiXinCard > 0)
                            {
                                WeiXinCard = Convert.ToInt32(SqlHelper.ExecuteScalar(SQLConn, CommandType.Text,
                                    "delete  from WeiXinCard where WXOpenID=@openid ",
                                    new SqlParameter("@openid", openid.Value)));

                                StringBuilder sb = new StringBuilder();
                                sb.AppendFormat("<xml>");
                                sb.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", openid.Value);
                                sb.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", firstid.Value);
                                sb.AppendFormat("<CreateTime>{0}</CreateTime>", Senparc.Weixin.MP.Helpers.DateTimeHelper.GetWeixinDateTime(DateTime.Now));
                                sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                                sb.AppendFormat("<Content><![CDATA[{0}]]></Content>",
                                    strUtil.RemoveHtmlTag("您已经成功解除与辽通卡的绑定！"));
                                sb.AppendFormat("<FuncFlag>0</FuncFlag>");
                                sb.AppendFormat("</xml>");
                                logger.Info("辽通卡绑定返回---" + sb.ToString());
                                Response.Write(sb.ToString());
                            }
                            else
                            {
                                StringBuilder sb = new StringBuilder();
                                sb.AppendFormat("<xml>");
                                sb.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", openid.Value);
                                sb.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", firstid.Value);
                                sb.AppendFormat("<CreateTime>{0}</CreateTime>", Senparc.Weixin.MP.Helpers.DateTimeHelper.GetWeixinDateTime(DateTime.Now));
                                sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                                sb.AppendFormat("<Content><![CDATA[{0}]]></Content>",
                                    strUtil.RemoveHtmlTag("您的微信没有绑定辽通卡！"));
                                sb.AppendFormat("<FuncFlag>0</FuncFlag>");
                                sb.AppendFormat("</xml>");
                                logger.Info("辽通卡绑定返回---" + sb.ToString());
                                Response.Write(sb.ToString());
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
                    else
                    {
                        using (StreamReader reader = new StreamReader(Request.InputStream, encode))
                        {
                            string msg = reader.ReadToEnd();
                            logger.Info("辽通卡绑定输入参数---" + msg);
                            Response.Clear();
                            XmlDocument doc = new XmlDocument();
                            try
                            {
                                doc.LoadXml(msg);
                                openid.Value = doc.GetElementsByTagName("FromUserName")[0].InnerText.Trim();
                                firstid.Value = doc.GetElementsByTagName("ToUserName")[0].InnerText.Trim();

                                int WeiXinCard = Convert.ToInt32(SqlHelper.ExecuteScalar(SQLConn, CommandType.Text,
                                    "select count(1) from WeiXinCard where WXOpenID=@openid",
                                    new SqlParameter("@openid", openid.Value)));

                                if (WeiXinCard > 0)
                                {
                                    StringBuilder sb = new StringBuilder();
                                    sb.AppendFormat("<xml>");
                                    sb.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", openid.Value);
                                    sb.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", firstid.Value);
                                    sb.AppendFormat("<CreateTime>{0}</CreateTime>", Senparc.Weixin.MP.Helpers.DateTimeHelper.GetWeixinDateTime(DateTime.Now));
                                    sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                                    sb.AppendFormat("<Content><![CDATA[{0}]]></Content>",
                                        strUtil.RemoveHtmlTag(
                                            ConfigurationManager.AppSettings["BandedMsg"].ToString().Trim()));
                                    sb.AppendFormat("<FuncFlag>0</FuncFlag>");
                                    sb.AppendFormat("</xml>");
                                    logger.Info("辽通卡绑定返回---" + sb.ToString());
                                    Response.Write(sb.ToString());
                                }
                                else
                                {

                                    StringBuilder sb = new StringBuilder();
                                    sb.AppendFormat("<xml>");
                                    sb.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", openid.Value);
                                    sb.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", firstid.Value);
                                    sb.AppendFormat("<CreateTime>{0}</CreateTime>", Senparc.Weixin.MP.Helpers.DateTimeHelper.GetWeixinDateTime(DateTime.Now));
                                    sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                                    sb.AppendFormat("<Content><![CDATA[{0}]]></Content>",
                                        "　　欢迎您绑定辽通卡，轻松绑定后即可享受以下贴心服务：\n1.查询帐户信息。\n2.查询通行记录。\n\n" +
                                        "<a href='http://218.25.53.5:8081/wechat/CardBanding.aspx?do=first&openid=" +
                                        openid.Value + "'>点击这里，立即绑定</a>");
                                    //sb.AppendFormat("<Content><![CDATA[{0}]]></Content>",
                                    //    strUtil.RemoveHtmlTag(
                                    //        ConfigurationManager.AppSettings["BandedMsg"].ToString().Trim()));
                                    sb.AppendFormat("<FuncFlag>0</FuncFlag>");
                                    sb.AppendFormat("</xml>");
                                    logger.Info("辽通卡绑定返回---" + sb.ToString());
                                    Response.Write(sb.ToString());
                                    //Response.End();

                                    //string aaa = "<xml>" +
                                    //             "<ToUserName><![CDATA[" + openid.Value + "]]></ToUserName>" +
                                    //             "<FromUserName><![CDATA[" + firstid.Value + "]]></FromUserName>" +
                                    //             "<CreateTime>" + DateTime.Now.Ticks + "</CreateTime>" +
                                    //             "<MsgType><![CDATA[news]]></MsgType>" +
                                    //             "<ArticleCount>1</ArticleCount>" +
                                    //             "<Articles>" +
                                    //             "<item><Title><![CDATA[点击绑定辽通卡]]></Title>" +
                                    //             "<Description><![CDATA[]]></Description>" +
                                    //             "<PicUrl><![CDATA[http://sht.cntx.net/welcome.png]]></PicUrl>" +
                                    //             "<Url><![CDATA[http://218.25.53.5:8081/wechat/CardBanding.aspx?do=first&openid=" +
                                    //             openid.Value + "]]></Url>" +
                                    //             "<FuncFlag>0</FuncFlag>" +
                                    //             "</item></Articles>" +
                                    //             "<FuncFlag>0</FuncFlag>" +
                                    //             "</xml>";
                                    //logger.Info("辽通卡绑定返回---" + aaa);
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
                //    logger.Info("辽通卡绑定失败---" + helper.Result.ErrorsText);
                //    helper.ResponseResult();
                //}
                //finally
                //{
                //    if (helper.GetParam("do").ToLower() == "")
                //        Response.End();
                //}
            }

        }

        public object FilterStr(object o)
        {
            return o;
        }


        public DataTable GetDataByCNAndPd(string cn, string pd)
        {
            //获取别名以及密码等
            logger.Info("辽通卡绑定失败---GetOtherNameAndOtherpd");
            DataTable OthDt = GetOtherNameAndOtherpd(cn);
            if ((OthDt == null ? 0 : OthDt.Rows.Count) > 0)
            {
                if (OthDt.Rows[0]["PASS_WORD"].ToString() == DESEncrypt(pd))
                {
                    return OthDt;
                }
            }
            logger.Info("辽通卡绑定失败---GetCardNoAndOtherpd");
            //获取卡号以及密码等
            DataTable CardhDt = GetCardNoAndOtherpd(cn);
            if ((CardhDt == null ? 0 : CardhDt.Rows.Count) > 0)
            {
                if (CardhDt.Rows[0]["PASS_WORD"].ToString() == DESEncrypt(pd))
                {
                    return CardhDt;
                }
            }

            StringBuilder builder = new StringBuilder();
            //增加验证登录名简称为卡号长度相同时报错问题
            if (cn.Length == 0x10 && Regex.IsMatch(cn, @"^\d+$"))
            {
                //增加判断别名是否与卡号相同
                if (CardName(cn))
                {
                    pd = DESEncrypt(pd);
                    builder.AppendFormat("SELECT * FROM USER_PASSWORD  WHERE LOGON_NAME=@cn AND PASS_WORD=@an",
                        new object[0]);
                }
                else
                {
                    //2013-11-29 新增限定无效的卡号+帐号后6位登录
                    builder.AppendFormat(
                        "SELECT TOP 1 B.[USER_NAME] ,CARD_NO ,B.ACCOUNT_NO ,CARD_TYPE ,PRIMARY_CARD ,CARD_STATUS \r\n\t            FROM LN_ETC_QUERY_NEW.dbo.CARD_ACCOUNT AS A\r\n\t            INNER JOIN LN_ETC_QUERY_NEW.dbo.A_ACCOUNT AS B ON A.ACCOUNT_NO =B.ACCOUNT_NO \r\n\t            WHERE A.CARD_NO=@cn AND RIGHT(B.ACCOUNT_NO,6) = @an AND CARD_STATUS IN (3,6,5) ORDER BY ID DESC ",
                        new object[0]);
                }
            }
            else
            {
                pd = DESEncrypt(pd);
                builder.AppendFormat("SELECT * FROM USER_PASSWORD  WHERE LOGON_NAME=@cn AND PASS_WORD=@an",
                    new object[0]);
            }
            logger.Info("辽通卡绑定失败---builder" + builder.ToString());
            SqlParameter[] prams = new SqlParameter[]
            {new SqlParameter("@cn", SqlDbType.VarChar, 20), new SqlParameter("@an", SqlDbType.VarChar, 20)};
            prams[0].Value = cn;
            prams[1].Value = pd;
            DataSet table = SqlHelper.ExecuteDataset(SQLConn, CommandType.Text, builder.ToString(), prams);
            if (table==null || table.Tables.Count==0|| table.Tables[0].Rows.Count == 0)
            {
                return null;
            }
            return table.Tables[0];
        }

        public DataTable GetOtherNameAndOtherpd(string cn)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SELECT * FROM USER_PASSWORD  WHERE LOGON_NAME=@cn", new object[0]);
            SqlParameter[] prams = new SqlParameter[] {new SqlParameter("@cn", SqlDbType.VarChar, 20)};
            prams[0].Value = cn;
            DataTable table = SqlHelper.ExecuteDataset(SQLConn, CommandType.Text, builder.ToString(), prams).Tables[0];
            if (table.Rows.Count == 0)
            {
                return null;
            }
            return table;
        }


        public DataTable GetCardNoAndOtherpd(string cn)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SELECT * FROM USER_PASSWORD  WHERE CARD_NO=@cn", new object[0]);
            SqlParameter[] prams = new SqlParameter[] {new SqlParameter("@cn", SqlDbType.VarChar, 20)};
            prams[0].Value = cn;
            DataTable table = SqlHelper.ExecuteDataset(SQLConn, CommandType.Text, builder.ToString(), prams).Tables[0];
            if (table.Rows.Count == 0)
            {
                return null;
            }
            return table;
        }

        /// <summary>
        /// 判断别名中是否存在登录别名等于卡号的记录
        /// </summary>
        /// <param name="sqlhelper"></param>
        /// <param name="CardNo"></param>
        /// <returns></returns>
        private bool CardName(string CardNo)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SELECT * FROM [LN_ETC_WEB_NEW].[dbo].[USER_PASSWORD]  WHERE [CARD_NO]=@CARD_NO",
                new object[0]);
            SqlParameter[] prams = new SqlParameter[] {new SqlParameter("@CARD_NO", SqlDbType.Char, 16)};
            prams[0].Value = CardNo;
            DataTable bmdt = new DataTable();
            bmdt = SqlHelper.ExecuteDataset(SQLConn, CommandType.Text, builder.ToString(), prams).Tables[0];

            if (bmdt.Rows.Count == 1)
            {
                if (bmdt.Rows[0]["LOGON_NAME"].ToString() == CardNo)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public string DESEncrypt(string inputString)
        {
            MemoryStream stream = null;
            CryptoStream stream2 = null;
            StreamWriter writer = null;
            string str;
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            try
            {
                stream = new MemoryStream();
                stream2 = new CryptoStream(stream, provider.CreateEncryptor(key, iv), CryptoStreamMode.Write);
                writer = new StreamWriter(stream2);
                writer.Write(inputString);
                writer.Flush();
                stream2.FlushFinalBlock();
                str = Convert.ToBase64String(stream.GetBuffer(), 0, (int) stream.Length);
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
                if (stream2 != null)
                {
                    stream2.Close();
                }
                if (stream != null)
                {
                    stream.Close();
                }
            }
            return str;
        }

        public bool CheckUserRegiste(string an, string cn)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SELECT * FROM USER_PASSWORD WHERE ACCOUNT_NO=@ACCOUNT_NO AND CARD_NO=@CARD_NO",
                new object[0]);
            SqlParameter[] prams = new SqlParameter[]
            {new SqlParameter("@ACCOUNT_NO", SqlDbType.Char, 14), new SqlParameter("@CARD_NO", SqlDbType.Char, 0x10)};
            prams[0].Value = an;
            prams[1].Value = cn;
            DataTable table = new DataTable();
            table = SqlHelper.ExecuteDataset(SQLConn, CommandType.Text, builder.ToString(), prams).Tables[0];
            return (table.Rows.Count > 0);
        }
    }
}