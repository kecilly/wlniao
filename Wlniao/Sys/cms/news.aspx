<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="news.aspx.cs" 
    ValidateRequest="false" Inherits="Wlniao.CMS.News" %>
<!DOCTYPE html>
<html lang="zh">
<head>
    <title>内容管理</title>
    <link href="/static/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="/static/jquery.js" type="text/javascript"></script>
    <script src="/static/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="/static/common.js" type="text/javascript"></script>
    <script src="/static/wln.js" type="text/javascript"></script>     
   <link href="/static/uploadify/uploadify.css" rel="stylesheet" type="text/css" />
   <script src="/static/uploadify/jquery.uploadify.js" type="text/javascript"></script>
    <script src="/static/ueditor/editor_all.js" type="text/javascript"></script>
    <script src="/static/ueditor/editor_config.js" type="text/javascript"></script>
    <style type="text/css">
        .container-narrow {margin: 0 auto;padding:0px 12px 0px 12px;}
        .span3 .btn-group{ height:700px; width:168px;}
        .span3 .btn{ width:168px;text-align:left;}
    </style>
</head>
<body>
    <div class="container-narrow">
      <h3 style=" font-family:微软雅黑;"><a href="site.aspx" style=" font-size:12px;">微网站设置</a>&nbsp;内容管理</h3>
      <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;管理网站栏目及栏目内容</p>
      
      <div class="row-fluid">
          <div class="span3" style=" margin-left:15px;">
            <div class="btn-group btn-group-vertical">
              <%=classlist%>
              <button class="btn btn-inverse" onclick="GotoList();"><i class="icon-arrow-left"></i> 返回文章列表</button>
            </div>
          </div>
          <div class="span9" style=" margin-left:-10px;">
                <div>
                    <form id="classeditform" method="post">
                        <input type="hidden" name="method" value="edit" />
                        <div style=" text-align:right;margin:12px; margin-top:-36px;">
                            <button class="btn" onclick="return GotoDel();return false;"><i class="icon-remove"></i> 删除</button>
                            <button class="btn btn-primary"><i class="icon-ok"></i> 保存</button>
                        </div>
                        <span style="width:60px; font-family:微软雅黑; display:inline-block; text-align:right; vertical-align:top;">内容标题:</span>&nbsp;<input type="text" id="newstitle" name="newstitle" class="txt grid-4 alpha pin" style=" width:377px;" value="<%=newstitle %>" />&nbsp;<span style=" line-height:21px; vertical-align:top;"><input type="checkbox" name="showinhomepage"<%if(showinhomepage=="on"){ %> checked="checked"<%} %> style=" vertical-align:top;" />在首页显示</span>
                        <div style=" line-height:8px;">&nbsp;</div>
                        <span style="width:60px; font-family:微软雅黑; display:inline-block; text-align:right; vertical-align:top;">内容简要:</span>&nbsp;<textarea id="shortcontent" name="shortcontent" class="txt grid-4 alpha pin" style=" width:477px; height:68px;"><%=shortcontent %></textarea>
                        <div style=" line-height:8px;">&nbsp;</div>
                        <span style="width:60px; font-family:微软雅黑; display:inline-block; text-align:right; vertical-align:top;">外链地址:</span>&nbsp;<input type="text" id="newsurl" name="newsurl" class="txt grid-4 alpha pin" style=" width:477px;" value="<%=newsurl %>" />
                        <div style=" line-height:8px;">&nbsp;</div>
                        <span style="width:60px; font-family:微软雅黑; display:inline-block; text-align:right; vertical-align:top;">内容图标:</span>&nbsp;<img id="imgPicUrl" src="<%=newsicons %>" style=" width:48px; display:none;" /><span id="swfUploadPic"></span><input type="hidden" name="newsicons" id="newsicons" value="<%=newsicons %>"/>
                        <div style=" line-height:8px;">&nbsp;</div>
                        <script id="ListContent" type="text/plain" name="myContent"></script>
                        <div style=" text-align:right;margin:12px;">
                            <button class="btn" onclick="return GotoDel();return false;"><i class="icon-remove"></i> 删除</button>
                            <button class="btn btn-primary"><i class="icon-ok"></i> 保存</button>
                        </div>
                    </form>
                    <script type="text/javascript">
                        window.UEDITOR_CONFIG.initialFrameWidth = 580;
                        window.UEDITOR_CONFIG.initialFrameHeight = 520;
                        var ueListContent = UE.getEditor('ListContent');
                        var isinit = true;
                        function contentInit() {
                            try {
                                ueListContent.setContent(html_decode('<%=htmlcontent %>'));
                                isinit = false;
                            } catch (e) { }
                        }
                        $(function () {
                            setTimeout(function () {
                                contentInit();
                                if (isinit) {
                                    setTimeout(function () {
                                        contentInit();
                                        if (isinit) {
                                            setTimeout(function () {
                                                contentInit();
                                            }, 1500);
                                        }
                                    }, 500);
                                }
                            }, 100);
                        });
                    </script>
                </div>    
          </div>
      </div>
    </div>
    <script type="text/javascript">
        function GotoDel() {
            if (confirm('删除操作不可恢复，确认要删除吗?')) {
                window.location.href = 'news.aspx?id=<%=newsindex%>&method=del';
            }
            return false;
        }
        function GotoList() {
            window.location.href = 'cmsclass.aspx?id=<%=classindex%>';
        }
        function Goto(url) {
            window.location.href = url;
        }
        function init(){
            if($('#imgPicUrl').attr('src')&&$('#imgPicUrl').attr('src')!='#'){
                $('#imgPicUrl').show();
            }
        }
        init();
        function showError(msg) {
            alert(msg)
        }
        
        $("#swfUploadPic").uploadify({
            'uploader': '/upload.aspx?filetype=pic&account=<%=_account %>',
             'formData': {},
             'buttonText': '选择图片',
             'auto': true,
             'multi': true,
             'onUploadSuccess': function (e, response, data) {
                 var stringArray = response.split("|");
                 if (stringArray[0] == "1") {
                     $('#imgPicUrl').attr('src', "<%=_dataurl %>/" + stringArray[1]).show();
                     $('#newsicons').val("/" + stringArray[1]);
                }
                else {
                    alert(stringArray[2]);
                }
            }
         });       
    </script>
    <%=_script %>
</body>
</html>
