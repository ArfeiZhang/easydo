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

public partial class shop_manage_deldt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strUrl = "";
        try { strUrl = Request.UrlReferrer.ToString(); }
        catch { }

        Response.Write(strUrl);
        //取得表名
        string strDt = Reisweb.ReisUtils.getRQ("dt","");
        //取得列名
        string strDc = Reisweb.ReisUtils.getRQ("key", "");
        //取得列值
        string strDcvalue = Reisweb.ReisUtils.getRQ("v", "");

        if (strDt != "" && strDc != "" && strDcvalue != "")
        {
            string strSql = "delete from " + strDt + " where " + strDc + "=" + strDcvalue + " ";
            Reisweb.DBHelper.ExecuteCommand(strSql);

        }

        Response.Redirect(strUrl);
        


        
        

    }

}
