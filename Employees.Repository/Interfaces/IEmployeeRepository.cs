using System;
using Employees.DataAccess.DataModels;

namespace Employees.Repository.Interfaces
{
	public interface IEmployeeRepository
	{
        public Task AddNonExistingEmployeesAsync(ICollection<int> employeeIds);
    }
}