using ExamProject.Application.Contracts.Services;
using ExamProject.Application.Response;
using ExamProject.Domain.Entities.Common;
using ExamProject.Domain.Repositories;
using Mapster;

namespace ExamProject.Infrastructure.Services
{
    public class BaseService<TEntity, TInput> : IBaseService<TEntity, TInput>
        where TEntity : BaseEntity
        where TInput : class
    {
        private readonly IBaseRepository<TEntity> repository;

        public BaseService(IBaseRepository<TEntity> repository)
        {
            this.repository=repository;
        }

        public virtual async Task<SingleResponse<TEntity>> Create(TInput input, CancellationToken cancellationToken)
        {
            var entity = input.Adapt<TEntity>();

            await repository.Create(entity, cancellationToken);

            return entity;
        }

        public virtual async Task<SingleResponse<TEntity>> Update(int id, TInput input, CancellationToken cancellationToken)
        {
            var entity = await repository.Get(id);
            if (entity == null)
                return ResponseStatus.NotFound;

            input.Adapt(entity);

            await repository.Update(id, entity, cancellationToken);

            return entity;
        }

        public virtual async Task<SingleResponse<bool>> Delete(int id, CancellationToken cancellationToken)
        {
            var entity = await repository.Get(id);
            if (entity == null)
                return ResponseStatus.NotFound;

            await repository.Delete(id, cancellationToken);

            return true;
        }

        public virtual async Task<SingleResponse<bool>> Archive(int id, CancellationToken cancellationToken)
        {
            var entity = await repository.Get(id);
            if (entity == null)
                return ResponseStatus.NotFound;

            var result = await repository.Archive(id, cancellationToken);
            return result;
        }

        public virtual async Task<SingleResponse<TEntity>> Get(int id, CancellationToken cancellationToken)
        {
            var entity = await repository.Get(id);
            if (entity == null)
                return ResponseStatus.NotFound;

            return entity;
        }

        public virtual ListResponse<TEntity> Get()
        {
            var entities = repository.Get();
            if (entities == null)
                return ResponseStatus.NotFound;

            return entities.ToList();
        }
    }
}
