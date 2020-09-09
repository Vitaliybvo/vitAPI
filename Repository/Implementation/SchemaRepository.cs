using Dapper;
using Repository.Abstract;
using Repository.Models;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Implementation
{
    public class SchemaRepository : BaseRepository, ISchemaRepository
    {
        public IEnumerable<SchemaModel> GetTablesSchema(string table)
        {
            var query = !string.IsNullOrEmpty(table) ? $"TABLE_NAME = '{table}'" : null;

            return Get<Schema>("INFORMATION_SCHEMA.COLUMNS", query)
                .Select(c => new SchemaModel()
                {
                    Table = c.TABLE_NAME,
                    DataType = ConverDataType(c.DATA_TYPE),
                    Field = c.COLUMN_NAME
                });
        }

        public IEnumerable<SchemaModel> Create(SchemaModel model)
        {
            Database().Execute($"IF COL_LENGTH('dbo.{model.Table}', '{model.Field}') IS NULL ALTER TABLE [{model.Table}] ADD {model.Field} {ConvertToDataType(model.DataType)}");
            return GetTablesSchema(model.Table);
        }

        public IEnumerable<SchemaModel> Update(SchemaModel model)
        {
            return GetTablesSchema(model.Table);
        }

        public IEnumerable<SchemaModel> Delete(SchemaModel model)
        {
            Database().Execute($"IF COL_LENGTH('dbo.{model.Table}', '{model.Field}') IS NOT NULL ALTER TABLE [{model.Table}] DROP COLUMN {model.Field}");
            return GetTablesSchema(model.Table);
        }

        private string ConvertToDataType(string dataType)
        {
            return dataType switch
            {
                "string" => "nvarchar",
                "bool" => "bit",
                _ => dataType,
            };
        }

        private string ConverDataType(string dataType)
        {
            return dataType switch
            {
                "nvarchar" => "string",
                "varchar" => "string",
                "bit" => "bool",
                _ => dataType,
            };
        }
    }
}
