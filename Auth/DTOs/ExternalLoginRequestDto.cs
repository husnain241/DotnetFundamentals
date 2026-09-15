namespace Auth.DTOs
{
    public class ExternalLoginRequestDto
    {
        public string Provider { get; set; } = "Google";
        public string ReturnUrl { get; set; } = "/";
    }
}