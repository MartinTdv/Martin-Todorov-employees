using System;
using System.ComponentModel.DataAnnotations;

namespace Employees.Models
{
	public class UploadInputModel
	{
        [Required]
        public IFormFile EmployeesData { get; set; }
    }
}