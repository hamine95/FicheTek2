using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Transactions;
using Dapper;

namespace BackEnd2.Database
{
    public class SqliteDataAccess
    {
        private readonly string connectionString;
        public SqliteDataAccess()
        {
        }

        public List<T> LoadData<T, U>(string sqlstatement, U parameters, string connectionStringName)
        {
            string connectionString = connectionStringName;

            using (SQLiteConnection con = new SQLiteConnection(connectionString))
            {
                // var cmd = new SQLiteCommand(sqlstatement, con);
                List<T> rows = con.Query<T>(sqlstatement, parameters).ToList();
                return rows;
            }
        }
        public long SaveData<U>(string sqlstatement, U parameters, string connectionStringName, bool IsStoredProcedure = false)
        {
            string connectionString = connectionStringName;
            CommandType commandType = CommandType.Text;
            if (IsStoredProcedure)
            {
                commandType = CommandType.StoredProcedure;
            }
            using (SQLiteConnection con = new SQLiteConnection(connectionString))
            {
               
                con.Open();
                con.Execute(sqlstatement, parameters, commandType: commandType);
             
               
                long testl = con.LastInsertRowId;
               
                    return Convert.ToInt32(testl);
                
               

            }
        }

    
    }
}