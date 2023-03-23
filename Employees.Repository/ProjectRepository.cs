using System;
using Microsoft.EntityFrameworkCore;
using Employees.DataAccess;
using Employees.DataAccess.DataModels;
using Employees.Repository.DTO;
using Employees.Repository.DTOs;
using Employees.Repository.Interfaces;

namespace Employees.Repository
{
	public class ProjectRepository : IProjectRepository
	{
        private readonly EmployeesDbContext _context;

		public ProjectRepository(EmployeesDbContext context)
		{
            _context = context;
		}

        public async Task AddNonExistingProjectsAsync(ICollection<int> projectIds)
        {
            var uniqueProjectIds = projectIds.Distinct();
            foreach (var projectId in uniqueProjectIds)
            {
                if(!_context.Projects.Any(x => x.ProjectId == projectId))
                {
                    _context.Projects.Add(new Project { ProjectId = projectId });
                }
            }
            await _context.SaveChangesAsync();
        }

        public ICollection<ProjectDto> GetAllAsNoTracking()
        {
            return _context.Projects
                .AsNoTracking()
                .Select(x => new ProjectDto
                {
                    ProjectId = x.ProjectId,
                    ProjectEmployees = x.ProjectEmployees
                    .Select(
                        x => new EmployeeProjectDto
                        {
                            EmployeeId = x.EmployeeId,
                            ProjectId = x.ProjectId,
                            StartDate = x.StartDate,
                            EndDate = x.EndDate,
                        })
                    .ToList(),
                })
                .ToList();
        }
    }
}