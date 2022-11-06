using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.ViewModels;
using Accouting.Domain.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.Controllers
{
    public class WorkDayController : Controller
    {
        private readonly IWorkDayManager _workDayManager;
        public WorkDayController(IWorkDayManager workDayProvider)
        {
            _workDayManager = workDayProvider;
        }

        public async Task<IActionResult> Update(UpdateWorkDayViewModel viewModel)
        {
            var workDay = await _workDayManager.GetById(viewModel.Id);
            if (workDay.Succed)
            {
                workDay.Data.Hours = viewModel.Hours;
                var updateResult = await _workDayManager.Update(workDay.Data);
                if (updateResult.Succed)
                {
                    return Ok();
                }
                if (updateResult.OperationStatus == OperationStatuses.Error || workDay.OperationStatus == OperationStatuses.Error)
                {
                    return StatusCode(500);
                }
            }
            return BadRequest(ModelState);
        }
    }
}
