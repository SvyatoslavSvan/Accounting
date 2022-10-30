using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Accounting.Domain.ViewModels
{
    public class UpdateDeducationlViewModel
    {
        [BindNever]
        public bool IsAdditional { get; set; }
        public decimal Ammount { get; set; }
        public Guid DeducationId { get; set; }
    }
}
