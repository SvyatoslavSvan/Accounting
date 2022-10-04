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
        public Document(List<NotBetEmployee> employees, List<Accrual> accruals, string name, DateTime dateCreate)
        {
            Employees = employees;
            Accruals = accruals;
            Name = name;
            DateCreate = dateCreate;
        }
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public List<NotBetEmployee> Employees { get; private set; }
        public List<Accrual> Accruals { get; private set; }
        public DateTime DateCreate { get; private set; }  
    }
}
