using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace SchoolIsComingSoon.Identity.Services
{
    public class EmailService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Скоро в школу!", "info@schooliscomingsoon.ru"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.timeweb.ru", 2525, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync("info@schooliscomingsoon.ru", "password");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}