using Accounting.Domain.Models.Base;

namespace Accounting.Domain.Models
{
    public class AccrualBetEmployee : AccrualBase
    {
        public Guid EmployeeId { get; private set; }
		private BetEmployee _betEmployee = null!;	

		public BetEmployee BetEmployee
		{
			get => _betEmployee;
			set { _betEmployee = value ?? throw new ArgumentNullException(); }
		}

	}
}
