using Accounting.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.Controllers
{
    public class TimesheetController : Controller
    {
        private readonly IWorkDayProvider _workDayProvider;
        private readonly IEmployeeProvider _employeeProvider;
        public TimesheetController(IWorkDayProvider workDayProvider, IEmployeeProvider employeeProvider)
        {
            _workDayProvider = workDayProvider;
            _employeeProvider = employeeProvider;
        }

        public async Task<IActionResult> Timesheet(DateTime month)
        {
            var createResult = await _workDayProvider.CreateNewWorkDays();
            if (createResult.Succed)
            {
                var getEmployeesResult = await _employeeProvider.GetEmployeesWithWorkDaysByDate(month == default(DateTime) ? DateTime.Now : month);
                if (getEmployeesResult.Succed)
                {
                    return View(getEmployeesResult.Data);
                }
            }
            return BadRequest();
        }

        
    }
   
}
