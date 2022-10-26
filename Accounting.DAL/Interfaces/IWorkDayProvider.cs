using Accounting.DAL.Interfaces.Base;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;

namespace Accounting.DAL.Interfaces
{
    public interface IWorkDayProvider : IBaseProvider<WorkDay>
    {
        public Task<BaseResult<bool>> CreateNewWorkDays();
        public Task<BaseResult<IList<WorkDay>>> GetAllFromThisMonth();
    }
}
