using Accounting.Domain.Models.Base;

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
        public DeducationNotBetEmployee(decimal ammount, bool isAdditional , NotBetEmployee employee) : base(ammount, isAdditional) => Employee = employee;
        public NotBetEmployee Employee { get; private set; }
        public Guid EmployeeId { get; private set; }
    }
}
