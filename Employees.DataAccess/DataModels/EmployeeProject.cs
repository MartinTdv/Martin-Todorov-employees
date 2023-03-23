using System;
using Microsoft.EntityFrameworkCore;

namespace Employees.DataAccess.DataModels
{
    public class EmployeeProject
	{
		public int EmployeeId { get; set; }
		public int ProjectId { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime? EndDate { get; set; }

        public Employee? Employee { get; set; }
        public Project? Project { get; set; }
    }
}

