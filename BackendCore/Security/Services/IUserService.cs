using BackendCore.Security.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackendCore.Security.Services
{
    public interface IUserService
    {
        Task<User> Authenticate(AuthenticationModel authenticationModel);
        Task<User> GetTokenForUser(string id);
    }
}
