using Accounting.ViewModels;
using Accouting.Domain.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReportManager _reportManager;
        private readonly ISalaryManager _salaryManager;
        private readonly IGroupManager _groupManager;
        public ReportController(IReportManager reportManager, ISalaryManager salaryManager, IGroupManager groupManager)
        {
            _reportManager = reportManager;
            _salaryManager = salaryManager;
            _groupManager = groupManager;
        }
        public async Task<IActionResult> GetReport(DateTime from, DateTime to)
        {
            var salaries = await _salaryManager.CalculateSalaries(from, to);
            var groups = await _groupManager.GetAll();
            return View(new ReportViewModel()
            {
                Salaries = salaries,
                From = from,
                To = to,
                Groups = groups.Data
            });
        }
        public async Task<IActionResult> DownloadReport(DateTime from, DateTime to)
        {
            var report = await _reportManager.GetReportAsExcel(from, to);
            return File(report, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Книга.xlsx");
        }

    }
}
