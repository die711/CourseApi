using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CourseApi.Entities;

[PrimaryKey(nameof(CourseId), nameof(StudentId))]
public class StudentCourse
{
    public int CourseId { get; set; }
    public int StudentId { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.Now;
    public DateTime UpdateDate { get; set; } = DateTime.Now;

    [ForeignKey("CourseId")]
    public Course Course { get; set; }
    [ForeignKey("StudentId")]
    public Student Student { get; set; }
}