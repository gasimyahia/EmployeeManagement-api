using EmployeeManagement.Contracts.v1.Query;
using EmployeeManagement.Contracts.v1.Requests.Queries;
using EmployeeManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Data
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> Search(string name,Gender? gender);
        Task<List<Employee>> GetEmployees(EmployeeQuery postQuery, PaginationFilter paginationFilter);
        Task<Employee> GetEmployee(int empId);
        Task<Employee> GetEmployeeByEmail(string email);
        Task<Employee> AddEmployee(Employee Employee);
        Task<Employee> UpdateEmployee(Employee Employee);
        Task DeleteEmployee(int empId);
    }
}
