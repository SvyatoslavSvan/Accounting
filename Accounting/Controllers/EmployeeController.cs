using Accounting.Domain.Models;
using Accounting.Domain.Models.Base;
using Accounting.Domain.Requests;
using Accounting.Domain.ViewModels;
using Accouting.Domain.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.Controllers
{
    public class EmployeeController : Controller
    {
#nullable disable
        private readonly IEmployeeManager _employeeManager;
        private readonly IGroupManager _groupManager;
        public EmployeeController(IEmployeeManager employeeProvider, IGroupManager groupProvider)
        {
            _employeeManager = employeeProvider;
            _groupManager = groupProvider;
        }

        [HttpGet]
        public async Task<IActionResult> Employees()
        {
            var getAllResult = await _employeeManager.GetAll();
            if (getAllResult.Succed)
            {
                return View(getAllResult.Data);
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetSearch(EmployeeSearchRequest request)
        {
            var getSearchedResult = await _employeeManager.GetSearch(request);
            if (getSearchedResult.Succed)
            {
                return View("Employees", getSearchedResult.Data);
            }
            return StatusCode(500);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeViewModel employeeViewModel)
        {
            if (ModelState.IsValid)
            {
                Employee employee;
                var employeeGroup = await _groupManager.GetById(employeeViewModel.GroupId);
                if (employeeViewModel.IsBet)
                {
                    employee = new BetEmployee(employeeViewModel.Name, (decimal)employeeViewModel.Bet, employeeViewModel.InnerId, employeeViewModel.Premium);
                    employee.AddToGroup(employeeGroup.Data);
                }
                else
                {
                    employee = new Employee(employeeViewModel.Name, employeeViewModel.InnerId, employeeViewModel.Premium);
                    employee.AddToGroup(employeeGroup.Data);
                }
                var createResult = await _employeeManager.Create(employee);
                if (createResult.Succed)
                {
                    return PartialView("createdEmployee" ,employee);
                }
                else
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleteResult = await _employeeManager.Delete(id);
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
            var getByIdResult = await _employeeManager.GetById(id);
            if (getByIdResult.Data is BetEmployee betEmployee)
            {
                updateViewModel.Bet = betEmployee.Bet;
                updateViewModel.Name = betEmployee.Name;
                updateViewModel.InnerId = betEmployee.InnerId;
                updateViewModel.GroupId = (Guid)betEmployee.GroupId;
                updateViewModel.IsBet = true;
                updateViewModel.Premium = betEmployee.Premium;
            }
            else if (getByIdResult.Data is Employee notBetEmployee)
            {
                updateViewModel.Name = notBetEmployee.Name;
                updateViewModel.InnerId = notBetEmployee.InnerId;
                updateViewModel.GroupId = (Guid)notBetEmployee.GroupId;
                updateViewModel.IsBet = false;
                updateViewModel.Premium = notBetEmployee.Premium;
            }
            return PartialView(updateViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateEmployeeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var getGroupResult = await _groupManager.GetById(viewModel.GroupId);
                if (getGroupResult.Succed)
                {
                    if (viewModel.IsBet)
                    {
                        var getEmployeeToUpdateResult = await _employeeManager.GetBetEmployee(viewModel.Id);
                        if (getEmployeeToUpdateResult.Succed)
                        {
                            getEmployeeToUpdateResult.Data.AddToGroup(getGroupResult.Data);
                            getEmployeeToUpdateResult.Data.Name = viewModel.Name;
                            getEmployeeToUpdateResult.Data.InnerId = viewModel.InnerId;
                            getEmployeeToUpdateResult.Data.Bet = (decimal)viewModel.Bet;
                            getEmployeeToUpdateResult.Data.Premium = viewModel.Premium;
                            await _employeeManager.Update(getEmployeeToUpdateResult.Data);
                            return RedirectToAction(nameof(Employees));
                        }
                    }
                    else
                    {
                        var getEmployeeToUpdateResult = await _employeeManager.GetNotBetEmployee(viewModel.Id);
                        if (getEmployeeToUpdateResult.Succed)
                        {
                            getEmployeeToUpdateResult.Data.AddToGroup(getGroupResult.Data);
                            getEmployeeToUpdateResult.Data.Premium = viewModel.Premium;
                            getEmployeeToUpdateResult.Data.Name = viewModel.Name;
                            getEmployeeToUpdateResult.Data.InnerId = viewModel.InnerId;
                            await _employeeManager.Update(getEmployeeToUpdateResult.Data);
                        }
                        return RedirectToAction(nameof(Employees));
                    }
                }
            }
            return View();
        }
    }
}
