namespace Accounting.Domain.Models
{
    public class Accrual
    {
#nullable disable
        public Accrual() { }
        public Guid Id { get; private set; }
        public Accrual(DateTime dateCreate, decimal ammount, bool isAdditional)
        {
            DateCreate = dateCreate;
            Ammount = ammount;
            IsAdditional = isAdditional;
        }
        public Accrual(DateTime dateCreate, decimal ammount, Guid id, bool isAdditional)
        {
            DateCreate = dateCreate;
            Ammount = ammount;
            Id = id;
            IsAdditional = isAdditional;
        }
        public bool IsAdditional { get; private set; }
        private decimal _ammount;

        public decimal Ammount
        {
            get => _ammount;
            set 
            {
                const decimal minValue = 0;
                if (value < minValue)
                {
                    value = minValue;
                    return;
                }
                _ammount = value; 
            }
        }

        public DateTime DateCreate { get; private set; }
        public Guid EmployeeId { get; private set; }
        public NotBetEmployee Employee { get; private set; }
        public Document Document { get; private set; } 

        public void AddEmployee(NotBetEmployee employee)
        {
            if (Employee is null)
                Employee = employee ?? throw new ArgumentNullException();
        }
        public void SetAmmount(decimal ammount) => this.Ammount = ammount;
    }
}
