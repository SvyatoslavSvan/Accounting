using Accounting.DAL.Interfaces;
using Accounting.DAL.Interfaces.Base;
using Accounting.Domain.Models;
using Accounting.Domain.SessionEntity;
using Accounting.Domain.ViewModels;
using Accounting.Extensions;
using Accounting.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.Controllers
{
#nullable disable
    public class DocumentController : Controller
    {
        private readonly IBaseProvider<Document> _documentProvider;
        private readonly IEmployeeProvider _employeeProvider;
        private readonly IBaseProvider<Accrual> _accrualProvider;
        private readonly ISessionDocumentService _sessionDocumentService;
        private const string sessionDocumentKey = "sessionDocumentKey";
        public DocumentController(IBaseProvider<Document> documentProvider, IEmployeeProvider employeeProvider, IBaseProvider<Accrual> accrualProvider, ISessionDocumentService sessionDocumentService)
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
            if (await _sessionDocumentService.CreateSessionDocument())
                return View(new DocumentViewModel() { DateCreate = DateTime.Now });
            return BadRequest();
        }
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
        public async Task<IActionResult> AddAccrualToEmployee(AccrualViewModel accrualViewModel)
        {
            var getByIdResult = await _employeeProvider.getNotBetEmployee(accrualViewModel.EmployeeId);
            if (getByIdResult.Succed)
            {
                var accrual = new Accrual(DateTime.Now, accrualViewModel.Ammount);
                accrual.AddEmployee(getByIdResult.Data);
                var createAccrualResult = await _accrualProvider.Create(accrual);
                if (createAccrualResult.Succed)
                    return PartialView("AddedAccrual",accrual.Ammount);
            }
            return BadRequest();
        }
        private SessionNotBetEmployee MapToSessionEmployee(NotBetEmployee employee)
        {
            var sessionEmployee = new SessionNotBetEmployee()
            {
                Name = employee.Name,
                Id = employee.Id,
                InnerId = employee.InnerId
            };
            return sessionEmployee;
        }
        private NotBetEmployee MapFromSessionEmployee(SessionNotBetEmployee sessionEmployee)
        {
            var employee = new NotBetEmployee(sessionEmployee.Name, sessionEmployee.InnerId);
            employee.SetId(sessionEmployee.Id);
            return employee;
        }
        private List<NotBetEmployee> MapToListFromSessionEmployee(List<SessionNotBetEmployee> sessionEmployees)
        {
            List<NotBetEmployee> employees = new List<NotBetEmployee>();
            foreach (var item in sessionEmployees)
                employees.Add(this.MapFromSessionEmployee(item));
            return employees;
        }
    }
}
