<%@ Page Language="C#" AutoEventWireup="true" CodeFile="newsedit.aspx.cs" Inherits="shop_manage_userlist" %>
<%@ Register Src="uc/adminnav.ascx" TagPrefix="uc" TagName="adminnav" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title>�޸�����</title>
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
   <div id="newsedit">
    <h5>�޸�����</h5>
	<%=Reisweb.ReisEditer.doEdit("News", "nid=" + Reisweb.ReisUtils.getRQ("nid", "") + "", "[ntitle|text||����||notnull||][nclass|select|sql<select ncname,ncid from News_Class>|�������||||][nkeyword|text||�ؼ���||||][ndescription|text||����||||][ncontent|html||��������||||][nview|text||�����||||]", "<script>alert('�޸ĳɹ�');</script>")%>
	<br/><a href="newslist.aspx">��˷��������б�</a>
   </div>
  </div>
 </div>
 <div id="footer"></div>
</div>

</body>
</html>
