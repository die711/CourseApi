using System.Net;
using CourseApi.Dto;
using CourseApi.Dto.Specifications;
using CourseApi.Dto.Teacher;
using CourseApi.Entities;
using CourseApi.Services.Interfaces;
using Microsoft.AspNetCore.JsonPatch;

namespace TestCourseApi.Teacher;

public class TeacherServiceFake : ITeacherService
{

    private readonly List<CourseApi.Entities.Teacher> _teachersList;
    private ApiResponse _response;
    
    
    public TeacherServiceFake()
    {
        _teachersList = new()
        {
            new CourseApi.Entities.Teacher() {Id = 1, Name = "diego", Description = "description 1"},    
            new CourseApi.Entities.Teacher() {Id = 2, Name = "juan", Description = "description 1"},    
            new CourseApi.Entities.Teacher() {Id = 3, Name = "fransisco", Description = "description 1"}    
        };

        _response = new();
    }
    
    public async Task<ApiResponse> ListAsync()
    {
        var teachears = _teachersList;

        _response.StatusCode = HttpStatusCode.OK;
        _response.Result = teachears;

        return _response;
    }

    public  Task<ApiResponse> GetAllPagination(Params parameters)
    {
        throw new NotImplementedException();
    }

    public async Task<ApiResponse> FindByIdAsync(int id)
    {
        var teacher = _teachersList[0];
        _response.StatusCode = HttpStatusCode.OK;
        _response.Result = teacher;

        return _response;
    }

    public Task<ApiResponse> CreateAsync(TeacherCreateDto model)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse> UpdateAsync(TeacherUpdateDto model, int id)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse> RemoveAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse> UpdatePartialAsync(JsonPatchDocument<TeacherUpdateDto> patchDto, int id)
    {
        throw new NotImplementedException();
    }
}