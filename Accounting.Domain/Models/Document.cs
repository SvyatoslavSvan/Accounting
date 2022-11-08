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
        public Document(List<NotBetEmployee> employees, List<AccrualNotBetEmployee> accruals, string name, DateTime dateCreate)
        {
            _notBetEmployees = employees;
            _accrualsNotBetEmployee = accruals;
            Name = name;
            DateCreate = dateCreate;
        }

        public Document(Guid id,List<NotBetEmployee> employees, List<AccrualNotBetEmployee> accruals, string name, DateTime dateCreate)
        {
            Id = id;
            _notBetEmployees = employees;
            _accrualsNotBetEmployee = accruals;
            Name = name;
            DateCreate = dateCreate;
        }

        //private void UpdateAccruals(List<AccrualNotBetEmployee> accruals)
        //{
        //    foreach (var item in accruals)
        //    {
        //        var contains = _accrualsNotBetEmployee.Contains(_accrualsNotBetEmployee.FirstOrDefault(x => x.Id == item.Id));
        //        if (!contains)
        //        {
        //            var elements = _accrualsNotBetEmployee.RemoveAll(x => x.Id == item.Id);
        //            if (elements == 0)
        //            {
        //                AccrualsNotBetEmployee.Add(item);
        //            }
        //        }
        //    }
        //    var elementsToRemove = new List<Guid>();
        //    foreach (var thisAccrual in _accrualsNotBetEmployee)
        //    {
        //        var containsInUpdate = accruals.Contains(accruals.FirstOrDefault(x => x.Id == thisAccrual.Id));
        //        if (!containsInUpdate)
        //        {
        //            elementsToRemove.Add(thisAccrual.Id);
        //        }
        //    }
        //    foreach (var item in elementsToRemove)
        //        _accrualsNotBetEmployee.RemoveAll(x => x.Id == item);
        //}

        //private void UpdateEmployees(List<NotBetEmployee> employees)
        //{
        //    foreach (var item in employees)
        //    {
        //        var containsInThis = _employees.Contains(_employees.FirstOrDefault(x => x.Id == item.Id));
        //        if (!containsInThis)
        //        {
        //            var elements = _employees.RemoveAll(x => x.Id == item.Id);
        //            if (elements == 0)
        //            {
        //                _employees.Add(item);
        //            }
        //        }
        //    }
        //    var elementsToRemove = new List<Guid>();
        //    foreach (var thisEmployee in _employees)
        //    {
        //        var containsInUpdate = employees.Contains(employees.FirstOrDefault(x => x.Id == thisEmployee.Id));
        //        if (!containsInUpdate)
        //        { 
        //            elementsToRemove.Add(thisEmployee.Id);
        //        }
        //    }
        //    foreach (var item in elementsToRemove)
        //    {
        //        _employees.RemoveAll(x => x.Id == item);
        //    }
        //}

        public Guid Id { get; private set; }
        public string Name { get; set; }
        private ICollection<NotBetEmployee> _notBetEmployees;

        public ICollection<NotBetEmployee> NotBetEmployees
        {
            get => _notBetEmployees;
            set 
            {
                //UpdateEmployees(value); 
            }
        }
        private ICollection<BetEmployee> _betEmployees;

        public ICollection<BetEmployee> BetEmployees
        {
            get { return _betEmployees; }
            set { _betEmployees = value; }
        }


        private ICollection<AccrualNotBetEmployee> _accrualsNotBetEmployee;

        public ICollection<AccrualNotBetEmployee> AccrualsNotBetEmployee
        {
            get => _accrualsNotBetEmployee;
            set 
            {
                //UpdateAccruals(value); 
            }
        }

        private ICollection<AccrualBetEmployee> _accrualBetEmployee;    

        public ICollection<AccrualBetEmployee> AccrualsBetEmplyee
        {
            get => _accrualBetEmployee;
            set { _accrualBetEmployee = value ?? throw new ArgumentNullException(); }
        }

        public DateTime DateCreate { get; set; }  
    }
}
