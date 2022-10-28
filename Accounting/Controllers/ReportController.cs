using Accounting.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReportService reportService;
        public ReportController(IReportService reportService)
        {
            this.reportService = reportService;
        }
        public async Task<IActionResult> SaveReport()
        {
            var report = await reportService.MakeReportAsExcel();
            return File(report, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report.xlsx");
        }
    }
}
