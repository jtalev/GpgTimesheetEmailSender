using GpgTimesheetEmailSender.Domain.ValueObjects;

namespace GpgTimesheetEmailSender.Domain.Entities
{
    public class Timesheet
    {
        public required FullName FullName { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public required string Day { get; set; }
    
        public Timesheet(FullName fullname, int hours, int minutes, string day)
        {
            FullName = fullname;
            Hours = hours;
            Minutes = minutes;
            Day = day;
        }
    }
}
