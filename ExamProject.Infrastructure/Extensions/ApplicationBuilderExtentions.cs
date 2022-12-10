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
        public static async void IntializeDatabase(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<DataContext>(); //Service locator

                //Applies any pending migrations for the context to the database like (Update-Database)
                dbContext.Database.Migrate();

                var dataInitializer = scope.ServiceProvider.GetService<IDataInitializer>();

                await dataInitializer.InitializeDataAsync();
            }
        }
    }
}
