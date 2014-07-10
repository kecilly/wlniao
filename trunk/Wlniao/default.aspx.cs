using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wlniao
{
    public partial class _Default : PageLogin
    {
        public Shijia.Service.IMPWechatService MPWechatService { get; set; }
        
        protected string _account;
        protected bool AppCenter = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    _account = GetAccountGuid();
                    Shijia.Domain.MPWechat mpwechat = MPWechatService.Check(_CurrentAccount.Id);

                    AppCenter = KeyValueDataService.GetBool("AppCenter");

                    if (string.IsNullOrEmpty(mpwechat.FristAccount))
                    {
                        Response.Clear();
                        Response.Write("<script>top.location.href='/Sys/Init.aspx?nosend';</script>");
                    }
                }
            }
            catch
            {
                Response.Redirect("login.aspx");
            }
        }

    }
}