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

public partial class newslist : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string newsClassId = Reisweb.ReisUtils.getRQ("class", "");

        string sql = "select * from News order by nid desc";
        if (newsClassId != "") { sql = "select * from News where nclass=" + newsClassId + " order by nid desc"; }

        string strFormat = "<li><a href=\"newsinfo.aspx?id=[nid||]\" target=\"_blank\" title=\"[ntitle||]\">[ntitle||]</a><span>[ncreatetime||]</span></li>";
        ltNewsList.Text =Reisweb.ReisLister.doList(sql, "", "s", strFormat, "14","");
    }
}

