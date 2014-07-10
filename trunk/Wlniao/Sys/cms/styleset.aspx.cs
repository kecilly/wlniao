using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wlniao.CMS
{
    public partial class StyleSet : PageLogin
    {
        protected string _account = "";
        protected string _classstr = "";
        protected string _liststr = "";
        protected string _script = "";
        protected string _scripttips = "";
        protected string _dataurl = Oss.DataUrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            _account = GetAccountGuid();
            if (!IsPostBack)
            {
                try
                {
                    List<Wlniao.Model.CmsClass> _classlist = Wlniao.CmsClass.GetPage(_account, 0, int.MaxValue).Results;
                    _classstr += "<select onchange=\"selChange(this)\" style=\" display:inline; width:158px;\"><option value=\"\">可选择已有栏目</option>";
                    foreach (var item in _classlist)
                    {
                        if (item.ClassType == "url")
                        {
                            _classstr += "<option value=\"" + item.ClassUrl + "\">" + item.ClassTitle + "</option>";
                        }
                        else if (item.ClassType == "page")
                        {
                            _classstr += "<option value=\"cms.aspx?a=" + _account + "&cid=" + item.ClassIndex + "\">" + item.ClassTitle + "</option>";
                        }
                        else
                        {
                            _classstr += "<option value=\"cms.aspx?a=" + _account + "&cid=" + item.ClassIndex + "\">" + item.ClassTitle + "</option>";
                        }
                    }
                    _classstr += "</select>";
                }
                catch { }


                List<Model.MiniSiteImgLink> list = Wlniao.MiniSite.GetImgLink(_account);
                if (list != null&&list.Count>0)
                {
                    System.Text.StringBuilder sbList = new System.Text.StringBuilder();
                    System.Text.StringBuilder sbScript = new System.Text.StringBuilder();
                    foreach (var item in list)
                    {
                        if (Request["method"] == "save")
                        {
                            string title = Request["Title-" + item.Id];
                            string link = Request["Link-" + item.Id];
                            string src = Request["Src-" + item.Id];

                            item.Title = title;
                            item.Link = link;
                            item.Src = src.Replace("\\", "/");
                        }

                        sbList.Append("\n<div class=\"newsli\">");
                        sbList.Append("\n<div class=\"newslipic\"><p id=\"p-" + item.Id + "\" name=\"p-" + item.Id + "\">缩略图</p><a href=\"" + item.Src + "\" target=\"_blank\"><img id=\"Img-" + item.Id + "\" name=\"Img-" + item.Id + "\" class=\"navimg\" src=\"" + ((!string.IsNullOrEmpty(item.Src) && item.Src != "#" && !item.Src.StartsWith("http://")) ? _dataurl + item.Src : item.Src) + "\" /></a></div>");
                        sbList.Append("\n<div class=\"newslitxt\">");
                        sbList.Append("\n<div><h4>" + item.Desc + "</h4>标题:<input name=\"Title-" + item.Id + "\" type=\"text\" value=\"" + item.Title + "\" /></div>");
                        sbList.Append("\n<div>连接:<input id=\"Link-" + item.Id + "\" name=\"Link-" + item.Id + "\" type=\"text\" value=\"" + item.Link + "\" />&nbsp;" + _classstr + "</div>");
                        sbList.Append("\n<div>图片:<input id=\"Src-" + item.Id + "\" name=\"Src-" + item.Id + "\" type=\"text\" value=\"" + item.Src + "\" />&nbsp;&nbsp;<span id=\"Sup-" + item.Id + "\"></span></div>");
                        sbList.Append("\n</div>");
                        sbList.Append("\n<div class=\"clearfix\"></div>");
                        sbList.Append("\n</div>");
                        sbList.Append("\n");

                        sbScript.Append("\n$(\"#Sup-" + item.Id + "\").uploadify({");
                        sbScript.Append("\n'uploader': '/upload.aspx?filetype=pic&account=" + _account + "',");
                        sbScript.Append("\n'formData': {},");
                        sbScript.Append("\n'buttonText': '选择图片',");
                        sbScript.Append("\n'auto': true,");
                        sbScript.Append("\n'multi': true,");
                        sbScript.Append("\n'onUploadSuccess': function (e, response, data) {");
                        sbScript.Append("\n    var stringArray = response.split(\"|\");");
                        sbScript.Append("\n    if (stringArray[0] == \"1\") {");
                        sbScript.Append("\n        $('#p-" + item.Id + "').hide();");
                        sbScript.Append("\n        $('#Img-" + item.Id + "').attr('src', \"" + _dataurl + "/\" + +stringArray[1]);");
                        sbScript.Append("\n        $('#Src-" + item.Id + "').val(\"/\"+stringArray[1]);");
                        sbScript.Append("\n   }");
                        sbScript.Append("\n   else {");
                        sbScript.Append("\n       alert(stringArray[2]);");
                        sbScript.Append("\n   }");
                        sbScript.Append("\n}");
                        sbScript.Append("\n});");

                        //sbScript.Append("\nwln.wlnUpload('Sup-" + item.Id + "', wln.path + '../../upload.aspx?filetype=pic&account=" + _account + "', uploadProgressUpload, uploadSuccess" + item.Id + ");");
                        //sbScript.Append("\nfunction uploadSuccess" + item.Id + "(fileobj, serverData) {");
                        //sbScript.Append("\n    var stringArray = serverData.split(\"|\");");
                        //sbScript.Append("\n    if (stringArray[0] == \"1\") {");
                        //sbScript.Append("\n        $('#Img-" + item.Id + "').attr('src',\"" + _dataurl + "/\" +  stringArray[1]).prev().hide();");
                        //sbScript.Append("\n        $('#Src-" + item.Id + "').val(\"/\" + stringArray[1]);");
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
                        Wlniao.MiniSite.SetImgLink(_account, list);
                        _scripttips = "<script>parent.showTips('恭喜你,风格内容设置已保存',4);</script>";
                    }
                }
                else
                {
                    Response.Redirect("style.aspx");
                }
            }
        }

    }
}