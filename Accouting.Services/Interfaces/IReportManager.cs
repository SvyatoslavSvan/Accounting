using Accounting.Domain.Models;

namespace Accouting.Domain.Managers.Interfaces
{
    public interface IReportManager
    {
        public Task<byte[]> GetReportAsExcel(DateTime from, DateTime to);
        public IList<Salary> GetReportAsModel();
    }
}
