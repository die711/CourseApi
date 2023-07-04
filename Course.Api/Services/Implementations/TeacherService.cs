using System.Net;
using AutoMapper;
using CourseApi.Dto;
using CourseApi.Dto.Teacher;
using CourseApi.Repositories.Interfaces;
using CourseApi.Services.Interfaces;

namespace CourseApi.Services.Implementations;

public class TeacherService: ITeacherService
{
    private readonly ITeacherRepository _teacherRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<TeacherService> _logger;
    protected ApiResponse _response;
    
    public TeacherService(ITeacherRepository teacherRepository, IMapper mapper, ILogger<TeacherService> logger )
    {
        _teacherRepository = teacherRepository;
        _mapper = mapper;
        _logger = logger;
        _response = new();
    }

    public async Task<ApiResponse> ListAsync()
    {
        try
        {
            var teachers = await _teacherRepository.GetAll(tracked: false);
            _response.StatusCode = HttpStatusCode.OK;
            _response.Result = _mapper.Map<List<TeacherDto>>(teachers);
            
        }
        catch (Exception ex)
        {
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.IsSucefull = false;
            _response.ErrorMessage = "Error in all teacher";
            _response.ErrorMessage = "Error in all teachers";
        }
        
        return _response;
    }
}