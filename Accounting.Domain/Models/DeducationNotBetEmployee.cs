using Accounting.Domain.Models.Base;
using System.Text.Json.Serialization;

namespace Accounting.Domain.Models
{
    public class DeducationNotBetEmployee : DeducationBase
    {
        public override void ToSerializable()
        {
            base.ToSerializable();
            Employee = null;
        }
        public DeducationNotBetEmployee() {}

        [JsonConstructor]
        public DeducationNotBetEmployee(Guid id, decimal ammount, bool isAdditional, Guid employeeId) : base(ammount, isAdditional)
        {
            Id = id;
            EmployeeId = employeeId;
        }

        public DeducationNotBetEmployee(decimal ammount, bool isAdditional , NotBetEmployee employee) : base(ammount, isAdditional) => Employee = employee;
        public NotBetEmployee Employee { get; private set; }
        public Guid EmployeeId { get; private set; }
    }
}
