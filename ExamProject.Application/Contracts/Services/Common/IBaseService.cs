using ExamProject.Application.Response;
using ExamProject.Domain.Entities.Common;

namespace ExamProject.Application.Contracts.Services
{
    public interface IBaseService<TEntity, TInput>
        where TEntity : BaseEntity
        where TInput : class
    {
        Task<SingleResponse<TEntity>> Create(TInput input, CancellationToken cancellationToken);
        Task<SingleResponse<TEntity>> Update(int id, TInput input, CancellationToken cancellationToken);
        Task<SingleResponse<bool>> Archive(int id, CancellationToken cancellationToken);
        Task<SingleResponse<bool>> Delete(int id, CancellationToken cancellationToken);
        ListResponse<TEntity> Get();
        Task<SingleResponse<TEntity>> Get(int id, CancellationToken cancellationToken);
    }
}