namespace Accounting.Domain.Models.Base
{
    public abstract class AccrualBase
    {
        public AccrualBase() {}
        public AccrualBase(decimal ammount, bool isAdditional)
        {
            Ammount = ammount;
            IsAdditional = isAdditional;
        }

        public AccrualBase(decimal ammount, Guid id, bool isAdditional)
        {
            Ammount = ammount;
            Id = id;
            IsAdditional = isAdditional;
        }

        public Guid Id { get; protected set; }
        public bool IsAdditional { get; protected set; }
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
        private Document _document;

        public Document Document
        {
            get => _document;
            set { _document = value ?? throw new ArgumentNullException(); }
        }

    }
}
