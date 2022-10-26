using System.ComponentModel.DataAnnotations;

namespace StoreMVC.DTOs
{
    public class LoginDto
    {
        [MinLength(4)]
        [MaxLength(23)]
        [Required]
        public string UserName { get; set; }
        [MinLength(5)]
        [MaxLength(18)]
        [Required]

        public string Password { get; set; }
    }
}
