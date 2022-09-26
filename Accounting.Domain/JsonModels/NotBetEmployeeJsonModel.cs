using Accounting.Domain.Models;

namespace Accounting.Domain.JsonModels
{
    public class NotBetEmployeeJsonModel
    {
#nullable disable
        public List<Accrual> Accruals { get; set; }
        public string Name { get; set; }
        public string InnerId { get; set; }
        public Guid Id { get; set; }
    }
}
