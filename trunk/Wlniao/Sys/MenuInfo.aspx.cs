using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shijia.Domain;

namespace Wlniao.Sys
{
    public partial class MenuInfo:PageLogin
    {
        public Shijia.Service.IMPMenuService MPMenuService { set; get; }
        public Shijia.Service.IHandleRuleService HandleRuleService { get; set; }
        public string treeData = "";
        protected string title = "新增菜单";
        protected string nid = "menu_tree_1";
        protected string item_id = "0";
        protected string _KeyWordList = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            item_id = Request.QueryString["item_id"] ?? "0";            
            title = item_id == "0" ? "新增菜单" : "编辑菜单";

            nid = Request.QueryString["nid"] ?? "menu_tree_1";

            treeData = MPMenuService.GetTreeData(_CurrentAccount.Id);
            try
            {
                long total = 0;
                var pager = HandleRuleService.HandleRuleDao.LoadAllByPage(_CurrentAccount.Id, out total , 0, 100,string.Empty,string.Empty);
                if (pager != null)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    if (pager != null)
                    {
                        foreach (var item in pager)
                        {
                            try
                            {
                                sb.AppendFormat("<option value=\"{0}\">{1}</option>", item.KeyWord, string.IsNullOrEmpty(item.Description) ? item.KeyWord : item.Description);
                            }
                            catch { }
                        }
                    }
                    _KeyWordList = sb.ToString();
                }
            }
            catch { }
        }
    }
}