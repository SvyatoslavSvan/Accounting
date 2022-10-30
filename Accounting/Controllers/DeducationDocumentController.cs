using Accounting.DAL.Interfaces;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.Utils;

namespace Accounting.Controllers
{
    public class DeducationDocumentController : Controller
    {
        private readonly IEmployeeProvider _employeeProvider;
        private readonly IDeducationDocumentProvider _deducationDocumentProvider;
        private readonly ISessionDeducationDocumentService _sessionDeducationDocumentService;
        public DeducationDocumentController(IEmployeeProvider employeeProvider, 
            IDeducationDocumentProvider deducationDocumentProvider, ISessionDeducationDocumentService deducationDocumentService)
        {
            _employeeProvider = employeeProvider;
            _deducationDocumentProvider = deducationDocumentProvider;
            _sessionDeducationDocumentService = deducationDocumentService;
        }

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
            if (await _sessionDeducationDocumentService.DeleteEmployee(getEmployeeResult.Data) && getEmployeeResult.Succed)
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
