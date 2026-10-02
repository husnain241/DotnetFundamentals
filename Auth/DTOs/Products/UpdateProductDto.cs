namespace Auth.DTOs.Products
{
    public class UpdateProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }

        // The client must send back the RowVersion they read
        public byte[] RowVersion { get; set; } = Array.Empty<byte>();
    }
}