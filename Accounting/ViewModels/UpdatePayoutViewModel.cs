using Accounting.ViewModels;

namespace Accounting.Domain.ViewModels
{
    public class UpdatePayoutViewModel : AddedEmployeeViewModel
    {
        public bool IsAdditional { get; set; }
        public decimal Ammount { get; set; }
        public Guid PayoutId { get; set; }
    }
}
