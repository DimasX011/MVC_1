using MailKit.Net.Smtp;
using MimeKit;
using WebApplication3.Interfaces;


namespace WebApplication3.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpConfig _smtpConfig;
       
        public EmailService(Microsoft.Extensions.Options.IOptions<SmtpConfig> options)
        {
            _smtpConfig = options.Value;
        }
        public async Task SendEmail(string email, string subject, string message, string firstname, string lastname, CancellationToken cancellationToken)
        {
            if (!(cancellationToken.IsCancellationRequested))
            {
                var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress("От студента GB " + firstname + " " + lastname, "Dimon1998daf@mail.ru"));
                emailMessage.To.Add(new MailboxAddress("", email));
                emailMessage.Subject = subject;
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = message
                };
                using (var client = new SmtpClient())
                {
                    await  client.ConnectAsync(_smtpConfig.Host, _smtpConfig.Port, false);
                    await  client.AuthenticateAsync(_smtpConfig.UserName, _smtpConfig.Password);
                    await  client.SendAsync(emailMessage);
                    await  client.DisconnectAsync(true);
            }
            }
        }
    }
}
