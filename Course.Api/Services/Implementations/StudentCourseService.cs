using System.Net;
using AutoMapper;
using CourseApi.Dto;
using CourseApi.Dto.Course;
using CourseApi.Dto.Student;
using CourseApi.Dto.Teacher;
using CourseApi.Repositories.Interfaces;
using CourseApi.Services.Interfaces;

namespace CourseApi.Services.Implementations;

public class StudentCourseService : IStudentCourseService
{
    private readonly IStudentCourseRepository _studentCourseRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<StudentCourseService> _logger;
    private ApiResponse _response;
    
    public StudentCourseService(IStudentCourseRepository studentCourseRepository, IStudentRepository studentRepository
        ,ICourseRepository courseRepository,  IMapper mapper, ILogger<StudentCourseService> logger)
    {
        _studentCourseRepository = studentCourseRepository;
        _studentRepository = studentRepository;
        _courseRepository = courseRepository;
        _mapper = mapper;
        _logger = logger;
        _response = new();
    }

    public async Task<ApiResponse> CoursesByStudent(int studentId)
    {
        try
        {
            if (studentId <= 0)
            {
                _response.ErrorMessage = "StudentId invalid";
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccessful = false;
                return _response;
            }

            if (await _studentRepository.GetById(studentId) == null)
            {
                _response.ErrorMessage = "Student not found";
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccessful = false;
                return _response;
            }

            var studentcourses =
                await _studentCourseRepository.GetAll(v => v.StudentId == studentId, includeProperties: "Course");

            var courses = studentcourses.Select(x => x.Course);

            _response.StatusCode = HttpStatusCode.OK;
            _response.Result = _mapper.Map<List<CourseDto>>(courses);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.IsSuccessful = false;
            _response.ErrorMessage = "Error getting student courses";
        }

        return _response;
    }

    public async Task<ApiResponse> StudentsByCourse(int courseId)
    {
        try
        {
            if (courseId <= 0)
            {
                _response.ErrorMessage = "CourseId invalid";
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccessful = false;
                return _response;
            }

            if (await _courseRepository.GetById(courseId) == null)
            {
                _response.ErrorMessage = "Course not found";
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccessful = false;
                return _response;
            }

            var studentcourses =
                await _studentCourseRepository.GetAll(v => v.CourseId == courseId, includeProperties: "Student");

            var students = studentcourses.Select(x => x.Student);

            _response.StatusCode = HttpStatusCode.OK;
            _response.Result = _mapper.Map<List<StudentDto>>(students);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.IsSuccessful = false;
            _response.ErrorMessage = "Error getting student courses";
        }

        return _response;
    }
}