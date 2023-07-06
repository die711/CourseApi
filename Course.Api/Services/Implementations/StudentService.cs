using System.Net;
using AutoMapper;
using CourseApi.Dto;
using CourseApi.Dto.Student;
using CourseApi.Repositories.Interfaces;
using CourseApi.Services.Interfaces;

namespace CourseApi.Services.Implementations;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<StudentService> _logger;
    protected ApiResponse _response;

    public StudentService(IStudentRepository studentRepository, IMapper mapper, ILogger<StudentService> logger)
    {
        _studentRepository = studentRepository;
        _mapper = mapper;
        _logger = logger;
        _response = new();
    }
    
    
    
    public async Task<ApiResponse> ListAsync()
    {
        try
        {
            var students =await _studentRepository.GetAll();

            _response.StatusCode = HttpStatusCode.OK;
            _response.Result = _mapper.Map<List<StudentDto>>(students);
        }
        catch (Exception ex)
        {
           _logger.LogError(ex, ex.Message);
           _response.StatusCode = HttpStatusCode.InternalServerError;
           _response.IsSucefull = false;
           _response.ErrorMessage = "Error getting students";
        }

        return _response;
    }

    public async Task<ApiResponse> FindByAsync(int id)
    {
        try
        {
            if (id <= 0)
            {
                _response.ErrorMessage = "Id invalid";
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSucefull = false;
                return _response;
            }
            
            
            
            
        }
        catch (Exception ex)
        {
        }

        return _response;
    }

    public Task<ApiResponse> CreateAsync(StudentCreateDto model)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse> UpdateAsync(StudentUpdateDto model)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse> RemoveAsync(int id)
    {
        throw new NotImplementedException();
    }
}