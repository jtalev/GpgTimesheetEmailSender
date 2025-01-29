using GpgTimesheetEmailSender.Domain.Interfaces;
using System;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;


namespace GpgTimesheetEmailSender.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        public async Task SendEmail(string email)
        {
            var apiKey = Environment.GetEnvironmentVariable("EMAIL_API_KEY");
            var client = new SendGridClient(apiKey);
            var fromEmail = new EmailAddress("talevjoshua1@gmail.com", "Josh Talev");
            var subject = "Weekly Timesheets";
            var toEmail = new EmailAddress("j.talev@outlook.com", "Josh Talev");
            var textContent = email;
            var msg = MailHelper.CreateSingleEmail(fromEmail, toEmail, subject, textContent, null);
            try
            {
                var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
                Console.WriteLine("Email sent, waiting on response");
                if ((int)response.StatusCode >= 200 && (int)response.StatusCode < 300)
                {
                    Console.WriteLine($"Email sent successfully: {(int)response.StatusCode}");
                }
                else
                {
                    Console.WriteLine($"Error response from sendgrid: {response.Body}");
                    throw new Exception($"Error response from sendgrid api: {response.Body}");
                }
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }
    }
}
