using System.Net;
using CourseApi.Dto;
using CourseApi.Dto.Course;
using CourseApi.Dto.Teacher;
using CourseApi.Repositories.Interfaces;
using CourseApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CourseApi.Controllers.v1;

[Route("api/[controller]")]
[ApiController]
public class CourseController : ControllerBase
{
    private readonly ICourseService _courseService;


    public CourseController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpGet]
    [Authorize(Roles = "admin")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse>> GetAll()
    {
        var response = await _courseService.ListAsync();
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet("Teacher/{teacherId:int}")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse>> GetAllTeacherId(int teacherId)
    {
        var response =await _courseService.ListByTeacherIdAsync(teacherId);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet("Ranking/{quantity:int}")]
    public async Task<ActionResult<ApiResponse>> GetRanking(int quantity)
    {
        var response =await _courseService.TopRankingAsync(quantity);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse>> FindById(int id)
    {
        var response = await _courseService.FindByIdAsync(id);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<ApiResponse>> Create([FromBody] CourseCreateDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = await _courseService.CreateAsync(model);

        if (response.StatusCode == HttpStatusCode.Created)
            return CreatedAtAction(nameof(FindById), new { id = response.Result }, response);

        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<ApiResponse>> Update([FromBody] CourseUpdateDto model, int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
       
        var response = await _courseService.UpdateAsync(model, id);

        return StatusCode((int)response.StatusCode, response);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<ApiResponse>> Delete(int id)
    {
        var response =await _courseService.RemoveAsync(id);
        return StatusCode((int)response.StatusCode, response);
    }
    
}