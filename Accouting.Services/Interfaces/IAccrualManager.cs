using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using Accouting.Domain.Managers.Interfaces.Base;

namespace Accouting.Domain.Managers.Interfaces
{
    public interface IAccrualManager : IBaseCrudManager<Accrual>
    {
        public Task<BaseResult<bool>> DeleteRange(List<Accrual> accruals);
        public Task<BaseResult<bool>> DeleteByDocumentId(Guid id);
    }
}
