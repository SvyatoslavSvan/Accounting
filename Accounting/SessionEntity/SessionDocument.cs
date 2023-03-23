using Accounting.Domain.Models;
using Accounting.Domain.Models.Base;
using System.Text.Json.Serialization;

namespace Accounting.SessionEntity
{
    public class SessionDocument
    {
        public List<NotBetEmployee> NotBetEmployees { get; set; } = new List<NotBetEmployee>();
        public List<BetEmployee> BetEmployees { get; set; } = new List<BetEmployee>();
        public List<PayoutBetEmployee> PayoutsBetEmployee { get; set; } = new List<PayoutBetEmployee>();
        public List<PayoutNotBetEmployee> PayoutsNotBetEmployee { get; set; } = new List<PayoutNotBetEmployee>();

        [JsonIgnore]
        public IList<EmployeeBase> Employees
        {
            get
            {
                var employees = new List<EmployeeBase>(NotBetEmployees);
                employees.AddRange(BetEmployees);
                return employees;
            }
        }

        [JsonIgnore]
        public IList<PayoutBase> Payouts
        {
            get
            {
                var payouts = new List<PayoutBase>(PayoutsNotBetEmployee);
                payouts.AddRange(PayoutsBetEmployee);
                return payouts;
            }
        }

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
            return;
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

        public void AddPayouts(IList<PayoutBase> payouts)
        {
            foreach (var item in payouts)
            {
                if (item is PayoutBetEmployee payoutBetEmployee)
                {
                    PayoutsBetEmployee.Add(payoutBetEmployee);
                }
                if (item is PayoutNotBetEmployee payoutNotBetEmployee)
                {
                    PayoutsNotBetEmployee.Add(payoutNotBetEmployee);
                }
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

        public List<EmployeeBase> GetAllEmployees()
        {
            var employees = new List<EmployeeBase>(NotBetEmployees);
            employees.AddRange(BetEmployees);
            return employees;
        }


        public void DeleteAccrual(Guid payoutId)
        {
            PayoutsBetEmployee.RemoveAll(x => x.Id == payoutId);
            PayoutsNotBetEmployee.RemoveAll(x => x.Id == payoutId);
        }

        public void DeleteEmployee(Guid employeeId, Guid payoutId)
        {
            NotBetEmployees.Remove(NotBetEmployees.FirstOrDefault(x => x.Id == employeeId));
            BetEmployees.Remove(BetEmployees.FirstOrDefault(x => x.Id == employeeId));
            PayoutsBetEmployee.Remove(PayoutsBetEmployee.FirstOrDefault(x => x.Id == payoutId));
            PayoutsNotBetEmployee.Remove(PayoutsNotBetEmployee.FirstOrDefault(x => x.Id == payoutId));
        }
    }
}
