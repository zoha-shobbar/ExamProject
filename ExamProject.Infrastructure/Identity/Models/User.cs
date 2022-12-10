using ExamProject.Domain.Entities.Common;
using Microsoft.AspNetCore.Identity;
using System.Runtime.Serialization;

namespace ExamProject.Infrastructure.Identity.Models
{
    public class User : IdentityUser<Guid>, IBaseEntity
    {
        public string? FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; } = false;

        [IgnoreDataMember]
        public string FullName => $"{FirstName} {LastName}";

        public bool IsArchive { get; set; } = false;
    }
}
