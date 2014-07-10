using System;
using System.Collections.Generic;
using Shijia.Domain;
using Shijia.Service;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wlniao
{
    public partial class cms : MiniPage
    {
        public Shijia.Service.IMPWechatService MPWechatService { set; get; }
        protected void Page_Load(object sender, EventArgs e)
        {

            bool local = false;
            string account = MiniSiteAccount;
            if (!string.IsNullOrEmpty(account))
            {
                Wlniao.Model.MiniSite site = Wlniao.MiniSite.Get(account);
                if (site != null)
                {
                    if (string.IsNullOrEmpty(site.Style))
                    {
                        site.Style = "default";
                    }
                    if (string.IsNullOrEmpty(Request["kw"]))
                    {
                        Session["MiniSiteAccount"] = account;
                        if (!string.IsNullOrEmpty(Request["cid"]))
                        {
                            #region 栏目展示
                            Wlniao.Model.CmsClass cms = Wlniao.CmsClass.Get(account, Request["cid"]);
                            if (cms != null)
                            {
                                if (cms.ClassType == "url")
                                {
                                    if (!string.IsNullOrEmpty(cms.ClassUrl) && cms.ClassUrl != "#")
                                    {
                                        Response.Redirect(cms.ClassUrl);
                                    }
                                }
                                else if (cms.ClassType == "page")
                                {
                                    string html = "";
                                   
                                     html = Oss.ReadStr("BaseData/Style/news.html");
                                  
                                    html = html.Replace("{tag.SiteName}", site.SiteName);
                                    string logo = DataUrl + "/Data/BaseData/Style/defaultlogo.png";
                                    if (!string.IsNullOrEmpty(site.LogoSrc) && site.LogoSrc != "#")
                                    {
                                        if (site.LogoSrc.StartsWith("http"))
                                        {
                                            logo = site.LogoSrc;
                                        }
                                        else if (site.LogoSrc.StartsWith("/"))
                                        {
                                            logo = DataUrl + site.LogoSrc;
                                        }
                                        else
                                        {
                                            logo = DataUrl + "/" + site.LogoSrc;
                                        }
                                        logo = logo.Replace("\\", "/");
                                    }
                                    html = html.Replace("{tag.Logo}", logo);
                                    html = html.Replace("{tag.ClassTitle}", cms.ClassTitle);
                                    html = html.Replace("{tag.ClassContent}", cms.ClassContent);
                                    html = html.Replace("{tag.CopyRight}", CopyRight);
                                    html = html.Replace("{tag.DataUrl}", Oss.DataUrl);
                                    Response.Write(html);
                                }
                                else if (cms.ClassType == "list")
                                {
                                    string html = "";
                                   
                                        html = Oss.ReadStr("BaseData/Style/list.html");
                                  
                                    html = html.Replace("{tag.SiteName}", site.SiteName);
                                    string logo = DataUrl + "/Data/BaseData/Style/defaultlogo.png";
                                    if (!string.IsNullOrEmpty(site.LogoSrc) && site.LogoSrc != "#")
                                    {
                                        if (site.LogoSrc.StartsWith("http"))
                                        {
                                            logo = site.LogoSrc;
                                        }
                                        else if (site.LogoSrc.StartsWith("/"))
                                        {
                                            logo = DataUrl + site.LogoSrc;
                                        }
                                        else
                                        {
                                            logo = DataUrl + "/" + site.LogoSrc;
                                        }
                                        logo = logo.Replace("\\", "/");
                                    }
                                    html = html.Replace("{tag.Logo}", logo);
                                    html = html.Replace("{tag.ClassTitle}", cms.ClassTitle);
                                    html = html.Replace("{tag.ClassContent}", cms.ClassContent);
                                    html = html.Replace("{tag.CopyRight}", CopyRight);
                                    html = html.Replace("{tag.DataUrl}", Oss.DataUrl);
                                    string picicon = "Data/BaseData/Style/nopic.png";
                                    try
                                    {
                                        string tpl = html.Substring(html.IndexOf("<!--List Start-->") + 17);
                                        tpl = tpl.Substring(0, tpl.IndexOf("<!--List End-->"));
                                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
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
                                        pager = Wlniao.CmsClass.GetPageNews(account, cms.ClassIndex, pageindex, 10);
                                        if (pager != null)
                                        {
                                            if (pager.Results != null)
                                            {
                                                foreach (var item in pager.Results)
                                                {
                                                    try
                                                    {
                                                        if (!string.IsNullOrEmpty(item.NewsIcons) && item.NewsIcons != "#")
                                                        {
                                                            if (item.NewsIcons.StartsWith("http"))
                                                            {

                                                            }
                                                            else if (item.NewsIcons.StartsWith("/"))
                                                            {
                                                                item.NewsIcons = DataUrl + item.NewsIcons;
                                                            }
                                                            else
                                                            {
                                                                item.NewsIcons = DataUrl + "/" + item.NewsIcons;
                                                            }
                                                            item.NewsIcons = item.NewsIcons.Replace("\\", "/");
                                                        }
                                                        string url = "cms.aspx?a=" + account + "&nid=" + item.NewsIndex;
                                                        sb.Append(tpl.Replace("{new.Url}", url).Replace("{new.NewsTitle}", item.NewsTitle).Replace("{new.ClassTitle}", cms.ClassTitle).Replace("{new.NewsContent}", strUtil.RemoveHtmlTag(item.ShortContent)).Replace("{new.UpdateTime}", item.UpdateTime.ToString("yyyy年MM月dd日")).Replace("{new.Icons}", string.IsNullOrEmpty(item.NewsIcons) ? picicon : item.NewsIcons.Replace("\\", "/")));
                                                    }
                                                    catch { }
                                                }
                                            }
                                        }
                                        html = html.Replace(tpl, sb.ToString());
                                    }
                                    catch { }
                                    Response.Write(html);
                                }
                            }
                            #endregion 栏目展示 结束
                        }
                        else if (!string.IsNullOrEmpty(Request["nid"]))
                        {
                            #region 内容展示
                            string html = "";
                           
                                html = Oss.ReadStr("BaseData/Style/news.html");
                           
                            html = html.Replace("{tag.SiteName}", site.SiteName);
                            html = html.Replace("{tag.DataUrl}", Oss.DataUrl);
                            string logo = DataUrl + "/Data/BaseData/Style/defaultlogo.png";
                            if (!string.IsNullOrEmpty(site.LogoSrc) && site.LogoSrc != "#")
                            {
                                if (site.LogoSrc.StartsWith("http"))
                                {
                                    logo = site.LogoSrc;
                                }
                                else if (site.LogoSrc.StartsWith("/"))
                                {
                                    logo = DataUrl + site.LogoSrc;
                                }
                                else
                                {
                                    logo = DataUrl + "/" + site.LogoSrc;
                                }
                                logo = logo.Replace("\\", "/");
                            }
                            html = html.Replace("{tag.Logo}", logo);
                            html = html.Replace("{tag.CopyRight}", CopyRight);
                            Wlniao.Model.CmsNews model = Wlniao.CmsClass.GetNews(account, Request["nid"]);
                            if (model != null)
                            {
                                html = html.Replace("{tag.ClassTitle}", model.NewsTitle);
                                html = html.Replace("{tag.ClassContent}", model.NewsContent);
                                html = html.Replace("{tag.UpdateTime}", model.UpdateTime.ToString("yyyy年MM月dd日"));
                            }
                            Response.Write(html);
                            #endregion 内容展示 结束
                        }
                    }
                    else
                    {
                        MPWechat mpwechat = MPWechatService.Get(_CurrentAccount.Id);
                        HandleRule kw = MPWechatService.GetByText(mpwechat, Request["kw"]);
                        if (kw != null && kw.MsgType == 2)
                        {
                            string[] msgs = kw.Content.Split(new string[] { "#@@@#" }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string msg in msgs)
                            {
                                string[] kv = msg.Split(new string[] { "#@@#" }, StringSplitOptions.None);
                                if (kv[0] == Request["t"])
                                {
                                    string html = "";
                                   
                                    html = Oss.ReadStr("BaseData/Style/page.html");
                                   
                                    html = html.Replace("{tag.Title}", kv[0]);
                                    html = html.Replace("{tag.Pic}", kv[2].Replace("\\", "/"));
                                    html = html.Replace("{tag.Content}", strUtil.HtmlDecode(kv[4]));
                                    html = html.Replace("{tag.CopyRight}", CopyRight);
                                    html = html.Replace("{tag.DataUrl}", Oss.DataUrl);
                                    Response.Write(html);
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    //无此用户信息
                    Response.Clear();
                    Response.End();
                }
            }
            else
            {
                //未知用户
                Response.Clear();
                Response.End();
            }
        }
    }
}