using System;
using Employees.DataAccess;
using Employees.DataAccess.DataModels;
using Employees.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Employees.Repository
{
	public class EmployeeRepository : IEmployeeRepository
	{
        private readonly EmployeesDbContext _context;

		public EmployeeRepository(EmployeesDbContext context)
		{
            _context = context;
		}

        public async Task AddNonExistingEmployeesAsync(ICollection<int> employeeIds)
        {
            var uniqueEmployeeIds = employeeIds.Distinct();
            foreach (var employeeId in uniqueEmployeeIds)
            {
                if(!_context.Employees.Any(x => x.EmployeeId == employeeId))
                {
                    _context.Employees.Add(new Employee { EmployeeId = employeeId });
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}