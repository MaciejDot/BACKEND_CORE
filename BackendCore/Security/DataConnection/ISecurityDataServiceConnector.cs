using System.Data;

namespace BackendCore.Security.DataConnection
{
    public interface ISecurityDataServiceConnector
    {
        ISqlDataConnection GetConnection();
    }
}