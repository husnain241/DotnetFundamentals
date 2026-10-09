namespace Auth.Models.Product
{
    // Yeh Product class se inherit kar raha hai
    public class PhysicalProduct : Product
    {
        public decimal Weight { get; set; }
        public string Dimensions { get; set; } = string.Empty;
    }
}