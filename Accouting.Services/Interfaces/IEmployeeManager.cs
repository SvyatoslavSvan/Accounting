using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using Accounting.Domain.Models.Base;
using Accounting.Domain.Requests;
using Accouting.Domain.Managers.Interfaces.Base;

namespace Accouting.Domain.Managers.Interfaces
{
    public interface IEmployeeManager : IBaseCrudManager<Employee>
    {
        public Task<BaseResult<Employee>> GetNotBetEmployee(Guid id);
        public Task<BaseResult<IEnumerable<Employee>>> GetNotBetEmployees();
        public Task<BaseResult<IEnumerable<Employee>>> GetNotBetEmployeesIncludeDocument();
        public Task<BaseResult<BetEmployee>> GetBetEmployee(Guid id);
        public Task<BaseResult<IList<BetEmployee>>> GetBetEmployees();
        public Task<BaseResult<IList<BetEmployee>>> GetEmployeesWithWorkDaysByDate(DateTime date);
        public Task<BaseResult<IList<Employee>>> GetEmployeeWithSalaryPropertiesByPeriod(DateTime from, DateTime to);
        public Task<BaseResult<IList<Employee>>> GetSearch(EmployeeSearchRequest request);
    }
}
