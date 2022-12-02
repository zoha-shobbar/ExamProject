using Intsoft.Exam.API.Models.Entities;
using Intsoft.Exam.API.Models.Inputs;
using Intsoft.Exam.API.Repositories;
using Intsoft.Exam.API.Response;

namespace Intsoft.Exam.API.Services
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
