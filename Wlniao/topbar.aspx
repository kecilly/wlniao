<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="topbar.aspx.cs" Inherits="Wlniao.Topbar" %>
document.title=document.title+'|<%=SiteName%>微信公众帐号互动管理系统';

document.body.style.paddingTop = '58px';
document.write('    <div class="navbar navbar-fixed-top" style="z-index:9999;">');
document.write('      <div class="navbar-inner">');
document.write('        <div class="container-fluid">');
document.write('		  <a class="brand" style="width:320px;overflow:hidden;"> <img alt="<%=SiteName%>" src="/static/logo20.png" style=" height:24px;" /><span><%=SiteName%></span></a>');
document.write('          <div class="btn-group pull-right">');
<%if (!string.IsNullOrEmpty(NoticeTitle))
  { %>
document.write('			<a class="btn btn-success" href="/notice.aspx" target="_blank">');
document.write('			  <i class="icon-bullhorn" id="bullhorn"></i> <%=NoticeTitle%>');
document.write('			</a>');
<%} %>
document.write('          </div>');
document.write('          <div class="nav-collapse collapse">');
document.write('            <ul class="nav">');
//document.write('              <li><a href="/logout.aspx">注销</a></li>');
document.write('            </ul>');
document.write('          </div>');
document.write('        </div>');
document.write('      </div>');
document.write('    </div>');