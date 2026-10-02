namespace Auth.Options
{
    public class SmsOptions
    {
        public const string SectionName = "Sms";

        public string AccountSid { get; set; } = string.Empty;
        public string AuthToken { get; set; } = string.Empty;
        public string FromPhoneNumber { get; set; } = string.Empty;
    }
}