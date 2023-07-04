using CourseApi.Dto;
using CourseApi.Dto.Teacher;

namespace CourseApi.Services.Interfaces;

public interface ITeacherService
{
    Task<ApiResponse> ListAsync();
    Task<ApiResponse> FindByIdAsync(int id);
    Task<ApiResponse> CreateAsync(TeacherCreateDto model);
    Task<ApiResponse> UpdateAsync(TeacherUpdateDto model, int id);
    Task<ApiResponse> RemoveAsync(int id);
}