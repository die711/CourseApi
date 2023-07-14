using CourseApi.Dto;

namespace CourseApi.Services.Interfaces;

public interface IStudentCourseService
{

    Task<ApiResponse> CoursesForStudent(int studentId);
    Task<ApiResponse> StudentForCourse(int courseId);

}