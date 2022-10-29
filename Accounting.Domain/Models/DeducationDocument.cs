namespace Accounting.Domain.Models
{
    public class DeducationDocument
    {
        public DeducationDocument() { }
        public DeducationDocument(string name, DateTime dateCreate, ICollection<Deducation> deducations, 
            ICollection<NotBetEmployee> notBetEmployees, ICollection<BetEmployee> betEmployees)
        {

        }
        public Guid Id { get; private set; }
        public DateTime DateCreate { get; set; }
        public string Name { get; set; } = null!;

        private ICollection<Deducation> _deducations = null!;

        public ICollection<Deducation> Deducations
        {
            get => _deducations;
            set 
            { _deducations = value ?? throw new ArgumentNullException(nameof(Deducations)); }
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
