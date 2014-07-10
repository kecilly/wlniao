using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wlniao.Sys
{
    public partial class HandleRuleList : PageLogin
    {
        public Shijia.Service.IHandleRuleService HandleRuleService { get; set; }
        protected string _ListStr = "";
        protected string _PageBar = "";
        protected string _Keyword = "";
        protected string fansCount = "0";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int pageindex = 1;
                int pagecount = 1;
                try

                {
                    pageindex = Convert.ToInt32(Request["page"]);
                    if (pageindex <= 0)
                    {
                        pageindex = 1;
                    }
                }
                catch { pageindex = 1; }
                try
                {
                    long total =0;
                    var pager = HandleRuleService.HandleRuleDao.LoadAllByPage(_CurrentAccount.Id, helper.GetParamInt("level"), out total, pageindex - 1, 10, "desc", "KeyWord");
                    if (pager != null)
                    {
                        fansCount = total.ToString();
                        pagecount = Convert.ToInt32(Math.Ceiling(total/10.00));
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        if (pager != null)
                        {
                            foreach (var item in pager)
                            {
                                try
                                {
                                    string MsgType = "";
                                    string GetMode = "";
                                    switch (item.MsgType)
                                    {
                                        case 1:
                                            MsgType = "基本文字回复";
                                            break;
                                        case 2:
                                            MsgType = "混合图文回复";
                                            break;
                                        case 3:
                                            MsgType = "基本语音回复";
                                            break;
                                        case 999:
                                            MsgType = "API扩展接口";
                                            break;
                                        default:
                                            MsgType = "其它";
                                            break;
                                    }
                                    switch (item.GetMode)
                                    {
                                        case 0:
                                            GetMode = "等于";
                                            break;
                                        case 1:
                                            GetMode = "智能";
                                            break;
                                        case 2:
                                            GetMode = "事件";
                                            break;
                                        case 3:
                                            GetMode = "语音";
                                            break;
                                        default:
                                            GetMode = "其它";
                                            break;
                                    }
                                    sb.AppendFormat("<div class=\"rule_item clearfix\"><div class=\"rule_content clearfix\"><div class=\"data fl\">{1} <span style=\"font-size:12px;\">（{2}）</span></div><div class=\"fr\"><font color=\"gray\">{4}</font>&nbsp;<font color=\"gray\">共{5}次推送</font>&nbsp;&nbsp;<a href=\"javascript:SetWelcome('{0}');\">关注推送</a>&nbsp;<a href=\"javascript:SetDefault('{0}')\">设为默认</a>&nbsp;<a href=\"HandleRuleForm.aspx?Id={0}\">编辑/查看</a>&nbsp;<a href=\"javascript:Del('{0}');\">删除</a></div></div><div class=\"rule_desc clearfix\" style=\"height:auto;line-height:25px;\">{3}</div></div>", item.Id, item.KeyWord, MsgType, item.Description, GetMode, item.PushCount);
                                }
                                catch { }
                            }
                        }
                        _ListStr = sb.ToString();
                        if (pagecount > 1)
                        {
                            _PageBar += "<div class=\"page\">";
                            if (pageindex == 1)
                            {
                                _PageBar += "<span><上一页</span>";
                            }
                            else
                            {
                                _PageBar += "<a href=\"HandleRuleList.aspx?page=" + (pageindex - 1).ToString() + "\"><上一页</a>";
                            }
                            int i = 1;
                            int min = 0;
                            int max = 10;
                            if (pageindex > 5)
                            {
                                max = pageindex + 5;
                            }
                            if (max > pagecount)
                            {
                                max = pagecount;
                            }
                            min = max - 11;
                            if (min < 0)
                            {
                                min = 0;
                            }
                            for (; min < max; min++)
                            {
                                if ((i + min) == pageindex)
                                {
                                    _PageBar += "<em>" + (i + min) + "</em>";
                                }
                                else
                                {
                                    _PageBar += "<a href=\"HandleRuleList.aspx?page=" + (i + min) + "\">" + (i + min) + "</a>";
                                }
                            }
                            if (pageindex == pagecount)
                            {
                                _PageBar += "<span>下一页></span>";
                            }
                            else
                            {
                                _PageBar += "<a href=\"HandleRuleList.aspx?page=" + (pageindex + 1).ToString() + "\">下一页></a>";
                            }
                            _PageBar += "</div>";
                        }
                    }
                }
                catch { }

            }
        }

    }
}