using ExamProject.Infrastructure.Identity.Models;

namespace ExamProject.Infrastructure.Services.Jwt
{
    public interface IJwtService
    {
        Task<AccessToken> GenerateToken(User user);
    }
}
