using Accounting.Domain.Models.Base;
using Accounting.Domain.ViewModels;

namespace Accounting.Domain.Models
{
    public class NotBetEmployee : EmployeeBase
    {
#nullable disable
        public List<Accrual> Accruals { get; private set; } 
        public List<Document> Documents { get; private set; } 
        public NotBetEmployee(string name, string innerId) : base(name, innerId)
        {
            
        }
        public NotBetEmployee(Guid id, Group group, string name, string innerId) : base(id, group ,name, innerId)
        {

        }
        public void Update(UpdateEmployeeViewModel viewModel, Group group)
        {
            Name = viewModel.Name; 
            Group = group;
            InnerId = viewModel.InnerId;
        }
        public override decimal CalculateSalary(DateTime from)
        {
            throw new NotImplementedException();
        }
    }
}
