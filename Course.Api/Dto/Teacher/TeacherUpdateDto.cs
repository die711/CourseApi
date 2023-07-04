using System.ComponentModel.DataAnnotations;

namespace CourseApi.Dto.Teacher;

public class TeacherUpdateDto
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string LastName { get; set; }
    public string Description { get; set; }
}