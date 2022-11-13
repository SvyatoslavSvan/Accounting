using Accounting.Domain.Models;
using Accounting.Domain.Models.Base;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Accounting.Domain.ViewModels
{
    public class UpdateDocumentViewModel : DocumentViewModel
    {
#nullable disable
        public Guid Id { get; set; }
        [BindNever]
        public List<EmployeeBase> EmployeesInDocument { get; set; }
        [BindNever]
        public List<EmployeeBase> EmployeesAddToDocument { get; set; }
    }
}
