using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Accounting.Domain.ViewModels
{
    public class UpdateAccrualViewModel 
    {
        [BindNever]
        public bool IsAdditional { get; set; }
        public decimal Ammount { get; set; }
        public Guid AccrualId { get; set; }
    }
}
