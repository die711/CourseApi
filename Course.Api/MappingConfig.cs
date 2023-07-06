using AutoMapper;
using CourseApi.Dto.Course;
using CourseApi.Dto.Student;
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
        
        CreateMap<Course, CourseDto>().ReverseMap();
        CreateMap<Course, CourseCreateDto>().ReverseMap();
        CreateMap<Course, CourseUpdateDto>().ReverseMap();

        CreateMap<Student, StudentDto>().ReverseMap();
        CreateMap<Student, StudentCreateDto>().ReverseMap();
        CreateMap<Student, StudentUpdateDto>().ReverseMap();
    }
}