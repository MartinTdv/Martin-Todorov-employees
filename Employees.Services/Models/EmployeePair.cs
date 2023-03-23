using System;
namespace Employees.Services.Models
{
	public class EmployeePair
	{
		public int EmployeeOneId { get; set; }
		public int EmployeeTwoId { get; set; }
		public int ProjectId { get; set; }
		public int DaysWorkedTogether { get; set; }
    }
}

