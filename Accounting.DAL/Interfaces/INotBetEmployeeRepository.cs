using Accounting.DAL.Interfaces.Base;
using Accounting.Domain.Models;

namespace Accounting.DAL.Interfaces
{
    public interface INotBetEmployeeRepository : IBaseEmployeeRepository<NotBetEmployee>
    {
        public Task<IEnumerable<NotBetEmployee>> GetAllIncludeDocument();
    }
}
