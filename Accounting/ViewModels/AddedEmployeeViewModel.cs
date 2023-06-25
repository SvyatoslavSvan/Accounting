using Accounting.Domain.Models.Base;

namespace Accounting.ViewModels
{
    public class AddedEmployeeViewModel
    {
        public Employee Employee { get; set; }
        public int CountInSessionDocument { get; set; }
        public Payout Payout { get; set; }
    }
}
