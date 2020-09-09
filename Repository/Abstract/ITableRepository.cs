using System.Collections.Generic;

namespace Repository.Abstract
{
    public interface ITableRepository
    {
        IEnumerable<string> GetMyTables();
        IEnumerable<string> CreateTable(string table);
        IEnumerable<string> DeleteTable(string table);
    }
}
