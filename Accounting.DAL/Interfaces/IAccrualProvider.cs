using Accounting.DAL.Interfaces.Base;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;

namespace Accounting.DAL.Interfaces
{
    public interface IAccrualProvider : IBaseProvider<Accrual>
    {
        public Task<BaseResult<bool>> DeleteRange(List<Accrual> accruals);
        public Task<BaseResult<bool>> DeleteRangeByDocumentId(Guid documentId);
    }
}
