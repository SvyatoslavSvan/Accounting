using Accounting.Domain.Models.Base;

namespace Accounting.ViewModels
{
    public class AddedEmployeeViewModel
    {
        public EmployeeBase Employee { get; set; }
        public int CountInSessionDocument { get; set; }
    }
}
