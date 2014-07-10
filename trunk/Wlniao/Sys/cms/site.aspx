<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="site.aspx.cs" Inherits="Wlniao.Cms.Site" %>
<!DOCTYPE html>
<html lang="zh">
<head>
    <title>微网站设置</title>
    <link href="/static/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="/static/jquery.js" type="text/javascript"></script>
    <script src="/static/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <style type="text/css">
        .container-narrow {margin: 0 auto;padding:0px 12px 0px 12px;}
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

    <form action="" method="post" class=" form-horizontal" style="margin:0px auto;">
                <div class="control-group">
                    <label class="control-label">微网站名称</label>
                    <div class="controls">
                        <input type="text" style=" width:320px;" name="sitename" value="<%=sitename %>" placeholder="填写你微网站的名称" />
                        <p style=" font-size:12px; color:#666666;">填写你微网站的名称</p>
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label">触发关键字</label>
                    <div class="controls" style=" width:320px;">
                        <input type="text" style=" width:60px;" name="keyword" value="<%=keyword %>" placeholder="触发关键字" />&nbsp;&nbsp;&nbsp;<input type="checkbox" name="setwelcome" style=" vertical-align:baseline;" /> 设为被关注时欢迎内容
                        <p style=" font-size:12px; color:#666666;">用户发送此内容用于获取微网站连接</p>
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label">消息标题</label>
                    <div class="controls">
                        <input type="text" style=" width:320px;" name="msgtitle" value="<%=msgtitle %>" placeholder="欢迎进入我们的微网站" />
                        <p style=" font-size:12px; color:#666666;">填写你微网站的名称</p>
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label"><br/>图文消息封面</label>
                    <div class="controls">
                        <input type="text" style=" width:320px; display:none;" id="logosrc" name="logosrc" value="<%=logosrc %>" placeholder="图文消息封面图片地址" onchange="picChange();" />
                        <img id="imgPicUrl" src="<%=((!string.IsNullOrEmpty(logosrc) && logosrc != "#" && !logosrc.StartsWith("http://")) ? _dataurl + logosrc : logosrc) %>" style=" max-width:320px; border:1px solid #999; display:none; margin:12px 0px;" /><br/>
                        <div><div id="fileQueue"></div><input type="file" name="uploadify" id="uploadify" /></div>
                    </div>
                </div>
                <div class="control-group">
                    <div class="controls">
                        <div><font color="red"><%=msg%></font></div>                    
                        <input name="method" type="hidden" value="save" />
                        <button type="submit" class="btn btn-primary">保存设置</button>
                    </div>
                </div>
    </form>

          </div>
      </div>
</div>
</body>
<script src="/static/wln.js" type="text/javascript"></script>
<link href="/static/uploadify/uploadify.css" rel="stylesheet" type="text/css" />
<script src="/static/uploadify/jquery.uploadify.js" type="text/javascript"></script>
<script type="text/javascript">
    function picChange() {
        var src = $('#logosrc').val();
        if (src) {
            if (src.substring(0, 7) == 'http://') {
                $('#imgPicUrl').attr('src', src).show();
            } else {
                $('#imgPicUrl').attr('src', "<%=_dataurl %>" + src).show();
            } 
        }
    }
    picChange();
    $(function () {
        $("#uploadify").uploadify({
            'uploader': '/upload.aspx?filetype=pic&account=<%=_account %>',
            'formData': {},
            'buttonText': '选择封面图片',
            'auto': true,
            'multi': true,
            'onUploadSuccess': function (e, response, data) {
                var stringArray = response.split("|");
                if (stringArray[0] == "1") {
                    $('#imgPicUrl').attr('src', "<%=_dataurl %>/" + stringArray[1]).show();
                    $('#logosrc').val("/" + stringArray[1]);
                    parent.showTips('封面图片上传成功!', 4);
                }
                else {
                    alert(stringArray[2]);
                }
            }
        });
        $.get('sitejs.aspx?curr=base', function (data) {
            $('#leftBtn').html(data);
        });
    });
</script>
<%=_script%>
</html>
