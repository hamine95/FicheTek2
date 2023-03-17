using System.Collections.Generic;

namespace BackEnd2.Database
{
    public interface ISqliteDataAccess
    {
        List<T> LoadData<T, U>(string sqlstatement, U parameters, string connectionStringName);

        void SaveData<U>(string sqlstatement, U parameters, string connectionStringName,
            bool IsStoredProcedure = false);
    }
}