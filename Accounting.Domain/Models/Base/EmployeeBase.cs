using Accounting.Domain.Enums;
using Microsoft.Extensions.Logging;
using System.Text.Json.Serialization;

namespace Accounting.Domain.Models.Base
{
#nullable disable
    public abstract class EmployeeBase : EntityBase
    {

        private string _name;

        public string Name
        {
            get => _name;
            set 
            {
                if (string.IsNullOrEmpty(value))
                    return;
                _name = value; 
            }
        }

        public Group Group { get; protected set; }
        public Guid GroupId { get; protected set; }
        private string _innerId;

        public string InnerId
        {
            get => _innerId;
            set 
            {
                if (string.IsNullOrEmpty(value))
                    return;
                _innerId = value; 
            }
        }
        private int _premium;

        public int Premium
        {
            get => _premium;
            set
            {
                if (value > 100)
                    return;
                if (value < 1)
                    return;
                _premium = value;
            }
        }
        public EmployeeBase() { }

        [JsonConstructor]
        public EmployeeBase(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public EmployeeBase(string name, string innerId, int premium)
        {
            Name = name ?? throw new ArgumentNullException(nameof(Name));
            InnerId = innerId ?? throw new ArgumentNullException(nameof(InnerId));
        }
        public EmployeeBase(Guid id , Group group, string name , string innerId, int premium)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(Name));
            InnerId = innerId ?? throw new ArgumentNullException(nameof(InnerId));
            Group = group;
        }
       
        private ICollection<Document> _documents;

        [JsonIgnore]
        public ICollection<Document> Documents
        {
            get => _documents;
            set => _documents = value ?? throw new ArgumentNullException(); 
        }


        public virtual Salary CalculateSalary(DateTime from, DateTime to) => CalculatePayments(from, to);

        public virtual void AddToGroup(Group group)
        {
            if(group is null)
                throw new ArgumentNullException(nameof(group));
            Group = group;
            GroupId = group.Id;
        }

        public void SetId(Guid id)
        {
            if (string.IsNullOrEmpty(Name) && string.IsNullOrEmpty(Name))
                return;
            if (this.Id == Guid.Empty)
                this.Id = id;
        }

        private Salary CalculatePayments(DateTime from, DateTime to)
        {   
            if (this is BetEmployee)
            { 
                return GetSalary<PayoutBetEmployee>(from, to);
            }
            if (this is NotBetEmployee)
            {
                return GetSalary<PayoutNotBetEmployee>(from, to);
            }
            return new Salary();
        }

        private Salary GetSalary<T>(DateTime from, DateTime to) where T : PayoutBase
        {
            var payoutAccruals = new List<T>();
            var payoutsDeducation = new List<T>();
            var documents = _documents.Where(x => x.DateCreate.Date >= from.Date && x.DateCreate.Date <= to.Date).ToList();
            documents.ForEach(x =>
            {
                var type = typeof(T);
                if (type == typeof(PayoutBetEmployee))
                {
                    if (x.DocumentType == DocumentType.Accrual)
                    {
                        payoutAccruals.AddRange((IEnumerable<T>)x.PayoutsBetEmployees.Where(x => x.EmployeeId == this.Id));
                    }
                    if (x.DocumentType == DocumentType.Deducation)
                    {
                        payoutsDeducation.AddRange((IEnumerable<T>)x.PayoutsBetEmployees.Where(x => x.EmployeeId == this.Id));
                    }
                }
                if (type == typeof(PayoutNotBetEmployee))
                {
                    if (x.DocumentType == DocumentType.Accrual)
                    {
                        payoutAccruals.AddRange((IEnumerable<T>)x.PayoutsNotBetEmployees.Where(x => x.EmployeeId == this.Id ));
                    }
                    if (x.DocumentType == DocumentType.Deducation)
                    {
                        payoutsDeducation.AddRange((IEnumerable<T>)x.PayoutsNotBetEmployees.Where(x => x.EmployeeId == this.Id));
                    }
                }

            });
            var sumOfAccruals = payoutAccruals.Where(x => !x.IsAdditional).Sum(x => x.Ammount);
            var sumOfDeducations = payoutsDeducation.Where(x => !x.IsAdditional).Sum(x => x.Ammount);
            var sumOfAdditionalPayouts = payoutAccruals.Where(x => x.IsAdditional).Sum(x => x.Ammount);
            var sumofAdditionalDeducations = payoutsDeducation.Where(x => x.IsAdditional).Sum(x => x.Ammount);
            return new Salary(sumOfAccruals, sumOfAdditionalPayouts, sumOfDeducations, sumofAdditionalDeducations, this);
        }
        
    }
}
