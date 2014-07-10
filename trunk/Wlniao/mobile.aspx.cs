using System;
using System.Collections.Generic;
using Shijia.Domain;
using Shijia.Service;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wlniao
{
    public partial class mobile : MiniPage
    {
        bool local = false;
        public Shijia.Service.IAccountService AccountService { set; get; }
        protected void Page_Load(object sender, EventArgs e)
        {
            string account = MiniSiteAccount;
            Wlniao.Model.MiniSite site = Wlniao.MiniSite.Get(account);
            if (!string.IsNullOrEmpty(account))
            {
                Session["MiniSiteAccount"] = account;
                if (site == null)
                {
                    Account acc = AccountService.Get(account);
                    
                    if (acc != null)
                    {
                        Wlniao.MiniSite.Set(account, "您的微网站开通啦", "欢迎进入我们的微网站");
                        site = Wlniao.MiniSite.Get(account);
                    }
                    else
                    {
                        return;
                    }
                }
                if (string.IsNullOrEmpty(site.Style))
                {
                    site.Style = "Default";
                }
                if (!string.IsNullOrEmpty(Request["style"]))
                {
                    site.Style = Request["style"];
                }
                Wlniao.Model.MiniSiteStyle style = Wlniao.MiniSite.GetStyle(account);
                string file = "UsersData/" + account + "/MiniSite/index.html";
                string html = "";
                if (Oss.Exists(file) && site.Style.ToLower() == "default")
                {
                    html = Oss.ReadStr(file);
                }
                else
                {
                    file = "BaseData/Style/" + site.Style + "/index.html";
                   
                        html = Oss.ReadStr(file);
                   
                }
                html = html.Replace("{tag.Account}", account);
                html = html.Replace("{tag.MPAccount}", site.MPAccount);
                html = html.Replace("{tag.SiteName}", site.SiteName);

                List<Wlniao.Model.MiniSiteImgLink> list = Wlniao.MiniSite.GetImgLink(account);
                if (list != null)
                {
                    System.Text.StringBuilder sbList = new System.Text.StringBuilder();
                    System.Text.StringBuilder sbScript = new System.Text.StringBuilder();
                    foreach (var item in list)
                    {
                        html = html.Replace("{tag.Title-" + item.Id + "}", item.Title).Replace("{tag.Link-" + item.Id + "}", item.Link).Replace("{tag.Src-" + item.Id + "}", (string.IsNullOrEmpty(item.Src) || item.Src == "#" || item.Src.StartsWith("http://")) ? item.Src : Oss.DataUrl + item.Src);
                    }
                }
                html = html.Replace("{tag.CopyRight}", CopyRight);
                #region Footer 处理
                try
                {
                    if (style.Logo)
                    {
                        string logo = Oss.DataUrl + "/Data/BaseData/Style/defaultlogo.png";

                        string t = "UsersData/" + account + "/MiniSite/Logo";
                        if (Oss.Exists(t + ".jpg"))
                        {
                            logo = t + ".jpg";
                        }
                        else if (Oss.Exists(t + ".gif"))
                        {
                            logo = t + ".gif";
                        }
                        else if (Oss.Exists(t + ".png"))
                        {
                            logo = t + ".png";
                        }
                        html = html.Replace("{tag.Logo}", Oss.DataUrl + "/" + logo);
                    }
                }
                catch { }
                #endregion Footer 处理 结束

                #region Color 处理
                try
                {
                    if (style.Color)
                    {
                        string[] _colors = new string[] { "#E48803", "#111111" };
                        if (!string.IsNullOrEmpty(site.Color))
                        {
                            if (site.Color.Contains(";"))
                            {
                                _colors = site.Color.Split(';');
                            }
                            else
                            {
                                _colors[0] = site.Color;
                            }
                        }
                        html = html.Replace("/*SkinColor*/", ".skincolorbg{background:#HHHHHH;}.skincolortxt{color:#IIIIII".Replace("#HHHHHH", _colors[0]).Replace("#IIIIII", _colors[1]));
                    }
                }
                catch { }
                #endregion Color 处理 结束

                #region CopyRight 处理
                try
                {
                    if (style.CopyRight)
                    {
                        html = html.Replace("{tag.Footer}", site.CopyRight);
                    }
                }
                catch { }
                #endregion CopyRight 处理 结束


                #region Banner 处理
                try
                {
                    if (style.Banner)
                    {
                        string tpl = html.Substring(html.IndexOf("<!--Banner Start-->") + 19);
                        tpl = tpl.Substring(0, tpl.IndexOf("<!--Banner End-->"));
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();

                        for (int i = 1; i <= 5; i++)
                        {
                            try
                            {
                                string srcTemp = "UsersData/" + account + "/MiniSite/banner" + i;
                                string src = "";
                                string url = "#";
                                if (Oss.Exists(srcTemp + ".jpg"))
                                {
                                    src = srcTemp + ".jpg";
                                }
                                else if (Oss.Exists(srcTemp + ".gif"))
                                {
                                    src = srcTemp + ".gif";
                                }
                                else if (Oss.Exists(srcTemp + ".png"))
                                {
                                    src = srcTemp + ".png";
                                }
                                if (!string.IsNullOrEmpty(src))
                                {
                                    src = Oss.DataUrl + "/" + src;
                                    sb.Append(tpl.Replace("{banner.Url}", url).Replace("{tag.DataUrl}/BaseData/Banner/1/default.jpg", src));
                                }

                            }
                            catch { }
                        }
                        html = html.Replace(tpl, sb.ToString());
                    }
                }
                catch { }
                #endregion Banner 处理 结束
                if (local)
                {
                    html = html.Replace("{tag.DataUrl}","");
                }
                else
                {
                    html = html.Replace("{tag.DataUrl}", Oss.DataUrl);
                }



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
                    pager = Wlniao.CmsClass.GetPageNewsOnHome(account,pageindex, 10);
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
                                    sb.Insert(0,tpl.Replace("{new.Url}", url).Replace("{new.NewsTitle}", item.NewsTitle).Replace("{new.ClassTitle}", item.ClassTitle).Replace("{new.NewsContent}", strUtil.RemoveHtmlTag(item.ShortContent)).Replace("{new.UpdateTime}", item.UpdateTime.ToString("yyyy年MM月dd日")).Replace("{new.Icons}", string.IsNullOrEmpty(item.NewsIcons) ? "" : item.NewsIcons.Replace("\\", "/")));
                                }
                                catch { }
                            }
                        }
                    }
                    html = html.Replace(tpl, sb.ToString());
                }
                catch { }


                Response.Write(html);
                return;
            }
            Response.Clear();
            Response.End();
        }
    }
}