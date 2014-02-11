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
        strFormat = "<tr><td>[ncid||]</td><td>[ncname||]</td><td><a href=\"newsclasslist.aspx?ncid=[ncid||]\">修改</a> <a href=\"deldt.aspx?dt=news_class&key=ncid&v=[ncid||]\">删除</a></td></tr>";


        ltUserList.Text = Reisweb.ReisLister.doList ("news_Class", " order by ncid desc", "t", strFormat, "20","");

        //修改类别
        string strNcid = Reisweb.ReisUtils.getRQ("ncid", "");

        if (strNcid != "")
        {
            string strFormat3 = "[ncname|text||类别名称||notnull||][ncparentid|select|sql<select ncname,ncid from news_Class where ncistop=0>|上级分类||||][ncistop|hidden|1|||||]";

            ltModifyClass.Text = "<h5>修改类别</h5>    " + Reisweb.ReisEditer.doEdit("News_Class", "ncid=" + strNcid + "", strFormat3, "<script>alert('修改成功');</script>");

        }
        else
        {
            //新增类别

            string strFormat2 = "[ncname|text||类别名称||notnull||][ncparentid|select|sql<select ncname,ncid from News_Class where ncistop=0>|上级分类||||][ncistop|hidden|1|||||]";

            ltAddNewClass.Text = "<h5>新增分类</h5> " + Reisweb.ReisInserter.doInsert("News_Class", strFormat2, "<script>alert('添加成功');</script>");
        }

    }
}
