using Accounting.DAL.Interfaces;
using Accounting.Domain.Models;
using Accounting.Services.Interfaces;
using OfficeOpenXml;

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
            sheet.Cells["A1:A2"].Merge = true;
            sheet.Cells["A1:A2"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells["A1:A2"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells["A1:A2"].Value = "Таб№";
            return await package.GetAsByteArrayAsync();
        }
    }
}
