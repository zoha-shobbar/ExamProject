using ExamProject.Application.Dtos.OutputDtos;
using ExamProject.Application.Response;
using ExamProject.Infrastructure.Identity.Models;
using Intsoft.Exam.Application.Dtos.Inputs;

namespace ExamProject.Infrastructure.Services
{
    public interface IAuthService
    {
        Task<SingleResponse<User>> Get(Guid id, CancellationToken cancellationToken);
        ListResponse<UserDto> Get();
        Task<SingleResponse<AccessToken>> SignIn(string userName, string password, CancellationToken cancellationToken);
        Task<SingleResponse<Guid>> SignUpAsync(UserInput userInput, CancellationToken cancellationToken);
    }
}