<%@ Control Language="C#" AutoEventWireup="true" CodeFile="header.ascx.cs" Inherits="uc_header" %>
<div id="header">
  <!--topNavbox start-->

  <!--miannav start-->
  <div id="miannav">
   <div id="logo"><a href="http://www.reisweb.cn">锐思网络工作室</a></div>
   <div id="nav">
    <ul>
	 
	 <li><a href="index.aspx" title="首页">首页</a></li>
	 <% =Reisweb.ReisRepeater.doReapeat("news_class", "", "", "t", "<li><a href=\"index.aspx?class={0}\" title=\"{3}\">{3}</a></li>")%>
<li><a href="manage/newslist.aspx" title="管理后台">管理后台</a></li>
	</ul>
   </div>
  </div>
  <!--search start-->
  <div id="search">
   <div id="menutitle"><a href="/user/shopcart.aspx">购物车内有 10 件商品</a></div>
   <div id="searchbox"><form action="/search.aspx" method="get" target="_blank" ><input name="k" type="text"  id="k"/><input value="搜索"  name="ok" type="submit"  id="botton"/></form></div>
  </div>
 </div>
