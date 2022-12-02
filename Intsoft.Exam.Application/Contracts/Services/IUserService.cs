using Intsoft.Exam.Application.Dtos.Inputs;
using Intsoft.Exam.Application.Response;
using Intsoft.Exam.Domain.Entities;

namespace Intsoft.Exam.Application.Contracts.Services
{
    public interface IUserService
    {
        SingleResponse<User> CreateUser(UserInput userInput);
        ListResponse<User> Get();
        SingleResponse<User> UpdateUser(int id, UserInput userInput);
    }
}