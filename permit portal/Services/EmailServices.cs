using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

using System.Threading.Tasks;



public interface IEmailService
{
    Task SendEmail(string toEmail, string subject, string body);
}


public class EmailServices : IEmailService
{
    private readonly IConfiguration _config;

    public EmailServices(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendEmail(string toEmail, string subject, string body)
    {
        try
        {
            var smtpHost = _config["EmailSettings:Host"];
            var smtpPort = int.Parse(_config["EmailSettings:Port"]);
            var smtpUser = _config["EmailSettings:Username"];
            var smtpPass = _config["EmailSettings:Password"];

            using var client = new SmtpClient(smtpHost, smtpPort)
            {
                Credentials = new NetworkCredential(smtpUser, smtpPass),
                EnableSsl = true
            };

            var message = new MailMessage
            {
                From = new MailAddress(smtpUser),
                Subject = subject,
                Body = body,
                IsBodyHtml = false
            };
            message.To.Add(toEmail);

              await client.SendMailAsync(message);
        }
        catch (Exception ex)
        {
            // Log the detailed exception
            Console.WriteLine("Email sending failed: " + ex.Message);
            if (ex.InnerException != null)
                Console.WriteLine("Inner exception: " + ex.InnerException.Message);

            throw; // rethrow to let the controller catch it
        }
    }
}