using ImageAIService.Models;
using System.ComponentModel.DataAnnotations;

public class User : BaseEntity
{
    public int Id { get; set; }
    public bool IsActive { get; set; } = true;
    [Required] public string FirstName { get; set; }
    [Required] public string LastName { get; set; }
    public string? ProfileImage { get; set; }
    public int UserType { get; set; } = 1;
    [Required] public string Username { get; set; }
    [Required]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; }
    [Required(ErrorMessage = "A password is required")]
    [MinLength(6)]
    public string Password { get; set; }
    public virtual List<Image> Images { get; set; } = new List<Image>();
    public bool IsLogedIn { get; set; } = false;
    public DateTime? LastLoginTime { get; set; }
    public string? OTP { get; set; }
    public string? Phone { get; set; }
}
