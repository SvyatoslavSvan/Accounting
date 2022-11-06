
namespace Accounting.Domain.SessionEntity
{
    public class SessionDocument
    {
#nullable disable
        public List<SessionAccrual> Accruals { get; set; } = new List<SessionAccrual>();
        public List<SessionNotBetEmployee> Employees { get; set; } = new List<SessionNotBetEmployee>();
    }
}
