using Accouting.Domain.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReportManager _reportManager;
        public ReportController(IReportManager reportManager)
        {
            _reportManager = reportManager;
        }
        public async Task<IActionResult> DownloadReport(DateTime from, DateTime to)
        {
            var report = await _reportManager.GetReportAsExcel(DateTime.Now, DateTime.Parse("18.12.2022"));
            return File(report, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
    }
}
