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
using Senparc.Weixin.MP.Entities;

namespace WebTest
{
    public partial class DisplayPass : System.Web.UI.Page
    {
        private System.Text.Encoding encode = System.Text.Encoding.UTF8;

        private static readonly log4net.ILog logger =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected AshxHelper helper = new AshxHelper(HttpContext.Current);
        private string SQLConn = ConfigurationManager.ConnectionStrings["etcConnString"].ToString().Trim();

        protected void Page_Load(object sender, EventArgs e)
        {
            logger.Info("辽通卡通行记录");
            if (!IsPostBack)
            {
                if (helper.GetParam("do").ToLower() == "list")
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
                        string openid = doc.GetElementsByTagName("FromUserName")[0].InnerText.Trim();
                        string firstid = doc.GetElementsByTagName("ToUserName")[0].InnerText.Trim();

                        int WeiXinCard = Convert.ToInt32(SqlHelper.ExecuteScalar(SQLConn, CommandType.Text,
                            "select count(1)  from WeiXinCard where WXOpenID=@openid ",
                            new SqlParameter("@openid", openid)));
                        if (WeiXinCard > 0)
                        {
                            string WeiXinCard_NO = SqlHelper.ExecuteScalar(SQLConn, CommandType.Text,
                                "select  Card_NO from WeiXinCard where WXOpenID=@openid ",
                                new SqlParameter("@openid", openid)).ToString();

                            string msgxml = "<xml>" +
                                            "<ToUserName><![CDATA[" + openid + "]]></ToUserName>" +
                                            "<FromUserName><![CDATA[" + firstid + "]]></FromUserName>" +
                                            "<CreateTime>" + Senparc.Weixin.MP.Helpers.DateTimeHelper.GetWeixinDateTime(DateTime.Now) + "</CreateTime>" +
                                            "<MsgType><![CDATA[news]]></MsgType>" +
                                            "<ArticleCount>1</ArticleCount>" +
                                            "<Articles>" +
                                            "<item><Title><![CDATA[辽通卡通行记录]]></Title>" +
                                            "<Description><![CDATA[　　查看您绑定辽通卡的最近一个月通行记录信息。]]></Description>" +
                                            "<Url><![CDATA[http://218.25.53.5:8081/wechat/DisplayPass.aspx?do=first&openid=" +
                                            openid + "]]></Url>" +
                                            "<FuncFlag>0</FuncFlag>" +
                                            "</item></Articles>" +
                                            "<FuncFlag>0</FuncFlag>" +
                                            "</xml>";
                            Response.Write(msgxml);
                            logger.Info("辽通卡绑定返回---" + msgxml);
                        }
                        else
                        {
                            logger.Info("您未与辽通卡进行绑定，请绑定后再进行此操作.");

                           
                            StringBuilder sb = new StringBuilder();           
                            sb.AppendFormat("<xml>");
                            sb.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", openid);
                            sb.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", firstid);
                            sb.AppendFormat("<CreateTime>{0}</CreateTime>", Senparc.Weixin.MP.Helpers.DateTimeHelper.GetWeixinDateTime(DateTime.Now));
                            sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                            sb.AppendFormat("<Content><![CDATA[{0}]]></Content>",
                                "您未与辽通卡进行绑定，请绑定后再进行此操作！\n\n" +
                                "<a href='http://218.25.53.5:8081/wechat/CardBanding.aspx?do=first&openid=" +
                                openid + "'>点击这里，立即绑定</a>");
                            sb.AppendFormat("<FuncFlag>0</FuncFlag>");
                            sb.AppendFormat("</xml>");
                            logger.Info("辽通卡绑定返回---" + sb.ToString());
                            Response.Write(sb.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendFormat(strUtil.RemoveHtmlTag("访问来源不明，请从微信共众平台访问。"));
                        logger.Info("访问来源不明，请从微信共众平台访问。" + sb.ToString() + "|ex:" + ex.Message);
                        Response.Write(sb.ToString());

                    }
                    finally
                    {
                        Response.End();
                    }
                }
                else
                {
                    string openid = helper.GetParam("openid");
                    logger.Info("显示通行记录openid=" + openid);
                    if (!string.IsNullOrEmpty(openid))
                    {
                        logger.Info("显示通行记录.");
                        int WeiXinCard1 = Convert.ToInt32(SqlHelper.ExecuteScalar(SQLConn, CommandType.Text,
                            "select count(1)  from WeiXinCard where WXOpenID=@openid ",
                            new SqlParameter("@openid", openid)));
                        logger.Info("WeiXinCard1=" + WeiXinCard1);
                        if (WeiXinCard1 > 0)
                        {
                            string WeiXinCard_NO = SqlHelper.ExecuteScalar(SQLConn, CommandType.Text,
                                "select  Card_NO from WeiXinCard where WXOpenID=@openid ",
                                new SqlParameter("@openid", openid)).ToString();
                            ViewState["Card_NO"] = WeiXinCard_NO;

                            InvoiceListBind(1);


                        }
                        else
                        {
                            logger.Info("您未与辽通卡进行绑定，请绑定后再进行此操作.");
                            StringBuilder sb = new StringBuilder();

                            sb.Append("<ul>\r\n");
                            sb.Append("  <li>\r\n");

                            sb.Append("   <b>您未与辽通卡进行绑定，请绑定后再进行此操作！</b><br/><br/>\r\n" +
                                      "<a href='http://218.25.53.5:8081/wechat/CardBanding.aspx?do=first&openid=" +
                                      openid + "'>点击这里，立即绑定</a>");
                            sb.Append("</li>\r\n");
                            sb.Append("</ui>\r\n");
                            lb_pass.Text = sb.ToString();
                            logger.Info("辽通卡绑定返回---" + sb.ToString());
                            Panel_no.Visible = false;
                            pass.Visible = true;
                        }
                    }
                }
            }
        }

        private void InvoiceListBind(int PageIndex)
        {

            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@Tables", "LN_ETC_QUERY_NEW.dbo.View_TRANSACTION"),
                new SqlParameter("@PrimaryKey", "IC_TRANS_TIME,CHARGE_TIME"),
                new SqlParameter("@Sort", "IC_TRANS_TIME desc"),
                new SqlParameter("@CurrentPage", PageIndex),
                new SqlParameter("@PageSize", 8),
                new SqlParameter("@Fields",
                    "[IC_TRANS_TIME],[CHARGE_CASH]*0.01 as CHARGE_CASH,DESCRIPTION"),
                new SqlParameter("@Filter",
                    "ISSUER_NUM = '" + ViewState["Card_NO"].ToString() + "' and IC_TRANS_TIME>='" +
                    DateTime.Today.AddMonths(-1).ToString("yyyy-MM-dd") + "'"),
                new SqlParameter("@docount", 1)
            };

            DataSet ds = SqlHelper.ExecuteDataset(SQLConn, CommandType.StoredProcedure,
                "LN_ETC_QUERY_NEW.[dbo].[SP_Pagination]", para);

            int count = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            if (count > 0)
            {
                pass.Visible = true;
                Panel_no.Visible = false;
            }
            else
            {
                pass.Visible = false;
                Panel_no.Visible = true;
                return;
            }

            JZInvoice_page.RecordCount = count;
            JZInvoice_page.CurrentPageIndex = PageIndex;


            para = new SqlParameter[]
            {
                new SqlParameter("@Tables", "LN_ETC_QUERY_NEW.dbo.View_TRANSACTION"),
                new SqlParameter("@PrimaryKey", "IC_TRANS_TIME,CHARGE_TIME"),
                new SqlParameter("@Sort", "IC_TRANS_TIME desc"),
                new SqlParameter("@CurrentPage", PageIndex),
                new SqlParameter("@PageSize", 8),
                new SqlParameter("@Fields",
                    "[IC_TRANS_TIME],[CHARGE_CASH]*0.01 as CHARGE_CASH,DESCRIPTION"),
                new SqlParameter("@Filter",
                    "ISSUER_NUM = '" + ViewState["Card_NO"].ToString() + "' and IC_TRANS_TIME>='" +
                    DateTime.Today.AddMonths(-1).ToString("yyyy-MM-dd") + "'"),
                new SqlParameter("@docount", 0)
            };
            ds = SqlHelper.ExecuteDataset(SQLConn, CommandType.StoredProcedure,
                "LN_ETC_QUERY_NEW.[dbo].[SP_Pagination]", para);
            bool oddcss = false;
            StringBuilder sb = new StringBuilder();
            sb.Append("<ul>\r\n");
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                if (oddcss)
                {
                    sb.Append("  <li class=\"odd\">\r\n");
                }
                else
                {
                    sb.Append("  <li>\r\n");
                }
                sb.Append("   <b>" + Convert.ToDateTime(row["IC_TRANS_TIME"]).ToString("yyyy年MM月dd日 HH:mm:ss") +
                          "</b>　　" + Convert.ToDouble(row["CHARGE_CASH"]).ToString("f2") + "元<br />\r\n");
                sb.Append(row["DESCRIPTION"].ToString() + "</li>\r\n");
                oddcss = !oddcss;
            }
            sb.Append("</ui>\r\n");
            lb_pass.Text = sb.ToString();
            logger.Info("display:" + sb.ToString());
        }

        protected void JZInvoice_page_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            InvoiceListBind(e.NewPageIndex);
        }
    }
}