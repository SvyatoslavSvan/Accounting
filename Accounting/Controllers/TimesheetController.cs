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
        private TimesheetViewModel GetViewModel(Timesheet timesheet)
        {
            TimesheetViewModel viewModel = new TimesheetViewModel();
            viewModel.WorkDaysToHeader = timesheet.Employees.First().WorkDays.Where(x => x.Date.Month == timesheet.Date.Month && x.Date.Year == timesheet.Date.Year).ToList();
            foreach (var item in timesheet.Employees)
            {
                item.WorkDays = item.WorkDays.Where(x => x.Date.Month == timesheet.Date.Month && x.Date.Year == timesheet.Date.Year).ToList();
            }
            viewModel.Employees = timesheet.Employees.ToList();
            viewModel.Date = timesheet.Date;
            return viewModel;
        }
        
    }
   
}
