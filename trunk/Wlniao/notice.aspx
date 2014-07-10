<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="notice.aspx.cs" ValidateRequest="false" Inherits="Wlniao._Notice" %>
<!DOCTYPE html>
<html lang="zh">
<head>
    <title><%=_NoticeTitle %></title>
    <link href="static/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="static/jquery.js" type="text/javascript"></script>
    <script src="static/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
</head>
<body>
    <script src="topbar.aspx" type="text/javascript"></script>
    <div class="row" style="margin:0px 60px;">
        <div class="span9" style=" border-right:1px solid #eee; min-height:568px; min-width:900px;">
            <div>
                <h1 style=" text-align:center;"><%=_NoticeTitle %></h1>
                <hr />
                <div style=" padding:5px;">
                <%=_NoticeContent%>
                </div>
            </div>
        </div>
        <div class="span3">
          <ul><%=_ListStr %></ul>          
        </div>
    </div>
</body>
</html>