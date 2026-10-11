using System.Collections.Generic;

namespace Auth.DTOs.Products
{
    public class ProductResponseDto
    {
        // Common Properties
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }

        // 🔑 Inheritance Properties (Nullable, kyunke har product mein nahi hongi)
        public string? ProductType { get; set; } // "Physical" ya "Digital" (Discriminator)
        public decimal? Weight { get; set; }
        public string? Dimensions { get; set; }
        public long? FileSize { get; set; }
        public string? DownloadUrl { get; set; }

        // 🔑 Many-to-Many Properties
        public List<string> CategoryNames { get; set; } = new List<string>();
        // Ya agar IDs chahiye: public List<int> CategoryIds { get; set; } = new List<int>();
    }
}