<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Regist.aspx.cs" Inherits="HappyLibrary.Regist" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta charset="utf-8">
        <meta content="telephone=no" name="format-detection">
        <meta content="initial-scale=1.0, maximum-scale=1.0, user-scalable=no, minimal-ui" name="viewport">
        <meta content="no" name="apple-mobile-web-app-capable">
        <meta content="black" name="apple-mobile-web-app-status-bar-style">
        <meta content="当当网触屏版" name="application-name">
        <meta content="must-revalidate,no-cache" http-equiv="Cache-Control">
    <title></title>
    <link href="css/login.css" rel="stylesheet" type="text/css">

</head>
<body>
    <article>
            <header>
                <div class="new_header">
                                            <a href="javascript:history.back();" class="xph_back"><span>返回</span></a>
                                            <h2>注册新用户</h2>
                </div>
            </header>
            <form id="form1" runat="server">
                <section class="login_wrap">
                    <div class="field username">
                        <input class="uname" type="text" id="txtUser" name="txtUser" value="" placeholder="请输入手机号" maxlength="11" autocomplete="off" tabindex="1"/>
                        <p>
                            <a href="javascript:void(0);" class="del-btn"></a>
                        </p>
                    </div>
                    <div id="inputcode" class="field clearfix auth_code" style="display: block">
                        <input class="imgcode" id="txtVcode" name="txtVcode" type="text" value="" placeholder="请输入图形验证码" maxlength='4' autocomplete="off" tabindex="2"/>
                        <p>
                            <a href="javascript:this.src='ValidateCode.aspx?rnd=' + Math.random();" class="del-btn"></a>
                        </p>
                        <span class="pic_code"><img id="imgVcode" src="ValidateCode.aspx"></span>
                        <a class="imgUpdate" name="imgUpdate" href="javascript:void(this);">换一张</a>
                    </div>
                    <div class="field">
                        <button type="button" id="btnGetSms" name="btnGetSms">获取验证码</button>
                    </div>
                    <div class="field zhuce_text">点击注册表示您同意《<a href="http://m.dangdang.com/help_center.php?page=20&sid=e1302e814144631e">当当交易条款</a>》和《<a href="http://m.dangdang.com/help_center.php?page=21&sid=e1302e814144631e">当当社区条款</a>》</div>
                    <div class="login_mode"><a href="reg_email.php?apkparams=ZnJvbWxvZ2luPTAmJnNpZD1lMTMwMmU4MTQxNDQ2MzFlJmJ1cmw9aHR0cDovL20uZGFuZ2RhbmcuY29tL3RvdWNoL29yZGVyLnBocD9zaWQ9ZTEzMDJlODE0MTQ0NjMxZQ==" name="btnEmailReg" class="right">邮箱注册</a></div>
                </section>
                <div>
                    <input type="hidden" id="action" name="action" value="register" />
                    <input type="hidden" id="view" name="view" value="0fBHZY+a4Qw="/>
                    <input type="hidden" id="captcha_key" name="captcha_key" value="touch_1414074909942376903" />
                    <input type="hidden" id="apkparams" name="apkparams" value="ZnJvbWxvZ2luPTAmJnNpZD1lMTMwMmU4MTQxNDQ2MzFlJmJ1cmw9aHR0cDovL20uZGFuZ2RhbmcuY29tL3RvdWNoL29yZGVyLnBocD9zaWQ9ZTEzMDJlODE0MTQ0NjMxZQ==" />
                    <input type="submit"  id="btnReg1" style=" display:none;"/>
                </div>
            </form>
            <section id="errMsg" class="error_mm"><span class="text"></span></section>
            <section class="error_mm error_loading">
                <div class="big_r">
                    <div class="little_r"></div>
                </div>
                <span class="text">正在加载 ...</span>
            </section>
        </article>
        <script src="js/zepto.js" type="text/javascript"></script>
        <script src="js/zepto.touch.js" type="text/javascript"></script>
        <script src="js/comdd.js" type="text/javascript"></script>
        <script src="js/mobilZC1.js" type="text/javascript"></script>
        <script type="text/javascript">
            var errorcode = parseInt(0);
            function(object mm3)
            {
                d            };
        </script>
</body>
</html>


