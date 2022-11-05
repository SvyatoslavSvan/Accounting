using Accounting.DAL.Interfaces;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using Accounting.Domain.Models.Base;
using Accounting.Domain.Requests;
using Accounting.Domain.ViewModels;
using Accounting.Services.Interfaces;
using Accouting.Domain.Managers.Interfaces;
using Accouting.Domain.Managers.Interfaces.Base;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.Controllers
{
    public class DeducationDocumentController : Controller
    {
        private readonly IEmployeeProvider _employeeProvider;
        private readonly ISessionDeducationDocumentService _sessionDeducationDocumentService;
        private readonly IDeducationProvider _deducationProvider;
        private readonly IDeducationDocumentManager _documentManager;

        public DeducationDocumentController(IEmployeeProvider employeeProvider,
            IDeducationDocumentManager deducationDocumentManager, ISessionDeducationDocumentService deducationDocumentService, IDeducationProvider deducationProvider)
        {
            _employeeProvider = employeeProvider;
            _documentManager = deducationDocumentManager;
            _sessionDeducationDocumentService = deducationDocumentService;
            _deducationProvider = deducationProvider;
        }

        [HttpGet]
        public async Task<IActionResult> DeducationDocuments()
        {
            var getDocumentsResult = await _documentManager.GetAll();
            if (getDocumentsResult.Succed)
            {
                return View(getDocumentsResult.Data);
            }
            if (getDocumentsResult.OperationStatus == OperationStatuses.Error)
            {
                return StatusCode(500);
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetSearch(DeducationDocumentSearchRequest request)
        {
            var getDocumentsResult = await _documentManager.GetSearch(request);
            if (getDocumentsResult.Succed)
            {
                return PartialView(getDocumentsResult.Data);
            }
            if (getDocumentsResult.OperationStatus == OperationStatuses.Error)
            {
                return StatusCode(500);
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var getDocumentResult = await _documentManager.GetById(id);
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
            var sessionDocument = _sessionDeducationDocumentService.GetDocumentFromSession();
            var document = new DeducationDocument(sessionDocument.Id, viewModel.Name, viewModel.DateCreate, 
                sessionDocument.DeducationBetEmployees, sessionDocument.NotBetEmployees, 
                sessionDocument.BetEmployees, sessionDocument.DeducationNotBetEmployees);
            var updateResult = await _documentManager.Update(document);
            if (updateResult.Succed)
            {
                return RedirectToAction("Documents", "Document");
            }
            return StatusCode(500);
        } 

        [HttpPost]
        public async Task<IActionResult> Create(DocumentViewModel viewModel)
        {
            var sessionDocument = _sessionDeducationDocumentService.GetDocumentFromSession();
            var document = new DeducationDocument(Guid.Empty, viewModel.Name, viewModel.DateCreate,
                sessionDocument.DeducationBetEmployees, sessionDocument.NotBetEmployees, sessionDocument.BetEmployees, sessionDocument.DeducationNotBetEmployees);
            var createResult = await _documentManager.Create(document);
            if (createResult.Succed)
            {
                return RedirectToAction("Documents", "Document");
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
