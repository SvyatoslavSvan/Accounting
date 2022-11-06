using Accounting.DAL.Interfaces.Base;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using System.Linq.Expressions;

namespace Accounting.DAL.Interfaces
{
    public interface IDeducationDocumentProvider : IBaseProvider<DeducationDocument>
    {
        public Task<BaseResult<IList<DeducationDocument>>> GetAllByPredicate(Expression<Func<DeducationDocument, bool>> predicate);
    }
}
