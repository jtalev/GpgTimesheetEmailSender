using System.Diagnostics;
using GpgTimesheetEmailSender.Application.DTOs;
using GpgTimesheetEmailSender.Application.Services;
using GpgTimesheetEmailSender.Models;
using GpgTimesheetEmailSender.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace GpgTimesheetEmailSender.Controllers
{
    public class TimesheetController : Controller
    {
        private readonly ILogger<TimesheetController> _logger;
        private readonly TimesheetService _timesheetService;

        public TimesheetController(ILogger<TimesheetController> logger, TimesheetService timesheetService)
        {
            _logger = logger;
            _timesheetService = timesheetService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Send([FromBody] TimesheetFormDTO form)
        {
            bool isValid;
            Error error;

            (isValid, error) = form.ValidatePasscode();
            if (!isValid)
            {
                _logger.Log(LogLevel.Error, $"Error validating passcode: unauthorized user");
                return BadRequest(error.Message);
            }
            try
            {
                (isValid, error) = form.ValidateForm();
                if (!isValid)
                {
                    _logger.Log(LogLevel.Error, $"Error validating form: {error.Message}");
                    return BadRequest(error.Message);
                }
            } catch (NullReferenceException e)
            {
                _logger.Log(LogLevel.Error, $"Null reference caught while validating form: {e.Message}");
                return BadRequest("Enter form values");
            }

            (isValid, error) = _timesheetService.SubmitTimesheet(form);
            if (!isValid)
            {
                _logger.Log(LogLevel.Error, $"Error submitting timesheet: {error.Message}");
                return StatusCode(500, error.Message);
            }

            _logger.Log(LogLevel.Information, $"Timesheet submitted by: {form.FirstName} {form.LastName}\nDate: {DateTime.Now}", form);
            return Ok("Timesheets submitted successfully");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
