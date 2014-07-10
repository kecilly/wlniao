using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wlniao.CMS
{
    public partial class CmsClass : PageLogin
    {
        protected string _account = "";
        protected string classindex = "";
        protected string classtitle = "";
        protected string classicons = "";
        protected string classtype = "";
        protected string classsort = "";
        protected string classurl = "";
        protected string showinhomepage = "";
        protected string showinnavbar = "";

        protected string classlist = "";
        protected string classbtn = "";
        protected string htmlcontent = "";
        protected string _script = "";
        protected string _ListStr = "";
        protected string _PageBar = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            _account = GetAccountGuid();
            if (!IsPostBack)
            {
                classindex = Request["id"];
                classtitle = Request["classtitle"];
                classicons = Request["classicons"];
                classtype = Request["classtype"];
                classsort = Request["classsort"];
                classurl = Request["classurl"];
                showinhomepage = Request["showinhomepage"];
                showinnavbar = Request["showinnavbar"];
                htmlcontent = Request["myContent"];
                if (Request["method"] == "del")
                {
                    if (Wlniao.CmsClass.Del(_account, classindex).IsValid)
                    {
                        Response.Redirect("cmsclass.aspx");
                    }
                }
                else if (Request["method"] == "edit")
                {
                    if (!string.IsNullOrEmpty(htmlcontent) && !string.IsNullOrEmpty(classindex))
                    {
                        Wlniao.CmsClass.SetContent(_account, classindex, htmlcontent);
                        _script = "<script>parent.showTips('栏目编辑成功!',4,'/sys/cms/cmsclass.aspx?id=" + classindex + "');</script>";
                    }
                }
                else if (Request["method"] == "add")
                {
                    classindex = DateTime.Now.Ticks.ToString();
                    if (Wlniao.CmsClass.Set(_account, classindex, classtitle, classtype, classicons, classsort, showinhomepage, showinnavbar, classurl) != null)
                    {
                        _script = "<script>parent.showTips('栏目添加成功!',4,'/sys/cms/cmsclass.aspx?id=" + classindex + "');</script>";
                    }
                }
                else if (Request["method"] == "save" && !string.IsNullOrEmpty(classtitle))
                {
                    if (string.IsNullOrEmpty(classindex))
                    {
                        classindex = DateTime.Now.Ticks.ToString();
                    }
                    if (Wlniao.CmsClass.Set(_account, classindex, classtitle, classtype, classicons, classsort, showinhomepage, showinnavbar, classurl) != null)
                    {
                        _script = "<script>parent.showTips('栏目修改成功!',4,'/sys/cms/cmsclass.aspx?id=" + classindex + "');</script>";
                    }
                }
                else if (Request["method"] == "delnews")
                {
                    if (Wlniao.CmsClass.DelNews(_account, Request["nid"]).IsValid)
                    {
                        _script = "<script>parent.showTips('内容删除成功!',4,'/sys/cms/cmsclass.aspx?id=" + classindex + "');</script>";
                    }
                }
                else
                {
                    Wlniao.Model.CmsClass model = Wlniao.CmsClass.Get(_account, classindex);
                    if (model != null)
                    {
                        classtitle = model.ClassTitle;
                        classicons = model.ClassIcons.Replace("\\", "/");
                        classtype = model.ClassType;
                        classsort = model.ClassSort;
                        classurl = model.ClassUrl;
                        if (model.ShowInHomePage)
                        {
                            showinhomepage = "on";
                        }
                        if (model.ShowInNavBar)
                        {
                            showinnavbar = "on";
                        }

                        htmlcontent = model.ClassContent;
                        if (classtype == "list")
                        {
                            DataPage<Wlniao.Model.CmsNews> pager = null;
                            int pageindex = 1;
                            try
                            {
                                pageindex = Convert.ToInt32(Request["page"]);
                                if (pageindex <= 0)
                                {
                                    pageindex = 1;
                                }
                            }
                            catch { pageindex = 1; }
                            pager = Wlniao.CmsClass.GetPageNews(GetAccountGuid(), classindex, pageindex, 10);
                            if (pager != null)
                            {
                                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                if (pager.Results != null)
                                {
                                    foreach (var item in pager.Results)
                                    {
                                        try
                                        {
                                            if (!string.IsNullOrEmpty(item.NewsIcons) && item.NewsIcons != "#")
                                            {
                                                if (!item.NewsIcons.StartsWith("http://"))
                                                {
                                                    if (item.NewsIcons.StartsWith("/"))
                                                    {
                                                        item.NewsIcons = Oss.DataUrl + item.NewsIcons;
                                                    }
                                                    else
                                                    {
                                                        item.NewsIcons = Oss.DataUrl + "/" + item.NewsIcons;
                                                    }
                                                }
                                            }
                                            sb.AppendFormat("<div class=\"newsli\"><div class=\"newslipic\"><p>缩略图</p><img class=\"navimg\" src=\"{2}\" /></div><div class=\"newslitxt\"><div><h4>{0}</h4></div><div>{4}{5}<a href=\"javascript:void(0)\" onclick=\"return GotoDelNews('{1}');return false;\">删除</a>&nbsp;<a href=\"news.aspx?id={1}&cid="+classindex+"\">编辑</a>&nbsp;<a href=\"/cms.aspx?a={3}&nid={1}\" target=\"_blank\">查看</a> </div></div><div class=\"clearfix\"></div></div>", item.NewsTitle, item.NewsIndex, item.NewsIcons, _account, item.ShowInHomePage ? "<font color=\"gray\">首页显示</font>&nbsp;" : "", string.IsNullOrEmpty(item.NewsUrl) ? "" : "<font color=\"gray\">外链</font>&nbsp;");
                                        }
                                        catch { }
                                    }
                                }
                                //sb.Append("</ul>");
                                _ListStr = sb.ToString();


                                if (pager.PageCount > 1)
                                {
                                    _PageBar += "<div class=\"page\">";
                                    if (pager.Current == 1)
                                    {
                                        _PageBar += "<span><上一页</span>";
                                    }
                                    else
                                    {
                                        _PageBar += "<a href=\"cmsclass.aspx?ct=" + classindex + "&page=" + (pager.Current - 1).ToString() + "\"><上一页></a>";
                                    }
                                    int i = 1;
                                    for (int time = 0; time < 10 && i + time <= pager.PageCount; time++)
                                    {
                                        if ((i + time) == pager.Current)
                                        {
                                            _PageBar += "<em>" + (i + time) + "</em>";
                                        }
                                        else
                                        {
                                            _PageBar += "<a href=\"cmsclass.aspx?ct=" + classindex + "&page=" + (i + time) + "\">" + (i + time) + "</a>";
                                        }
                                    }
                                    if (pager.Current == pager.PageCount)
                                    {
                                        _PageBar += "<span>下一页></span>";
                                    }
                                    else
                                    {
                                        _PageBar += "<a href=\"cmsclass.aspx?ct=" + classindex + "&page=" + (pager.Current + 1).ToString() + "\">下一页></a>";
                                    }
                                    _PageBar += "</div>";
                                }
                            }
                        }

                    }
                }
                try
                {
                    List<Wlniao.Model.CmsClass> _classlist = Wlniao.CmsClass.GetPage(_account, 0, int.MaxValue).Results;
                    foreach (var item in _classlist)
                    {
                        string icons = "list";
                        string typename = "文章列表";
                        string url = "#";
                        if (item.ClassType == "url")
                        {
                            icons = "share";
                            typename = "链接地址";
                            url = item.ClassUrl;
                        }
                        else if (item.ClassType == "page")
                        {
                            icons = "file";
                            typename = "单页内容";
                        }
                        if (item.ClassIndex == classindex)
                        {
                            //classbtn = string.Format("<button class=\"btn btn-info\" onclick=\"return OpenNew('{2}');return false;\"><i class=\"icon-{1}\"></i> {0}</button>", typename, icons, url);
                            classbtn = string.Format("<button class=\"btn btn-info\" onclick=\"return OpenNew('/cms.aspx?a={2}&cid={3}');return false;\"><i class=\"icon-{1}\"></i> {0}</button>", typename, icons, _account,item.ClassIndex);
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