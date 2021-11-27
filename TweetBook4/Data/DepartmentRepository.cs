using EmployeeManagement.Contracts.v1.Query;
using EmployeeManagement.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetBook4.Data;

namespace EmployeeManagement.Data
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _dbContext;
        public DepartmentRepository(AppDbContext appDbContext)
        {
            this._dbContext = appDbContext;
        }
        public async Task<Department> AddDepartment(Department Department)
        {
            var result = await _dbContext.Departments.AddAsync(Department);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteDepartment(int deptId)
        {
            var result = await _dbContext.Departments.FirstOrDefaultAsync(d => d.DeptId == deptId);
            if(result != null)
            {
                _dbContext.Departments.Remove(result);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<Department> GetDepartment(int deptId)
        {
            return await _dbContext.Departments.FirstOrDefaultAsync(d => d.DeptId == deptId);
        }

        public async Task<List<Department>> GetDepartments(DeptQuery DeptQuery = null, PaginationFilter paginationFilter = null)
        {
            var queryable = _dbContext.Departments.AsQueryable();
            if (DeptQuery.Name != null)
            {
                queryable = queryable.Where(em => em.DeptName == DeptQuery.Name);
            }
            if (paginationFilter == null)
            {
                return await queryable.ToListAsync();
            }
            var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;
            return await queryable.Skip(skip).Take(paginationFilter.PageSize).OrderBy(d=>d.DeptId).ToListAsync();
        }

        public async Task<IEnumerable<Department>> Search(string name)
        {
            IQueryable<Department> query = _dbContext.Departments;
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(d => d.DeptName.Contains(name));
            }
            return await query.ToListAsync();
        }

        public async Task<Department> UpdateDepartment(Department Department)
        {
            var result = await _dbContext.Departments.FirstOrDefaultAsync(d => d.DeptId == Department.DeptId);
            if(result != null)
            {
                result.DeptName = Department.DeptName;
                await _dbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}
