using Accounting.Domain.Enums;
using Accounting.Domain.Models.Base;
using System.Text.Json.Serialization;

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

        public Document(ICollection<NotBetEmployee> notBetEmployees, ICollection<BetEmployee> betEmployees, 
            ICollection<PayoutBetEmployee> payoutsBetEmployees, ICollection<PayoutNotBetEmployee> payoutsNotBetEmployees, string name, DateTime dateCreate, DocumentType documentType)
        {
            _betEmployees = betEmployees;
            _notBetEmployees = notBetEmployees;
            _payoutsBetEmployee = payoutsBetEmployees;
            _payoutsNotBetEmployee = payoutsNotBetEmployees;
            Name = name;
            DateCreate = dateCreate;
            DocumentType = documentType;
        }

        public DocumentType DocumentType { get; private set; }
        
        public string Name { get; set; }
        private ICollection<NotBetEmployee> _notBetEmployees;

        public ICollection<NotBetEmployee> NotBetEmployees
        {
            get => _notBetEmployees;
            set 
            {
                if (_notBetEmployees != null)
                {
                    UpdateRange(value, _notBetEmployees);
                    return;
                }
                _notBetEmployees = value ?? throw new ArgumentNullException(nameof(NotBetEmployees));
            }
        }
        private ICollection<BetEmployee> _betEmployees;

        public ICollection<BetEmployee> BetEmployees
        {
            get => _betEmployees;
            set 
            {
                if (_betEmployees != null)
                {
                    UpdateRange(value, _betEmployees);
                    return;
                }
                _betEmployees = value ?? throw new ArgumentNullException(nameof(BetEmployees));
            }
        }


        private ICollection<PayoutNotBetEmployee> _payoutsNotBetEmployee;

        public ICollection<PayoutNotBetEmployee> PayoutsNotBetEmployees
        {
            get => _payoutsNotBetEmployee;
            set 
            {
                if (_payoutsNotBetEmployee != null)
                {
                    UpdateRange(value, _payoutsNotBetEmployee);
                    return;
                }
                _payoutsNotBetEmployee = value ?? throw new ArgumentNullException(nameof(PayoutsNotBetEmployees));
            }
        }

        private ICollection<PayoutBetEmployee> _payoutsBetEmployee;    

        public ICollection<PayoutBetEmployee> PayoutsBetEmployees
        {
            get => _payoutsBetEmployee;
            set 
            {
                if (_payoutsBetEmployee != null)
                {
                    UpdateRange(value, _payoutsBetEmployee);
                    return;
                }
                _payoutsBetEmployee = value ?? throw new ArgumentNullException(nameof(PayoutsNotBetEmployees));
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

        public DateTime DateCreate { get; set; }  
    }
}
