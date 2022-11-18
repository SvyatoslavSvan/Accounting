using Accounting.Domain.Models.Base;
using System.Text.Json.Serialization;

namespace Accounting.Domain.Models
{
    public class NotBetEmployee : EmployeeBase
    {
#nullable disable
        [JsonConstructor]
        public NotBetEmployee(Guid id, string name) : base(id, name) { }
        private ICollection<PayoutNotBetEmployee> _accruals;
        [JsonIgnore]
        public ICollection<PayoutNotBetEmployee> Accruals
        {
            get { return _accruals; }
            set { _accruals = value; }
        }

        public NotBetEmployee(string name, string innerId, int premium) : base(name, innerId, premium)
        {
            
        }

        public NotBetEmployee(Guid id, Group group, string name, string innerId, int premium) : base(id, group ,name, innerId, premium)
        {

        }

        public override Salary CalculateSalary(DateTime from, DateTime to)
        {
            return base.CalculateSalary(from, to);
        }
    }
}
