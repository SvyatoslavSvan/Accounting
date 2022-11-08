﻿using Accounting.Domain.Models.Base;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace Accounting.Domain.Models
{
    public class NotBetEmployee : EmployeeBase
    {
#nullable disable
        [JsonConstructor]
        public NotBetEmployee(Guid id, string name) : base(id, name) { }
        private ICollection<AccrualNotBetEmployee> _accruals;

        public ICollection<AccrualNotBetEmployee> Accruals
        {
            get { return _accruals; }
            set { _accruals = value; }
        }


        private ICollection<DeducationNotBetEmployee> _deducationNotBetEmployee;

        public ICollection<DeducationNotBetEmployee> DeducationNotBetEmployee
        {
            get => _deducationNotBetEmployee;
            set { _deducationNotBetEmployee = value; }
        }
        public NotBetEmployee(string name, string innerId, int premium) : base(name, innerId, premium)
        {
            
        }

        public NotBetEmployee(Guid id, Group group, string name, string innerId, int premium) : base(id, group ,name, innerId, premium)
        {

        }

        public override void ToSerializable()
        {
            base.ToSerializable();
            this.Accruals = null;
            this.Documents = null;
        }

        public override Salary CalculateSalary()
        {
            throw new NotImplementedException();
        }
    }
}
