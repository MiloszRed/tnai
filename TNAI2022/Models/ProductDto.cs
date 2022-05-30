using System.ComponentModel.DataAnnotations;

namespace TNAI2022.Models
{
    public class ProductDto
    {
        [Required(ErrorMessage = "Not null!")]
        public string Name  { get; set; }

        [Range(0, 10)]
        public int    Price { get; set; }
    }
}