using System.ComponentModel.DataAnnotations;

namespace CourseApi.Dto.User;

public class RegisterRequestDto
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string Role { get; set; }
}