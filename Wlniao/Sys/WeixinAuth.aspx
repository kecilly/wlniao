<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeixinAuth.aspx.cs" Inherits="Wlniao.Sys.WeixinAuth" %>
<!DOCTYPE html>
<html lang="zh">
<head>
    <title>公众帐号授权设置</title>
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
    <h3 style="font-family:微软雅黑;" ><a href="Setting.aspx" style=" font-size:12px;">帐号接入信息</a>&nbsp;授权设置</h3> 
</div>
<div style="margin-top:18px"> 
    <form action="#" class="well form-horizontal" style="width:680px;margin:0px auto;">
        <div class="row">
            <div class="span7">
                <div class="control-group">
                    在使用部分高级功能前，需要在此处授权
                </div>
                <div class="control-group">
                    <label class="control-label">帐号类型</label>
                    <div class="controls">
                        <select id="MPType">
                            <option value="0">订阅号</option>
                            <option value="1">普通服务号</option>
                            <option value="2">已认证服务号</option>
                        </select>
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label">AppId</label>
                    <div class="controls">
                        <input type="text" style=" width:360px;" id="AppId" value="<%=AppId %>"  />
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label">AppSecret</label>
                    <div class="controls">
                        <input type="password" style=" width:360px;" id="AppSecret" value="<%=AppSecret %>"  />
                    </div>
                </div>     
                <div class="control-group">
                    <label class="control-label"><font color="red">*</font></label>
                    <div class="controls">
                        <div class="notice">1. 要在微信公众平台“开发模式”下使用自定义菜单，首先要在公众平台申请自定义菜单使用的AppId和AppSecret；</div>
                    </div>
                </div>
                <div class="control-group">
                    <div class="controls">
                        <a href="javascript:SetAppIdAndSecret();" class="btn btn-primary">保存设置</a>
                        <a href="javascript:Clear();" class="btn">取消</a>
                    </div>
                </div>
            </div>
        </div>
    </form>
 </div>
</body>
<script type="text/javascript">
    // 保存设置
    function SetAppIdAndSecret() {
        $.getJSON("/Rest/WX/SetAppIdAndSecret.aspx", { "MPType": $('#MPType').val(), "AppId": $('#AppId').val(), "AppSecret": $('#AppSecret').val() }, function (json) {
            if (json.success) {
                parent.showTips('Success，授权信息已通过验证并保存!', 4);
            } else {
                parent.showTips(json.msg, 5);
            }
        });
    }
    function Clear() {
        if (confirm('授权信息清除后部分功能将无法使用，确定要清除吗？')) {
            $.getJSON("/Rest/WX/SetAppIdAndSecret.aspx", { "Clear": "true" }, function (json) {
                if (json.success) {
                    $('#AppId').val('');
                    $('#AppSecret').val('');
                    parent.showTips('Success，授权信息已清除!', 4);
                } else {
                    parent.showTips(json.msg, 5);
                }
            });
        }
    }
    $('#MPType').val('<%=MPType %>');
</script>
</html>
