using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace Reisweb
{
    public class DBHelper
    {
        static bool isAccess=true;
        static string strReisDb = ReisUtils.GetDBConnection(out isAccess);

        DBHelper()
        {
            isAccess = true;
            strReisDb = ReisUtils.GetDBConnection(out isAccess);
            Console.Out.WriteLine("in");
        }


        //（无参）返回执行的行数(删除修改更新)   
        public static int ExecuteCommand(string safeSql)
        {

            return isAccess ? Reisweb.AccessDbHelper.ExecuteCommand(safeSql) : Reisweb.SqlDbHelper.ExecuteSql(safeSql);

        }

        //（无参）返回第一行第一列(删除修改更新)   
        public static object GetScalar(string safeSql)
        {
            return isAccess ? Reisweb.AccessDbHelper.GetScalar(safeSql) : Reisweb.SqlDbHelper.GetSingle(safeSql);
        }



        //返回一个DataTable   
        public static DataTable GetDataSet(string safeSql)
        {

            return isAccess ? Reisweb.AccessDbHelper.GetDataSet(safeSql) : Reisweb.SqlDbHelper.QueryDataTable(safeSql);

        }
        //存储过程
        public static int runProcedure(string procedureName, SqlParameter[] paramArray)
        {
            //影响行数
            int num = -1;
            Reisweb.SqlDbHelper.RunProcedure(procedureName, paramArray, out num);
            return num;
        }

    }
}
