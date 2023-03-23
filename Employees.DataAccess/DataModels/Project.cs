using System;

namespace Employees.DataAccess.DataModels
{
	public class Project
	{
		public int ProjectId { get; set; }

		public ICollection<EmployeeProject> ProjectEmployees { get; set; } = new List<EmployeeProject>();
    }
}