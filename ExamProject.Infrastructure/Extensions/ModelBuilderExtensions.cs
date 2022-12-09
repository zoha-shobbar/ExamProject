using ExamProject.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using System.Reflection;

namespace ExamProject.Infrastructure.Extensions
{
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// Set DeleteBehavior.Restrict by default for relations
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void AddRestrictDeleteBehaviorConvention(this ModelBuilder modelBuilder)
        {
            IEnumerable<IMutableForeignKey> cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (IMutableForeignKey fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;
        }

        /// <summary>
        /// Dynamicaly register all Entities that inherit from specific BaseType
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <param name="baseType">Base type that Entities inherit from this</param>
        /// <param name="assemblies">Assemblies contains Entities</param>
        public static void RegisterAllEntities<BaseType>(this ModelBuilder modelBuilder, params Assembly[] assemblies)
        {
            IEnumerable<Type> types = assemblies.SelectMany(a => a.GetExportedTypes())
                .Where(c => c.IsClass && !c.IsAbstract && c.IsPublic && typeof(BaseType).IsAssignableFrom(c));

            foreach (Type type in types)
                modelBuilder.Entity(type);
        }

        /// <summary>
        /// Ignoring entities that IsArchive = true
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <param name="entitiesAssembly"></param>
        public static void AddGolobalFilter(this ModelBuilder modelBuilder, Assembly entitiesAssembly)
        {
            Expression<Func<IBaseEntity, bool>> filterExpr = bm => !bm.IsArchive;
            foreach (var mutableEntityType in modelBuilder.Model.GetEntityTypes())
            {
                if (mutableEntityType.ClrType.IsAssignableTo(typeof(IBaseEntity)))
                {
                    var parameter = Expression.Parameter(mutableEntityType.ClrType);
                    var body = ReplacingExpressionVisitor.Replace(filterExpr.Parameters.First(), parameter, filterExpr.Body);
                    var lambdaExpression = Expression.Lambda(body, parameter);

                    // set filter
                    mutableEntityType.SetQueryFilter(lambdaExpression);
                }
            }
        }
    }
}
