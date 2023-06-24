using System.ComponentModel.DataAnnotations;

namespace MAmail.Dtos
{
    public class UserLoginDto
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        [MinLength(8)]
        [MaxLength(256)]
        public string? Password { get; set; }
    }
}
