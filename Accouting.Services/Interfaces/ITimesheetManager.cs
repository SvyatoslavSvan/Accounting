using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using Accouting.Domain.Managers.Interfaces.Base;

namespace Accouting.Domain.Managers.Interfaces
{
    public interface ITimesheetManager : IBaseCrudManager<Timesheet>
    {
        public Task<BaseResult<Timesheet>> CreateNew();
    }
}
