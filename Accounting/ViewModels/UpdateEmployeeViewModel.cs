using System.ComponentModel.DataAnnotations;

namespace Accounting.Domain.ViewModels
{
    public class UpdateEmployeeViewModel
    {
#nullable disable
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Guid GroupId { get; set; }
        [Required]
        public string InnerId { get; set; }
        public bool IsBet { get; set; }
        public decimal? Bet { get; set; }
    }
}
