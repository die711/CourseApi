using System.Net;
using AutoMapper;
using CourseApi.Dto;
using CourseApi.Dto.Student;
using CourseApi.Entities;
using CourseApi.Repositories.Interfaces;
using CourseApi.Services.Interfaces;

namespace CourseApi.Services.Implementations;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<StudentService> _logger;
    protected ApiResponse _response;

    public StudentService(IStudentRepository studentRepository, ICourseRepository courseRepository, 
        IMapper mapper, ILogger<StudentService> logger)
    {
        _studentRepository = studentRepository;
        _courseRepository = courseRepository;
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
           _response.IsSuccessful = false;
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
                _response.IsSuccessful = false;
                return _response;
            }

            var student = await _studentRepository.GetById(id);

            if (student == null)
            {
                _response.ErrorMessage = "Student not found";
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccessful = false;
                return _response;
            }

            _response.StatusCode = HttpStatusCode.OK;
            _response.Result = _mapper.Map<StudentDto>(student);
            
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.IsSuccessful = false;
            _response.ErrorMessage = "Error obtaining the student";
        }

        return _response;
    }

    public async Task<ApiResponse> CreateAsync(StudentCreateDto model)
    {
        try
        {
            var entity = _mapper.Map<Student>(model);
            await _studentRepository.Create(entity);
            _response.StatusCode = HttpStatusCode.Created;
            _response.Result = entity.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.IsSuccessful = false;
            _response.ErrorMessage = "Error creating the student";
            
        }
        return _response;
    }

    public async Task<ApiResponse> UpdateAsync(StudentUpdateDto model, int id)
    {
        try
        {
            if (id <= 0 || model.Id <= 0)
            {
                _response.ErrorMessage = "Id invalid";
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccessful = false;
                return _response;
            }
        
            if (id != model.Id)
            {
                _response.IsSuccessful = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessage = "Different Id";
                return _response;
            }

            var student = await _studentRepository.GetById(id);

            if (student == null)
            {
                _response.ErrorMessage = "Student not found";
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccessful = false;
                return _response;
            }

            student.Name = model.Name;
            student.LastName = model.LastName;
            student.Email = model.Email;
            student.UpdateDate = DateTime.Now;
            student.PhoneNumber = model.PhoneNumber;

            await _studentRepository.Update();
            _response.StatusCode = HttpStatusCode.NoContent;
        }
        catch (Exception ex)
        {
          _logger.LogError(ex, ex.Message);
          _response.StatusCode = HttpStatusCode.InternalServerError;
          _response.IsSuccessful = false;
          _response.ErrorMessage = "Error updating a student";
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
                _response.IsSuccessful = false;
                return _response;
            }

            var student = await _studentRepository.GetById(id);

            if (student == null)
            {
                _response.ErrorMessage = "Student not found";
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccessful = false;
                return _response;
            }

            await _studentRepository.Remove(student);
            _response.StatusCode = HttpStatusCode.NoContent;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,ex.Message);
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.IsSuccessful = false;
            _response.ErrorMessage = "Error deleting a student";
        }

        return _response;
    }

    public async Task<ApiResponse> Enroll(int studentId, int courseId)
    {
        try
        {
            var student = await _studentRepository.GetById(studentId);

            if (student == null)
            {
                _response.ErrorMessage = "Student not found";
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccessful = false;
                return _response;
            }

            var course =await _courseRepository.GetById(courseId);
            if (course == null)
            {
                _response.ErrorMessage = "Course not found";
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccessful = false;
                return _response;
            }

            await _studentRepository.Enroll(studentId, courseId);
            
            _response.StatusCode = HttpStatusCode.OK;
            _response.Result = "Student enroll successful";

        }
        catch (Exception ex)
        {
           _logger.LogError(ex, ex.Message);
           _response.StatusCode = HttpStatusCode.InternalServerError;
           _response.IsSuccessful = false;
           _response.ErrorMessage = "Error enrolling student";
        }

        return _response;
    }
}