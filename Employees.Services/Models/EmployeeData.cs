using System;
namespace Employees.Services.Models
{
	public class EmployeeData
	{
		public int EmployeeId { get; set; }
		public int ProjectId { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime? EndDate { get; set; }
	}
}

