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

        string strFormat = "";
        strFormat = "<tr><td>[nid||]</td><td>[ntitle||]</td><td>[nview||]</td><td>[ncreatetime||]</td><td><a href=\"newsedit.aspx?nid=[nid||]\">修改</a> <a href=\"deldt.aspx?dt=News&key=nid&v=[nid||]\">删除</a></td></tr>";


        ltUserList.Text = Reisweb.ReisLister.doList("News", " order by nid desc", "t", strFormat, "20","");


    }
}
