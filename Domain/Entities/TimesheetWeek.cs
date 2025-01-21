using GpgTimesheetEmailSender.Domain.ValueObjects;

namespace GpgTimesheetEmailSender.Domain.Entities
{
    public class TimesheetWeek
    {
        public required FullName FullName { get; set; }
        public DateTime WeekStartDate { get; set; }
        public Timesheet[] Timesheets { get; set; }

        public TimesheetWeek(FullName fullname, DateTime weekStartDate, Timesheet[] timesheets)
        {
            FullName = fullname;
            WeekStartDate = weekStartDate;
            Timesheets = timesheets;
        }
    }
}
