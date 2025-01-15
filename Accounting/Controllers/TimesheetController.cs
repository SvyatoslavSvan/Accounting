using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using Accounting.ViewModels;
using Accouting.Domain.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.Controllers
{
    public class TimesheetController : Controller
    {
        private readonly ITimesheetManager _timesheetManager;
        public TimesheetController(ITimesheetManager timesheetManager)
        {
            _timesheetManager = timesheetManager;
        }

        public async Task<IActionResult> Timesheet(DateTime Date)
        {
            var createResult = await _timesheetManager.CreateNew();
            if (createResult.Succed && createResult.OperationStatus == OperationStatuses.Ok)
            {
                return View(GetViewModel(createResult.Data));
            }
            if (createResult.Succed && createResult.OperationStatus == OperationStatuses.AllTimesheetsMatch && Date != default(DateTime))
            {
                var timesheet = await _timesheetManager.GetByDate(Date);
                if (timesheet.Data == null)
                {
                    return View("NotFound");
                }
                return View(GetViewModel(timesheet.Data));
            }
            if (createResult.Succed && createResult.OperationStatus == OperationStatuses.AllTimesheetsMatch)
            {
                var timesheet = await _timesheetManager.GetByDate(DateTime.Now);
                return View(GetViewModel(timesheet.Data));
            }
            return BadRequest();
        }

        public async Task<IActionResult> UpdateWorkHoursDays(UpdateHoursWorkDaysViewModel viewModel)
        {
            var getTimesheetResult = await _timesheetManager.GetById(viewModel.TimesheetId);
            if (getTimesheetResult.Succed)
            {
                getTimesheetResult.Data.HoursCount = viewModel.WorkHours;
                getTimesheetResult.Data.DaysCount = viewModel.WorkDays;
                var updateResult = await _timesheetManager.Update(getTimesheetResult.Data);
                if (updateResult.Succed)
                {
                    return Ok();
                }
            }
            return StatusCode(500);
        }

        public async Task<IActionResult> UpdateWorkDaysForEmployee(UpdateWorkDaysForEmployeeViewModel viewModel)
        {
            var updateResult = await _timesheetManager.UpdateWorkDaysForEmployee(viewModel.Value, viewModel.EmployeeId, viewModel.TimesheetId);
            if (updateResult.Succed)
            {
                return View("Timesheet", GetViewModel(updateResult.Data));
            }
            return StatusCode(500);
        }

        public async Task<IActionResult> UpdateWorkDaysByDate(Guid timesheetId, float value, DateTime date)
        {
            var updateResult = await _timesheetManager.UpdateWorkDaysByDate(value, date, timesheetId);
            if (updateResult.Succed)
            {
                return View("Timesheet", GetViewModel(updateResult.Data));
            }
            return StatusCode(500);
        }

        private TimesheetViewModel GetViewModel(Timesheet timesheet)
        {
            TimesheetViewModel viewModel = new TimesheetViewModel();
            viewModel.WorkDaysToHeader = timesheet.WorkDays.Where(x => x.EmployeeId == timesheet.Employees.First().Id).ToList();
            foreach (var item in timesheet.Employees)
            {
                item.WorkDays = timesheet.WorkDays.Where(x => x.EmployeeId == item.Id).ToList();
            }
            viewModel.Employees = timesheet.Employees.ToList();
            viewModel.Date = timesheet.Date;
            viewModel.Id = timesheet.Id;
            viewModel.DaysCount = timesheet.DaysCount;
            viewModel.HoursCount = timesheet.HoursCount;
            return viewModel;
        }
        
    }
   
}
