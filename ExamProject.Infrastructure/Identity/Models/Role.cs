using ExamProject.Domain.Entities.Common;
using Microsoft.AspNetCore.Identity;

namespace ExamProject.Infrastructure.Identity.Models
{
    public class Role : IdentityRole<Guid>, IBaseEntity
    {
        public string? Description { get; set; }
        public bool IsArchive { get; set; } = false;
    }
}
