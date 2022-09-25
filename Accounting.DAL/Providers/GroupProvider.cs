using Accounting.DAL.Interfaces;
using Accounting.DAL.Interfaces.Base;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;

namespace Accounting.DAL.Providers
{
#nullable disable
    public class GroupProvider : IBaseProvider<Group>
    {
        private readonly IGroupRepository _groupRepository;
        public GroupProvider(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }
        public async Task<BaseResult<bool>> Create(Group entity)
        {
            try
            {
                var isUniq = await this.IsUniqName(entity.Name);
                if (isUniq)
                {
                    await _groupRepository.Add(entity);
                    return new BaseResult<bool>(true, true);
                }
                return new BaseResult<bool>(false, false);
            }
            catch (Exception)
            {
                return new BaseResult<bool>(false, false);
            }
        }

        public async Task<BaseResult<Group>> GetById(Guid id)
        {
            try
            {
                var getByIdResult = await _groupRepository.ReadById(id);
                if (getByIdResult is not null)
                    return new BaseResult<Group>(true, getByIdResult);
                return new BaseResult<Group>(false, null);
            }
            catch (Exception)
            {
                return new BaseResult<Group>(false, null);
            }
        }

        public async Task<BaseResult<List<Group>>> GetAll()
        {
            try
            {
                var getAllEmployees = await _groupRepository.ReadAll();
                return new BaseResult<List<Group>>(true, getAllEmployees.ToList());
            }
            catch (Exception)
            {
                return new BaseResult<List<Group>>(false, null);
            }
        }

        public async Task<BaseResult<bool>> Update(Group entity)
        {
            try
            {
                await _groupRepository.Update(entity);
                return new BaseResult<bool>(true, true);
            }
            catch (Exception)
            {
                return new BaseResult<bool>(false, false);
            }
        }
        private async Task<bool> IsUniqName(string name) => await _groupRepository.CountGroupsWithName(name) == 0 ? true : false;

        public Task<BaseResult<bool>> Delete(Guid entity)
        {
            throw new NotImplementedException();
        }
    }
}
