using Repository.Models;
using System.Collections.Generic;

namespace Repository.Abstract
{
    public interface ISchemaRepository
    {
        IEnumerable<SchemaModel> GetTablesSchema(string table);
        IEnumerable<SchemaModel> Create(SchemaModel model);
        IEnumerable<SchemaModel> Update(SchemaModel model);
        IEnumerable<SchemaModel> Delete(SchemaModel model);
    }
}
