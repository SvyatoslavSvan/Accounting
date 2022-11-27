using Accounting.Domain.Models.Base;
using System.Runtime.CompilerServices;

namespace Accounting.Domain.Models
{
	public class Salary
    {
		public Salary() { }

		public Salary(decimal payment, decimal additionalPayout, decimal deducation, decimal additionalDeducation)
		{
			Payment = payment;
			AdditionalDeducation = additionalDeducation;
			Deducation = deducation;
			AdditionalPayout = additionalPayout;
		}

		private EmployeeBase _employee = null!;

		public EmployeeBase Employee
		{
			get => _employee;
			set => _employee = value ?? throw new ArgumentNullException();
        }

		public const decimal minMoneyOperationValue = 0;

		private decimal _payment;

		public decimal Payment
		{
			get => _payment;
			set
			{
				if (value < minMoneyOperationValue)
				{
					_payment = minMoneyOperationValue;
					return;
				}
				_payment = value;
			}
		}

		private decimal _additionalPayout;

		public decimal AdditionalPayout
		{
			get => _additionalPayout;
			set 
			{
				if (_additionalPayout < minMoneyOperationValue)
				{
					_additionalPayout = value;
					return;
				}
				_additionalPayout = value; 
			}
		}

		public decimal Premium => (Payment / 100m) * Employee.Premium;

		public decimal TotalAmmount => _payment + _additionalPayout + Premium;
		

		private decimal _deducation;

		public decimal Deducation
		{
			get => _deducation;
			set 
			{
				if (_deducation < minMoneyOperationValue)
				{
					_deducation = minMoneyOperationValue;
					return;
				}
				_deducation = value; 
			}
		}

        private decimal _additionalDeducation;

        public decimal AdditionalDeducation
        {
            get => _additionalDeducation;
            set
            {
                if (_additionalDeducation < minMoneyOperationValue)
                {
                    _additionalDeducation = minMoneyOperationValue;
                    return;
                }
                _additionalDeducation = value;
            }
        }

		public decimal TotalDeducation => _deducation + _additionalDeducation;
		public decimal Total => TotalAmmount - TotalDeducation;
    }
}
