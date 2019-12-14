using BackendCore.Configuration;
using BackendCore.Data;
using BackendCore.Security.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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

namespace BackendCore.Security.Services
{
    public class UserService : IUserService
    {
        private readonly AppOptions _options;
        private readonly IDistributedCache _cache;
        private readonly Random _random;
        private readonly ApplicationDatabaseContext _context;

        public UserService( Random random, 
            IOptions<AppOptions> options, 
            ApplicationDatabaseContext context)
        {
            _context = context;
            _random = random;
            _options = options.Value;
        }

        public string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(26 * _random.Next(26) + 65);
                builder.Append(ch);
            }
            return builder.ToString();
        }

        public async Task<bool> AddUser(RegisterUser user, CancellationToken cancellationToken)
        {
            if (await _context.AspNetUsers.AnyAsync(x=>x.Email == user.Email|| x.NormalizedEmail == user.Email || x.UserName == user.Username || x.NormalizedUserName == user.Username, cancellationToken))
            {
                return false;
            }
            var stamp = RandomString(100);
            var passwordHash = GetHash(stamp, user.Password);
            var id = RandomString(80);
            await _context.AspNetUsers.AddAsync(new AspNetUsers { Id = id, Email = user.Email, UserName = user.Username, SecurityStamp = stamp, PasswordHash = passwordHash }, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<User> GetTokenForUser(string id, CancellationToken cancellationToken)
        {
            var userData = await _context.AspNetUsers.Select(x=> new {User = x, Roles = x.AspNetUserRoles.Select(x => x.Role.Name).ToList() }).SingleAsync(x => x.User.Id == id, cancellationToken);
            var user = userData.User;
            var roles = userData.Roles;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_options.JwtTokenSecret);

            var claims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim("Name", user.UserName));
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

        public async Task<User> Authenticate(AuthenticationModel authenticationModel, CancellationToken cancellationToken)
        {
            var userData = await _context.AspNetUsers
                .Select(x => new { User = x , Roles = x.AspNetUserRoles.Select(x => x.Role.Name).ToList()})
                .SingleAsync( x=>x.User.Email == authenticationModel.Email, cancellationToken);
            var user = userData.User;
            if (GetHash(user.SecurityStamp, authenticationModel.Password) != user.PasswordHash)
            {
                throw new Exception("not valid model");
            }
            var roles = userData.Roles;
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
