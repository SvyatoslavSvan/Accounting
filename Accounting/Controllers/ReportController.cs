using Accounting.ViewModels;
using Accouting.Domain.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReportManager _reportManager;
        private readonly ISalaryManager _salaryManager;
        public ReportController(IReportManager reportManager, ISalaryManager salaryManager)
        {
            _reportManager = reportManager;
            _salaryManager = salaryManager;
        }
        public async Task<IActionResult> GetReport(DateTime from, DateTime to)
        {
            var salaries = await _salaryManager.CalculateSalaries(from, to);
            return View(new ReportViewModel()
            {
                Salaries = salaries,
                From = from,
                To = to
            });
        }
        public async Task<IActionResult> DownloadReport(DateTime from, DateTime to)
        {
            var report = await _reportManager.GetReportAsExcel(from, to);
            return File(report, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

    }
}
