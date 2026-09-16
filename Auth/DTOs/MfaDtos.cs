namespace Auth.DTOs
{
    public class EnableMfaDto
    {
        public bool Enable { get; set; }
    }

    public class MfaStatusDto
    {
        public bool IsMfaEnabled { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public bool IsPhoneNumberConfirmed { get; set; }
    }
}