<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Regist.aspx.cs" Inherits="HappyLibrary.Regist" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta charset="utf-8">
        <meta content="telephone=no" name="format-detection">
        <meta content="initial-scale=1.0, maximum-scale=1.0, user-scalable=no, minimal-ui" name="viewport">
        <meta content="no" name="apple-mobile-web-app-capable">
        <meta content="black" name="apple-mobile-web-app-status-bar-style">
        <meta content="快乐少年图书馆" name="application-name">
        <script src="js/jquery.min.js"></script>
        <meta content="must-revalidate,no-cache" http-equiv="Cache-Control">
    <title></title>
    <link href="css/login.css" rel="stylesheet" type="text/css">

</head>
<body>
    <article>
            <header>
                <div class="new_header">
                                            <a href="javascript:history.back();" class="xph_back"><span>返回</span></a>
                                            <h2>微信用户绑定借书卡</h2>
                </div>
            </header>
            <form id="form1" runat="server">
                <asp:Panel runat="server" ID="pnbanding">
                <section class="login_wrap">
                    <div class="field username">
                        <input  class="uname" type="text" id="txtUser" name="txtUser" value="" placeholder="请输入手机号" maxlength="11" autocomplete="off" tabindex="1"/>
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
                        <a class="imgUpdate" name="imgUpdate" href="javascript:rfcode();">换一张</a>
                    </div>
                    <div class="field">
                        <button type="button" id="btnGetSms" name="btnGetSms" onclick="JAVASCRIPT:BandCardInfo();return false;">关联</button>
                    </div>
                    </section>
                 </asp:Panel>
                <asp:Panel runat="server" ID="pninfo" Visible="False">
                    <header>
                <div class="new_header">
                                            <h2>　　您已经绑定“快乐少年图书馆”借书卡。</h2>
                </div>
            </header>
                    </asp:Panel>
                    <asp:HiddenField ID="timestamp" runat="server" />
                    <asp:HiddenField ID="openid" runat="server" />
                    <asp:HiddenField ID="firstid" runat="server" />
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
            function rfcode()
            {
                $("#imgVcode").attr("src", "ValidateCode.aspx?rnd=" + Math.random()); 
            };
        </script>
     <script type="text/javascript">
         function BandCardInfo() {
             if ($('#txtUser').val() == '') {
                 alert('手机号不能为空!');
                 return false;
             }
             else if ($('#txtUser').val().length != 11)
             {
                 alert('手机号长度不正确!');
                 return false;
             }
             if ($('#txtVcode').val() == '') {
                 alert('验证码不能为空!');
                 return false;
             }

             $.getJSON("regist.aspx?rnd=" + Math.random(), { "do": "Add", "name": $('#txtUser').val(), "password": $('#txtVcode').val(),  "openid": $('#<%=openid.ClientID%>').val(), "firstid": $('#firstid').val() }, function (json) {
                if (json.success) {
                    location.reload();
                } else {
                    alert(json.msg);
                }
            });
        }
    </script>


</body>
</html>


