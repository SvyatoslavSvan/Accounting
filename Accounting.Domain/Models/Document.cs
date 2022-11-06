namespace Accounting.Domain.Models
{
    public class Document
    {
#nullable disable
        public Document() { }
        
        public Document(string name, DateTime dateCreate)
        {
            Name = name;
            DateCreate = dateCreate;
        }
        public Document(List<NotBetEmployee> employees, List<Accrual> accruals, string name, DateTime dateCreate)
        {
            _employees = employees;
            _accruals = accruals;
            Name = name;
            DateCreate = dateCreate;
        }

        public Document(Guid id,List<NotBetEmployee> employees, List<Accrual> accruals, string name, DateTime dateCreate)
        {
            Id = id;
            _employees = employees;
            _accruals = accruals;
            Name = name;
            DateCreate = dateCreate;
        }
        public void Update(Document document)
        {
            Name = document.Name;
            DateCreate = document.DateCreate;
            UpdateAccruals(document.Accruals);
            UpdateEmployees(document.Employees);
        }

        private void UpdateAccruals(List<Accrual> accruals)
        {
            foreach (var item in accruals)
            {
                var contains = _accruals.Contains(_accruals.FirstOrDefault(x => x.Id == item.Id));
                if (!contains)
                {
                    var elements = _accruals.RemoveAll(x => x.Id == item.Id);
                    if (elements == 0)
                    {
                        Accruals.Add(item);
                    }
                }
            }
            var elementsToRemove = new List<Guid>();
            foreach (var thisAccrual in _accruals)
            {
                var containsInUpdate = accruals.Contains(accruals.FirstOrDefault(x => x.Id == thisAccrual.Id));
                if (!containsInUpdate)
                {
                    elementsToRemove.Add(thisAccrual.Id);
                }
            }
            foreach (var item in elementsToRemove)
                _accruals.RemoveAll(x => x.Id == item);
        }

        private void UpdateEmployees(List<NotBetEmployee> employees)
        {
            foreach (var item in employees)
            {
                var containsInThis = _employees.Contains(_employees.FirstOrDefault(x => x.Id == item.Id));
                if (!containsInThis)
                {
                    var elements = _employees.RemoveAll(x => x.Id == item.Id);
                    if (elements == 0)
                    {
                        _employees.Add(item);
                    }
                }
            }
            var elementsToRemove = new List<Guid>();
            foreach (var thisEmployee in _employees)
            {
                var containsInUpdate = employees.Contains(employees.FirstOrDefault(x => x.Id == thisEmployee.Id));
                if (!containsInUpdate)
                { 
                    elementsToRemove.Add(thisEmployee.Id);
                }
            }
            foreach (var item in elementsToRemove)
            {
                _employees.RemoveAll(x => x.Id == item);
            }
        }

        public Guid Id { get; private set; }
        public string Name { get; set; }
        private List<NotBetEmployee> _employees;

        public List<NotBetEmployee> Employees
        {
            get => _employees;
            set 
            {
                UpdateEmployees(value); 
            }
        }
        private List<Accrual> _accruals;

        public List<Accrual> Accruals
        {
            get => _accruals;
            set 
            {
                UpdateAccruals(value); 
            }
        }


        public DateTime DateCreate { get; set; }  
    }
}
