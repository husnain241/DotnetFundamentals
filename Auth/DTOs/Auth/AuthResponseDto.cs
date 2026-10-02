namespace Auth.DTOs.Auth
{
    public class AuthResponseDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }


        // MFA Specific properties
        public bool IsMfaRequired { get; set; }
        public string? UserId { get; set; }
        public string? Provider { get; set; }
    }
}