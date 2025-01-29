using GpgTimesheetEmailSender.Application.DTOs;
using GpgTimesheetEmailSender.Domain.Entities;
using GpgTimesheetEmailSender.Domain.Interfaces;
using GpgTimesheetEmailSender.Domain.ValueObjects;

namespace GpgTimesheetEmailSender.Application.Services
{
    public class TimesheetService
    {
        private readonly IEmailService _emailService;
        public TimesheetService(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public (bool, Error) SubmitTimesheet(TimesheetFormDTO requestDto)
        {
            TimesheetWeek timesheetWeek = CreateTimesheetWeek(requestDto);

            var (isValid, error) = timesheetWeek.Validate();
            if (!isValid)
            {
                return (isValid, error);
            }

            string email = FormatEmailString(timesheetWeek);
            try
            {
                _emailService.SendEmail(email);
            }
            catch (Exception) { 
                return (false, new Error { Message = "Internal error sending email" });
            }

            return (true, new Error { Message = "" });
        }

        public TimesheetWeek CreateTimesheetWeek(TimesheetFormDTO requestDto)
        {
            TimesheetWeek outTimesheetWeek = new();

            string fullName = new FullName(requestDto.FirstName, requestDto.LastName).ToString();
            DateTime weekStartDate = FormatDate(requestDto.WeekStartDate);
            Timesheet[] timesheets = MapTimesheets(requestDto.Timesheets);

            outTimesheetWeek.FullName = fullName;
            outTimesheetWeek.WeekStartDate = weekStartDate;
            outTimesheetWeek.Timesheets = timesheets;

            return outTimesheetWeek;
        }

        public DateTime FormatDate(string inDate)
        {
            string[] dateArr = inDate.Split("-");
            int yearInt, monthInt, dayInt;
            DateTime outDate;
            try
            {
                yearInt = Int32.Parse(dateArr[0]);
                monthInt = Int32.Parse(dateArr[1]);
                dayInt = Int32.Parse(dateArr[2]);
            } catch (FormatException e)
            {
                throw new FormatException(e.Message);
            }
            outDate = new DateTime(yearInt, monthInt, dayInt);
            return outDate;
        }

        public Timesheet[] MapTimesheets(TimesheetDTO[] inTimesheetsDto)
        {
            Timesheet[] outTimesheets = new Timesheet[7];

            for (int i = 0; i < inTimesheetsDto.Length; i++)
            {
                Timesheet timesheet = new Timesheet{
                    Hours = inTimesheetsDto[i].Hours,
                    Minutes = inTimesheetsDto[i].Minutes,
                    Day = inTimesheetsDto[i].Day,
                };
                outTimesheets[i] = timesheet;
            }

            return outTimesheets;
        }

        private string FormatEmailString(TimesheetWeek timesheetWeek)
        {
            string total = timesheetWeek.CalcuateTotal();
            string email = $"Employee: {timesheetWeek.FullName}\n" +
                $"Payweek start date: {timesheetWeek.WeekStartDate.Day}-{timesheetWeek.WeekStartDate.Month}-{timesheetWeek.WeekStartDate.Year}\n\n";
            for (int i = 0; i < 7; i++)
            {
                email += timesheetWeek.Timesheets[i].ToStringEmail();
            }
            email += $"\nTotal     - {total}\n";
            return email;
        }
    }
}
