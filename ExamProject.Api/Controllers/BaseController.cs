using ExamProject.Application.Contracts.Services;
using ExamProject.Application.Response;
using ExamProject.Domain.Entities.Common;
using Microsoft.AspNetCore.Mvc;

namespace ExamProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<TService, TEntity, TInput> : ControllerBase
        where TEntity : BaseEntity
        where TInput : class
        where TService : IBaseService<TEntity, TInput>
    {
        private readonly TService service;

        public BaseController(TService service)
        {
            this.service=service;
        }

        [HttpPost]
        public virtual async Task<ActionResult<SingleResponse<TEntity>>> Create(TInput input, CancellationToken cancellationToken)
        {
            return await service.Create(input, cancellationToken);
        }

        [HttpPut("{id}")]
        public virtual async Task<ActionResult<SingleResponse<TEntity>>> Update(int id, TInput input, CancellationToken cancellationToken)
        {
            return await service.Update(id, input, cancellationToken);
        }

        [HttpPut("[action]/{id}")]
        public virtual async Task<ActionResult<SingleResponse<bool>>> Archive(int id, CancellationToken cancellationToken)
        {
            return await service.Archive(id, cancellationToken);
        }

        [HttpDelete("{id}")]
        public virtual async Task<ActionResult<SingleResponse<bool>>> Delete(int id, CancellationToken cancellationToken)
        {
            return await service.Delete(id, cancellationToken);
        }

        [HttpGet]
        public virtual ActionResult<ListResponse<TEntity>> Get()
        {
            return service.Get();
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<SingleResponse<TEntity>>> Get(int id, CancellationToken cancellationToken)
        {
            return await service.Get(id, cancellationToken);
        }
    }
}
