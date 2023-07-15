using System.Net;
using CourseApi.Dto;
using CourseApi.Dto.User;
using CourseApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CourseApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("Register")]
    public async Task<ActionResult<ApiResponse>> Register([FromBody] RegisterRequestDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = await _userService.Register(dto);
        return StatusCode((int)response.StatusCode, response);
    }
    
    







}