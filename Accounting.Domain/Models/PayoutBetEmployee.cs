using Accounting.Domain.Models.Base;
using System.Text.Json.Serialization;

namespace Accounting.Domain.Models
{
    public class PayoutBetEmployee : PayoutBase
    {
        public Guid EmployeeId { get; private set; }
		private BetEmployee _betEmployee = null!;
        public PayoutBetEmployee() { }
        [JsonConstructor]
        public PayoutBetEmployee(decimal ammount, bool isAdditional, BetEmployee employee) : base(ammount, isAdditional)
        {
            BetEmployee = employee;
        }
        [JsonIgnore]
        public BetEmployee BetEmployee
		{
			get => _betEmployee;
			set { _betEmployee = value ?? throw new ArgumentNullException(); }
		}

	}
}
