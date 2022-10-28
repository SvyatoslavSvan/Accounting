using Accounting.DAL.Interfaces;
using Accounting.Domain.Models;
using Accounting.Services.Interfaces;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Accounting.Services
{
    public class ReportService : IReportService
    {
        private readonly IEmployeeProvider _employeeProvider;
        public ReportService(IEmployeeProvider employeeProvider)
        {
            _employeeProvider = employeeProvider;
        }

        public async Task<byte[]> MakeReportAsExcel()
        {
            using var package = new ExcelPackage();
            var sheet = package.Workbook.Worksheets.Add("Salary Report");
            sheet = MakeHead(sheet);
            return await package.GetAsByteArrayAsync();
        }
        private ExcelWorksheet MakeHead(ExcelWorksheet sheet)
        {
            sheet.Cells["A1:A2"].Merge = true;
            sheet.Cells["A1:A2"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            sheet.Cells["A1:A2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sheet.Cells["A1:A2"].Value = "Таб№";
            sheet.Cells["B1:E2"].Merge = true;
            sheet.Cells["B1:E2"].Value = "Працівники";
            sheet.Cells["B1:E2"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            sheet.Cells["B1:E2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            return sheet;
        }
    }
}
