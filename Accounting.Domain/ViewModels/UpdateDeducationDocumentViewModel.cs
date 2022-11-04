using Accounting.Domain.Models.Base;

namespace Accounting.Domain.ViewModels
{
    public class UpdateDeducationDocumentViewModel
    {
        public UpdateDeducationDocumentViewModel()
        {
        }

        public List<EmployeeBase> EmployeesInDocument { get; set; }
        public List<EmployeeBase> EmployeesAddToDocument { get; set; }
        public string Name { get; set; }
        public DateTime DateCreate { get; set; }
    }
}