using CourseApi.Dto;
using CourseApi.Dto.Student;

namespace CourseApi.Services.Interfaces;

public interface IStudentService
{
    Task<ApiResponse> ListAsync();
    Task<ApiResponse> FindByAsync();
    Task<ApiResponse> CreateAsync(StudentCreateDto model);
    Task<ApiResponse> UpdateAsync(StudentUpdateDto model);
    Task<ApiResponse> RemoveAsync(int id);

}