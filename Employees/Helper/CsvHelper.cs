using System;
using System.Globalization;
using System.IO.Compression;
using System.Diagnostics;
using Employees.Services.Models;

namespace Employees.Helper
{
	public static class CsvHelper
	{
        public static IEnumerable<EmployeeData> ConvertCsvToEmployeeDatas(IFormFile file)
        {
            var employeeData = new List<EmployeeData>();
            using (var fileStream = file.OpenReadStream())
            {
                using (var reader = new StreamReader(fileStream))
                {
                    string row;
                    while ((row = reader.ReadLine()) != null)
                    {
                        var rowData = row.Split(',');
                        employeeData.Add(new EmployeeData
                        {
                            EmployeeId = int.Parse(rowData[0]),
                            ProjectId = int.Parse(rowData[1]),
                            StartDate = DateTime.Parse(rowData[2]),
                            EndDate = rowData[3] != "NULL" ? DateTime.Parse(rowData[3]) : null,
                        });
                    }
                }
            }

            return employeeData;
        }
    }
}

