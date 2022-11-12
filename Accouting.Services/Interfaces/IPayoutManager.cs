using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using Accounting.Domain.Models.Base;
using Accouting.Domain.Managers.Interfaces.Base;

namespace Accouting.Domain.Managers.Interfaces
{
    public interface IPayoutManager : IBaseCrudManager<PayoutBase>
    {
        public Task<BaseResult<bool>> DeleteRange(List<PayoutNotBetEmployee> accruals);
        public Task<BaseResult<bool>> DeleteByDocumentId(Guid id);
    }
}
