using Accounting.Domain.Models.Base;

namespace Accounting.Domain.Models
{
    public class Deducation
    {

		public Deducation() { }
		public Deducation(decimal ammount, bool isAdditional, EmployeeBase employee)
		{
			Ammount = ammount;
			IsAdditional = isAdditional;
			if (employee is NotBetEmployee notBetEmployee)
			{
				NotBetEmployee = notBetEmployee;
			}
			if(employee is BetEmployee betEmployee)
			{
				BetEmployee = betEmployee;
			}
		} 
        public Guid Id { get; private set; }

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

		private NotBetEmployee? _notBetEmployee;

		public NotBetEmployee? NotBetEmployee
		{
			get => _notBetEmployee;
			set 
			{
				if (_betEmployee == null)
				{
					_notBetEmployee = value ?? throw new ArgumentNullException();
				} 
			}
		}

		private DeducationDocument? _document;

		public DeducationDocument? Document
		{
			get => _document;
			set 
			{ 
				if(_document != null)
					return;
				_document = value; 
			}
		}

		public bool IsAdditional { get; set; }

		private BetEmployee _betEmployee;

		public BetEmployee BetEmployee
		{
			get => _betEmployee;
			set 
			{
				if (_notBetEmployee == null)
				{
					_betEmployee = value ?? throw new ArgumentNullException();
				} 
			}
		}


	}
}
