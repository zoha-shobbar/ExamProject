using Intsoft.Exam.Application.Contracts.Services;
using Intsoft.Exam.Application.Dtos.Inputs;
using Intsoft.Exam.Application.Response;
using Intsoft.Exam.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Intsoft.Exam.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService=userService;
        }

        [HttpGet]
        public ActionResult<ListResponse<User>> Get()
        {
            return userService.Get();
        }

        [HttpPost]
        public ActionResult<SingleResponse<User>> CreateUser(UserInput input)
        {
            return userService.CreateUser(input);
        }

        [HttpPut("{id}")]
        public ActionResult<SingleResponse<User>> UpdateUser(int id, UserInput input)
        {
            return userService.UpdateUser(id, input);
        }


    }
}
