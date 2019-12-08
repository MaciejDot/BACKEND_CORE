using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace BackendCore.Configuration
{
    public class AppOptions
    {
        public string ApplicationDatabaseConnectionString { get; set; }
        public string AdministrationDatabaseConnectionString { get; set; }
        public string JwtTokenSecret { get; set; }
    }
}
