using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;

public class EmailService
{
    private readonly string _smtpHost = "smtp.gmail.com";
    private readonly int _smtpPort = 587; // TLS port
    private readonly string _adminEmail;    // your admin Gmail
    private readonly string _appPassword;   // Gmail app password

    public EmailService(string adminEmail, string appPassword)
    {
        _adminEmail = adminEmail;
        _appPassword = appPassword;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Admin", _adminEmail));
        message.To.Add(MailboxAddress.Parse(toEmail));
        message.Subject = subject;

        message.Body = new TextPart("plain")
        {
            Text = body
        };

        using var client = new SmtpClient();
        await client.ConnectAsync(_smtpHost, _smtpPort, MailKit.Security.SecureSocketOptions.StartTls);

        // Authenticate using app password
        await client.AuthenticateAsync(_adminEmail, _appPassword);

        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}
