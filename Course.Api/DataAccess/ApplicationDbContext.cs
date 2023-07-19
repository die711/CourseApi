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
    public DbSet<StudentCourse> StudentCourses { get; set; }
    public DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Teacher>().Property(v => v.Name)
            .HasMaxLength(50);

        modelBuilder.Entity<Course>().Property(e => e.Name)
            .HasMaxLength(50);

    }
}