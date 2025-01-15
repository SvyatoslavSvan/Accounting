using Accounting.Domain.Models.Base;

namespace Accounting.Domain.Models
{
    public class Salary
    {
		public Salary() { }

		public Salary(decimal payment, decimal additionalPayout, decimal deducation, decimal additionalDeducation, Employee employee)
		{
			Payment = payment;
			AdditionalDeducation = additionalDeducation;
			Deducation = deducation;
			AdditionalPayout = additionalPayout;
			Employee = employee;
		}

		private Employee _employee = null!;

		private decimal _payment;

		private decimal _additionalPayout;

		private decimal _deducation;

        private decimal _additionalDeducation;

		public Employee Employee
		{
			get => _employee;
			set => _employee = value ?? throw new ArgumentNullException();
        }

		public decimal Payment
		{
			get => _payment;
			set
			{
				_payment = value;
			}
		}

		public decimal AdditionalPayout
		{
			get => _additionalPayout;
			set 
			{
				_additionalPayout = value; 
			}
		}

		public decimal Deducation
		{
			get => _deducation;
			set 
			{
				_deducation = value; 
			}
		}

        public decimal AdditionalDeducation
        {
            get => _additionalDeducation;
            set
            {
                _additionalDeducation = value;
            }
        }

		public decimal Premium => (Payment / 100m) * Employee.Premium;

		public decimal TotalAmmount => _payment + _additionalPayout + Premium;

		public decimal TotalDeducation => _deducation + _additionalDeducation;

		public decimal Total => TotalAmmount - TotalDeducation;
    }
}
