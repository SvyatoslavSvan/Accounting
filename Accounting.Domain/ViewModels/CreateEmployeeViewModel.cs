using Accounting.Domain.Models;
using System.ComponentModel.DataAnnotations;
namespace Accounting.Domain.ViewModels
{
    public class CreateEmployeeViewModel
    {
        [Required(ErrorMessage ="Внутрений номер обязателен для ввода")]
        public string InnerId { get; set; } = null!;
        [Required(ErrorMessage = "Имя обязательно для ввода")]
        public string Name { get; set; } = null!;
        public decimal? Bet { get; set; }
        public bool IsBet { get; set; }
        [Required]
        public Guid GroupId { get; set; }
    }
}
