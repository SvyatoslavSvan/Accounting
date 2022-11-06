using Accounting.DAL.Interfaces;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using Accouting.Domain.Managers.Interfaces;

namespace Accouting.Domain.Managers.Implementations
{
    public class AccrualManager : IAccrualManager
    {
        private readonly IAccrualProvider _provider;

        public AccrualManager(IAccrualProvider provider)
        {
            _provider = provider;
        }

        public async Task<BaseResult<Accrual>> Create(Accrual model)
        {
            var createResult = await _provider.Create(model);
            return new BaseResult<Accrual>(createResult.Succed, model, createResult.OperationStatus);
        }

        public async Task<BaseResult<bool>> Delete(Guid id) => await _provider.Delete(id);

        public async Task<BaseResult<bool>> DeleteByDocumentId(Guid id)
        {
            var getDocumentsToDeleteResult = await _provider.GetAllByPredicate(x => x.Document.Id == id);
            if (getDocumentsToDeleteResult.Succed)
            {
                var deleteResult = await _provider.DeleteRange(getDocumentsToDeleteResult.Data);
                return deleteResult;
            }
            return new BaseResult<bool>(false, false, getDocumentsToDeleteResult.OperationStatus); 
        }

        public async Task<BaseResult<bool>> DeleteRange(List<Accrual> accruals) => await _provider.DeleteRange(accruals);


        public async Task<BaseResult<IList<Accrual>>> GetAll()
        {
            var getAllResult = await _provider.GetAll();
            return new BaseResult<IList<Accrual>>(getAllResult.Succed, getAllResult.Data, getAllResult.OperationStatus);
        }

        public async Task<BaseResult<Accrual>> GetById(Guid id) => await _provider.GetById(id);

        public async Task<BaseResult<Accrual>> Update(Accrual model)
        {
            var updateResult = await _provider.Update(model);
            return new BaseResult<Accrual>(updateResult.Succed, model, updateResult.OperationStatus);
        }
    }
}
