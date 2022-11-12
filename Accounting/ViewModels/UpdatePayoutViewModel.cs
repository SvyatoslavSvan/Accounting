using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Accounting.Domain.ViewModels
{
    public class UpdatePayoutViewModel 
    {
        [BindNever]
        public bool IsAdditional { get; set; }
        public decimal Ammount { get; set; }
        public Guid PayoutId { get; set; }
    }
}
