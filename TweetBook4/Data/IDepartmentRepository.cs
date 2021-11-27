using EmployeeManagement.Contracts.v1.Query;
using EmployeeManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Data
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> Search(string name);
        Task<List<Department>> GetDepartments(DeptQuery DeptQuery = null, PaginationFilter paginationFilter = null);
        Task<Department> GetDepartment(int deptId);
        Task<Department> AddDepartment(Department Department);
        Task<Department> UpdateDepartment(Department Department);
        Task DeleteDepartment(int deptId);
    }
}
