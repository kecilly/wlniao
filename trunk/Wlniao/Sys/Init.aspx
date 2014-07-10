<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Init.aspx.cs" Inherits="Wlniao.Sys.Init" %>
<!DOCTYPE html>
<html lang="zh">
<head>
    <title>您的帐号需要初始化</title>
    <link href="../static/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../static/jquery.js" type="text/javascript"></script>
    <script src="../static/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
</head>
<body>
<div class="" style="margin-top:88px">  
    <form class="well form-horizontal" style="width:580px;margin:0px auto;">
        <div class="row">
            <div class="span7">
                <h3>第二步，帐号接入</h3>
                <p class="lead">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;您的帐号已成功，请前往 <a href="http://mp.weixin.qq.com" target="_blank"><b>公众平台</b></a> 或 <a href="http://mp.weixin.qq.com/debug/cgi-bin/sandbox?t=sandbox/login" target="_blank"><b>申请测试帐号</b></a>并使用以下内容接入认证！</p>
                <div style=" background-color:#fafafa; padding:12px 18px; margin-bottom:21px; border:1px dashed #cccccc; ">
                    <h1 style=" clear:both; display:inline-block; width:90px; text-align:right; font-family:微软雅黑; font-size:14px; line-height:1em; margin:12px 8px 8px 8px;">接口地址</h1>
                    <span><a id="zclipApi" style="color:Black;">http://<%=_website%>/wxapi.aspx?a=<%=_CurrentAccount.AccountUserName%></a>&nbsp;<button id="zclipApiBtn" class="btn" style=" height:18px; line-height:12px; padding:2px 8px;">复制</button></span>
                    <br />
                    <h1 style=" clear:both; display:inline-block; width:90px; text-align:right; font-family:微软雅黑; font-size:14px; line-height:1em; margin:12px 8px 8px 8px;">Token</h1>
                    <span><a id="zclipToken" style="color:Black;"><%=weixintoken%></a>&nbsp;<button id="zclipTokenBtn" class="btn" style=" height:18px; line-height:12px; padding:2px 8px;">复制</button></span>
                    <br />
                </div>
                <h3>第三步，初始化</h3>
                <p class="lead">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;请用任意帐号发送消息<font style="font-weight:bold;color:Red;"><%=verifycontent%></font>至您的公众帐号！</p>
                <%if (showMsg)
                  { %>
                  <div style="text-align:center;">
                    <font style="font-size:12px;color:Red;">您尚未发送初始化信息，请先发送后再点击完成按钮!</font>
                  </div>
                <%} %>
                <div class="control-group">
                    <div class="controls">
                        <button type="button" class="btn btn-primary" onclick="Goback();" style=" float:right;">操作完成</button>
                    </div>
                </div>
            </div>
        </div>
    </form>
 </div>
 <script src="../static/zclip/jquery.zclip.min.js" type="text/javascript"></script>
 <script type="text/javascript">
     function Goback() {
         top.location.href = '/default.aspx';
     }
     $(function () {
         $("#zclipApiBtn").zclip({
             path: '/static/zclip/ZeroClipboard.swf',
             copy: $('#zclipApi').html()
         });
         $("#zclipTokenBtn").zclip({
             path: '/static/zclip/ZeroClipboard.swf',
             copy: $('#zclipToken').html()
         });
     });
 </script>
</body>
</html>
