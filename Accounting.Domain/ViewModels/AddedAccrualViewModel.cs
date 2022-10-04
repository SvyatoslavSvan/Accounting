namespace Accounting.Domain.ViewModels
{
    public class AddedAccrualViewModel
    {
        public AddedAccrualViewModel(int ammount, Guid accrualId)
        {
            Ammount = ammount;
            AccrualId = accrualId;
        }

        public int Ammount { get; set; }
        public Guid AccrualId { get; set; }
    }
}
