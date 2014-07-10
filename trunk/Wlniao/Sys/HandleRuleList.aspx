<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HandleRuleList.aspx.cs" Inherits="Wlniao.Sys.HandleRuleList" %>
<!DOCTYPE html>
<html lang="zh">
<head>
    <title>自动处理规则</title>
    <link href="/static/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .container-narrow {margin: 0 auto;padding:0px 12px 0px 12px;}
        .container-narrow h4{font-family:微软雅黑;font-size:smaller;line-height:1em;}
        .container-narrow p{font-family:楷体;color:#999999;}
        .pagerlist {margin:0 auto;max-width: 768px;}
        .pagerlist ul{list-style:none;}
        .pagerlist li{clear:both;line-height:1.8em;border:none;border-bottom:1px dotted #eaeaea;}
        .pagerlist li em{float:right;}
        .pagerlist li i{color:#999999;padding-left:8px;}
        .rule_item{border:1px solid #E1E1E1; border-radius:3px; margin:20px; background-color:white;}
        .rule_content{border-bottom:1px solid #E1E1E1; padding:0 10px; height:30px; line-height:30px; background:#EEE;}
        .rule_content .data{font-weight:bold; color:#333;}
        .rule_desc{padding:10px 10px 7px 10px; border-bottom:2px #E1E1E1 solid; border-top:1px #FFF solid; background:#F9F9F9;}
        .rule_kw{display:inline-block; padding:0 10px; border-radius:3px; margin-bottom:3px; background:#E7E7E7; color:#888;}
        .fl{ float:left;}
        .fr{ float:right;}
        .clearfix{ clear:both;}
        .news{width:678px;min-height:757px;float:left;padding:15px;}
        .news li{width:665px;float:left;line-height:26px;background:url(../img/dian_2.gif) no-repeat left 11px;padding-left:13px;}
        .news li a{float:left;font-size:14px;color:#333333;}
        .news li em{float:right;color:#999999;}
        .news ul{width:678px;float:left;margin-top:5px;padding-bottom:5px;background:url(../img/dian_1.gif) repeat-x left bottom;}
        .page{width:678px;float:left;padding:10px 0px 23px 0px;text-align:center;}
        .page a{margin:0px 2px;border:1px solid #CCDBE4;line-height:22px;color:#1044BA;display:inline-block;padding:0px 6px;}
        .page span{margin:0px 2px;border:1px solid #CCDBE4;line-height:22px;color:#DBE1E6;display:inline-block;padding:0px 6px;}
        .page em{margin:0px 2px;line-height:22px;color:#333333;display:inline-block;padding:0px 6px;}
    </style>
</head>
<body>
    <div class="container-narrow">
      <h3 style=" font-family:微软雅黑;">自动处理规则&nbsp;<font  style=" font-size:12px;">【&nbsp;<a href="HandleRuleForm.aspx" style=" font-size:12px;">添加自定义规则</a>&nbsp;.&nbsp;<a href="HandleRuleList.aspx?level=1" style=" font-size:12px;">系统规则</a>&nbsp;】</font></h3>
    </div>
    <div class="pagerlist">
        <%=_ListStr%>
        <%=_PageBar%>
    </div>
    <script src="/static/jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
        function SetWelcome(key) {
            if (confirm("您确定要将当前规则设置为关注时的推送内容吗?")) {
                $.post("HandleRuleSet.aspx", { "do": "Welcome", "Id": key }, function (json) {
                    if (json.success) {
                        parent.showTips('Success,规则已设为关注时的推送内容!', 4);
                    } else {
                        parent.showTips(json.msg, 5);
                    }
                }, "json");
            }
        }
        function SetDefault(key) {
            if (confirm("您确定要将当前规则设置为默认回复内容吗?")) {
                $.post("HandleRuleSet.aspx", { "do": "Default", "Id": key }, function (json) {
                    if (json.success) {
                        parent.showTips('Success,规则已设为默认回复内容!', 4);
                    } else {
                        parent.showTips(json.msg, 5);
                    }
                }, "json");
            }
        }
        function Del(key) {
            if (confirm("删除操作不可恢复，确认要删除吗?")) {
                $.post("HandleRuleSet.aspx", { "do": "Del", "Id": key }, function (json) {
                    if (json.success) {
                        parent.showTips('Success,操作保存成功!', 4);
                        self.location.reload();
                    } else {
                        parent.showTips(json.msg, 5);
                    }
                }, "json");
            }
        }
    </script>
</body>
</html>
