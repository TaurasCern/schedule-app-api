using System.ComponentModel.DataAnnotations;

namespace ScheduleAppApi.DTOs.User
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(7, ErrorMessage = "Password cannot be shorter than 7 characters")]
        [MaxLength(20, ErrorMessage = "Password cannot be longer than 20 characters")]
        public string Password { get; set; }
    }
}
