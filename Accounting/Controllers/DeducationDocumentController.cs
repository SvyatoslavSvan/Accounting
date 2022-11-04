using Accounting.DAL.Interfaces;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using Accounting.Domain.Models.Base;
using Accounting.Domain.SessionEntity;
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

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var getDocumentResult = await _deducationDocumentProvider.GetById(id);
            if (getDocumentResult.Succed)
            {
                if (await _sessionDeducationDocumentService.LoadDocument(getDocumentResult.Data))
                {
                    var employeesInDocument = new List<EmployeeBase>(getDocumentResult.Data.NotBetEmployees);
                    employeesInDocument.AddRange(getDocumentResult.Data.BetEmployees);
                    var employeesAddToDocument = await _employeeProvider.GetAll();
                    employeesInDocument.ForEach(x =>
                    {
                        employeesAddToDocument.Data.RemoveAll(y => y.Id == x.Id);
                    });
                    return View(new UpdateDeducationDocumentViewModel()
                    {
                        Name = getDocumentResult.Data.Name,
                        DateCreate = getDocumentResult.Data.DateCreate,
                        EmployeesAddToDocument = employeesAddToDocument.Data,
                        EmployeesInDocument = employeesInDocument
                    });
                }
            }
            return StatusCode(500);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateDeducationDocumentViewModel viewModel)
        {
            var document = await UpdateDocument(viewModel, _sessionDeducationDocumentService.GetDocumentFromSession());
            var updateResult = await _deducationDocumentProvider.Update(document);
            if (updateResult.Succed)
            {
                return RedirectToAction("Documents", "Document");
            }
            return StatusCode(500);
        }

        [NonAction]
        private async Task<DeducationDocument> UpdateDocument(UpdateDeducationDocumentViewModel viewModel, SessionDeducationDocument document)
        {
            var getDocumentResult = await _deducationDocumentProvider.GetById(document.Id);
            getDocumentResult.Data.Name = viewModel.Name;
            getDocumentResult.Data.DateCreate = viewModel.DateCreate;
            getDocumentResult.Data.DeducationsNotBetEmployee = document.DeducationNotBetEmployees;
            getDocumentResult.Data.DeducationsBetEmployee = document.DeducationBetEmployees;
            getDocumentResult.Data.BetEmployees = document.BetEmployees;
            getDocumentResult.Data.NotBetEmployees = document.NotBetEmployees;
            return getDocumentResult.Data;
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
