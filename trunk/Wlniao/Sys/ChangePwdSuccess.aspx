<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePwdSuccess.aspx.cs" Inherits="Wlniao.Sys.ChangePwdSuccess" %>
<!DOCTYPE html>
<html lang="zh">
<head>
    <title>修改成功</title>
    <link href="../static/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../static/jquery.js" type="text/javascript"></script>
    <script src="../static/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
</head>
<body>
<div style="margin-top:100px">  
    <div class="well" style="width:580px;margin:0px auto;">
        <div class="row">
            <div class="span7">
                <h3>成功啦</h3>
                <p class="lead">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;恭喜你，您的密码已经修改成功，下次登录系统请使用新的帐号及密码！</p>
                <div class="control-group">
                    <div class="controls">
                        <button type="submit" class="btn btn-primary" onclick="GoBack();" style=" float:right;">立即返回</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
 </div>
 <script type="text/javascript">
     function GoBack() {
         top.location.href = '../default.aspx';
     }
 </script>
</body>
</html>
