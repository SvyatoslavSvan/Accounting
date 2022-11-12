using Accounting.DAL.Interfaces;
using Accounting.DAL.Result.Provider.Base;
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
        public DocumentManager(IDocumentProvider provider)
        {
            _provider = provider;
        }
        public async Task<BaseResult<Document>> Create(Document model)
        {
            var createResult = await _provider.Create(model);
            return new BaseResult<Document>(createResult.Succed, model, createResult.OperationStatus);
        }

        public async Task<BaseResult<bool>> Delete(Guid id) => await _provider.Delete(id);
        

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
            return predicate;
        }

        public async Task<BaseResult<Document>> Update(Document model)
        {
            var getDocumentToUpdateResult = await _provider.GetById(model.Id);
            UpdateFieds(getDocumentToUpdateResult.Data, model);
            var updateResult = await _provider.Update(getDocumentToUpdateResult.Data);
            return new BaseResult<Document>(updateResult.Succed, model, updateResult.OperationStatus);
        }

        private void UpdateFieds(Document oldDocument, Document newDocument)
        {
            oldDocument.NotBetEmployees = newDocument.NotBetEmployees;
            oldDocument.PayoutsNotBetEmployees = newDocument.PayoutsNotBetEmployees;
            oldDocument.Name = newDocument.Name;
            oldDocument.DateCreate = newDocument.DateCreate;
        }     
    }
}
