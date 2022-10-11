using Accounting.DAL.Interfaces.Base;
using Accounting.Domain.Models;

namespace Accounting.DAL.Interfaces
{
    public interface IAccrualRepository : IBaseRepository<Accrual>
    {
        public Task DeleteRange(List<Accrual> accruals);
    }
}
