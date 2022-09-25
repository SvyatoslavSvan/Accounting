using System.ComponentModel.DataAnnotations;

namespace Accounting.Domain.ViewModels
{
    public class GroupViewModel
    {
        [Required(ErrorMessage = "Імя групи обовязково для заповнення")]
        public string Name { get; set; }
    }
}
