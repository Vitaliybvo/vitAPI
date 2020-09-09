using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Repository.Implementation
{
    public class BaseRepository
    {
        readonly string connectionString = "Server=(LocalDb)\\MSSQLLocalDB;Database=SimpleAdminApp;";

        protected SqlConnection Database() => new SqlConnection(connectionString);

        protected IEnumerable<T> Get<T>(string table, string query = null)
        {
            return Database().Query<T>(BuildSQL(table, query));
        }

        protected IEnumerable<dynamic> Get(string table, string query = null)
        {
            return Database().Query(BuildSQL(table, query));
        }

        private string BuildSQL(string table, string query)
        {
            var sql = $"SELECT * FROM {table}";

            if (!string.IsNullOrEmpty(query))
                sql += $" WHERE {query}";

            return sql;
        }
    }
}
