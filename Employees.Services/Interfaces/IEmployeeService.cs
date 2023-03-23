using System;
using Employees.DataAccess.DataModels;
using Employees.Services.Models;

namespace Employees.Services.Interfaces
{
	public interface IEmployeeService
	{
		public ICollection<EmployeePair> GetEmployeePairs();
		public Task SaveEmployeeDataAsync(ICollection<EmployeeData> employeeDatas);
    }
}

