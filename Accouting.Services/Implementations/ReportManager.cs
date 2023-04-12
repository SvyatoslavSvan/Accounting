using Accounting.Domain.Models;
using Accouting.Domain.Managers.Interfaces;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Accouting.Domain.Managers.Implementations
{
    public class ReportManager : IReportManager
    {
        private readonly ISalaryManager _salaryManager;
        private readonly IGroupManager _groupManager;

        public ReportManager(ISalaryManager salaryManager, IGroupManager groupManager)
        {
            _salaryManager = salaryManager;
            _groupManager = groupManager;
        }

        public async Task<byte[]> GetReportAsExcel(DateTime from, DateTime to)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using var package = new ExcelPackage();
            var sheet = package.Workbook.Worksheets.Add("Report");
            sheet = MakeHead(sheet, from, to);
            var salaries = _salaryManager.CalculateSalaries(from, to);
            sheet = await MakeRows(sheet, salaries);
            return await package.GetAsByteArrayAsync();
        }

        private async Task<ExcelWorksheet> MakeRows(ExcelWorksheet sheet, IList<Salary> salaries)
        {
            var column = 1;
            var row = 5;
            var groups = await _groupManager.GetAll();
            foreach (var group in groups.Data)
            {
                MakeGroupRow(group.Name , $"A{row}:J{row}", sheet);
                row++;
                var salariesWithGroup = salaries.Where(x => x.Employee.Group.Id == group.Id).ToList();
                if (salariesWithGroup.Count() > 0)
                {
                    foreach (var salary in salariesWithGroup)
                    {
                        sheet.Cells[row, column].Value = salary.Employee.InnerId;
                        sheet.Cells[row, column + 1].Value = salary.Employee.Name;
                        sheet.Cells[row, column + 2].Value = Math.Round(salary.Payment, 2, MidpointRounding.AwayFromZero);
                        sheet.Cells[row, column + 3].Value = Math.Round(salary.Premium, 2, MidpointRounding.AwayFromZero);
                        sheet.Cells[row, column + 4].Value = Math.Round(salary.AdditionalPayout, 2, MidpointRounding.AwayFromZero);
                        sheet.Cells[row, column + 5].Value = Math.Round(salary.TotalAmmount, 2, MidpointRounding.AwayFromZero);
                        sheet.Cells[row, column + 6].Value = Math.Round(salary.Deducation, 2, MidpointRounding.AwayFromZero);
                        sheet.Cells[row, column + 7].Value = Math.Round(salary.AdditionalDeducation, 2, MidpointRounding.AwayFromZero);
                        sheet.Cells[row, column + 8].Value = Math.Round(salary.TotalDeducation, 2, MidpointRounding.AwayFromZero);
                        sheet.Cells[row, column + 9].Value = Math.Round(salary.Total, 2, MidpointRounding.AwayFromZero);
                        row++;
                    }
                }
            }
            return sheet;
        }

        private ExcelWorksheet MakeGroupRow(string name, string address, ExcelWorksheet sheet)
        {
            sheet.Cells[address].Merge = true;
            sheet.Cells[address].Value = name;
            AlingValue(address, sheet);
            return sheet;
        }

        private ExcelWorksheet MakeHead(ExcelWorksheet sheet, DateTime from, DateTime to)
        {
            const string title = "Зведена відомість за період";
            string subTitle = $"За період з {from.Day} {GetMonth(from.Month)} {from.Year} р. по {to.Day} {GetMonth(to.Month)} {to.Year} р.";
            sheet.Column(2).Width = 80;
            sheet.Cells["A1:J1"].Merge = true;
            sheet.Cells["A1:J1"].Value = title;
            AlingValue("A1:J1", sheet);
            sheet.Cells["A2:J2"].Merge = true;
            sheet.Cells["A2:J2"].Value = subTitle;
            AlingValue("A2:J2", sheet);
            sheet.Cells["A3:A4"].Merge = true;
            sheet.Cells["A3:A4"].Value = "таб №";
            AlingValue("A3:A4", sheet);
            sheet.Cells["B3:B4"].Merge = true;
            sheet.Cells["B3:B4"].Value = "Працівник";
            AlingValue("B3:B4", sheet);
            sheet.Cells["C3:F3"].Merge = true;
            sheet.Cells["C3:F3"].Value = "Начислення";
            AlingValue("C3:F3", sheet);
            sheet.Cells["C4"].Value = "Оплата";
            AlingValue("C4", sheet);
            sheet.Cells["D4"].Value = "Премія";
            AlingValue("D4", sheet);
            sheet.Cells["E4"].Value = "Відп/боль";
            AlingValue("E4", sheet);
            sheet.Cells["F4"].Value = "Усього";
            AlingValue("F4", sheet);
            sheet.Cells["G3:I3"].Merge = true;
            sheet.Cells["G3:I3"].Value = "Утримано";
            AlingValue("G3:I3", sheet);
            sheet.Cells["G4"].Value = "Податки";
            AlingValue("G4", sheet);
            sheet.Cells["H4"].Value = "Карточка";
            AlingValue("H4", sheet);
            sheet.Cells["I4"].Value = "Усього";
            AlingValue("I4", sheet);
            sheet.Cells["J3:J4"].Merge = true;
            sheet.Cells["J3:J4"].Value = "На руки";
            AlingValue("J3:J4", sheet);
            return sheet;
        }

        private void AlingValue(string address, ExcelWorksheet sheet)
        {
            sheet.Cells[address].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sheet.Cells[address].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
        }

        private string GetMonth(int month)
        {
            switch (month)
            {
                case 1:
                    return "Січень";
                case 2:
                    return "Лютий";
                case 3:
                    return "Березень";
                case 4:
                    return "Квітень";
                case 5:
                    return "Травень";
                case 6:
                    return "Червень";
                case 7:
                    return "Липень";
                case 8:
                    return "Серпень";
                case 9:
                    return "Вересень";
                case 10:
                    return "Жовтень";
                case 11:
                    return "Листопад";
                case 12:
                    return "Грудень";
                default:
                    return string.Empty;
            }
        }
    }
}
