using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCore.Security.Models
{
    public class User
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public bool EmailConfirmed { get; set; }
        public string Token { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
