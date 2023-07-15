using CourseApi.Dto;
using CourseApi.Dto.User;

namespace CourseApi.Services.Interfaces;

public interface IUserService
{
    Task<ApiResponse> Register(RegisterRequestDto model);

    Task<ApiResponse> Login(LoginRequestDto model);
}