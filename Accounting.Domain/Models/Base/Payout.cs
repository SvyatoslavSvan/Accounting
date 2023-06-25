using System.Text.Json.Serialization;

namespace Accounting.Domain.Models.Base
{
    public class Payout : EntityBase
    {
        public Payout() {}

        public Payout(decimal ammount, bool isAdditional, Employee employee)
        {
            Ammount = ammount;
            IsAdditional = isAdditional;
            Employee = employee;
        }

        private Document? _document;

        private decimal _ammount;

        [JsonConstructor]
        public Payout(decimal ammount, Guid id, bool isAdditional, Guid employeeId)
        {
            Ammount = ammount;
            Id = id;
            IsAdditional = isAdditional;
            EmployeeId = employeeId;
        }

        public Guid EmployeeId { get; protected set; }

        public bool IsAdditional { get; set; }

        public decimal Ammount
        {
            get => _ammount;
            set
            {
                const decimal minValue = 0;
                if (value < minValue)
                {
                    _ammount = minValue;
                    return;
                }
                _ammount = value;
            }
        }

        [JsonIgnore]
        public Document? Document
        {
            get => _document;
            set { _document = value ?? throw new ArgumentNullException(); }
        }

        public Employee Employee { get; private set; }
    }
}
