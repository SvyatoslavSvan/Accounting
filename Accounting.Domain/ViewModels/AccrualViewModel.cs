
namespace Accounting.Domain.ViewModels
{
    public class AccrualViewModel
    {
        public Guid EmployeeId { get; set; }
        public bool IsAdditional { get; set; }
        public decimal Ammount { get; set; }
    }
}
