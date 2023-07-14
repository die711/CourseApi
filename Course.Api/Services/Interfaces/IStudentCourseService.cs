using CourseApi.Dto;

namespace CourseApi.Services.Interfaces;

public interface IStudentCourseService
{

    Task<ApiResponse> CoursesByStudent(int studentId);
    Task<ApiResponse> StudentsByCourse(int courseId);

}