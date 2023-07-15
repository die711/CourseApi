using CourseApi.Controllers.v1;
using CourseApi.Dto;
using CourseApi.Services.Implementations;
using CourseApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace TestCourseApi.Teacher;

public class TeacherControllerTest
{
    private TeacherController _controller;
    private ITeacherService _teacherService;

    public TeacherControllerTest()
    {
        _teacherService = new TeacherServiceFake();
        _controller = new TeacherController(_teacherService);
    }
    
    [Fact]
    public async void GetAll_test()
    {
        var result =await _controller.GetAll();

        var result2 = result.Result as ObjectResult; 

        var result3 = result2?.Value as ApiResponse;
        
        Assert.True(result3.IsSuccessful);
        Assert.Equal(200,(int)result3.StatusCode);

        var teacherList = result3.Result as List<CourseApi.Entities.Teacher>;
        
        Assert.Equal(3, teacherList.Count);
    }

    [Fact]
    public async void FindByIdAsync_test()
    {
        var result = await _controller.FindById(1);

        var result2 = result.Result as ObjectResult;

        var result3 = result2?.Value as ApiResponse;
        
        Assert.True(result3.IsSuccessful);
        Assert.Equal(200,(int)result3.StatusCode);

        var teacher = result3.Result as CourseApi.Entities.Teacher;
        Assert.NotNull(teacher);
        Assert.Equal(1, teacher.Id);
        Assert.Equal("diego", teacher.Name);
        Assert.Equal("description 1", teacher.Description);
        



    }
    
}