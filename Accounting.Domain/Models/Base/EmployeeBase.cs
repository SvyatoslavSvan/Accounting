﻿namespace Accounting.Domain.Models.Base
{
#nullable disable
    public abstract class EmployeeBase
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Group Group { get; private set; }
        public Guid GroupId { get; private set; }
        public string InnerId { get; private set; }
        public EmployeeBase(string name, string innerId)
        {
            Name = name ?? throw new ArgumentNullException(nameof(Name));
            InnerId = innerId ?? throw new ArgumentNullException(nameof(InnerId));
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
