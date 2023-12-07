using System.Net.Mail;
using System.Net;
using WebAPI.Models;
using Shared;
using Microsoft.Extensions.Options;

namespace WebAPI
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfiguration;

        public EmailSender(IOptions<EmailConfiguration> emailConfiguration)
        {
            _emailConfiguration = emailConfiguration.Value;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            using (var client = new SmtpClient(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort))
            {
                client.Credentials = new NetworkCredential(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);
                client.EnableSsl = true;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_emailConfiguration.SmtpUsername),
                    Subject = subject,
                    Body = body,
                };

                mailMessage.To.Add(toEmail);

                await client.SendMailAsync(mailMessage);
            }
        }

        public async Task SendOrderConfirmationAsync(string toEmail, Orden orden)
        {
            
            var subject = "Confirmación de Orden";
            var body = $"Gracias por tu orden. Detalles de la orden: {orden}";
            await SendEmailAsync(toEmail, subject, body);
        }
    }
}