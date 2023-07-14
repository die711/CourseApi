using CourseApi.Entities;

namespace CourseApi.Repositories.Interfaces;

public interface IStudentRepository : IRepository<Student>
{
    Task Enroll(int studentId, int courseId);

}