using Accounting.DAL.Interfaces;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using Accounting.Domain.Requests;
using Accounting.Domain.ViewModels;
using Accounting.Services.Interfaces;
using Accouting.Domain.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.Controllers
{
#nullable disable
    public class DocumentController : Controller
    {
        private readonly IDocumentManager _documentManager;
        private readonly IEmployeeProvider _employeeProvider;
        private readonly IAccrualManager _accrualManager;
        private readonly ISessionDocumentService _sessionDocumentService;

        public DocumentController(IEmployeeProvider employeeProvider, IAccrualManager accrualManager, ISessionDocumentService sessionDocumentService, IDocumentManager documentManager)
        {
            _employeeProvider = employeeProvider;
            _accrualManager = accrualManager;
            _sessionDocumentService = sessionDocumentService;
            _documentManager = documentManager;
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var getDocumentToUpdateResult = await _documentManager.GetById(id);
            if (getDocumentToUpdateResult.Succed)
            {
                var loadToSessionResult = await _sessionDocumentService.LoadDocument(getDocumentToUpdateResult.Data);
                if (loadToSessionResult)
                    return View(await GetUpdateViewModelAsync(getDocumentToUpdateResult.Data));    
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateDocumentViewModel viewModel)
        {
            var updateResult = await _documentManager.Update(_sessionDocumentService.GetDocument(viewModel));
            if (updateResult.Succed)
            {
                return RedirectToAction(nameof(Documents));
            }
            if (updateResult.OperationStatus == OperationStatuses.Error)
            {
                return StatusCode(500);
            }
            return BadRequest();
        }

        [NonAction]
        private async Task<UpdateDocumentViewModel> GetUpdateViewModelAsync(Document document) => new UpdateDocumentViewModel()
        {
            Name = document.Name,
            DateCreate = document.DateCreate,
            Id = document.Id,
            EmployeesInDocument = document.Employees,
            EmployeesAddToDocument = await GetEmployeesExcludeByDocumentIdAsync(document.Id)
        };

        [NonAction]
        private async Task<List<NotBetEmployee>> GetEmployeesExcludeByDocumentIdAsync(Guid id)
        {
            var getEmployeesResult = await _employeeProvider.GetNotBetEmployeesIncludeDocument();
            var employees = getEmployeesResult.Data.ToList();
            var employeesWithoutDocument = employees.Where(x => x.Documents == null).ToList();
            employees.RemoveAll(x => x.Documents == null);
            employees.RemoveAll(x => x.Documents.Contains(x.Documents.FirstOrDefault(x => x.Id == id)));
            employees.AddRange(employeesWithoutDocument);
            return employees;
        }

        [HttpGet]
        public async Task<IActionResult> Documents()
        {
            var getAllResult = await _documentManager.GetAll();
            if (getAllResult.Succed)
                return View(getAllResult.Data);
            return View("NotFound");
        }
        [HttpGet]
        public async Task<IActionResult> GetSearch(DocumentSearchRequest request)
        {
            var getBySearchResult = await _documentManager.GetSearch(request);
            if (getBySearchResult.Succed)
                return PartialView(getBySearchResult.Data);
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Document(Guid id)
        {
            var getByIdResult = await _documentManager.GetById(id);
            if (getByIdResult.Succed)
                return View(getByIdResult);
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid documentId)
        {
            var deleteResult = await _documentManager.Delete(documentId);
                if (deleteResult.Succed)
                    return Ok();
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            if (await _sessionDocumentService.CreateSessionDocument())
                return View(new DocumentViewModel() { DateCreate = DateTime.Now });
            return BadRequest();
        }

        [HttpGet]
        public IActionResult GetSumOfAccruals() => PartialView(_sessionDocumentService.SumOfAccruals());

        [HttpPost]
        public async Task<IActionResult> Create(DocumentViewModel documentViewModel)
        {
            if (ModelState.IsValid)
            {
                var document = _sessionDocumentService.GetDocument(documentViewModel);
                var createResult = await _documentManager.Create(document);
                if (createResult.Succed)
                {
                    await _sessionDocumentService.Clear();
                    return RedirectToAction(nameof(Documents));
                }
                ModelState.AddModelError("", "Не вдалося створити документ");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployeeToDocument(Guid employeeId)
        {
            var getByIdResult = await _employeeProvider.getNotBetEmployee(employeeId);
            if (getByIdResult.Succed)
            {
                var addEmployeeToDocumentResult = await _sessionDocumentService.AddEmployee(getByIdResult.Data);
                if (addEmployeeToDocumentResult)
                    return PartialView("AddedEmployee", getByIdResult.Data);
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAccrual(UpdateAccrualViewModel updateAccrualViewModel)
        {
            if (ModelState.IsValid)
            {
                var getAccrualToUpdateResult = await _accrualManager.GetById(updateAccrualViewModel.AccrualId);
                if (getAccrualToUpdateResult.Succed)
                    getAccrualToUpdateResult.Data.Ammount = updateAccrualViewModel.Ammount;
                var updateAccrualResult = await _accrualManager.Update(getAccrualToUpdateResult.Data);
                if (updateAccrualResult.Succed)
                    if (await _sessionDocumentService.UpdateAccrual(updateAccrualViewModel.Ammount, updateAccrualViewModel.AccrualId))
                        return Ok(updateAccrualViewModel.Ammount);
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEmployeeFromDocument(Guid EmployeeId)
        {
            var deleteEmployeeFromSessionResult = await _sessionDocumentService.DeleteEmployee(EmployeeId);
            var employeeToAdd = await _employeeProvider.getNotBetEmployee(EmployeeId);
            if (employeeToAdd.Succed)
            {
                var tiedToEmployeeAccruals = _sessionDocumentService.GetAccrualsByEmployeeId(employeeToAdd.Data.Id);
                if (tiedToEmployeeAccruals is not null)
                {
                    if (await _sessionDocumentService.DeleteAccrualsByEmployeeId(employeeToAdd.Data.Id))
                    {
                        if (_accrualManager.DeleteRange(tiedToEmployeeAccruals).GetAwaiter().GetResult().Succed)
                        {
                            if (deleteEmployeeFromSessionResult)
                                return PartialView("RemovedEmployeeFromDocument", employeeToAdd.Data);
                        }
                    }
                }
                else
                {
                    if (deleteEmployeeFromSessionResult)
                        return PartialView("RemovedEmployeeFromDocument", employeeToAdd.Data);
                }
            }
            return BadRequest();
        }

        [HttpGet]
        public IActionResult CreateAccrual(Guid id) => PartialView(new CreateAccrualViewModel()
        {
            Accruals = _sessionDocumentService.GetAccrualsByEmployeeId(id),
            EmployeeId = id
        });

        [HttpPost]
        public async Task<IActionResult> AddAccrualToEmployee(AccrualViewModel accrualViewModel)
        {
            var getByIdResult = await _employeeProvider.getNotBetEmployee(accrualViewModel.EmployeeId);
            if (getByIdResult.Succed)
            {
                var accrual = new Accrual(DateTime.Now, accrualViewModel.Ammount, accrualViewModel.IsAdditional);
                accrual.AddEmployee(getByIdResult.Data);
                var createAccrualResult = await _accrualManager.Create(accrual);
                if (createAccrualResult.Succed)
                {
                    var addAccrualToSessionReuslt = await _sessionDocumentService.AddAccrual(accrual);
                    if(addAccrualToSessionReuslt)
                        return PartialView("AddedAccrual", new UpdateAccrualViewModel() { AccrualId = accrual.Id, Ammount = accrualViewModel.Ammount, IsAdditional = accrualViewModel.IsAdditional});
                }
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAccrual(Guid accrualId)
        {
            if (await _sessionDocumentService.DeleteAccrual(accrualId))
            {
                var deleteAccrualResult = await _accrualManager.Delete(accrualId);
                if (deleteAccrualResult.Succed)
                    return Ok();
            }
            return BadRequest();
        }
    }
}
