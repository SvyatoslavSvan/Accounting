using Accounting.Domain.Models.Base;
using Accounting.Domain.ViewModels;
using System.Text.Json.Serialization;

namespace Accounting.Domain.Models
{
    public class NotBetEmployee : EmployeeBase
    {
#nullable disable
        [JsonConstructor]
        public NotBetEmployee(Guid id, string name) : base(id, name) { }
        public List<Accrual> Accruals { get; private set; } 
        public List<Document> Documents { get; private set; }

        private ICollection<DeducationNotBetEmployee> _deducationNotBetEmployee;

        public ICollection<DeducationNotBetEmployee> DeducationNotBetEmployee
        {
            get => _deducationNotBetEmployee;
            set { _deducationNotBetEmployee = value; }
        }
        public NotBetEmployee(string name, string innerId, int premium) : base(name, innerId, premium)
        {
            
        }

        public NotBetEmployee(Guid id, Group group, string name, string innerId, int premium) : base(id, group ,name, innerId, premium)
        {

        }

        public override void ToSerializable()
        {
            base.ToSerializable();
            this.Accruals = null;
            this.Documents = null;
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
