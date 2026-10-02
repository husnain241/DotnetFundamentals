using Auth.Configuration;
using Auth.Options;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Auth.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailOptions _emailOptions;
        private readonly ILogger<EmailSender> _logger;

        public EmailSender(IOptions<EmailOptions> emailOptions, ILogger<EmailSender> logger)
        {
            _emailOptions = emailOptions.Value;
            _logger = logger;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            // Real SMTP credentials set na hone ki surat mein Dev mode log fall-back
            if (string.IsNullOrEmpty(_emailOptions.Host) || string.IsNullOrEmpty(_emailOptions.Username))
            {
                _logger.LogInformation("SMTP credentials not configured. Development MFA Email targeted to {Email} with Subject: {Subject}", email, subject);
                return;
            }

            using var client = new SmtpClient(_emailOptions.Host, _emailOptions.Port)
            {
                Credentials = new NetworkCredential(_emailOptions.Username, _emailOptions.Password),
                EnableSsl = _emailOptions.EnableSsl
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailOptions.From),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };
            mailMessage.To.Add(email);

            await client.SendMailAsync(mailMessage);
        }
    }
}