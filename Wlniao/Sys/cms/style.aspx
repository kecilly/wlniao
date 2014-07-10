<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="style.aspx.cs" Inherits="Wlniao.CMS.Style" %>
<!DOCTYPE html>
<html lang="zh">
<head>
    <title>风格选择</title>
    <link href="/static/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="/static/jquery.js" type="text/javascript"></script>
    <script src="/static/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <style type="text/css">
        .container-narrow {margin: 0 auto;padding:0px 5px 0px 12px;}
        .span3 .itemStyle{ background-color:#fafafa; border:1px solid #f5f5f5; margin:10px 0px; width:180px; height:250px; text-align:center; padding:18px;}
        .itemStyle span{ cursor:pointer;}
        .itemStyle h1,.itemStyle h2{ font-size:14px; font-family:微软雅黑; color:#666;}
        .itemStyle h2{ color:Blue;}
        .itemStyle span:hover h1,.itemStyle span:hover h2{ color:Red;}
        .stylepic{float:left; height:230px;width:180px;border: 1px solid #B8B8B8;overflow: hidden;}
        .stylepic p{text-shadow: 0 1px 1px white;background: #F5F6F7;display: block;text-align: center;color: #666;letter-spacing: 5px;font-weight: bold;font-size: 22px;line-height: 230px;}
        .stylepic img{color: transparent;height:230px;width:180px;font-size: 0;vertical-align: middle;-ms-interpolation-mode: bicubic;}
    </style>
</head>
<body>
    <div class="container-narrow">
      <h3 style=" font-family:微软雅黑;"><a href="site.aspx" style=" font-size:12px;">微网站设置</a>&nbsp;风格选择&nbsp;<a href="styleset.aspx" style=" font-size:12px;">首页内容</a>&nbsp;<a href="cmsclass.aspx" style=" font-size:12px;">栏目管理</a></h3>
      <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;每套风格使用不同的 <a href="styleset.aspx">首页内容</a>，但切换风格后并不会删除已有的 <a href="styleset.aspx">首页内容</a>，你可以随时切换回原来的风格。</p>      
      <div class="row" style=" margin:0px auto;">          
          <%=_liststr%>
      </div>

    </div>
    <script type="text/javascript">
        $(function () {
            $('.navimg').each(function () {
                if ($(this).attr('src') != '' && $(this).attr('src') != '#') {
                    $(this).prev().hide();
                }
            });
        });
        function setStyle(style) {
            if (confirm('您确定要应用选中的风格吗？')) {
                self.location.href = 'style.aspx?style=' + style;
            }
        }
        function onUse() {
            alert('您正在使用当前风格，无需切换');
        }
    </script>
<%=_script%>
</body>
</html>
