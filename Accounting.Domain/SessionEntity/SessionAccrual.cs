namespace Accounting.Domain.SessionEntity
{
    public class SessionAccrual
    {
        public decimal Ammount { get; set; }
        public Guid Id { get; set; }
        public DateTime DateCreate { get; set; }
        public Guid EmployeeId { get; set; }
        public bool IsAdditional { get; set; }
    }
}
