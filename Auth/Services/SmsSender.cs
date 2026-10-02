using Microsoft.Extensions.Options;
using Auth.Options;

namespace Auth.Services
{
    public class SmsSender : ISmsSender
    {
        private readonly SmsOptions _smsOptions;
        private readonly ILogger<SmsSender> _logger;

        public SmsSender(IOptions<SmsOptions> smsOptions, ILogger<SmsSender> logger)
        {
            _smsOptions = smsOptions.Value;
            _logger = logger;
        }

        public async Task SendSmsAsync(string phoneNumber, string message)
        {
            // Real SMS Credentials/Provider setup na hone par Dev Logging Safe Fallback
            if (string.IsNullOrEmpty(_smsOptions.AccountSid) || string.IsNullOrEmpty(_smsOptions.AuthToken))
            {
                _logger.LogInformation("SMS Credentials not provided. Development MFA SMS sent to {PhoneNumber} with Message: {Message}", phoneNumber, message);
                await Task.CompletedTask;
                return;
            }

            // Real SMS Provider Logic (e.g. Twilio/Bandwidth API integration) goes here
            _logger.LogInformation("Sending SMS to {PhoneNumber} via SMS Provider", phoneNumber);
            await Task.CompletedTask;
        }
    }
}