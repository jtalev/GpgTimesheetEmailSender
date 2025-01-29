namespace GpgTimesheetEmailSender.Domain.ValueObjects
{
    public class Error
    {
        public required string Message { get; set; }

        public Error() { }

        public Error(string message)
        {
            Message = message;
        }
    }
}
