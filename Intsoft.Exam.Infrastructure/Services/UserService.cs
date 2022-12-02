using Intsoft.Exam.Application.Contracts.Services;
using Intsoft.Exam.Application.Dtos.Inputs;
using Intsoft.Exam.Application.Response;
using Intsoft.Exam.Domain.Entities;
using Intsoft.Exam.Domain.Repositories;

namespace Intsoft.Exam.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository repository;

        public UserService(IUserRepository repository)
        {
            this.repository=repository;
        }

        public ListResponse<User> Get()
        {
            return repository.Get();
        }

        public SingleResponse<User> CreateUser(UserInput userInput)
        {
            User user = new User
            {
                CreationDate = DateTime.Now,
                FirstName = userInput.FirstName,
                LastName= userInput.LastName,
                NationalCode= userInput.NationalCode,
                PhoneNumber= userInput.PhoneNumber
            };

            return repository.CreateUser(user);
        }

        public SingleResponse<User> UpdateUser(int id, UserInput userInput)
        {
            User user = new User
            {
                FirstName = userInput.FirstName,
                LastName= userInput.LastName,
                NationalCode= userInput.NationalCode,
                PhoneNumber= userInput.PhoneNumber
            };

            return repository.UpdateUser(id, user);
        }

    }
}
