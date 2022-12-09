using ExamProject.Domain.Entities.Common;
using ExamProject.Infrastructure.Extensions;
using ExamProject.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExamProject.Infrastructure.Persistence
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var entitiesAssembly = typeof(IBaseEntity).Assembly;

            modelBuilder.RegisterAllEntities<IBaseEntity>(entitiesAssembly);
            modelBuilder.AddRestrictDeleteBehaviorConvention();
            modelBuilder.AddGolobalFilter(entitiesAssembly);
            //modelBuilder.AddPluralizingTableNameConvention();
        }
    }
}
