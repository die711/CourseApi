using System.Net;
using AutoMapper;
using CourseApi.Dto;
using CourseApi.Dto.User;
using CourseApi.Entities;
using CourseApi.Repositories.Implementations;
using CourseApi.Repositories.Interfaces;
using CourseApi.Services.Interfaces;

namespace CourseApi.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UserService> _logger;
    private ApiResponse _response;

    public UserService(IUserRepository userRepository, IMapper mapper, ILogger<UserService> logger)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _logger = logger;
        _response = new();
    }
    
    public async Task<ApiResponse> Register(RegisterRequestDto model)
    {
        try
        {
            if (await _userRepository.isUniqueUser(model.UserName))
            {
                _response.IsSuccessful = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessage = "User is not found";
                return _response;
            }

            var user = _mapper.Map<User>(model);
            await _userRepository.Registrar(user);
            _response.StatusCode = HttpStatusCode.Created;

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.IsSuccessful = false;
            _response.ErrorMessage = "Error registering the user";
        }

        return _response;
    }

    
}