using LayuiTableGenerate.Classes;
using LayuiTableGenerate.Model;
using ServiceStack;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static LayuiTableGenerate.Enum.Enum;

namespace LayuiTableGenerate.Repository
{
    public class GetDataBaseHelper
    {


        public static List<string> GetDataBaseTable(string dbCon, int dbType)
        {
            //SELECT objname,value FROM ::fn_listextendedproperty (NULL, 'user', 'dbo', 'table', 'KnowledgeType', 'column', default)


            // mysql select distinct column_name as 字段名,column_comment as 字段备注 from information_schema.columns where table_schema = 'aigo_suggestionbox' and table_name = 'suggestioncategory'
            if (dbType == (int)DbType.MySql)
            {
                var dbFactory = new OrmLiteConnectionFactory(dbCon, MySqlDialect.Provider);
                var database = dbCon.Replace("Database=", "").Split(';')[0];
                using (var db = dbFactory.Open())
                {
                    var sql = "SELECT table_name  FROM information_schema.tables  WHERE table_schema = '"+ db.Database + "'  ORDER BY table_name DESC";
                    var res = db.Query<string>(sql).ToList();
                    return res;
                }
            }
            if (dbType == (int)DbType.SqlServer)
            {
                var dbFactory = new OrmLiteConnectionFactory(dbCon, SqlServerDialect.Provider);
                using (var db = dbFactory.Open())
                {
                    var sql = "SELECT Name FROM SysObjects Where XType='U' ORDER BY Name";
                    var res = db.Query<string>(sql).ToList();
                    return res;
                }
            }

            return null;
        }

        public static List<column> GetColumnList(int dbType, string dbCon, string dbTable)
        {
            if (dbType == (int)DbType.MySql)
            {
                var dbFactory = new OrmLiteConnectionFactory(dbCon, MySqlDialect.Provider);

                using (var db = dbFactory.Open())
                {
                    var sql = "select distinct column_name as columnTitle,column_comment as columnDes from information_schema.columns where  table_name = '"+ dbTable + "'";
                    var res = db.Query<column>(sql).ToList();

                    foreach (var item in res)
                    {
                        if (item.ColumnDes=="")
                        {
                            item.ColumnDes = item.ColumnTitle;
                        }
                    }
                    return res;
             
                }
            }
            if (dbType == (int)DbType.SqlServer)
            {
                var dbFactory = new OrmLiteConnectionFactory(dbCon, SqlServerDialect.Provider);
                using (var db = dbFactory.Open())
                {
                    var sql = "SELECT objname as ColumnTitle ,value as columnDes FROM ::fn_listextendedproperty (NULL, 'user', 'dbo', 'table', '" + dbTable + "', 'column', default)";
                    var res = db.Query<column>(sql).ToList();
                    if (res.Count==0)
                    {
                        var sql2 = "select t.column_name as columnTitle,'' as columnDes   from information_schema.columns t where t.table_name='" + dbTable + "'";
                        res = db.Query<column>(sql2).ToList();
                    }
                    foreach (var item in res)
                    {
                        if (item.ColumnDes == "")
                        {
                            item.ColumnDes = item.ColumnTitle;
                        }
                    }
                    return res;
                }
            }
            return null;
        }




    }
}
