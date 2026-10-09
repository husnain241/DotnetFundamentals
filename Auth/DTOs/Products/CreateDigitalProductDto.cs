using System.ComponentModel.DataAnnotations;

namespace Auth.Models.Dtos
{
    public class CreateDigitalProductDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public decimal Price { get; set; }
        public int StockQuantity { get; set; }

        // Digital specific properties
        public long FileSize { get; set; }
        public string DownloadUrl { get; set; }
    }
}