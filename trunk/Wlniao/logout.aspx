<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="logout.aspx.cs" Inherits="Wlniao.Logout" %>
<!DOCTYPE html>
<html lang="zh">
<head>
    <title>已退出</title>
    <link href="../static/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../static/jquery.js" type="text/javascript"></script>
    <script src="../static/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
</head>
<body>
<div class="container" style="margin-top:100px">  
    <div class="well" style="width:580px;margin:0px auto;">
        <div class="row">
            <div class="span7">
                <h3>您已退出登录</h3>
                <p class="lead">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;您尚未登录或登录已超时，系统将在<font color="red" id="count"></font>秒后自动转向登录页面！</p>
                <div class="control-group">
                    <div class="controls">
                        <button type="submit" class="btn btn-primary" onclick="GotoLogin();" style=" float:right;">立即返回登录页面</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
 </div>
 <script type="text/javascript">
     function GotoLogin() {
         top.location.href = 'login.aspx';
     }
     window.onload = function () {
         var time = 5;
         document.getElementById('count').innerHTML = time;
         setInterval(function () {
             time = time - 1;
             if (time >= 0) {
                 document.getElementById('count').innerHTML = time;
             } else {
                 top.location.href = 'login.aspx';
             }
         }, 980);
     }
 </script>
</body>
</html>
