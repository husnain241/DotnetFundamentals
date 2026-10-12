using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Auth.Models.Dtos
{
    public class CreateProductDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }

        public List<int> CategoryIds { get; set; } = new List<int>();
    }
}