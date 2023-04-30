﻿using Accounting.Domain.Models.Base;
using System.Text.Json.Serialization;

namespace Accounting.Domain.Models
{
    public class BetEmployee : EmployeeBase
    {
#nullable disable
        [JsonConstructor]
        public BetEmployee(Guid id, string name) : base(id, name) { }

        public BetEmployee() { }

        public BetEmployee(string name, decimal bet, string innerId, int premium) : base(name, innerId, premium)
        {
            Bet = bet;
        }

        public BetEmployee(Guid id, Group group, string name, string innerId, decimal bet, int premium) : base(id, group, name, innerId, premium)
        {
            Bet = bet;
        }

        private decimal _bet;

        private List<WorkDay> _workDays;

        private ICollection<Timesheet> _timesheets;

        private ICollection<PayoutBetEmployee> _accruals;

        public decimal Bet
        {
            get => _bet;
            set 
            {
                const int minValue = 0;
                if (value < minValue)
                {
                    _bet = value;
                    return;
                }
                _bet = value; 
            }
        }

        [JsonIgnore]
        public List<WorkDay> WorkDays
        {
            get => _workDays;

            set 
            { 
                _workDays = value; 
            }
        }

        public ICollection<Timesheet> Timesheets
        {
            get => _timesheets;
            set { _timesheets = value; }
        }


        [JsonIgnore]
        public ICollection<PayoutBetEmployee> Accruals
        {
            get { return _accruals; }
            set { _accruals = value ?? throw new ArgumentNullException(); }
        }

        
        public override Salary CalculateSalary(DateTime from, DateTime to)
        {
            var salary = base.CalculateSalary(from, to);
            salary.Payment += CalculateBetPayout(from, to);
            return salary;
        }

        private decimal CalculateBetPayout(DateTime from, DateTime to)
        {
            decimal payment = 0;
            var timesheets = Timesheets.Where(x => x.Date.Year >= from.Date.Year && x.Date.Year <= to.Date.Year && x.Date.Month >= from.Date.Month && x.Date.Month <= to.Date.Month).ToList();
            timesheets.ForEach(x =>
            {
                var payForHour = (Bet / x.DaysCount) / (decimal)(x.HoursCount / x.DaysCount);
                var workHours = x.WorkDays.Where(x => x.EmployeeId == this.Id).Sum(x => x.Hours);
                payment += (decimal)workHours * payForHour;
            });
            return payment; 
        }

       
    }
}
