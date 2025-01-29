using GpgTimesheetEmailSender.Domain.ValueObjects;

namespace GpgTimesheetEmailSender.Domain.Entities
{
    public class TimesheetWeek
    {
        public string FullName { get; set; }
        public DateTime WeekStartDate { get; set; }
        public Timesheet[] Timesheets { get; set; }

        public TimesheetWeek() { }

        public TimesheetWeek(string fullname, DateTime weekStartDate, Timesheet[] timesheets)
        {
            FullName = fullname;
            WeekStartDate = weekStartDate;
            Timesheets = timesheets;
        }

        public override string ToString() {
            return $"TimesheetWeek:\n" +
                $"FullName: {this.FullName}\n" +
                $"WeekStartDate: {this.WeekStartDate}\n" +
                $"Timesheets: {this.Timesheets} \n";
        }

        public (bool, Error) Validate()
        {
            var (isValid, error) = this.ValidateWeekStartDate();
            if (!isValid)
            {
                return (isValid, error);
            }
            for (int i = 0; i < 7; i++)
            {
                (isValid, error) = this.Timesheets[i].Validate();
                if (!isValid)
                {
                    return (isValid, error);
                }
            }

            return (true, new Error { Message = "" });
        }

        public (bool, Error) ValidateWeekStartDate()
        {
            Error error = new Error { Message = "" };
            DateTime date = this.WeekStartDate;
            if (date.DayOfWeek != DayOfWeek.Wednesday)
            {
                error.Message = "Payweek start date must be a Wednesday";
                return (false, error);
            }

            return (true, error);
        }

        public string CalcuateTotal()
        {
            int hrs = 0, mins = 0;

            for (int i = 0; i < 7; i++)
            {
                hrs += this.Timesheets[i].Hours;
                mins += this.Timesheets[i].Minutes;
            }
            hrs += mins / 60;
            mins = mins % 60;
            return $"{hrs}:{mins}";
        }
    }
}
