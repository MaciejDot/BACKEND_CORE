using System;
using SimpleInjector;
using System.Linq;
using System.Reflection;
using BackendCore.Helpers;
using BackendCore.Data;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BackendCore.Security.Services;
using Microsoft.AspNetCore.Authorization.Policy;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace BackendCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddDbContext<ApplicationDatabaseContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ApplicationDatabase")));
            services.AddScoped<IStringToHtmlHelper, StringToHtmlHelper>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<Random>();
            services.AddMvc(
                    options => {  options.EnableEndpointRouting = false; })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)

                ;

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });
            
            var rsa = RSA.Create(16384);
            UserService.RSAKey = rsa;
            services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new RsaSecurityKey(rsa),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseHsts();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
