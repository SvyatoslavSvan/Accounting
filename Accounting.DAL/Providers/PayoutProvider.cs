﻿using Accounting.DAL.Contexts;
using Accounting.DAL.Interfaces;
using Accounting.DAL.Providers.BaseProvider;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using Accounting.Domain.Models.Base;
using Calabonga.UnitOfWork;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Accounting.DAL.Providers
{
    public class PayoutProvider : ProviderBase, IPayoutProvider
    {
#nullable disable
        public PayoutProvider(IUnitOfWork<ApplicationDBContext> unitOfWork, ILogger<PayoutBase> logger) : base(unitOfWork, logger) { }

        public async Task<BaseResult<bool>> Create(PayoutBase entity)
        {
            if (entity is PayoutBetEmployee payoutBetEmployee)
            {
                _unitOfWork.DbContext.Attach(payoutBetEmployee.BetEmployee);
                await _unitOfWork.GetRepository<PayoutBetEmployee>().InsertAsync(payoutBetEmployee);
            }
            if (entity is PayoutNotBetEmployee payoutNotBetEmployee)
            {
                _unitOfWork.DbContext.Attach(payoutNotBetEmployee.Employee);
                await _unitOfWork.GetRepository<PayoutNotBetEmployee>().InsertAsync(payoutNotBetEmployee);
            }
            await _unitOfWork.SaveChangesAsync();
            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                return HandleException<bool>();
            }
            return new BaseResult<bool>(true, true, OperationStatuses.Ok);
        }

        public async Task<BaseResult<bool>> Delete(Guid id)
        {
            var count = await _unitOfWork.GetRepository<PayoutBetEmployee>().CountAsync(predicate: x => x.Id == id);
            if (count == 0)
            {
                _unitOfWork.GetRepository<PayoutNotBetEmployee>().Delete(id);
            }
            else
            {
                _unitOfWork.GetRepository<PayoutBetEmployee>().Delete(id);
            }
            await _unitOfWork.SaveChangesAsync();
            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                return HandleException<bool>();
            }
            return new BaseResult<bool>(true, true, OperationStatuses.Ok);
        }

        public async Task<BaseResult<bool>> DeleteRange(List<PayoutBase> deducationBases)
        {
            var deducationsBetEmployee = new List<PayoutBetEmployee>();
            var deducationsNotBetEmployee = new List<PayoutNotBetEmployee>();
            deducationBases.ForEach(x =>
            {
                if (x is PayoutBetEmployee xBet)
                    deducationsBetEmployee.Add(xBet);
                if (x is PayoutNotBetEmployee xNotBet)
                    deducationsNotBetEmployee.Add(xNotBet);
            });
            _unitOfWork.GetRepository<PayoutBetEmployee>().Delete(deducationsBetEmployee);
            _unitOfWork.GetRepository<PayoutNotBetEmployee>().Delete(deducationsNotBetEmployee);
            await _unitOfWork.SaveChangesAsync();
            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                return HandleException<bool>();
            }
            return new BaseResult<bool>(true, true, OperationStatuses.Ok);
        }

        public async Task<BaseResult<List<PayoutBase>>> GetAll()
        {
            try
            {
                var deducations = new List<PayoutBase>();
                deducations.AddRange(await _unitOfWork.GetRepository<PayoutBetEmployee>().GetAllAsync(
                    disableTracking: true));
                deducations.AddRange(await _unitOfWork.GetRepository<PayoutNotBetEmployee>().GetAllAsync(true));
                return new BaseResult<List<PayoutBase>>(true, deducations, OperationStatuses.Ok);
            }
            catch (Exception ex)
            {
                return HandleException<List<PayoutBase>>(ex);
            }
        }

        public Task<BaseResult<IList<PayoutBase>>> GetAllByPredicate(Expression<Func<PayoutBetEmployee, bool>> predicatePayoutBetEmployee, 
            Expression<Func<PayoutNotBetEmployee, bool>> predicatePayoutNotBetEmployee)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResult<PayoutBase>> GetById(Guid id)
        {
            try
            {
                var count = await _unitOfWork.GetRepository<PayoutBetEmployee>().CountAsync(predicate: x => x.Id == id);
                if (count == 0)
                {
                    return new BaseResult<PayoutBase>(true, await _unitOfWork.
                        GetRepository<PayoutNotBetEmployee>().
                        GetFirstOrDefaultAsync(predicate: x => x.Id == id), OperationStatuses.Ok);
                }
                else
                {
                    return new BaseResult<PayoutBase>(true, await
                        _unitOfWork.GetRepository<PayoutBetEmployee>()
                        .GetFirstOrDefaultAsync(predicate: x => x.Id == id), OperationStatuses.Ok);
                }
            }
            catch (Exception ex)
            {
                return HandleException<PayoutBase>(ex);
            }
        }

        public async Task<BaseResult<bool>> Update(PayoutBase entity)
        {
            if (entity is PayoutBetEmployee deducationBetEmployee)
            {
                _unitOfWork.DbContext.Attach(deducationBetEmployee.BetEmployee);
                _unitOfWork.GetRepository<PayoutBetEmployee>().Update(deducationBetEmployee);
            }
            if (entity is PayoutNotBetEmployee deducationNotBetEmployee)
            {
                _unitOfWork.DbContext.Attach(deducationNotBetEmployee);
                _unitOfWork.GetRepository<PayoutNotBetEmployee>().Update(deducationNotBetEmployee);
            }
            await _unitOfWork.SaveChangesAsync();
            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                return HandleException<bool>();
            }
            return new BaseResult<bool>(true, true, OperationStatuses.Ok);
        }
    }
}
