using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using Accounting.Domain.Requests;
using Accouting.Domain.Managers.Interfaces.Base;

namespace Accouting.Domain.Managers.Interfaces
{
    public interface IDeducationDocumentManager : IBaseCrudManager<DeducationDocument>
    {
        public Task<BaseResult<IList<DeducationDocument>>> GetSearch(DeducationDocumentSearchRequest request);
    }
}
