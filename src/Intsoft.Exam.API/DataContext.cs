using Intsoft.Exam.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Intsoft.Exam.API
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
    }
}
