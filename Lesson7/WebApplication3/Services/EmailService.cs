using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using WebApplication3.Interfaces;

namespace WebApplication3.Services
{
    public class EmailService : IEmailService
    {
        async public void SendEmail(string email, string subject, string message, string firstname, string lastname)
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
               await  client.ConnectAsync("smtp.mail.ru", 25, false);
               await  client.AuthenticateAsync("Dimon1998daf@mail.ru", "1587panda");
               await  client.SendAsync(emailMessage);
               await  client.DisconnectAsync(true);
            }
        }
    }
}
