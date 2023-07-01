using System.ComponentModel.DataAnnotations.Schema;

namespace CourseApi.Entities;

public class Course
{
    public int Id { get; set; }
    public int TeacherId { get; set; }
    public string Name { get; set; }
    public  string Description { get; set; }
    public double Duration { get; set; }
    public double Ranking { get; set; }
    public double Price { get; set; }
    public DateTime LastUpdate { get; set; } = DateTime.Now;
    public DateTime CreationDate { get; set; } = DateTime.Now;
    public DateTime UpdateDate { get; set; } = DateTime.Now;

    [ForeignKey("TeacherId")]
    public Teacher Teacher { get; set; }
}