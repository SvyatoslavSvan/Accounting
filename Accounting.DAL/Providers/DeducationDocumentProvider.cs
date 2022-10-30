using Accounting.DAL.Contexts;
using Accounting.DAL.Interfaces;
using Accounting.DAL.Providers.BaseProvider;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using Calabonga.UnitOfWork;
using Microsoft.Extensions.Logging;

namespace Accounting.DAL.Providers
{
    public class DeducationDocumentProvider : ProviderBase , IDeducationDocumentProvider
    {
        public DeducationDocumentProvider(IUnitOfWork<ApplicationDBContext> unitOfWork, ILogger<DeducationDocument> logger) : base(unitOfWork, logger) {}
        public async Task<BaseResult<bool>> Create(DeducationDocument entity)
        {
            _unitOfWork.DbContext.Attach(entity.Deducations);
            _unitOfWork.DbContext.Attach(entity.BetEmployees);
            _unitOfWork.DbContext.Attach(entity.NotBetEmployees);
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

        public async Task<BaseResult<List<DeducationDocument>>> GetAll()
        {
            try
            {
                var deducationDocuments = await _unitOfWork.GetRepository<DeducationDocument>().GetAllAsync(true);
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
                return new BaseResult<DeducationDocument>(true, await _unitOfWork.GetRepository<DeducationDocument>().GetFirstOrDefaultAsync(), OperationStatuses.Ok);
            }
            catch (Exception ex)
            {
                return HandleException<DeducationDocument>(ex);
            }
        }

        public async Task<BaseResult<bool>> Update(DeducationDocument entity)
        {
            _unitOfWork.DbContext.Attach(entity.Deducations);
            _unitOfWork.DbContext.Attach(entity.BetEmployees);
            _unitOfWork.DbContext.Attach(entity.NotBetEmployees);
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
