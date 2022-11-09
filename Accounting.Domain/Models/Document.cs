using Accounting.Domain.Enums;

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
        public Document(List<NotBetEmployee> employees, List<PayoutNotBetEmployee> accruals, string name, DateTime dateCreate)
        {
            _notBetEmployees = employees;
            _accrualsNotBetEmployee = accruals;
            Name = name;
            DateCreate = dateCreate;
        }

        public Document(Guid id,List<NotBetEmployee> employees, List<PayoutNotBetEmployee> accruals, string name, DateTime dateCreate)
        {
            Id = id;
            _notBetEmployees = employees;
            _accrualsNotBetEmployee = accruals;
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


        private ICollection<PayoutNotBetEmployee> _accrualsNotBetEmployee;

        public ICollection<PayoutNotBetEmployee> AccrualsNotBetEmployee
        {
            get => _accrualsNotBetEmployee;
            set 
            {
                //UpdateAccruals(value); 
            }
        }

        private ICollection<PayoutBetEmployee> _accrualBetEmployee;    

        public ICollection<PayoutBetEmployee> AccrualsBetEmplyee
        {
            get => _accrualBetEmployee;
            set { _accrualBetEmployee = value ?? throw new ArgumentNullException(); }
        }

        public DateTime DateCreate { get; set; }  
    }
}
