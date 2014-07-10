using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using GotDotNet.ApplicationBlocks.Data;

namespace WebTest
{
    public partial class DisplayAccountInfo : System.Web.UI.Page
    {
        private System.Text.Encoding encode = System.Text.Encoding.UTF8;
        private string SQLConn = ConfigurationManager.ConnectionStrings["etcConnString"].ToString().Trim();
        private string token = ConfigurationManager.AppSettings["token"].ToString().Trim();
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            logger.Info("开始调用");
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
               string openid = doc.GetElementsByTagName("FromUserName")[0].InnerText.Trim();
               string firstid = doc.GetElementsByTagName("ToUserName")[0].InnerText.Trim();

                DataSet ds = SqlHelper.ExecuteDataset(SQLConn, CommandType.Text,
                    "select Card_NO,Account_NO  from WeiXinCard where WXOpenID=@openid ",
                    new SqlParameter("@openid", openid));
                if (ds==null || ds.Tables.Count==0 ||ds.Tables[0].Rows.Count==0)
                {
                    logger.Info("您未与辽通卡进行绑定，请绑定后再进行此操作.");
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("<xml>");
                    sb.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", openid);
                    sb.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", firstid);
                    sb.AppendFormat("<CreateTime>{0}</CreateTime>", Senparc.Weixin.MP.Helpers.DateTimeHelper.GetWeixinDateTime(DateTime.Now));
                    sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                    sb.AppendFormat("<Content><![CDATA[{0}]]></Content>",
                        "您未与辽通卡进行绑定，请绑定后再进行此操作！\n\n"+
                                        "<a href='http://218.25.53.5:8081/wechat/CardBanding.aspx?do=first&openid=" +
                                        openid + "'>点击这里，立即绑定</a>");
                    sb.AppendFormat("<FuncFlag>0</FuncFlag>");
                    sb.AppendFormat("</xml>");
                    logger.Info("辽通卡绑定返回---" + sb.ToString());
                    Response.Write(sb.ToString());
                }
                else
                {
                    logger.Info("您与辽通卡进行绑定，开始查询操作.");
                    CardSelect(
                        ds.Tables[0].Rows[0]["Account_NO"] == null
                            ? string.Empty
                            : ds.Tables[0].Rows[0]["Account_NO"].ToString(),
                        ds.Tables[0].Rows[0]["Card_NO"] == null
                            ? string.Empty
                            : ds.Tables[0].Rows[0]["Card_NO"].ToString(),openid,firstid);


                    //StringBuilder sb = new StringBuilder();
                    //sb.AppendFormat("<xml>");
                    //sb.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", openid.Value);
                    //sb.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", firstid.Value);
                    //sb.AppendFormat("<CreateTime>{0}</CreateTime>", DateTime.Now.Ticks);
                    //sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                    //sb.AppendFormat("<Content><![CDATA[{0}]]></Content>",
                    //    strUtil.RemoveHtmlTag("您的微信没有绑定辽通卡！"));
                    //sb.AppendFormat("<FuncFlag>0</FuncFlag>");
                    //sb.AppendFormat("</xml>");
                    //logger.Info("辽通卡绑定返回---" + sb.ToString());
                    //Response.Write(sb.ToString());
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

        private void CardSelect(string accountno, string cardno, string openid, string firstid)
        {
            string CustomerName = string.Empty;
            string CustomerNo = string.Empty;
            logger.Info("开始查询客户操作.");
            string str = accountno;
            string builder =
                "SELECT USER_NO,[USER_NAME],ACCOUNT_NO,TELEPHONE_NO,BALANCE,LOW_MONEY,PRE_BALANCE,ACCOUNT_TYPE,MAIN_CARD_NO,MOBILE_NO  FROM LN_ETC_QUERY_NEW.dbo.A_ACCOUNT" +
                "  WHERE ACCOUNT_NO=@ACCOUNT_NO AND ACCOUNT_STATUS=0 ";
            SqlParameter[] prams = { new SqlParameter("@ACCOUNT_NO", SqlDbType.Char, 14) };
            prams[0].Value = str;
            DataSet ds = SqlHelper.ExecuteDataset(SQLConn, CommandType.Text, builder, prams);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                logger.Info("开始客户未查询到.");
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("<xml>");
                sb.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", openid);
                sb.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", firstid);
                sb.AppendFormat("<CreateTime>{0}</CreateTime>", Senparc.Weixin.MP.Helpers.DateTimeHelper.GetWeixinDateTime(DateTime.Now));
                sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                sb.AppendFormat("<Content><![CDATA[{0}]]></Content>",
                    strUtil.RemoveHtmlTag("客户信息不存在，请您核对！"));
                sb.AppendFormat("<FuncFlag>0</FuncFlag>");
                sb.AppendFormat("</xml>");
                logger.Info("辽通卡绑定返回---" + sb.ToString());
                Response.Write(sb.ToString());
            }
            else
            {
                CustomerName = ds.Tables[0].Rows[0]["USER_NAME"].ToString();
                CustomerNo = ds.Tables[0].Rows[0]["USER_NO"].ToString();
            }


            bool onlyaccount = false;
            string sql = string.Empty;
            var sqllist = new List<SqlParameter>();
            string where = string.Empty;

            logger.Info("开始查询卡帐及帐户操作.cardno=" + cardno);
            switch (cardno.Substring(8,1))
            {
                case "0":
                    logger.Info("开始查询储值卡操作.");
                    builder =
                        "SELECT top 1 vt.[CARD_TYPE],[ISSUER_NUM],[LAST_BALANCE]*0.01 as LAST_BALANCE,[IC_TRANS_TIME],vt.[ACCOUNT_NO]," +
                        "CASE WHEN CARD_STATUS IN(3,6,5) THEN '有效' ELSE '无效' END CARD_STATUS,ca.CARD_EXPIRED_DATE " +
                        "FROM [LN_ETC_QUERY_NEW].[dbo].[View_TRANSACTION] vt join LN_ETC_QUERY_NEW.[dbo].CARD_ACCOUNT ca on vt.ISSUER_NUM = ca.CARD_NO" +
                        " where vt.ISSUER_NUM =@card_no order by IC_TRANS_TIME desc";
                    ds = SqlHelper.ExecuteDataset(SQLConn, CommandType.Text, builder, new SqlParameter("@card_no", cardno));
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        logger.Info("已经查询储值卡信息，开始返回信息.");
                        string msgxml = "<xml>" +
                                "<ToUserName><![CDATA[" + openid + "]]></ToUserName>" +
                                "<FromUserName><![CDATA[" + firstid + "]]></FromUserName>" +
                                "<CreateTime>" + Senparc.Weixin.MP.Helpers.DateTimeHelper.GetWeixinDateTime(DateTime.Now) + "</CreateTime>" +
                                "<MsgType><![CDATA[news]]></MsgType>" +
                                "<ArticleCount>1</ArticleCount>" +
                                "<Articles>" +
                                "<item><Title><![CDATA[辽通卡帐户信息]]></Title>" +
                                "<Description><![CDATA[用户编号：" + CustomerNo + "\n" +
                                                  "用户名称：" + CustomerName + "\n" +
                                                  "账号：" + accountno + "\n" +
                                                  "卡号：" + cardno + "\n" +
                                                  "余额：" + Convert.ToDouble(ds.Tables[0].Rows[0]["LAST_BALANCE"]).ToString("f2") + "元\n" +
                                                  "余额结算时间：" + Convert.ToDateTime(ds.Tables[0].Rows[0]["IC_TRANS_TIME"]).ToString("yyyy年MM月dd日") + "\n" +
                                                  "状态：" + ds.Tables[0].Rows[0]["CARD_STATUS"].ToString() + "\n" +
                                                  "卡有效期：" + Convert.ToDateTime(ds.Tables[0].Rows[0]["CARD_EXPIRED_DATE"]).ToString("yyyy年MM月dd日") + "]]></Description>" +
                                "<FuncFlag>0</FuncFlag>" +
                                "</item></Articles>" +
                                "<FuncFlag>0</FuncFlag>" +
                                "</xml>";
                        Response.Write(msgxml);
                        logger.Info("辽通卡绑定返回---" + msgxml);
                    }
                    break;
                case "1":
                     logger.Info("开始查询记帐卡操作.");
                    builder =
                        "select ca.CARD_NO,a.MAIN_CARD_NO, a.BALANCE,CASE WHEN CARD_STATUS IN(3,6,5) THEN '有效' ELSE '无效' END CARD_STATUS,ca.CARD_EXPIRED_DATE " +
                        "from LN_ETC_QUERY_NEW.dbo.CARD_ACCOUNT ca join LN_ETC_QUERY_NEW.dbo.A_ACCOUNT a on ca.ACCOUNT_NO = a.ACCOUNT_NO " +
                        "where ca.CARD_NO = @card_no";
                    ds = SqlHelper.ExecuteDataset(SQLConn, CommandType.Text, builder, new SqlParameter("@card_no", cardno));
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        logger.Info("已经查询储值卡信息，开始返回信息.");
                        string MAIN_CARD_NO = ds.Tables[0].Rows[0]["MAIN_CARD_NO"].ToString();
                        if (MAIN_CARD_NO == cardno)
                        {
                            string msgxml = "<xml>" +
                               "<ToUserName><![CDATA[" + openid + "]]></ToUserName>" +
                               "<FromUserName><![CDATA[" + firstid + "]]></FromUserName>" +
                               "<CreateTime>" + Senparc.Weixin.MP.Helpers.DateTimeHelper.GetWeixinDateTime(DateTime.Now) + "</CreateTime>" +
                               "<MsgType><![CDATA[news]]></MsgType>" +
                               "<ArticleCount>1</ArticleCount>" +
                               "<Articles>" +
                               "<item><Title><![CDATA[辽通卡帐户信息]]></Title>" +
                               "<Description><![CDATA[用户编号：" + CustomerNo + "\n" +
                                                 "用户名称：" + CustomerName + "\n" +
                                                 "账号：" + accountno + "\n" +
                                                 "卡号：" + cardno + "\n" +
                                                 "余额：" + Convert.ToDouble(ds.Tables[0].Rows[0]["BALANCE"]).ToString("f2") + "元\n" +
                                                 "状态：" + ds.Tables[0].Rows[0]["CARD_STATUS"].ToString() + "\n" +
                                                 "卡有效期：" + Convert.ToDateTime(ds.Tables[0].Rows[0]["CARD_EXPIRED_DATE"]).ToString("yyyy年MM月dd日") + "]]></Description>" +
                               "<FuncFlag>0</FuncFlag>" +
                               "</item></Articles>" +
                               "<FuncFlag>0</FuncFlag>" +
                               "</xml>";
                            Response.Write(msgxml);
                            logger.Info("辽通卡绑定返回---" + msgxml);
                        }
                        else
                        {
                            string msgxml = "<xml>" +
                               "<ToUserName><![CDATA[" + openid + "]]></ToUserName>" +
                               "<FromUserName><![CDATA[" + firstid + "]]></FromUserName>" +
                               "<CreateTime>" + Senparc.Weixin.MP.Helpers.DateTimeHelper.GetWeixinDateTime(DateTime.Now) + "</CreateTime>" +
                               "<MsgType><![CDATA[news]]></MsgType>" +
                               "<ArticleCount>1</ArticleCount>" +
                               "<Articles>" +
                               "<item><Title><![CDATA[辽通卡帐户信息]]></Title>" +
                               "<Description><![CDATA[用户编号：" + CustomerNo + "\n" +
                                                 "用户名称：" + CustomerName + "\n" +
                                                 "账号：" + accountno + "\n" +
                                                 "卡号：" + cardno + "\n" +
                                                 "状态：" + ds.Tables[0].Rows[0]["CARD_STATUS"].ToString() + "\n" +
                                                 "卡有效期：" + Convert.ToDateTime(ds.Tables[0].Rows[0]["CARD_EXPIRED_DATE"]).ToString("yyyy年MM月dd日") + "]]></Description>" +
                               "<FuncFlag>0</FuncFlag>" +
                               "</item></Articles>" +
                               "<FuncFlag>0</FuncFlag>" +
                               "</xml>";
                            Response.Write(msgxml);
                            logger.Info("辽通卡绑定返回---" + msgxml);
                        }
                    }
                    break;
                case "2":
                case "3":
                case "4":
                case "5":
                default:
                     string msgxml1 = "<xml>" +
                               "<ToUserName><![CDATA[" + openid + "]]></ToUserName>" +
                               "<FromUserName><![CDATA[" + firstid + "]]></FromUserName>" +
                               "<CreateTime>" + Senparc.Weixin.MP.Helpers.DateTimeHelper.GetWeixinDateTime(DateTime.Now) + "</CreateTime>" +
                               "<MsgType><![CDATA[news]]></MsgType>" +
                               "<ArticleCount>1</ArticleCount>" +
                               "<Articles>" +
                               "<item><Title><![CDATA[辽通卡帐户信息]]></Title>" +
                               "<Description><![CDATA[用户编号：" + CustomerNo + "\n" +
                                                 "用户名称：" + CustomerName + "\n" +
                                                 "账号：" + accountno + "\n" +
                                                 "卡号：" + cardno + "\n" +
                                                 "状态：" + ds.Tables[0].Rows[0]["CARD_STATUS"].ToString() + "\n" +
                                                 "卡有效期：" + Convert.ToDateTime(ds.Tables[0].Rows[0]["CARD_EXPIRED_DATE"]).ToString("yyyy年MM月dd日") + "]]></Description>" +
                               "<FuncFlag>0</FuncFlag>" +
                               "</item></Articles>" +
                               "<FuncFlag>0</FuncFlag>" +
                               "</xml>";
                            Response.Write(msgxml1);
                            logger.Info("辽通卡绑定返回---" + msgxml1);
                    break;
            }


           
            //if (accountno.Trim().Length > 0)
            //{
            //    where += " AND ACCOUNT_NO=@ACCOUNT_NO";
            //    sqllist.Add(new SqlParameter("@ACCOUNT_NO", accountno.Trim()));
            //    sql = "SELECT * FROM LN_ETC_QUERY_NEW.dbo.A_ACCOUNT WHERE 2>1 " + where;
            //    onlyaccount = true;
            //}

            //if (cardno.Trim().Length > 0)
            //{
            //    where += " AND CARD_NO=@CARD_NO";
            //    sqllist.Add(new SqlParameter("@CARD_NO", cardno.Trim()));
            //    sql = "SELECT * FROM LN_ETC_QUERY_NEW.dbo.CARD_ACCOUNT WHERE CARD_STATUS IN (3,6,5) " + where;
            //}
           
            //if (onlyaccount)
            //{
            //    UserInfoBind(accountno);
            //}
            //else
            //{
            //    //SqlParameter[] prams = sqllist.ToArray();

            //    //var table = new DataTable();
            //    //table = SqlHelper.ExecuteDataset(helper.strCon, CommandType.Text, sql, prams).Tables[0];

            //    ////ToolHelper.GetData(this.helper, sql, prams, ToolHelper.GetIPAddress());
            //    //pl_Condition.Visible = true;

            //    //UserInfoBind(table.Rows[0]["ACCOUNT_NO"].ToString());
            //    //if (table.Rows[0]["CARD_TYPE"].ToString() == "22")
            //    //{
            //    //    CardInfoBind_M(table.Rows[0]["ACCOUNT_NO"].ToString());
            //    //}
            //}
        }
    }
}