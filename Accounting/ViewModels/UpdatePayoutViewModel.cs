using Accounting.ModelBinders;
using Accounting.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Accounting.Domain.ViewModels
{
    public class UpdatePayoutViewModel : AddedEmployeeViewModel
    {
        public bool IsAdditional { get; set; }
        [BindProperty(BinderType = typeof(DecimalModelBinder))]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal Ammount { get; set; }
        public Guid PayoutId { get; set; }
    }
}
