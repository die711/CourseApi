namespace CourseApi.Entities;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.Now;
    public DateTime UpdateDate { get; set; } = DateTime.Now;
}