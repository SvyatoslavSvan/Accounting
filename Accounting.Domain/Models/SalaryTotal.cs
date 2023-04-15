namespace Accounting.Domain.Models
{
    public class SalaryTotal
    {
        public SalaryTotal(IList<Salary> salaries)
        {
            SumOfPayments = salaries.Sum(x => x.Payment);
            SumOfAdditionalPayments = salaries.Sum(x => x.AdditionalPayout);
            SumOfPremium = salaries.Sum(x => x.Premium);
            SumOfDeducations = salaries.Sum(x => x.Deducation);
            SumOfAdditionalDeducations = salaries.Sum(x => x.AdditionalDeducation);
            TotalSumOfPayments = salaries.Sum(x => x.TotalAmmount);
            TotalSumOfDeducation = salaries.Sum(x => x.TotalDeducation);
            SumOfTotal = salaries.Sum(x => x.Total);
        }

        public decimal SumOfPayments { get; }

        public decimal SumOfAdditionalPayments { get; }

        public decimal SumOfPremium { get; }

        public decimal SumOfDeducations { get; }

        public decimal SumOfAdditionalDeducations { get; }

        public decimal TotalSumOfPayments { get; }

        public decimal TotalSumOfDeducation { get; }

        public decimal SumOfTotal { get; }
    }
}
