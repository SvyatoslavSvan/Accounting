using Accounting.DAL.Interfaces.Base;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;

namespace Accounting.DAL.Interfaces
{
    public interface IDocumentProvider : IBaseProvider<Document>
    {
        public Task<BaseResult<IEnumerable<Document>>> GetBySearchDocuments(DateTime from, DateTime to, string name, DateTime dateCreate);
    }
}
