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
    private readonly ITeacherRepository _teacherRepository;
    private readonly ILogger<CourseService> _logger;
    private readonly ApiResponse _response;

    public CourseService(ICourseRepository courseRepository, IMapper mapper, 
                         ITeacherRepository teacherRepository,   ILogger<CourseService> logger)
    {
        _courseRepository = courseRepository;
        _mapper = mapper;
        _teacherRepository = teacherRepository;
        _logger = logger;
        _response = new();
    }

    public async Task<ApiResponse> ListAsync()
    {
        try
        {
            var courses = await _courseRepository.GetAll(tracked: false, includeProperties: "Teacher");
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

    public async Task<ApiResponse> ListByTeacherIdAsync(int teacherId)
    {
        try
        {
            if (await _teacherRepository.GetById(teacherId) == null)
            {
                _response.ErrorMessage = "Doesn't exist a teacher with that id";
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSucefull = false;
                return _response;
            }

            var courses = await _courseRepository.GetAll(v => v.TeacherId == teacherId, includeProperties: "Teacher");
            _response.StatusCode = HttpStatusCode.OK;
            _response.Result = _mapper.Map<List<CourseDto>>(courses);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.IsSucefull = false;
            _response.ErrorMessage = "Error obtaining the courses";
        }

        return _response;
    }

    public async Task<ApiResponse> FindByIdAsync(int id)
    {
        try
        {
            if (id <= 0)
            {
                _response.ErrorMessage = "Id invalid";
                _response.IsSucefull = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                return _response;
            }

            var course = await _courseRepository.GetById(id);

            if (course == null)
            {
                _response.ErrorMessage = $"Course with id {id} not found";
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSucefull = false;
                return _response;
            }

            _response.StatusCode = HttpStatusCode.OK;
            _response.Result = _mapper.Map<CourseDto>(course);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.IsSucefull = false;
            _response.ErrorMessage = $"Error in get course with id: {id}";
        }

        return _response;
    }

    public async Task<ApiResponse> CreateAsync(CourseCreateDto model)
    {
        try
        {
            var entity = _mapper.Map<Course>(model);
            await _courseRepository.Create(entity);
            _response.StatusCode = HttpStatusCode.Created;
            _response.Result = entity.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.IsSucefull = false;
            _response.ErrorMessage = "Error creating the course";
        }

        return _response;
    }

    public async Task<ApiResponse> UpdateAsync(CourseUpdateDto model, int id)
    {
        try
        {
            if (id != model.Id)
            {
                _response.IsSucefull = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessage = "Id invalid";
                return _response;
            }

            var course = await _courseRepository.GetById(id);

            if (course == null)
            {
                _response.ErrorMessage = $"course with id {id} not found";
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSucefull = false;
                return _response;
            }

            course.TeacherId = model.TeacherId;
            course.Name = model.Name;
            course.Description = model.Description;
            course.Price = model.Price;
            course.Duration = model.Duration;
            course.Ranking = model.Ranking;
            course.LastUpdate = model.LastUpdate;

            await _courseRepository.Update();
            _response.StatusCode = HttpStatusCode.NoContent;

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.IsSucefull = false;
            _response.ErrorMessage = "Error updating the course";
        }

        return _response;
    }

    public async Task<ApiResponse> RemoveAsync(int id)
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

            var course = await _courseRepository.GetById(id);
            if (course == null)
            {
                _response.ErrorMessage = $"course with id {id} not found";
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSucefull = false;
                return _response;
            }

            await _courseRepository.Remove(course);
            _response.StatusCode = HttpStatusCode.NoContent;

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.IsSucefull = false;
            _response.ErrorMessage = "Error deletiing the course";
        }

        return _response;
    }

    public async Task<ApiResponse> TopRankingAsync(int quantity)
    {
        try
        {
            if (quantity <= 0)
            {
                _response.ErrorMessage = "number need be mayor a zero";
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSucefull = false;
                return _response;
            }

            var courses =await _courseRepository.TopRanking(quantity);
            _response.StatusCode = HttpStatusCode.OK;
            _response.Result = _mapper.Map<List<CourseDto>>(courses);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.IsSucefull = false;
            _response.ErrorMessage = "Error obtaining the courses";
        }
        
        return _response;
    }
}