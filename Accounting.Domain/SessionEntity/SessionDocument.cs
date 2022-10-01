using Accounting.Domain.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Accounting.Domain.SessionEntity
{
    public class SessionDocument
    {
        [BindNever]
        public List<SessionNotBetEmployee> Employees { get; set; } = new List<SessionNotBetEmployee>();
    }
}
