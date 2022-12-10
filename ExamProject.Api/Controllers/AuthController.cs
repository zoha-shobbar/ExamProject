using ExamProject.Application.Dtos.OutputDtos;
using ExamProject.Application.Response;
using ExamProject.Infrastructure.Identity.Models;
using ExamProject.Infrastructure.Services;
using ExamProject.Infrastructure.Services.Jwt;
using Intsoft.Exam.Application.Dtos.Inputs;
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
        private readonly IAuthService service;

        public AuthController(IAuthService service)
        {
            this.service=service;
        }

        [HttpGet("{id}")]
        public async Task<SingleResponse<User>> Get(Guid id, CancellationToken cancellationToken)
        {
            return await service.Get(id, cancellationToken);
        }

        [HttpGet]
        public ListResponse<UserDto> Get()
        {
            return service.Get();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<SingleResponse<Guid>> SignUpAsync(UserInput userInput, CancellationToken cancellationToken)
        {
            return await service.SignUpAsync(userInput, cancellationToken);
        }

        [HttpGet("[action]")]
        [AllowAnonymous]
        public async Task<SingleResponse<AccessToken>> SignIn(string userName, string password, CancellationToken cancellationToken)
        {
            return await service.SignIn(userName, password, cancellationToken);
        }


    }
}
