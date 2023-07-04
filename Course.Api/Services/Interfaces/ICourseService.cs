using CourseApi.Dto;
using CourseApi.Dto.Course;
using CourseApi.Dto.Specifications;

namespace CourseApi.Services.Interfaces;

public interface ICourseService
{
    Task<ApiResponse> ListAsync();
    Task<ApiResponse> GetAllPagination(Params parameters);
    Task<ApiResponse> ListByTeacherIdAsync(int teacherId);
    Task<ApiResponse> FindByIdAsync(int id);
    Task<ApiResponse> CreateAsync(CourseCreateDto model);
    Task<ApiResponse> UpdateAsync(CourseUpdateDto model, int id);
    Task<ApiResponse> RemoveAsync(int id);
    Task<ApiResponse> TopRankingAsync(int number);

}