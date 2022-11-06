using Accounting.DAL.Interfaces;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models.Base;
using Accouting.Domain.Managers.Interfaces;

namespace Accouting.Domain.Managers.Implementations
{
    public class DeducationManager : IDeducationManager
    {
        private readonly IDeducationProvider _deducationProvider;
        public DeducationManager(IDeducationProvider deducationProvider) => _deducationProvider = deducationProvider;
    
        public async Task<BaseResult<DeducationBase>> Create(DeducationBase model)
        {
            var createResult = await _deducationProvider.Create(model);
            return new BaseResult<DeducationBase>(createResult.Succed, model, createResult.OperationStatus);
        }

        public async Task<BaseResult<bool>> Delete(Guid id) => await _deducationProvider.Delete(id);
       

        public async Task<BaseResult<IList<DeducationBase>>> GetAll()
        {
            var getAllResult = await _deducationProvider.GetAll();
            return new BaseResult<IList<DeducationBase>>(getAllResult.Succed, 
                getAllResult.Data, getAllResult.OperationStatus);
        }

        public async Task<BaseResult<DeducationBase>> GetById(Guid id)
        {
            var getByIdResult = await _deducationProvider.GetById(id);
            return new BaseResult<DeducationBase>(getByIdResult.Succed, getByIdResult.Data, getByIdResult.OperationStatus);
        }

        public async Task<BaseResult<DeducationBase>> Update(DeducationBase model)
        {
            var updateResult = await _deducationProvider.Update(model);
            return new BaseResult<DeducationBase>(updateResult.Succed, model, updateResult.OperationStatus);
        }
    }
}
