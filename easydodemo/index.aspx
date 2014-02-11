<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="newslist" %>
<%@ Register Src="uc/leftmenu.ascx" TagPrefix="uc" TagName="leftmenu" %>
<%@ Register Src="uc/header.ascx" TagPrefix="uc" TagName="header" %>
<%@ Register Src="uc/footer.ascx" TagPrefix="uc" TagName="footer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title>新闻列表</title>
<link href="style/layout.css" rel="stylesheet" type="text/css" />
<link href="style/mianpage.css" rel="stylesheet" type="text/css" />

<style type="text/css">
.paged a:hover{color:red;}
</style>
</head>
<body>
<div id="container">
 <!--header start-->
 <uc:header ID="header" runat="server" Visible="true" />
 <!--mianbody start-->
 <div id="mianbody">
  <div id="left">
   <!--asrt start-->
   <uc:leftmenu ID="lm" runat="server" Visible="true" />
   <!--brand start-->
  
  </div>
  <!--right start-->
  <div id="right">
   <!--trans start-->
   <!--list start-->
   
   <div id="listbox">
    <div id="listtitle">网站公告</div>
	<div id="list">
	 <ul>
         <asp:Literal ID="ltNewsList" runat="server"></asp:Literal>
	 
	 </ul>
	</div>
   </div>
  </div>
 </div>
 <!--footer start-->
 <uc:footer ID="footer" runat="server" Visible="true" />
</div>
</body>
</html>

