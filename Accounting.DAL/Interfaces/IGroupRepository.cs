using Accounting.DAL.Interfaces.Base;
using Accounting.Domain.Models;

namespace Accounting.DAL.Interfaces
{
    public interface IGroupRepository : IBaseRepository<Group>
    {
        public Task<int> CountGroupsWithName(string name);
    }
}
