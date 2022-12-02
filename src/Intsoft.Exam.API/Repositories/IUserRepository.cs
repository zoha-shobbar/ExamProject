using Intsoft.Exam.API.Models.Entities;

namespace Intsoft.Exam.API.Repositories
{
    public interface IUserRepository
    {
        User CreateUser(User user);
        List<User> Get();
        User UpdateUser(int id, User user);
    }
}