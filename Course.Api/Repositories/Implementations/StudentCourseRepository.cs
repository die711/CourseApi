using CourseApi.DataAccess;
using CourseApi.Entities;
using CourseApi.Repositories.Interfaces;

namespace CourseApi.Repositories.Implementations;

public class StudentCourseRepository : Repository<StudentCourse>, IStudentCourseRepository
{
    public StudentCourseRepository(ApplicationDbContext db) : base(db)
    {
        
    }
}