using Accounting.Domain.Interfaces;

namespace Accounting.Domain.Models.Base
{
    public abstract class DeducationBase : IJsonSerializable
    {
        public DeducationBase() { }
        public DeducationBase(decimal ammount,bool isAdditional)
        {
            Ammount = ammount;
            IsAdditional = isAdditional;
        }
        
        public Guid Id { get; protected set; }
        public bool IsAdditional { get; set; }

        private decimal _ammount;

        public decimal Ammount
        {
            get => _ammount;
            set
            {
                const int minValue = 0;
                if (value < minValue)
                {
                    _ammount = minValue;
                    return;
                }
                _ammount = value;
            }
        }

        private DeducationDocument? _document;

        public DeducationDocument? Document
        {
            get => _document;
            set
            {
                if (_document != null)
                    return;
                _document = value;
            }
        }

        public virtual void ToSerializable()
        {
            _document = null;
        }
    }
}
