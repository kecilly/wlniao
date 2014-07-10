<%@ Page Title="" Language="C#" MasterPageFile="~/MiNiPage.Master" AutoEventWireup="true" CodeBehind="CardBanding.aspx.cs" Inherits="WebTest.CardBanding" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>辽宁高速ETC</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Main Content Begin -->
	<section id="content">
		<div class="content-scroll">
			<!-- Main Content Area -->
			<section id="main" class="inner">
				<!-- Repeat this Area for Multiple Boxes -->
				<div class="module">
					<h3>辽通卡绑定</h3>
					<div class="odd">
						<p>　　将辽通卡与微信绑定后，即可使用微信方便的查询帐户信息与最近的通行记录。绑定操作需要您填写辽通卡的卡号和服务密码。</p>
					</div>
                    <asp:Panel runat="server" ID="pnbanding">
				    <div id="banding">
						<div>
							<p><label for="name">辽通卡卡号：</label>
							<input type="number" id="name" name="name"  /></p>
						</div>
						<div>
							<p><label for="password">服务密码：</label>
							<input type="password" id="password" name="password" /></p>
						</div>
						<div>
							<p><label for="message">验证码：</label>
							<input id="message" name="message" style="width: 100px" /><img id="Img1" runat="server" src="ValidateCode.aspx" style="width: 57px; height: 20px"
                              onclick="javascript:this.src=this.src+'?rnd=' + Math.random();" alt="" title="看不清,点击刷新" /></p>
						</div>
						<div>
							<p><label>Tips</label>
							<small>　　1.辽通卡服务密码与<a href="http://218.25.53.5:8081/" target="_self">辽宁高速ETC官网</a>相同。如要修改请点击<a href="http://218.25.53.5:8081/wechat/forgetpws.aspx?do=forget" target="_self">“修改服务密码”</a>进行修改修改。<br/>　　2.首次登录使用卡号，服务密码为账号后六位。</small><br /></p>
						</div>
						<div class="center">
							<p><button onclick="JAVASCRIPT:BandCardInfo();return false;">绑　定</button></p>
						</div>
				    </div>
                    </asp:Panel>
                     <asp:Panel runat="server" ID="pninfo" Visible="False">
                    <div id="info" >
                        <p>　　您已经绑定辽通卡信息。</p>
                    </div>
                      </asp:Panel>
				</div>
				
			</section>
			
			<!-- Copyright Information -->
			<footer>
				<p><b>　Copyright &copy; <script type="text/javascript">
                 　      				    var d = new Date();
                 　      				    var vYear = d.getFullYear();
                 　      				    var vMon = d.getMonth() + 1;
                 　      				    var vDay = d.getDate();
                 　      				    document.write(vYear);
</script>  辽宁省高速公路ETC运营管理中心.</b></p>
			</footer>
		
		</div>
	</section>
	<!-- Main Content End -->
    <script type="text/javascript">
        function BandCardInfo() {
            if ($('#name').val() == '') {
                alert('辽通卡卡号不能为空!');
                return false;
            }
            else if ($('#name').val().length == 16 || ($('#name').val() == 20)) {
                
            } else {
                alert('辽通卡卡号长度不正确!');
                return false;
            }
            if ($('#password').val() == '') {
                alert('辽通卡密码不能为空!');
                return false;
            }
            if ($('#message').val() == '') {
                alert('验证码不能为空!');
                return false;
            }
           
            $.getJSON("CardBanding.aspx?rnd=" + Math.random(), { "do": "Add", "name": $('#name').val(), "password": $('#password').val(), "message": $('#message').val(), "openid": $('#<%=openid.ClientID%>').val(), "firstid": $('#firstid').val() }, function (json) {
                if (json.success) {
                    location.reload();
                } else {
                    alert(json.msg);
                }
            });
        }
    </script>

    <asp:HiddenField ID="timestamp" runat="server" />
    <asp:HiddenField ID="openid" runat="server" />
    <asp:HiddenField ID="firstid" runat="server" />
</asp:Content>
