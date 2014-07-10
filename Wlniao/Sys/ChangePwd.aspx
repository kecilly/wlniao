<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePwd.aspx.cs" Inherits="Wlniao.Sys.ChangePwd" %>
<!DOCTYPE html>
<html lang="zh">
<head>
    <title>修改密码</title>
    <link href="../static/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../static/jquery.js" type="text/javascript"></script>
    <script src="../static/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
</head>
<body>
<div class="" style="margin-top:68px">  
    <form action="changepwd.aspx" method="post" class="well" style="width:300px;margin:0px auto;">
        <h3>修改密码</h3>
        <label>旧密码：</label>
        <input type="password" name="oldpassword" value="<%=oldpassword %>" style="height:30px" class="span4" />
        <label>新密码：</label>
        <input type="password" name="newpassword" value="<%=newpassword %>" style="height:30px" class="span4" />
        <label>重复密码：</label>
        <input type="password" name="repassword" value="<%=repassword %>" style="height:30px" class="span4" />
        <div style="color:red;font-size:14px; margin-bottom:5px;"><%=msg%></div>
        <button type="submit" class="btn btn-primary">确认修改</button>
    </form>
 </div>
</body>
</html>
