using ExamProject.Application.Dtos.Inputs;
using ExamProject.Application.Response;
using ExamProject.Domain.Entities;

namespace ExamProject.Application.Contracts.Services
{
    public interface IBookService : IBaseService<Book, BookInput>
    {
        Task<SingleResponse<Book>> ChangeBookAvailability(int id, bool isAvailable, CancellationToken cancellationToken);
    }
}
