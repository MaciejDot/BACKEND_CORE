using BackendCore.Configuration;
using BackendCore.Security.DataConnection;
using BackendCore.Security.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
//using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace BackendCore.Security.Services
{
    public class UserService : IUserService
    {
        private readonly AppOptions _options;
        private readonly ISqlDataConnection _connection;
        private readonly IDistributedCache _cache;
        private readonly IHttpContextAccessor _httpContext;

        public UserService(IDistributedCache cache,// IHttpContextAccessor httpContext, 
            IOptions<AppOptions> options, ISecurityDataServiceConnector connector)
        {
            _cache = cache;
            //_httpContext = httpContext;
            _options = options.Value;
            _connection = connector.GetConnection();
        }

        public async Task<bool> AddUser(RegisterUser user)
        {
            var count = (await _connection.QueryFirstAsync<int>(@"Select count(*) from 
                [dbo].[AspNetUsers] where
                Email = @Username OR
                UserName = @Username OR
                Email = @Email OR
                UserName = @Email", user));
            if (count > 0)
            {
                return false;
            }
            /*
            var stamp = _securityHelper.RandomString(100);
            var passwordHash = _securityHelper.Hash(stamp, user.Password);
            var id = _securityHelper.RandomString(80);
            await _connection.ExecuteAsync(@"Insert into [dbo].[AspNetUsers](Id,Email,UserName,SecurityStamp,PasswordHash,EmailConfirmed) values (@id,@Email,@Username,@stamp,@passwordHash,@confirmed)", new { id, user.Email, user.Username, stamp, passwordHash, confirmed = false });
            */
            return true;
        }

        public async Task<User> GetTokenForUser(string id)
        {
            var user = (await _connection.QueryFirstAsync<SimpleUser>(@"Select  Top 1
                [Id]
                ,[UserName]
                ,[Email]
                ,[EmailConfirmed]
                ,[PasswordHash]
                ,[SecurityStamp]
                from [dbo].[AspNetUsers] where
                Id = @Id ", new { Id = id}));
            var roles = (await _connection.QueryAsync<string>(@"Select [Name] from [dbo].[AspNetRoles] 
                Where [Id] IN (Select [RoleId] from [dbo].[AspNetUserRoles] where [UserId] = @Id)", user)).ToList();
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_options.JwtTokenSecret);

            var claims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim("Id", user.Id));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new User
            {
                Id = user.Id,
                EmailConfirmed = user.EmailConfirmed,
                Token = tokenHandler.WriteToken(token),
                Roles = roles,
                UserName = user.UserName
            };
        }

        public async Task<User> Authenticate(AuthenticationModel authenticationModel)
        {
            var user = (await _connection.QueryFirstAsync<SimpleUser>(@"Select  Top 1
                [Id]
                ,[UserName]
                ,[EmailConfirmed]
                ,[PasswordHash]
                ,[SecurityStamp]
                from [dbo].[AspNetUsers] where
                Email = @Email ", new { Email = authenticationModel.Email }));
            if (GetHash(user.SecurityStamp, authenticationModel.Password) != user.PasswordHash)
            {
                throw new Exception("not valid model");
            }
            var roles =( await _connection.QueryAsync<string>(@"Select [Name] from [dbo].[AspNetRoles] 
                Where [Id] IN (Select [RoleId] from [dbo].[AspNetUserRoles] where [UserId] = @Id)", user)).ToList();
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_options.JwtTokenSecret);
            
            var claims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();
            claims.Add(new Claim(ClaimTypes.Email, authenticationModel.Email));
            claims.Add(new Claim("Id", user.Id));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new User
            {
                Id = user.Id,
                EmailConfirmed = user.EmailConfirmed,
                Token = tokenHandler.WriteToken(token),
                Roles = roles,
                UserName = user.UserName
            };
        }

        public async Task Register() { }

        private string GetHash(string stamp, string password)
        {
            var sha256 = SHA256.Create();
            var stampBytes = Encoding.ASCII.GetBytes(stamp);
            var passwordBytes = Encoding.ASCII.GetBytes(password);
            var finalHash = passwordBytes;
            for(int i = 0; i < 10000; i++)
            {
                finalHash = sha256.ComputeHash(finalHash.Union(stampBytes).ToArray());
            }
            return Convert.ToBase64String(finalHash);
        }

    }
}
