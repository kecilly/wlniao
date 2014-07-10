using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using GotDotNet.ApplicationBlocks.Data;
using WebTest.SMSService;

namespace WebTest
{
    public partial class forgetpws : System.Web.UI.Page
    {
        private System.Text.Encoding encode = System.Text.Encoding.UTF8;

        private static readonly log4net.ILog logger =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected AshxHelper helper = new AshxHelper(HttpContext.Current);
        private string SQLConn = ConfigurationManager.ConnectionStrings["etcConnString"].ToString().Trim();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (helper.GetParam("do").ToLower() != "forget")
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat(strUtil.RemoveHtmlTag("访问来源不明，请从微信共众平台访问。"));
                    logger.Info("访问来源不明，请从微信共众平台访问。" + sb.ToString());
                    Response.Write(sb.ToString());
                }
            }
        }

        protected void btn_Validete_Click(object sender, EventArgs e)
        {
            ViewState["WeiXinCard_NO"] = name.Text.Trim();
            string str = Validete.Text.Trim().ToUpper();
            if ((str == "") || (str.ToUpper() != base.Session["CheckCode"].ToString().ToUpper()))
            {
                lb_vindex.Text = "亲，你验证码输入错误,请重新输入!";
                logger.Info("辽通卡绑定失败---" + helper.Result.ErrorsText);
            }

            int WeiXinCard = Convert.ToInt32(SqlHelper.ExecuteScalar(SQLConn, CommandType.Text,
                "select count(1)  from SMS_Validate where Card_NO=@WeiXinCard_NO and ValidateCode= @ValidateCode",
                new SqlParameter("@WeiXinCard_NO", ViewState["WeiXinCard_NO"].ToString()),
                new SqlParameter("@ValidateCode", SMSValidete.Text.Trim())));
            if (WeiXinCard > 0)
            {
                Panel_SetPassword.Visible = true;
                pass.Visible = false;
                Panel_no.Visible = false;
            }
            else
            {
                lb_vindex.Text = "亲，你短信验证码输入错误,请重新输入!";
                logger.Info("辽通卡绑定失败---" + helper.Result.ErrorsText);
            }
        }

        protected void btn_send_Click(object sender, EventArgs e)
        {
            ViewState["WeiXinCard_NO"] = name.Text.Trim();
            name.ReadOnly = true;
            int index = DateTime.Now.Second;
            string validatecode = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 4);
            var mobile = SqlHelper.ExecuteScalar(SQLConn, CommandType.Text,
                "select  a.MOBILE_NO from [LN_ETC_QUERY_NEW].[dbo].[CARD_ACCOUNT] ca join  [LN_ETC_QUERY_NEW].[dbo].[A_ACCOUNT] a " +
                " on ca.ACCOUNT_NO= a.ACCOUNT_NO where CARD_NO=@WeiXinCard_NO ", new SqlParameter("@WeiXinCard_NO", ViewState["WeiXinCard_NO"].ToString()));

            if (mobile != null && mobile.ToString().Length == 11)
            {
                SMSService.Service1Client sc = new Service1Client();
                int i = sc.SendSMSPost("00000", mobile.ToString(), "尊敬的ETC用户，您申请修改辽通卡服务密码操作的验证码为" + validatecode + "(序号" + index + ")【辽宁ETC客服中心】", "", "", "");
                logger.Info("修改密码发短信：" + i.ToString());
            }
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@WeiXinCard_NO", ViewState["WeiXinCard_NO"].ToString()),
                new SqlParameter("@Index_NO", index.ToString()),
                new SqlParameter("@ValidateCode", validatecode),
                new SqlParameter("@CreateTiem", DateTime.Now)
            };

            SqlHelper.ExecuteNonQuery(SQLConn, CommandType.Text,
                "delete from [SMS_Validate] where  [Card_NO] = @WeiXinCard_NO;" +
                "insert into [SMS_Validate] ([Card_NO] ,[Index_NO],[ValidateCode],[CreateTime]) values (@WeiXinCard_NO,@Index_NO,@ValidateCode,@CreateTiem)",
                para);
            lb_vindex.Text = "短信验证码(序号为" + index.ToString() +
                             ")已经发送到您申办ETC时所预留的手机号码中，如果您忘记手机号码或更改了手机号码，请拨打服务热线96199，进行咨询。";

            Panel_SetPassword.Visible = false;
            pass.Visible = true;
            Panel_no.Visible = false;
            btn_send.Visible = false;
            btn_Validete.Visible = true;
        }

        protected void btn_savePWS_Click(object sender, EventArgs e)
        {
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@WeiXinCard_NO", ViewState["WeiXinCard_NO"].ToString()),
                new SqlParameter("@PWD", DESEncrypt(txt_password.Text.Trim()))

            };

            try
            {
                SqlHelper.ExecuteNonQuery(SQLConn, CommandType.Text,
                    "  if exists(select * from [LN_ETC_WEB_NEW].[dbo].[USER_PASSWORD] where card_no=@WeiXinCard_NO)" +
                    " update [LN_ETC_WEB_NEW].[dbo].[USER_PASSWORD] set PASS_WORD =@PWD where card_no=@WeiXinCard_NO " +
                    "else " +
                    "insert into [LN_ETC_WEB_NEW].[dbo].[USER_PASSWORD] ([LOGON_NAME],[PASS_WORD],[ACCOUNT_NO],[CARD_NO],[REGIST_DATE],[USER_NAME]) " +
                    "select ca.CARD_NO,@PWD,ca.ACCOUNT_NO,ca.CARD_NO,ca.REGISTE_DATE,a.USER_NAME " +
                    "from LN_ETC_QUERY_NEW.dbo.CARD_ACCOUNT ca join LN_ETC_QUERY_NEW.dbo.A_ACCOUNT a on ca.ACCOUNT_NO= a.ACCOUNT_NO where CARD_NO=@WeiXinCard_NO",
                    para);

                Panel_SetPassword.Visible = false;
                pass.Visible = false;
                Panel_no.Visible = true;
                lb_error.Text = "密码修改成功，现在您可以使用新的密码了。";
            }
            catch (Exception)
            {
                Panel_SetPassword.Visible = false;
                pass.Visible = false;
                Panel_no.Visible = true;
                lb_error.Text = "密码修改失败，请您稍候重试！";
            }

        }

        public string DESEncrypt(string inputString)
        {
            byte[] iv = new byte[] {0x12, 0x34, 0x56, 120, 0x90, 0xab, 0xcd, 0xef};
            byte[] key = Encoding.ASCII.GetBytes("WikeSoft");
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
    }
}