using CourseApi.Dto;
using CourseApi.Repositories.Interfaces;
using CourseApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CourseApi.Controllers.v1;

[Route("api/[controller]")]
[ApiController]
public class TeacherController : ControllerBase
{
    private readonly ITeacherService _teacherService;
   

    public TeacherController(ITeacherService teacherService)
    {
        _teacherService = teacherService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse>> GetAll()
    {
        var response = await _teacherService.ListAsync();
        return StatusCode((int)response.StatusCode, response);
    }

}