using Accounting.Domain.Models;

namespace Accounting.Services.Interfaces
{
    public interface IReportService
    {
        public Task<byte[]> MakeReportAsExcel();
    }
}
