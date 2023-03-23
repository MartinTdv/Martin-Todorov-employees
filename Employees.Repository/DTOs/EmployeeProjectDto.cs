using System;
using Employees.DataAccess.DataModels;

namespace Employees.Repository.DTOs
{
    public class EmployeeProjectDto
    {
        public int EmployeeId { get; set; }
        public int ProjectId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}