using System.Text.Json.Serialization;

namespace GpgTimesheetEmailSender.Application.DTOs
{
    public record class TimesheetDTO
    {
        [JsonPropertyName("hours")]
        public required int Hours { get; set; }
        [JsonPropertyName("minutes")]
        public required int Minutes { get; set; }
        [JsonPropertyName("day")]
        public required string Day { get; set; }
    }
}
