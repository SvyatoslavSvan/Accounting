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
        public DocumentController(ISessionDocumentService sessionDocumentService, IEmployeeManager employeeManager)
        {
            _sessionDocumentService = sessionDocumentService;
            _employeeManager = employeeManager; 
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await _sessionDocumentService.Create();
            return View();
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

    }
}
