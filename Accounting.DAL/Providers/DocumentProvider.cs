using Accounting.DAL.Contexts;
using Accounting.DAL.Interfaces;
using Accounting.DAL.Providers.BaseProvider;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using Accounting.Domain.Models.Base;
using Calabonga.PredicatesBuilder;
using Calabonga.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Accounting.DAL.Providers
{
#nullable disable
    public class DocumentProvider : ProviderBase , IDocumentProvider
    {

        public DocumentProvider(IUnitOfWork<ApplicationDBContext> unitOfWork, ILogger<Document> logger) : base(unitOfWork, logger) { }
       

        public async Task<BaseResult<bool>> Create(Document entity)
        {
            _unitOfWork.DbContext.AttachRange(entity.Employees);
            _unitOfWork.DbContext.AttachRange(entity.Payouts);
            await DeletePayoutsByDocumentId(entity.Id);
            await _unitOfWork.GetRepository<Document>().InsertAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                return HandleException<bool>();
            }
            return new BaseResult<bool>(true, true, OperationStatuses.Ok);
        }

        public async Task<BaseResult<bool>> Delete(Guid id)
        {
            _unitOfWork.GetRepository<Document>().Delete(id);
            
            await _unitOfWork.SaveChangesAsync();
            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                return HandleException<bool>();
            }
            return new BaseResult<bool>(true, true, OperationStatuses.Ok);
        }

        private async Task DeletePayoutsByDocumentId(Guid id)  
        {
            var payoutRepository = _unitOfWork.GetRepository<Payout>();
            payoutRepository.Delete(await payoutRepository.GetAllAsync(predicate: x => x.Document.Id == id));
        }

        public async Task<BaseResult<List<Document>>> GetAll()
        {
            try
            {
                var documents = await _unitOfWork.GetRepository<Document>().GetAllAsync(
                  orderBy: x => x.OrderBy(x => x.DateCreate)
                 ,disableTracking: false);
                return new BaseResult<List<Document>>(true, documents.ToList(), OperationStatuses.Ok);
            }
            catch (Exception ex)
            {
                return HandleException<List<Document>>(ex);
            }
        }

        public async Task<BaseResult<IList<Document>>> GetAllByPredicate(Expression<Func<Document, bool>> predicate)
        {
            try
            {
                var documents = await _unitOfWork.GetRepository<Document>().GetAllAsync(
                  orderBy: x => x.OrderBy(x => x.DateCreate)
                 , disableTracking: false,
                  predicate: predicate,
                  include: x => x.Include(x => x.Payouts));
                return new BaseResult<IList<Document>>(true, documents, OperationStatuses.Ok);
            }
            catch (Exception ex)
            {
                return HandleException<IList<Document>>(ex);
            }
        }

        public async Task<BaseResult<Document>> GetById(Guid id)
        {
            try
            {
                return new BaseResult<Document>(true, await _unitOfWork.GetRepository<Document>().GetFirstOrDefaultAsync(
                    predicate: x => x.Id == id,
                    include: x => x.Include(x => x.Employees).Include(x => x.Payouts),
                    disableTracking: false 
                    ), OperationStatuses.Ok);
            }
            catch (Exception ex)
            {
                return HandleException<Document>(ex);
            }
        }

        public async Task<BaseResult<IEnumerable<Document>>> GetBySearchDocuments(DateTime from, DateTime to, string name, DateTime dateCreate)
        {
            try
            {
                var predicate = BuildSearchPredicate(from, to, name, dateCreate);
                var searchedDocuments = await _unitOfWork.GetRepository<Document>().GetAllAsync(predicate: predicate, disableTracking: false);
                return new BaseResult<IEnumerable<Document>>(true, searchedDocuments, OperationStatuses.Ok);
            }
            catch (Exception ex)
            {
                return HandleException<IEnumerable<Document>>(ex);
            }

        }

        public async Task<BaseResult<bool>> Update(Document entity)
        {
            _unitOfWork.DbContext.AttachRange(entity.Payouts);
            _unitOfWork.DbContext.AttachRange(entity.Employees);
            _unitOfWork.GetRepository<Document>().Update(entity);
            await _unitOfWork.SaveChangesAsync();
            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                return HandleException<bool>();
            }
            return new BaseResult<bool>(true, true, OperationStatuses.Ok);
        }
        private Expression<Func<Document, bool>> BuildSearchPredicate(DateTime from, DateTime to, string name, DateTime dateCreate)
        {
            var predicate = PredicateBuilder.True<Document>();
            if (from != default(DateTime))
                predicate = predicate.And(x => x.DateCreate >= from);
            if (to != default(DateTime))
                predicate = predicate.And(x => x.DateCreate <= to);
            if (!string.IsNullOrWhiteSpace(name))
                predicate = predicate.And(x => x.Name.Contains(name));
            if (dateCreate != default(DateTime) && from == default(DateTime) && to == default(DateTime))
                predicate = predicate.And(x => x.DateCreate == dateCreate);
            return predicate;
        }
    }
}
