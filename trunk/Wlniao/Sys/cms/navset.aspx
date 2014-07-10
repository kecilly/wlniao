<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="navset.aspx.cs" Inherits="Wlniao.CMS.NavSet" %>
<!DOCTYPE html>
<html lang="zh">
<head>
    <title>微导航设置</title>
    <link href="/static/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="/static/jquery.js" type="text/javascript"></script>
     <link href="/static/uploadify/uploadify.css" rel="stylesheet" type="text/css" />
    <script src="/static/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <style type="text/css">
        .container-narrow {margin: 0 auto;padding:0px 5px 0px 12px;}
        .span3 .btn-group{ height:500px; width:168px;}
        .span3 .btn{ width:168px; text-align:left;}
        .newsli{ background:#fafafa; clear:both; display:block; padding:8px 5px 1px 5px; margin:5px 0px;}
        .newslipic{float:left; height:100px;width:100px;border: 1px solid #B8B8B8;overflow: hidden;}
        .newslipic p{text-shadow: 0 1px 1px white;background: #F5F6F7;display: block;text-align: center;color: #666;letter-spacing: 5px;font-weight: bold;font-size: 22px;line-height: 100px;}
        .newslipic img{color: transparent; background-color:#999999; height:100px;width:100px;font-size: 0;vertical-align: middle;-ms-interpolation-mode: bicubic;}
        .newslitxt{ margin:0px 0px 0px 120px;}
        .newslitxt h4{ float:right; font-size:14px;}
        .newslitxt div{  width:500px;word-wrap:break-word;}
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
          <form method="post">
            <input name="method" type="hidden" value="save" />
            <div>
			    <input name="submit" type="submit" value="保存配置" class="mt10 btn btn-primary grid-2 alpha" style="float:right; margin:5px;">
			    <input type="button" value="效果预览" onclick="openWeb()" class="mt10 btn grid-2 alpha" style="float:right; margin:5px;">
            </div> 
            <div style=" clear:both; min-height:120px;"><%=_liststr%></div>
            <div>
			    <input name="submit" type="submit" value="保存配置" class="mt10 btn btn-primary grid-2 alpha" style="float:right;">
            </div> 
          </form>
          <br />
          <br />
          </div>
      </div>
    </div>
    <script src="/static/wln.js" type="text/javascript"></script>
    <script src="/static/uploadify/jquery.uploadify.js" type="text/javascript"></script>
    <script type="text/javascript">
      
        function uploadProgressUpload() {
        }
        <%=_script %>
        $(function () {
            $('.navimg').each(function () {
                if ($(this).attr('src') != '' && $(this).attr('src') != '#') {
                    $(this).parent().prev().hide();
                }
            });
        });
        function openWeb(){
            if($.browser.msie) { 
                alert("您使用的是低版本的IE或IE内核浏览器，预览过程中可能会出现错位现象，建议您先升级浏览器!"); 
            } 
            open('/mobile.aspx?a=<%=_account %>');
        }
        function selChange(e){
            if($(e).val()){
                $(e).prev().val($(e).val());            
                $(e).parent().val($(e).val());
                $(e).parent().parent().prev().find('p').first().hide();
                $(e).parent().parent().prev().find('.navimg').first().attr('src',$(e).val());
            }else{
                $(e).parent().parent().prev().find('p').first().show();
            }
        }
        function typeChange(e){
        if($(e).val()){
            //$(e).prev().val($(e).val());
            }
        }
        $(function () {
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
</body>
</html>
