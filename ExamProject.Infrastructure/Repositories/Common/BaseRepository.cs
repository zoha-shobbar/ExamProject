using ExamProject.Domain.Entities.Common;
using ExamProject.Domain.Repositories;
using ExamProject.Infrastructure.Persistence;

namespace ExamProject.Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : BaseEntity
    {
        private readonly DataContext dataContext;

        public BaseRepository(DataContext dataContext)
        {
            this.dataContext=dataContext;
        }
        public virtual async Task<TEntity> Create(TEntity entity, CancellationToken cancellationToken)
        {
            await dataContext.AddAsync(entity);
            await dataContext.SaveChangesAsync(cancellationToken);

            return entity;
        }

        public virtual async Task<TEntity> Update(int id, TEntity entity, CancellationToken cancellationToken)
        {
            var currentEntity = await dataContext.Set<TEntity>().FindAsync(id);
            if (currentEntity != null)
            {
                currentEntity = entity;
                await dataContext.SaveChangesAsync();
            }
            return currentEntity;
        }

        public virtual async Task<bool> Archive(int id, CancellationToken cancellationToken)
        {
            var currentEntity = await dataContext.Set<TEntity>().FindAsync(id);
            if (currentEntity != null)
            {
                currentEntity.IsArchive= true;
                await dataContext.SaveChangesAsync();
            }
            return currentEntity.IsArchive;
        }

        public virtual async Task Delete(int id, CancellationToken cancellationToken)
        {
            var currentEntity = await dataContext.Set<TEntity>().FindAsync(id);
            if (currentEntity != null)
            {
                dataContext.Set<TEntity>().Remove(currentEntity);
                await dataContext.SaveChangesAsync();

                var cursrentEntity = await dataContext.Set<TEntity>().FindAsync(id);
            }
        }

        public virtual IQueryable<TEntity> Get()
        {
            return dataContext.Set<TEntity>();
        }

        public virtual async Task<TEntity> Get(int id)
        {
            return await dataContext.Set<TEntity>().FindAsync(id);
        }
    }
}
