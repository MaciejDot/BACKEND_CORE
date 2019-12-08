using BackendCore.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCore.Security.DataConnection
{
    public class SecurityDataServiceConnector : ISecurityDataServiceConnector
    {
        private readonly IOptions<AppOptions> _options;
        public SecurityDataServiceConnector(IOptions<AppOptions> options)
        {
            _options = options;
        }
        public ISqlDataConnection GetConnection()
        {
            return new SqlDataConnection(new SqlConnection(_options.Value.AdministrationDatabaseConnectionString));
        }
    }
}
