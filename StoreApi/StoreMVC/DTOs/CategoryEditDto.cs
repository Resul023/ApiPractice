using System.ComponentModel.DataAnnotations;

namespace StoreMVC.DTOs
{
    public class CategoryEditDto
    {
        [MaxLength(20)]
        [Required]
        public string Name { get; set; }    
    }
}
