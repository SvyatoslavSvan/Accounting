using Accounting.DAL.Contexts;
using Accounting.DAL.Interfaces;
using Accounting.DAL.Providers.BaseProvider;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using Calabonga.UnitOfWork;
using Microsoft.Extensions.Logging;
using OfficeOpenXml.Utils;

namespace Accounting.DAL.Providers
{
    public class DeducationProvider : ProviderBase , IDeducationProvider
    {
#nullable disable
        public DeducationProvider(IUnitOfWork<ApplicationDBContext> unitOfWork, ILogger<Deducation> logger) : base(unitOfWork, logger) { }

        public async Task<BaseResult<bool>> Create(Deducation entity)
        {
            if (entity.BetEmployee is not null)
            {
                _unitOfWork.DbContext.Attach(entity.BetEmployee);
            }
            if (entity.NotBetEmployee is not null)
            {
                _unitOfWork.DbContext.Attach(entity.NotBetEmployee);
            }
            await _unitOfWork.GetRepository<Deducation>().InsertAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                return HandleException<bool>();
            }
            return new BaseResult<bool>(true, true, OperationStatuses.Ok);
        }

        public async Task<BaseResult<bool>> Delete(Guid id)
        {
            _unitOfWork.GetRepository<Deducation>().Delete(id);
            await _unitOfWork.SaveChangesAsync();
            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                return HandleException<bool>();
            }
            return new BaseResult<bool>(true, true, OperationStatuses.Ok);
        }

        public async Task<BaseResult<List<Deducation>>> GetAll()
        {
            try
            {
                var Deducations = await _unitOfWork.GetRepository<Deducation>().GetAllAsync(true);
                return new BaseResult<List<Deducation>>(true, Deducations.ToList(), OperationStatuses.Ok);
            }
            catch (Exception ex)
            {
                return HandleException<List<Deducation>>(ex);
            }
        }

        public async Task<BaseResult<Deducation>> GetById(Guid id)
        {
            try
            {
                return new BaseResult<Deducation>(true, await _unitOfWork.GetRepository<Deducation>()
                    .GetFirstOrDefaultAsync(predicate: x => x.Id == id), OperationStatuses.Ok);
            }
            catch (Exception ex)
            {
                return HandleException<Deducation>(ex);    
            }
        }

        public async Task<BaseResult<bool>> Update(Deducation entity)
        {
            _unitOfWork.DbContext.Attach(entity.BetEmployee);
            _unitOfWork.DbContext.Attach(entity.NotBetEmployee);
            _unitOfWork.GetRepository<Deducation>().Update(entity);
            await _unitOfWork.SaveChangesAsync();
            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                return HandleException<bool>();
            }
            return new BaseResult<bool>(true, true, OperationStatuses.Ok);
        }
       
    }
}
