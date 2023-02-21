using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos;

public class RegisterDto
{
    [Required,MaxLength(100)]
    public string  Email { get; set; }
 
    [Required,MinLength(9)]
    public string  PhoneNumber { get; set; }
  
    [Required(ErrorMessage = "Password is required")]
    [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required(ErrorMessage = "Confirm Password is required")]
    [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
    [DataType(DataType.Password)]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }
}