using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shijia.Domain;

namespace Wlniao
{
    public partial class Topbar : System.TemplateEngine.PageBase
    {
        public Shijia.Service.INoticeService NoticeService { get; set; }
        protected string SiteName;
        protected string NoticeTitle;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    SiteName = KeyValueDataService.GetString("SiteName");
                }
                catch { }
                try
                {
                    NoticeTitle = NoticeService.LoadALL(0).First().NoticeTitle;
                }
                catch { }
            }
        }
    }
}