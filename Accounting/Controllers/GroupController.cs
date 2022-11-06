using Accounting.DAL.Interfaces.Base;
using Accounting.Domain.Models;
using Accounting.Domain.ViewModels;
using Accouting.Domain.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
                return RedirectToAction(nameof(Groups));
            if (createResult.OperationStatus == DAL.Result.Provider.Base.OperationStatuses.Error)
                return View("Error");
            return BadRequest();
        }
    }
}
