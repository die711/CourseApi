using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CourseApi.Entities;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
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