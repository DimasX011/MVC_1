namespace WebApplication3.Interfaces
{
    public interface IEmailService
    {
        public Task SendEmail(string email, string subject, string message, string firstname, string lastname, CancellationToken cancellationToken);
    }
}
