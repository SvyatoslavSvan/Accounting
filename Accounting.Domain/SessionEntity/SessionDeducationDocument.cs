using Accounting.Domain.Interfaces;
using Accounting.Domain.Models;
using Accounting.Domain.Models.Base;

namespace Accounting.Domain.SessionEntity
{
    public class SessionDeducationDocument : IJsonSerializable
    {
        public Guid Id { get; set; } = Guid.Empty;
        public List<DeducationBetEmployee> DeducationBetEmployees { get; set; } = new List<DeducationBetEmployee>();
        public List<DeducationNotBetEmployee> DeducationNotBetEmployees { get; set; } = new List<DeducationNotBetEmployee>();
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
        public List<DeducationBase> getDeducationsByEmployeeId(Guid id)
        {
            var deducations = new List<DeducationBase>();
            deducations.AddRange(DeducationNotBetEmployees.Where(x => x.EmployeeId == id));
            deducations.AddRange(DeducationBetEmployees.Where(x => x.EmployeeId == id));
            return deducations;
        }

        public void RemoveDeducation(Guid id)
        {
            var removedBetDeducation = DeducationBetEmployees.RemoveAll(x => x.Id == id);
            if (removedBetDeducation == 0)
            {
                DeducationNotBetEmployees.RemoveAll(x => x.Id == id);
            }
        }

        public void RemoveEmployee(EmployeeBase employee)
        {
            if (employee is BetEmployee)
            {
                BetEmployees.RemoveAll(x => x.Id == employee.Id);
                DeducationBetEmployees.RemoveAll(x => x.EmployeeId == employee.Id);
                return;
            }
            if (employee is NotBetEmployee)
            {
                NotBetEmployees.RemoveAll(x => x.Id == employee.Id);
                DeducationNotBetEmployees.RemoveAll(x => x.EmployeeId == employee.Id);
            }
        }

        public void UpdateDeducation(decimal ammount, Guid deducationId)
        {
            var deducationBetEmployee = DeducationBetEmployees.FirstOrDefault(x => x.Id == deducationId);
            if (deducationBetEmployee == null)
            {
                var deducationNotBetEmployee = DeducationNotBetEmployees.FirstOrDefault(x => x.Id == deducationId);
                deducationNotBetEmployee.Ammount = ammount;
                return;
            }
            deducationBetEmployee.Ammount = ammount;
        }

        public void AddDeducation(DeducationBase deducation)
        {
            if (deducation is DeducationBetEmployee deducationBetEmployee)
            {
                DeducationBetEmployees.Add(deducationBetEmployee);
                return;
            }
            if (deducation is DeducationNotBetEmployee deducationNotBetEmployee)
            {
                DeducationNotBetEmployees.Add(deducationNotBetEmployee);
            }
        }

        public void ToSerializable()
        {
            DeducationNotBetEmployees.ForEach(x => x.ToSerializable());
            DeducationBetEmployees.ForEach(x => x.ToSerializable());
            BetEmployees.ForEach(x => x.ToSerializable());
            NotBetEmployees.ForEach(x => x.ToSerializable());
        }
    }
}
