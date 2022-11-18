using Accounting.Domain.Models.Base;
using System.Text.Json.Serialization;

namespace Accounting.Domain.Models
{
    public class PayoutBetEmployee : PayoutBase
    {
		private BetEmployee _betEmployee = null!;
        public PayoutBetEmployee() { }
        [JsonConstructor]
        public PayoutBetEmployee(Guid id,decimal ammount, bool isAdditional, Guid employeeId) : base(ammount, isAdditional) 
        {
            EmployeeId = employeeId;
            Id = id;
        }
        public PayoutBetEmployee(decimal ammount, bool isAdditional, BetEmployee employee) : base(ammount, isAdditional)
        {
            Employee = employee;
        }
        [JsonIgnore]
        public BetEmployee Employee
		{
			get => _betEmployee;
			set { _betEmployee = value ?? throw new ArgumentNullException(); }
		}

	}
}
