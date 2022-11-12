using Accounting.Domain.Enums;
using Accounting.Domain.Models.Base;
using System.Text.Json.Serialization;

namespace Accounting.Domain.Models
{
    public class Document
    {
#nullable disable
        public Document() { }
        
        public Document(string name, DateTime dateCreate)
        {
            Name = name;
            DateCreate = dateCreate;
        }

        [JsonConstructor]
        public Document(ICollection<NotBetEmployee> notBetEmployees, ICollection<BetEmployee> betEmployees, ICollection<PayoutBetEmployee> payoutsBetEmployees, ICollection<PayoutNotBetEmployee> payoutsNotBetEmployees)
        {
            _betEmployees = betEmployees;
            _notBetEmployees = notBetEmployees;
            _payoutsBetEmployee = payoutsBetEmployees;
            _payoutsNotBetEmployee = payoutsNotBetEmployees;
        }

        public Document(List<NotBetEmployee> employees, List<PayoutNotBetEmployee> accruals, string name, DateTime dateCreate)
        {
            _notBetEmployees = employees;
            _payoutsNotBetEmployee = accruals;
            Name = name;
            DateCreate = dateCreate;
        }

        public Document(Guid id,List<NotBetEmployee> employees, List<PayoutNotBetEmployee> accruals, string name, DateTime dateCreate)
        {
            Id = id;
            _notBetEmployees = employees;
            _payoutsNotBetEmployee = accruals;
            Name = name;
            DateCreate = dateCreate;
        }

        public DocumentType DocumentType { get; private set; }
        public Guid Id { get; private set; }
        public string Name { get; set; }
        private ICollection<NotBetEmployee> _notBetEmployees;

        public ICollection<NotBetEmployee> NotBetEmployees
        {
            get => _notBetEmployees;
            set 
            {
                _notBetEmployees = value ?? throw new ArgumentNullException(); 
            }
        }
        private ICollection<BetEmployee> _betEmployees;

        public ICollection<BetEmployee> BetEmployees
        {
            get { return _betEmployees; }
            set { _betEmployees = value; }
        }


        private ICollection<PayoutNotBetEmployee> _payoutsNotBetEmployee;

        public ICollection<PayoutNotBetEmployee> PayoutsNotBetEmployees
        {
            get => _payoutsNotBetEmployee;
            set 
            {
                //UpdateAccruals(value); 
            }
        }

        private ICollection<PayoutBetEmployee> _payoutsBetEmployee;    

        public ICollection<PayoutBetEmployee> PayoutsBetEmployees
        {
            get => _payoutsBetEmployee;
            set { _payoutsBetEmployee = value ?? throw new ArgumentNullException(); }
        }

        public void Initialize()
        {
            _betEmployees = new List<BetEmployee>();
            _notBetEmployees = new List<NotBetEmployee>();
            _payoutsBetEmployee = new List<PayoutBetEmployee>();
            _payoutsNotBetEmployee = new List<PayoutNotBetEmployee>();
        }
        public DateTime DateCreate { get; set; }  
    }
}
