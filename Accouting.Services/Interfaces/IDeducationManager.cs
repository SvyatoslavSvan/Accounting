using Accouting.Domain.Managers.Interfaces.Base;
using Accounting.Domain.Models.Base;
using Accounting.DAL.Result.Provider.Base;

namespace Accouting.Domain.Managers.Interfaces
{
    public interface IDeducationManager : IBaseCrudManager<DeducationBase>
    {
        public Task<BaseResult<IList<DeducationBase>>> DeleteDeducations(List<DeducationBase> deducations);
    }
}
