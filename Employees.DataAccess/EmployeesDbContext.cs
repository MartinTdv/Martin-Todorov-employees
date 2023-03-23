using System;
using Employees.DataAccess.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Employees.DataAccess
{
	public class EmployeesDbContext : DbContext
	{
		public EmployeesDbContext()
		{
		}
		public EmployeesDbContext(DbContextOptions<EmployeesDbContext> options)
			:base(options)
		{
		}

		public DbSet<Employee> Employees { get; set; }
		public DbSet<Project> Projects { get; set; }
		public DbSet<EmployeeProject> EmployeeProjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.Entity<Employee>()
				.Property(x => x.EmployeeId)
				.ValueGeneratedNever();
            modelBuilder.Entity<Project>()
                .Property(x => x.ProjectId)
                .ValueGeneratedNever();
			modelBuilder.Entity<EmployeeProject>()
				.HasKey(x => new { x.EmployeeId, x.ProjectId });
			modelBuilder.Entity<EmployeeProject>()
				.HasOne(x => x.Employee)
				.WithMany(x => x.EmployeeProjects);
            modelBuilder.Entity<EmployeeProject>()
				.HasOne(x => x.Project)
				.WithMany(x => x.ProjectEmployees);

            base.OnModelCreating(modelBuilder);
        }
    }
}