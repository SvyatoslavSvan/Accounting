using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Accounting.Domain.ViewModels
{
    public class UpdateDeducationViewModel
    {
        public Guid Id { get; set; }
        public decimal Ammount { get; set; }
        [BindNever]
        public bool IsAdditional { get; set; }
    }
}
