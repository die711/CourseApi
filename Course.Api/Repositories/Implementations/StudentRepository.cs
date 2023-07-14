using CourseApi.DataAccess;
using CourseApi.Entities;
using CourseApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Query;

namespace CourseApi.Repositories.Implementations;

public class StudentRepository : Repository<Student>, IStudentRepository
{
    public StudentRepository(ApplicationDbContext db) : base(db)
    {
    }

    public async Task Enroll(int studentId, int courseId)
    {
        var studentCourse = new StudentCourse()
        {
            StudentId = studentId,
            CourseId = courseId
        };

        await _db.StudentCourses.AddAsync(studentCourse);
        await Save();

    }
}