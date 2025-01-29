using GpgTimesheetEmailSender.Domain.ValueObjects;

namespace GpgTimesheetEmailSender.Domain.Entities
{
    public class Timesheet
    {
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public required string Day { get; set; }

        public Timesheet() { }
    
        public Timesheet(int hours, int minutes, string day)
        {
            Hours = hours;
            Minutes = minutes;
            Day = day;
        }

        public override string ToString()
        {
            return $"Timesheet\nHours: {this.Hours}\nMinutes: {this.Minutes}\nDay: {this.Day}\n";
        }

        public string ToStringEmail()
        {
            switch (this.Day)
            {
                case "Wednesday":
                    return $"{this.Day} - Hrs: {this.Hours}   Mins: {this.Minutes}\n";
                case "Thursday":
                    return $"{this.Day}  - Hrs: {this.Hours}   Mins: {this.Minutes}\n";
                case "Friday":
                    return $"{this.Day}    - Hrs: {this.Hours}   Mins: {this.Minutes}\n";
                case "Saturday":
                    return $"{this.Day}  - Hrs: {this.Hours}   Mins: {this.Minutes}\n";
                case "Sunday":
                    return $"{this.Day}    - Hrs: {this.Hours}   Mins: {this.Minutes}\n";
                case "Monday":
                    return $"{this.Day}    - Hrs: {this.Hours}   Mins: {this.Minutes}\n";
                case "Tuesday":
                    return $"{this.Day}   - Hrs: {this.Hours}   Mins: {this.Minutes}\n";
                default:
                    return "Error bulding timesheet string";
            }
        }

        public (bool, Error) Validate()
        {
            if (this.Hours < 0 || this.Minutes < 0)
            {
                return (false, new Error { Message = "Hrs and Mins values must be 0 or greater" });
            }

            return (true, new Error { Message = "" });
        }
    }
}
