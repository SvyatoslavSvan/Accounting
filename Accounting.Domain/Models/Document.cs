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
        public List<NotBetEmployee> Employees { get; private set; } 
        public DateTime DateCreate { get; private set; }  
        public void AddEmployeesToDocument(List<NotBetEmployee> employees)
        {
            if (Employees is null)
                Employees = employees;
            else
                Employees.AddRange(employees);
        }
        public void SetId(Guid newId)
        {
            if (Id == Guid.Empty)
                Id = newId;
        }
    }
}
