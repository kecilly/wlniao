using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shijia.Domain;
using Shijia.Service;
using System.IO;

namespace Wlniao
{
    public class MiniSite
    {
        public static Result Set(int accontid,String account, String sitename, String msgtitle,IHandleRuleService HandRule,IMPWechatService mpwechat, String url = "", String keyword = "", String logosrc = "", String setwelcome = "", String setdefault = "")
        {
            Result result = new Result();
            try
            {
                if (string.IsNullOrEmpty(sitename))
                {
                    result.Add("Sorry，微网站名称未填写");
                }
                else if (string.IsNullOrEmpty(msgtitle))
                {
                    result.Add("Sorry,图文信息标题未填写");
                }
                else
                {
                    string keyPath = "UsersData/" + account + "/MiniSite/Config.data";
                    Wlniao.Model.MiniSite model = Get(account);
                    if (model == null)
                    {
                        model = new Model.MiniSite();
                    }
                    model.SiteName = sitename;
                    model.MsgTitle = msgtitle;
                    string old = model.KeyWords;
                    model.KeyWords = keyword;
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        string pic = logosrc;
                        if (string.IsNullOrEmpty(pic))
                        {
                            pic = "#";
                        }
                        HandleRule handkeyword = mpwechat.Get(accontid,keyword);
                         if (handkeyword == null)
                    {
                        handkeyword = new  HandleRule();
                    }
                         handkeyword.AccountId = accontid;
                    handkeyword.KeyWord = keyword;
                    handkeyword.Description =  "微网站引导信息（自动生成）";
                    handkeyword.MsgType = 2;
                    handkeyword.RuleLevel = 1;
                    handkeyword.GetMode = 0;                   
                    handkeyword.Content =  msgtitle + "#@@#微网站引导信息#@@#" + pic + "#@@#" + url + "#@@#" + sitename + ",更多精彩呈现,点击进入" + "#@@#";
                    HandRule.SaveOrUpdate(handkeyword);
                        if (result.IsValid && setwelcome == "on")
                        {
                            MPWechat mpwechatinfo = mpwechat.GetByAccountID(accontid);
                            mpwechatinfo.WelcomeMsg = "Link:" + keyword;
                            mpwechat.Update(mpwechatinfo);                            
                        }
                        if (result.IsValid && setdefault == "on")
                        {
                            MPWechat mpwechatinfo = mpwechat.GetByAccountID(accontid);
                            mpwechatinfo.DefaultMsg = "Link:" + keyword;
                            mpwechat.Update(mpwechatinfo);   
                           
                        }
                    }
                    if (!string.IsNullOrEmpty(logosrc))
                    {
                        model.LogoSrc = logosrc;
                    }
                    if (string.IsNullOrEmpty(model.Style))
                    {
                        model.Style = "default";
                    }
                    if (result.IsValid)
                    {
                        string json = Json.ToStringEx(model);
                        Oss.WriteStr(keyPath, json);
                    }
                }
            }
            catch (Exception ex)
            {
                result.Add(ex.Message);
            }
            return result;
        }

        public static Result Set(String account, String sitename, String msgtitle, String url = "", String keyword = "", String logosrc = "", String setwelcome = "", String setdefault = "")
        {
            Result result = new Result();
            try
            {
                if (string.IsNullOrEmpty(sitename))
                {
                    result.Add("Sorry，微网站名称未填写");
                }
                else if (string.IsNullOrEmpty(msgtitle))
                {
                    result.Add("Sorry,图文信息标题未填写");
                }
                else
                {
                    string keyPath = "UsersData/" + account + "/MiniSite/Config.data";
                    Wlniao.Model.MiniSite model = Get(account);
                    if (model == null)
                    {
                        model = new Model.MiniSite();
                    }
                    model.SiteName = sitename;
                    model.MsgTitle = msgtitle;
                    string old = model.KeyWords;
                    model.KeyWords = keyword;
                    
                    if (!string.IsNullOrEmpty(logosrc))
                    {
                        model.LogoSrc = logosrc;
                    }
                    if (string.IsNullOrEmpty(model.Style))
                    {
                        model.Style = "default";
                    }
                    if (result.IsValid)
                    {
                        string json = Json.ToStringEx(model);
                        Oss.WriteStr(keyPath, json);
                    }
                }
            }
            catch (Exception ex)
            {
                result.Add(ex.Message);
            }
            return result;
        }

        public static Result SetStyle(String account, String style)
        {
            Result result = new Result();
            try
            {
                if (!string.IsNullOrEmpty(style))
                {
                    string keyPath = "UsersData/" + account + "/MiniSite/Config.data";
                    Wlniao.Model.MiniSite model = Get(account);
                    if (model == null)
                    {
                        Set(account, "您的微网站开通啦", "欢迎进入我们的微网站");
                        model = Get(account);
                    }
                    if (model.Style != style)
                    {
                        string srcfile = "BaseData/Style/" + style + "/ImgLink.data";
                        model.Style = style;
                        try
                        {
                            if (!Oss.Exists(srcfile))
                            {
                                List<Model.MiniSiteImgLink> linklist = new List<Model.MiniSiteImgLink>();
                                Model.MiniSiteImgLink msil = new Model.MiniSiteImgLink();
                                msil.Id = "NAV01";
                                msil.Desc = "导航栏";
                                msil.Title = "未设置";
                                msil.Link = "#";
                                msil.Src = "#";
                                linklist.Add(msil);
                                string jsonsrc = Json.ToStringList(linklist);
                                Oss.WriteStr(srcfile, jsonsrc);
                            }
                            string imglinkfile = "UsersData/" + account + "/MiniSite/ImgLink-" + style + ".data";
                            if (!Oss.Exists(imglinkfile))
                            {
                                Oss.Copy(srcfile, imglinkfile);
                            }
                        }
                        catch { }
                        string json = Json.ToStringEx(model);
                        Oss.WriteStr(keyPath, json);
                    }
                }
            }
            catch (Exception ex)
            {
                result.Add(ex.Message);
            }
            return result;
        }
        public static Result SetCopyright(String account, String copyright)
        {
            Result result = new Result();
            try
            {
                if (string.IsNullOrEmpty(copyright))
                {
                    copyright = "";
                }
                string keyPath = "UsersData/" + account + "/MiniSite/Config.data";
                Wlniao.Model.MiniSite model = Json.ToObject<Wlniao.Model.MiniSite>(Oss.ReadStr(keyPath));
                if (model == null)
                {
                    Set(account, "您的微网站开通啦", "欢迎进入我们的微网站");
                    model = Get(account);
                }
                model.CopyRight = copyright;
                Oss.WriteStr(keyPath, Json.ToStringEx(model));
            }
            catch (Exception ex)
            {
                result.Add(ex.Message);
            }
            return result;
        }
        public static Result SetColor(String account, String color)
        {
            Result result = new Result();
            try
            {
                if (string.IsNullOrEmpty(color))
                {
                    color = "E48803";
                }
                color = color.Replace("#", "");
                string keyPath = "UsersData/" + account + "/MiniSite/Config.data";
                Wlniao.Model.MiniSite model = Json.ToObject<Wlniao.Model.MiniSite>(Oss.ReadStr(keyPath));
                if (model == null)
                {
                    Set(account, "您的微网站开通啦", "欢迎进入我们的微网站");
                    model = Get(account);
                }
                model.Color = "#" + color;
                Oss.WriteStr(keyPath, Json.ToStringEx(model));
            }
            catch (Exception ex)
            {
                result.Add(ex.Message);
            }
            return result;
        }
        public static Result SetMiniNav(String account, Int32 style)
        {
            Result result = new Result();
            try
            {
                string keyPath = "UsersData/" + account + "/MiniSite/Config.data";
                Wlniao.Model.MiniSite model = Get(account);
                if (model == null)
                {
                    Set(account, "您的微网站开通啦", "欢迎进入我们的微网站");
                    model = Get(account);
                }
                model.MiniNav = style;
                string json = Json.ToStringEx(model);
                Oss.WriteStr(keyPath, json);
            }
            catch (Exception ex)
            {
                result.Add(ex.Message);
            }
            return result;
        }
        public static Wlniao.Model.MiniSite Get(String account)
        {
            try
            {
                string keyPath = "UsersData/" + account + "/MiniSite/Config.data";
                string json = Oss.ReadStr(keyPath);
                Wlniao.Model.MiniSite model = Json.ToObject<Wlniao.Model.MiniSite>(json);

                return model;
            }
            catch (Exception ex)
            {
            }
            return null;
        }
        public static Wlniao.Model.MiniSiteStyle GetStyle(String account)
        {
            Wlniao.Model.MiniSiteStyle style = new Model.MiniSiteStyle();
            try
            {
                string json = Oss.ReadStr("/UsersData/" + account + "/MiniSite/Config.data");
                Wlniao.Model.MiniSite model = Json.ToObject<Wlniao.Model.MiniSite>(json);
                
                json = Oss.ReadStr("/BaseData/Style/" + model.Style + "/Config.data");
              
                style = Json.ToObject<Wlniao.Model.MiniSiteStyle>(json);
            }
            catch (Exception ex)
            {
            }
            return style;
        }
        public static List<Model.MiniSiteStyle> GetStyleList(String account)
        {
            List<Model.MiniSiteStyle> list = new List<Model.MiniSiteStyle>();
            try
            {
                bool local = false;
                string styleindex = "BaseData/Data/Style.index";
                string[] files = null;
               
                    if (!Oss.Exists(styleindex))
                    {
                        #region 当索引文件不存在时，自动生成索引文件
                        string txt = "";
                        string[] sf = Oss.GetFiles("BaseData/Style/");
                        if (sf == null || sf.Length == 0)
                        {
                            Model.MiniSiteStyle mss = new Model.MiniSiteStyle();
                            mss.StylePath = "Default";
                            mss.StyleName = "自定义风格";
                            mss.PicPath = "#";
                            string jsonsrc = Json.ToStringEx(mss);
                            Oss.WriteStr("BaseData/Style/Default/Config.data", jsonsrc);
                            list.Add(mss);
                            txt = "BaseData/Style/Default/Config.data\n";
                        }
                        else
                        {
                            foreach (string file in sf)
                            {
                                if (file.ToLower().EndsWith("config.data"))
                                {
                                    txt += file + "\n";
                                }
                            }
                            Oss.WriteStr(styleindex, txt);
                        }
                        files = txt.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                        #endregion
                    }
                    else
                    {
                        files = Oss.ReadStr(styleindex).Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    }
                
                foreach (string file in files)
                {
                    try
                    {
                        Model.MiniSiteStyle obj = null;
                        
                            obj = Json.ToObject<Model.MiniSiteStyle>(Oss.ReadStr(file));
                            if (!string.IsNullOrEmpty(obj.PicPath) && obj.PicPath != "#" && !obj.PicPath.StartsWith("http://"))
                            {
                                obj.PicPath = Oss.DataUrl + obj.PicPath;
                            }
                       
                        list.Add(obj);
                    }
                    catch { }
                }
            }
            catch { }
            return list;
        }
        public static List<Model.MiniSiteImgLink> GetImgLink(String account)
        {
            try
            {
                Wlniao.Model.MiniSite model = Get(account);
                string style = "default";
                try
                {
                    style = model.Style;
                }
                catch { }

                string keyPath = "UsersData/" + account + "/MiniSite/ImgLink-" + style + ".data";
                List<Model.MiniSiteImgLink> linklist = new List<Model.MiniSiteImgLink>();
                if (Oss.Exists(keyPath))
                {
                    linklist = Json.ToList<Model.MiniSiteImgLink>(Oss.ReadStr(keyPath));
                }
                return linklist;
            }
            catch (Exception ex)
            {

            }
            return new List<Model.MiniSiteImgLink>();
        }
        public static Result SetImgLink(String account, List<Model.MiniSiteImgLink> imglink)
        {
            Result result = new Result();
            try
            {
                Wlniao.Model.MiniSite model = Get(account);
                string style = "default";
                try
                {
                    style = model.Style;
                }
                catch { }

                string keyPath = "UsersData/" + account + "/MiniSite/ImgLink-" + style + ".data";
                Oss.WriteStr(keyPath, Json.ToStringEx(imglink));
            }
            catch (Exception ex)
            {
                result.Add(ex.Message);
            }
            return result;
        }
        public static List<Model.MiniNavLink> GetMiniNavLink(String account)
        {
            try
            {
                string keyPath = "UsersData/" + account + "/MiniSite/MiniNav.data";
                List<Model.MiniNavLink> linklist = new List<Model.MiniNavLink>();
                if (Oss.Exists(keyPath))
                {
                    linklist = Json.ToList<Model.MiniNavLink>(Oss.ReadStr(keyPath));
                }
                else
                {
                    linklist = new List<Model.MiniNavLink>();
                    for (int i = 1; i <= 8; i++)
                    {
                        Model.MiniNavLink mnl = new Model.MiniNavLink();
                        mnl.Id = i.ToString();
                        mnl.Src = "";
                        mnl.Title = "";
                        mnl.Type = "Link";
                        mnl.Value = "";
                        linklist.Add(mnl);
                    }
                    string jsonsrc = Json.ToStringEx(linklist);
                    Oss.WriteStr(keyPath, jsonsrc);
                }
                return linklist;
            }
            catch { }
            return new List<Model.MiniNavLink>();
        }
        public static Result SetMiniNavLink(String account, List<Model.MiniNavLink> navlink)
        {
            Result result = new Result();
            try
            {
                string keyPath = "UsersData/" + account + "/MiniSite/MiniNav.data";
                Oss.WriteStr(keyPath, Json.ToStringEx(navlink));
            }
            catch (Exception ex)
            {
                result.Add(ex.Message);
            }
            return result;
        }
    }
}