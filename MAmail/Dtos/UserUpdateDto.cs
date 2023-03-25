using System.ComponentModel.DataAnnotations;

namespace MAmail.Dtos
{
    public class UserUpdateDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string FirstName { get; set; } = null!;
        [Required]
        [MinLength(3)]
        [MaxLength(15)]
        public string LastName { get; set; } = null!;
    }
}
