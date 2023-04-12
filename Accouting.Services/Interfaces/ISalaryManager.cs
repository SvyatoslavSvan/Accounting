using Accounting.Domain.Models;

namespace Accouting.Domain.Managers.Interfaces
{
    public interface ISalaryManager
    {
        public IList<Salary> CalculateSalaries(DateTime from, DateTime to);
    }
}
