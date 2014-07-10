using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wlniao
{
    public class CmsClass
    {
        public static Wlniao.Model.CmsClass Set(String account, String index, String classtitle, String classtype, String classicons = "", String classsort = "", String showinhomepage = "", String showinnavbar = "", String classurl = "")
        {
            if (string.IsNullOrEmpty(classtitle))
            {
                return null;
            }
            if (string.IsNullOrEmpty(index))
            {
                index = DateTime.Now.Ticks.ToString();
            }
            try
            {
                string keyPath = "UsersData/" + account + "/CmsClass/" + index + ".data";
                Wlniao.Model.CmsClass model = Get(account, index);
                if (model == null)
                {
                    model = new Model.CmsClass();
                    model.ClassIndex = index;
                }
                model.ClassTitle = classtitle;
                model.ClassIcons = classicons;
                model.ClassType = classtype;
                try
                {
                    model.ClassSort = int.Parse(classsort).ToString();
                }
                catch
                {
                    model.ClassSort = "0";
                }
                model.ClassUrl = classurl;
                if (showinhomepage == "on")
                {
                    model.ShowInHomePage = true;
                }
                else
                {
                    model.ShowInHomePage = false;
                }
                if (showinnavbar == "on")
                {
                    model.ShowInNavBar = true;
                }
                else
                {
                    model.ShowInNavBar = false;
                }
                string json = Json.ToStringEx(model);
                Oss.WriteStr(keyPath, json);
                return model;
            }
            catch (Exception ex) { }
            return null;
        }
        public static Wlniao.Model.CmsClass SetContent(String account, String index, String classcontent)
        {
            try
            {
                string keyPath = "UsersData/" + account + "/CmsClass/" + index + ".data";
                string json = Oss.ReadStr(keyPath);
                Wlniao.Model.CmsClass model = Json.ToObject<Wlniao.Model.CmsClass>(json);
                if (model == null)
                {
                    return null;
                }
                if (string.IsNullOrEmpty(model.GuidContent))
                {
                    model.GuidContent = Guid.NewGuid().ToString();
                }
                try
                {
                    Oss.WriteStr("UsersData/" + account + "/GuidContent/" + model.GuidContent + ".data", classcontent);
                }
                catch { }
                json = Json.ToStringEx(model);
                Oss.WriteStr(keyPath, json);
                return model;
            }
            catch (Exception ex) { }
            return null;
        }
        public static Wlniao.Model.CmsClass Get(String account, String index)
        {
            try
            {
                string keyPath = "UsersData/" + account + "/CmsClass/" + index + ".data";              
                string json = Oss.ReadStr(keyPath);
                Wlniao.Model.CmsClass model = Json.ToObject<Wlniao.Model.CmsClass>(json);
                try
                {
                    model.ClassContent = Oss.ReadStr("UsersData/" + account + "/GuidContent/" + model.GuidContent + ".data");
                }
                catch { }
                return model;
            }
            catch (Exception ex)
            {
            }
            return null;
        }
        public static Result Del(String account, String index)
        {
            Result result = new Result();
            try
            {
                string keyPath = "UsersData/" + account + "/CmsClass/" + index + ".data";
                Oss.Delete(keyPath);
            }
            catch (Exception ex)
            {
                result.Add("错误：" + ex.Message);
            }
            return result;
        }

        public static DataPage<Wlniao.Model.CmsClass> GetPage(String account, int pageindex = 0, int pagesize = 10)
        {
            var pager = new DataPage<Wlniao.Model.CmsClass>();

            string[] files = Oss.GetFiles("UsersData/" + account + "/CmsClass/");
            var pagerstr = DataPage<String>.GetPage(new List<string>(files), pagesize, pageindex);
            List<Wlniao.Model.CmsClass> cmsclasslist = new List<Model.CmsClass>();
            foreach (string file in pagerstr.Results)
            {
                try
                {
                    string json = Oss.ReadStr(file);
                    var temp = Json.ToObject<Wlniao.Model.CmsClass>(json);
                    try
                    {
                        temp.ClassContent = Oss.ReadStr("UsersData/" + account + "/GuidContent/" + temp.GuidContent + ".data");
                    }
                    catch { }
                    cmsclasslist.Add(temp);
                }
                catch { }
            }

            cmsclasslist.Sort(Wlniao.Model.CmsClass.CompareBySort);
            pager.RecordCount = pagerstr.RecordCount;
            pager.Size = pagerstr.Size;
            pager.PageCount = pagerstr.PageCount;
            pager.Current = pageindex;
            pager.Results = cmsclasslist;
            return pager;
        }
        public static Result SetNews(String account, String index, String title, String shortcontent, String icons, String content, String newsurl, Boolean showinhomepage)
        {
            Result rlt = new Result();
            try
            {
                if (string.IsNullOrEmpty(index))
                {
                    index = DateTime.Now.Ticks.ToString();
                }
                Wlniao.Model.CmsNews model = null;
                try
                {
                    model = Json.ToObject<Wlniao.Model.CmsNews>(Oss.ReadStr("UsersData/" + account + "/News/" + index + ".data"));
                }
                catch { }
                if (model == null)
                {
                    model = new Model.CmsNews();
                    model.NewsIndex = index;
                    model.CreateTime = DateTime.Now;
                }
                model.NewsTitle = title;
                model.ShortContent = shortcontent;
                model.NewsIcons = icons;
                model.NewsUrl = newsurl;
                model.ShowInHomePage = showinhomepage;
                if (string.IsNullOrEmpty(model.GuidContent))
                {
                    model.GuidContent = Guid.NewGuid().ToString();
                }
                try
                {
                    Oss.WriteStr("UsersData/" + account + "/GuidContent/" + model.GuidContent + ".data", content);
                }
                catch { }
                model.UpdateTime = DateTime.Now;
                model.ClassTitle = "";
                if (showinhomepage)
                {
                    if (string.IsNullOrEmpty(model.HomePageFile))
                    {
                        model.HomePageFile = "UsersData/" + account + "/HomePage/News/" + DateTime.Now.Ticks.ToString() + ".data";
                    }
                    Oss.WriteStr(model.HomePageFile, Json.ToStringEx(model));
                }
                else if (!string.IsNullOrEmpty(model.HomePageFile))
                {
                    System.file.Delete(model.HomePageFile);
                }
                string json = Json.ToStringEx(model);
                Oss.WriteStr("UsersData/" + account + "/News/" + index + ".data", json);
            }
            catch (Exception ex)
            {
                rlt.Add("错误：" + ex.Message);
            }
            return rlt;
        }
        public static Wlniao.Model.CmsNews GetNews(String account, String index)
        {
            try
            {
                string file = "UsersData/" + account + "/News/" + index + ".data";
                string json = Oss.ReadStr(file);
                Wlniao.Model.CmsNews model = Json.ToObject<Wlniao.Model.CmsNews>(json);
                try
                {
                    model.NewsContent = Oss.ReadStr("UsersData/" + account + "/GuidContent/" + model.GuidContent + ".data");
                }
                catch { }
                return model;
            }
            catch { }
            return null;
        }
        public static Result DelNews(String account, String index)
        {
            Result result = new Result();
            try
            {
                string file = "UsersData/" + account + "/News/" + index + ".data";
                Oss.Delete(file);
            }
            catch (Exception ex)
            {
                result.Add("错误：" + ex.Message);
            }
            return result;
        }
        public static DataPage<Wlniao.Model.CmsNews> GetPageNews(String account, String classindex, int pageindex = 0, int pagesize = 10)
        {
            var pager = new DataPage<Wlniao.Model.CmsNews>();
            string[] files = Oss.GetFiles("UsersData/" + account + "/News/");
            var pagerstr = DataPage<String>.GetPage(new List<string>(files), pagesize, pageindex);
            List<Wlniao.Model.CmsNews> cmsnewslist = new List<Model.CmsNews>();
            foreach (string file in pagerstr.Results)
            {
                try
                {
                    if (!file.Contains(classindex))
                    {
                        continue;
                    }
                    string json = Oss.ReadStr(file);
                    var model = Json.ToObject<Wlniao.Model.CmsNews>(json);
                    try
                    {
                        model.NewsContent = Oss.ReadStr("UsersData/" + account + "/GuidContent/" + model.GuidContent + ".data");
                    }
                    catch { }
                    cmsnewslist.Add(model);
                }
                catch { }
            }
            pager.RecordCount = pagerstr.RecordCount;
            pager.Size = pagerstr.Size;
            pager.PageCount = pagerstr.PageCount;
            pager.Current = pageindex;
            pager.Results = cmsnewslist;
            return pager;
        }
        public static DataPage<Wlniao.Model.CmsNews> GetPageNewsOnHome(String account, int pageindex = 0, int pagesize = 10)
        {
            var pager = new DataPage<Wlniao.Model.CmsNews>();
            string[] files = Oss.GetFiles("UsersData/" + account + "/HomePage/News/");
            var pagerstr = DataPage<String>.GetPage(new List<string>(files), pagesize, pageindex);
            List<Wlniao.Model.CmsNews> cmsnewslist = new List<Model.CmsNews>();
            foreach (string file in pagerstr.Results)
            {
                try
                {
                    string json = Oss.ReadStr(file);
                    var model = Json.ToObject<Wlniao.Model.CmsNews>(json);
                    try
                    {
                        model.NewsContent = Oss.ReadStr("UsersData/" + account + "/GuidContent/" + model.GuidContent + ".data");
                    }
                    catch { }
                    cmsnewslist.Add(model);
                }
                catch { }
            }
            pager.RecordCount = pagerstr.RecordCount;
            pager.Size = pagerstr.Size;
            pager.PageCount = pagerstr.PageCount;
            pager.Current = pageindex;
            pager.Results = cmsnewslist;
            return pager;
        }
    }
}