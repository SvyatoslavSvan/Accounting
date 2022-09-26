using Accounting.Domain.JsonModels;
using Accounting.Domain.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Accounting.Domain.SessionEntity
{
    public class SessionDocument
    {
        [BindNever]
        public List<NotBetEmployeeJsonModel> Employees { get; set; } = new List<NotBetEmployeeJsonModel>();
        [BindNever]
        public List<Accrual> Accruals { get; set; } = new List<Accrual>();
    }
}
