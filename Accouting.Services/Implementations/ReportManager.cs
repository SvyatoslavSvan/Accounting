using Accounting.Domain.Models;
using Accouting.Domain.Managers.Interfaces;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Accouting.Domain.Managers.Implementations
{
    public class ReportManager : IReportManager
    {
        public async Task<byte[]> GetReportAsExcel(DateTime from, DateTime to)
        {
            using var package = new ExcelPackage();
            var sheet = package.Workbook.Worksheets.Add("Report");
            sheet = MakeHead(sheet, from, to);
            return await package.GetAsByteArrayAsync();
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

        public IList<Salary> GetReportAsModel()
        {
            throw new NotImplementedException();
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
