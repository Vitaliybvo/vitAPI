using Dapper;
using Repository.Abstract;
using System.Collections.Generic;

namespace Repository.Implementation
{
    public class TableRepository : BaseRepository, ITableRepository
    {
        public IEnumerable<string> GetMyTables()
        {
            return Database().Query<string>("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE'");
        }

        public IEnumerable<string> CreateTable(string table)
        {
            if (!IsTableExist(table))
            {
                Database().Execute($"CREATE TABLE [{table}]([Id] [int] IDENTITY(1, 1) NOT NULL)");
            }
            
            return GetMyTables();
        }

        public IEnumerable<string> DeleteTable(string table)
        {
            if (IsTableExist(table))
            {
                Database().Execute($"DROP TABLE [{table}]");
            }
            
            return GetMyTables();
        }

        private bool IsTableExist(string table)
        {
            return Database().QuerySingle<int>($"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='{table}'") != 0;
        }
    }

}
