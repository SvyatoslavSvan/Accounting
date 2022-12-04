using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using Accouting.Domain.Managers.Interfaces.Base;

namespace Accouting.Domain.Managers.Interfaces
{
    public interface ITimesheetManager : IBaseCrudManager<Timesheet>
    {
        public Task<BaseResult<Timesheet>> CreateNew();
        public Task<BaseResult<Timesheet>> GetByDate(DateTime date);
        public Task<BaseResult<Timesheet>> UpdateWorkDaysForEmployee(float value, Guid employeeId, Guid timesheetId);
    }
}
