using Accounting.DAL.Interfaces;
using Accounting.DAL.Interfaces.Base;
using Accounting.Domain.Models;
using Accounting.Domain.ViewModels;
using Accounting.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.Controllers
{
#nullable disable
    public class DocumentController : Controller
    {
        private readonly IBaseProvider<Document> _documentProvider;
        private readonly IEmployeeProvider _employeeProvider;
        private readonly IAccrualProvider _accrualProvider;
        private readonly ISessionDocumentService _sessionDocumentService;

        public DocumentController(IBaseProvider<Document> documentProvider, IEmployeeProvider employeeProvider, IAccrualProvider accrualProvider, ISessionDocumentService sessionDocumentService)
        {
            _documentProvider = documentProvider;
            _employeeProvider = employeeProvider;
            _accrualProvider = accrualProvider;
            _sessionDocumentService = sessionDocumentService;
        }

        [HttpGet]
        public async Task<IActionResult> Documents()
        {
            var getAllResult = await _documentProvider.GetAll();
            if (getAllResult.Succed)
                return View(getAllResult.Data);
            return View("NotFound");
        }

        [HttpGet]
        public async Task<IActionResult> Document(Guid id)
        {
            var getByIdResult = await _documentProvider.GetById(id);
            if (getByIdResult.Succed)
                return View(getByIdResult);
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid documentId)
        {
            var deleteAccrualsTiedToDocumentResult = await _accrualProvider.DeleteRangeByDocumentId(documentId);
            if (deleteAccrualsTiedToDocumentResult.Succed)
            {
                var deleteResult = await _documentProvider.Delete(documentId);
                if (deleteResult.Succed)
                    return Ok();
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            if (await _sessionDocumentService.Clear() && await _sessionDocumentService.CreateSessionDocument())
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
                var createResult = await _documentProvider.Create(document);
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
                var getAccrualToUpdateResult = await _accrualProvider.GetById(updateAccrualViewModel.AccrualId);
                if (getAccrualToUpdateResult.Succed)
                    getAccrualToUpdateResult.Data.SetAmmount(updateAccrualViewModel.Ammount);
                var updateAccrualResult = await _accrualProvider.Update(getAccrualToUpdateResult.Data);
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
                        if (_accrualProvider.DeleteRange(tiedToEmployeeAccruals).GetAwaiter().GetResult().Succed)
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
                var accrual = new Accrual(DateTime.Now, accrualViewModel.Ammount);
                accrual.AddEmployee(getByIdResult.Data);
                var createAccrualResult = await _accrualProvider.Create(accrual);
                if (createAccrualResult.Succed)
                {
                    var addAccrualToSessionReuslt = await _sessionDocumentService.AddAccrual(accrual);
                    if(addAccrualToSessionReuslt)
                        return PartialView("AddedAccrual", new UpdateAccrualViewModel() { AccrualId = accrual.Id, Ammount = accrualViewModel.Ammount});
                }
            }
            return BadRequest();
        }
        [HttpPost]
        public async Task<IActionResult> DeleteAccrual(Guid accrualId)
        {
            if (await _sessionDocumentService.DeleteAccrual(accrualId))
            {
                var deleteAccrualResult = await _accrualProvider.Delete(accrualId);
                if (deleteAccrualResult.Succed)
                    return Ok();
            }
            return BadRequest();
        }
    }
}
