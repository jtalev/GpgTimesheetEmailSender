using GpgTimesheetEmailSender.Domain.Entities;
using GpgTimesheetEmailSender.Domain.ValueObjects;
using System.Globalization;
using System.Text.Json.Serialization;

namespace GpgTimesheetEmailSender.Application.DTOs
{
    public record class TimesheetFormDTO
    {
        [JsonPropertyName("first_name")]
        public required string FirstName { get; set; } = string.Empty;
        [JsonPropertyName("last_name")]
        public required string LastName { get; set; } = string.Empty;
        [JsonPropertyName("week_start_date")]
        public required string WeekStartDate { get; set; } = string.Empty;
        [JsonPropertyName("timesheets")]
        public required TimesheetDTO[] Timesheets { get; set; } = Array.Empty<TimesheetDTO>();
        [JsonPropertyName("passcode")]
        public required string Passcode { get; set; } = string.Empty;
        
        public (bool, Error) ValidateForm()
        {
            var (isValid, error) = this.ValidateName();
            if (!isValid)
            {
                return (isValid, error);
            }
            (isValid, error) = this.ValidateWeekStartDate();
            if (!isValid)
            {
                return (isValid, error);
            }
            (isValid, error) = this.ValidateTimesheets();
            if (!isValid)
            {
                return (isValid, error);
            }

            return (true, new Error { Message = "" });
        }

        public (bool, Error) ValidateName()
        {
            Error error = new Error { Message = "" };
            if (0 == this.FirstName.Length || 0 == this.LastName.Length)
            {
                error.Message = "First and last name required";
                return (false, error);
            }

            return (true, error);
        }

        public (bool, Error) ValidateWeekStartDate()
        {
            Error error = new Error { Message = "" };
            string[] dateArr = this.WeekStartDate.Split("-");


            if (3 != dateArr.Length)
            {
                error.Message = "Input valid payweek start date";
                return (false, error);
            }

            return (true, error);
        }

        public (bool, Error) ValidateTimesheets()
        {
            Error error = new Error { Message = "" };

            if (7 != this.Timesheets.Length)
            {
                error.Message = "Enter value for all timesheets";
                return (false, error);
            }

            return (true, error);
        }
    }

}
