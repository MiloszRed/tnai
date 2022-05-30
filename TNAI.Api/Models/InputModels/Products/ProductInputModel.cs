using System.ComponentModel.DataAnnotations;

namespace TNAI.Api.Models.InputModels.Products
{
    public class ProductInputModel
    {
        [Required]
        [MaxLength(50)]
        public string Name  { get; set; }
        
        [Range(0, int.MaxValue)]
        public int    Price { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}