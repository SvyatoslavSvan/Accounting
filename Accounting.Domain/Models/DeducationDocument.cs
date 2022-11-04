using Accounting.Domain.Models.Base;

namespace Accounting.Domain.Models
{
    public class DeducationDocument
    {
        public DeducationDocument() { }
        public DeducationDocument(Guid id,string name, DateTime dateCreate, ICollection<DeducationBetEmployee> deducationsBetEmployee, 
            ICollection<NotBetEmployee> notBetEmployees, ICollection<BetEmployee> betEmployees, ICollection<DeducationNotBetEmployee> deducationNotBetEmployees)
        {
            Name = name;
            DateCreate = dateCreate;
            DeducationsBetEmployee = deducationsBetEmployee;
            NotBetEmployees = notBetEmployees;
            BetEmployees = betEmployees;
            DeducationsNotBetEmployee = deducationNotBetEmployees;
        }
        public Guid Id { get; private set; }
        public DateTime DateCreate { get; set; }
        public string Name { get; set; } = null!;

        private ICollection<DeducationBetEmployee> _deducationsBetEmployee = null!;

        public ICollection<DeducationBetEmployee> DeducationsBetEmployee
        {
            get => _deducationsBetEmployee;
            set 
            {
                UpdateDeducationsRange(value, DeducationsBetEmployee);
            }
        }

        private ICollection<DeducationNotBetEmployee> _deducationsNotBetEmployee = null!;

        public ICollection<DeducationNotBetEmployee> DeducationsNotBetEmployee
        {
            get => _deducationsNotBetEmployee;
            set
            {
                UpdateDeducationsRange(value, DeducationsNotBetEmployee);
            }
        }

        private ICollection<NotBetEmployee> _notBetEmployees = null!;

        public ICollection<NotBetEmployee> NotBetEmployees
        {
            get => _notBetEmployees;
            set 
            {
                UpdateEmployeesRange(value, NotBetEmployees); 
            }
        }

        private ICollection<BetEmployee> _betEmployees = null!;

        public ICollection<BetEmployee> BetEmployees
        {
            get => _betEmployees;
            set
            {
                UpdateEmployeesRange(value, BetEmployees);
            }
        }

        private void UpdateEmployeesRange<T>(ICollection<T> newRange, ICollection<T> oldRange) where T : EmployeeBase
        {
            foreach (var item in newRange)
            {
                var containsInThis = oldRange.Contains(oldRange.FirstOrDefault(x => x.Id == item.Id));
                if (!containsInThis)
                {
                    oldRange.Add(item);
                }
            }
            var elementsToRemove = new List<Guid>();
            foreach (var thisEmployee in oldRange)
            {
                var containsInUpdate = newRange.Contains(newRange.FirstOrDefault(x => x.Id == thisEmployee.Id));
                if (!containsInUpdate)
                {
                    elementsToRemove.Add(thisEmployee.Id);
                }
            }
            foreach (var item in elementsToRemove)
            {
                oldRange.Remove(oldRange.FirstOrDefault(x => x.Id == item));
            }
        }

        private void UpdateDeducationsRange<T>(ICollection<T> newRange, ICollection<T> oldRange) where T : DeducationBase
        {
            foreach (var item in newRange)
            {
                var containsInThis = oldRange.Contains(oldRange.FirstOrDefault(x => x.Id == item.Id));
                if (!containsInThis)
                {
                    oldRange.Add(item);
                }
            }
            var elementsToRemove = new List<Guid>();
            foreach (var thisEmployee in oldRange)
            {
                var containsInUpdate = newRange.Contains(newRange.FirstOrDefault(x => x.Id == thisEmployee.Id));
                if (!containsInUpdate)
                {
                    elementsToRemove.Add(thisEmployee.Id);
                }
            }
            foreach (var item in elementsToRemove)
            {
                oldRange.Remove(oldRange.FirstOrDefault(x => x.Id == item));
            }
        }

    }
}
