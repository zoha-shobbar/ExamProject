using Intsoft.Exam.Application.Contracts.Repositories;
using Intsoft.Exam.Domain.Entities;
using Intsoft.Exam.Infrastructure.Persistence;

namespace Intsoft.Exam.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext dataContext;

        public UserRepository(DataContext dataContext)
        {
            this.dataContext=dataContext;
        }

        public List<User> Get()
        {
            return dataContext.Users.ToList();
        }

        public User CreateUser(User user)
        {
            dataContext.Add(user);
            dataContext.SaveChanges();

            return user;
        }

        public User UpdateUser(int id, User user)
        {
            var existingUser = dataContext.Users.Find(id);

            existingUser.NationalCode = user.NationalCode;
            existingUser.PhoneNumber=user.PhoneNumber;
            existingUser.FirstName= user.FirstName;
            existingUser.LastName= user.LastName;
            existingUser.ModificationDate= DateTime.Now;

            dataContext.SaveChanges();

            return existingUser;
        }
    }
}
