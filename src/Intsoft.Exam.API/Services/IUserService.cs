using Intsoft.Exam.API.Models.Entities;
using Intsoft.Exam.API.Models.Inputs;
using Intsoft.Exam.API.Response;

namespace Intsoft.Exam.API.Services
{
    public interface IUserService
    {
        SingleResponse<User> CreateUser(UserInput userInput);
        ListResponse<User> Get();
        SingleResponse<User> UpdateUser(int id, UserInput userInput);
    }
}