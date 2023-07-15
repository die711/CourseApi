using System.Net;
using CourseApi.Dto;
using CourseApi.Dto.Teacher;
using CourseApi.Repositories.Interfaces;
using CourseApi.Services.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
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
    
    [HttpGet("Pagination")]
    

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse>> FindById(int id)
    {
        var response = await _teacherService.FindByIdAsync(id);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<ApiResponse>> Create([FromBody] TeacherCreateDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = await _teacherService.CreateAsync(model);

        if (response.StatusCode == HttpStatusCode.Created)
            return CreatedAtAction(nameof(FindById), new { id = response.Result }, response);

        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<ApiResponse>> Update([FromBody] TeacherUpdateDto model, int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = await _teacherService.UpdateAsync(model, id);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<ApiResponse>> Delete(int id)
    {
        var response = await _teacherService.RemoveAsync(id);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPatch("{id:int}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<ApiResponse>> UpdatePartial(JsonPatchDocument<TeacherUpdateDto> patchDto, int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = await _teacherService.UpdatePartialAsync(patchDto, id);
        return StatusCode((int)response.StatusCode, response);
    }
    
}