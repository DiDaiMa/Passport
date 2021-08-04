using System;
using System.Collections.Generic;
using MySql.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace Common
{
    public class DapperHelp<T> where T:class,new()
    {
        private string Conn = Config.MySqlString;

        //查询
        public List<T> ExecuteQuery(string sql,object obj=null)
        {
            using (IDbConnection connection = new SqlConnection(Conn))
            {
                connection.Open();
                return connection.Query<T>(sql, obj).ToList();
            }
        }

        //增删改
        public int ExecuteNonQuery(string sql, object obj = null)
        {
            using (IDbConnection connection = new SqlConnection(Conn))
            {
                connection.Open();
                return connection.Execute(sql, obj);
            }
        }
    }
}
