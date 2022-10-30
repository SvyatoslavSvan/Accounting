using Accounting.Domain.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Accounting.Domain.ViewModels
{
    public class CreateDeducationViewModel
    {
        [BindNever]
        public List<Deducation> Deducations { get; set; }
        public Guid EmployeeId { get; set; }
        public decimal Ammount { get; set; }
        public bool IsAdditional { get; set; }
    }
}
