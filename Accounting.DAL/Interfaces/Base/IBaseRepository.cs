namespace Accounting.DAL.Interfaces.Base
{
    public interface IBaseRepository<T>
    {
        public Task<T> ReadById(Guid id);
        public Task<IEnumerable<T>> ReadAll();
        public Task Add(T entity);
        public Task Update(T entity);
        public Task Delete(Guid id);
    }
}
