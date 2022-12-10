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
        private readonly RoleManager<Role> roleManager;

        public UserDataInitializer(UserManager<User> userManager,
                                    IConfiguration configuration,
                                    RoleManager<Role> roleManager)
        {
            this.userManager = userManager;
            this.configuration=configuration;
            this.roleManager=roleManager;
        }

        public async Task InitializeDataAsync()
        {
            string[] roleNames = { "Admin", "Member" };
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    var roleResult = await roleManager.CreateAsync(new Role { Name= roleName });
                }
            }

            if (!userManager.Users.AsNoTracking().Any(p => p.UserName ==  configuration["Admin:UserName"]))
            {
                var user = new User
                {
                    FirstName = configuration["Admin:FirstName"],
                    LastName = configuration["Admin:LastName"],
                    PhoneNumber = configuration["Admin:PhoneNumber"],
                    UserName = configuration["Admin:UserName"],
                    Email = configuration["Admin:Email"]
                };
                var result = userManager.CreateAsync(user, configuration["Admin:Password"])
                                .GetAwaiter()
                                .GetResult();

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }
    }
}