<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Setting.aspx.cs" Inherits="Wlniao.Sys.Setting" %>
<!DOCTYPE html>
<html lang="zh">
<head>
    <title>帐号接入信息</title>
    <link href="../static/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../static/jquery.js" type="text/javascript"></script>
    <script src="../static/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../static/zclip/jquery.zclip.min.js" type="text/javascript"></script>
    <style type="text/css">
        .container-narrow {margin: 0 auto;padding:0px 12px 0px 12px;}
    </style>
</head>
<body>
<div class="container-narrow">
    <h3 style="font-family:微软雅黑;" >帐号接入信息&nbsp;<a href="WeixinAuth.aspx" style=" font-size:12px;">授权设置</a></h3>
</div>
<div class="" style="margin-top:18px">  
    <form action="#" class="well form-horizontal" style="width:680px;margin:0px auto;">
        <div class="row">
            <div class="span7">
                <div class="control-group">
                    <label class="control-label">公众号名称</label>
                    <div class="controls">
                        <input type="text" style=" width:360px;" id="WeixinName" value="<%=WeixinName %>" placeholder="填写你微信公众帐号的名称" />
                        <p style=" font-size:12px; color:#666666;">填写你微信公众帐号的名称</p>
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label"><font color="red">接口URL</font></label>
                    <div class="controls" id="apiurlwx">
                        <input type="text" id="WeixinApi" style=" width:292px;" value="http://<%=_website%>/wxapi.aspx?a=<%=_CurrentAccount.AccountUserName%>" readonly="readonly" /><button class="btn">复制</button>
                        <p style=" font-size:12px; color:#666666;">设置“微信公众平台接口”配置信息中的接口地址</p>
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label"><font color="red">Token</font></label>
                    <div class="controls" id="token">
                        <input type="text" style=" width:292px;" id="WeixinToken" value="<%=WeixinToken %>" placeholder="公众平台的Token" /><button class="btn">复制</button>
                        <p style=" font-size:12px; color:#666666;">Token用于数据加密,长度为3到32位英文或者数字.请妥善保管, Token 泄露将可能被窃取或篡改微信平台的操作数据.<a href="javascript:SetToken();">生成新的</a></p>
                    </div>
                </div>
                <div class="control-group">
                    <div class="controls">
                        <a href="javascript:SetNameAndToken();" class="btn btn-primary">保存设置</a>
                        <a href="javascript:Clear();" class="btn">取消</a>
                    </div>
                </div>
            </div>
        </div>
    </form>
 </div>
</body>
<script type="text/javascript">
    $(function () {
        $("#apiurlwx button").zclip({
            path: '/static/zclip/ZeroClipboard.swf',
            copy: $('#WeixinApi').val()
        });
        $("#token button").zclip({
            path: '/static/zclip/ZeroClipboard.swf',
            copy: $('#WeixinToken').val()
        });
    });
    // 生成新的Token
    function SetToken() {
        $.getJSON("/Rest/WX/SetToken.aspx", {}, function (json) {
            if (json.success) {
                $("#token input").val(json.token);
                parent.showTips('已为您生成了一个新的Token，别忘记保存哦!', 1);
            } else {
                parent.showTips(json.msg, 5);
            }
        });
    }
    // 保存设置
    function SetNameAndToken() {
        $.getJSON("/Rest/WX/SetNameAndToken.aspx", { "WeixinName": $('#WeixinName').val(), "WeixinToken": $('#WeixinToken').val() }, function (json) {
            if (json.success) {
                parent.showTips('Success，您的设置已保存!', 4);
            } else {
                parent.showTips(json.msg, 5);
            }
        });
    }
    function Clear() {
        $("#WeixinName").val('<%=WeixinName %>');
        $("#WeixinToken").val('<%=WeixinToken %>');
    }
</script>
</html>
