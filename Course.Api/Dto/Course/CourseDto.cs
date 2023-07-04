using System.ComponentModel.DataAnnotations;
using CourseApi.Dto.Teacher;

namespace CourseApi.Dto.Course;

public class CourseDto
{
    [Required]
    public int Id { get; set; }
    [Required]
    public int TeacherId { get; set; }
    [Required]
    public string Name { get; set; }
    public  string Description { get; set; }
    public double Duration { get; set; }
    public double Ranking { get; set; }
    public double Price { get; set; }
    public DateTime LastUpdate { get; set; } = DateTime.Now;

    public TeacherDto Teacher { get; set; }
}