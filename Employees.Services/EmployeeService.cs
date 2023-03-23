using Employees.DataAccess.DataModels;
using Employees.Repository.DTOs;
using Employees.Repository.Interfaces;
using Employees.Services.Interfaces;
using Employees.Services.Models;

namespace Employees.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IProjectRepository _projectRepo;
        private readonly IEmployeeRepository _employeeRepo;
        private readonly IEmployeeProjectRepository _employeeProjectRepo;

        public EmployeeService(
            IProjectRepository projectRepo,
            IEmployeeRepository employeeRepo,
            IEmployeeProjectRepository employeeProjectRepo)
        {
            _projectRepo = projectRepo;
            _employeeRepo = employeeRepo;
            _employeeProjectRepo = employeeProjectRepo;
        }

        public ICollection<EmployeePair> GetEmployeePairs()
        {
            var employeePairs = new List<EmployeePair>();

            foreach (var project in _projectRepo.GetAllAsNoTracking())
            {
                var projectEmployeesCount = project.ProjectEmployees.Count;

                for (int i = 0; i < projectEmployeesCount; i++)
                {
                    for (int j = i; j < projectEmployeesCount; j++)
                    {
                        var employeeOne = project.ProjectEmployees.ToList()[i];
                        var employeeTwo = project.ProjectEmployees.ToList()[j];

                        var latestStartDate = employeeOne.StartDate < employeeTwo.StartDate ? employeeTwo.StartDate : employeeOne.StartDate;
                        var earliestEndDate = employeeOne.EndDate < employeeTwo.EndDate ? employeeOne.EndDate : employeeTwo.EndDate;

                        var daysWorkedTogether = earliestEndDate != null
                            ? latestStartDate.Subtract((DateTime)earliestEndDate).Days
                            : DateTime.UtcNow.Subtract(latestStartDate).Days;

                        if(daysWorkedTogether > 0)
                        {
                            employeePairs.Add(new EmployeePair
                            {
                                EmployeeOneId = employeeOne.EmployeeId,
                                EmployeeTwoId = employeeTwo.EmployeeId,
                                ProjectId = project.ProjectId,
                                DaysWorkedTogether = daysWorkedTogether
                            });
                        }
                    }
                }
            }

            return employeePairs;
        }

        public async Task SaveEmployeeDataAsync(ICollection<EmployeeData> employeeDatas)
        {
            await _employeeRepo.AddNonExistingEmployeesAsync(
                employeeDatas.Select(x => x.EmployeeId).ToList());
            await _projectRepo.AddNonExistingProjectsAsync(
                employeeDatas.Select(x => x.ProjectId).ToList());

            var employeeProjects = employeeDatas.Select(x => 
                new EmployeeProjectDto
                {
                    EmployeeId = x.EmployeeId,
                    ProjectId = x.ProjectId,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                }).ToList();

            await _employeeProjectRepo.AddNonExistingEmployeeProjectsAsync(employeeProjects);
        }
    }
}