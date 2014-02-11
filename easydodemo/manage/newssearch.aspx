<%@ Page Language="C#" AutoEventWireup="true" CodeFile="newssearch.aspx.cs" Inherits="shop_manage_userlist" %>
<%@ Register Src="uc/adminnav.ascx" TagPrefix="uc" TagName="adminnav" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title>搜索文章</title>
<link href="css/layout.css" rel="stylesheet" type="text/css" />
<link href="css/mianpage.css" rel="stylesheet" type="text/css" />
</head>
<body>
<div id="container">
 <div id="header">
  <div id="topnav"></div>
  <div id="admin"></div>
 </div>
 <div id="mainbody">  
   <uc:adminnav ID="an" runat="server" Visible="true" />
  <div id="shop_admin">
   <div id="newssearch">
    <h5>搜索文章</h5>
	<form id="form1" runat="server">
	<table><tr><td>搜索条件</td>
	<td>标题<asp:TextBox ID="tbNtitle" runat="server"></asp:TextBox></td>
	<td>类别<asp:DropDownList ID="ddlNclass" runat="server"></asp:DropDownList></td>
	<td>
        <asp:Button ID="btSearch" runat="server" Text="搜索" OnClick="btSearch_Click" /></td></tr></table>
	</form>
	
	<table width="98%">
	<tr style=" font-size:16px; color:Red; "><td>编号</td><td>标题</td><td>浏览数</td><td>录入日期</td><td>操作</td></tr>
	<asp:Literal ID="ltUserList" runat="server"></asp:Literal></table>
   </div>
  </div>
 </div>
 <div id="footer"></div>
</div>

</body>
</html>
