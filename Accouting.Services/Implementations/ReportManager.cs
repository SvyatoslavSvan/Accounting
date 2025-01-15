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
        private IList<Salary> _salaries;
        private ExcelWorksheet _excelWorksheet;

        public ReportManager(ISalaryManager salaryManager, IGroupManager groupManager)
        {
            _salaryManager = salaryManager;
            _groupManager = groupManager;
        }

        public async Task<byte[]> GetReportAsExcel(DateTime from, DateTime to)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using var package = new ExcelPackage();
            _excelWorksheet = package.Workbook.Worksheets.Add("Report");
            _excelWorksheet = MakeHead(from, to);
            _salaries = await _salaryManager.CalculateSalariesAsync(from, to);
            await MakeRows();
            return await package.GetAsByteArrayAsync();
        }

        private async Task<ExcelWorksheet> MakeRows()
        {
            var column = 1;
            var row = 5;
            var groups = await _groupManager.GetAll();
            foreach (var group in groups.Data)
            {
                MakeGroupRow(group.Name , $"A{row}:J{row}");
                row++;
                var salariesWithGroup = _salaries.Where(x => x.Employee.Group.Id == group.Id).ToList();
                if (salariesWithGroup.Count() > 0)
                {
                    foreach (var salary in salariesWithGroup)
                    {
                        _excelWorksheet.Cells[row, column].Value = salary.Employee.InnerId;
                        _excelWorksheet.Cells[row, column + 1].Value = salary.Employee.Name;
                        _excelWorksheet.Cells[row, column + 2].Value = Math.Round(salary.Payment, 2, MidpointRounding.AwayFromZero);
                        _excelWorksheet.Cells[row, column + 3].Value = Math.Round(salary.Premium, 2, MidpointRounding.AwayFromZero);
                        _excelWorksheet.Cells[row, column + 4].Value = Math.Round(salary.AdditionalPayout, 2, MidpointRounding.AwayFromZero);
                        _excelWorksheet.Cells[row, column + 5].Value = Math.Round(salary.TotalAmmount, 2, MidpointRounding.AwayFromZero);
                        _excelWorksheet.Cells[row, column + 6].Value = Math.Round(salary.Deducation, 2, MidpointRounding.AwayFromZero);
                        _excelWorksheet.Cells[row, column + 7].Value = Math.Round(salary.AdditionalDeducation, 2, MidpointRounding.AwayFromZero);
                        _excelWorksheet.Cells[row, column + 8].Value = Math.Round(salary.TotalDeducation, 2, MidpointRounding.AwayFromZero);
                        _excelWorksheet.Cells[row, column + 9].Value = Math.Round(salary.Total, 2, MidpointRounding.AwayFromZero);
                        row++;
                    }
                }
            }
            MakeSumOfFields(row, column);
            return _excelWorksheet;
        }

        private void MakeSumOfFields(int row, int column)
        {
            var salaryTotal = new SalaryTotal(_salaries);
            _excelWorksheet.Cells[row, column].Value = "Итого";
            _excelWorksheet.Cells[row, column + 2].Value = Math.Round(salaryTotal.SumOfPayments, 2, MidpointRounding.AwayFromZero);
            _excelWorksheet.Cells[row, column + 3].Value = Math.Round(salaryTotal.SumOfPremium, 2, MidpointRounding.AwayFromZero);
            _excelWorksheet.Cells[row, column + 4].Value = Math.Round(salaryTotal.SumOfAdditionalPayments, 2, MidpointRounding.AwayFromZero);
            _excelWorksheet.Cells[row, column + 5].Value = Math.Round(salaryTotal.TotalSumOfPayments, 2, MidpointRounding.AwayFromZero);
            _excelWorksheet.Cells[row, column + 6].Value = Math.Round(salaryTotal.SumOfDeducations, 2, MidpointRounding.AwayFromZero);
            _excelWorksheet.Cells[row, column + 7].Value = Math.Round(salaryTotal.SumOfAdditionalDeducations, 2, MidpointRounding.AwayFromZero);
            _excelWorksheet.Cells[row, column + 8].Value = Math.Round(salaryTotal.TotalSumOfDeducation, 2, MidpointRounding.AwayFromZero);
            _excelWorksheet.Cells[row, column + 9].Value = Math.Round(salaryTotal.SumOfTotal, 2, MidpointRounding.AwayFromZero);
        }

        private ExcelWorksheet MakeGroupRow(string name, string address)
        {
            _excelWorksheet.Cells[address].Merge = true;
            _excelWorksheet.Cells[address].Value = name;
            AlingValue(address, _excelWorksheet);
            return _excelWorksheet;
        }

        private ExcelWorksheet MakeHead(DateTime from, DateTime to)
        {
            const string title = "Зведена відомість за період";
            string subTitle = $"За період з {from.Day} {GetMonth(from.Month)} {from.Year} р. по {to.Day} {GetMonth(to.Month)} {to.Year} р.";
            _excelWorksheet.Column(2).Width = 80;
            _excelWorksheet.Cells["A1:J1"].Merge = true;
            _excelWorksheet.Cells["A1:J1"].Value = title;
            AlingValue("A1:J1", _excelWorksheet);
            _excelWorksheet.Cells["A2:J2"].Merge = true;
            _excelWorksheet.Cells["A2:J2"].Value = subTitle;
            AlingValue("A2:J2", _excelWorksheet);
            _excelWorksheet.Cells["A3:A4"].Merge = true;
            _excelWorksheet.Cells["A3:A4"].Value = "таб №";
            AlingValue("A3:A4", _excelWorksheet);
            _excelWorksheet.Cells["B3:B4"].Merge = true;
            _excelWorksheet.Cells["B3:B4"].Value = "Працівник";
            AlingValue("B3:B4", _excelWorksheet);
            _excelWorksheet.Cells["C3:F3"].Merge = true;
            _excelWorksheet.Cells["C3:F3"].Value = "Начислення";
            AlingValue("C3:F3", _excelWorksheet);
            _excelWorksheet.Cells["C4"].Value = "Оплата";
            AlingValue("C4", _excelWorksheet);
            _excelWorksheet.Cells["D4"].Value = "Премія";
            AlingValue("D4", _excelWorksheet);
            _excelWorksheet.Cells["E4"].Value = "Відп/боль";
            AlingValue("E4", _excelWorksheet);
            _excelWorksheet.Cells["F4"].Value = "Усього";
            AlingValue("F4", _excelWorksheet);
            _excelWorksheet.Cells["G3:I3"].Merge = true;
            _excelWorksheet.Cells["G3:I3"].Value = "Утримано";
            AlingValue("G3:I3", _excelWorksheet);
            _excelWorksheet.Cells["G4"].Value = "Податки";
            AlingValue("G4", _excelWorksheet);
            _excelWorksheet.Cells["H4"].Value = "Карточка";
            AlingValue("H4", _excelWorksheet);
            _excelWorksheet.Cells["I4"].Value = "Усього";
            AlingValue("I4", _excelWorksheet);
            _excelWorksheet.Cells["J3:J4"].Merge = true;
            _excelWorksheet.Cells["J3:J4"].Value = "На руки";
            AlingValue("J3:J4", _excelWorksheet);
            return _excelWorksheet;
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
