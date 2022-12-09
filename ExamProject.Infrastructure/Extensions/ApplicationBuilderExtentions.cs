using ExamProject.Application.Contracts;
using ExamProject.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ExamProject.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtentions
    {
        /// <summary>
        /// Initializing database
        /// </summary>
        /// <param name="app"></param>
        public static void IntializeDatabase(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<DataContext>(); //Service locator

                //Dos not use Migrations, just Create Database with latest changes
                //dbContext.Database.EnsureCreated();
                //Applies any pending migrations for the context to the database like (Update-Database)
                dbContext.Database.Migrate();

                var dataInitializers = scope.ServiceProvider.GetServices<IDataInitializer>();
         
                foreach (var dataInitializer in dataInitializers)
                    dataInitializer.InitializeData();
            }
        }
    }
}
