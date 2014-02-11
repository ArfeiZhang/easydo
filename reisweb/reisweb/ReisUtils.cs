using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace Reisweb
{
    /// <summary>
    /// ReisUtils 的摘要说明
    /// </summary>
    public class ReisUtils
    {
        public ReisUtils()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 防注入，返回过滤后的字串
        /// </summary>
        /// <param name="ParaName">源字串</param>
        /// <param name="ParaType">类型，1为数字，0为字串</param>
        /// <returns>经安全过滤的字串</returns>
        public static string SafeRequest(string ParaName, int ParaType)
        {
            //如果是1为数字，0为字符串
            string Paravalue = "";
            Paravalue = ParaName;
            if (ParaType == 1)
            {
                if (!(IsNumeric(Paravalue)))
                {
                    Paravalue = "0";
                }
            }
            else
            {
                Paravalue = Paravalue.Replace("'", "’");
            }
            return (Paravalue);
        }
        /// <summary>
        /// 检测是否是数字
        /// </summary>
        /// <param name="strData">字串</param>
        /// <returns> 布尔值</returns>
        public static bool IsNumeric(string strData)
        {
            float fData;
            bool bValid = true;
            if (strData.Length > 12)
            {
                bValid = false;
            }
            else
            {
                try
                {
                    fData = float.Parse(strData);
                }
                catch (FormatException)
                {
                    bValid = false;
                }
            }
            return bValid;
        }


        /*以上防注入使用例子
         * private void Button1_Click(object sender, System.EventArgs e)
        {
            Label1.Text = SafeRequest(TextBox1.Text, 1);
        }*/

        ///<summary>
        /// 检测传入Request.QueryString对字符串判断是否合法
        ///</summary>
        ///<param name="str">Request.QueryString</param>
        ///<returns>bool</returns>
        public static bool UrlToFilter(String str)
        {
            if (str.IndexOf("'") > 0 || str.IndexOf("%3b") > 0 || str.IndexOf("+") > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        ///<summary>
        /// 对用户输入的文本框进行过滤,替换危险字符
        ///</summary>
        ///<param name="str">用户输入的字符串</param>
        ///<returns>String</returns>
        public static string StringToFilter(String str)
        {
            str = str.ToLower().Replace("xp_cmdshell", "xp_cmdshellx");
            str = str.ToLower().Replace("/add", "/addx");
            str = str.ToLower().Replace("exec", "execx");
            str = str.ToLower().Replace("%", "％");
            str = str.ToLower().Replace("select", "selectx");
            str = str.ToLower().Replace("count", "countx");
            str = str.ToLower().Replace("'", "’");
            str = str.ToLower().Replace(":", "：");
            str = str.ToLower().Replace("insert", "insertx");
            str = str.ToLower().Replace("delete", "delete fromx");
            str = str.ToLower().Replace("drop", "dropx");
            str = str.ToLower().Replace("update", "updatex");
            str = str.ToLower().Replace("truncate", "truncatex");
            str = str.ToLower().Replace("iframe", "xiframex");
            str = str.ToLower().Replace("<script>", ">script%ltx");
            str = str.ToLower().Replace("</script>", "</scriptx>");
            str = str.ToLower().Replace("or", "orx");
            str = str.ToLower().Replace("and", "andx");
            str = str.ToLower().Replace("*", "＊");
            str = str.ToLower().Replace("chr", "chrx");
            str = str.ToLower().Replace("mid", "midx");
            str = str.ToLower().Replace("master", "masterx");
            str = str.ToLower().Replace("truncate", "truncatex");
            str = str.ToLower().Replace("char", "charx");
            str = str.ToLower().Replace("declare", "declarex");
            str = str.ToLower().Replace("join", "joinx");
            return str;
        }


        /**/
        /// <summary>  
        /// MD5 16位加密  
        /// </summary>  
        /// <param name="ConvertString"></param>  
        /// <returns></returns>  
        public static string GetMd5Str(string ConvertString)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(ConvertString)), 4, 8);
            t2 = t2.Replace("-", "");
            return t2;
        }

        /// <summary>
        /// 取得传入参数
        /// </summary>
        /// <param name="strQueryName">参数名</param>
        /// <param name="strDefaultValue">默认值</param>
        /// <returns></returns>
        public static string getRQ(string strQueryName, string strDefaultValue)
        {

            string strTemp = "";

            try { strTemp = System.Web.HttpContext.Current.Request[strQueryName].ToString().Trim(); }
            catch { strTemp = strDefaultValue; }
            return strTemp;
        }

        /// <summary>
        /// 用来上传文件的目录，如果不存在则创建
        /// </summary>
        /// <param name="CreateDirectoryPath">要用来上传文件的目录，如果不存在则创建。使用虚拟目录</param>
        public static string CreateDirecotry()
        {
            string uploadDirecotry = DateTime.Now.ToString("yyyyMMdd");
            string DirectoryPath = ConfigurationManager.AppSettings["SitePath"].ToString() + "/userfiles/" + uploadDirecotry + "/";
            if (Directory.Exists(HttpContext.Current.Server.MapPath(DirectoryPath)) == false)
            {
                //****创建文件夹
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(DirectoryPath));
            }
            return DirectoryPath;
        }

         public static string UTF8ToGB2312(string str)
        {
            try
            {
                Encoding utf8 = Encoding.GetEncoding(65001);
                Encoding gb2312 = Encoding.GetEncoding("gb2312");
                byte[] temp = utf8.GetBytes(str);
                byte[] temp1 = Encoding.Convert(utf8, gb2312, temp);
                string result = gb2312.GetString(temp1);
                return result;
            }
            catch
            {
                return null;
            }
        }

        public static string GB2312ToUTF8(string str)
        {
            try
            {
                Encoding uft8 = Encoding.GetEncoding(65001);
                Encoding gb2312 = Encoding.GetEncoding("gb2312");
                byte[] temp = gb2312.GetBytes(str);
                byte[] temp1 = Encoding.Convert(gb2312, uft8, temp);
                string result = uft8.GetString(temp1);
                return result;
            }
            catch
            {
                return null;
            }
        }
        //取连接字符串，ACCESS路径则直接写在ReisDb中，SQL则把connectionString的name写在ResiDb中，格式是connectionStrings.name
        public static string GetDBConnection(out bool isAccess) {
            string connectionString = ConfigurationManager.AppSettings["ReisDb"].ToString();
            isAccess = true;
            if (connectionString.IndexOf("connectionStrings") > -1) {
                //如果是去读connectionStrings配置
                connectionString=ConfigurationManager.ConnectionStrings[connectionString.Split('.')[1]].ConnectionString;
                isAccess = false;
            }
            return connectionString ;
        }


    }
}
