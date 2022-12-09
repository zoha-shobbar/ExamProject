using ExamProject.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ExamProject.Infrastructure.Services.Jwt
{
    public class JwtService : IJwtService
    {
        private readonly SignInManager<User> signInManager;
        private readonly IConfiguration configuration;

        public JwtService(SignInManager<User> signInManager, IConfiguration configuration)
        {
            this.signInManager=signInManager;
            this.configuration=configuration;
        }
        public async Task<AccessToken> GenerateToken(User user)
        {
            var secretKey = Encoding.UTF8.GetBytes("MySecretKey");
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

            var claims = (await signInManager.ClaimsFactory.CreateAsync(user)).Claims;

            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = configuration["Jwt:Issuer"],
                Audience = configuration["Jwt:Audience"],
                IssuedAt = DateTime.Now,
                Expires = DateTime.Now.AddHours(2),
                SigningCredentials = signingCredentials,
                Subject = new ClaimsIdentity(claims)
            };

            var securityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = securityTokenHandler.CreateToken(descriptor);
            var token = securityTokenHandler.WriteToken(securityToken);

            return new AccessToken
            {
                Token = token,
                ExpiresIn = (int)TimeSpan.FromHours(2).TotalSeconds
            };
        }
    }
}
