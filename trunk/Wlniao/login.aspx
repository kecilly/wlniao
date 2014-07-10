<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Wlniao.Login" %>
<!DOCTYPE html>
<html lang="zh">
<head>
    <title><%=SiteName%> - 微信公众号智能服务平台</title>  
    <style type="text/css">
        body,h1,h2,h3,h4,h5,h6,blockquote,p,pre,dl,dd,ol,ul,caption,th,td,form,fieldset,legend,input,button,textarea,address{margin:0;padding:0}
        .clearfix:after{display: block; content: "\20"; height: 0; clear: both; overflow: hidden; visibility: hidden;}/*ie8以上*/
        .clearfix{*zoom:1;}/*ie6、7*/ 
        body{background:#f2f2f2; background-image:url('static/img/home_bg.png');}
        .container{margin-right: auto;margin-left: auto;width: 940px;}
        .content{padding-top:30px; height:500px; position:relative;}
        .loadimg{background:url(static/img/p/login.png) 60px 20px no-repeat;}
        .login_form{width:377px; float:right; background:#fff url(static/img/p/login_f.gif) left top repeat-x;}
        .pad_50{padding:0 0 22px 50px; border:1px solid #dcdcdc; border-bottom:1px dashed #d5d5d5;}
        .login_form h4{font:normal 22px/1.1 \5FAE\8F6F\96C5\9ED1,\9ED1\4F53,\6587\6CC9\9A7F\9ED1\4F53; color:#FF7B00; height:52px; padding-top:50px;}
        .input-field{position:relative; margin-bottom:14px;}
        .input_kuang{display:block; float:left; vertical-align:top; height:27px; line-height:27px; padding:8px; font-size:14px; border:1px solid #e3e3e3; border-top:1px solid #ccc; width:252px;
        background-color:#fff; background-position:0 -118px; box-shadow:0 1px 1px #fff;}
        .sub_bg, .input_kuang, .input_b, .sub_login, .mt_login{-moz-border-radius:3px; -webkit-border-radius:3px; border-radius:3px; }
        .sub_bg , .mt_login, .input_b{-moz-box-shadow:0 1px 1px #f0f0f0; -webkit-box-shadow:0 1px 1px #f0f0f0; box-shadow:0 1px 1px #f0f0f0;}
        .sub_login{margin:0 auto; width:130px; border:1px solid #FF7B00; height:40px; background-position:0 0 ; cursor:pointer;}
        .no_bg{border:0 none;padding:0;background-color:#FF7B00;cursor:pointer; display:block;}
        .sub_bg input{width:132px; height:36px;}
        .sub_login input{width:130px;height:40px;color:#fff;font:700 16px/40px \5FAE\8F6F\96C5\9ED1,\9ED1\4F53,\6587\6CC9\9A7F\9ED1\4F53;}
        .sub_login .sub_login_a{ display:inline-block; _display:inline; *zoom:1; width:130px;height:40px;text-align:center;color:#fff;font:700 16px/40px \5FAE\8F6F\96C5\9ED1,\9ED1\4F53,\6587\6CC9\9A7F\9ED1\4F53;}
        .sub_log a{float:left; margin:10px 0 0 20px; font-size:14px; color:#333; height:14px; display:inline-block; line-height:1.5;}
        .mt_login{display:inline-block; *display:inline; zoom:1; height:24px; border:1px solid #dadada; font-size:14px; cursor:pointer; padding-top:14px; width:273px;
        text-align:center; color:#FF7B00; font-weight:bold; background-position:0 -40px; }
        .ano_log{padding:30px 50px 10px 50px; background:#f8f8f8; border:1px solid #dcdcdc; border-bottom:none; border-top:none;}
        .ano_log .mt_login{height:30px; padding-top:12px;}
        .ano_span_t{color:#999; line-height:1.1; text-align:center; font-size:12px; width:377px; margin:0px; height:46px; background:url(static/img/p/login_b.gif) left bottom no-repeat;}
        .flt_l{float:left;}
        
        footer {height: 46px;background-color: #E6EAED;}
        footer div {height: 46px;background-color: #D2DAE1;border-bottom: 1px solid white;-webkit-box-shadow: inset 0 1px 3px rgba(136, 159, 171, .75);-moz-box-shadow: inset 0 1px 3px rgba(136,159,171,.75);box-shadow: inset 0 1px 3px rgba(136, 159, 171, .75);}
        footer ul {width: 210px;margin: 0 auto;font-size: 0px;}
        footer ul li {display: inline-block;font-size: 12px;list-style: none;}
        footer ul li a {height: 46px;line-height: 46px;margin:0px 8px;font-size: 12px;color: #3C3C3C;text-decoration:none;}
    </style>
    <link href="static/maopaotips.css" rel="stylesheet" type="text/css" />
    <script src="static/jquery.js" type="text/javascript"></script>
<%--<script src="../static/jquery-ui.min.js" type="text/javascript"></script>--%>
</head>
<body>
 <div class="container" style="margin-top:58px">
    <div id="loaddiv" class="content clearfix loadimg">
      <div class="login_form clearfix">
        <div class="pad_50 clearfix">
          <h4>欢迎使用</h4>
          <form method="post" action="login.aspx" >
            <div class="input-field clearfix">
              <input type="text" id="user_ph" class="input_kuang item errortip" value="用户名" style="color:#999;display:none;">
              <input type="text" name="username" value="<%=username %>" id="user" class="input_kuang item errortip" placeholder="用户名">
            </div>
            <div class="input-field clearfix">
              <input type="text" id="pwd_ph" value="密码" class="input_kuang item errortip" style="color:#999;display:none;">
              <input type="password" name="password" class="input_kuang item errortip" id="pwd" placeholder="密码">
            </div>
            <div style="color:red;font-size:14px; margin-bottom:5px;"><%=msg%></div>
            <div class="sub_log clearfix">
              <div class="sub_login flt_l"><input type="submit" class="no_bg" value="登录" id="btn_login" onclick="Login();"></div>
              <%--<a href="#">忘记密码?</a>--%>
            </div>
          </form>
        </div>
        <div class="ano_log">
          <a class="mt_login" href="Sys/Create.aspx">注册帐号</a>
        </div>
        <div class="ano_span_t">如果您有不明白的地方，可以联系客服为您解答</div>
      </div>
    </div>    
 </div>
<footer>
    <div>
        <ul>
            <li><a href="https://mp.weixin.qq.com" target="_blank">公众平台</a></li>
            <li class="ml10 mr10">|</li>
            <li><a href="#" target="_blank">服务协议</a></li>
            <li class="ml10 mr10">|</li>
            <li><a href="#">发展建议</a></li>
        </ul>
    </div>

</footer>
</body>
</html>
