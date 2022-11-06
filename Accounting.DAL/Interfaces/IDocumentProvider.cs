using Accounting.DAL.Interfaces.Base;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using System.Linq.Expressions;

namespace Accounting.DAL.Interfaces
{
    public interface IDocumentProvider : IBaseProvider<Document>
    {
        public Task<BaseResult<IList<Document>>> GetAllByPredicate(Expression<Func<Document, bool>> predicate);
    }
}
