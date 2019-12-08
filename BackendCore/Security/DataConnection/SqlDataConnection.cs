using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCore.Security.DataConnection
{
    public class SqlDataConnection : ISqlDataConnection
    {
        private readonly IDbConnection _connection;
        public SqlDataConnection(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> ExecuteAsync(string request, object param = null)
        {
            return await _connection.ExecuteAsync(request, param);
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string query, object param = null)
        {
            return await _connection.QueryAsync<T>(query, param);
        }

        public async Task<T> QueryFirstAsync<T>(string query, object param = null)
        {
            return await _connection.QueryFirstAsync<T>(query, param);
        }
    }
}
