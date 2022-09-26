using Accounting.Domain.Models;
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
    }
}
