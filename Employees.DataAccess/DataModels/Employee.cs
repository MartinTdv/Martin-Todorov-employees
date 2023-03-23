using System;

namespace Employees.DataAccess.DataModels
{
	public class Employee
	{
		public int EmployeeId { get; set; }

		public ICollection<EmployeeProject> EmployeeProjects { get; set; } = new List<EmployeeProject>();
    }
}

