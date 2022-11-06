using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using Accounting.Domain.Models.Base;
using Accouting.Domain.Managers.Interfaces.Base;

namespace Accouting.Domain.Managers.Interfaces
{
    public interface IEmployeeManager : IBaseCrudManager<EmployeeBase>
    {
        public Task<BaseResult<NotBetEmployee>> GetNotBetEmployee(Guid id);
        public Task<BaseResult<IEnumerable<NotBetEmployee>>> GetNotBetEmployees();
        public Task<BaseResult<IEnumerable<NotBetEmployee>>> GetNotBetEmployeesIncludeDocument();
        public Task<BaseResult<BetEmployee>> GetBetEmployee(Guid id);
        public Task<BaseResult<IList<BetEmployee>>> GetBetEmployees();
        public Task<BaseResult<IList<BetEmployee>>> GetEmployeesWithWorkDaysByDate(DateTime date);
    }
}
