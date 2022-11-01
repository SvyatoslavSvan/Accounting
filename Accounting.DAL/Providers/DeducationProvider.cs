using Accounting.DAL.Contexts;
using Accounting.DAL.Interfaces;
using Accounting.DAL.Providers.BaseProvider;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using Accounting.Domain.Models.Base;
using Calabonga.UnitOfWork;
using Microsoft.Extensions.Logging;

namespace Accounting.DAL.Providers
{
    public class DeducationProvider : ProviderBase , IDeducationProvider
    {
#nullable disable
        public DeducationProvider(IUnitOfWork<ApplicationDBContext> unitOfWork, ILogger<DeducationBase> logger) : base(unitOfWork, logger) { }

        public async Task<BaseResult<bool>> Create(DeducationBase entity)
        {
            if (entity is DeducationBetEmployee deducationBetEmployee)
            {
                _unitOfWork.DbContext.Attach(deducationBetEmployee.Employee);
                await _unitOfWork.GetRepository<DeducationBetEmployee>().InsertAsync(deducationBetEmployee);
            }
            if (entity is DeducationNotBetEmployee deducationNotBetEmployee)
            {
                _unitOfWork.DbContext.Attach(deducationNotBetEmployee.Employee);
                await _unitOfWork.GetRepository<DeducationNotBetEmployee>().InsertAsync(deducationNotBetEmployee);
            }
            await _unitOfWork.SaveChangesAsync();
            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                return HandleException<bool>();
            }
            return new BaseResult<bool>(true, true, OperationStatuses.Ok);
        }

        public async Task<BaseResult<bool>> Delete(Guid id)
        {
            var count = await _unitOfWork.GetRepository<DeducationBetEmployee>().CountAsync(predicate: x => x.Id == id);
            if (count == 0)
            {
                _unitOfWork.GetRepository<DeducationNotBetEmployee>().Delete(id);
            }
            else
            {
                _unitOfWork.GetRepository<DeducationBetEmployee>().Delete(id);
            }
            await _unitOfWork.SaveChangesAsync();
            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                return HandleException<bool>();   
            }
            return new BaseResult<bool>(true, true, OperationStatuses.Ok);
        }

        public async Task<BaseResult<List<DeducationBase>>> GetAll()
        {
            try
            {
                var deducations = new List<DeducationBase>();
                deducations.AddRange(await _unitOfWork.GetRepository<DeducationBetEmployee>().GetAllAsync(true));
                deducations.AddRange(await _unitOfWork.GetRepository<DeducationNotBetEmployee>().GetAllAsync(true));
                return new BaseResult<List<DeducationBase>>(true, deducations, OperationStatuses.Ok);
            }
            catch (Exception ex)
            {
                return HandleException<List<DeducationBase>>(ex);
            }
        }

        public async Task<BaseResult<DeducationBase>> GetById(Guid id)
        {
            try
            {
                var count = await _unitOfWork.GetRepository<DeducationBetEmployee>().CountAsync(predicate: x => x.Id == id);
                if (count == 0)
                {
                    return new BaseResult<DeducationBase>(true, await _unitOfWork.
                        GetRepository<DeducationNotBetEmployee>().
                        GetFirstOrDefaultAsync(predicate: x => x.Id == id), OperationStatuses.Ok);
                }
                else
                {
                    return new BaseResult<DeducationBase>(true, await
                        _unitOfWork.GetRepository<DeducationBetEmployee>()
                        .GetFirstOrDefaultAsync(predicate: x => x.Id == id), OperationStatuses.Ok);
                }
            }
            catch (Exception ex)
            {
                return HandleException<DeducationBase>(ex);
            }
        }

        public async Task<BaseResult<bool>> Update(DeducationBase entity)
        {
            if (entity is DeducationBetEmployee deducationBetEmployee)
            {
                _unitOfWork.DbContext.Attach(deducationBetEmployee.Employee);
                _unitOfWork.GetRepository<DeducationBetEmployee>().Update(deducationBetEmployee);
            }
            if(entity is DeducationNotBetEmployee deducationNotBetEmployee)
            {
                _unitOfWork.DbContext.Attach(deducationNotBetEmployee);
                _unitOfWork.GetRepository<DeducationNotBetEmployee>().Update(deducationNotBetEmployee);
            }
            await _unitOfWork.SaveChangesAsync();
            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                return HandleException<bool>();
            }
            return new BaseResult<bool>(true, true, OperationStatuses.Ok);
        }
    }
}
