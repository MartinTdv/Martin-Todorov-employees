using System;

namespace Employees.Models
{
	public class EmployeePairViewModel
	{
        public int EmployeeOneId { get; set; }
        public int EmployeeTwoId { get; set; }
        public int ProjectId { get; set; }
        public int DaysWorkedTogether { get; set; }
    }
}

