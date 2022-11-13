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


        public abstract Salary CalculateSalary();

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
