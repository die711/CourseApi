using System.Net;
using CourseApi.Dto;
using CourseApi.Dto.User;
using CourseApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CourseApi.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersionNeutral]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("Register")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<ApiResponse>> Register([FromBody] RegisterRequestDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = await _userService.Register(dto);
        return StatusCode((int)response.StatusCode, response);
    }

    
    [HttpPost("Login")]
    public async Task<ActionResult<ApiResponse>> Login([FromBody] LoginRequestDto dto)
    {
        var response = await _userService.Login(dto);
        return StatusCode((int)response.StatusCode, response);
    }










}