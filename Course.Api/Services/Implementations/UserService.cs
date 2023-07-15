using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using CourseApi.Dto;
using CourseApi.Dto.User;
using CourseApi.Entities;
using CourseApi.Repositories.Implementations;
using CourseApi.Repositories.Interfaces;
using CourseApi.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace CourseApi.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UserService> _logger;
    private ApiResponse _response;
    private string? secretKey;

    public UserService(IUserRepository userRepository, IMapper mapper, ILogger<UserService> logger,IConfiguration configuration)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _logger = logger;
        _response = new();
        secretKey = configuration.GetValue<string>("ApiSettings:Secret");
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
                    Token = "",
                    User = null
                };

                return _response;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var loginResponseDto = new LoginResponseDto()
            {
                Token = tokenHandler.WriteToken(token),
                User = _mapper.Map<UserDto>(user)
            };

            _response.StatusCode = HttpStatusCode.OK;
            _response.Result = loginResponseDto;

            return _response;

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