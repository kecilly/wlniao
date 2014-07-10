<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="site_logo.aspx.cs" Inherits="Wlniao.Cms.SiteLogo" %>
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

            <form action="" method="post" class="form-horizontal" style="margin:0px auto;">
                <div class="control-group">
                    <label class="control-label">LOGO图片</label>
                    <div class="controls">
                        <div><div id="fileQueue"></div><input type="file" name="uploadify" id="uploadify" /></div>
                        <input type="text" style=" width:320px; display:none;" id="logosrc" name="logosrc" value="<%=logosrc %>" placeholder="网站LOGO图片地址" onchange="picChange();" />
                        <img id="imgPicUrl" src="<%=((!string.IsNullOrEmpty(logosrc) && logosrc != "#" && !logosrc.StartsWith("http://")) ? _dataurl + logosrc : logosrc) %>" style=" max-width:320px; border:1px solid #999; display:none; margin:12px 0px;" />
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label">&nbsp;</label>
                    <div class="controls"><font color="red"><%=msg%></font></div>
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
        $(function () {
            $("#uploadify").uploadify({
                'uploader': '/sys/cms/site_logoup.aspx?filetype=pic&account=<%=_account %>',
                'formData': {},
                'buttonText': '选择LOGO图片',
                'auto': true,
                'multi': true,
                'onUploadSuccess': function (e, response, data) {
                    var stringArray = response.split("|");
                    if (stringArray[0] == "1") {
                        var now = new Date();
                        var number = now.getYear().toString() + now.getMonth().toString() + now.getDate().toString() + now.getHours().toString() + now.getMinutes().toString() + now.getSeconds().toString();
                        $('#imgPicUrl').attr('src', "<%=_dataurl %>/" + stringArray[1] + "?v=" + number).show();
                        $('#logosrc').val("/" + stringArray[1]);
                        parent.showTips('LOGO图片上传成功!', 4);
                    }
                    else {
                        alert(stringArray[2]);
                    }
                }
            });

            $.get('sitejs.aspx?curr=logo', function (data) {
                $('#leftBtn').html(data);
            });


        });
        function picChange() {
            var src = $('#logosrc').val();
            if (src) {
                var now = new Date();
                var number = now.getYear().toString() + now.getMonth().toString() + now.getDate().toString() + now.getHours().toString() + now.getMinutes().toString() + now.getSeconds().toString();
                if (src.substring(0, 7) == 'http://') {
                    $('#imgPicUrl').attr('src', src + "?v=" + number).show();
                } else {
                    $('#imgPicUrl').attr('src', "<%=_dataurl %>/" + src + "?v=" + number).show();
                }
            }
        }
        picChange();
    </script>
    <%=_script%>
</html>
