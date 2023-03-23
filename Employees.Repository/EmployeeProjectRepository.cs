using System;
using Employees.DataAccess;
using Employees.DataAccess.DataModels;
using Employees.Repository.DTOs;
using Employees.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Employees.Repository
{
	public class EmployeeProjectRepository : IEmployeeProjectRepository
	{
        private readonly EmployeesDbContext _context;

		public EmployeeProjectRepository(EmployeesDbContext context)
		{
            _context = context;
		}

        public async Task AddNonExistingEmployeeProjectsAsync(ICollection<EmployeeProjectDto> employeeProjects)
        {
            var uniqueEmployeeProjects = employeeProjects.DistinctBy(x => new { x.EmployeeId, x.ProjectId });
            foreach (var ep in employeeProjects)
            {
                if(!_context.EmployeeProjects.Any(x => x.EmployeeId == ep.EmployeeId && x.ProjectId == ep.ProjectId))
                {
                    _context.EmployeeProjects.Add(new EmployeeProject
                    {
                        EmployeeId = ep.EmployeeId,
                        ProjectId = ep.ProjectId,
                        StartDate = ep.StartDate,
                        EndDate = ep.EndDate,
                    });
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}