using Accounting.DAL.Contexts;
using Accounting.DAL.Interfaces;
using Accounting.DAL.Providers.BaseProvider;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using Calabonga.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Accounting.DAL.Providers
{
    public class DeducationDocumentProvider : ProviderBase , IDeducationDocumentProvider
    {
#nullable disable
        public DeducationDocumentProvider(IUnitOfWork<ApplicationDBContext> unitOfWork, ILogger<DeducationDocument> logger) : base(unitOfWork, logger) {}

        public async Task<BaseResult<bool>> Create(DeducationDocument entity)
        {
            _unitOfWork.DbContext.AttachRange(entity.DeducationsNotBetEmployee);
            _unitOfWork.DbContext.AttachRange(entity.DeducationsBetEmployee);
            _unitOfWork.DbContext.AttachRange(entity.BetEmployees);
            _unitOfWork.DbContext.AttachRange(entity.NotBetEmployees);
            await _unitOfWork.GetRepository<DeducationDocument>().InsertAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                return HandleException<bool>();
            }
            return new BaseResult<bool>(true, true, OperationStatuses.Ok);
        }

        public async Task<BaseResult<bool>> Delete(Guid id)
        {
            _unitOfWork.GetRepository<DeducationDocument>().Delete(id);
            await _unitOfWork.SaveChangesAsync();
            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                return HandleException<bool>();
            }
            return new BaseResult<bool>(true, true, OperationStatuses.Ok);
        }

        public async Task<BaseResult<List<DeducationDocument>>> GetAll(Expression<Func<DeducationDocument, bool>> predicate = null)
        {
            try
            {
                var deducationDocuments = await _unitOfWork.GetRepository<DeducationDocument>().GetAllAsync(disableTracking: true, predicate: predicate);
                return new BaseResult<List<DeducationDocument>>(true, deducationDocuments.ToList(), OperationStatuses.Ok);
            }
            catch (Exception ex)
            {
                return HandleException<List<DeducationDocument>>(ex);
            }
            
        }

        public async Task<BaseResult<DeducationDocument>> GetById(Guid id)
        {
            try
            {
                return new BaseResult<DeducationDocument>(true, await _unitOfWork.GetRepository<DeducationDocument>()
                    .GetFirstOrDefaultAsync(disableTracking: false,
                    predicate: x => x.Id == id, 
                    include: x => x.Include(x => x.BetEmployees).Include(x => x.NotBetEmployees).Include(x => x.DeducationsBetEmployee).Include(x => x.DeducationsNotBetEmployee)), 
                    OperationStatuses.Ok);
            }
            catch (Exception ex)
            {
                return HandleException<DeducationDocument>(ex);
            }
        }

        public async Task<BaseResult<bool>> Update(DeducationDocument entity)
        {
            _unitOfWork.DbContext.AttachRange(entity.DeducationsBetEmployee);
            _unitOfWork.DbContext.AttachRange(entity.DeducationsNotBetEmployee);
            _unitOfWork.DbContext.AttachRange(entity.BetEmployees);
            _unitOfWork.DbContext.AttachRange(entity.NotBetEmployees);
            _unitOfWork.GetRepository<DeducationDocument>().Update(entity);
            await _unitOfWork.SaveChangesAsync();
            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                return HandleException<bool>();
            }
            return new BaseResult<bool>(true, true, OperationStatuses.Ok);
        }
    }
}
