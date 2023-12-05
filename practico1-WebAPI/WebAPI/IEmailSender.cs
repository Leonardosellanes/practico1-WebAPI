using Shared;

namespace WebAPI
{
public interface IEmailSender
{
    Task SendEmailAsync(string toEmail, string subject, string body);

    Task SendOrderConfirmationAsync(string toEmail, Orden orden);

    }
}

