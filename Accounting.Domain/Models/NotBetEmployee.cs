using Accounting.Domain.Models.Base;
using Accounting.Domain.ViewModels;

namespace Accounting.Domain.Models
{
    public class NotBetEmployee : EmployeeBase
    {
#nullable disable
        public List<Accrual> Accruals { get; private set; } 
        public List<Document> Documents { get; private set; }
        public NotBetEmployee(string name, string innerId) : base(name, innerId, 0)
        {

        }
        public NotBetEmployee(string name, string innerId, int premium) : base(name, innerId, premium)
        {
            
        }

        public NotBetEmployee(Guid id, Group group, string name, string innerId, int premium) : base(id, group ,name, innerId, premium)
        {

        }
        private ICollection<DeducationDocument> _deducationDocuments;

        public ICollection<DeducationDocument> DeducationDocuments
        {
            get => _deducationDocuments;
            set { _deducationDocuments = value; }
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
