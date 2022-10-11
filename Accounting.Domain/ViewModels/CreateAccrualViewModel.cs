using Accounting.Domain.Models;

namespace Accounting.Domain.ViewModels
{
    public class CreateAccrualViewModel
    {
#nullable disable
        public List<Accrual> Accruals { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
