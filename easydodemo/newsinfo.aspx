<%@ Page Language="C#" AutoEventWireup="true" CodeFile="newsinfo.aspx.cs" Inherits="newsinfo" %>
<%@ Register Src="uc/leftmenu.ascx" TagPrefix="uc" TagName="leftmenu" %>
<%@ Register Src="uc/header.ascx" TagPrefix="uc" TagName="header" %>
<%@ Register Src="uc/footer.ascx" TagPrefix="uc" TagName="footer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<%=Reisweb.ReisRepeater.doReapeat("News", "  where nid="+ Request.QueryString["id"] +" ", "1", "t", "<title>{1}</title><meta name=\"keywords\" content=\"{2}\"><meta name=\"Description\" content=\"{3}\">")%>
<link href="style/layout.css" rel="stylesheet" type="text/css" />
<link href="style/mianpage.css" rel="stylesheet" type="text/css" />
<script src="js/yu.js" type="text/javascript"></script>
<script src="js/tb.js" type="text/javascript"></script>
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
   <!--list start-->
   <div id="infor">
       <asp:Literal ID="ltNewsInfo" runat="server"></asp:Literal>
    
   </div>
  </div>
 </div>
 <!--footer start-->
 <uc:footer ID="footer" runat="server" Visible="true" />
</div>
</body>
</html>