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
                    deducation = new DeducationNotBetEmployee(viewModel.Ammount, viewModel.IsAdditional, employee as NotBetEmployee);
                }
                var createResult = await _deducationProvider.Create(deducation);
                if (createResult.Succed)
                {
                    return PartialView("CreatedDeducation", new UpdateDeducationlViewModel()
                    {
                        Ammount = deducation.Ammount,
                        DeducationId = deducation.Id
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
    }
}
