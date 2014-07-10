<%@ Page Title="" Language="C#" MasterPageFile="~/MiNiPage.Master" AutoEventWireup="true" CodeBehind="DisplayPass.aspx.cs" Inherits="WebTest.DisplayPass" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>辽宁高速ETC</title>
     <style type="text/css" >
        .paginator {
            font: 12px Arial, Helvetica, sans-serif;
            padding: 10px 20px 10px 0;
            margin: 0px;
        }

            .paginator a {
                border: solid 1px #ccc;
                color: #0063dc;
                cursor: pointer;
                text-decoration: none;
            }

                .paginator a:visited {
                    padding: 1px 6px;
                    border: solid 1px #ddd;
                    background: #fff;
                    text-decoration: none;
                }

            .paginator .cpb {
                border: 1px solid #F50;
                font-weight: 700;
                color: #F50;
                background-color: #ffeee5;
            }

            .paginator a:hover {
                border: solid 1px #F50;
                color: #f60;
                text-decoration: none;
            }

            .paginator a, .paginator a:visited, .paginator .cpb, .paginator a:hover {
                float: left;
                height: 16px;
                line-height: 16px;
                min-width: 10px;
                _width: 10px;
                margin-right: 5px;
                text-align: center;
                white-space: nowrap;
                font-size: 12px;
                font-family: Arial,SimSun;
                padding: 0 3px;
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
                		<h3>辽通卡通行记录</h3>					
                        <div class="odd">
                            <p><b></b> 下面是您辽通卡的通行记录，由于网络原因，可能存在部分记录未上传，查询结果仅供参考。</p>					
                        </div>					
                        <asp:Label ID="lb_pass" runat="server"></asp:Label>                           
                       <webdiyer:AspNetPager ID="JZInvoice_page" CssClass="paginator"   CurrentPageButtonClass="cpb" runat="server" 
FirstPageText="首页"  LastPageText="尾页" NextPageText="下一页"  PageSize="8" PrevPageText="上一页"  ShowCustomInfoSection="Left"  CustomInfoTextAlign="Left" LayoutType="Table"  ShowInputBox="Never" OnPageChanging="JZInvoice_page_PageChanging" NumericButtonCount="5" ShowDisabledButtons="False">
</webdiyer:AspNetPager>
                	</asp:Panel>
                        <asp:Panel CssClass="module" runat="server" ID="Panel_no" Visible="False">
                              <h3>辽通卡通行记录</h3>
                            <div class="odd">
                            <p>您的辽通卡的最近一个月没有通行记录，由于网络原因，可能存在部分记录未上传，查询时间为:<% = DateTime.Now.ToString("G")%></p>					
                        </div>	
                        </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        
                        <asp:AsyncPostBackTrigger ControlID="JZInvoice_page" EventName="PageChanging" />
                        
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
