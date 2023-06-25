using Accounting.Domain.Models.Base;
namespace Accounting.Domain.Models
{
#nullable disable
    public class Group : EntityBase
    {
        private string _name;

        private ICollection<Employee> _employees;

        public Group(string name)
        {
            Name = name;
        }

        public Group() { }

        public string Name
        {
            get => _name;
            set 
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Value cannot be WhiteSpace" + nameof(Name));
                }
                _name = value;
            }
        }

        public ICollection<Employee> Employees
        {
            get => _employees;
            set { _employees = value; }
        }

        public void AddToGroup(Employee employee)
        {
            _employees.Add(employee);
        }

        public void RemoveFromGroup(Employee employee)
        {
            _employees.Remove(employee);
        }
    }
}