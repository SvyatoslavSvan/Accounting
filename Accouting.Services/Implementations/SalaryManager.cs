using Accounting.Domain.Models;
using Accounting.Domain.Models.Base;
using Accouting.Domain.Managers.Interfaces;

namespace Accouting.Domain.Managers.Implementations
{
    public class SalaryManager : ISalaryManager
    {
        private readonly IEmployeeManager _employeeManager;

        public SalaryManager(IEmployeeManager employeeManager)
        {
            _employeeManager = employeeManager;
        }

        public async Task<IList<Salary>> CalculateSalaries(DateTime from, DateTime to) => GetSalaries(_employeeManager.GetEmployeeWithSalaryPropertiesByPeriod(from, to).Result.Data);


        private IList<Salary> GetSalaries(IList<EmployeeBase> employees)
        {
            var salaries = new List<Salary>();
            foreach (var item in employees)
            {
                salaries.Add(item.CalculateSalary());
            }
            return salaries;
        }

        //private decimal GetBetPayment(IList<WorkDay> workDays, decimal bet)
        //{
        //    decimal payment = 0;
        //    var payForHour = (bet / workDays.Count()) / 8;
        //    foreach (var item in workDays)
        //        payment += (decimal)item.Hours * payForHour;
        //    return payment;
        //}

    }
}
