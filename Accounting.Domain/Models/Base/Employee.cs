using Accounting.Domain.Enums;
using System.Text.Json.Serialization;

namespace Accounting.Domain.Models.Base
{
#nullable disable
    public class Employee : EntityBase
    {
        private string _name;

        private string _innerId;

        private int _premium;

        private ICollection<Document> _documents;

        public Group Group { get; protected set; }

        public Guid GroupId { get; protected set; }

        public Employee() { }

        [JsonConstructor]
        public Employee(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Employee(string name, string innerId, int premium)
        {
            Name = name ?? throw new ArgumentNullException(nameof(Name));
            InnerId = innerId ?? throw new ArgumentNullException(nameof(InnerId));
        }

        public Employee(Guid id , Group group, string name , string innerId, int premium)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(Name));
            InnerId = innerId ?? throw new ArgumentNullException(nameof(InnerId));
            Group = group;
        }
       

        [JsonIgnore]
        public ICollection<Document> Documents
        {
            get => _documents;
            set => _documents = value ?? throw new ArgumentNullException(); 
        }

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

        public virtual Salary CalculateSalary(DateTime from, DateTime to) => GetSalary(from, to);

        public virtual void AddToGroup(Group group)
        {
            if (group is null)
                throw new ArgumentNullException(nameof(group));
            Group = group;
            GroupId = group.Id;
        }

        private Salary GetSalary(DateTime from, DateTime to) 
        {
            var payoutAccruals = new List<Payout>();
            var payoutsDeducation = new List<Payout>();
            var documents = _documents.Where(x => x.DateCreate.Date >= from.Date && x.DateCreate.Date <= to.Date).ToList();
            documents.ForEach(x =>
            {
                if (x.DocumentType == DocumentType.Accrual)
                {
                    foreach (var item in x.Payouts)
                    {
                        payoutAccruals.Add(item);
                    }
                }
                if(x.DocumentType == DocumentType.Deducation)
                {
                    foreach (var item in x.Payouts)
                    {
                        payoutsDeducation.Add(item);
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
