using Accounting.DAL.Interfaces.Base;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using Calabonga.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Accounting.DAL.Providers
{
#nullable disable
    public class GroupProvider : IBaseProvider<Group>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<Group> _logger;

        public GroupProvider(IUnitOfWork unitOfWork, ILogger<Group> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger; 
        }

        public async Task<BaseResult<bool>> Create(Group entity)
        {
            if (await IsUniqName(entity.Name))
            {
                
                await _unitOfWork.GetRepository<Group>().InsertAsync(entity);
                await _unitOfWork.SaveChangesAsync();
                if (!_unitOfWork.LastSaveChangesResult.IsOk)
                {
                    LogErrorMessage();
                    return new BaseResult<bool>(false, false, OperationStatuses.Error);
                }
                return new BaseResult<bool>(true, true, OperationStatuses.Ok);
            }
            return new BaseResult<bool>(false, false, OperationStatuses.NotUniqName);
        }

        public async Task<BaseResult<Group>> GetById(Guid id)
        {
            try
            {
                var group = await _unitOfWork.GetRepository<Group>()
                .GetFirstOrDefaultAsync(
                predicate: x => x.Id == id,
                include: x => x.Include(x => x.NotBetEmployees).Include(x => x.BetEmployees),
                disableTracking: false
                );
                if (group is null)
                    return new BaseResult<Group>(false, group, OperationStatuses.NotFound);
                return new BaseResult<Group>(true, group, OperationStatuses.Ok);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex);
                return new BaseResult<Group>(false, null, OperationStatuses.Error);
            }
            
        }

        public async Task<BaseResult<List<Group>>> GetAll()
        {  
            try
            {
                var Groups = await _unitOfWork.GetRepository<Group>().GetAllAsync(
                    include: x => x.Include(x => x.NotBetEmployees).Include(x => x.BetEmployees));
                return new BaseResult<List<Group>>(true, Groups.ToList(), OperationStatuses.Ok);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex);
                return new BaseResult<List<Group>>(false, null, OperationStatuses.Error);
            }
        }

        public async Task<BaseResult<bool>> Update(Group entity)
        {
            _unitOfWork.GetRepository<Group>().Update(entity);
            await _unitOfWork.SaveChangesAsync();
            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                LogErrorMessage();
                return new BaseResult<bool>(false, false, OperationStatuses.Error);
            }
            return new BaseResult<bool>(true, true, OperationStatuses.Ok);
        }

        public async Task<BaseResult<bool>> Delete(Guid entity)
        {
            _unitOfWork.GetRepository<Group>().Delete(entity);
            await _unitOfWork.SaveChangesAsync();
            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                LogErrorMessage();
                return new BaseResult<bool>(false, false, OperationStatuses.Error);
            }
            return new BaseResult<bool>(true, true, OperationStatuses.Ok);
        }

        private async Task<bool> IsUniqName(string name) => await _unitOfWork.GetRepository<Group>().CountAsync(predicate: x=> x.Name == name) == 0 ? true : false;
        private void LogErrorMessage(Exception ex = null)
        {
            var exception = ex ?? _unitOfWork.LastSaveChangesResult.Exception;
            _logger.LogError(exception.Message);
            _logger.LogError(exception.StackTrace);
        }
    }
}
