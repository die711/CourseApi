using CourseApi.DataAccess;
using CourseApi.Entities;
using CourseApi.Repositories.Interfaces;

namespace CourseApi.Repositories.Implementations;

public class StudentRepository : Repository<Student>, IStudentRepository
{
    public StudentRepository(ApplicationDbContext db) : base(db)
    {
    }

    public Task Matricular(int studentId, int courseId)
    {
        throw new NotImplementedException();
    }
}