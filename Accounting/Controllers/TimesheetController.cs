using Accounting.DAL.Result.Provider.Base;
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

        public async Task<IActionResult> Timesheet(DateTime month)
        {
            var createResult = await _timesheetManager.CreateNew();
            if (createResult.Succed && createResult.OperationStatus == OperationStatuses.Ok)
            {
                return View(createResult.Data);
            }
            if (createResult.Succed && createResult.OperationStatus == OperationStatuses.AllTimesheetsMatch && month != default(DateTime))
            {
                var timesheet = await _timesheetManager.GetByDate(month);
                if (timesheet.Data == null)
                {
                    return View("NotFound");
                }
                return View(timesheet.Data);
            }
            if (createResult.Succed && createResult.OperationStatus == OperationStatuses.AllTimesheetsMatch)
            {
                var timesheet = await _timesheetManager.GetByDate(DateTime.Now);
                return View(timesheet.Data);
            }
            return BadRequest();
        }

        
    }
   
}
