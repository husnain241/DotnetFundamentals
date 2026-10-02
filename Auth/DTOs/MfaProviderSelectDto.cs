namespace Auth.DTOs
{
    public class MfaProviderSelectDto
    {
        public string Provider { get; set; } = "Email"; // "Email" or "Phone"
    }
}