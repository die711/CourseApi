using System.ComponentModel.DataAnnotations;

namespace CourseApi.Dto.Teacher;

public class TeacherCreateDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string LastName { get; set; }
    public string Description { get; set; }
}