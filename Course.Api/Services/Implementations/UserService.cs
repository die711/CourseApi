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
            if (!await _userRepository.isUniqueUser(model.UserName))
            {
                _response.IsSuccessful = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessage = "Username exists yet";
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

    public async Task<ApiResponse> Login(LoginRequestDto model)
    {
        try
        {
            var user = await _userRepository.Get(u =>
                u.UserName.ToLower() == model.UserName.ToLower() && u.Password == model.Password);

            if (user == null)
            {

                _response.ErrorMessage = "Username o password incorrect";
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccessful = false;

                _response.Result = new LoginResponseDto()
                {
                };

            }
            
            
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.IsSuccessful = false;
            _response.ErrorMessage = "Error int the login";
        }

        return _response;
    }
}