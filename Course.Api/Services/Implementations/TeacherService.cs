using System.Net;
using CourseApi.Dto;
using CourseApi.Repositories.Interfaces;
using CourseApi.Services.Interfaces;

namespace CourseApi.Services.Implementations;

public class TeacherService: ITeacherService
{
    private readonly ITeacherRepository _teacherRepository;
    protected ApiResponse _response;
    
    public TeacherService(ITeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
        _response = new();
    }

    public async Task<ApiResponse> ListAsync()
    {
        try
        {
            var teachers = await _teacherRepository.GetAll();
        }
        catch (Exception ex)
        {
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.IsSucefull = false;
            _response.ErrorMessage = "Error in all teacher";
        }
        
        return _response;
    }
}