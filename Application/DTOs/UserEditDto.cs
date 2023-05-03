using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class UserEditDto
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string Login { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    [RegularExpression("[MF]")]
    public string Gender { get; set; }
}