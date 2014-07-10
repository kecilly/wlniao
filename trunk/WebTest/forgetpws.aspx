<%@ Page Title="" Language="C#" MasterPageFile="~/MiNiPage.Master" AutoEventWireup="true" CodeBehind="forgetpws.aspx.cs" Inherits="WebTest.forgetpws" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <title>辽宁高速ETC</title>
    <style type="text/css" >
        .txtboxv {
            width: 100px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
         <!-- Main Content Begin -->
	<section id="content">
		<div class="content-scroll">
			<!-- Main Content Area -->
			<section id="main" class="inner">
			    <!-- Repeat this Area for Multiple Boxes -->
                <asp:UpdatePanel runat="server" ID="upPanel">
                   <ContentTemplate>
                       
                       <asp:Panel CssClass="module" runat="server" ID="pass">
                		<h3>忘记密码--短信验证</h3>					
                        <div class="odd">
                            <p><b></b><asp:Label ID="lb_vindex" runat="server" Text=""></asp:Label></p>					
                        </div>
                           <ul>
                               <li>
							<label for="<% =name.ClientID %>">辽通卡卡号：</label>
                                   <asp:TextBox ID="name" runat="server" MaxLength="20" ></asp:TextBox><br/>
                                    <asp:RequiredFieldValidator ValidationGroup="v2" ID="RequiredFieldValidator5" runat="server" ErrorMessage="请添写辽通卡卡号" ControlToValidate="name"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="辽通卡卡号错误！" ControlToValidate="name" ValidationExpression="\d{16}|\d{20}" ValidationGroup="v"></asp:RegularExpressionValidator>
						</li>
                                <li class="odd"><label for="<% =SMSValidete.ClientID %>">短信验证码：</label>
                                    <asp:TextBox ID="SMSValidete" runat="server" MaxLength="4" CssClass="txtboxv"></asp:TextBox>　　<asp:Button ID="btn_send" runat="server" Text="发送" ValidationGroup= "v2" OnClick="btn_send_Click" /><br/>
                                    <asp:RequiredFieldValidator ValidationGroup="v" ID="RequiredFieldValidator1" runat="server" ErrorMessage="请添写短信验证码" ControlToValidate="SMSValidete"></asp:RequiredFieldValidator></li>
                                <li><label for="<% =Validete.ClientID %>">验证码：</label>
                                    <asp:TextBox ID="Validete" runat="server" MaxLength="4" CssClass="txtboxv"></asp:TextBox>　　<img id="Img1" runat="server" src="ValidateCode.aspx" style="width: 57px; height: 20px"
                              onclick="javascript:this.src=this.src+'?rnd=' + Math.random();" alt="" title="看不清,点击刷新" /><br/>
                                    <asp:RequiredFieldValidator ValidationGroup="v" ID="RequiredFieldValidator2" runat="server" ErrorMessage="请添写验证码" ControlToValidate="Validete"></asp:RequiredFieldValidator></li>
                              </ul>  
                           <div class="center">
							<p><asp:Button ID="btn_Validete" ValidationGroup="v" runat="server" Text="提  交" OnClick="btn_Validete_Click" Visible="False" /></p>
                               </div>
                           					
                        <asp:Label ID="lb_pass" runat="server"></asp:Label>                           
                	</asp:Panel>
                        <asp:Panel CssClass="module" runat="server" ID="Panel_SetPassword" Visible="False">
                            <h3>忘记密码--设置新密码</h3>					
                           <ul>
                                <li><label for="<% =SMSValidete.ClientID %>">密　　码：</label>
                                    <asp:TextBox ID="txt_password" runat="server" MaxLength="18" TextMode="Password"></asp:TextBox><br/>
                                    <asp:RequiredFieldValidator ValidationGroup="v1" ID="RequiredFieldValidator3" runat="server" ErrorMessage="请添写新的密码" ControlToValidate="txt_password"></asp:RequiredFieldValidator></li>
                                <li class="odd"><label for="<% =Validete.ClientID %>">确认密码：</label>
                                    <asp:TextBox ID="txt_password1" runat="server" MaxLength="18" TextMode="Password"></asp:TextBox><br/>
                                    <asp:RequiredFieldValidator ValidationGroup="v1" ID="RequiredFieldValidator4" runat="server" ErrorMessage="请确认您的密码" ControlToValidate="txt_password1"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="密码确认错误" ControlToCompare="txt_password" ControlToValidate="txt_password1" ValidationGroup="v1"></asp:CompareValidator>
                                </li>
                              </ul>  
                           <div class="center">
							<p><asp:Button ID="btn_savePWS" ValidationGroup="v1" runat="server" Text="提  交" OnClick="btn_savePWS_Click" /></p>
                               </div>
                        </asp:Panel>
                       <asp:Panel CssClass="module" runat="server" ID="Panel_no" Visible="False">
                            <h3>辽宁高速ETC</h3>	
                           <div class="odd">
                           
                          <div class="center">
							<p> <asp:Label ID="lb_error" runat="server" Text=""></asp:Label>					
                        </p>
                               </div></div>
                       </asp:Panel>
                    </ContentTemplate>
       
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btn_Validete" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btn_savePWS" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btn_Validete" EventName="Click" />
                    </Triggers>
       
                </asp:UpdatePanel>							
                <!-- Repeat this Area for Multiple Boxes -->	
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
</asp:Content>
