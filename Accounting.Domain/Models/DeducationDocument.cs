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
            { _deducationsBetEmployee = value ?? throw new ArgumentNullException(); }
        }

        private ICollection<DeducationNotBetEmployee> _deducationsNotBetEmployee = null!;

        public ICollection<DeducationNotBetEmployee> DeducationsNotBetEmployee
        {
            get => _deducationsNotBetEmployee;
            set
            { _deducationsNotBetEmployee = value ?? throw new ArgumentNullException(nameof(DeducationsNotBetEmployee)); }
        }

        private ICollection<NotBetEmployee> _notBetEmployees = null!;

        public ICollection<NotBetEmployee> NotBetEmployees
        {
            get => _notBetEmployees;
            set 
            {
                _notBetEmployees = value ?? throw new ArgumentNullException(nameof(NotBetEmployees)); 
            }
        }
        private ICollection<BetEmployee> _betEmployees = null!;

        public ICollection<BetEmployee> BetEmployees
        {
            get => _betEmployees;
            set { _betEmployees = value ?? throw new ArgumentNullException(nameof(BetEmployees)); }
        }


    }
}
