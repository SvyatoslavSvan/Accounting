using Accounting.Domain.Models;

namespace Accounting.ViewModels
{
    public class ReportViewModel
    {
        public IList<Salary> Salaries { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public IList<Group> Groups { get; set; }
    }
}
