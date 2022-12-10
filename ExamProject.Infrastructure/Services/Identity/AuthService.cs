using ExamProject.Application.Dtos.OutputDtos;
using ExamProject.Application.Response;
using ExamProject.Infrastructure.Identity.Models;
using ExamProject.Infrastructure.Services.Jwt;
using Intsoft.Exam.Application.Dtos.Inputs;
using Microsoft.AspNetCore.Identity;

namespace ExamProject.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly SignInManager<User> signInManager;
        private readonly IJwtService jwtService;

        public AuthService(UserManager<User> userManager,
                                RoleManager<Role> roleManager,
                                SignInManager<User> signInManager,
                                IJwtService jwtService)
        {
            this.userManager=userManager;
            this.roleManager=roleManager;
            this.signInManager=signInManager;
            this.jwtService=jwtService;
        }

        public async Task<SingleResponse<User>> Get(Guid id, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            return user;
        }

        public ListResponse<UserDto> Get()
        {
            var users = userManager.Users
                            .ToList()
                            .Select(x => new UserDto
                            {
                                Id = x.Id,
                                FullName = x.FullName,
                                PhoneNumber = x.PhoneNumber,
                                Role = string.Join(",", userManager.GetRolesAsync(x).Result.ToArray())
                            })
                            .ToList();
            return users;
        }

        public async Task<SingleResponse<Guid>> SignUpAsync(UserInput userInput, CancellationToken cancellationToken)
        {
            var user = new User
            {
                FirstName = userInput.FirstName,
                LastName = userInput.LastName,
                PhoneNumber = userInput.PhoneNumber,
                UserName = userInput.UserName,
                Email = userInput.Email
            };
            var result = userManager.CreateAsync(user, userInput.Password)
                            .GetAwaiter()
                            .GetResult();

            await userManager.AddToRoleAsync(user, "Member");

            return user.Id;
        }

        public async Task<SingleResponse<AccessToken>> SignIn(string userName, string password, CancellationToken cancellationToken)
        {
            var result = await signInManager.PasswordSignInAsync(userName, password, false, false);

            if (!result.Succeeded)
                throw new UnauthorizedAccessException();

            var user = await userManager.FindByNameAsync(userName);
            var token = await jwtService.GenerateToken(user);

            return token;
        }

    }
}
