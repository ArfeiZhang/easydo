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
    /// 轻量级重复器
    /// 例：Reisweb.ReisRepeater.doReapeat("reis_news_class","","t","<tr><td>{0}</td><td>{3}</td></tr>")
    /// 最后一个参数，用{n}表示数据表中的字段，从0开始，比如上例中{3}表示reis_news_class中第四个数据列
    /// </summary>
    public class ReisRepeater
    {
        public ReisRepeater()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 返回重复字串
        /// </summary>
        /// <param name="strSource">数据源，可以为表名或SQL语句</param>
        /// <param name="strAddSql">排序字串或其它，用在SQL后的</param>
        /// <param name="strRowNum">数据条数,空值则返回所有,当数据源为SQL时，此字段无效</param>
        /// <param name="TorSql">表为t，sql为s</param>
        /// <param name="strRepeater">重复哪个字串</param>
        /// <returns></returns>
        public static string doReapeat(string strSource, string strAddSql,string strRowNum, string TorSql, string strRepeater)
        {
            //构造的SQL语句
            string strSql = "";
            string setRownum = "";

            if (!string.IsNullOrEmpty(strRowNum)) { setRownum = "top " + strRowNum; }
            
            //判断是SQL还是表名,0为表，1为sql
            if (TorSql == "t")
            {
                strSql = "select "+ setRownum +" * from " + strSource + "";
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


            

            MatchCollection mc;

            int[] matchposition = new int[20];
            Regex r = new Regex("{\\d*}"); //定义一个Regex对象实例
            //mc为验证组
            mc = r.Matches(strRepeater);


            //Response.Write(s);

            StringBuilder sb = new StringBuilder();

            DataTable dt = new DataTable();
            dt = DBHelper.GetDataSet(strSql);
            //对结果集的操作
            foreach (DataRow dr in dt.Rows)
            {
                string strTemp = "";
                strTemp = strRepeater;
                for (int i = 0; i < mc.Count; i++) //在输入字符串中找到所有匹配
                {

                    //取得字段索引
                    int t = int.Parse(mc[i].Value.Replace("{", "").Replace("}", ""));
                    strTemp = strTemp.Replace(mc[i].Value, dr[t].ToString().Trim());
                }
                sb.Append(strTemp);
            }
            return sb.ToString();
        }


    }
}