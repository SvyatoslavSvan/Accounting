using Accounting.Domain.Models;

namespace Accounting.ViewModels
{
    public class TimesheetViewModel
    {
        public Guid Id { get; set; }
        public IList<WorkDay> WorkDaysToHeader { get; set; }
        public IList<BetEmployee> Employees { get; set; }
        public DateTime Date { get; set; }
        public int DaysCount { get; set; }
        public float HoursCount { get; set; }
    }
}
