using Newtonsoft.Json;

namespace Accounting.Domain.Models.Base
{
#nullable disable
    public abstract class EmployeeBase
    {
        
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public Group Group { get; protected set; }
        public Guid GroupId { get; protected set; }
        public string InnerId { get; protected set; }

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

        private ICollection<Deducation> _deducations;

        public ICollection<Deducation> Deducations
        {
            get => _deducations;
            set { _deducations = value; }
        }
        public virtual void ToSerializable()
        {
            this.Deducations = null;
            this.Group = null;
            this.DeducationDocuments = null;
        }

        private ICollection<DeducationDocument> _deducationDocuments;

        public ICollection<DeducationDocument> DeducationDocuments
        {
            get => _deducationDocuments;
            set { _deducationDocuments = value; }
        }

        public abstract decimal CalculateSalary(DateTime from);
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
       
        
    }
}
