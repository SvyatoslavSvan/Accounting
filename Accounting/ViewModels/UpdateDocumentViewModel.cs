using Accounting.Domain.Models.Base;
using Accounting.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Accounting.Domain.ViewModels
{
    public class UpdateDocumentViewModel : DocumentViewModel
    {
#nullable disable
        public Guid Id { get; set; }
        [BindNever]
        public List<Payout> Payouts { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
