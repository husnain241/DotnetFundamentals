namespace Auth.DTOs
{
    public class VerifyMfaDto
    {
        public string UserId { get; set; } = string.Empty;
        public string Provider { get; set; } = string.Empty; // "Email" or "Phone"
        public string Code { get; set; } = string.Empty;
    }
}