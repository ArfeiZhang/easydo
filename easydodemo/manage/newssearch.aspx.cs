using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts; 
using System.Web.UI.HtmlControls;

public partial class shop_manage_userlist : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //避免一直返回第一项
        if (!Page.IsPostBack)
        {
            DataTable dtNclass = Reisweb.DBHelper.GetDataSet("select ncid,ncname from News_Class ");

            ddlNclass.DataSource = dtNclass;
            ddlNclass.DataTextField = "ncname";
            ddlNclass.DataValueField = "ncid";
            ddlNclass.DataBind();
        }
       


    }
    protected void btSearch_Click(object sender, EventArgs e)
    {
        string strNtitle = tbNtitle.Text;
        string strNclass =ddlNclass.SelectedValue;
        

        //构造查询字段
        if (strNtitle != "") strNtitle = "and   ntitle like '%" + strNtitle+ "%' ";
        if (strNclass != "") strNclass = "and   nclass = " + strNclass;


        string addSql = strNtitle + strNclass;

        addSql = " where " + addSql.Substring(4);
        //Response.Write(addSql);

        string strFormat = "";
        strFormat = "<tr><td>[nid||]</td><td>[ntitle||]</td><td>[nview||]</td><td>[ncreatetime||]</td><td><a href=\"newsedit.aspx?nid=[nid||]\">修改</a> <a href=\"deldt.aspx?dt=News&key=nid&v=[nid||]\">删除</a></td></tr>";


        ltUserList.Text =Reisweb.ReisLister.doList("News", addSql, "t", strFormat, "20","");
    }
}
