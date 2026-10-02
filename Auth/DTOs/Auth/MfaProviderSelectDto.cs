namespace Auth.DTOs.Auth
{
    public class MfaProviderSelectDto
    {
        public string Provider { get; set; } = "Email"; // "Email" or "Phone"
    }
}