using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Xml;

using System.IO;
using System.Text;

namespace WebTest
{
    public partial class wxapi : System.Web.UI.Page
    {
        System.Text.Encoding encode = System.Text.Encoding.UTF8;
        protected void Page_Load(object sender, EventArgs e)
        {
            string timestamp = Request["timestamp"].ToString();
            string token = "y0ji4zqvihxb";
            string[] arr = { token, timestamp, timestamp };
            Array.Sort(arr);     //字典排序

           string signature =  System.Encryptor.GetSHA1(string.Join("", arr)).ToLower();

           if (signature == Request["signature"].ToString().ToLower())
           {
               using (StreamReader reader = new StreamReader(Request.InputStream, encode))
               {
                   string msg = reader.ReadToEnd();
                   Response.Clear();
                   Response.Write("<xml>" +
                                  "<ToUserName><![CDATA[of5qOuCGOP0_Zy2O-ehWtvsrfqms]]></ToUserName>" +
                                  "<FromUserName><![CDATA[gh_94362f46911e]]></FromUserName>" +
                                  "<CreateTime>635300076788522933</CreateTime>" +
                                  "<MsgType><![CDATA[news]]></MsgType>" +
                                  "<ArticleCount>1</ArticleCount>" +
                                  "<Articles>" +
                                  "<item><Title><![CDATA[辽宁]]></Title>" +
                                  "<Description><![CDATA[]]></Description>" +
                                  "<PicUrl><![CDATA[http://sht.cntx.net/welcome.png]]></PicUrl>" +
                                  "<Url><![CDATA[http://sht.cntx.net/minipage.aspx?AppUserId=63529634002049250000000002&openid=of5qOuCGOP0_Zy2O-ehWtvsrfqms]]></Url>" +
                                  "<FuncFlag>0</FuncFlag>" +
                                  "</item></Articles>" +
                                  "<FuncFlag>0</FuncFlag>" +
                                  "</xml>");
                   Response.End();
               }
           }
        }
    }
}