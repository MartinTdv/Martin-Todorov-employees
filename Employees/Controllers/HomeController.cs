using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Employees.Models;
using Employees.Services.Interfaces;
using Employees.Services.Models;
using System.Collections.Generic;

namespace Employees.Controllers;

public class HomeController : Controller
{
    private readonly IEmployeeService _employeeService;
    private readonly ILogger<HomeController> _logger;

    public HomeController(
        IEmployeeService employeeService,
        ILogger<HomeController> logger)
    {
        _employeeService = employeeService;
        _logger = logger;
    }

    public IActionResult Index()
    {
        var viewModel = _employeeService.GetEmployeePairs()
            .Select(x => new EmployeePairViewModel
            {
                EmployeeOneId = x.EmployeeOneId,
                EmployeeTwoId = x.EmployeeTwoId,
                ProjectId = x.ProjectId,
                DaysWorkedTogether = x.DaysWorkedTogether
            });
        return View(viewModel);
    }

    [HttpGet]
    public IActionResult Upload()
    {
        var inputModel = new UploadInputModel();
        return View(inputModel);
    }

    [HttpPost]
    public async Task<IActionResult> Upload(UploadInputModel inputModel)
    {
        if(inputModel.EmployeesData.FileName.EndsWith(".csv"))
        {
            try
            {
                var employeesData = Employees.Helper.CsvHelper.ConvertCsvToEmployeeDatas(inputModel.EmployeesData).ToList();
                await _employeeService.SaveEmployeeDataAsync(employeesData);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Error");
            }
        }
        return RedirectToAction("Error");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

