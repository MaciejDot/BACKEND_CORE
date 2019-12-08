using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackendCore.Security.DataConnection
{
    public interface ISqlDataConnection
    {
        Task<int> ExecuteAsync(string request, object param = null);
        Task<IEnumerable<T>> QueryAsync<T>(string query, object param = null);
        Task<T> QueryFirstAsync<T>(string query, object param = null);
    }
}