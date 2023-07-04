using CourseApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseApi.DataAccess;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Course> Course { get; set; }
    public DbSet<Student> Students { get; set; }
}