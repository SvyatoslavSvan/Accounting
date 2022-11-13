using Accounting.Domain.Models;
using Accounting.Domain.Models.Base;
using Accounting.Domain.Requests;
using Accounting.Domain.ViewModels;
using Accounting.Services;
using Accouting.Domain.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.Utils;

namespace Accounting.Controllers
{
#nullable disable
    public class DocumentController : Controller
    {
        private readonly ISessionDocumentService _sessionDocumentService;
        private readonly IEmployeeManager _employeeManager;
        private readonly IPayoutManager _payoutManager;
        private readonly IDocumentManager _documentManager;
        public DocumentController(ISessionDocumentService sessionDocumentService, IEmployeeManager employeeManager, IPayoutManager payoutManager, IDocumentManager documentManager)
        {
            _sessionDocumentService = sessionDocumentService;
            _employeeManager = employeeManager; 
            _payoutManager = payoutManager;
            _documentManager = documentManager;
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var document = await _documentManager.GetById(id);
            if (document.Succed)
            {
                if (await _sessionDocumentService.LoadDocument(document.Data))
                {
                    var employeesInDocument = new List<EmployeeBase>(document.Data.NotBetEmployees);
                    employeesInDocument.AddRange(document.Data.BetEmployees);
                    var employeesAddToDocument = await _employeeManager.GetAll();
                    foreach (var item in employeesInDocument)
                    {
                        employeesAddToDocument.Data.Remove(employeesAddToDocument.Data.FirstOrDefault(x => x.Id == item.Id));
                    }
                    return View(new UpdateDocumentViewModel()
                    {
                        Name = document.Data.Name,
                        DateCreate = document.Data.DateCreate,
                        EmployeesAddToDocument = employeesAddToDocument.Data.ToList(),
                        EmployeesInDocument = employeesInDocument,
                        Id = document.Data.Id
                    });
                }
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateDocumentViewModel viewModel)
        {
            var sessionDocument = _sessionDocumentService.GetDocument();
            var getDocumentResult = await _documentManager.GetById(viewModel.Id);
            if (getDocumentResult.Succed && sessionDocument is not null)
            {
                getDocumentResult.Data.PayoutsBetEmployees = sessionDocument.PayoutsBetEmployee;
                getDocumentResult.Data.PayoutsNotBetEmployees = sessionDocument.PayoutsNotBetEmployee;
                getDocumentResult.Data.NotBetEmployees = sessionDocument.NotBetEmployees;
                getDocumentResult.Data.BetEmployees = sessionDocument.BetEmployees;
                var updateResult = await _documentManager.Update(getDocumentResult.Data);
                if (updateResult.Succed)
                {
                    return RedirectToAction(nameof(Documents));
                }
                return StatusCode(500);
            }
            return StatusCode(500);
        }

        [HttpGet]
        public async Task<IActionResult> Documents()
        {
            var documents = await _documentManager.GetAll();
            return View(documents.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetSearch(DocumentSearchRequest request)
        {
            var getSearchedDocumentsResult = await _documentManager.GetSearch(request);
            return PartialView(getSearchedDocumentsResult.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await _sessionDocumentService.Create();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DocumentViewModel viewModel)
        {
            var sessionDocument = _sessionDocumentService.GetDocument();
            var document = new Document(sessionDocument.NotBetEmployees, sessionDocument.
                BetEmployees, sessionDocument.PayoutsBetEmployee, sessionDocument.PayoutsNotBetEmployee, viewModel.Name, viewModel.DateCreate);
            var createResult = await _documentManager.Create(document);
            if (createResult.Succed)
            {
                return RedirectToAction(nameof(Documents));
            }
            if (createResult.OperationStatus == DAL.Result.Provider.Base.OperationStatuses.Error)
            {
                return StatusCode(500);
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid documentId)
        {
            var deleteResult = await _documentManager.Delete(documentId);
            if (deleteResult.Succed)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(Guid employeeId)
        {
            var getEmployeeResult = await _employeeManager.GetById(employeeId);
            if (getEmployeeResult.Succed)
            {
                if (await _sessionDocumentService.AddEmployeeToDocument(getEmployeeResult.Data))
                {
                    return PartialView("AddedEmployee", getEmployeeResult.Data);
                }
                else
                {
                    return StatusCode(500);
                }
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEmployee(Guid EmployeeId)
        {
            var deleteResult = await _payoutManager.DeleteRange(_sessionDocumentService.GetPayoutsByEmployeeId(EmployeeId));
            if (deleteResult.Succed)
            {
                if (await _sessionDocumentService.DeleteEmployee(EmployeeId))
                {
                    return PartialView("RemovedEmployeeFromDocument", _employeeManager.GetById(EmployeeId).Result.Data);
                }
            }
            return BadRequest();
        }

    }
}
