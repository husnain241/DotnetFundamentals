namespace Auth.Models.Product
{
    // Yeh bhi Product class se inherit kar raha hai
    public class DigitalProduct : Product
    {
        public long FileSize { get; set; } // Bytes mein
        public string DownloadUrl { get; set; } = string.Empty;
    }
}