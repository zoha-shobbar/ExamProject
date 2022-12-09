using System.IdentityModel.Tokens.Jwt;

namespace ExamProject.Infrastructure.Identity.Models
{
    public class AccessToken
    {
        public string Token { get; set; }
        public int ExpiresIn { get; set; }
    }
}
