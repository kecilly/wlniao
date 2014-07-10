<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="appbox.aspx.cs" Inherits="Wlniao.appbox" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>应用中心</title>
</head>
<body style="background-color:#ffffff; padding:0px 12px;">
<div style=" width:800px; float:left;">
    <iframe id="appFramePage" src="http://weback.cn/MyApps.aspx?clientaccountid=<%=_clientaccountid %>&clientaccountname=<%=_clientaccountname %>&userid=<%=_clientaccountname %>&clientid=<%=_clientid %>&sercetcheck=<%=_sercetcheck %>" frameborder="0" marginheight="0" marginwidth="0" scrolling="no" width="100%" height="300"></iframe>
</div>
<div style=" float:left;">
</div>
</body>
</html>
