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
using System.Collections.Generic;
using System.Data.SqlClient;


namespace Reisweb
{
    /// <summary>
    /// 构成表单，插入数据
    /// </summary>
    public class ReisEditer
    {
        public ReisEditer()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ExcuteTable">表名</param>
        /// <param name="strLimit">限定字串，如：id=8</param>
        /// <param name="tests">智能标签， 智能字段说明：[1数据表内列名|2控件类型|3默认值或SQL语句 sql<select a,b from c> ,sql可以取其它表中的值|4控件前的说明|5控件附加字串,将会放到控件代码中,比如加入键盘按下事件以验证内容长度或数字|6对输入值进行验证（num|email|notnull）| 7 输入说明，如：这里填写标题|8是否安全检测，非空则检测]</param>
        /// <param name="strShow">执行成功后的显示字串，可直接写JS，比如：<script>alert('添加成功');</script> </param>
        /// <returns></returns>
        public static string doEdit(string ExcuteTable, string strLimit, string strFormat, string strShow, bool isProcedure = false, DataTable dtPreData = null)
        {

            string tests = "";
            //表单名，用以处理不同的FORM
            //ExcuteTable = "Reis_News";
            string formname = ExcuteTable;
            //tests = "[ntitle|text||标题][ncontent|textarea||内容]";
            tests = strFormat.Replace("][", "*").Replace("[", "").Replace("]", "");

            //结果值，附加到FORM后面
            string insertResult = "";
            //控件组
            string[] controls = tests.Split('*');

            //控件名称组，用于从FROM里取值
            string[] controlsName = new string[controls.Length];
            //表列名组，用于构造SQL以取得默认值
            string[] dataColun = new string[controls.Length];
            //错误值
            string errmsg = "";


            //取得控件名的正则
            Regex regGetInputName = new Regex(@"^\w*");
            Regex regNum = new Regex(@"^\d*$");
            Regex regEmail = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");

            //填入控件名
            for (int i = 0; i < controlsName.Length; i++)
            {

                Match match = regGetInputName.Match(controls[i]);
                //取得数据表中字段名
                dataColun[i] = match.Value;
                //构造控件名
                controlsName[i] = "reis_" + formname + "_" + match.Value;

            }
            bool isPostBack = false;

            try
            {

                if (System.Web.HttpContext.Current.Request.Form[0] != null) { isPostBack = true; }

            }
            catch { }

            if (isPostBack)
            {

                //构成完整的SQL
                string allSql = "UPDATE " + ExcuteTable + " set ";
                //sql中的列名
                string colun = "";
                //sql中的列值
                string values = "";
                //控件名前缀，用于构串时去除，以和数据表中列名恢复一致
                string colunPre = "reis_" + formname + "_";
                //存储过程的参数列表
                List<SqlParameter> paramList = new List<SqlParameter>();
                string columnName = null;
                string columnValue = null;
                for (int i = 0; i < controlsName.Length; i++)
                {
                    columnName = controlsName[i].Replace(colunPre, "");
                    colun = colun + "," + controlsName[i].ToString();
                    if (controls[i].IndexOf("file") < 0)
                    {
                        string[] c = controls[i].Split('|');
                        //是否对默认值进行安全检测
                        string v = "";
                        if (string.IsNullOrEmpty(c[7]))
                        {
                            v = System.Web.HttpContext.Current.Request.Form[controlsName[i]].ToString().Trim();
                        }
                        else
                        {
                            v = ReisUtils.StringToFilter(System.Web.HttpContext.Current.Request.Form[controlsName[i]].ToString().Trim());
                        }




                        if (c[5] == "num")//验证是否为数字，如果是的话，构SQL串，不是则返错误值
                        {

                            if (regNum.Match(v).Success && v.Length > 0)
                            {
                                values = values + ",[" + dataColun[i] + "]=" + v + "";
                            }
                            else { errmsg = errmsg + "'" + c[3] + "'" + "要求填写为数字<br/>"; break; }

                        }
                        else if (c[5] == "email")//验证是否为email，如果是的话，构SQL串，不是则返错误值
                        {

                            if (regEmail.Match(v).Success && v.Length > 0)
                            {
                                values = values + ",[" + dataColun[i] + "]='" + v + "'";
                            }
                            else { errmsg = errmsg + "'" + c[3] + "'" + "要求填写为邮件地址<br/>"; break; }

                        }
                        else if (c[5] == "notnull")//验证是否为非空，如果是的话，构SQL串，不是则返错误值
                        {

                            if (string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.Form[controlsName[i]]))
                            { errmsg = errmsg + "'" + c[3] + "'" + "要求不能为空<br/>"; break; }


                            else
                            {

                                values = values + ",[" + dataColun[i] + "]='" + v + "'";
                            }

                        }
                        else if (!(controls[i].IndexOf("html") < 0))//如果是HTML编辑器，则进行HTMLDECODE
                        {

                            values = values + ",[" + dataColun[i] + "]='" + System.Web.HttpUtility.HtmlDecode(System.Web.HttpContext.Current.Request.Form[controlsName[i]].ToString().Trim()) + "'";

                        }
                        else
                        {

                            values = values + ",[" + dataColun[i] + "]='" + v + "'";
                        }

                        columnValue = v;

                    }
                    else
                    {
                        int fileLenth = 0;
                        string saveName = DateTime.Now.ToString("yyyymmddhhmmssffff") + ".jpg";

                        try
                        {

                            fileLenth = System.Web.HttpContext.Current.Request.Files[controlsName[i]].ContentLength;
                        }
                        catch { }
                        //如果文件字节数为零，则不参于构串，不创建文件
                        if (fileLenth != 0)
                        {



                            string saveDirectory = ReisUtils.CreateDirecotry();


                            System.Web.HttpContext.Current.Request.Files[controlsName[i]].SaveAs(System.Web.HttpContext.Current.Request.MapPath(saveDirectory) + saveName);

                            values = values + ",[" + dataColun[i] + "]='" + saveDirectory + saveName + "'";

                            columnValue = saveDirectory + saveName;
                            System.Threading.Thread.Sleep(500);


                        }
                    }
                    paramList.Add(new SqlParameter("@" + columnName, columnValue));

                }
                //去除列名与列值前的第一个逗号
                colun = colun.Substring(1).Replace(colunPre, "");
                if (values.Length > 2) values = values.Substring(1);

                //构造SQL
                allSql = allSql + values + " where " + strLimit;

                //如果错误信息没有，则执行数据
                if (errmsg.Length > 0)
                {
                }
                else
                {

                    //操作成功后说明
                    //选择使用存储过程或直接SQL操作数据库
                    int intInsert = (isProcedure ? Reisweb.DBHelper.runProcedure(ExcuteTable, paramList.ToArray()) : Reisweb.DBHelper.ExecuteCommand(allSql));

                    if (string.IsNullOrEmpty(strShow))
                    {
                        //Response.Write(allSql + "<br/>");
                        insertResult = "操作成功，影响数据行数： " + intInsert + "<br/>" + DateTime.Now.ToString();
                    }
                    else
                    {
                        insertResult = strShow;
                    }

                }

            }

            //取得默认值表
            //默认字段连接口串
            string addColun = "";
            for (int i = 0; i < dataColun.Length; i++)
            {
                addColun = addColun + "," + dataColun[i];
            }
            //去除列串前的第一个" , "符号
            addColun = addColun.Substring(1);

            DataTable dtGetValue = (dtPreData == null) ? Reisweb.DBHelper.GetDataSet("select " + addColun + " from " + ExcuteTable + " where " + strLimit) : dtPreData;
            //判断该内容是否存在
            if (dtGetValue.Rows.Count == 0)
            {
                return "未找到相应的内容。";

            }


            //构造FORM
            StringBuilder returnForm = new StringBuilder();
            returnForm.Append("<table width=\"98%\" id=\"ReisEditer\"><form name=\"" + formname + "\" method=\"post\" enctype=\"multipart/form-data\">");
            //添加FORM内的控件
            for (int i = 0; i < controls.Length; i++)
            {
                string s = controls[i].ToString();
                string[] control = s.Split('|');
                //Response.Write("0" + control[0] + "1" + control[1] + "2" + control[2]+"<br>");
                //returnForm.Append(getInput(control[0], control[1].Trim(), "同意"));
                returnForm.Append("<tr><th>" + control[3].ToString() + "</th><td>");
                //当控件有默认值时，用默认值，否则用表中值
                string defaultValue = "";
                if (control[2].ToString().Trim().Length > 0)
                {
                    defaultValue = control[2].ToString().Trim();
                }
                else
                {
                    defaultValue = dtGetValue.Rows[0][control[0]].ToString().Trim();


                }

                //checkbox,radio,select选中状态呈现，利用控件附加字串，传入默认选中值。在相关的控件段进行对比，以设置选中状态

                if ((control[1].Trim() == "checkbox") || (control[1].Trim() == "radio") || (control[1].Trim() == "select")) control[4] = dtGetValue.Rows[0][control[0]].ToString().Trim();


                returnForm.Append(getInput("reis_" + formname + "_" + control[0].ToString(), control[1].ToString(), defaultValue, control[4].ToString()));
                returnForm.Append(control[6]);
                returnForm.Append("</td></tr>");
            }
            returnForm.Append("<tr><td colspan=\"2\" id=\"button\"><input type=\"submit\" name=\"Submit\" value=\"提交\"/>");
            returnForm.Append("<input type=\"reset\" name=\"Reset\"  value=\"重置\" /></td></tr>");
            returnForm.Append("</form></table>");

            if (errmsg.Length > 0)//判断是否有错误信息,有则显示错误信息，无则是显示数据执行结果
            {
                returnForm.Append("未成功操作：<br/>" + errmsg);
            }
            else
            {
                returnForm.Append(insertResult);
            }



            return returnForm.ToString();





        }
        /// <summary>
        /// 依格式化的值构成控件，比如：[ntitle|text||标题|onclick=\"alert('123')\"]
        /// </summary>
        /// <param name="InputName">对应表的字段</param>
        /// <param name="InputType">控件类型</param>
        /// <param name="InputValue">控件的默认值</param>
        /// <param name="addInControl">控件附加字串，比如加入键盘按下事件以验证内容长度或数字</param>
        /// <returns></returns>

        public static string getInput(string InputName, string InputType, string InputValue, string addInControl)
        {

            //sql提值
            Regex regIsSql = new Regex(@"(?<=sql\<).*");
            Match match = regIsSql.Match(InputValue);

            if (match.Success)
            {
                InputValue = "";
                string strSql = match.Value;

                strSql = strSql.Replace(">", "");

                DataTable dt = new DataTable();
                dt = DBHelper.GetDataSet(strSql);

                foreach (DataRow dr in dt.Rows)
                {
                    //一个字段时，适用于文本框默认值
                    if (dr.ItemArray.Length == 1)
                    {
                        InputValue = dr[0].ToString() + "," + InputValue;
                    }
                    else
                    {
                        //两个字段时，适用于下拉，列表等复值控件
                        InputValue = dr[0].ToString() + ":" + dr[1].ToString() + ";" + InputValue;

                    }

                }



            }



            string strReturnControl = "";
            if (InputType == "text")
            {

                strReturnControl = "<input type=\"text\" name=\"" + InputName + "\" value=\"" + InputValue + "\" " + addInControl + "/>";

            }

            else if (InputType == "password")
            {

                strReturnControl = "<input type=\"password\" name=\"" + InputName + "\" value=\"" + InputValue + "\" " + addInControl + "/>";
            }
            else if (InputType == "hidden")
            {

                strReturnControl = "<input type=\"hidden\" name=\"" + InputName + "\" value=\"" + InputValue + "\"/>";
            }
            else if (InputType == "textarea")
            {
                strReturnControl = "<textarea name=\"" + InputName + "\"  " + addInControl + ">" + InputValue + "</textarea>";
            }
            else if (InputType == "checkbox")
            {
                //构造内层内容
                string strInControl = "";
                string[] EachSelect = InputValue.Split(';');
                //Response.Write(EachSelect.Length);
                for (int i = 0; i < EachSelect.Length; i++)
                {
                    // Response.Write(EachSelect[i] + "<br>");
                    //清除空值
                    if (!(EachSelect[i].IndexOf(':') < 0))
                    {
                        //Response.Write(EachSelect[i] + "<br>");
                        string[] Select = EachSelect[i].Split(':');
                        //选中状态
                        string strSelected = "";
                        if (Select[1] == addInControl) strSelected = "checked=\"checked\"";
                        strInControl = strInControl + "<input type=\"checkbox\" name=\"" + InputName + "\" value=\"" + Select[1] + "\" " + strSelected + " /> " + Select[0];


                    }


                }


                strReturnControl = strInControl;



            }
            else if (InputType == "radio")
            {

                //构造内层内容
                string strInControl = "";
                string[] EachSelect = InputValue.Split(';');
                //Response.Write(EachSelect.Length);
                for (int i = 0; i < EachSelect.Length; i++)
                {
                    // Response.Write(EachSelect[i] + "<br>");
                    //清除空值
                    if (!(EachSelect[i].IndexOf(':') < 0))
                    {


                        //Response.Write(EachSelect[i] + "<br>");
                        string[] Select = EachSelect[i].Split(':');

                        string strSelected = "";
                        if (Select[1] == addInControl) strSelected = "checked=\"checked\"";

                        strInControl = strInControl + "<input type=\"radio\" name=\"" + InputName + "\" value=\"" + Select[1] + "\" " + strSelected + " /> " + Select[0];


                    }


                }


                strReturnControl = strInControl;



            }
            else if (InputType == "select")
            {//构造内层内容
                string strInControl = "";
                string[] EachSelect = InputValue.Split(';');
                for (int i = 0; i < EachSelect.Length; i++)
                {

                    //清除空值
                    if (!(EachSelect[i].IndexOf(':') < 0))
                    {
                        string[] Select = EachSelect[i].Split(':');
                        string strSelected = "";
                        if (Select[1] == addInControl) strSelected = "selected=\"selected\"";
                        strInControl = strInControl + "<option value=\"" + Select[1] + "\" " + strSelected + " >" + Select[0] + "</option>";
                    }


                }


                strReturnControl = "<select name=\"" + InputName + "\">" + strInControl + "</select>";


            }
            else if (InputType == "file")
            {
                strReturnControl = " <input type=\"file\" name=\"" + InputName + "\" value=\"\"/> ";

                if (InputValue != "")
                {
                    strReturnControl = strReturnControl + "<a href=\"" + InputValue + "\" target=\"_blank\">查看源图</a>";
                }
            }
            else if (InputType == "html")
            {
                strReturnControl = "<div><input type=\"hidden\" id=\"" + InputName + "\" name=\"" + InputName + "\" value=\"" + InputValue + "\" /><input type=\"hidden\" id=\"" + InputName + "___Config\" value=\"HtmlEncodeOutput=true\" /><iframe id=\"" + InputName + "___Frame\" src=\"" + ConfigurationManager.AppSettings["SitePath"].ToString() + "/fckeditor/editor/fckeditor.html?InstanceName=" + InputName + "&amp;Toolbar=Default\" width=\"600\" height=\"400px\" frameborder=\"no\" scrolling=\"no\"></iframe></div>";
            }


            return strReturnControl;
        }




    }


}