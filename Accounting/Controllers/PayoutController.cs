using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using Accounting.Domain.Models.Base;
using Accounting.Domain.ViewModels;
using Accounting.Services;
using Accouting.Domain.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.Controllers
{
    public class PayoutController : Controller
    {
        private readonly ISessionDocumentService _sessionDocumentService;
        private readonly IEmployeeManager _employeeManager;
        private readonly IPayoutManager _payoutManager;
        public PayoutController(ISessionDocumentService sessionDocumentService, IEmployeeManager employeeManager, IPayoutManager payoutManager)
        {
            _sessionDocumentService = sessionDocumentService;
            _employeeManager = employeeManager;
            _payoutManager = payoutManager; 
        }

        [HttpGet]
        public IActionResult CreatePayout(Guid id)
        {
            return PartialView(new CreatePayoutViewModel()
            {
                Payouts = _sessionDocumentService.GetPayoutsByEmployeeId(id),
                EmployeeId = id
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetSumOfPayouts() => PartialView(_sessionDocumentService.GetSumOfPayouts());

        [HttpPost]
        public async Task<IActionResult> CreatePayout(PayoutViewModel viewModel)
        {
            var getEmployeeResult = await _employeeManager.GetById(viewModel.EmployeeId);
            if (getEmployeeResult.Succed)
            {
                PayoutBase payout;
                if (getEmployeeResult.Data is BetEmployee betEmployee)
                {
                    payout = new PayoutBetEmployee(viewModel.Ammount, viewModel.IsAdditional, betEmployee);
                }
                else
                {
                    payout = new PayoutNotBetEmployee(viewModel.Ammount, viewModel.IsAdditional, getEmployeeResult.Data as NotBetEmployee);
                }
                var createResult = await _payoutManager.Create(payout);
                if (createResult.Succed)
                {
                    if (await _sessionDocumentService.AddPayout(payout))
                    {
                        return PartialView("AddedPayout", new UpdatePayoutViewModel()
                        {
                            PayoutId = payout.Id,
                            Ammount = payout.Ammount,
                            IsAdditional = payout.IsAdditional
                        });
                    }
                }
                return StatusCode(500);
            }
            if (getEmployeeResult.OperationStatus == OperationStatuses.Error)
            {
                return StatusCode(500);
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdatePayoutViewModel viewModel)
        {
            var getPayoutResult = await _payoutManager.GetById(viewModel.PayoutId);
            if (getPayoutResult.Succed)
            {
                getPayoutResult.Data.Ammount = viewModel.Ammount;
                var updateResult = await _payoutManager.Update(getPayoutResult.Data);
                if (await _sessionDocumentService.UpdatePayout(viewModel.PayoutId, viewModel.Ammount))
                {
                    if (updateResult.Succed)
                    {
                        return Ok(getPayoutResult.Data.Ammount);
                    }
                }
                return StatusCode(500);
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid payoutId)
        {
            var deleteResult = await _payoutManager.Delete(payoutId);
            if (deleteResult.Succed)
            {
                if (await _sessionDocumentService.DeleteAccrual(payoutId))
                {
                    return Ok();
                }   
            }
            return StatusCode(500);
        }
    }
}
