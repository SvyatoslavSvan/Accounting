using Accounting.Domain.Models;
using Accounting.Domain.Models.Base;

namespace Accounting.Domain.ViewModels
{
    public class CreatePayoutViewModel
    {
#nullable disable
        public List<PayoutBase> Payouts { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
