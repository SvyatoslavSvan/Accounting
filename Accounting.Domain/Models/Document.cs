using Accounting.Domain.Enums;
using Accounting.Domain.Models.Base;

namespace Accounting.Domain.Models
{
    public class Document : EntityBase
    {
#nullable disable
        public Document() { }
        
        public Document(string name, DateTime dateCreate)
        {
            Name = name;
            DateCreate = dateCreate;
        }

        public Document(ICollection<Employee> employees, string name, DateTime dateCreate, DocumentType documentType, ICollection<Payout> payouts)
        {
            Name = name;
            DateCreate = dateCreate;
            DocumentType = documentType;
            Payouts = payouts;
            Employees = employees;
        }

        public DocumentType DocumentType { get; private set; }
        
        public string Name { get; set; }


        private ICollection<Employee> _employees;

        public ICollection<Employee> Employees
        {
            get => _employees;
            set 
            {
                if (_employees != null)
                {
                    UpdateRange(value, _employees);
                    return;
                } 
                _employees = value ??  throw new ArgumentNullException(nameof(value));
            }
        }

        private ICollection<Payout> _payouts;

        public ICollection<Payout> Payouts
        {
            get => _payouts;
            set 
            {
                if (_payouts != null)
                {
                    UpdateRange(value, _payouts);
                    return;
                }
                _payouts = value ?? throw new ArgumentNullException(nameof(Payouts));
            }
        }

        private void UpdateRange<T>(ICollection<T> newRange, ICollection<T> oldRange) where T : EntityBase
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

        public decimal GetSumOfPayouts() => Payouts.Sum(x => x.Ammount);

        public DateTime DateCreate { get; set; }  
    }
}
