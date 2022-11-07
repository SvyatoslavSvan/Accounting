using Accounting.Domain.Models;

namespace Accounting.Domain.ViewModels
{
    public class CreateAccrualViewModel
    {
#nullable disable
        public List<AccrualNotBetEmployee> Accruals { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
