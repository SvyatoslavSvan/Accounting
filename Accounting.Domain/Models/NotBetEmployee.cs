using Accounting.Domain.Models.Base;

namespace Accounting.Domain.Models
{
    public class NotBetEmployee : EmployeeBase
    {
#nullable disable
        public List<Accrual> Accruals { get; private set; } 
        public List<Document> Documents { get; private set; } 
        public NotBetEmployee(string name, string innerId) : base(name, innerId)
        {
            
        }
        public void AddAccrual(Accrual accrual)
        {
            if (Accruals is null)
            {
                Accruals = new List<Accrual>();
                Accruals.Add(accrual);
            }
            else
            {
                Accruals.Add(accrual);
            }  
        }
        public void AddAcruals(List<Accrual> accruals)
        {
            if (this.Accruals is null)
                this.Accruals = accruals;
            else
                this.Accruals.AddRange(accruals);
        }

        public override decimal CalculateSalary(DateTime from)
        {
            throw new NotImplementedException();
        }
    }
}
