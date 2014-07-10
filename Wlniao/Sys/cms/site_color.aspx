<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="site_color.aspx.cs" Inherits="Wlniao.Cms.SiteColor" %>
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
                            <label class="control-label">当前主题色</label>
                            <div class="controls" style=" width:320px;">
                                <input type="hidden" id="color" name="color" value="<%=color %>" />
                                <div id="colorbg" style=" width:288px; text-align:center; padding:5px; background-color:<%=color %>"><span id="colortxt">文本颜色</span></div>
                                <p style=" font-size:12px; color:#666666;">微网站的主题颜色设置</p>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label">常用颜色</label>
                            <div class="controls" style=" width:320px;">
                                <button type="button" style=" margin:5px; padding:5px 15px; background-color:#111111;color:#ffffff;" onclick="setColor('#111111;#ffffff')">文本</button>
                                <button type="button" style=" margin:5px; padding:5px 15px; background-color:#666666;" onclick="setColor('#666666')">文本</button>
                                <button type="button" style=" margin:5px; padding:5px 15px; background-color:#EEEEEE;" onclick="setColor('#EEEEEE')">文本</button>
                                <button type="button" style=" margin:5px; padding:5px 15px; background-color:#FFFFFF;" onclick="setColor('#FFFFFF')">文本</button>
                                <button type="button" style=" margin:5px; padding:5px 15px; background-color:#E40684;" onclick="setColor('#E40684')">文本</button>
                                <button type="button" style=" margin:5px; padding:5px 15px; background-color:#51A351;" onclick="setColor('#51A351')">文本</button>
                                <button type="button" style=" margin:5px; padding:5px 15px; background-color:#F89406;" onclick="setColor('#F89406')">文本</button>
                                <button type="button" style=" margin:5px; padding:5px 15px; background-color:#06AFFF;" onclick="setColor('#06AFFF')">文本</button>
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
<script src="../static/wln.js" type="text/javascript"></script>
<script src="../static/SWFUpload/swfupload.js" type="text/javascript"></script>
<script type="text/javascript">
    $(function () {
        $.get('sitejs.aspx?curr=color', function (data) {
            $('#leftBtn').html(data);
        });
    });
    function setColor(colorbg, colortxt) {
        if (colortxt) {
            $('#color').val(colorbg + ";" + colortxt);
        } else {
            $('#color').val(colorbg);
        }
        showColor();
    }
    function showColor() {
        if ($('#color').val().length > 7) {
            var colors = $('#color').val().split(';');
            $('#colorbg').css({ "backgroundColor": colors[0], "color": colors[1] });
        } else {
            $('#colorbg').css({ "backgroundColor": $('#color').val(), "color": "#111111" });
        }
    }
    showColor();
</script>
<%=_script%>
</html>
