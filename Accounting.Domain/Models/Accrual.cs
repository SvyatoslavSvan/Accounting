namespace Accounting.Domain.Models
{
    public class Accrual
    {
#nullable disable
        public Guid Id { get; private set; }
        public Accrual(DateTime dateCreate, int ammount)
        {
            DateCreate = dateCreate;
            Ammount = ammount;
        }
        public int Ammount { get; private set; }
        public DateTime DateCreate { get; private set; }
        public Guid EmployeeId { get; private set; }
        public NotBetEmployee Employee { get; private set; }
        public void AddEmployee(NotBetEmployee employee)
        {
            if (Employee is null)
                Employee = employee ?? throw new ArgumentNullException();
        }
    }
}
