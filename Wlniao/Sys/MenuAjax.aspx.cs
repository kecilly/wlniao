using System;
using System.Collections.Generic;
using Shijia.Domain;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wlniao.Sys
{
    public partial class MenuAjax : PageLogin
    {
        public Shijia.Service.IMPMenuService MPMenuService { set; get; }
        public Shijia.Service.IMPWechatService MPWechatService { get; set; }
        private string uid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            uid = GetAccountGuid();
            var context = Context;
            string action = GetRequest("do");
            switch (action)
            {
                case "saveMenuItem":
                    SaveMenuItem(context);
                    break;
                case "delMenuItem":
                    DeleteMenu(context);
                    break;
                case "syncMenu":
                    PublishMenu(context);
                    break;
                case "stopMenu":
                    StopMenu(context);
                    break;
                case "getMenu":
                    GetMenu(context);
                    break;
                case "saveTreeData":
                    SaveTree(context);
                    break;
                case "get":
                    GetWxMenuJson(context);
                    break;
                default:
                    GetWxMenuJson(context);
                    break;

            }
            context.Response.End();
        }


        #region GetWxMenuJson
        /// <summary>
        ///获取微信菜单格式信息
        /// </summary>
        /// <param name="context"></param>
        private void GetWxMenuJson(HttpContext context)
        {
            string data = MPMenuService.GetMenuData(_CurrentAccount.Id);
            context.Response.Write(data);
        }
        #endregion

        #region SaveMenuItem
        /// <summary>
        /// SaveMenuItem
        /// </summary>
        /// <param name="context"></param>
        private void SaveMenuItem(HttpContext context)
        {

            string text = GetRequest("menu_text");
            string type = GetRequest("menu_type");
            string key = GetRequest("menu_key");
            int itemId = 0;
            try
            {
                itemId = int.Parse(GetRequest("item_id"));
            }
            catch { }

            string data = "";
            if (string.IsNullOrEmpty(GetRequest("item_id")) || GetRequest("item_id") == "0")
            {
                int sequense = MPMenuService.GetNewSequense(_CurrentAccount.Id);
                data = "{\"menu_text\":\"" + text + "\",\"menu_type\":\"" + type + "\",\"menu_key\":\"" + key + "\",\"itemId\":" + sequense + ",\"insert\":true,\"success\":true}";
                MPMenuService.UpdateSeed(_CurrentAccount.Id, sequense);

            }
            else
            {
                data = "{\"menu_text\":\"" + text + "\",\"menu_type\":\"" + type + "\",\"menu_key\":\"" + key + "\",\"itemId\":" + itemId + ",\"insert\":false,\"success\":true}";
            }
            context.Response.Write(data);
        }
        #endregion

        #region SaveData
        /// <summary>
        /// SaveData
        /// </summary>
        /// <param name="context"></param>
        private void SaveTree(HttpContext context)
        {
            int seed = 0;
            try
            {
                seed = int.Parse(GetRequest("id"));
            }
            catch { }
            string treedata = GetRequest("treedata");


            MPMenuService.SaveMenuData(_CurrentAccount.Id, Shijia.Service.MPMenuService.WeixinMenuHelper.Parse(treedata).Replace(", \"url\":\"\"", "").Replace(", \"key\":\"\"", ""));
            MPMenuService.SaveTreeData(_CurrentAccount.Id, treedata);
            context.Response.Write("{\"success\":true}");
        }
        #endregion

        #region DeleteMenu
        /// <summary>
        /// DeleteMenu
        /// </summary>
        /// <param name="context"></param>
        private void DeleteMenu(HttpContext context)
        {
            int id = int.Parse(GetRequest("itemId"));
            string treeData = GetRequest("treeData");

            MPMenuService.SaveMenuData(_CurrentAccount.Id, Shijia.Service.MPMenuService.WeixinMenuHelper.Parse(treeData).Replace(",\"url\":\"\"", "").Replace(", \"key\":\"\"", ""));
            MPMenuService.SaveTreeData(_CurrentAccount.Id, treeData);
            context.Response.Write("{\"success\":true}");
        }
        #endregion

        #region PublishMenu
        private void PublishMenu(HttpContext context)
        {
            string json = MPMenuService.GetMenuData(_CurrentAccount.Id);
            try
            {
                if (!string.IsNullOrEmpty(json))
                {
                    Shijia.Domain.MPWechat weixin = MPWechatService.Check(_CurrentAccount.Id);
                    if (string.IsNullOrEmpty(weixin.AppId) || string.IsNullOrEmpty(weixin.AppSecret))
                    {
                        context.Response.Write("{\"success\":false,\"errormsg\":\"你暂未设置授权信息，请先设置授权信息\"}");
                    }
                    else
                    {
                        Result rlt = Wlniao.MP.SyncMenu(weixin.AppId, weixin.AppSecret, json);
                        if (rlt.IsValid)
                        {
                            context.Response.Write("{\"success\":true}");
                        }
                        else
                        {
                            context.Response.Write("{\"success\":false,\"errormsg\":\"" + rlt.Errors[0] + "\"}");
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                context.Response.Write("{\"success\":false,\"errormsg\":\"错误：" + ex.Message + "\"}");
            }
        }
        #endregion

        #region StopMenu
        private void StopMenu(HttpContext context)
        {
            try
            {
                MPWechat weixin = MPWechatService.Check(_CurrentAccount.Id);
                if (string.IsNullOrEmpty(weixin.AppId) || string.IsNullOrEmpty(weixin.AppSecret))
                {
                    context.Response.Write("{\"success\":false,\"errormsg\":\"你暂未设置授权信息，请先设置授权信息\"}");
                }
                else
                {
                    Result rlt = Wlniao.MP.DelMenu(weixin.AppId, weixin.AppSecret);
                    if (rlt.IsValid)
                    {
                        context.Response.Write("{\"success\":true}");
                    }
                    else
                    {
                        context.Response.Write("{\"success\":false,\"errormsg\":\"" + rlt.Errors[0] + "\"}");
                    }
                }
            }
            catch (Exception ex)
            {
                context.Response.Write("{\"success\":false,\"errormsg\":\"错误：" + ex.Message + "\"}");
            }
        }
        #endregion

        #region GetMenu
        private void GetMenu(HttpContext context)
        {
            string json = MPMenuService.GetMenuData(_CurrentAccount.Id);
            try
            {
                MPWechat weixin = MPWechatService.Check(_CurrentAccount.Id);
                if (string.IsNullOrEmpty(weixin.AppId) || string.IsNullOrEmpty(weixin.AppSecret))
                {
                    context.Response.Write("{\"success\":false,\"errormsg\":\"你暂未设置授权信息，请先设置授权信息\"}");
                }
                else
                {
                    Result rlt = Wlniao.MP.GetMenu(weixin.AppId, weixin.AppSecret,out json);

                    if (json.Length > 9)
                    {
                        string menuData = json.Substring(8, json.Length - 9);
                        MPMenuService.SaveMenuData(_CurrentAccount.Id, menuData);
                    }
                    if (rlt.IsValid)
                    {
                        context.Response.Write("{\"success\":true}");
                    }
                    else
                    {
                        context.Response.Write("{\"success\":false,\"errormsg\":\"" + rlt.Errors[0] + "\"}");
                    }
                }
            }
            catch (Exception ex)
            {
                context.Response.Write("{\"success\":false,\"errormsg\":\"错误：" + ex.Message + "\"}");
            }
        }
        #endregion

        #region utility
        public string GetRequest(string param)
        {
            return HttpContext.Current.Request[param];
            // return HttpContext.Current.Request.QueryString[param];

        }

        public string PostRequest(string param)
        {
            return HttpContext.Current.Request.Form[param];
        }
        #endregion
    }

}