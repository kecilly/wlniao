<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cmsclass.aspx.cs" ValidateRequest="false" Inherits="Wlniao.CMS.CmsClass" %>
<!DOCTYPE html>
<html lang="zh">
<head>
    <title>栏目管理</title>
    <link href="/static/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="/static/jquery.js" type="text/javascript"></script>
    <script src="/static/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="/static/common.js" type="text/javascript"></script>
    <script src="/static/wln.js" type="text/javascript"></script>
    <script src="/static/SWFUpload/swfupload.js" type="text/javascript"></script>
    <script src="/static/ueditor/editor_all.js" type="text/javascript"></script>
    <script src="/static/ueditor/editor_config.js" type="text/javascript"></script>
    <style type="text/css">
    
      .container-narrow {margin: 0 auto;padding:0px 12px 0px 12px;}
      .span3 .btn-group{ height:700px; width:168px;}
      .span3 .btn{ width:168px; text-align:left;}
      
    .newsli{ background:#fafafa; clear:both; display:block; padding:3px 5px 3px 5px; margin:5px 0px; border:1px solid #f0f0f0;}
    .newslipic{float:left; height:50px;width:50px;border: 1px solid #B8B8B8;overflow: hidden;}
    .newslipic p{text-shadow: 0 1px 1px white;background: #F5F6F7;display: block;text-align: center;color: #666;letter-spacing:3px;font-weight:normal;font-size: 12px;line-height: 50px;}
    .newslipic img{color: transparent;height:50px;width:50px;font-size: 0;vertical-align: middle;-ms-interpolation-mode: bicubic;}
    .newslitxt{ margin:0px 0px 0px 60px;}
    .newslitxt h4{font-size:14px; line-height:1.8em; margin:0px; text-align:left;}
    .newslitxt div{  width:526px; text-align:right;}
    
    .page{width:658px;float:left;padding:10px 0px 23px 0px;text-align:center;}
    .page a{margin:0px 2px;border:1px solid #CCDBE4;line-height:22px;color:#1044BA;display:inline-block;padding:0px 6px;}
    .page span{margin:0px 2px;border:1px solid #CCDBE4;line-height:22px;color:#DBE1E6;display:inline-block;padding:0px 6px;}
    .page em{margin:0px 2px;line-height:22px;color:#333333;display:inline-block;padding:0px 6px;}
    </style>
</head>
<body>
    <div class="container-narrow">
      <h3 style=" font-family:微软雅黑;"><a href="site.aspx" style=" font-size:12px;">微网站设置</a>&nbsp;内容管理</h3>
      <div class="row-fluid">
          <div class="span3" style=" margin-left:15px;">
            <div class="btn-group btn-group-vertical">
              <%=classlist%>
              <button class="btn btn-primary" onclick="add();"><i class="icon-plus"></i> 新建栏目</button>
            </div>
          </div>
          <div class="span9" style=" margin-left:-10px;">
              <%if (string.IsNullOrEmpty(classindex))
                { %>
                <div class="hero-unit">
                  <p>欢迎使用栏目管理功能，请选中一个栏目或新增一个栏目，支持的栏目类型包括：文章列表、单页及功能页面。</p>
                  <p>
                    <a class="btn btn-primary btn-large" onclick="add();">新增栏目</a>
                  </p>
                </div> 
              <%}else{ %>    
                <form id="classeditform" method="post">
                <div class="btn-group">
                    <%=classbtn %>&nbsp;
                    <button class="btn" onclick="return edit(); return false;"><i class="icon-cog"></i> 栏目设置</button>
                    <button class="btn" onclick="return GotoDel(); return false;"><i class="icon-remove"></i> 删除栏目</button>
                </div>               
                <%if (classtype == "list")
                { %>
                    <div class="btn-group" style=" float:right;">
                        <button class="btn  btn-primary" onclick="return Goto('news.aspx?cid=<%=classindex %>'); return false;"><i class="icon-plus"></i> 新增内容</button>
                    </div>                 
                    <div class="pagerlist">
                        <%=_ListStr%>
                        <%=_PageBar%>
                    </div>
                  <%}
                else if (classtype == "page")
                { %>  
                    <div class="btn-group" style=" float:right;">
                        <button class="btn  btn-primary"><i class="icon-ok"></i> 保存</button>
                    </div>
                    <div style=" margin:8px 0px;">
                        <script id="ListContent" type="text/plain" name="myContent"></script>
                        <div style=" text-align:right; margin-top:8px;">
                            <input type="hidden" name="method" value="edit" />
                            <button class="btn btn-primary"><i class="icon-ok"></i> 保存</button>
                        </div>
                    </div>
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
                  <%}
                else if (classtype == "url")
                { %>
                    <div class="hero-unit" style=" margin:18px 0px;">
                      <p>当前栏目为外链栏目，你可以点击上方绿色按钮查看连接的页面。</p>
                      <p>
                        <a class="btn btn-primary btn-large" onclick="edit();">修改地址</a>
                      </p>
                    </div> 
                  <%}%>
              </form>
              <%}%>       
          </div>
      </div>
    <div id="myModal" class="modal hide fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <form method="post">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
        <h3>栏目设置</h3>
      </div>
        <div class="modal-body">
        <span style="width:90px; font-family:微软雅黑; display:inline-block; text-align:right;">名称:</span>&nbsp;<input type="text" id="classtitle" name="classtitle" class="txt grid-4 alpha pin" />
        <div style=" line-height:8px;">&nbsp;</div>
        <%--<span style="width:90px; font-family:微软雅黑; display:inline-block; text-align:right;">图标:</span>&nbsp;<img id="imgPicUrl" src="#" style=" width:48px;" /><span id="swfUploadPic"></span><input type="hidden" name="classicons" id="classicons" />
        <div style=" line-height:8px;">&nbsp;</div>--%>
        <span style="width:90px; font-family:微软雅黑; display:inline-block; text-align:right;">类型:</span>
            <select id="classtype" name="classtype" class="sel" onchange="showTypeTips();">
                <option value="list" selected="selected">文章列表</option>
                <option value="page">单页内容</option>
                <option value="url">链接地址</option>
            </select>
        <div style=" line-height:8px;">&nbsp;</div>
        <span style="width:90px; font-family:微软雅黑; display:inline-block; text-align:right;">排序:</span>&nbsp;<input type="text" id="classsort" name="classsort" class="txt grid-4 alpha pin" style=" width:60px;" /><span style="color:#999999; vertical-align:top; line-height:30px;">&nbsp;请输入数字,数值越小排序越靠前</span>
        <div style=" line-height:8px;">&nbsp;</div>
        <span style="width:90px; font-family:微软雅黑; display:inline-block; text-align:right;">其它选项:</span>&nbsp;&nbsp;<span style=" line-height:21px; vertical-align:top;"><input type="checkbox" id="showinhomepage" name="showinhomepage" style=" vertical-align:top;" />&nbsp;在首页显示</span>&nbsp;&nbsp;&nbsp;&nbsp;<span style=" line-height:21px; vertical-align:top;"><input type="checkbox" id="showinnavbar" name="showinnavbar" style=" vertical-align:top;" />&nbsp;在导航栏显示</span>
        <div style=" line-height:8px;">&nbsp;</div>
        <div id="urldiv" style=" display:none;">
        <span style="width:90px; font-family:微软雅黑; display:inline-block; text-align:right;">URL:</span>&nbsp;<input type="text" id="classurl" name="classurl" class="txt grid-4 alpha pin" style=" width:373px;" />
        </div>
        <div style=" line-height:8px;">&nbsp;</div>
      </div>
      <div class="modal-footer">
        <input id="method" type="hidden" name="method" value="add" />
        <button class="btn" data-dismiss="modal" aria-hidden="true">关闭</button>
        <button class="btn btn-primary" onclick="return classSave();">保存</button>
      </div>
      </form>
    </div>

    </div>
    <script type="text/javascript">
        function showTypeTips(){
            $('#urldiv').hide();
            if ($('#classtype').val() == 'url') {
                $('#urldiv').show();
            }
        }
        function add() {
            $('#myModal').modal();
            $('#classtitle').val('');
            $('#classtype').val('list');
            $('#classicons').val('');
            $('#classsort').val('');
            $('#classurl').val('');
            $('#showinhomepage').attr('checked', null);
            $('#showinnavbar').attr('checked', null);
            $('#method').val('add');
            $('#imgPicUrl').attr('src', '');
            return false;
        }
        function edit() {
            $('#myModal').modal();
            $('#classtitle').val('<%=classtitle %>');
            $('#classtype').val('<%=classtype %>');
            $('#classicons').val('<%=classicons %>');
            $('#classsort').val('<%=classsort %>');
            $('#classurl').val('<%=classurl %>');

            <%if(showinhomepage == "on"){%>
                $('#showinhomepage').attr('checked', 'checked');
            <%} %>
            <%if(showinnavbar == "on"){%>
                $('#showinnavbar').attr('checked', 'checked');
            <%} %>


            $('#method').val('save');
            $('#imgPicUrl').attr('src', $('#classicons').val());
            showTypeTips();
            return false;
        }
        function classSave() {
            if ($('#classtitle').val()) {
                return true;
            } else {
                alert('Sorry,请填写栏目名称！');
                $('#classtitle').focus();
            }
            return false;
        }
        function Goto(url) {
            window.location.href = url;
            return false;
        }
        function GotoDel() {
            if (confirm('删除操作不可恢复，确认要删除吗?')) {
                window.location.href = 'cmsclass.aspx?id=<%=classindex %>&method=del';
            }
            return false;
        }
        function GotoDelNews(nid) {
            if (confirm('删除操作不可恢复，确认要删除吗?')) {
                window.location.href = 'cmsclass.aspx?id=<%=classindex%>&method=delnews&nid=' + nid;
            }
            return false;
        }
        function OpenNew(url) {
            if (url && url != '#') {
                open(url);
            }
            return false;
        }
        function showError(msg) {
            $('#myModal').modal();
            alert(msg)
            return false;
        }
        $(function () {
            $('.navimg').each(function () {
                if ($(this).attr('src') != '' && $(this).attr('src') != '#') {
                    $(this).prev().hide();
                }
            });
        });
    </script>
    <%=_script %>
</body>
</html>
