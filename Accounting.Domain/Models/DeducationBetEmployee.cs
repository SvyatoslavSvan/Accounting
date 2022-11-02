using Accounting.Domain.Models.Base;
using System.Text.Json.Serialization;

namespace Accounting.Domain.Models
{
    public class DeducationBetEmployee : DeducationBase
    {
        public override void ToSerializable()
        {
            base.ToSerializable();
            Employee = null;
        }
        [JsonConstructor]
        public DeducationBetEmployee(Guid id, decimal ammount, bool isAdditional, Guid employeeId) : base(ammount, isAdditional)
        {
            Id = id;
            EmployeeId = employeeId;
        }
        public DeducationBetEmployee() {}
        public DeducationBetEmployee(decimal ammount,bool isAdditional, BetEmployee employee) : base(ammount, isAdditional) => Employee = employee;        
        public BetEmployee Employee { get; private set; }
        public Guid EmployeeId { get; private set; }
    }
}
