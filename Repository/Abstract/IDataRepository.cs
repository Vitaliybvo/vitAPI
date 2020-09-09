using System.Collections.Generic;

namespace Repository.Abstract
{
    public interface IDataRepository
    {
        IEnumerable<dynamic> GetAll(string table);
        dynamic GetItem(string table, int id);
        dynamic Create(string table, IDictionary<string, object> model);
        dynamic Update(string table, IDictionary<string, object> model);
        dynamic Delete(string table, int id);
    }
}
