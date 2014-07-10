<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="site_copyright.aspx.cs" Inherits="Wlniao.Cms.SiteCopyRight" %>
<!DOCTYPE html>
<html lang="zh">
<head>
    <title>微网站设置</title>
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
            <form action="" method="post" class="well form-horizontal" style="margin:0px auto;">
                        <br />
                        <div class="control-group">
                            <label class="control-label">页面底部文字</label>
                            <div class="controls" style=" width:320px;">
                                <input type="text" style=" width:320px;" name="copyright" value="<%=copyright %>" placeholder="" />
                                <p style=" font-size:12px; color:#666666;">显示在页面底部,不能超过30个字</p>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label">&nbsp;</label>
                            <div class="controls"><font color="red"><%=msg%></font></div>
                        </div>
                        <div class="control-group">
                            <div class="controls">                    
                                <input name="method" type="hidden" value="save" />
                                <button type="submit" class="btn btn-warning">保存设置</button>
                            </div>
                        </div>
            </form>
          </div>
      </div>
</div>
</body>
<script src="/static/wln.js" type="text/javascript"></script>
<script src="/static/SWFUpload/swfupload.js" type="text/javascript"></script>
<script type="text/javascript">
    $(function () {
        $.get('sitejs.aspx?curr=copyright', function (data) {
            $('#leftBtn').html(data);
        });
    });
</script>
<%=_script%>
</html>
