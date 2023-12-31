using CourseApi.Dto;
using CourseApi.Dto.Student;

namespace CourseApi.Services.Interfaces;

public interface IStudentService
{
    Task<ApiResponse> ListAsync();
    Task<ApiResponse> FindByAsync(int id);
    Task<ApiResponse> CreateAsync(StudentCreateDto model);
    Task<ApiResponse> UpdateAsync(StudentUpdateDto model,int Id);
    Task<ApiResponse> RemoveAsync(int id);

    Task<ApiResponse> Enroll(int studentId, int courseId);

}