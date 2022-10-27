using Accounting.DAL.Interfaces;
using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.Controllers
{
    public class WorkDayController : Controller
    {
        private readonly IWorkDayProvider _workDayProvider;
        public WorkDayController(IWorkDayProvider workDayProvider)
        {
            _workDayProvider = workDayProvider;
        }

        public async Task<IActionResult> Update(UpdateWorkDayViewModel viewModel)
        {
            var workDay = await _workDayProvider.GetById(viewModel.Id);
            if (workDay.Succed)
            {
                workDay.Data.Hours = viewModel.Hours;
                var updateResult = await _workDayProvider.Update(workDay.Data);
                if (updateResult.Succed)
                {
                    return Ok();
                }
                if (updateResult.OperationStatus == OperationStatuses.Error || workDay.OperationStatus == OperationStatuses.Error)
                {
                    return StatusCode(505);
                }
            }
            return BadRequest(ModelState);
        }
    }
}
