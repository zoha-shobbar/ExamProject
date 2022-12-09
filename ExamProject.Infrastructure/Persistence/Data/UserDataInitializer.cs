using ExamProject.Application.Contracts;
using ExamProject.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ExamProject.Infrastructure.Persistence.Data
{
    public class UserDataInitializer : IDataInitializer
    {
        private readonly UserManager<User> userManager;
        private readonly IConfiguration configuration;

        public UserDataInitializer(UserManager<User> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration=configuration;
        }

        public void InitializeData()
        {
            if (!userManager.Users.AsNoTracking().Any(p => p.UserName == "Admin"))
            {
                var user = new User
                {
                    FirstName = configuration["Admin:FirstName"],
                    LastName = configuration["Admin:LastName"],
                    PhoneNumber = configuration["Admin:PhoneNumber"],
                    UserName = configuration["Admin:UserName"],
                    Email = configuration["Admin:Email"]
                };
                var result = userManager.CreateAsync(user, configuration["Admin:Password"]).GetAwaiter().GetResult();
            }
        }
    }
}