using Intsoft.Exam.Domain.Entities;

namespace Intsoft.Exam.Application.Contracts.Repositories
{
    public interface IUserRepository
    {
        User CreateUser(User user);
        List<User> Get();
        User UpdateUser(int id, User user);
    }
}