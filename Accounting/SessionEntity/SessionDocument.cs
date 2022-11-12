using Accounting.Domain.Models;
using Accounting.Domain.Models.Base;

namespace Accounting.SessionEntity
{
    public class SessionDocument
    {
        public List<NotBetEmployee> NotBetEmployees { get; set; } = new List<NotBetEmployee>();
        public List<BetEmployee> BetEmployees { get; set; } = new List<BetEmployee>();
        public List<PayoutBetEmployee> PayoutsBetEmployee { get; set; } = new List<PayoutBetEmployee>();
        public List<PayoutNotBetEmployee> PayoutsNotBetEmployee { get; set; } = new List<PayoutNotBetEmployee>();

        public void AddEmployee(EmployeeBase employeeBase)
        {
            if (employeeBase is BetEmployee betEmployee)
            {
                BetEmployees.Add(betEmployee);
            }
            if (employeeBase is NotBetEmployee notBetEmployee)
            {
                NotBetEmployees.Add(notBetEmployee);
            }
        }
        public void AddPayout(PayoutBase payoutBase)
        {
            if (payoutBase is PayoutBetEmployee payoutBetEmployee)
            {
                PayoutsBetEmployee.Add(payoutBetEmployee);
            }
            if (payoutBase is PayoutNotBetEmployee payoutNotBetEmployee)
            {
                PayoutsNotBetEmployee.Add(payoutNotBetEmployee);
            }
        }

        public List<PayoutBase> GetPayoutsByEmployeeId(Guid employeeId)
        {
            var payouts = new List<PayoutBase>();
            payouts.AddRange(PayoutsBetEmployee.Where(x => x.EmployeeId == employeeId));
            payouts.AddRange(PayoutsNotBetEmployee.Where(x => x.EmployeeId == employeeId));
            return payouts;
        }

        public void UpdatePayout(Guid payoutId, decimal ammount)
        {
            var payoutBetEmployee = PayoutsBetEmployee.FirstOrDefault(x => x.Id == payoutId);
            if (payoutBetEmployee == null)
            {
                var payoutNotBetEmployee = PayoutsNotBetEmployee.FirstOrDefault(x => x.Id == payoutId);
                payoutNotBetEmployee.Ammount = ammount;
                return;
            }
            payoutBetEmployee.Ammount = ammount;
        }

        public void DeleteAccrual(Guid payoutId)
        {
            PayoutsBetEmployee.RemoveAll(x => x.Id == payoutId);
            PayoutsNotBetEmployee.RemoveAll(x => x.Id == payoutId);
        }
    }
}
