using Microsoft.EntityFrameworkCore;
using WebApplication4.Model;

namespace WebApplication4
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
         public DbSet<instrctor> instrctors { get; set; }
        public DbSet<Subject> subjects { get; set; }
        public DbSet<Student> students { get; set; }    
    }
}
