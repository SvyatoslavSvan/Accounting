using Accounting.Domain.Models;

namespace Accounting.ViewModels
{
    public class TimesheetViewModel
    {
        public IList<WorkDay> WorkDaysToHeader { get; set; }
        public IList<BetEmployee> Employees { get; set; }
        public DateTime Date { get; set; }
    }
}
