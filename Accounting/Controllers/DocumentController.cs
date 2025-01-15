using Accounting.Domain.Models;
using Accounting.Domain.Requests;
using Accounting.Domain.ViewModels;
using Accounting.Services;
using Accouting.Domain.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.Controllers
{
#nullable disable
    public class DocumentController : Controller
    {
        private readonly ISessionDocumentService _sessionDocumentService;
        private readonly IEmployeeManager _employeeManager;
        private readonly IPayoutManager _payoutManager;
        private readonly IDocumentManager _documentManager;

        public DocumentController(ISessionDocumentService sessionDocumentService, 
            IEmployeeManager employeeManager, IPayoutManager payoutManager, IDocumentManager documentManager)
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
                    var payouts = document.Data.Payouts;
                    var sumOfPayouts = document.Data.GetSumOfPayouts();
                    var getEmployeesResult = await _employeeManager.GetAll();
                    return View(new UpdateDocumentViewModel()
                    {
                        Name = document.Data.Name,
                        DateCreate = document.Data.DateCreate,
                        Payouts = payouts.ToList(),
                        Id = document.Data.Id,
                        SumOfpayouts = sumOfPayouts,
                        Employees = getEmployeesResult.Data.ToList(),
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
                getDocumentResult.Data.Employees = sessionDocument.Employees;
                getDocumentResult.Data.Payouts = sessionDocument.Payouts;
                getDocumentResult.Data.Name = viewModel.Name;
                getDocumentResult.Data.DateCreate = viewModel.DateCreate;
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
            var documents = await _documentManager.GetSearch(new DocumentSearchRequest()
            {
                DocumentType = Domain.Enums.DocumentType.Accrual
            });
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
            return View(new DocumentViewModel()
            {
                DateCreate = DateTime.Now
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(DocumentViewModel viewModel)
        {
            var sessionDocument = _sessionDocumentService.GetDocument();
            var document = new Document(sessionDocument.Employees, viewModel.Name, viewModel.DateCreate, viewModel.DocumentType, sessionDocument.Payouts);
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
        public async Task<IActionResult> DeleteEmployee(Guid EmployeeId , Guid PayoutId)
        {
            var deletePayoutResult = await _payoutManager.Delete(PayoutId);
            if (deletePayoutResult.Succed)
            {
                if (await _sessionDocumentService.DeleteEmployee(EmployeeId, PayoutId))
                {
                    return Ok();
                }
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetSearchEmployeesAddToDocument(EmployeeSearchRequest request)
        {
            var getEmployeesResult = await _employeeManager.GetSearch(request);
            if (getEmployeesResult.Succed)
            {
                return PartialView(getEmployeesResult.Data);
            }
            return StatusCode(500);
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeesAddToDocument()
        {
            var getEmployeesResult = await _employeeManager.GetAll();
            if (getEmployeesResult.Succed)
            {
                return PartialView("GetSearchEmployeesAddToDocument", getEmployeesResult.Data);
            }
            return StatusCode(500);
        }
    }
}
