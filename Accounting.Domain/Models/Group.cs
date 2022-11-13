using Accounting.Domain.Models.Base;
namespace Accounting.Domain.Models
{
#nullable disable
    public class Group : EntityBase
    {
        public Group() { }
        public string Name { get; private set; }
        public List<BetEmployee> BetEmployees { get; private set; }
        public List<NotBetEmployee> NotBetEmployees { get; private set; } // возможно надо будут миграции потому что private set
        public Group(string name)
        {
            Name = name;
        }
        public virtual void AddToGroup(EmployeeBase employee)
        {
            if (employee is BetEmployee betEmployee)
                BetEmployees.Add(betEmployee);
            else if (employee is NotBetEmployee notBetEmployee)
                NotBetEmployees.Add(notBetEmployee);
        }
        public virtual void RemoveFromGroup(EmployeeBase employee)
        {
            if (employee is BetEmployee betEmployee)
                BetEmployees.Remove(betEmployee);
            else if (employee is NotBetEmployee notBetEmployee)
                NotBetEmployees.Remove(notBetEmployee);
        }
    }
}