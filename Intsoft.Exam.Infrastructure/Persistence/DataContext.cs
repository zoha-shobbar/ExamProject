using Intsoft.Exam.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Intsoft.Exam.Infrastructure.Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
    }
}
