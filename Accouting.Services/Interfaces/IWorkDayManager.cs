using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using Accouting.Domain.Managers.Interfaces.Base;

namespace Accouting.Domain.Managers.Interfaces
{
    public interface IWorkDayManager : IBaseCrudManager<WorkDay>
    {
        public Task<BaseResult<bool>> CreateNewWorkDays();
    }
}
