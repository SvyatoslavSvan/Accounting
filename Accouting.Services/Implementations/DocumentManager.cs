using Accounting.DAL.Interfaces;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Enums;
using Accounting.Domain.Models;
using Accounting.Domain.Requests;
using Accouting.Domain.Managers.Interfaces;
using Calabonga.PredicatesBuilder;
using System.Linq.Expressions;

namespace Accouting.Domain.Managers.Implementations
{
    public class DocumentManager : IDocumentManager
    {
        private readonly IDocumentProvider _provider;
        private readonly IPayoutManager _payoutManager;
        public DocumentManager(IDocumentProvider provider, IPayoutManager payoutManager)
        {
            _provider = provider;
            _payoutManager = payoutManager;
        }
        public async Task<BaseResult<Document>> Create(Document model)
        {
            var createResult = await _provider.Create(model);
            return new BaseResult<Document>(createResult.Succed, model, createResult.OperationStatus);
        }

        public async Task<BaseResult<bool>> Delete(Guid id)
        {
            var deletePayoutsTiedToDocument = await _payoutManager.DeleteByDocumentId(id);
            if (deletePayoutsTiedToDocument.Succed)
            {
                return await _provider.Delete(id);
            }
            else
            {
                return deletePayoutsTiedToDocument;
            }
        }
        

        public async Task<BaseResult<IList<Document>>> GetAll()
        {
            var getAllResult = await _provider.GetAll();
            return new BaseResult<IList<Document>>(getAllResult.Succed, getAllResult.Data, getAllResult.OperationStatus);
        }

        public async Task<BaseResult<Document>> GetById(Guid id) => await _provider.GetById(id);
       

        public Task<BaseResult<IList<Document>>> GetSearch(DocumentSearchRequest request) => _provider.GetAllByPredicate(BuildSearchPredicate(request));    
         

        private Expression<Func<Document, bool>> BuildSearchPredicate(DocumentSearchRequest request)
        {
            var predicate = PredicateBuilder.True<Document>();
            if (request.From != default(DateTime))
                predicate = predicate.And(x => x.DateCreate >= request.From);
            if (request.To != default(DateTime))
                predicate = predicate.And(x => x.DateCreate <= request.To);
            if (!string.IsNullOrWhiteSpace(request.Name))
                predicate = predicate.And(x => x.Name.Contains(request.Name));
            if (request.DateCreate != default(DateTime) && request.From == default(DateTime) && request.To == default(DateTime))
                predicate = predicate.And(x => x.DateCreate == request.DateCreate);
            if (request.DocumentType == DocumentType.Accrual)
                predicate = predicate.And(x => x.DocumentType == DocumentType.Accrual);
            if (request.DocumentType == DocumentType.Deducation)
                predicate = predicate.And(x => x.DocumentType == DocumentType.Deducation);
            return predicate;
        }

        public async Task<BaseResult<Document>> Update(Document model)
        {
            var updateResult = await _provider.Update(model);
            return new BaseResult<Document>(updateResult.Succed, model, updateResult.OperationStatus);
        }        
    }
}
