using System.ComponentModel.DataAnnotations;

namespace Auth.Models.Dtos
{
    public class CreatePhysicalProductDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public decimal Price { get; set; }
        public int StockQuantity { get; set; }

        // Physical specific properties
        public decimal Weight { get; set; }
        public string Dimensions { get; set; }
    }
}