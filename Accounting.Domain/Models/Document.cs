﻿namespace Accounting.Domain.Models
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

        public Document(Guid id,List<NotBetEmployee> employees, List<Accrual> accruals, string name, DateTime dateCreate)
        {
            Id = id;
            Employees = employees;
            Accruals = accruals;
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
                var contains = Accruals.Contains(Accruals.FirstOrDefault(x => x.Id == item.Id));
                if (!contains)
                {
                    var elements = Accruals.RemoveAll(x => x.Id == item.Id);
                    if (elements == 0)
                    {
                        Accruals.Add(item);
                    }
                }
            }
            var elementsToRemove = new List<int>();
            foreach (var thisAccrual in Accruals)
            {
                var containsInUpdate = accruals.Contains(accruals.FirstOrDefault(x => x.Id == thisAccrual.Id));
                if (!containsInUpdate)
                {
                    elementsToRemove.Add(Accruals.IndexOf(thisAccrual));
                }
            }
            foreach (var item in elementsToRemove)
                Accruals.RemoveAt(item);
        }

        private void UpdateEmployees(List<NotBetEmployee> employees)
        {
            foreach (var item in employees)
            {
                var containsInThis = Employees.Contains(Employees.FirstOrDefault(x => x.Id == item.Id));
                if (!containsInThis)
                {
                    var elements = Employees.RemoveAll(x => x.Id == item.Id);
                    if (elements == 0)
                    {
                        Employees.Add(item);
                    }
                }
            }
            var elementsToRemove = new List<int>();
            foreach (var thisEmployee in Employees)
            {
                var containsInUpdate = employees.Contains(employees.FirstOrDefault(x => x.Id == thisEmployee.Id));
                if (!containsInUpdate)
                { 
                    elementsToRemove.Add(Employees.IndexOf(thisEmployee));
                }
            }
            foreach (var item in elementsToRemove)
                Employees.RemoveAt(item);
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public List<NotBetEmployee> Employees { get; private set; }
        public List<Accrual> Accruals { get; private set; }
        public DateTime DateCreate { get; private set; }  
    }
}
