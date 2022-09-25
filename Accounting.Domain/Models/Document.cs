namespace Accounting.Domain.Models
{
    public class Document
    {
#nullable disable
        public Document(string name, DateTime dateCreate)
        {
            Name = name;
            DateCreate = dateCreate;
        }
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public List<NotBetEmployee> Employees { get; set; }
        public DateTime DateCreate { get; private set; }  
        public List<Accrual> Accruals { get; private set; }
        public void AddEmployeesToDocument(List<NotBetEmployee> employees)
        {
            if (employees.Count() == 0)
                Employees = employees;
            else
              Employees.AddRange(employees);
        }

    }
}
