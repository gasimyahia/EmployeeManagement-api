using EmployeeManagement.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using TweetBook4.Domain;

namespace TweetBook4.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<RefreshToken> RefreshToken { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // seed Department table
            builder.Entity<Department>().HasData(new Department { DeptId=1,DeptName="HR"});
            builder.Entity<Department>().HasData(new Department { DeptId = 2, DeptName = "IT" });

            // Seed Employee Table
            builder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 1,
                FirstName = "ahmed",
                LastName = "omer",
                Email = "ahmed@gmail.com",
                DateOfBrith = new DateTime(1990, 5, 15),
                DeptId=1,
                Gender=Gender.Male,
                PhotoPath="Image/emp.jpg"
            });
            builder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 2,
                FirstName = "oasman",
                LastName = "ali",
                Email = "oasman@gmail.com",
                DateOfBrith = new DateTime(1994, 5, 15),
                DeptId = 2,
                Gender = Gender.Male,
                PhotoPath = "Image/emp.jpg"
            });
        }

    }
}
