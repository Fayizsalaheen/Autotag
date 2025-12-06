using System.ComponentModel.DataAnnotations;

namespace ImageAIService.DTO.UsereServcieDTO
{
    public class CreateUserDto
    {

        [Required]
        public string Username { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }


        [Required]
        [EmailAddress(ErrorMessage = "The email format is incorrect")]
        public string Email { get; set; }
        

        [Required]
        public string Password { get; set; }

        public string? phone { get; set; }

    }
}
