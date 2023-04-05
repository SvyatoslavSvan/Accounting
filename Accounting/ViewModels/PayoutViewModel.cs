using Accounting.ModelBinders;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Accounting.Domain.ViewModels
{
    public class PayoutViewModel
    {
        public Guid EmployeeId { get; set; }

        public bool IsAdditional { get; set; }

        [BindProperty(BinderType = typeof(DecimalModelBinder))]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal Ammount { get; set; }
    }

    
}
