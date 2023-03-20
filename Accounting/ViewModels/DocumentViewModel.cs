using Accounting.Domain.Enums;
using Accounting.Domain.Models.Base;
using Accounting.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Accounting.Domain.ViewModels
{
    public class DocumentViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime DateCreate { get; set; }
        public DocumentType DocumentType { get; set; }
        [BindNever]
        public decimal SumOfpayouts { get; set; }
        [BindNever]
        public IEnumerable<AddEmployeeViewModel> Employees { get; set; }
    }
}
