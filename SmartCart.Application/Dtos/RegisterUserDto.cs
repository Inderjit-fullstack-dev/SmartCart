using System.ComponentModel.DataAnnotations;

namespace SmartCart.Application.Dtos
{
    public class RegisterUserDto
    {
        [Required]
        [MaxLength(50)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [MaxLength(15)]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
