using System.ComponentModel.DataAnnotations;

namespace CourseApi.Dto.User;

public class UserDto
{
    
    [Required]
    public int Id { get; set; }
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Role { get; set; }
}