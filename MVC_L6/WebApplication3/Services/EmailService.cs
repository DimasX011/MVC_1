using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using WebApplication3.Interfaces;

namespace WebApplication3.Services
{
    public class EmailService : IEmailService
    {
        public async Task SendEmail(string email, string subject, string message, string firstname, string lastname)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("От студента GB " + firstname + " " + lastname, "asp2022gb@rodion-m.ru"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.beget.com", 25, false);
                await client.AuthenticateAsync("asp2022gb@rodion-m.ru", "3drtLSa1");
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}
