using Accounting.Domain.Models.Base;
using System.Text.Json.Serialization;

namespace Accounting.Domain.Models
{
    public class PayoutNotBetEmployee : PayoutBase
    {
#nullable disable
        public PayoutNotBetEmployee() { }
        public PayoutNotBetEmployee(decimal ammount, bool isAdditional, NotBetEmployee employee) : base(ammount, isAdditional) 
        {
            Employee = employee;
        }
        [JsonConstructor]
        public PayoutNotBetEmployee(decimal ammount, Guid id, bool isAdditional) : base(ammount, id, isAdditional) { }

        public Guid EmployeeId { get; private set; }
        private NotBetEmployee _employee = null!;
        [JsonIgnore]
        public NotBetEmployee Employee
        {
            get => _employee;
            set { _employee = value ?? throw new ArgumentNullException(); }
        }


    }
}
