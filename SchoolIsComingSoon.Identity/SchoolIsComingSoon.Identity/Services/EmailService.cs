using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace SchoolIsComingSoon.Identity.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var smtpSettings = _configuration.GetSection("Smtp");

            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Скоро в школу!", smtpSettings["From"]));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using var client = new SmtpClient();
            client.ServerCertificateValidationCallback = (s, c, h, e) => true;

            await client.ConnectAsync(smtpSettings["Host"], int.Parse(smtpSettings["Port"]), SecureSocketOptions.SslOnConnect);
            await client.AuthenticateAsync(smtpSettings["User"], smtpSettings["Password"]);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }
}
