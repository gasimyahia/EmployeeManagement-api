using EmployeeManagement.Contracts.v1.Query;
using EmployeeManagement.Contracts.v1.Requests.Queries;
using EmployeeManagement.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetBook4.Data;

namespace EmployeeManagement.Data
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext appDbContext;
        public EmployeeRepository(AppDbContext DbContext)
        {
            this.appDbContext = DbContext;
        }
        public async Task<Employee> AddEmployee(Employee Employee)
        {
            // to ignore department object by entity framework
            if(Employee.Department != null)
            {
                appDbContext.Entry(Employee.Department).State = EntityState.Unchanged;
            }
            var result = await appDbContext.Employees.AddAsync(Employee);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteEmployee(int empId)
        {
            var result = await appDbContext.Employees.FirstOrDefaultAsync(x => x.EmployeeId == empId);
            if(result != null)
            {
                appDbContext.Employees.Remove(result);
                await appDbContext.SaveChangesAsync();
            }
        }

        public async Task<Employee> GetEmployee(int empId)
        {
            return await appDbContext.Employees.Include(e=> e.Department).FirstOrDefaultAsync(x => x.EmployeeId == empId);
        }

        public async Task<Employee> GetEmployeeByEmail(string email)
        {
            return await appDbContext.Employees.Include(e => e.Department).FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<List<Employee>> GetEmployees(EmployeeQuery empQuery, PaginationFilter paginationFilter)
        {
            var queryable = appDbContext.Employees.AsQueryable();
            if(empQuery.DeptId != null)
            {
                queryable = queryable.Where(em => em.DeptId == empQuery.DeptId);
            }
            if (empQuery.Gender != null)
            {
                queryable = queryable.Where(em => em.Gender == empQuery.Gender);
            }
            if (paginationFilter == null)
            {
                return await queryable.Include(e => e.Department).ToListAsync();
            }
            var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;
            return await queryable.Include(p => p.Department).Skip(skip).Take(paginationFilter.PageSize).OrderBy(d => d.DeptId).ToListAsync();
        }

        public async Task<IEnumerable<Employee>> Search(string name, Gender? gender)
        {
            IQueryable<Employee> query = appDbContext.Employees;
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.FirstName.Contains(name) || e.LastName.Contains(name));
            }
            if (gender != null)
            {
                query = query.Where(e => e.Gender==gender);
            }
            return await query.ToListAsync();
        }

        public async Task<Employee> UpdateEmployee(Employee Employee)
        {
            var result = await appDbContext.Employees.FirstOrDefaultAsync(e => e.EmployeeId == Employee.EmployeeId);
            if(result != null)
            {
                result.FirstName = Employee.FirstName;
                result.LastName = Employee.LastName;
                result.Email = Employee.Email;
                result.DateOfBrith = Employee.DateOfBrith;
                result.Gender = Employee.Gender;
                result.DeptId = Employee.DeptId;
                result.PhotoPath = Employee.PhotoPath;

                await appDbContext.SaveChangesAsync();
                return result;
            }
            return null;

        }
    }
}
