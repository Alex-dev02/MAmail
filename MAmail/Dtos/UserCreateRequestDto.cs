using System.ComponentModel.DataAnnotations;

namespace MAmail.Dtos
{
    public class UserCreateRequestDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string FirstName { get; set; } = null!;
        [Required]
        [MinLength(3)]
        [MaxLength(15)]
        public string LastName { get; set; } = null!;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        [MinLength(8)]
        [MaxLength(64)]
        public string Password { get; set; } = null!;
    }
}
