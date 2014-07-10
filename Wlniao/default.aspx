<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Wlniao._Default" %>
<!DOCTYPE html>
<html lang="zh">
<head>
    <title><%=_CurrentAccount.AccountUserName %></title>
    <link href="/static/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="/static/jquery.js" type="text/javascript"></script>
    <script src="/static/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <link href="static/css3frame/css/style.css" rel="stylesheet" type="text/css" />
    <link href="static/tips/style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
    ul.box,ul.box2{ clear:both;}
    ul.box,ul.box li,ul.box2,ul.box2 li{ list-style:none;}
    .box li,.box2 li{ width:138px; height:76px;background:#EF8577; margin-right:10px; margin-bottom:10px; float:left; overflow:hidden; position:relative;}
    .toll_img{width:138px; height:76px;}
    .toll_img img{ border:none; margin:10px 0px 0px 13px;}
    .toll_img h1{ font-size:12px; font-family:微软雅黑; color:#fff; position:absolute; right:8px; top:33px;}
    .toll_info{ height:76px; background:rgba(45,45,45,0.5); vertical-align:middle;display:table-cell;}
    .box li p,.box2 li p{ padding:5px 7px; font:normal 12px/24px '微软雅黑'; color:White; cursor:pointer;}
    .box li a,.box2 li a{ color:#666; text-decoration:none; position:absolute; top:0; left:0;}
    
    .box2 li{ width:82px; height:52px; background:rgba(180,180,180,0.7);}
    .box2 .toll_img{width:82px; height:52px;}
    .box2 .toll_img img{ border:none; margin:9px 0px 0px 23px;}
    .box2 .toll_info{ height:52px; vertical-align:middle;display:table-cell;}
    .box2 .toll_info p{ width:88px; padding:3px 7px; font-weight:bolder; display:block;}
    </style>
</head>
<body>
<script src="topbar.aspx" type="text/javascript"></script>
<div class="page">
	<div class="admin-panel clearfix">
		<div id="slidebar" class="slidebar">
			<div class="logo">
				<img src="" alt="" />
			</div>
            <div style=" line-height:30px; color:#999; padding-left:12px;">↓常用功能</div>
			<ul>
				<li class="css3 css3_home active"><a href="#" onclick="showDashboard();"><%=_CurrentAccount.AccountUserName%>的首页</a></li>
				<li class="css3 css3_navset"><a href="#" onclick="showMainFrameMenu('Sys/Setting.aspx','css3_navset');">帐号接入信息</a></li>
				<li class="css3 css3_weixin"><a href="#" onclick="showMainFrameMenu('Sys/ResponseMsg.aspx','css3_weixin');">被关注&默认回复</a></li>
				<li class="css3 css3_keywords"><a href="#" onclick="showMainFrameMenu('Sys/HandleRuleList.aspx','css3_keywords');">自动处理规则</a></li>
				<li class="css3 css3_cms1"><a href="#" onclick="showMainFrameMenu('Sys/MenuSet.aspx','css3_cms1');">微信自定义菜单</a></li>
				<li class="css3 css3_user"><a href="#" onclick="showMainFrameMenu('Sys/MPUserInfo.aspx','css3_user');">用户管理</a></li>
				<li class="css3 css3_user"><a href="#" onclick="showMainFrameMenu('Sys/Cms/site.aspx','css3_user');">微网站管理</a></li>
                <%if (AppCenter){ %><li class="css3 css3_module"><a href="#" onclick="showMainFrameMenu('appbox.aspx?v=2','css3_module');">应用中心</a></li><%} %>
				<li class="css3 css3_pic"><a href="#" onclick="showMainFrameMenu('Sys/ChangePwd.aspx','css3_pic');">修改密码</a></li>
				<li class="css3 css3_logout"><a href="Logout.aspx">注销</a></li>
			</ul>
            <div style=" padding:18px; text-align:center; line-height:24px; font-size:14px;">
                <div style=" font-size:12px; color:Gray;"></div>
            </div>
		</div>
		<div class="main">
			<ul class="topbar clearfix">						
				<li><a href="#" onclick="showDashboard();" title="返回我的主页" style=" width:98px;">c<font style="font-family:微软雅黑; padding-left:6px; font-size:14px;">回到首页</font></a></li>
				<li><a href="#" onclick="reloadMainFrame();" title="刷新"><font style="font-family:微软雅黑; padding-left:6px; font-size:14px;">刷新</font></a></li>
                <li><a href="#" onclick="showMainFrame('sys/HandleRuleList.aspx');" title="关键字自动回复">q</a></li>
				<li><a href="#" onclick="showMainFrame('sys/responsemsg.aspx');" title="首次关注&默认回复">f</a></li>
				<!--<li><a href="/mobile.aspx?a=<%=_account %>" title="打开我的微网站" target="_blank">p</a></li>-->
                
			</ul>
			<div class="mainContent clearfix">
				<div id="dashboard">
                    <div style=" margin:0px; padding:0px; clear:both; display:block;">
					<h2 class="header">基础服务</h2>
                    <ul class="box">
	                    <li style=" background-color:#C6BF91;">
		                    <a href="#" onclick="showMainFrameMenu('Sys/Setting.aspx','css3_navset');">
			                    <div class="toll_img"><img src="static/icons/setting.png" alt="" /><h1>帐号接入信息</h1> </div>
			                    <div class="toll_info"><p>使用本平台前，请使用本模块中的内容前往公众平台完成接入操作。</p></div>
		                    </a>
	                    </li>
	                    <li style=" background-color:#C6BF91;">
		                    <a href="#" onclick="showMainFrameMenu('Sys/ResponseMsg.aspx','css3_weixin');">
			                    <div class="toll_img"><img src="static/icons/scgz.png" alt="" /><h1>首次关注&默认回复</h1> </div>
			                    <div class="toll_info"><p>设置当用户关注该帐号时发送的欢迎消息和未知内容时的默认回复。</p></div>
		                    </a>
	                    </li>
	                    <li style=" background-color:#C6BF91;">
		                    <a href="#" onclick="showMainFrameMenu('sys/HandleRuleList.aspx','css3_keywords');">
			                    <div class="toll_img"><img src="static/icons/gjzhf.png" alt="" /><h1>自动处理规则</h1> </div>
			                    <div class="toll_info"><p>该模块可设置用户发送指定关键字时自动回复的文本或多媒体内容。</p></div>
		                    </a>
	                    </li>
	                    <li style=" background-color:#9999CC;">
		                    <a href="#" onclick="showMainFrameMenu('Sys/MPUserInfo.aspx','css3_user');">
			                    <div class="toll_img"><img src="static/icons/user.png" alt="" /><h1>用户</h1></div>
			                    <div class="toll_info"><p>通过这里查看、备注关注过您公众帐号的用户和消息记录。</p></div>
		                    </a>
	                    </li>              
                        <li style="background-color:#9999CC;">
                            <a href="#" onclick="showMainFrameMenu('Sys/MenuSet.aspx','css3_cms1');">
                                <div class="toll_img"><img src="static/icons/zdycd.png" alt="" /><h1>自定义菜单管理</h1></div>
                                <div class="toll_info"><p>您可以在此对公众帐号界面底部的自定义按钮进行定制(限服务号)。</p></div>
                            </a>
                        </li>
                    </ul>
                    </div>
				</div>
                <div id="mainFrame"></div>
			</div>
		</div>
	</div>
</div>
</body>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.box img').each(function (e) {
                var pic = $(this).attr('src');
                if (pic && pic != '#') {
                    $(this).show();
                } else {
                    $(this).hide();
                }
            });
            $('.box a').mouseover(function () {
                $(this).stop().animate({ "top": "-78px" }, 100);
            })
            $('.box a').mouseout(function () {
                $(this).stop().animate({ "top": "0" }, 100);
            })


            $('.box2 img').each(function (e) {
                var pic = $(this).attr('src');
                if (pic && pic != '#') {
                    $(this).show();
                } else {
                    $(this).hide();
                }
            });
            $('.box2 a').mouseover(function () {
                $(this).stop().animate({ "top": "-52px" }, 100);
            })
            $('.box2 a').mouseout(function () {
                $(this).stop().animate({ "top": "0" }, 100);
            })
        })
        var onhome = true;
        function showDashboard() {
            onhome = true;
            $('#mainFrame').hide();
            $('#dashboard').show();
            $('.css3').removeClass('active');
            $('.css3_home').addClass('active');
        }
        var iheig = 0;
        function showMainFrame(url) {
            onhome = false;
            if (iheig == 0) {
                iheig = $('#slidebar').height()-50;
            }
            $('#dashboard').hide();
            //$("#thisFramePage").contents().find("body").empty();
            $("#mainFrame").html('<iframe id="thisFramePage" src="' + url + '" frameborder="0" marginheight="0" marginwidth="0" scrolling="auto" width="100%" height="'+iheig+'px"></iframe> ');
            $('#mainFrame').show();
        }
        function showMainFrameMenu(url, toActive) {
            onhome = false;
            showMainFrame(url);
            $('.css3').removeClass('active');
            if (toActive) {
                $('.' + toActive).addClass('active');
            }
        }
        function reloadMainFrame() {
            if (onhome) {
                self.location.reload();
            } else {
                var temp = $("#mainFrame").html();
                if (temp != '') {
                    if (iheig == 0) {
                        iheig = $('#slidebar').height() - 50;
                    }
                    $('#dashboard').hide();
                    $("#mainFrame").html('');
                    $("#mainFrame").html(temp);
                    $('#mainFrame').show();
                } 
            }
        }
        function showTips(msg, icon, url) {
            var _icon = 1;
            if (icon) {
                _icon = icon;
            }
            ZENG.msgbox.show('&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;' + msg + '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;', _icon, 1800);
            if (url) {
                setTimeout(showMainFrame(url), 100);
            }
        }
        $('.logo img').hide();

        setInterval(function () {
            $('#bullhorn').fadeTo("slow", 0.7).fadeTo("slow", 0.15);
        }, 2000);
    </script>
<script type="text/javascript" src="static/tips/tips.js"></script>
</html>
