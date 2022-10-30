﻿using Accounting.Domain.Models;
using Accounting.Domain.Models.Base;

namespace Accounting.Domain.SessionEntity
{
    public class SessionDeducationDocument
    {
        
        public List<Deducation> Deducations { get; set; } = new List<Deducation>();
        public List<BetEmployee> BetEmployees { get; set; } = new List<BetEmployee>();
        public List<NotBetEmployee> NotBetEmployees { get; set; } = new List<NotBetEmployee>(); 
        public EmployeeBase GetEmployee(Guid id)
        {
            EmployeeBase employee;
            employee = BetEmployees.FirstOrDefault(x => x.Id == id);
            if (employee is null)
            {
                employee = NotBetEmployees.FirstOrDefault(x => x.Id == id);
            }
            return employee;
        }
        public void RemoveEmployee(EmployeeBase employee)
        {
            if (employee is BetEmployee)
            {
                BetEmployees.RemoveAll(x => x.Id == employee.Id);
                return;
            }
            if (employee is NotBetEmployee)
            {
                NotBetEmployees.RemoveAll(x => x.Id == employee.Id);
            }
        }
    }
}
