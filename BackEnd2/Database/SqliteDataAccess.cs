using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using Dapper;

namespace BackEnd2.Database
{
    public class SqliteDataAccess
    {
        private readonly string connectionString;

        public List<T> LoadData<T, U>(string sqlstatement, U parameters, string connectionStringName)
        {
            var connectionString = connectionStringName;

            using (var con = new SQLiteConnection(connectionString))
            {
                // var cmd = new SQLiteCommand(sqlstatement, con);
                var rows = con.Query<T>(sqlstatement, parameters).ToList();
                return rows;
            }
        }

        public long SaveData<U>(string sqlstatement, U parameters, string connectionStringName,
            bool IsStoredProcedure = false)
        {
            var connectionString = connectionStringName;
            var commandType = CommandType.Text;
            if (IsStoredProcedure) commandType = CommandType.StoredProcedure;
            using (var con = new SQLiteConnection(connectionString))
            {
                con.Open();
                con.Execute(sqlstatement, parameters, commandType: commandType);


                var testl = con.LastInsertRowId;

                return Convert.ToInt32(testl);
            }
        }
    }
}