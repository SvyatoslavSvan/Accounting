﻿using Accounting.Domain.Models;
using Accounting.Domain.ViewModels;

namespace Accounting.Services.Interfaces
{
    public interface ISessionDocumentService
    {
        public Task<bool> AddEmployee(NotBetEmployee employee);
        public Document GetDocument(DocumentViewModel documentViewModel);
        public Task<bool> Clear();
        public Task<bool> CreateSessionDocument();
        public Task<bool> AddAccrual(Accrual accrual);
        public decimal SumOfAccruals();
        public Task<bool> DeleteEmployee(Guid Id);
        public Task<bool> DeleteAccrualsByEmployeeId(Guid Id);
        public List<Accrual> GetAccrualsByEmployeeId(Guid employeeId);
        public Task<bool> UpdateAccrual(decimal ammount, Guid accrualId);
    }
}
