using CourseApi.Dto;

namespace CourseApi.Services.Interfaces;

public interface ITeacherService
{
    Task<ApiResponse> ListAsync();

}