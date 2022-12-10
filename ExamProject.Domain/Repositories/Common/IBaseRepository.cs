using ExamProject.Domain.Entities.Common;

namespace ExamProject.Domain.Repositories
{
    public interface IBaseRepository<TEntity>
        where TEntity : BaseEntity
    {
        Task<TEntity> Create(TEntity entity, CancellationToken cancellationToken);
        Task<TEntity> Update(int id, TEntity entity, CancellationToken cancellationToken);
        Task<bool> Archive(int id, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);

        Task<TEntity> Get(int id);
        IQueryable<TEntity> Get();
    }
}
