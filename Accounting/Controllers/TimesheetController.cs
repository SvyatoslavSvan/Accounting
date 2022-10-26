using Accounting.DAL.Interfaces;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.Controllers
{
    public class TimesheetController : Controller
    {
        private readonly IWorkDayProvider _workDayProvider;
        public TimesheetController(IWorkDayProvider workDayProvider)
        {
            _workDayProvider = workDayProvider;
        }

        public async Task<IActionResult> Timesheet()
        {
            var createResult = await _workDayProvider.CreateNewWorkDays();
            var workDays = await _workDayProvider.GetAllFromThisMonth();
            return View(workDays.Data);
        }

        
    }
   
}
