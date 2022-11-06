using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using Accounting.Domain.Requests;
using Accouting.Domain.Managers.Interfaces.Base;

namespace Accouting.Domain.Managers.Interfaces
{
    public interface IDocumentManager : IBaseCrudManager<Document>
    {
        public Task<BaseResult<IList<Document>>> GetSearch(DocumentSearchRequest request);
    }
}
