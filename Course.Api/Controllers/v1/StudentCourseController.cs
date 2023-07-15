using CourseApi.Dto;
using CourseApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CourseApi.Controllers.v1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class StudentCourseController : ControllerBase
{
    private readonly IStudentCourseService _studentCourseService;

    public StudentCourseController( IStudentCourseService studentCourseService)
    {
        _studentCourseService = studentCourseService;
    }

    [HttpGet("/course/{studentId:int}")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse>> GetCoursesByStudent(int studentId)
    {
        var response = await _studentCourseService.CoursesByStudent(studentId);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet("/student/{courseId:int}")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse>> GetStudentsByCourse(int courseId)
    {
        var response = await _studentCourseService.StudentsByCourse(courseId);
        return StatusCode((int)response.StatusCode, response);
    }

}