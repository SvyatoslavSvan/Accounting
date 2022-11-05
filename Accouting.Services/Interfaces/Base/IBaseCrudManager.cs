using Accounting.DAL.Result.Provider.Base;

namespace Accouting.Domain.Managers.Interfaces.Base
{
    public interface IBaseCrudManager<T> 
    {
        public Task<BaseResult<T>> Create(T model);
        public Task<BaseResult<T>> Update(T model);
        public Task<BaseResult<T>> Delete(T model);
        public Task<BaseResult<IList<T>>> GetAll();
        public Task<BaseResult<T>> GetById(Guid id);
    }
}
