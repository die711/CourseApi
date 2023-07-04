using System.Linq.Expressions;
using System.Net;
using AutoMapper;
using CourseApi.Dto;
using CourseApi.Dto.Course;
using CourseApi.Dto.Specifications;
using CourseApi.Entities;
using CourseApi.Repositories.Interfaces;
using CourseApi.Services.Interfaces;

namespace CourseApi.Services.Implementations;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CourseService> _logger;
    private readonly ApiResponse _response;

    public CourseService(ICourseRepository courseRepository, IMapper mapper, ILogger<CourseService> logger)
    {
        _courseRepository = courseRepository;
        _mapper = mapper;
        _logger = logger;
        _response = new();
    }
    
    public async Task<ApiResponse> ListAsync()
    {
        try
        {
            var courses = await _courseRepository.GetAll(tracked: false,includeProperties: "Teacher");
            _response.StatusCode = HttpStatusCode.OK;
            _response.Result = _mapper.Map<List<CourseDto>>(courses);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.IsSucefull = false;
            _response.ErrorMessage = "Error in all courses";
        }

        return _response;
    }

    public Task<ApiResponse> GetAllPagination(Params parameters)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse> ListByTeacherIdAsync(int teacherId)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse> FindByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse> CreateAsync(CourseCreateDto model)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse> UpdateAsync(CourseUpdateDto model, int id)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse> RemoveAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse> TopRankingAsync(int number)
    {
        throw new NotImplementedException();
    }
}