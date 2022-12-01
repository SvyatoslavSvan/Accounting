using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;
using Accounting.Domain.ViewModels;
using Accounting.ViewModels;
using Accouting.Domain.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Accounting.Controllers
{
    public class GroupController : Controller
    {
        private readonly IGroupManager _groupManager;

        public GroupController(IGroupManager groupManager)
        {
            _groupManager = groupManager;
        }

        public async Task<IActionResult> Groups()
        {
            var allGroups = await _groupManager.GetAll();
            return View(allGroups.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(GroupViewModel groupViewModel)
        {
            var createResult = await _groupManager.Create(new Group(groupViewModel.Name));
            if (createResult.Succed)
                return PartialView("CreatedGroup", createResult.Data);
            if (createResult.OperationStatus == DAL.Result.Provider.Base.OperationStatuses.Error)
                return View("Error");
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateGroupViewModel viewModel)
        {
            var groupToUpdate = await _groupManager.GetById(viewModel.Id);
            groupToUpdate.Data.Name = viewModel.Name;
            var updateResult = await _groupManager.Update(groupToUpdate.Data);
            if (updateResult.Succed)
            {
                return Ok(viewModel.Name);
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleteResult = await _groupManager.Delete(id);
            if (deleteResult.Succed)
            {
                return Ok();
            }
            if (deleteResult.OperationStatus == OperationStatuses.Error)
            {
                return StatusCode(500);
            }
            return BadRequest();
        }
    }
}
