namespace Accounting.DAL.Interfaces.Base
{
    public interface IBaseEmployeeRepository<T> : IBaseRepository<T>
    {
        public Task<int> CountGroupsWithInnerId(string innerId); 
    }
}
