using ExamProject.Infrastructure.Identity.Models;
using ExamProject.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ExamProject.Infrastructure.Extensions
{
    public static class IdentityConfiguration
    {
        public static IServiceCollection AddCustomIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequireDigit= true;
                options.Password.RequireLowercase= false;
                options.Password.RequireUppercase= false;
                options.Password.RequireNonAlphanumeric= false;
                options.Password.RequiredLength= 4;
                options.User.RequireUniqueEmail= true;
            })
                .AddRoles<Role>()
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders(); ;

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey
                            (Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                };
            });

            services.AddAuthorization();

            return services;
        }
    }
}
