<%@ Control Language="C#" AutoEventWireup="true" CodeFile="leftmenu.ascx.cs" Inherits="uc_leftmenu" %>
 <!--asrt start-->
   <div id="asrt">
       <asp:Literal ID="L1" runat="server"></asp:Literal>
   </div>
   
   <!--leftad start-->

   
     <div id="knowledge">
	<div id="knowledge_title">
	  <dl>
	   <dt>家电资讯</dt>
	   <dd><a href="index.aspx?class=3" target="_blank">更多</a></dd>
	  </dl>
	 </div>
	<div id="knowledge_list">
	  <ul>
	  <%=Reisweb.ReisRepeater.doReapeat("News"," where nclass=3 order by nid desc","10","t","<li><a href=\"newsinfo.aspx?id={0}\" title=\"{1}\" target=\"_blank\">{1}</a></li>") %>
	   
	  </ul>
	 </div>
   </div>  
   
   
   
  