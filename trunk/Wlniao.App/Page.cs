using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shijia.App.Domain;
using System.Text;
using System.Net;
using System.IO;

namespace Shijia.App
{
    /// <summary>
    /// Weback第三方应用后台页面基类
    /// </summary>
    public class Page:System.Web.UI.Page
    {
        protected AshxHelper helper = new AshxHelper();
        protected string _iback = "";
        private static string appkey = cfgHelper.GetAppSettings("XCenterAppkey");
        private static string sercet = cfgHelper.GetAppSettings("XCenterSercet");
        public Shijia.App.Service.IKeyValueDataService KeyValueDataService { get; set; }
        /// <summary>
        /// App与用户通信的Token
        /// </summary>
        protected string AppToken { get; set; }
        /// <summary>
        /// Weback平台的Url
        /// </summary>
        protected string WebackUrl { get; set; }



        /// <summary>
        /// Weback平台用户唯一ID
        /// </summary>
        protected string AppUserId { get; set; }
        /// <summary>
        /// 微信公众平台Token
        /// </summary>
        protected string MPToken { get; set; }
        /// <summary>
        /// 当前用户订购是否有效
        /// </summary>
        protected bool IsPeriod = false;
        protected override void OnLoad(EventArgs e)
        {
            AppUserId = Request.QueryString["appuserid"];
            if (!string.IsNullOrEmpty(AppUserId))
            {
                string temp = Encryptor.DesDecrypt(AppUserId, appkey);
                if (!string.IsNullOrEmpty(temp))
                {
                    AppUserId = temp;
                    Response.Cookies["App_OpenId"].Value = AppUserId;
                }
                else
                {
                    Response.Cookies["App_OpenId"].Value = "";
                }
            }
            else
            {
                try
                {
                    AppUserId = Request.Cookies["App_OpenId"].Value;
                }
                catch { AppUserId = ""; }
            }
            if (string.IsNullOrEmpty(AppUserId))
            {
                Response.Clear();
                Response.Write("<html><head><title>验证失败！</title></head><body><div style=\"text-align:center;padding:18%;\"><span style=\"color:#333;font-size:18px;\">Sorry,验证失败!</span><br/><span style=\"color:#999; font-size:12px;\">未获取到您的用户信息,无法使用当前功能!</span><span style=\"color:#999; font-size:12px;\"></span></div></body></html>");
                Response.End();
            }
            else
            {
                try
                {
                    if (!string.IsNullOrEmpty(Request["iback"]))
                    {
                        _iback = Request["iback"];
                        Response.Cookies["BaseApp_IBack"].Value = _iback;
                    }
                    else
                    {
                        _iback = Request.Cookies["BaseApp_IBack"].Value;
                    }
                }
                catch { }
                try
                {
                    string sessionkey = "";
                    if (string.IsNullOrEmpty(Request.QueryString["session"]))
                    {
                        sessionkey = Request.Cookies["App_Session"].Value;
                    }
                    else
                    {
                        string temp = Encryptor.DesDecrypt(Request.QueryString["session"], sercet);
                        if (!string.IsNullOrEmpty(temp))
                        {
                            Response.Cookies["App_Session"].Value = temp;
                        }
                        sessionkey = temp;
                    }
                    WebackAccount _account = Json.ToObject<WebackAccount>(sessionkey);
                    if (Convert.ToInt64(_account.Period) > DateTools.ConvertToUnixofLong())
                    {
                        IsPeriod = true;
                    }
                    WebackUrl = _account.WebackUrl;
                    AppToken = _account.AppToken;
                    MPToken = _account.MPToken;
                    if (string.IsNullOrEmpty(AppUserId))
                    {
                        AppUserId = _account.AppUserId;
                    }
                }
                catch { }

                base.OnLoad(e);
            }
        }

        private string GetWithMethod(string Method, params Domain.KeyValueData[] kvs)
        {
            List<Domain.KeyValueData> kvList = new List<Domain.KeyValueData>(kvs);
            kvList.Add(Domain.KeyValueData.Create("apptoken", AppToken));
            kvList.Add(Domain.KeyValueData.Create("method", Method));
            kvList.Sort(Domain.KeyValueData.CompareByKey);
            System.Text.StringBuilder values = new System.Text.StringBuilder();
            foreach (Domain.KeyValueData param in kvList)
            {
                if (!string.IsNullOrEmpty(param.KeyValue))
                {
                    values.Append(param.KeyValue);
                }
            }
            values.Append(MPToken);          //指定VerifyCode
            //生成sig
            byte[] md5_result = System.Security.Cryptography.MD5.Create().ComputeHash(System.Text.Encoding.UTF8.GetBytes(values.ToString()));
            System.Text.StringBuilder sig_builder = new System.Text.StringBuilder();
            foreach (byte b in md5_result)
            {
                sig_builder.Append(b.ToString("x2"));
            }
            string url = WebackUrl + "/api.aspx?sig=" + sig_builder.ToString();

            foreach (Domain.KeyValueData kv in kvList)
            {
                url += "&" + kv.KeyName + "=" + kv.KeyValue;
            }
            //string resultStr = new System.Net.WebClient().DownloadString(url);
            string resultStr = GetResponseString(url, "utf-8");
            return resultStr;
        }

        public static String GetResponseString(string url, string encoding, params Shijia.App.Domain.KeyValueData[] parameters)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < parameters.Length; i++)
            {
                if (i > 0)
                    builder.Append("&");
                builder.Append(parameters[i].ToEncodedString());
            }
            byte[] response_bytes = GetResponseBytes(url, builder.ToString(), encoding);
            return System.Text.Encoding.GetEncoding(encoding).GetString(response_bytes);
        }

        public static byte[] GetResponseBytes(string apiUrl, string postData, string encoding)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(apiUrl);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postData.Length;
            request.Timeout = 20000;
            HttpWebResponse response = null;
            Encoding encode = Encoding.UTF8;
            if (!string.IsNullOrEmpty(encoding))
            {
                encode = System.Text.Encoding.GetEncoding(encoding);
            }
            try
            {
                StreamWriter swRequestWriter = new StreamWriter(request.GetRequestStream());
                swRequestWriter.Write(postData);
                try
                {
                    if (swRequestWriter != null)
                        swRequestWriter.Close();
                }
                catch { }
                response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), encode))
                {
                    return encode.GetBytes(reader.ReadToEnd());
                }
            }
            catch
            {
                return encode.GetBytes("对不起，Url地址无法访问！");
            }
            finally
            {
                if (response != null)
                    response.Close();
            }
        }

        /// <summary>
        /// 设置用户信息（不修改的字段请留空）
        /// </summary>
        /// <param name="Openid"></param>
        /// <returns></returns>
        protected Result SetMPUserInfo(String Openid, String RemarkName = "", String MobileNumber = "", String QQNumber = "")
        {
            Result rlt = null;
            try
            {
                rlt = Json.ToObject<Result>(GetWithMethod("setmpuserinfo", Domain.KeyValueData.Create("openid", Openid), Domain.KeyValueData.Create("RemarkName", RemarkName), Domain.KeyValueData.Create("RemarkName", RemarkName), Domain.KeyValueData.Create("QQNumber", QQNumber)));
            }
            catch { }
            return rlt;
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="Openid"></param>
        /// <returns></returns>
        protected MPUserInfo GetMPUserInfo(String Openid)
        {
            MPUserInfo mpu = null;
            try
            {
                mpu = Json.ToObject<MPUserInfo>(GetWithMethod("getmpuserinfo", Domain.KeyValueData.Create("openid", Openid)));
            }
            catch { }
            return mpu;
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="Openid"></param>
        /// <returns></returns>
        protected MPUserInfo GetMPUserInfoByMobileNumber(String MobileNumber)
        {
            MPUserInfo mpu = null;
            try
            {
                mpu = Json.ToObject<MPUserInfo>(GetWithMethod("getmpuserinfobymobile", Domain.KeyValueData.Create("mobilenumber", MobileNumber)));
            }
            catch { }
            return mpu;
        }
        /// <summary>
        /// 发送文本消息
        /// </summary>
        /// <param name="Openid"></param>
        /// <returns></returns>
        protected Result SendText(String OpenId,String TextMsg)
        {
            Result rlt = null;
            try
            {
                rlt = Json.ToObject<Result>(GetWithMethod("sendtext", Domain.KeyValueData.Create("OpenId", OpenId), Domain.KeyValueData.Create("text", TextMsg)));
            }
            catch { }
            return rlt;
        }
    }

    /// <summary>
    /// Weback第三方应用手机页面基类
    /// </summary>
    public class MobilePage : System.Web.UI.Page
    {
        protected AshxHelper helper = new AshxHelper();
        /// <summary>
        /// Weback平台用户唯一ID
        /// </summary>
        protected string AppUserId { get; set; }
        /// <summary>
        /// 微信用户Openid
        /// </summary>
        protected string MPOpenId { get; set; }
        protected override void OnLoad(EventArgs e)
        {
            AppUserId = helper.GetParam("AppUserId");
            MPOpenId = helper.GetParam("openid");
            if (!string.IsNullOrEmpty(MPOpenId))
            {
                Response.Cookies["ClientAppUserId"].Value = AppUserId;
            }
            else
            {
                try
                {
                    AppUserId = Request.Cookies["ClientAppUserId"].Value;
                }
                catch { }
            }
            if (!string.IsNullOrEmpty(MPOpenId))
            {
                Response.Cookies["MPOpenId"].Value = MPOpenId;
            }
            else
            {
                try
                {
                    MPOpenId = Request.Cookies["MPOpenId"].Value;
                }
                catch { }
            }
            if (string.IsNullOrEmpty(MPOpenId) || string.IsNullOrEmpty(AppUserId))
            {
                Response.Clear();
                Response.Write("<html><head><title>参数丢失！</title></head><body><div style=\"text-align:center;padding:18%;\"><span style=\"color:#333;font-size:18px;\">Sorry,参数丢失!</span><br/><span style=\"color:#999; font-size:12px;\">未获取到您的用户信息,无法使用当前功能!</span><span style=\"color:#999; font-size:12px;\"></span></div></body></html>");
                Response.End();
            }
            else
            {
                base.OnLoad(e);
            }
        }
    }
}