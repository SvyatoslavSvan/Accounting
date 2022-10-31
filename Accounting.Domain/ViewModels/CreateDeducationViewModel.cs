using Accounting.Domain.Models;
using Accounting.Domain.Models.Base;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Accounting.Domain.ViewModels
{
    public class CreateDeducationViewModel
    {
        [BindNever]
        public List<DeducationBase> Deducations { get; set; }
        public Guid EmployeeId { get; set; }
        public decimal Ammount { get; set; }
        public bool IsAdditional { get; set; }
    }
}
