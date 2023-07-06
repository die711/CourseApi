using AutoMapper;
using CourseApi.Dto;
using CourseApi.Dto.Student;
using CourseApi.Services.Interfaces;

namespace CourseApi.Services.Implementations;

public class StudentService : IStudentService
{
    private readonly IStudentService _studentService;
    private readonly IMapper _mapper;
    private readonly ILogger<StudentService> _logger;
    protected ApiResponse _response;

    public StudentService(IStudentService studentService, IMapper mapper, ILogger<StudentService> logger)
    {
        _studentService = studentService;
        _mapper = mapper;
        _logger = logger;
        _response = new();
    }
    
    
    
    public async Task<ApiResponse> ListAsync()
    {
        try
        {

        }
        catch (Exception ex)
        {
           
        }

        return _response;
    }

    public Task<ApiResponse> FindByAsync()
    {
        throw new NotImplementedException();
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