using Accounting.DAL.Interfaces;
using Accounting.DAL.Interfaces.Base;
using Accounting.Domain.Models;
using Accounting.Domain.SessionEntity;
using Accounting.Domain.ViewModels;
using Accounting.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.Controllers
{
#nullable disable
    public class DocumentController : Controller
    {
        private readonly IBaseProvider<Document> _documentProvider;
        private readonly IEmployeeProvider _employeeProvider;
        private readonly ISession _session;
        private readonly IBaseProvider<Accrual> _accrualProvider;
        private const string sessionDocumentKey = "sessionDocumentKey";
        public DocumentController(IBaseProvider<Document> documentProvider, IEmployeeProvider employeeProvider, IServiceProvider provider, IBaseProvider<Accrual> accrualProvider)
        {
            _documentProvider = documentProvider;
            _employeeProvider = employeeProvider;
            _session = provider.GetRequiredService<IHttpContextAccessor>().HttpContext.Session;
            _accrualProvider = accrualProvider;
        }
        [HttpGet]
        public IActionResult Create()
        {
            var document = new SessionDocument();
            _session.SetJson(sessionDocumentKey, document);
            return View(new DocumentViewModel() { DateCreate = DateTime.Now});  
        }
        [HttpPost]
        public async Task<IActionResult> Create(DocumentViewModel documentViewModel)
        {
            if (ModelState.IsValid)
            {
                var sessionDocument = _session.GetJson<SessionDocument>(sessionDocumentKey);
                var document = new Document(documentViewModel.Name, documentViewModel.DateCreate);
                document.AddEmployeesToDocument(this.MapToListFromSessionEmployee(sessionDocument.Employees));
                var createResult = await _documentProvider.Create(document);
                if (createResult.Succed)
                    return Ok();
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
                var sessionDocument = _session.GetJson<SessionDocument>(sessionDocumentKey) ?? new SessionDocument();
                sessionDocument.Employees.Add(this.MapToSessionEmployee(getByIdResult.Data));
                _session.SetJson(sessionDocumentKey, sessionDocument);
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
                    return Ok();
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
