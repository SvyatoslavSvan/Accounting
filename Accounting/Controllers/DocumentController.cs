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
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleteResult = await _documentProvider.Delete(id);
            if (deleteResult.Succed)
                return RedirectToAction(nameof(Documents));
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
                        return PartialView("AddedAccrual", updateAccrualViewModel);
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
                var tiedToEmployeeAccrual = _sessionDocumentService.GetAccrualByEmployeeId(employeeToAdd.Data.Id);
                if (tiedToEmployeeAccrual is not null)
                {
                    if (await _sessionDocumentService.DeleteAccrual(tiedToEmployeeAccrual.Id))
                    {
                        if (_accrualProvider.Delete(tiedToEmployeeAccrual.Id).GetAwaiter().GetResult().Succed)
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
                        return PartialView("AddedAccrual", new UpdateAccrualViewModel() { AccrualId = accrual.Id, Ammount = accrualViewModel.Ammount, EmployeeId = accrualViewModel.EmployeeId});
                }
            }
            return BadRequest();
        }
       
    }
}
