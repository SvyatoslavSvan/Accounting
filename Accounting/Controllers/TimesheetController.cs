using Accouting.Domain.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.Controllers
{
    public class TimesheetController : Controller
    {
        private readonly IWorkDayManager _workDayManager;
        private readonly IEmployeeManager _employeeManager;
        public TimesheetController(IWorkDayManager workDayManager, IEmployeeManager employeeProvider)
        {
            _workDayManager = workDayManager;
            _employeeManager = employeeProvider;
        }

        public async Task<IActionResult> Timesheet(DateTime month)
        {
            var createResult = await _workDayManager.CreateNewWorkDays();
            if (createResult.Succed)
            {
                var getEmployeesResult = await _employeeManager.GetEmployeesWithWorkDaysByDate(month == default(DateTime) ? DateTime.Now : month);
                if (getEmployeesResult.Succed)
                {
                    return View(getEmployeesResult.Data);
                }
            }
            return BadRequest();
        }

        
    }
   
}
