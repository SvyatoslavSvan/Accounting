﻿using Accounting.DAL.Interfaces;
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
            return BadRequest();
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
                var getGroupResult = await _groupProvider.GetById(viewModel.GroupId);
                if (getGroupResult.Succed)
                {
                    if (viewModel.IsBet)
                    {
                        var getEmployeeToUpdateResult = await _employeeProvider.GetBetEmployee(viewModel.Id);
                        if (getEmployeeToUpdateResult.Succed)
                        {
                            getEmployeeToUpdateResult.Data.Update(viewModel, getGroupResult.Data);
                            await _employeeProvider.Update(getEmployeeToUpdateResult.Data);
                            return RedirectToAction(nameof(Employees));
                        }
                    }
                    else
                    {
                        var getEmployeeToUpdateResult = await _employeeProvider.getNotBetEmployee(viewModel.Id);
                        if (getEmployeeToUpdateResult.Succed)
                        {
                            getEmployeeToUpdateResult.Data.Update(viewModel, getGroupResult.Data);
                            await _employeeProvider.Update(getEmployeeToUpdateResult.Data);
                        }
                        return RedirectToAction(nameof(Employees));
                    }
                }
            }
            return View();
        }
    }
}
