using Accounting.Domain.Models;
using Accounting.Domain.Models.Base;
using Accouting.Domain.Managers.Interfaces;

namespace Accouting.Domain.Managers.Implementations
{
    public class SalaryManager : ISalaryManager
    {
        private readonly IEmployeeManager _employeeManager;

        public SalaryManager(IEmployeeManager employeeManager, IPayoutManager payoutManager)
        {
            _employeeManager = employeeManager;
        }

        public async Task<IList<Salary>> CalculateSalariesAsync(DateTime from, DateTime to)
        {
            var salaries = await _employeeManager.GetEmployeeWithSalaryPropertiesByPeriod(from, to);
            return GetSalaries(salaries.Data, from, to);
        }


        private IList<Salary> GetSalaries(IList<EmployeeBase> employees, DateTime from, DateTime to)
        {
            var salaries = new List<Salary>();
            foreach (var item in employees)
            {
                salaries.Add(item.CalculateSalary(from, to));
            }
            return salaries;
        }
    }
}
