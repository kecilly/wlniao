<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="site_banner.aspx.cs" Inherits="Wlniao.Cms.SiteBanner" %>
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
                    <label class="control-label">Banner 一图</label>
                    <div class="controls">
                        <div><div id="fileQueue1"></div><input type="file" name="uploadify" id="uploadify1" /></div>
                        <img id="imgPicUrl1" src="<%=string.IsNullOrEmpty(banner1src) ?"":_dataurl +"/"+  banner1src %>" style=" max-width:320px; border:1px solid #999; display:none; margin:12px 0px;" />
                        <div id="del01" style=" display:none;"><a onclick="return confirm('您确定要删除当前图片吗？');return false;" href="site_banner.aspx?method=del1" class="btn">删除</a></div>
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label">Banner 二图</label>
                    <div class="controls">
                        <div><div id="fileQueue2"></div><input type="file" name="uploadify" id="uploadify2" /></div>
                        <img id="imgPicUrl2" src="<%=string.IsNullOrEmpty(banner2src) ?"":_dataurl +"/"+ banner2src %>" style=" max-width:320px; border:1px solid #999; display:none; margin:12px 0px;" />
                        <div id="del02" style=" display:none;"><a onclick="return confirm('您确定要删除当前图片吗？');return false;" href="site_banner.aspx?method=del2" class="btn">删除</a></div>
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label">Banner 三图</label>
                    <div class="controls">
                        <div><div id="fileQueue3"></div><input type="file" name="uploadify" id="uploadify3" /></div>
                        <img id="imgPicUrl3" src="<%=string.IsNullOrEmpty(banner3src) ?"":_dataurl +"/"+ banner3src %>" style=" max-width:320px; border:1px solid #999; display:none; margin:12px 0px;" />
                        <div id="del03" style=" display:none;"><a onclick="return confirm('您确定要删除当前图片吗？');return false;" href="site_banner.aspx?method=del3" class="btn">删除</a></div>
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label">Banner 四图</label>
                    <div class="controls">
                        <div><div id="fileQueue4"></div><input type="file" name="uploadify" id="uploadify4" /></div>
                        <img id="imgPicUrl4" src="<%=string.IsNullOrEmpty(banner4src) ?"":_dataurl +"/"+ banner4src %>" style=" max-width:320px; border:1px solid #999; display:none; margin:12px 0px;" />
                        <div id="del04" style=" display:none;"><a onclick="return confirm('您确定要删除当前图片吗？');return false;" href="site_banner.aspx?method=del4" class="btn">删除</a></div>
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label">Banner 五图</label>
                    <div class="controls">
                        <div><div id="fileQueue5"></div><input type="file" name="uploadify" id="uploadify5" /></div>
                        <img id="imgPicUrl5" src="<%=string.IsNullOrEmpty(banner5src) ?"":_dataurl +"/"+ banner5src %>" style=" max-width:320px; border:1px solid #999; display:none; margin:12px 0px;" />
                        <div id="del05" style=" display:none;"><a onclick="return confirm('您确定要删除当前图片吗？');return false;" href="site_banner.aspx?method=del5" class="btn">删除</a></div>
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
        $(function () {

            $("#uploadify1").uploadify({
                'uploader': '/sys/cms/site_logoup.aspx?filetype=pic&account=<%=_account %>',
                'formData': { 'picname': 'banner1' },
                'buttonText': '选择 第一张 Banner',
                'auto': true,
                'multi': true,
                'onUploadSuccess': function (e, response, data) {
                    var stringArray = response.split("|");
                    if (stringArray[0] == "1") {
                        var now = new Date();
                        var number = now.getYear().toString() + now.getMonth().toString() + now.getDate().toString() + now.getHours().toString() + now.getMinutes().toString() + now.getSeconds().toString();
                        $('#imgPicUrl1').attr('src', "<%=_dataurl %>/" + stringArray[1] + "?v=" + number).show();
                        parent.showTips('图片上传成功!', 4);
                    }
                    else {
                        alert(stringArray[2]);
                    }
                }
            });
            $("#uploadify2").uploadify({
                'uploader': '/sys/cms/site_logoup.aspx?filetype=pic&account=<%=_account %>',
                'formData': { 'picname': 'banner2' },
                'buttonText': '选择 第二张 Banner',
                'auto': true,
                'multi': true,
                'onUploadSuccess': function (e, response, data) {
                    var stringArray = response.split("|");
                    if (stringArray[0] == "1") {
                        var now = new Date();
                        var number = now.getYear().toString() + now.getMonth().toString() + now.getDate().toString() + now.getHours().toString() + now.getMinutes().toString() + now.getSeconds().toString();
                        $('#imgPicUrl2').attr('src', "<%=_dataurl %>/" + stringArray[1] + "?v=" + number).show();
                        parent.showTips('图片上传成功!', 4);
                    }
                    else {
                        alert(stringArray[2]);
                    }
                }
            });
            $("#uploadify3").uploadify({
                'uploader': '/sys/cms/site_logoup.aspx?filetype=pic&account=<%=_account %>',
                'formData': { 'picname': 'banner3' },
                'buttonText': '选择 第三张 Banner',
                'auto': true,
                'multi': true,
                'onUploadSuccess': function (e, response, data) {
                    var stringArray = response.split("|");
                    if (stringArray[0] == "1") {
                        var now = new Date();
                        var number = now.getYear().toString() + now.getMonth().toString() + now.getDate().toString() + now.getHours().toString() + now.getMinutes().toString() + now.getSeconds().toString();
                        $('#imgPicUrl3').attr('src', "<%=_dataurl %>/" + stringArray[1] + "?v=" + number).show();
                        parent.showTips('图片上传成功!', 4);
                    }
                    else {
                        alert(stringArray[2]);
                    }
                }
            });
            $("#uploadify4").uploadify({
                'uploader': '/sys/cms/site_logoup.aspx?filetype=pic&account=<%=_account %>',
                'formData': { 'picname': 'banner4' },
                'buttonText': '选择 第四张 Banner',
                'auto': true,
                'multi': true,
                'onUploadSuccess': function (e, response, data) {
                    var stringArray = response.split("|");
                    if (stringArray[0] == "1") {
                        var now = new Date();
                        var number = now.getYear().toString() + now.getMonth().toString() + now.getDate().toString() + now.getHours().toString() + now.getMinutes().toString() + now.getSeconds().toString();
                        $('#imgPicUrl4').attr('src', "<%=_dataurl %>/" + stringArray[1] + "?v=" + number).show();
                        parent.showTips('图片上传成功!', 4);
                    }
                    else {
                        alert(stringArray[2]);
                    }
                }
            });
            $("#uploadify5").uploadify({
                'uploader': '/sys/cms/site_logoup.aspx?filetype=pic&account=<%=_account %>',
                'formData': { 'picname': 'banner5' },
                'buttonText': '选择 第五张 Banner',
                'auto': true,
                'multi': true,
                'onUploadSuccess': function (e, response, data) {
                    var stringArray = response.split("|");
                    if (stringArray[0] == "1") {
                        var now = new Date();
                        var number = now.getYear().toString() + now.getMonth().toString() + now.getDate().toString() + now.getHours().toString() + now.getMinutes().toString() + now.getSeconds().toString();
                        $('#imgPicUrl5').attr('src', "<%=_dataurl %>/" + stringArray[1] + "?v=" + number).show();
                        parent.showTips('图片上传成功!', 4);
                    }
                    else {
                        alert(stringArray[2]);
                    }
                }
            });

            $.get('sitejs.aspx?curr=banner', function (data) {
                $('#leftBtn').html(data);
            });


        });
        function picChange() {
            if ($('#imgPicUrl1').attr('src')) {
                $('#imgPicUrl1').show();
                $('#del01').show();
            }
            if ($('#imgPicUrl2').attr('src')) {
                $('#imgPicUrl2').show();
                $('#del02').show();
            }
            if ($('#imgPicUrl3').attr('src')) {
                $('#imgPicUrl3').show();
                $('#del03').show();
            }
            if ($('#imgPicUrl4').attr('src')) {
                $('#imgPicUrl4').show();
                $('#del04').show();
            }
            if ($('#imgPicUrl5').attr('src')) {
                $('#imgPicUrl5').show();
                $('#del05').show();
            }
        }
        picChange();
    </script>
    <%=_script%>
</html>
