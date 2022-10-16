using Accounting.DAL.Interfaces.Base;
using Accounting.Domain.Models;
using System.Linq.Expressions;

namespace Accounting.DAL.Interfaces
{
    public interface IDocumentRepository : IBaseRepository<Document>
    {
        public Task<IEnumerable<Document>> GetSearchDocumentsByPredicate(Expression<Func<Document, bool>> queryWhere);       
    }
}
