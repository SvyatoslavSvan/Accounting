using Accounting.DAL.Interfaces.Base;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using Accouting.Domain.Managers.Interfaces;

namespace Accouting.Domain.Managers.Implementations
{
    public class GroupManager : IGroupManager
    {
        private readonly IBaseProvider<Group> _groupProvider;

        public GroupManager(IBaseProvider<Group> groupProvider)
        {
            _groupProvider = groupProvider;
        }

        public async Task<BaseResult<Group>> Create(Group model)
        {
            var createResult = await _groupProvider.Create(model);
            return new BaseResult<Group>(createResult.Succed, model, createResult.OperationStatus);
        }

        public async Task<BaseResult<bool>> Delete(Guid id)
        {
            var deleteResult = await _groupProvider.Delete(id);
            return new BaseResult<bool>(deleteResult.Succed, deleteResult.Data, deleteResult.OperationStatus);
        }

        public async Task<BaseResult<IList<Group>>> GetAll()
        {
            var getAllResult = await _groupProvider.GetAll();
            return new BaseResult<IList<Group>>(getAllResult.Succed, getAllResult.Data, getAllResult.OperationStatus);
        }

        public async Task<BaseResult<Group>> GetById(Guid id)
        {
            var getByIdResult = await _groupProvider.GetById(id);
            return new BaseResult<Group>(getByIdResult.Succed, getByIdResult.Data, getByIdResult.OperationStatus);  
        }

        public async Task<BaseResult<Group>> Update(Group model)
        {
            var updateResult = await _groupProvider.Update(model);
            return new BaseResult<Group>(updateResult.Succed, model, updateResult.OperationStatus);
        }
    }
}
