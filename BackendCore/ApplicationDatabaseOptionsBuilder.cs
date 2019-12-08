using BackendCore.Configuration;
using BackendCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCore
{
    public class ApplicationDatabaseOptionsBuilder : DbContextOptionsBuilder<ApplicationDatabaseContext>
    {
        private readonly AppOptions _options;
        public ApplicationDatabaseOptionsBuilder(IOptions<AppOptions> options)
            :base()
        {
            _options = options.Value;
        }
        public DbContextOptions<ApplicationDatabaseContext> GetOptions()
        {
            return this.UseSqlServer(_options.ApplicationDatabaseConnectionString).Options;
        }
    }
}
