<%@ Page Language="C#" AutoEventWireup="true" CodeFile="newsclasslist.aspx.cs" Inherits="shop_manage_userlist" %>
<%@ Register Src="uc/adminnav.ascx" TagPrefix="uc" TagName="adminnav" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title>分类列表</title>
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
    <h5>分类列表</h5>
	<table id="productclasslist">
	<tr><td id="id">分类ID</td><td id="name">类别名称</td><td id="operating">操作</td></tr>
	<asp:Literal ID="ltUserList" runat="server"></asp:Literal>
	</table>
    <asp:Literal ID="ltModifyClass" runat="server"></asp:Literal>
       
	<asp:Literal ID="ltAddNewClass" runat="server"></asp:Literal>

  </div>
 </div>
 <div id="footer"></div>
</div>

</body>
</html>
