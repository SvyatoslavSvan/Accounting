using Accounting.DAL.Interfaces;
using Accouting.Domain.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.Controllers
{
    public class TimesheetController : Controller
    {
        private readonly IWorkDayProvider _workDayProvider;
        private readonly IEmployeeManager _employeeManager;
        public TimesheetController(IWorkDayProvider workDayProvider, IEmployeeManager employeeProvider)
        {
            _workDayProvider = workDayProvider;
            _employeeManager = employeeProvider;
        }

        public async Task<IActionResult> Timesheet(DateTime month)
        {
            var createResult = await _workDayProvider.CreateNewWorkDays();
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
