using CourseApi.Entities;

namespace CourseApi.Repositories.Interfaces;

public interface IStudentRepository : IRepository<Student>
{
    Task Matricular(int studentId, int courseId);

}