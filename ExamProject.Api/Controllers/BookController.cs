using ExamProject.Application.Contracts.Services;
using ExamProject.Application.Dtos.Inputs;
using ExamProject.Application.Response;
using ExamProject.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExamProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : BaseController<IBookService, Book, BookInput>
    {
        private readonly IBookService service;

        public BookController(IBookService service) : base(service)
        {
            this.service=service;
        }

        [HttpPut("[action]/{id}")]
        public async Task<ActionResult<SingleResponse<Book>>> ChangeBookAvailability(int id, bool isAvailable, CancellationToken cancellationToken)
        {
            return await service.ChangeBookAvailability(id, isAvailable, cancellationToken);
        }
    }
}
