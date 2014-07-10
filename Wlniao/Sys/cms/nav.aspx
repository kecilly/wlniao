<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="nav.aspx.cs" ValidateRequest="false" Inherits="Wlniao.CMS.Nav" %>
<!DOCTYPE html>
<html lang="zh">
<head>
    <title>导航管理</title>
    <link href="/static/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="/static/jquery.js" type="text/javascript"></script>
    <script src="/static/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <style type="text/css">
        .container-narrow {margin: 0 auto;padding:0px 5px 0px 12px;}
        .span3 .btn-group{ height:500px; width:168px;}
        .span3 .btn{ width:168px; text-align:left;}
    </style>
</head>
<body>
<div class="container-narrow">
    <h3 style="font-family:微软雅黑;" >微网站设置 &nbsp;<a href="cmsclass.aspx" style=" font-size:12px;">内容管理</a></h3>
      <div class="row-fluid">
          <div class="span3" style=" margin-left:15px;">
            <div class="btn-group btn-group-vertical" id="leftBtn">
            </div>
          </div>
          <div class="span9" style=" margin-left:-10px;">
            <form action="nav.aspx" method="post" class="well form-horizontal" style="margin:0px auto;">
                <div class="control-group">
                    <label class="control-label">微导航样式</label>
                    <div class="controls">
                        <select name="style" id="style" onchange="styleChange();">
                        <%=_styleList%>
                        </select>
                        <p style=" font-size:12px; color:#666666;">请选择您要使用的微导航效果</p>
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label">&nbsp;</label>
                    <div class="controls"><img id="thumbPic" src="#" style=" display:none;" /></div>
                </div>
                <div class="control-group">
                    <label class="control-label">&nbsp;</label>
                    <div class="controls"><font color="red"><%=msg%></font></div>
                </div>
                <div class="control-group">
                    <div class="controls">
                        <button type="submit" class="btn btn-primary">保存设置</button>
                        <a href="navset.aspx" class="btn">微导航按钮设置</a>
                    </div>
                </div>
            </form>
          </div>
      </div>
 </div>
</body>
</html>
<script>
    function styleChange() {
        var style = $('#style').val();
        if (style && style != '0') {
            $('#thumbPic').attr('src', '<%=_dataurl %>/BaseData/Style/nav' + style + '.png');
            $('#thumbPic').show();
        } else {
            $('#thumbPic').hide();
        }
    }
    $(function () {
        styleChange();
        $.get('sitejs.aspx?curr=mininav', function (data) {
            $('#leftBtn').html(data);
        });
    });
    function Goto(url) {
        window.location.href = url;
        return false;
    }
</script>
<%=_scripttips%>
