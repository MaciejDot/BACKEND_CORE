using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCore.Security.Models
{
    public class AuthenticationModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
