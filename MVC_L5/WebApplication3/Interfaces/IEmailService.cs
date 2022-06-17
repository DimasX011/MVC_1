namespace WebApplication3.Interfaces
{
    public interface IEmailService
    {
        public void SendEmail(string email, string subject, string message, string firstname, string lastname);
    }
}
