using System;
using Employees.DataAccess.DataModels;
using Employees.Repository.DTOs;

namespace Employees.Repository.Interfaces
{
	public interface IEmployeeProjectRepository
	{
        public Task AddNonExistingEmployeeProjectsAsync(ICollection<EmployeeProjectDto> employeeProjects);
    }
}