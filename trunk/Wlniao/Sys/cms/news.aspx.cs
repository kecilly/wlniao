using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wlniao.CMS
{
    public partial class News : PageLogin
    {
        protected string _account = "";
        protected string classindex = "";
        protected string newsindex = "";
        protected string newstitle = "";
        protected string newsicons = "";
        protected string newsurl = "";
        protected string shortcontent = "";
        protected string showinhomepage = "";
        protected string classlist = "";
        protected string htmlcontent = "";
        protected string _script = "";
        protected string _dataurl = Oss.DataUrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            _account = GetAccountGuid();
            classindex = Request["cid"];
            newsindex = Request["id"];

            newstitle = Request["newstitle"];
            newsicons = Request["newsicons"];
            shortcontent = Request["shortcontent"];
            newsurl = Request["newsurl"];
            showinhomepage = Request["showinhomepage"];
            newsicons = Request["newsicons"];
            htmlcontent = Request["myContent"];

            if (Request["method"] == "edit")
            {
                if (string.IsNullOrEmpty(newsindex))
                {
                    newsindex = classindex + "_" + DateTime.Now.Ticks.ToString();
                }
               Result rlt= Wlniao.CmsClass.SetNews(_account, newsindex, newstitle, shortcontent, newsicons, htmlcontent, newsurl, showinhomepage == "on");
               if (rlt.IsValid)
               {
                   _script = "<script>parent.showTips('内容保存成功!',4,'/sys/cms/cmsclass.aspx?id=" + classindex + "');</script>";
               }
               else
               {
                   _script = "<script>parent.showTips('" + rlt.Errors[0] + "!',5);</script>";
               }
            }
            else
            {
                if (!IsPostBack)
                {
                    try
                    {
                        var cmsclass = Wlniao.CmsClass.Get(_account, classindex);
                        if (cmsclass == null)
                        {
                            Response.Redirect("cmsclass.aspx");
                        }
                        else if (cmsclass.ClassType != "list")
                        {
                            Response.Redirect("cmsclass.aspx?id=" + classindex);
                        }
                    }
                    catch
                    {
                        Response.Redirect("cmsclass.aspx?id=" + classindex);
                    }
                    Wlniao.Model.CmsNews model = Wlniao.CmsClass.GetNews(_account, newsindex);
                    if (model != null)
                    {
                        newstitle = model.NewsTitle;
                        newsicons = model.NewsIcons;
                        shortcontent = strUtil.RemoveHtmlTag(model.ShortContent);
                        newsurl = model.NewsUrl;
                        showinhomepage = model.ShowInHomePage ? "on" : "";
                        if (!string.IsNullOrEmpty(newsicons) && newsicons != "#")
                        {
                            if (newsicons.StartsWith("http"))
                            {

                            }
                            else if (newsicons.StartsWith("/"))
                            {
                                newsicons = Oss.DataUrl + newsicons;
                            }
                            else
                            {
                                newsicons = Oss.DataUrl + "/" + newsicons;
                            }
                            newsicons = newsicons.Replace("\\", "/");
                        }
                        htmlcontent = model.NewsContent;
                    }

                    try
                    {
                        List<Wlniao.Model.CmsClass> _classlist = Wlniao.CmsClass.GetPage(_account, 0, int.MaxValue).Results;
                        foreach (var item in _classlist)
                        {
                            string icons = "list";
                            string url = "#";
                            if (item.ClassType == "url")
                            {
                                icons = "share";
                                url = item.ClassUrl;
                            }
                            else if (item.ClassType == "page")
                            {
                                icons = "file";
                            }
                            if (item.ClassIndex == classindex)
                            {
                                classlist += string.Format("<button class=\"btn btn-info\" onclick=\"Goto('cmsclass.aspx?id={0}');\"><i class=\"icon-{2}\"></i> {1}</button>", item.ClassIndex, item.ClassTitle, icons);
                            }
                            else
                            {
                                classlist += string.Format("<button class=\"btn\" onclick=\"Goto('cmsclass.aspx?id={0}');\"><i class=\"icon-{2}\"></i> {1}</button>", item.ClassIndex, item.ClassTitle, icons);
                            }
                        }
                    }
                    catch
                    {
                        classlist = "";
                    }

                }





            }
        }
    }
}