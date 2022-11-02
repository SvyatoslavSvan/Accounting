using Accounting.DAL.Interfaces.Base;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models.Base;

namespace Accounting.DAL.Interfaces
{
    public interface IDeducationProvider : IBaseProvider<DeducationBase>
    {
        public Task<BaseResult<bool>> DeleteDeducations(List<DeducationBase> deducationBases);
    }
}
