using ExamProject.Application.Dtos.OutputDtos;
using ExamProject.Infrastructure.Identity.Models;
using ExamProject.Infrastructure.Services.Jwt;
using Intsoft.Exam.Application.Dtos.Inputs;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExamProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly SignInManager<User> signInManager;
        private readonly IJwtService jwtService;

        public AuthController(UserManager<User> userManager,
            RoleManager<Role> roleManager,
            SignInManager<User> signInManager,
            IJwtService jwtService)
        {
            this.userManager=userManager;
            this.roleManager=roleManager;
            this.signInManager=signInManager;
            this.jwtService=jwtService;
        }

        [HttpGet("{id}")]
        public async Task<User> Get(Guid id, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            return user;
        }

        [HttpGet]
        public List<UserDto> Get()
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

        [HttpPost]
        [AllowAnonymous]
        public async Task<Guid> SignUpAsync(UserInput userInput)
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

        [HttpGet("[action]")]
        [AllowAnonymous]
        public async Task<AccessToken> SignIn(string userName, string password)
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
