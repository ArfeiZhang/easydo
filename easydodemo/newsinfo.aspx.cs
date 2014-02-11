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

public partial class newsinfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strNewsId = "";
        try { strNewsId  = Request.QueryString["id"].ToString().Trim(); }
        catch { }
        string strNewsInfo = "<div id=\"infortitle\">{1}</div><span>发布时间:{5}</span><p>{4}</p>";
        if (strNewsId != "") 
        {
            Reisweb.DBHelper.ExecuteCommand("update News set nview=nview+1 where nid=" + strNewsId);
            ltNewsInfo.Text = Reisweb.ReisRepeater.doReapeat("News", "where nid=" + strNewsId, "1", "t",strNewsInfo);
        
        
        }


    }
}
