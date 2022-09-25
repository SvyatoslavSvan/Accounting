using Accounting.DAL.Result.Provider.Base;
namespace Accounting.DAL.Interfaces.Base
{
    public interface IBaseProvider<T>
    {
        public Task<BaseResult<T>> GetById(Guid id);
        public Task<BaseResult<List<T>>> GetAll();
        public Task<BaseResult<bool>> Create(T entity);
        public Task<BaseResult<bool>> Update(T entity);
        public Task<BaseResult<bool>> Delete(Guid id); 
    }
}
