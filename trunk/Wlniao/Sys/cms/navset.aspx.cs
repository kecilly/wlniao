using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wlniao.CMS
{
    public partial class NavSet : PageLogin
    {
        protected string _account = "";
        protected string _piclist = "";
        protected string _liststr = "";
        protected string _script = "";
        protected string _scripttips = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            _account = GetAccountGuid();
            if (!IsPostBack)
            {
                try
                {
                    _piclist += "<select onchange=\"selChange(this)\" style=\" display:inline; width:158px;\"><option value=\"\">可用的内置图标</option>";

                    _piclist += "<option value=\"/static/icons/nav/tel.png\">拨号</option>";
                    _piclist += "<option value=\"/static/icons/nav/home.png\">主页</option>";
                    _piclist += "<option value=\"/static/icons/nav/plus.png\">新增</option>";
                    _piclist += "<option value=\"/static/icons/nav/news.png\">菜单</option>";
                    _piclist += "<option value=\"/static/icons/nav/local.png\">位置</option>";
                    _piclist += "<option value=\"/static/icons/nav/qq.png\">QQ</option>";
                    _piclist += "</select>";
                }
                catch { }


                List<Model.MiniNavLink> list = Wlniao.MiniSite.GetMiniNavLink(_account);
                if (list != null&&list.Count>0)
                {
                    System.Text.StringBuilder sbList = new System.Text.StringBuilder();
                    System.Text.StringBuilder sbScript = new System.Text.StringBuilder();
                    foreach (var item in list)
                    {
                        if (Request["method"] == "save")
                        {
                            string title = Request["Title-" + item.Id];
                            string type = Request["Type-" + item.Id];
                            string value = Request["Value-" + item.Id];
                            string src = Request["Src-" + item.Id];

                            item.Title = title;
                            item.Type = type;
                            item.Value = value;
                            if (value == "#")
                            {
                                item.Type = "Link";
                            }
                            item.Src = src.Replace("\\", "/");
                        }
                        string select = "";
                        switch (item.Type.ToLower())
                        {
                            case "link":
                                select = "<select id=\"Type-" + item.Id + "\" name=\"Type-" + item.Id + "\" onchange=\"typeChange(this)\" style=\" display:inline; width:98px;\"><option value=\"Link\" selected=\"selected\">普通连接</option><option value=\"AddFriend\">微信关注</option><option value=\"Tel\">电话号码</option></select>";
                                break;
                            case "addfriend":
                                select = "<select id=\"Type-" + item.Id + "\" name=\"Type-" + item.Id + "\" onchange=\"typeChange(this)\" style=\" display:inline; width:98px;\"><option value=\"Link\">普通连接</option><option value=\"AddFriend\" selected=\"selected\">微信关注</option><option value=\"Tel\">电话号码</option></select>";
                                break;
                            case "tel":
                                select = "<select id=\"Type-" + item.Id + "\" name=\"Type-" + item.Id + "\" onchange=\"typeChange(this)\" style=\" display:inline; width:98px;\"><option value=\"Link\">普通连接</option><option value=\"AddFriend\">微信关注</option><option value=\"Tel\" selected=\"selected\">电话号码</option></select>";
                                break;
                            default:
                                select = "<select id=\"Type-" + item.Id + "\" name=\"Type-" + item.Id + "\" onchange=\"typeChange(this)\" style=\" display:inline; width:98px;\"><option value=\"Link\">普通连接</option><option value=\"AddFriend\">微信关注</option><option value=\"Tel\">电话号码</option></select>";
                                break;
                        }

                        sbList.Append("\n<div class=\"newsli\">");
                        sbList.Append("\n<div class=\"newslipic\"><p id=\"p-" + item.Id + "\" name=\"p-" + item.Id + "\">缩略图</p><a href=\"" + item.Src + "\" target=\"_blank\"><img id=\"Img-" + item.Id + "\" name=\"Img-" + item.Id + "\" class=\"navimg\" src=\"" + item.Src + "\" /></a></div>");
                        sbList.Append("\n<div class=\"newslitxt\">");
                        sbList.Append("\n<div>导航标题:<input name=\"Title-" + item.Id + "\" type=\"text\" value=\"" + item.Title + "\" /></div>");
                        sbList.Append("\n<div>" + select + ":<input id=\"Value-" + item.Id + "\" name=\"Value-" + item.Id + "\" type=\"text\" value=\"" + item.Value + "\" style=\"width:163px;\" />&nbsp;</div>");
                        sbList.Append("\n<div>图片地址:<input id=\"Src-" + item.Id + "\" name=\"Src-" + item.Id + "\" type=\"text\" value=\"" + item.Src + "\" />&nbsp;" + _piclist + "</div>");
                        sbList.Append("\n<div><span id=\"Sup-" + item.Id + "\"></span></div>");
                        sbList.Append("\n</div>");
                        sbList.Append("\n<div class=\"clearfix\"></div>");
                        sbList.Append("\n</div>");
                        sbList.Append("\n");

                          sbScript.Append("\n$(\"#Sup-" + item.Id + "\").uploadify({");
            sbScript.Append("\n'uploader': '/upload.aspx?filetype=pic&account="+_account+"',");
             sbScript.Append("\n'formData': {},");
             sbScript.Append("\n'buttonText': '选择图片',");
             sbScript.Append("\n'auto': true,");
             sbScript.Append("\n'multi': true,");
             sbScript.Append("\n'onUploadSuccess': function (e, response, data) {");
             sbScript.Append("\n    var stringArray = response.split(\"|\");");
             sbScript.Append("\n    if (stringArray[0] == \"1\") {");           
             sbScript.Append("\n        $('#p-" + item.Id + "').hide();");
             sbScript.Append("\n        $('#Img-" + item.Id + "').attr('src', '/'+stringArray[1]);");
             sbScript.Append("\n        $('#Src-" + item.Id + "').val( '/'+stringArray[1]);");
             sbScript.Append("\n   }");
             sbScript.Append("\n   else {");
             sbScript.Append("\n       alert(stringArray[2]);");
             sbScript.Append("\n   }");
            sbScript.Append("\n}");
         sbScript.Append("\n});");

                        //sbScript.Append("\nwln.wlnUpload('Sup-" + item.Id + "', wln.path + '../../upload.aspx?filetype=pic', uploadProgressUpload, uploadSuccess" + item.Id + ");");
                        //sbScript.Append("\nfunction uploadSuccess" + item.Id + "(fileobj, serverData) {");
                        //sbScript.Append("\n    var stringArray = serverData.split(\"|\");");
                        //sbScript.Append("\n    if (stringArray[0] == \"1\") {");
                        //sbScript.Append("\n        $('#Img-" + item.Id + "').attr('src', stringArray[1]).prev().hide();");
                        //sbScript.Append("\n        $('#Src-" + item.Id + "').val(stringArray[1]);");
                        //sbScript.Append("\n    }");
                        //sbScript.Append("\n    else {");
                        //sbScript.Append("        alert(stringArray[2]);");
                        //sbScript.Append("\n    }");
                        //sbScript.Append("\n}");
                    }
                    _liststr = sbList.ToString();
                    _script = sbScript.ToString();

                    if (Request["method"] == "save")
                    {
                        Wlniao.MiniSite.SetMiniNavLink(_account, list);
                        _scripttips = "<script>parent.showTips('恭喜你,微导航设置保存成功',4);</script>";
                    }
                }
                else
                {
                    Response.Redirect("nav.aspx");
                }
            }
        }

    }
}