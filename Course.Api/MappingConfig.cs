using AutoMapper;
using CourseApi.Dto.Teacher;
using CourseApi.Entities;

namespace CourseApi;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<Teacher, TeacherDto>().ReverseMap();
        CreateMap<Teacher, TeacherCreateDto>().ReverseMap();
        CreateMap<Teacher, TeacherUpdateDto>().ReverseMap();
    }
    
}