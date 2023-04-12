using Accounting.ModelBinders;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.Domain.ViewModels
{
    public class UpdateWorkDayViewModel
    {
        public Guid Id { get; set; }
        [BindProperty(BinderType = typeof(FloatModelBinder))]
        public float Hours { get; set; }
    }
}
