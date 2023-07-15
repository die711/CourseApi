using System.Net;
using CourseApi.Dto;
using CourseApi.Dto.Course;
using CourseApi.Dto.Student;
using CourseApi.Dto.Teacher;
using CourseApi.Repositories.Interfaces;
using CourseApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CourseApi.Controllers.v1;

[Route("api/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly IStudentService _studentService;
    
    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet]
    [Authorize(Roles = "supervisor")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse>> GetAll()
    {
        var response = await _studentService.ListAsync();
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse>> FindById(int id)
    {
        var response = await _studentService.FindByAsync(id);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<ApiResponse>> Create( [FromBody] StudentCreateDto model )
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = await _studentService.CreateAsync(model);

        if (response.StatusCode == HttpStatusCode.Created)
            return CreatedAtAction(nameof(FindById), new { id = response.Result }, response);

        return StatusCode((int)response.StatusCode, response);
    }
    
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<ApiResponse>> Update([FromBody] StudentUpdateDto model, int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var response =await _studentService.UpdateAsync(model, id);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<ApiResponse>> Delete(int id)
    {
        var response = await _studentService.RemoveAsync(id);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet("Enroll/{studentId:int}/{courseId:int}")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse>> Enroll(int studentId, int courseId)
    {
        var response = await _studentService.Enroll(studentId, courseId);
        return StatusCode((int)response.StatusCode, response);
    }





}