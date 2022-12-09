using ExamProject.Application.Dtos.OutputDtos;
using ExamProject.Infrastructure.Identity.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExamProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly SignInManager<User> signInManager;

        public AuthController(UserManager<User> userManager,
            RoleManager<Role> roleManager,
            SignInManager<User> signInManager)
        {
            this.userManager=userManager;
            this.roleManager=roleManager;
            this.signInManager=signInManager;
        }

        [HttpGet("{id}")]
        public async Task<User> Get(int id, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            return user;
        }

        [HttpGet]
        public List<User> Get()
        {
            //TypeAdapterConfig<User, UserDto>
            //    .NewConfig()
            //        .Map(d => d.FullName, s => s.FirstName + " " + s.LastName);

            var users = userManager.Users
                           // ProjectToType<UserDto>()
                            .ToList(); ;
            return users;
        }

    }
}
