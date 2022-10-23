using Accounting.DAL.Interfaces.Base;
using Accounting.Domain.Models;
using Accounting.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.Controllers
{
    public class GroupController : Controller
    {
        private readonly IBaseProvider<Group> _groupProvider;
        public GroupController(IBaseProvider<Group> groupProvider)
        {
            _groupProvider = groupProvider;
        }
        public async Task<IActionResult> Groups()
        {
            var allGroups = await _groupProvider.GetAll();
            return View(allGroups.Data);
        }
        [HttpPost]
        public async Task<IActionResult> Create(GroupViewModel groupViewModel)
        {
            var createResult = await _groupProvider.Create(new Group(groupViewModel.Name));
            if (createResult.Succed)
                return RedirectToAction(nameof(Groups));
            return BadRequest();
        }
        
    }
}
