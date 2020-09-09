using Dapper;
using Repository.Abstract;
using System.Collections.Generic;

namespace Repository.Implementation
{
    public class DataRepository : BaseRepository, IDataRepository
    {
        public IEnumerable<dynamic> GetAll(string table) 
        {
            return Get(table);
        }

        public dynamic GetItem(string table, int id)
        {
            return Database().QueryFirstOrDefault($"SELECT * FROM {table} WHERE id = {id}");
        }

        public dynamic Delete(string table, int id)
        {
            return Database().Execute($"DELETE FROM {table} WHERE Id = @id", new { id });
        }

        public dynamic Create(string table, IDictionary<string, object> model)
        {
            if (model.ContainsKey("Id"))
            {
                int.TryParse(model["Id"].ToString(), out int id);

                if (id > 0) return null;
            }
            
            var columns = "";
            var values = "";
            foreach (var item in model)
            {
                if (item.Key != "Id")
                {
                    columns += $"{item.Key}, ";
                    values += $"'{item.Value}', ";
                }
            }
            columns = columns.Remove(columns.Length - 2);
            values = values.Remove(values.Length - 2);

            var sql = $"INSERT INTO {table} ({columns}) VALUES ({values}) SELECT CAST(SCOPE_IDENTITY() as int)";

            var insertedId = Database().QuerySingle<int>(sql);

            return GetItem(table, insertedId);
        }

        public dynamic Update(string table, IDictionary<string, object> model)
        {
            int.TryParse(model["Id"].ToString(), out int id);

            if (id == 0) return null;

            var sql = $"UPDATE {table} SET ";

            foreach (var item in model)
            {
                if (item.Key != "Id")
                {
                    sql += $"{item.Key} = '{item.Value}', ";
                }
            }

            sql = sql.Remove(sql.Length - 2);

            sql += $" WHERE Id = {id}";

            Database().Execute(sql);

            return GetItem(table, id);
        }
    }
}
