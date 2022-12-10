using ExamProject.Application.Contracts.Services;
using ExamProject.Application.Dtos.Inputs;
using ExamProject.Application.Response;
using ExamProject.Domain.Entities;
using ExamProject.Domain.Repositories;

namespace ExamProject.Infrastructure.Services
{
    public class BookService : BaseService<Book, BookInput>, IBookService
    {
        private readonly IBaseRepository<Book> repository;

        public BookService(IBaseRepository<Book> repository) : base(repository)
        {
            this.repository=repository;
        }

        public async Task<SingleResponse<Book>> ChangeBookAvailability(int id, bool isAvailable, CancellationToken cancellationToken)
        {
            var entity = await repository.Get(id);

            if (entity == null)
                return ResponseStatus.NotFound;

            entity.IsAvailable=isAvailable;

            await repository.Update(id, entity, cancellationToken);

            return entity;
        }
    }
}
