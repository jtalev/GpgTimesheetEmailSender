namespace GpgTimesheetEmailSender.Domain.Interfaces
{
    public interface IEmailService
    {
        Task SendEmail(string email);
    }
}
