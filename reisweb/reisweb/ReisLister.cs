using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.Text;


namespace Reisweb
{
    /// <summary>
    /// ReisGetList 的摘要说明,智能字段为：[数据表中列名|值处理(左截left(n),右截right(n),中截mid(m,n))|值替换（比如：分类中显示的数据替换为分类名）]
    /// 例：string strList = "<tr><td>[ncid||0:零;1:壹;2:贰;3:叁;]</td><td>[ncname|mid(0,4)|]</td><td>[ncid||]</td></tr>";
    ///string getUserList =Reisweb.ReisGetList.getList("reis_news_class", "", "0", strList, "2");
    /// </summary>
    public class ReisLister
    {
        public ReisLister()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 重复列表，用于较多数据展示，带分页
        /// </summary>
        /// <param name="strSource">数据源，数据表或SQL</param>
        /// <param name="strAddSql">排序字串或其它，用在SQL后的</param>
        /// <param name="TorSql">表为t，SQL为s</param>
        /// <param name="strFormatStr">重复的填充字符串</param>
        /// <param name="numPaged">分页数</param>
        /// <param name="strBeforePager">分页器前代码</param>
        /// <returns>数据填充后的列表</returns>
        public static string doList(string strSource, string strAddSql, string TorSql, string strFormatStr, string numPaged,string strBeforePager)
        {
            //构造的SQL语句
            string strSql = "";

            //判断是SQL还是表名,t为表，s为sql
            if (TorSql == "t")
            {
                strSql = "select * from " + strSource + "";
            }
            else
            {
                strSql = strSource;
            }

            if (!string.IsNullOrEmpty(strAddSql))
            {
                strSql = strSql + " " + strAddSql;
            }


            //在编码中分拣替换字串



            //进行对显示字段的捕获
            MatchCollection mc;


            Regex r = new Regex(@"\[.*?\]"); //定义一个Regex对象实例
            //mc为验证组
            mc = r.Matches(strFormatStr);


            //<tr><td>[username||]</td><td>[password||]</td><td>[uid||]</td></tr>

            StringBuilder sb = new StringBuilder();

            DataTable dt2 = new DataTable();
            dt2 = DBHelper.GetDataSet(strSql);


            //对展示列进行处理，以便分页
            //当前页
            int nowPage = 0;

            try { nowPage = int.Parse(System.Web.HttpContext.Current.Request.QueryString["page"].ToString()); }
            catch { }

            if (nowPage < 0 || nowPage > dt2.Rows.Count) nowPage = 0;
            //总页数
            int allPage = (int)Math.Ceiling(dt2.Rows.Count / float.Parse(numPaged));


            //上一页
            int prePage = nowPage - 1;
            if (prePage < 0) prePage = 0;
            //下一页
            int nextPage = nowPage + 1;
            if (nextPage > allPage) nextPage = allPage;



            //开始记录数
            int startRow = int.Parse(numPaged) * nowPage;

            //结束记录数
            int endRow = int.Parse(numPaged) * nowPage + int.Parse(numPaged);
            if (endRow > dt2.Rows.Count) endRow = dt2.Rows.Count;

            //正则验证，用于取左，或取右，或取中值

            Regex regGetLorR = new Regex(@"\d+");
            Regex regGetMid = new Regex(@"\d+\,\d+");


            //对结果集的操作


            for (int k = startRow; k < endRow; k++)
            {

                string strTemp = "";
                strTemp = strFormatStr;


                for (int i = 0; i < mc.Count; i++)
                {
                    string s = mc[i].Value.ToString().Replace("[", "").Replace("]", "");
                    string[] strSplit = s.Split('|');
                    string getStr = dt2.Rows[k][strSplit[0]].ToString().Trim();
                    //左截
                    if (strSplit[1].IndexOf("left") >= 0)
                    {
                        Match m = regGetLorR.Match(strSplit[1]);
                        getStr = Left(getStr, int.Parse(m.Value));

                    }

                    //右截

                    if (strSplit[1].IndexOf("right") >= 0)
                    {
                        Match m = regGetLorR.Match(strSplit[1]);
                        getStr = Right(getStr, int.Parse(m.Value));

                    }

                    //中截

                    if (strSplit[1].IndexOf("mid") >= 0)
                    {
                        Match m = regGetMid.Match(strSplit[1]);

                        string[] si = m.Value.Split(',');
                        getStr = Mid(getStr, int.Parse(si[0]), int.Parse(si[1]));

                    }

                    //值替换

                    if (!string.IsNullOrEmpty(strSplit[2]))
                    {

                        Regex findIn = new Regex("" + getStr.Trim() + @".*?\;");
                        //Regex findIn = new Regex(@"0.*?\;");
                        Match m = findIn.Match(strSplit[2]);
                        //定位值边界索引
                        int a1 = m.Value.IndexOf(":")+1;
                        int a2 = m.Value.IndexOf(";");

                        if ((a1 >= 0) && (a2 >= 0))
                        {
                            getStr = Mid(m.Value, a1, a2 - a1);
                        }



                    }

                    strTemp = strTemp.Replace(mc[i].Value, getStr);
                }

                sb.Append(strTemp);
            }
            //取得queryString的参数

            string qs = System.Web.HttpContext.Current.Request.Url.Query;
            Regex getPage = new Regex(@"page\=\d*");
            Match mPage = getPage.Match(qs);
            if (!string.IsNullOrEmpty(qs))
            {
                if(!string.IsNullOrEmpty(mPage.Value)) qs = qs.Replace(mPage.Value, "")+"&";
            }
            else 
            {
                qs = "?";
            }
            if (qs != "?") qs = qs + "&";
            //if (!string.IsNullOrEmpty(qs)) {  }
            //if (qs.IndexOf("?") == 0) { qs.Substring(0); qs = "&"+qs; }
            

            //上一页
            string strPre = "";
            if (nowPage > 0) { strPre = "<a href=\"" + System.Web.HttpContext.Current.Request.CurrentExecutionFilePath + qs + "page=" + prePage +"\">上一页 </a>"; }
            
            //下一页
            string strNext = "";
            if ((nowPage + 1) != allPage) { strNext = "<a href=\"" + System.Web.HttpContext.Current.Request.CurrentExecutionFilePath + qs + "page=" + nextPage + "\"> 下一页</a>"; }
            //尾页
            string strLast = "";
            strLast = "<a href=\"" + System.Web.HttpContext.Current.Request.CurrentExecutionFilePath + qs + "page=" + (allPage - 1) + "\">尾页</a>";

            string strPage =strBeforePager+ "<table class=\"paged\"><tr><td><a href=\"" + System.Web.HttpContext.Current.Request.CurrentExecutionFilePath + qs + "page=" + 0 + "\">首页</a>   " + strPre + " " + strNext + " " + strLast + " 当前第" + (nowPage + 1) + "页 共" + allPage + "页</td></tr></table>";

            sb.Append(strPage);

            return sb.ToString();
        }

        //截取函数
        //从左截取
        public static string Left(string sSource, int iLength)
        {
            return sSource.Substring(0, iLength > sSource.Length ? sSource.Length : iLength);
        }
        //从右截取
        public static string Right(string sSource, int iLength)
        {
            return sSource.Substring(iLength > sSource.Length ? 0 : sSource.Length - iLength);
        }
        //取中间
        public static string Mid(string sSource, int iStart, int iLength)
        {
            int iStartPoint = iStart > sSource.Length ? sSource.Length : iStart;
            return sSource.Substring(iStartPoint, iStartPoint + iLength > sSource.Length ? sSource.Length - iStartPoint : iLength);



        }

        


    }
}