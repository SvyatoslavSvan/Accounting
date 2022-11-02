using Accounting.DAL.Interfaces;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using Accounting.Domain.ViewModels;
using Accounting.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.Controllers
{
    public class DeducationDocumentController : Controller
    {
        private readonly IEmployeeProvider _employeeProvider;
        private readonly IDeducationDocumentProvider _deducationDocumentProvider;
        private readonly ISessionDeducationDocumentService _sessionDeducationDocumentService;
        private readonly IDeducationProvider _deducationProvider;

        public DeducationDocumentController(IEmployeeProvider employeeProvider, 
            IDeducationDocumentProvider deducationDocumentProvider, ISessionDeducationDocumentService deducationDocumentService, IDeducationProvider deducationProvider)
        {
            _employeeProvider = employeeProvider;
            _deducationDocumentProvider = deducationDocumentProvider;
            _sessionDeducationDocumentService = deducationDocumentService;
            _deducationProvider = deducationProvider;
        }

        [HttpPost]
        public async Task<IActionResult> Create(DocumentViewModel viewModel)
        {
            var sessionDocument = _sessionDeducationDocumentService.GetDocumentFromSession();
            var document = new DeducationDocument(Guid.Empty, viewModel.Name, viewModel.DateCreate,
                sessionDocument.DeducationBetEmployees, sessionDocument.NotBetEmployees, sessionDocument.BetEmployees, sessionDocument.DeducationNotBetEmployees);
            var createResult = await _deducationDocumentProvider.Create(document);
            if (createResult.Succed)
            {
                return RedirectToAction("Document", "Documents");
            }
            return BadRequest();
        }

        [HttpGet]
        public IActionResult GetSumOfDeducations() => PartialView(_sessionDeducationDocumentService.GetSum());

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await _sessionDeducationDocumentService.CreateSessionDeducationDocument();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEmployee(Guid employeeId)
        {
            var getEmployeeResult = await _employeeProvider.GetById(employeeId);
            var deleteDeducationsResult = await _deducationProvider.DeleteDeducations(_sessionDeducationDocumentService.GetDeducationsByEmployeeId(employeeId));
            if (await _sessionDeducationDocumentService.DeleteEmployee(getEmployeeResult.Data) && getEmployeeResult.Succed && deleteDeducationsResult.Succed)
            {
                return PartialView("RemovedEmployeeFromDocument", getEmployeeResult.Data);
            }
            else if (getEmployeeResult.OperationStatus == OperationStatuses.Error)
            {
                return StatusCode(500);
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(Guid employeeId)
        {
            var getEmployeeResult = await _employeeProvider.GetById(employeeId);
            if (getEmployeeResult.Succed)
            {
                if (await _sessionDeducationDocumentService.AddEmployee(getEmployeeResult.Data))
                {
                    return PartialView("AddedEmployee", getEmployeeResult.Data);
                }
            }
            else if(getEmployeeResult.OperationStatus == OperationStatuses.Error)
            {
                return StatusCode(500);
            }
            return BadRequest();
        }

    }
}
