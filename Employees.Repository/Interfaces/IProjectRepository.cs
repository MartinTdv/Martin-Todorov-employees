using System;
using Employees.DataAccess.DataModels;
using Employees.Repository.DTO;

namespace Employees.Repository.Interfaces
{
	public interface IProjectRepository
	{
        public ICollection<ProjectDto> GetAllAsNoTracking();
        public Task AddNonExistingProjectsAsync(ICollection<int> projectIds);
    }
}