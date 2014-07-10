using System;
using System.Collections.Generic;
using Shijia.Domain;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;

namespace Wlniao
{
    public partial class _Notice : PageLogin
    {
        public Shijia.Service.INoticeService NoticeService { get; set; }
        protected string _ListStr = "";
        protected string _NoticeTitle = "";
        protected string _NoticeContent = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    Notice model = null;
                    try
                    {
                        model = NoticeService.Get(int.Parse(Request["id"]));
                    }
                    catch { }
                    List<Notice> list = NoticeService.LoadALL(_CurrentAccount.Id).ToList();
                    foreach (Notice notice in list)
                    {
                        if (model == null)
                        {
                            model = notice;
                        }
                        _ListStr += string.Format("<li><a href=\"notice.aspx?id={0}\">{1}</a></li>", notice.Id, notice.NoticeTitle);
                    }
                    if (model == null || model.RangeId == 2)
                    {
                        Response.Redirect("default.aspx");
                    }
                    else
                    {
                        _NoticeTitle = model.NoticeTitle;
                        _NoticeContent = model.NoticeContent;
                    }
                }
                catch
                {
                    Response.Redirect("default.aspx");
                }
            }
        }

    }
}