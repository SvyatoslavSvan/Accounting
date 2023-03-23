using System.Text.Json.Serialization;

namespace Accounting.Domain.Models.Base
{
    public abstract class PayoutBase : EntityBase
    {
        public PayoutBase() {}
        public PayoutBase(decimal ammount, bool isAdditional)
        {
            Ammount = ammount;
            IsAdditional = isAdditional;
        }

        [JsonConstructor]
        public PayoutBase(decimal ammount, Guid id, bool isAdditional)
        {
            Ammount = ammount;
            Id = id;
            IsAdditional = isAdditional;
        }
        public Guid EmployeeId { get; protected set; }

        public bool IsAdditional { get; set; }
        private decimal _ammount;
        public decimal Ammount
        {
            get => _ammount;
            set
            {
                const decimal minValue = 0;
                if (value < minValue)
                {
                    value = minValue;
                    return;
                }
                _ammount = value;
            }
        }
        private Document? _document;
        [JsonIgnore]
        public Document? Document
        {
            get => _document;
            set { _document = value ?? throw new ArgumentNullException(); }
        }

    }
}
