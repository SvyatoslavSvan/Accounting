using Accounting.Domain.Enums;
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
    }
}
