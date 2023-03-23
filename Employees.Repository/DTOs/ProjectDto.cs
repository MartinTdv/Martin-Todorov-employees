using System;
using Employees.Repository.DTOs;

namespace Employees.Repository.DTO
{
	public class ProjectDto
	{
		public int ProjectId { get; set; }
		public ICollection<EmployeeProjectDto> ProjectEmployees { get; set; } = new List<EmployeeProjectDto>();
	}
}