using Accounting.DAL.Contexts;
using Accounting.DAL.Interfaces.Base;
using Accounting.DAL.Providers.BaseProvider;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using Calabonga.UnitOfWork;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OfficeOpenXml.FormulaParsing.ExpressionGraph;
using System.Linq.Expressions;

namespace Accounting.DAL.Providers
{
#nullable disable
    public class GroupProvider : ProviderBase, IBaseProvider<Group>
    {

        public GroupProvider(IUnitOfWork<ApplicationDBContext> unitOfWork, ILogger<Group> logger) : base(unitOfWork, logger) { }
        

        public async Task<BaseResult<bool>> Create(Group entity)
        {
            if (await IsUniqName(entity.Name))
            {
                
                await _unitOfWork.GetRepository<Group>().InsertAsync(entity);
                await _unitOfWork.SaveChangesAsync();
                if (!_unitOfWork.LastSaveChangesResult.IsOk)
                {
                    return HandleException<bool>();
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
                include: x => x.Include(x => x.Employees),
                disableTracking: false
                );
                if (group is null)
                    return new BaseResult<Group>(false, group, OperationStatuses.NotFound);
                return new BaseResult<Group>(true, group, OperationStatuses.Ok);
            }
            catch (Exception ex)
            {
                return HandleException<Group>(ex);
            }
            
        }

        public async Task<BaseResult<List<Group>>> GetAll()
        {  
            try
            {
                var Groups = await _unitOfWork.GetRepository<Group>().GetAllAsync(
                    include: x => x.Include(x => x.Employees));
                return new BaseResult<List<Group>>(true, Groups.ToList(), OperationStatuses.Ok);
            }
            catch (Exception ex)
            {
                return HandleException<List<Group>>(ex);
            }
        }

        public async Task<BaseResult<bool>> Update(Group entity)
        {
            _unitOfWork.GetRepository<Group>().Update(entity);
            await _unitOfWork.SaveChangesAsync();
            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                return HandleException<bool>();
            }
            return new BaseResult<bool>(true, true, OperationStatuses.Ok);
        }

        public async Task<BaseResult<bool>> Delete(Guid entity)
        {
            _unitOfWork.GetRepository<Group>().Delete(entity);
            await _unitOfWork.SaveChangesAsync();
            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                return HandleException<bool>();
            }
            return new BaseResult<bool>(true, true, OperationStatuses.Ok);
        }

        private async Task<bool> IsUniqName(string name) => await _unitOfWork.GetRepository<Group>().CountAsync(predicate: x=> x.Name == name) == 0 ? true : false;
    }
}
