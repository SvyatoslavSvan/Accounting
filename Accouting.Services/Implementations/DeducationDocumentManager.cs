using Accounting.DAL.Interfaces;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using Accounting.Domain.Requests;
using Accouting.Domain.Managers.Interfaces;
using Calabonga.PredicatesBuilder;
using System.Linq.Expressions;

namespace Accouting.Domain.Managers.Implementations
{
    public class DeducationDocumentManager : IDeducationDocumentManager
    {
        private IDeducationDocumentProvider _deducationDocumentProvider;

        public DeducationDocumentManager(IDeducationDocumentProvider deducationDocumentProvider)
        {
            _deducationDocumentProvider = deducationDocumentProvider;
        }

        public async Task<BaseResult<DeducationDocument>> Create(DeducationDocument model)
        {
            var createResult = await _deducationDocumentProvider.Create(model);
            return new BaseResult<DeducationDocument>(createResult.Succed, model, createResult.OperationStatus);
        }

        public async Task<BaseResult<bool>> Delete(Guid id)
        {
            var deleteDeducationsResult = await _deducationDocumentProvider.Delete(id);
            return new BaseResult<bool>(deleteDeducationsResult.Succed,deleteDeducationsResult.Data, deleteDeducationsResult.OperationStatus);
        }

        

        public async Task<BaseResult<IList<DeducationDocument>>> GetAll()
        {
            var getAllResult = await _deducationDocumentProvider.GetAll();
            return new BaseResult<IList<DeducationDocument>>(getAllResult.Succed, getAllResult.Data, getAllResult.OperationStatus);
        }

        public Task<BaseResult<List<DeducationDocument>>> GetAll(Expression<Func<DeducationDocument, bool>> predicate = null)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResult<DeducationDocument>> GetById(Guid id)
        {
            var document = await _deducationDocumentProvider.GetById(id);
            return new BaseResult<DeducationDocument>(document.Succed, document.Data, document.OperationStatus);
        }

        public async Task<BaseResult<IList<DeducationDocument>>> GetSearch(DocumentSearchRequest request)
        {
            var predicate = BuildSearchPredicate(request);
            var getSearchResult = await _deducationDocumentProvider.GetAllByPredicate(predicate);
            return new BaseResult<IList<DeducationDocument>>(getSearchResult.Succed, getSearchResult.Data, getSearchResult.OperationStatus);
        }

        private Expression<Func<DeducationDocument, bool>> BuildSearchPredicate(DocumentSearchRequest request)
        {
            var predicate = PredicateBuilder.True<DeducationDocument>();
            if (request.From != default(DateTime))
                predicate = predicate.And(x => x.DateCreate >= request.From);
            if (request.To != default(DateTime))
                predicate = predicate.And(x => x.DateCreate <= request.To);
            if (!string.IsNullOrWhiteSpace(request.Name))
                predicate = predicate.And(x => x.Name.Contains(request.Name));
            if (request.DateCreate != default(DateTime) && request.From == default(DateTime) && request.To == default(DateTime))
                predicate = predicate.And(x => x.DateCreate == request.DateCreate);
            return predicate;
        }

        public async Task<BaseResult<DeducationDocument>> Update(DeducationDocument model)
        {
            var document = await UpdateFields(model);
            var updateResult = await _deducationDocumentProvider.Update(document);
            return new BaseResult<DeducationDocument>(updateResult.Succed, document, updateResult.OperationStatus);
        }

       
        private async Task<DeducationDocument> UpdateFields(DeducationDocument model)
        {
            var getDocumentResult = await _deducationDocumentProvider.GetById(model.Id);
            getDocumentResult.Data.Name = model.Name;
            getDocumentResult.Data.DateCreate = model.DateCreate;
            getDocumentResult.Data.DeducationsNotBetEmployee = model.DeducationsNotBetEmployee;
            getDocumentResult.Data.DeducationsBetEmployee = model.DeducationsBetEmployee;
            getDocumentResult.Data.BetEmployees = model.BetEmployees;
            getDocumentResult.Data.NotBetEmployees = model.NotBetEmployees;
            return getDocumentResult.Data;
        }
    }
}
