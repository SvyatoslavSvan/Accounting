using Accounting.DAL.Interfaces;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using Accounting.Domain.Models.Base;
using Accounting.Domain.ViewModels;
using Accounting.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.Controllers
{
    public class DeducationController : Controller
    {
        private readonly IEmployeeProvider _employeeProvider;
        private readonly IDeducationProvider _deducationProvider;
        private readonly ISessionDeducationDocumentService _sessionDeducationDocumentService;
        public DeducationController(IEmployeeProvider employeeProvider,
            IDeducationProvider deducationProvider, ISessionDeducationDocumentService deducationDocumentService)
        {
            _employeeProvider = employeeProvider;
            _deducationProvider = deducationProvider;
            _sessionDeducationDocumentService = deducationDocumentService;
        }
        [HttpGet]
        public IActionResult CreateDeducation(Guid id)
        {
            return PartialView(new CreateDeducationViewModel()
            {
                Deducations = _sessionDeducationDocumentService.GetDeducationsByEmployeeId(id),
                EmployeeId = id
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateDeducation(CreateDeducationViewModel viewModel)
        {
            var employee = _sessionDeducationDocumentService.GetEmployeeById(viewModel.EmployeeId);
            if (employee is not null)
            {
                DeducationBase deducation;
                if (employee is BetEmployee betEmployee)
                {
                    deducation = new DeducationBetEmployee(viewModel.Ammount, viewModel.IsAdditional, betEmployee);
                }
                else
                {
                    deducation = new DeducationNotBetEmployee(viewModel.Ammount, viewModel.IsAdditional, (NotBetEmployee)employee);
                }
                var createResult = await _deducationProvider.Create(deducation);
                if (createResult.Succed && await _sessionDeducationDocumentService.AddDeducation(deducation))
                {
                    return PartialView("CreatedDeducation", new UpdateDeducationlViewModel()
                    {
                        Ammount = deducation.Ammount,
                        DeducationId = deducation.Id,
                        IsAdditional = deducation.IsAdditional
                    });
                }
                if (createResult.OperationStatus == OperationStatuses.Error)
                {
                    return StatusCode(500);
                }
            }
            else
            {
                return NotFound();
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDeducation(UpdateDeducationlViewModel viewModel)
        {
            var getDeducationResult = await _deducationProvider.GetById(viewModel.DeducationId);
            if (getDeducationResult.Succed)
            {
                getDeducationResult.Data.Ammount = viewModel.Ammount;
                var updateResult = await _deducationProvider.Update(getDeducationResult.Data);
                if (updateResult.Succed)
                {
                    if (await _sessionDeducationDocumentService.UpdateDeducation(viewModel))
                    {
                        return Ok(getDeducationResult.Data.Ammount);
                    }
                }
                if (updateResult.OperationStatus == OperationStatuses.Error)
                {
                    return StatusCode(500);
                }
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDeducation(Guid deducationId)
        {
            var deleteResult = await _deducationProvider.Delete(deducationId);
            if (deleteResult.Succed)
            {
                await _sessionDeducationDocumentService.DeleteDeducation(deducationId);
                return Ok();
            }
            if (deleteResult.OperationStatus == OperationStatuses.Error)
            {
                return StatusCode(500);
            }
            return BadRequest();
        }

    }
}
