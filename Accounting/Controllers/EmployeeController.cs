using Accounting.DAL.Interfaces;
using Accounting.DAL.Interfaces.Base;
using Accounting.Domain.Models;
using Accounting.Domain.Models.Base;
using Accounting.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
namespace Accounting.Controllers
{
    public class EmployeeController : Controller
    {
#nullable disable
        private readonly IEmployeeProvider _employeeProvider;
        private readonly IBaseProvider<Group> _groupProvider;
        public EmployeeController(IEmployeeProvider employeeProvider, IBaseProvider<Group> groupProvider)
        {
            _employeeProvider = employeeProvider;
            _groupProvider = groupProvider;
        }
        [HttpGet]
        public async Task<IActionResult> Employees()
        {
            var getAllResult = await _employeeProvider.GetAll();
            if (getAllResult.Succed)
            {
                return View(getAllResult.Data);
            }
            else
            {
                return View("NotFound");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeViewModel employeeViewModel)
        {
            if (ModelState.IsValid)
            {
                EmployeeBase employee;
                var employeeGroup = await _groupProvider.GetById(employeeViewModel.GroupId);
                if (employeeViewModel.IsBet)
                {
                    employee = new BetEmployee(employeeViewModel.Name, (decimal)employeeViewModel.Bet, employeeViewModel.InnerId);
                    employee.AddToGroup(employeeGroup.Data);
                }
                else
                {
                    employee = new NotBetEmployee(employeeViewModel.Name, employeeViewModel.InnerId);
                    employee.AddToGroup(employeeGroup.Data);
                }
                var createResult = await _employeeProvider.Create(employee);
                if (createResult.Succed)
                {
                    return RedirectToAction(nameof(Employees));
                }
                else
                {
                    return BadRequest();
                }
            }
            return RedirectToAction(nameof(Employees));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleteResult = await _employeeProvider.Delete(id);
            if (deleteResult.Succed)
            {
                return RedirectToAction(nameof(Employees));
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var updateViewModel = new UpdateEmployeeViewModel();
            var getByIdResult = await _employeeProvider.GetById(id);
            if (getByIdResult.Data is BetEmployee betEmployee)
            {
                updateViewModel.Bet = betEmployee.Bet;
                updateViewModel.Name = betEmployee.Name;
                updateViewModel.InnerId = betEmployee.InnerId;
                updateViewModel.GroupId = betEmployee.GroupId;
                updateViewModel.IsBet = true;
            }
            else if (getByIdResult.Data is NotBetEmployee notBetEmployee)
            {
                updateViewModel.Name = notBetEmployee.Name;
                updateViewModel.InnerId = notBetEmployee.InnerId;
                updateViewModel.GroupId = notBetEmployee.GroupId;
                updateViewModel.IsBet = false;
            }
            return PartialView(updateViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateEmployeeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                EmployeeBase employeeToUpdate;
                var group = await _groupProvider.GetById(viewModel.GroupId);
                if (viewModel.IsBet)
                {
                    employeeToUpdate = new BetEmployee(viewModel.Name, (decimal)viewModel.Bet, viewModel.InnerId);
                    employeeToUpdate.SetId(viewModel.Id);
                }
                else
                {
                    employeeToUpdate = new NotBetEmployee(viewModel.Name, viewModel.InnerId);
                }
                employeeToUpdate.AddToGroup(group.Data);
                var updateResult = await _employeeProvider.Update(employeeToUpdate);
                if (updateResult.Succed)
                    return RedirectToAction(nameof(Employees));
                return BadRequest();
            }
            return View();
        }
    }
}
