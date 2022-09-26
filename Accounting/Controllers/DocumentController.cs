using Accounting.DAL.Interfaces;
using Accounting.DAL.Interfaces.Base;
using Accounting.Domain.JsonModels;
using Accounting.Domain.Models;
using Accounting.Domain.SessionEntity;
using Accounting.Domain.ViewModels;
using Accounting.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using System.Diagnostics.SymbolStore;

namespace Accounting.Controllers
{
    public class DocumentController : Controller
    {
        private readonly IBaseProvider<Document> _documentProvider;
        private readonly IEmployeeProvider _employeeProvider;
        private readonly ISession _session;
        private readonly IBaseProvider<Accrual> _accrualProvider;
        private const string sessionDocumentKey = "SESSIONDOCUMENTKEY";
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
            _session.SetJson(sessionDocumentKey, new SessionDocument());
            return View(new DocumentViewModel()
            {
                DateCreate = DateTime.Now.ToLocalTime(),
            });
        }
        [HttpPost]
        public async Task<IActionResult> Create(DocumentViewModel documentViewModel)
        {
            if (ModelState.IsValid)
            {
                var sessionDocument = _session.GetJson<SessionDocument>(sessionDocumentKey);
                var document = new Document(documentViewModel.Name, documentViewModel.DateCreate);
                List<NotBetEmployee> notBetEmployees = new List<NotBetEmployee>();
                foreach (var item in sessionDocument.Employees)
                {
                    var employee = new NotBetEmployee(item.Name, item.InnerId);
                    employee.SetId(item.Id);
                    employee.AddAcruals(sessionDocument.Accruals);
                    notBetEmployees.Add(employee);
                }
                document.AddEmployeesToDocument(notBetEmployees);
                document.AddAccrualsToDocument(sessionDocument.Accruals);
                var createResult = await _documentProvider.Create(document);
                if (createResult.Succed)
                    return RedirectToAction("Employee", "Employees");

            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployeeToDocument(AddEmployeeToDocumentViewModel viewModel)
        {
            var getEmployeeToAddResult = await _employeeProvider.getNotBetEmployee(viewModel.EmployeeId);
            if (getEmployeeToAddResult.Succed)
            {
                var document = _session.GetJson<SessionDocument>(sessionDocumentKey) ?? new SessionDocument();
                var accrual = new Accrual(DateTime.Now, viewModel.Accrual);
                getEmployeeToAddResult.Data.AddAccrual(accrual);
                var notBetEmployeeJsonModel = new NotBetEmployeeJsonModel();
                notBetEmployeeJsonModel.Id = getEmployeeToAddResult.Data.Id;
                notBetEmployeeJsonModel.InnerId = getEmployeeToAddResult.Data.InnerId;
                notBetEmployeeJsonModel.Accruals = getEmployeeToAddResult.Data.Accruals;
                notBetEmployeeJsonModel.Name = getEmployeeToAddResult.Data.Name;
                document.Accruals.Add(accrual);
                document.Employees.Add(notBetEmployeeJsonModel);
                _session.Clear();
                _session.SetJson(sessionDocumentKey, document);
                return PartialView("AddedEmployee", getEmployeeToAddResult.Data);
            }
            return BadRequest();
        }
    }
}
